using System;
using System.Collections.Generic;
using System.Text;
using PriceMonitor.Enums;
using PriceMonitor.Events;

namespace PriceMonitor
{
    public class Trigger
    {
        public TriggerTypeEnum TriggerType { get; set; }
        public TriggerDirectionEnum TriggerDirection { get; set; }
        public double? Threshold { get; set; }
        public double? LastNotificationPrice { get; set; }
        public double Sensitivity { get; set; }

        public event EventHandler<ThresholdReachedEventArgs> TriggerThresholdReached;

        public virtual void OnTriggerThresholdReached(ThresholdReachedEventArgs e)
        {
            EventHandler<ThresholdReachedEventArgs> handler = TriggerThresholdReached;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public void UpdateValue(double threshold, double sensitivity, TriggerTypeEnum triggerType, TriggerDirectionEnum? triggerDirection)
        {
            Threshold = threshold;
            Sensitivity = sensitivity;
            TriggerType = triggerType;
            TriggerDirection = triggerDirection ?? TriggerDirectionEnum.Both;
        }
    }
}
