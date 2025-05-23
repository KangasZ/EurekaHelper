﻿using Dalamud.Game;
using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Game.Text.SeStringHandling;
using Dalamud.Game.Text.SeStringHandling.Payloads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Dalamud.Plugin.Services;
using EurekaHelper.Windows;
using EurekaHelper.XIV;
using EurekaHelper.XIV.Zones;

namespace EurekaHelper.System
{
    public class ElementalManager : IDisposable
    {
        public List<EurekaElemental> Elementals = new();

        public ElementalManager()
        {
            DalamudApi.ClientState.TerritoryChanged += OnTerritoryChanged;

            if (Utils.IsPlayerInEurekaZone(DalamudApi.ClientState.TerritoryType))
                DalamudApi.Framework.Update += OnUpdate;
        }

        private void OnTerritoryChanged(ushort territoryId)
        {
            if (Utils.IsPlayerInEurekaZone(territoryId))
            {
                DalamudApi.Framework.Update += OnUpdate;
                PluginWindow.ResetDefaultIcon();

                if (EurekaHelper.Config.ElementalAlwaysClear)
                    Elementals.Clear();
            }
            else
            {
                DalamudApi.Framework.Update -= OnUpdate;
            }
        }

        private void OnUpdate(IFramework framework)
        {
            if (!EurekaHelper.Config.ElementalCrowdsource && !EurekaHelper.Config.DisplayElemental && !EurekaHelper.Config.DisplayElementalToast)
                return;

            var elementals = DalamudApi.ObjectTable.Where(x => x is IBattleNpc bnpc && Constants.EurekaElementals.Contains(bnpc.NameId));
            foreach (var elemental in elementals)
            {
                if (Elementals.Exists(x => x.ObjectId == elemental.EntityId))
                {
                    var match = Elementals.FirstOrDefault(x => x.ObjectId == elemental.EntityId);
                    if (match == null)
                        continue;

                    match.LastSeen = DateTimeOffset.Now.ToUnixTimeSeconds();
                    Elementals.Sort((x, y) => x.LastSeen.CompareTo(y.LastSeen));
                    continue;
                }

                var eurekaElemental = new EurekaElemental(elemental.Name.TextValue, DalamudApi.ClientState.TerritoryType, elemental.Position, elemental.EntityId);
                Elementals.Add(eurekaElemental);
                Elementals.Sort((x, y) => x.LastSeen.CompareTo(y.LastSeen));

                if (EurekaHelper.Config.ElementalCrowdsource)
                {
                    var knownLocations = GetKnownLocations(DalamudApi.ClientState.TerritoryType);
                    if (!knownLocations.Any(x => Utils.IsWithinMinimumDistance(x, eurekaElemental.RawPosition, 15.0f)))
                    {
                        EurekaHelper.PrintMessage("Elemental found that is not in the plugin database.");
                        EurekaHelper.PrintMessage("Please send the following information to the developer on GitHub or Discord DM. You can find the contact information in the \"About\" tab.");
                        EurekaHelper.PrintMessage("You can also opt-out of crowdsourcing for Elemental positions in the \"Elementals\" tab.");
                        EurekaHelper.PrintMessage($"Send -> T: {DalamudApi.ClientState.TerritoryType} X: {eurekaElemental.RawPosition.X} Y: {eurekaElemental.RawPosition.Y} Z: {eurekaElemental.RawPosition.Z}");
                    }
                }

                var sb = new SeStringBuilder()
                .AddText($"{eurekaElemental.Name}: ")
                .Append(eurekaElemental.GetMapLink());

                if (EurekaHelper.Config.DisplayElementalToast)
                    DalamudApi.ToastGui.ShowQuest(sb.BuiltString);

                if (EurekaHelper.Config.DisplayElemental)
                {
                    DalamudApi.PluginInterface.RemoveChatLinkHandler(eurekaElemental.ObjectId);

                    if (EurekaHelper.Config.ElementalPayloadOptions != PayloadOptions.Nothing)
                    {
                        DalamudLinkPayload payload = DalamudApi.PluginInterface.AddChatLinkHandler(eurekaElemental.ObjectId, (i, m) =>
                        {
                            Utils.SetFlagMarker(eurekaElemental.TerritoryId, eurekaElemental.MapId, eurekaElemental.Position);

                            switch (EurekaHelper.Config.ElementalPayloadOptions)
                            {
                                case PayloadOptions.CopyToClipboard:
                                    Utils.CopyToClipboard($"{eurekaElemental.Name} <flag>");
                                    break;

                                default:
                                case PayloadOptions.ShoutToChat:
                                    Utils.SendMessage($"/sh {eurekaElemental.Name} <flag>");
                                    break;
                            }
                        });

                        var text = EurekaHelper.Config.ElementalPayloadOptions switch
                        {
                            PayloadOptions.ShoutToChat => "shout",
                            PayloadOptions.CopyToClipboard => "copy",
                            _ => "shout"
                        };

                        sb.AddText(" ");
                        sb.AddUiForeground(32);
                        sb.Add(payload);
                        sb.AddText($"[Click to {text}]");
                        sb.Add(RawPayload.LinkTerminator);
                        sb.AddUiForegroundOff();
                    }

                    EurekaHelper.PrintMessage(sb.BuiltString);
                }

                if (EurekaHelper.Config.ElementalAutoMark)
                {
                    Utils.AddMapMarker(eurekaElemental.TerritoryId, eurekaElemental.RawPosition, PluginWindow.DefaultIcon, true);
                    PluginWindow.DefaultIcon++;

                    if (PluginWindow.DefaultIcon > 60476)
                        PluginWindow.ResetDefaultIcon();
                }
            }
        }

        public static List<Vector3> GetKnownLocations(ushort territoryId)
        {
            return territoryId switch
            {
                732 => EurekaAnemos.ElementalPositions,
                763 => EurekaPagos.ElementalPositions,
                795 => EurekaPyros.ElementalPositions,
                827 => EurekaHydatos.ElementalPositions,
                _ => throw new NotImplementedException(),
            };
        }

        public void Dispose()
        {
            DalamudApi.ClientState.TerritoryChanged -= OnTerritoryChanged;
            DalamudApi.Framework.Update -= OnUpdate;
        }
    }
}
