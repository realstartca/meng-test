using System;
using System.Collections.Generic;
using System.Text;
using PriceMonitor.Enums;

namespace PriceMonitor
{
    public class Trigger
    {
        public TriggerTypeEnum TriggerType { get; set; }
        public TriggerDirectionEnum TriggerDirection { get; set; }
        public double? Threshold { get; set; }
        public double? LastNotificationPrice { get; set; }
        public double Sensitivity { get; set; }
    }
}
