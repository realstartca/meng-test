using System;
using System.Collections.Generic;
using System.Text;

namespace PriceMonitor.Events
{
    public class ThresholdReachedEventArgs : EventArgs
    {
        public double Threshold { get; set; }
        public double Price { get; set; }
        public DateTime TimeReached { get; set; }
    }
}
