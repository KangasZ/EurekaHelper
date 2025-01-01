using System.Collections.Generic;

namespace EurekaHelper.XIV;

public enum BaseSoundEffect
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
    
public enum ChatSoundEffect
{
    ChatSoundEffect1 = 1,
    ChatSoundEffect2 = 2,
    ChatSoundEffect3 = 3,
    ChatSoundEffect4 = 4,
    ChatSoundEffect5 = 5,
    ChatSoundEffect6 = 6,
    ChatSoundEffect7 = 7,
    ChatSoundEffect8 = 8,
    ChatSoundEffect9 = 9,
    ChatSoundEffect10 = 10,
    ChatSoundEffect11 = 11,
    ChatSoundEffect12 = 12,
    ChatSoundEffect13 = 13,
    ChatSoundEffect14 = 14,
    ChatSoundEffect15 = 15,
    ChatSoundEffect16 = 16,
}

public class SoundEffects
{
    public static ChatSoundEffect MapOldToNew(BaseSoundEffect oldSoundEffect)
    {
        switch (oldSoundEffect)
        {
            case BaseSoundEffect.SoundEffect36:
                return ChatSoundEffect.ChatSoundEffect1;
            case BaseSoundEffect.SoundEffect37:
                return ChatSoundEffect.ChatSoundEffect2;
            case BaseSoundEffect.SoundEffect38:
                return ChatSoundEffect.ChatSoundEffect3;
            case BaseSoundEffect.SoundEffect39:
                return ChatSoundEffect.ChatSoundEffect4;
            case BaseSoundEffect.SoundEffect40:
                return ChatSoundEffect.ChatSoundEffect5;
            case BaseSoundEffect.SoundEffect41:
                return ChatSoundEffect.ChatSoundEffect6;
            case BaseSoundEffect.SoundEffect42:
                return ChatSoundEffect.ChatSoundEffect7;
            case BaseSoundEffect.SoundEffect43:
                return ChatSoundEffect.ChatSoundEffect8;
            case BaseSoundEffect.SoundEffect44:
                return ChatSoundEffect.ChatSoundEffect9;
            case BaseSoundEffect.SoundEffect45:
                return ChatSoundEffect.ChatSoundEffect10;
            case BaseSoundEffect.SoundEffect46:
                return ChatSoundEffect.ChatSoundEffect11;
            case BaseSoundEffect.SoundEffect47:
                return ChatSoundEffect.ChatSoundEffect12;
            case BaseSoundEffect.SoundEffect48:
                return ChatSoundEffect.ChatSoundEffect13;
            case BaseSoundEffect.SoundEffect49:
                return ChatSoundEffect.ChatSoundEffect14;
            case BaseSoundEffect.SoundEffect50:
                return ChatSoundEffect.ChatSoundEffect15;
            case BaseSoundEffect.SoundEffect51:
                return ChatSoundEffect.ChatSoundEffect16;
            case BaseSoundEffect.SoundEffect52:
                return ChatSoundEffect.ChatSoundEffect1;
            default:
                var soundEffectId = (uint)oldSoundEffect;
                if (soundEffectId is < 1 or > 16)
                {
                    return ChatSoundEffect.ChatSoundEffect1;
                }

                return (ChatSoundEffect)soundEffectId;
        }
    }
}