using System;
using System.Collections.Generic;
using EurekaHelper.XIV;
using FFXIVClientStructs.FFXIV.Client.UI;

namespace EurekaHelper.System
{
    public static class SoundManager
    {
        public static void PlayAlarmSoundEffect(EurekaAlarm alarm)
        {
            if (EurekaHelper.Config.GlobalUseChatSoundEffect)
            {
                PlaySoundEffect(alarm.ChatSoundEffect);
            }
            else
            {
                PlaySoundEffect(alarm.SoundEffect);
            }
        }
        
        public static void PlayNMSoundEffect()
        {
            if (EurekaHelper.Config.GlobalUseChatSoundEffect)
            {
                PlaySoundEffect(EurekaHelper.Config.NMChatSoundEffect);
            }
            else
            {
                PlaySoundEffect(EurekaHelper.Config.NMSoundEffect);
            }
        }
        
        public static void PlayBunnySoundEffect()
        {
            if (EurekaHelper.Config.GlobalUseChatSoundEffect)
            {
                PlaySoundEffect(EurekaHelper.Config.BunnyChatSoundEffect);
            }
            else
            {
                PlaySoundEffect(EurekaHelper.Config.BunnySoundEffect);
            }
        }
        
        public static void PlaySoundEffect(BaseSoundEffect soundEffect)
        {
            if (!Enum.IsDefined(typeof(BaseSoundEffect), soundEffect))
            {
                DalamudApi.Log.Error($"Invalid sound effect ID sent to SoundManager, silently failing. Report this to the plugin creator: {soundEffect}");
                soundEffect = BaseSoundEffect.SoundEffect36;
            }
            
            UIGlobals.PlaySoundEffect((uint)soundEffect);
        }
        
        public static void PlaySoundEffect(ChatSoundEffect soundEffect)
        {
            if (!Enum.IsDefined(typeof(ChatSoundEffect), soundEffect))
            {
                DalamudApi.Log.Error($"Invalid sound effect ID sent to SoundManager, silently failing. report this to the plugin creator: {soundEffect}");
                soundEffect = ChatSoundEffect.ChatSoundEffect1;
            }

            UIGlobals.PlayChatSoundEffect((uint)soundEffect);
        }
    }
}
