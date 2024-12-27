using System.Collections.Generic;
using EurekaHelper.XIV;
using FFXIVClientStructs.FFXIV.Client.UI;

namespace EurekaHelper.System
{
    public static class SoundManager
    {
        public static void PlaySoundEffect(ChatSoundEffect soundEffect)
        {
            unsafe
            {
                UIGlobals.PlayChatSoundEffect((uint)soundEffect);
            }
        }
    }
}
