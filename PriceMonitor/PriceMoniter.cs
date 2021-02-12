using System;
using System.Collections.Generic;
using System.Text;
using PriceMonitor.Enums;
using PriceMonitor.Events;

namespace PriceMonitor
{
    public class PriceMoniter
    {
        private double _price;

        public Trigger BuyTrigger { get; set; }
        public Trigger SellTrigger { get; set; }

        public event EventHandler<ThresholdReachedEventArgs> BuyTriggerThresholdReached;
        public event EventHandler<ThresholdReachedEventArgs> SellTriggerThresholdReached;

        public void SetPrice(double price)
        {
            try
            {
                // Check BuyTrigger
                if (BuyTrigger != null && BuyTrigger.Threshold != null)
                {
                    // If price rises
                    if (price > _price)
                    {
                        if (price >= BuyTrigger.Threshold && BuyTrigger.TriggerDirection != TriggerDirectionEnum.Drop)
                        {
                            bool shouldTrigger = true;

                            // if fluctuations less than Sensitivity value, do not send notification
                            if (BuyTrigger.LastNotificationThreshold != null && Math.Abs(price - _price) < Math.Abs(BuyTrigger.Sensitivity))
                            {
                                shouldTrigger = false;
                            }

                            if (shouldTrigger)
                            {
                                // Send BuyTrigger notificaiton
                                ThresholdReachedEventArgs args = new ThresholdReachedEventArgs();
                                args.Threshold = Convert.ToDouble(BuyTrigger.Threshold);
                                args.TimeReached = DateTime.Now;
                                OnBuyTriggerThresholdReached(args);
                                BuyTrigger.LastNotificationThreshold = BuyTrigger.Threshold;
                            }
                        }
                    }
                    // If price drops
                    else if (price < _price && price <= BuyTrigger.Threshold && BuyTrigger.TriggerDirection != TriggerDirectionEnum.Rise)
                    {
                        bool shouldTrigger = true;

                        // if fluctuations less than Sensitivity value, do not send notification
                        if (BuyTrigger.LastNotificationThreshold != null && Math.Abs(price - _price) < Math.Abs(BuyTrigger.Sensitivity))
                        {
                            shouldTrigger = false;
                        }

                        if (shouldTrigger)
                        {
                            // Send BuyTrigger notificaiton
                            ThresholdReachedEventArgs args = new ThresholdReachedEventArgs();
                            args.Threshold = Convert.ToDouble(BuyTrigger.Threshold);
                            args.TimeReached = DateTime.Now;
                            OnBuyTriggerThresholdReached(args);
                            BuyTrigger.LastNotificationThreshold = BuyTrigger.Threshold;
                        }
                    }
                }

                // Check Sell Trigger
                if (SellTrigger != null && SellTrigger.Threshold != null)
                {
                    // If price rises
                    if (price > _price)
                    {
                        if (price >= SellTrigger.Threshold && SellTrigger.TriggerDirection != TriggerDirectionEnum.Drop)
                        {
                            bool shouldTrigger = true;

                            // if fluctuations less than Sensitivity value, do not send notification
                            if (SellTrigger.LastNotificationThreshold != null && Math.Abs(price - _price) < Math.Abs(SellTrigger.Sensitivity))
                            {
                                shouldTrigger = false;
                            }

                            if (shouldTrigger)
                            {
                                // Send SellTrigger notificaiton
                                ThresholdReachedEventArgs args = new ThresholdReachedEventArgs();
                                args.Threshold = Convert.ToDouble(SellTrigger.Threshold);
                                args.TimeReached = DateTime.Now;
                                OnSellTriggerThresholdReached(args);
                                SellTrigger.LastNotificationThreshold = SellTrigger.Threshold;
                            }
                        }
                    }
                    // If price drops
                    else if (price < _price && price <= SellTrigger.Threshold && SellTrigger.TriggerDirection != TriggerDirectionEnum.Rise)
                    {
                        bool shouldTrigger = true;

                        // if fluctuations less than Sensitivity value, do not send notification
                        if (SellTrigger.LastNotificationThreshold != null && Math.Abs(price - _price) < Math.Abs(SellTrigger.Sensitivity))
                        {
                            shouldTrigger = false;
                        }

                        if (shouldTrigger)
                        {
                            // Send SellTrigger notificaiton
                            ThresholdReachedEventArgs args = new ThresholdReachedEventArgs();
                            args.Threshold = Convert.ToDouble(SellTrigger.Threshold);
                            args.TimeReached = DateTime.Now;
                            OnSellTriggerThresholdReached(args);
                            SellTrigger.LastNotificationThreshold = SellTrigger.Threshold;
                        }
                    }
                }
            }
            catch
            {
                // Log Error
            }

            _price = price;
        }
        
        public void SetTrigger(double threshold, double sensitivity, TriggerTypeEnum triggerType, TriggerDirectionEnum? triggerDirection)
        {
            Trigger trigger = new Trigger()
            {
                Threshold = threshold,
                Sensitivity = sensitivity,
                TriggerType = triggerType,
                TriggerDirection = triggerDirection ?? TriggerDirectionEnum.Both
            };

            if (triggerType == TriggerTypeEnum.Buy)
            {
                BuyTrigger = trigger;
            }
            else if (triggerType == TriggerTypeEnum.Sell)
            {
                SellTrigger = trigger;
            }
        }

        protected virtual void OnBuyTriggerThresholdReached(ThresholdReachedEventArgs e)
        {
            EventHandler<ThresholdReachedEventArgs> handler = BuyTriggerThresholdReached;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnSellTriggerThresholdReached(ThresholdReachedEventArgs e)
        {
            EventHandler<ThresholdReachedEventArgs> handler = SellTriggerThresholdReached;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
