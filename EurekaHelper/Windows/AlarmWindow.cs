﻿
using System;
using System.Linq;
using System.Numerics;
using Dalamud.Interface;
using Dalamud.Interface.Colors;
using Dalamud.Interface.Components;
using Dalamud.Interface.Windowing;
using EurekaHelper.System;
using EurekaHelper.XIV;
using EurekaHelper.XIV.Zones;
using ImGuiNET;

namespace EurekaHelper.Windows
{
    internal class AlarmWindow : Window, IDisposable
    {
        private readonly EurekaHelper Plugin = null!;

        public AlarmWindow(EurekaHelper plugin) : base("Eureka Helper - Alarms")
        {
            Plugin = plugin;
            SizeConstraints = new WindowSizeConstraints { MinimumSize = new Vector2(360, 350), MaximumSize = new Vector2(float.MaxValue, float.MaxValue) };
        }

        public void Dispose() { }

        private readonly float LabelSize = 100f;

        // Default values
        private bool IsInMenu = false;
        private string AlarmName = string.Empty;
        private AlarmType AlarmType = AlarmType.Weather;
        private TimeType TimeType = TimeType.Day;
        private EurekaWeather WeatherType = EurekaWeather.FairSkies;
        private ChatSoundEffect ChatSoundEffect = ChatSoundEffect.SoundEffect10;
        private ushort AlarmZone = 732;
        private int MinutesBefore = 5;

        public override void Draw()
        {
            if (ImGuiComponents.IconButton(FontAwesomeIcon.Plus))
            {
                IsInMenu = false;
                ImGui.OpenPopup("Add Alarm");
            }
            Utils.SetTooltip("Add an alarm");

            ImGui.PushStyleVar(ImGuiStyleVar.PopupBorderSize, 1f);
            ImGui.PushStyleColor(ImGuiCol.Border, ImGui.GetColorU32(ImGuiCol.TabActive));
            if (ImGui.BeginPopup("Add Alarm"))
            {
                if (!IsInMenu)
                {
                    // reset to default values
                    AlarmName = string.Empty;
                    AlarmType = AlarmType.Weather;
                    TimeType = TimeType.Day;
                    WeatherType = EurekaWeather.FairSkies;
                    ChatSoundEffect = ChatSoundEffect.SoundEffect10;
                    AlarmZone = 732;
                    MinutesBefore = 5;

                    IsInMenu = true;
                }

                ImGui.Text("Add Alarm");
                ImGui.SetNextItemWidth(LabelSize);
                ImGui.LabelText("##NameLabel", "Name:"); 
                ImGui.SameLine(); 
                ImGui.SetNextItemWidth(150f);
                ImGui.InputTextWithHint("##Name", "Name of alarm", ref AlarmName, 15);

                ImGui.SetNextItemWidth(LabelSize);
                ImGui.LabelText("##TypeLabel", "Type:"); 
                ImGui.SameLine(); 
                ImGui.SetNextItemWidth(150f);
                var currentAlarmType = Array.IndexOf(Enum.GetValues<AlarmType>(), AlarmType);
                if (ImGui.Combo("##AlarmTypeCombo", ref currentAlarmType, Enum.GetNames<AlarmType>(), Enum.GetNames<AlarmType>().Length))
                    AlarmType = Enum.GetValues<AlarmType>()[currentAlarmType];

                if (AlarmType == AlarmType.Weather)
                {
                    ImGui.SetNextItemWidth(LabelSize);
                    ImGui.LabelText("##ZoneLabel", "Zone:"); 
                    ImGui.SameLine(); 
                    ImGui.SetNextItemWidth(150f);
                    var allZones = Constants.EurekaZones.Select(Utils.GetZoneName).ToArray();
                    var currentZone = Array.IndexOf(allZones, Utils.GetZoneName(AlarmZone));
                    if (ImGui.Combo("##AlarmZoneCombo", ref currentZone, allZones, 4))
                        AlarmZone = Constants.EurekaZones[currentZone];

                    ImGui.SetNextItemWidth(LabelSize);
                    ImGui.LabelText("##WeatherLabel", "Weather:"); 
                    ImGui.SameLine(); 
                    ImGui.SetNextItemWidth(150f);

                    // each zone has a different set of weathers:
                    var selectedZoneWeathers = AlarmZone switch
                    {
                        732 => EurekaAnemos.GetZoneWeathers().ToArray(),
                        763 => EurekaPagos.GetZoneWeathers().ToArray(),
                        795 => EurekaPyros.GetZoneWeathers().ToArray(),
                        827 => EurekaHydatos.GetZoneWeathers().ToArray(),
                        _ => throw new NotImplementedException(),
                    };

                    var currentWeather = Array.IndexOf(selectedZoneWeathers, WeatherType);
                    if (currentWeather == -1)
                    {
                        WeatherType = EurekaWeather.FairSkies;
                        currentWeather = Array.IndexOf(selectedZoneWeathers, EurekaWeather.FairSkies);
                    }

                    if (ImGui.Combo("##WeatherCombo", ref currentWeather, selectedZoneWeathers.Select(x => x.ToFriendlyString()).ToArray(), selectedZoneWeathers.Length))
                        WeatherType = selectedZoneWeathers[currentWeather];
                }
                else
                {
                    ImGui.SetNextItemWidth(LabelSize);
                    ImGui.LabelText("##TimeLabel", "Time:"); 
                    ImGui.SameLine(); 
                    ImGui.SetNextItemWidth(150f);
                    var currentTimeType = Array.IndexOf(Enum.GetValues<TimeType>(), TimeType);
                    if (ImGui.Combo("##TimeTypeCombo", ref currentTimeType, Enum.GetNames<TimeType>(), Enum.GetNames<TimeType>().Length))
                        TimeType = Enum.GetValues<TimeType>()[currentTimeType];
                }

                ImGui.SetNextItemWidth(LabelSize);
                ImGui.LabelText("##SoundLabel", "Sound Effect:"); 
                ImGui.SameLine(); 
                ImGui.SetNextItemWidth(150f);
                var currentSoundEffect = Array.IndexOf(Enum.GetValues<ChatSoundEffect>(), ChatSoundEffect);
                if (ImGui.Combo("##Sound", ref currentSoundEffect, Enum.GetNames<ChatSoundEffect>(), Enum.GetNames<ChatSoundEffect>().Length))
                {
                    ChatSoundEffect = Enum.GetValues<ChatSoundEffect>()[currentSoundEffect];
                    SoundManager.PlaySoundEffect(ChatSoundEffect);
                }

                ImGui.SetNextItemWidth(LabelSize);
                ImGui.LabelText("##TimeLabel", "Minutes Before:");
                ImGui.SameLine();
                ImGui.SetNextItemWidth(150f);
                ImGui.SliderInt("##TimeSlider", ref MinutesBefore, 1, 20, "%d", ImGuiSliderFlags.NoInput);

                if (ImGui.Button("Add", new Vector2(ImGui.GetContentRegionAvail().X, 0.0f))) 
                {
                    Plugin.AlarmManager.AddAlarm(CreateAlarm());
                    ImGui.CloseCurrentPopup();
                }

                ImGui.EndPopup();
            }

            ImGui.PopStyleVar();
            ImGui.PopStyleColor();

            ImGui.SameLine();
            if (ImGui.Button("Delete All"))
                Plugin.AlarmManager.DeleteAlarm(null, true); 
            
            ImGuiComponents.HelpMarker("Wow, you can now edit alarms.");

            ImGui.Separator();
            ImGui.Text("Your Alarms");

            ImGui.PushStyleColor(ImGuiCol.Border, ImGui.GetColorU32(ImGuiCol.TabActive));
            ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(0.0f, 0.0f));
            ImGui.BeginChild("AlarmDisplay", ImGui.GetContentRegionAvail(), true);
            ImGui.PopStyleColor();
            ImGui.PopStyleVar();

            DrawAlarmTable();

            ImGui.EndChild();
        }

        private void DrawAlarmTable()
        {
            if (ImGui.BeginTable("AlarmTable", 3, ImGuiTableFlags.Resizable | ImGuiTableFlags.BordersInnerH | ImGuiTableFlags.BordersV | ImGuiTableFlags.NoBordersInBody | ImGuiTableFlags.ScrollY | ImGuiTableFlags.NoSavedSettings))
            {
                ImGui.TableSetupColumn("Alarm Name");
                ImGui.TableSetupColumn("Timeleft");
                ImGui.TableSetupColumn("Alarm Configurations / Edit", ImGuiTableColumnFlags.WidthFixed);
                ImGui.TableHeadersRow();

                for (int i = EurekaHelper.Config.Alarms.Count - 1; i >= 0; i--)
                {
                    var alarm = EurekaHelper.Config.Alarms[i];

                    ImGui.TableNextColumn();
                    ImGui.Text(alarm.Name);
                    if (ImGui.IsItemHovered())
                    {
                        ImGui.PushStyleVar(ImGuiStyleVar.PopupBorderSize, 1f);
                        ImGui.PushStyleColor(ImGuiCol.Border, ImGui.GetColorU32(ImGuiCol.TabActive));
                        ImGui.BeginTooltip();

                        ImGui.TextColored(ImGuiColors.DalamudOrange, "Alarm Information");
                        ImGui.Text("Name:");
                        ImGui.SameLine();
                        ImGui.Text(alarm.Name);

                        ImGui.Text("Type:");
                        ImGui.SameLine();
                        ImGui.Text(alarm.Type.ToString());

                        if (alarm.Type == AlarmType.Weather)
                        {
                            ImGui.Text("Zone:");
                            ImGui.SameLine();
                            ImGui.Text(Utils.GetZoneName(alarm.ZoneId));

                            ImGui.Text("Weather:");
                            ImGui.SameLine();
                            ImGui.Text(alarm.Weather.ToFriendlyString());
                        }
                        else
                        {
                            ImGui.Text("Time:");
                            ImGui.SameLine();
                            ImGui.Text(alarm.TimeType.ToString());
                        }

                        ImGui.Text("Sound Effect:");
                        ImGui.SameLine();
                        ImGui.Text(alarm.ChatSoundEffect.ToString());

                        ImGui.Text("Minutes Before:");
                        ImGui.SameLine();
                        ImGui.Text(alarm.MinutesOffset.ToString());

                        ImGui.EndTooltip();
                        ImGui.PopStyleVar();
                        ImGui.PopStyleColor();
                    }


                    ImGui.TableNextColumn();
                    var (start, end) = AlarmManager.GetUptime(alarm);
                    var now = DateTime.Now.AddMinutes(alarm.MinutesOffset);
                    if (start > now)
                    {
                        var diff = start - now;
                        Utils.RightAlignTextInColumn($"{(diff.ToString(diff.Hours > 0 ? "hh'h 'mm'm 'ss's'" : "mm'm 'ss's'"))}");
                    }
                    else
                    {
                        Utils.RightAlignTextInColumn("Triggered", ImGuiColors.ParsedGreen);
                        if (ImGui.IsItemHovered())
                        {
                            ImGui.PushStyleVar(ImGuiStyleVar.PopupBorderSize, 1f);
                            ImGui.PushStyleColor(ImGuiCol.Border, ImGui.GetColorU32(ImGuiCol.TabActive));
                            ImGui.BeginTooltip();

                            ImGui.TextColored(ImGuiColors.DalamudOrange, "Uptime");

                            ImGui.Text("Start:");
                            ImGui.SameLine();
                            ImGui.Text($"{start:d MMM yyyy hh:mm tt}");

                            ImGui.Text("End:");
                            ImGui.SameLine();
                            ImGui.Text($"{end:d MMM yyyy hh:mm tt}");

                            ImGui.EndTooltip();
                            ImGui.PopStyleVar();
                            ImGui.PopStyleColor();
                        }
                    }

                    ImGui.TableNextColumn();
                    var enabled = alarm.Enabled;
                    var printMessage = alarm.PrintMessage;
                    var showToast = alarm.ShowToast;

                    if (ImGui.Checkbox($"##Toggle{alarm.ID}", ref enabled))
                        Plugin.AlarmManager.ToggleAlarm(alarm);
                    Utils.SetTooltip("Toggles the alarm to be enabled/disabled");
                    ImGui.SameLine();

                    if (ImGui.Checkbox($"##Print{alarm.ID}", ref printMessage))
                        Plugin.AlarmManager.SetAlarmPrintMessage(alarm, printMessage);
                    Utils.SetTooltip("Prints a message whenever the alarm is triggered");
                    ImGui.SameLine();

                    if (ImGui.Checkbox($"##Toast{alarm.ID}", ref showToast))
                        Plugin.AlarmManager.SetAlarmShowToast(alarm, showToast);
                    Utils.SetTooltip("Display a toast whenever the alarm is triggered");
                    ImGui.SameLine();

                    if (ImGuiComponents.IconButton($"##Edit{alarm.ID}", FontAwesomeIcon.Edit))
                    {
                        IsInMenu = false;
                        ImGui.OpenPopup($"Edit Alarm {alarm.ID}");
                    }
                    Utils.SetTooltip("Edit the current alarm");
                    ImGui.SameLine();

                    if (ImGuiComponents.IconButton($"##Delete{alarm.ID}", FontAwesomeIcon.Trash))
                        Plugin.AlarmManager.DeleteAlarm(alarm);
                    Utils.SetTooltip("Delete the current alarm");

                    ImGui.PushStyleVar(ImGuiStyleVar.PopupBorderSize, 1f);
                    ImGui.PushStyleColor(ImGuiCol.Border, ImGui.GetColorU32(ImGuiCol.TabActive));
                    if (ImGui.BeginPopup($"Edit Alarm {alarm.ID}"))
                    {
                        if (!IsInMenu)
                        {
                            // set values to existing alarm
                            AlarmName = alarm.Name;
                            AlarmType = alarm.Type;
                            TimeType = alarm.TimeType;
                            WeatherType = alarm.Weather;
                            ChatSoundEffect = alarm.ChatSoundEffect;
                            AlarmZone = alarm.ZoneId != 0 ? alarm.ZoneId : (ushort)732;
                            MinutesBefore = alarm.MinutesOffset;

                            IsInMenu = true;
                        }

                        ImGui.Text("Edit Alarm");
                        ImGui.SetNextItemWidth(LabelSize);
                        ImGui.LabelText("##NameLabel", "Name:");
                        ImGui.SameLine();
                        ImGui.SetNextItemWidth(150f);
                        ImGui.InputTextWithHint("##Name", "Name of alarm", ref AlarmName, 15);

                        ImGui.SetNextItemWidth(LabelSize);
                        ImGui.LabelText("##TypeLabel", "Type:");
                        ImGui.SameLine();
                        ImGui.SetNextItemWidth(150f);
                        var currentAlarmType = Array.IndexOf(Enum.GetValues<AlarmType>(), AlarmType);
                        if (ImGui.Combo("##AlarmTypeCombo", ref currentAlarmType, Enum.GetNames<AlarmType>(), Enum.GetNames<AlarmType>().Length))
                            AlarmType = Enum.GetValues<AlarmType>()[currentAlarmType];

                        if (AlarmType == AlarmType.Weather)
                        {
                            ImGui.SetNextItemWidth(LabelSize);
                            ImGui.LabelText("##ZoneLabel", "Zone:");
                            ImGui.SameLine();
                            ImGui.SetNextItemWidth(150f);
                            var allZones = Constants.EurekaZones.Select(Utils.GetZoneName).ToArray();
                            var currentZone = Array.IndexOf(allZones, Utils.GetZoneName(AlarmZone));
                            if (ImGui.Combo("##AlarmZoneCombo", ref currentZone, allZones, 4))
                                AlarmZone = Constants.EurekaZones[currentZone];

                            ImGui.SetNextItemWidth(LabelSize);
                            ImGui.LabelText("##WeatherLabel", "Weather:");
                            ImGui.SameLine();
                            ImGui.SetNextItemWidth(150f);

                            // each zone has a different set of weathers:
                            var selectedZoneWeathers = AlarmZone switch
                            {
                                732 => EurekaAnemos.GetZoneWeathers().ToArray(),
                                763 => EurekaPagos.GetZoneWeathers().ToArray(),
                                795 => EurekaPyros.GetZoneWeathers().ToArray(),
                                827 => EurekaHydatos.GetZoneWeathers().ToArray(),
                                _ => throw new NotImplementedException(),
                            };

                            var currentWeather = Array.IndexOf(selectedZoneWeathers, WeatherType);
                            if (currentWeather == -1)
                            {
                                WeatherType = EurekaWeather.FairSkies;
                                currentWeather = Array.IndexOf(selectedZoneWeathers, EurekaWeather.FairSkies);
                            }

                            if (ImGui.Combo("##WeatherCombo", ref currentWeather, selectedZoneWeathers.Select(x => x.ToFriendlyString()).ToArray(), selectedZoneWeathers.Length))
                                WeatherType = selectedZoneWeathers[currentWeather];
                        }
                        else
                        {
                            ImGui.SetNextItemWidth(LabelSize);
                            ImGui.LabelText("##TimeLabel", "Time:");
                            ImGui.SameLine();
                            ImGui.SetNextItemWidth(150f);
                            var currentTimeType = Array.IndexOf(Enum.GetValues<TimeType>(), TimeType);
                            if (ImGui.Combo("##TimeTypeCombo", ref currentTimeType, Enum.GetNames<TimeType>(), Enum.GetNames<TimeType>().Length))
                                TimeType = Enum.GetValues<TimeType>()[currentTimeType];
                        }

                        ImGui.SetNextItemWidth(LabelSize);
                        ImGui.LabelText("##SoundLabel", "Sound Effect:");
                        ImGui.SameLine();
                        ImGui.SetNextItemWidth(150f);
                        var currentSoundEffect = Array.IndexOf(Enum.GetValues<ChatSoundEffect>(), ChatSoundEffect);
                        if (ImGui.Combo("##Sound", ref currentSoundEffect, Enum.GetNames<ChatSoundEffect>(), Enum.GetNames<ChatSoundEffect>().Length))
                        {
                            ChatSoundEffect = Enum.GetValues<ChatSoundEffect>()[currentSoundEffect];
                            SoundManager.PlaySoundEffect(ChatSoundEffect);
                        }

                        ImGui.SetNextItemWidth(LabelSize);
                        ImGui.LabelText("##TimeLabel", "Minutes Before:");
                        ImGui.SameLine();
                        ImGui.SetNextItemWidth(150f);
                        ImGui.SliderInt("##TimeSlider", ref MinutesBefore, 1, 20, "%d", ImGuiSliderFlags.NoInput);

                        if (ImGui.Button("Update", new Vector2(ImGui.GetContentRegionAvail().X, 0.0f)))
                        {
                            UpdateAlarm(alarm);
                            ImGui.CloseCurrentPopup();
                        }

                        ImGui.EndPopup();
                    }

                    ImGui.PopStyleVar();
                    ImGui.PopStyleColor();
                }

                ImGui.EndTable();
            }
        }

        private EurekaAlarm CreateAlarm() => new()
        {
            Name = !string.IsNullOrEmpty(AlarmName) ? AlarmName : "Unamed",
            Type = AlarmType,
            ZoneId = AlarmType == AlarmType.Weather ? AlarmZone : (ushort)0,
            Weather = AlarmType == AlarmType.Weather ? WeatherType : 0,
            TimeType = AlarmType == AlarmType.Time ? TimeType : 0,
            ChatSoundEffect = ChatSoundEffect,
            MinutesOffset = MinutesBefore
        };

        private void UpdateAlarm(EurekaAlarm alarm)
        {
            var alarmName = !string.IsNullOrEmpty(AlarmName) ? AlarmName : "Unamed";
            var alarmType = AlarmType;
            var alarmZoneId = AlarmType == AlarmType.Weather ? AlarmZone : (ushort)0;
            var alarmWeather = AlarmType == AlarmType.Weather ? WeatherType : 0;
            var alarmTimeType = AlarmType == AlarmType.Time ? TimeType : 0;
            var alarmSoundEffect = ChatSoundEffect;
            var alarmMinutesOffset = MinutesBefore;

            alarm.Name = alarmName;
            alarm.ChatSoundEffect = alarmSoundEffect;
            EurekaHelper.Config.Save();

            // Check if values are updated
            if (alarmType == alarm.Type &&
                alarmTimeType == alarm.TimeType &&
                alarmWeather == alarm.Weather &&
                alarmZoneId == alarm.ZoneId &&
                alarmMinutesOffset == alarm.MinutesOffset)
                return;

            alarm.Type = alarmType;
            alarm.ZoneId = alarmZoneId;
            alarm.Weather = alarmWeather;
            alarm.TimeType = alarmTimeType;
            alarm.MinutesOffset = alarmMinutesOffset;

            Plugin.AlarmManager.UpdateAlarm(alarm);
        }
    }
}
