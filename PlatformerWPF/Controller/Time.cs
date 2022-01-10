using System;

namespace BattleCitySharp
{
    public class Time
    {
        public static float DeltaTime { get; private set; }

        private static DateTime time1 = DateTime.Now;
        private static DateTime time2 = DateTime.Now;

        public static void SetDeltaTime()
        {
            time2 = DateTime.Now;
            DeltaTime = (time2.Ticks - time1.Ticks) / 10000000f;
            time1 = time2;
        }
    }
}
