using System.Collections.Generic;

namespace EurekaHelper.XIV;

public enum OldSoundEffect
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
    SoundEffect1 = 1,
    SoundEffect2 = 2,
    SoundEffect3 = 3,
    SoundEffect4 = 4,
    SoundEffect5 = 5,
    SoundEffect6 = 6,
    SoundEffect7 = 7,
    SoundEffect8 = 8,
    SoundEffect9 = 9,
    SoundEffect10 = 10,
    SoundEffect11 = 11,
    SoundEffect12 = 12,
    SoundEffect13 = 13,
    SoundEffect14 = 14,
    SoundEffect15 = 15,
    SoundEffect16 = 16,
}

public class SoundEffects
{

    public static Dictionary<OldSoundEffect, ChatSoundEffect> oldToNewMapping = new()
    {
        { OldSoundEffect.SoundEffect36, ChatSoundEffect.SoundEffect1 },
        { OldSoundEffect.SoundEffect37, ChatSoundEffect.SoundEffect2 },
        { OldSoundEffect.SoundEffect38, ChatSoundEffect.SoundEffect3 },
        { OldSoundEffect.SoundEffect39, ChatSoundEffect.SoundEffect4 },
        { OldSoundEffect.SoundEffect40, ChatSoundEffect.SoundEffect5 },
        { OldSoundEffect.SoundEffect41, ChatSoundEffect.SoundEffect6 },
        { OldSoundEffect.SoundEffect42, ChatSoundEffect.SoundEffect7 },
        { OldSoundEffect.SoundEffect43, ChatSoundEffect.SoundEffect8 },
        { OldSoundEffect.SoundEffect44, ChatSoundEffect.SoundEffect9 },
        { OldSoundEffect.SoundEffect45, ChatSoundEffect.SoundEffect10 },
        { OldSoundEffect.SoundEffect46, ChatSoundEffect.SoundEffect11 },
        { OldSoundEffect.SoundEffect47, ChatSoundEffect.SoundEffect12 },
        { OldSoundEffect.SoundEffect48, ChatSoundEffect.SoundEffect13 },
        { OldSoundEffect.SoundEffect49, ChatSoundEffect.SoundEffect14 },
        { OldSoundEffect.SoundEffect50, ChatSoundEffect.SoundEffect15 },
        { OldSoundEffect.SoundEffect51, ChatSoundEffect.SoundEffect16 },
        { OldSoundEffect.SoundEffect52, ChatSoundEffect.SoundEffect1 }
    };

    public static ChatSoundEffect MapOldToNew(OldSoundEffect oldSoundEffect)
    {
        return oldToNewMapping.TryGetValue(oldSoundEffect, out var newSoundEffect) ? newSoundEffect : throw new KeyNotFoundException($"No mapping found for {oldSoundEffect}");
    }
}