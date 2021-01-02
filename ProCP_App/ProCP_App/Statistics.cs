using System;
using System.Timers;

namespace ProCP_App
{
    public class Statistics
    {
        private int totalCartsNeeded = 0;
        private int totalEmpsNeeded;
        private Belt b;
        public int transported = 0;
        public int availableCarts = 0;
        public int busyCarts = 0;
        private int cartCapacity;
        private double busyCartsTimerSpeed;

        public Statistics(Belt b, int cartCapacity)
        {
            this.b = b;
            this.cartCapacity = cartCapacity;
        }
        public void SetCartCapacity(int cartCapacity)
        {
            this.cartCapacity = cartCapacity;
        }
        public void SetBusyCartsTimerSpeed(double speed)
        {
            busyCartsTimerSpeed = speed * 1000;
        }

        public bool BagsReachedEnd()
        {
            if (b.Passed == b.MaxLuggage)
                return true;
            return false;
        }
        public int CalculateTotalCartsNeeded()
        {
            if (cartCapacity == 0) return 0;
            if (b.Passed - transported >= cartCapacity)
            {
                if (availableCarts == 0)
                {
                    availableCarts++;
                    totalCartsNeeded++;
                }
                availableCarts--;
                busyCarts++;
                transported += cartCapacity;
                Timer t = null;
                SetTimer(t);
            }
            else
            {
                if (BagsReachedEnd())
                {
                    if (availableCarts == 0)
                    {
                        availableCarts++;
                        totalCartsNeeded++;
                    }
                    availableCarts--;
                    busyCarts++;
                    transported += b.Passed % cartCapacity;
                    Timer t = null;
                    SetTimer(t);
                }
            }
            return totalCartsNeeded;
        }

        private void SetTimer(Timer t)
        {
            t = new Timer();
            t.Interval = busyCartsTimerSpeed;
            t.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            t.AutoReset = false;
            t.Start();
        }
        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            busyCarts--;
            availableCarts++;
        }
        public int CalculateTotalEmpsNeeded()
        {
            totalEmpsNeeded = totalCartsNeeded;
            return this.totalEmpsNeeded;
        }
        public string StatisticsInfo()
        {
            return $"Date: {DateTime.Now.ToString()}, Luggages: {transported}, Carts: {totalCartsNeeded}, Cart_Capacity: {cartCapacity}, Employees_Needed: {totalEmpsNeeded}";
        }
        public void ClearStatistics()
        {
            totalCartsNeeded = 0;
            totalEmpsNeeded = 0;
            transported = 0;
            availableCarts = 0;
            busyCarts = 0;
            cartCapacity = 0;
        }
        public int StatsCartsNeeded()
        { return CalculateTotalCartsNeeded(); }
        public int StatsEmpsNeeded()
        { return CalculateTotalEmpsNeeded(); }
        public int StatsAvailableCarts()
        { return availableCarts; }
        public int StatsBusyCarts()
        { return busyCarts; }
        public int StatsTransported()
        { return transported; }
    }
}