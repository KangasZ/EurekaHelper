using FFXIVClientStructs.FFXIV.Client.UI;

namespace EurekaHelperZ.System
{
    public enum SoundEffect
    {
        SoundEffect36 = 0x24,
        SoundEffect37 = 0x25,
        SoundEffect38 = 0x26,
        SoundEffect39 = 0x27,
        SoundEffect40 = 0x28,
        SoundEffect41 = 0x29,
        SoundEffect42 = 0x2A,
        SoundEffect43 = 0x2B,
        SoundEffect44 = 0x2C,
        SoundEffect45 = 0x2D,
        SoundEffect46 = 0x2E,
        SoundEffect47 = 0x2F,
        SoundEffect48 = 0x30,
        SoundEffect49 = 0x31,
        SoundEffect50 = 0x32,
        SoundEffect51 = 0x33,
        SoundEffect52 = 0x34
    }

    public static class SoundManager
    {
        public static void PlaySoundEffect(SoundEffect soundEffect)
        {
            unsafe
            {
                UIGlobals.PlaySoundEffect((uint)soundEffect);
            }
        }
    }
}
