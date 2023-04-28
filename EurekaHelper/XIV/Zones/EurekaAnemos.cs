﻿using System;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;

namespace EurekaHelper.XIV.Zones
{
    public class EurekaAnemos : IEurekaTracker
    {
        private readonly List<EurekaFate> Fates = new();

        private static readonly (int, EurekaWeather)[] Weathers = new (int, EurekaWeather)[]
        {
            (30, EurekaWeather.FairSkies),
            (60, EurekaWeather.Gales),
            (90, EurekaWeather.Showers),
            (100, EurekaWeather.Snow)
        };

        public static readonly List<Vector3> ElementalPositions = new()
        {
            new(-227.09471f, 35.390423f, 121.25729f),
            new(-364.4897f, 69.47017f, -276.5466f),
            new(-373.7721f, 82.72685f, 18.47673f),
            new(-399.8804f, 17.590218f, 291.9889f),
            new(-408.8093f, 27.3615f, 425.7613f),
            new(-464.80188f, 63.225822f, -435.7054f),
            new(-580.6493f, 41.84066f, -5.5684004f),
            new(-642.79144f, 41.421658f, -119.1096f),
            new(-732.5733f, 25.723625f, 180.26799f),
            new(13.26962f, 14.806233f, 300.034f),
            new(137.8132f, 37.18542f, -168.3959f),
            new(239.4716f, 40.204037f, 49.06235f),
            new(256f, 32.288704f, -330.4921f),
            new(262.5656f, 30.271093f, -113.7084f),
            new(364.5631f, 37.654808f, -410.9562f),
            new(396.8458f, 33.99593f, -62.532932f),
            new(567.33813f, 35.447132f, -79.25026f),
            new(643.54285f, 35.356274f, -306.5175f),
            new(686.88556f, 38.07274f, -154.0496f)
        };

        public EurekaAnemos(List<EurekaFate> fates) { Fates = fates; }

        public static EurekaAnemos GetTracker()
        {
            List<EurekaFate> AnemosFates = new()
            {
                new(1332, 1, 732, 414,  "Unsafety Dance", "Sabotender Corrido", "Sabo", new Vector2(14.0f, 22.0f), "Flowering Sabotender", new Vector2(14.0f, 22.0f), EurekaWeather.None, EurekaWeather.None, EurekaElement.Wind, EurekaElement.Wind, false),
                new(1348, 2, 732, 414,  "The Shadow over Anemos", "The Lord of Anemos", "Lord", new Vector2(30.0f, 27.0f), "Sea Bishop", new Vector2(14.0f, 22.0f), EurekaWeather.None, EurekaWeather.None, EurekaElement.Water, EurekaElement.Water, false),
                new(1333, 3, 732, 414,  "Teles House", "Teles", "Teles", new Vector2(25.9f, 27.0f), "Anemos Harpeia", new Vector2(25.9f, 27.0f), EurekaWeather.None, EurekaWeather.None, EurekaElement.Wind, EurekaElement.Wind, false),
                new(1328, 4, 732, 414,  "The Swarm Never Sets", "The Emperor of Anemos", "Emperor", new Vector2(17.1f, 22.2f), "Darner", new Vector2(17.1f, 22.2f), EurekaWeather.None, EurekaWeather.None, EurekaElement.Wind, EurekaElement.Wind, false),
                new(1344, 5, 732, 414,  "One Missed Callisto", "Callisto", "Callisto", new Vector2(26.0f, 22.0f), "Val Bear", new Vector2(26.0f, 22.0f), EurekaWeather.None, EurekaWeather.None, EurekaElement.Earth, EurekaElement.Earth, false),
                new(1347, 6, 732, 414,  "By Numbers", "Number", "Number", new Vector2(24.0f, 23.0f), "Pneumaflayer", new Vector2(24.0f, 23.0f), EurekaWeather.None, EurekaWeather.None, EurekaElement.Lightning, EurekaElement.Lightning, false),
                new(1345, 7, 732, 414,  "Disinherit the Wind", "Jahannam", "Jaha", new Vector2(19.1f, 19.6f), "Typhoon Sprite", new Vector2(19.1f, 19.6f), EurekaWeather.None, EurekaWeather.Gales, EurekaElement.Wind, EurekaElement.Wind, false),
                new(1334, 8, 732, 414,  "Prove Your Amemettle", "Amemet", "Amemet", new Vector2(15.0f, 16.0f), "Abraxas", new Vector2(15.0f, 16.0f), EurekaWeather.None, EurekaWeather.None, EurekaElement.Fire, EurekaElement.Fire, false),
                new(1335, 9, 732, 414,  "Caym What May", "Caym", "Caym", new Vector2(14.0f, 13.0f), "Stalker Ziz", new Vector2(14.0f, 13.0f), EurekaWeather.None, EurekaWeather.None, EurekaElement.Ice, EurekaElement.Ice, false),
                new(1336, 10, 732, 414, "The Killing of a Sacred Bombardier", "Bombadeel", "Bomba", new Vector2(28.2f, 20.3f), "Traveling Gourmand", new Vector2(28.2f, 20.3f), EurekaWeather.None, EurekaWeather.None, EurekaElement.Earth, EurekaElement.Earth, true),
                new(1339, 11, 732, 414, "Short Serket 2", "Serket", "Serket", new Vector2(24.9f, 18.2f), "Khor Claw", new Vector2(24.9f, 18.2f), EurekaWeather.None, EurekaWeather.None, EurekaElement.Earth, EurekaElement.Earth, false),
                new(1346, 12, 732, 414, "Don't Judge Me, Morbol", "Judgemental Julika", "Julika", new Vector2(21.9f, 14.5f), "Henbane", new Vector2(21.9f, 14.5f), EurekaWeather.None, EurekaWeather.None, EurekaElement.Ice, EurekaElement.Ice, false),
                new(1343, 13, 732, 414, "When You Ride Alone", "The White Rider", "Rider", new Vector2(20.0f, 13.0f), "Duskfall Dullahan", new Vector2(20.0f, 13.0f), EurekaWeather.None, EurekaWeather.None, EurekaElement.Lightning, EurekaElement.Lightning, true),
                new(1337, 14, 732, 414, "Sing, Muse", "Polyphemus", "Poly", new Vector2(26.0f, 14.0f), "Monoeye", new Vector2(26.0f, 14.0f), EurekaWeather.None, EurekaWeather.None, EurekaElement.Ice, EurekaElement.Ice, false),
                new(1342, 15, 732, 414, "Simurghasbord", "Simurgh's Strider", "Strider", new Vector2(29.0f, 13.0f), "Old World Zu", new Vector2(29.0f, 13.0f), EurekaWeather.None, EurekaWeather.None, EurekaElement.Wind, EurekaElement.Fire, false),
                new(1341, 16, 732, 414, "To the Mat", "King Hazmat", "Hazmat", new Vector2(35.0f, 18.0f), "Anemos Anala", new Vector2(35.0f, 18.0f), EurekaWeather.None, EurekaWeather.None, EurekaElement.Fire, EurekaElement.Fire, false),
                new(1331, 17, 732, 414, "Wine and Honey", "Fafnir", "Fafnir", new Vector2(36.0f, 22.0f), "Fossil Dragon", new Vector2(36.0f, 22.0f), EurekaWeather.None, EurekaWeather.None, EurekaElement.Fire, EurekaElement.Fire, true),
                new(1340, 18, 732, 414, "I Amarok", "Amarok", "Amarok", new Vector2(7.7f, 17.9f), "Voidscale", new Vector2(7.7f, 17.9f), EurekaWeather.None, EurekaWeather.None, EurekaElement.Ice, EurekaElement.Ice, false),
                new(1338, 19, 732, 414, "Drama Lamashtu", "Lamashtu", "Lamashtu", new Vector2(7.6f, 26.6f), "Val Specter", new Vector2(7.6f, 26.6f), EurekaWeather.None, EurekaWeather.None, EurekaElement.Wind, EurekaElement.Wind, true),
                new(1329, 20, 732, 414, "Wail in the Willows", "Pazuzu", "Paz", new Vector2(7.4f, 21.6f), "Shadow Wraith", new Vector2(8.6f, 20.2f), EurekaWeather.Gales, EurekaWeather.None, EurekaElement.Wind, EurekaElement.Fire, true)
            };

            Utils.GetFatePositionFromLgb(732, AnemosFates);

            return new EurekaAnemos(AnemosFates);
        }

        public List<EurekaFate> GetFates() => Fates;

        public (EurekaWeather Weather, TimeSpan Timeleft) GetCurrentWeatherInfo() => EorzeaWeather.GetCurrentWeatherInfo(Weathers);

        public static List<DateTime> GetWeatherForecast(EurekaWeather targetWeather, int count) =>
            EorzeaWeather.GetCountWeatherForecasts(targetWeather, count, Weathers);

        public List<(EurekaWeather Weather, TimeSpan Time)> GetAllNextWeatherTime() => EorzeaWeather.GetAllWeathers(Weathers);

        public void SetPopTimes(Dictionary<ushort, long> keyValuePairs)
        {
            var zoneFates = Fates.Where(x => x.IncludeInTracker).ToList();
            foreach (var fate in zoneFates)
            {
                if (keyValuePairs.ContainsKey((ushort)fate.TrackerId))
                    fate.SetKill(keyValuePairs[(ushort)fate.TrackerId]);
                else
                    fate.ResetKill();
            }
        }
    }
}
