using System;
using System.Collections.Generic;

namespace EurekaHelperZ.XIV
{
    public interface IEurekaTracker
    {
        List<EurekaFate> GetFates();

        (EurekaWeather Weather, TimeSpan Timeleft) GetCurrentWeatherInfo();

        List<(EurekaWeather Weather, TimeSpan Time)> GetAllNextWeatherTime();

        void SetPopTimes(Dictionary<ushort, long> keyValuePairs);
    }
}
