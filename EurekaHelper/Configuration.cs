using Dalamud.Configuration;
using Dalamud.Game.Text;
using System;
using System.Collections.Generic;
using EurekaHelper.System;
using EurekaHelper.XIV;

namespace EurekaHelper
{
    public enum PayloadOptions
    {
        ShoutToChat,
        CopyToClipboard,
        Nothing
    }

    [Serializable]
    public class Configuration : IPluginConfiguration
    {
        public int Version { get; set; } = 3;

        public void Initialize() 
        {
            if (CustomMessages.Count == 0)
            {
                CustomMessages.Add("/shout %bossName% POP. %flag%");
            }
            
            if (Version < 3)
            {
                Version = 3;
                NMChatSoundEffect = SoundEffects.MapOldToNew(NMSoundEffect);
                BunnyChatSoundEffect = SoundEffects.MapOldToNew(BunnySoundEffect);
                Alarms.ForEach(x => x.ChatSoundEffect = SoundEffects.MapOldToNew(x.SoundEffect));
            }
            
            if (!Enum.IsDefined(typeof(ChatSoundEffect), NMChatSoundEffect))
            {
                DalamudApi.Log.Error($"NM Sound Effect ID is invalid, resetting to default.");
                NMChatSoundEffect = ChatSoundEffect.ChatSoundEffect1;
            }
            if (!Enum.IsDefined(typeof(BaseSoundEffect), NMSoundEffect))
            {
                DalamudApi.Log.Error($"NM Chat Sound Effect ID is invalid, resetting to default.");
                NMSoundEffect = BaseSoundEffect.SoundEffect36;
            }
            if (!Enum.IsDefined(typeof(ChatSoundEffect), BunnyChatSoundEffect))
            {
                DalamudApi.Log.Error($"Bunny Sound Effect ID is invalid, resetting to default.");
                BunnyChatSoundEffect = ChatSoundEffect.ChatSoundEffect6;
            }
            if (!Enum.IsDefined(typeof(BaseSoundEffect), BunnySoundEffect))
            {
                DalamudApi.Log.Error($"Bunny Chat Sound Effect ID is invalid, resetting to default.");
                BunnySoundEffect = BaseSoundEffect.SoundEffect41;
            }
            
            foreach (var alarm in Alarms)
            {
                if (!Enum.IsDefined(typeof(BaseSoundEffect), alarm.SoundEffect))
                {
                    DalamudApi.Log.Error($"Alarm Sound Effect ID is invalid, resetting to default.");
                    alarm.SoundEffect = BaseSoundEffect.SoundEffect36;
                }
                if (!Enum.IsDefined(typeof(ChatSoundEffect), alarm.ChatSoundEffect))
                {
                    DalamudApi.Log.Error($"Alarm Chat Sound Effect ID is invalid, resetting to default.");
                    alarm.ChatSoundEffect = ChatSoundEffect.ChatSoundEffect1;
                }
            }
            
            Save();
        }

        /*
         * General Configurations
         */
        public XivChatType ChatChannel { get; set; } = XivChatType.Echo;

        /*
         * Tracker Configurations
         */
        public bool DisplayFateProgress = false;

        public bool DisplayBunnyFates = false;

        public bool DisplayFatePop = true;

        public bool PlayPopSound = true;

        public bool DisplayToastPop = false;

        public bool AutoPopFate = true;

        public bool RandomizeMapCoords = true;

        public bool AutoCreateTracker = false;

        public bool AutoPopFateWithinRange = false;
        public bool GlobalUseChatSoundEffect = false;

        public bool ShowLevelInTrackerTable = false;

        public List<string> CustomMessages { get; set; } = new();

        public BaseSoundEffect NMSoundEffect { get; set; } = BaseSoundEffect.SoundEffect36;

        public BaseSoundEffect BunnySoundEffect { get; set; } = BaseSoundEffect.SoundEffect41;
        public ChatSoundEffect NMChatSoundEffect { get; set; } = ChatSoundEffect.ChatSoundEffect1;
        public ChatSoundEffect BunnyChatSoundEffect { get; set; } = ChatSoundEffect.ChatSoundEffect6;

        public PayloadOptions PayloadOptions { get; set; } = PayloadOptions.ShoutToChat;

        /*
         * Server ID Configurations
         */
        public bool DisplayServerId = false;

        public bool DisplayServerIdInServerInfo = false;

        /*
         * Elemental Configurations
         */

        public bool DisplayElemental = true;

        public bool DisplayElementalToast = false;

        public bool ElementalCrowdsource = true;

        public bool ElementalAutoMark = false;

        public bool ElementalAlwaysClear = false;

        public PayloadOptions ElementalPayloadOptions { get; set;} = PayloadOptions.CopyToClipboard;

        /*
         * Relic Configurations
         */
        public List<uint> CompletedRelics { get; set; } = new();

        /*
         * Alarm Configurations
         */
        public List<EurekaAlarm> Alarms { get; set; } = new();

        public void Save() => DalamudApi.PluginInterface.SavePluginConfig(this);
    }
}
