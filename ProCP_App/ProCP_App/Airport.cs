using System.Collections.Generic;
using System.Linq;

namespace ProCP_App
{
    public class Airport
    {
        public Belt belt { get; private set; }
        public Statistics Statistics { get; set; }

        public int maxLuggage = 0;
        public Airport(int lug, int cartCapacity)
        {
            this.belt = new Belt(lug);
            this.maxLuggage = lug;
            this.Statistics = new Statistics(this.belt, cartCapacity);
        }

        public void UpdateBusyCartsSpeed(double speed)
        {
            this.Statistics.SetBusyCartsTimerSpeed(speed);
        }

        public int getStatsPassed()
        { return this.belt.Passed; }
        public int getStatsCartsNeeded()
        { return Statistics.StatsCartsNeeded(); }
        public int getStatsEmpsNeeded()
        { return Statistics.StatsEmpsNeeded(); }
        public int getStatsAvailableCarts()
        { return Statistics.StatsAvailableCarts(); }
        public int getStatsBusyCarts()
        { return Statistics.StatsBusyCarts(); }
        public int getStatsTransported()
        { return Statistics.StatsTransported(); }

        public string[] saveStatistics()
        {
            List<string> output = new List<string>();
            output.Add("Statistics: ");
            string info = this.Statistics.StatisticsInfo();
            string[] s = info.Split("\r\n".ToCharArray()).ToArray();
            for (int i = 0; i < s.Length; i += 2)
            {
                output.Add(s[i]);
            }
            return output.ToArray();
        }
    }
}
