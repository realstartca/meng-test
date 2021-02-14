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
                            if (BuyTrigger.LastNotificationPrice != null && Math.Abs(price - Convert.ToDouble(BuyTrigger.LastNotificationPrice)) < Math.Abs(BuyTrigger.Sensitivity))
                            {
                                shouldTrigger = false;
                            }

                            if (shouldTrigger)
                            {
                                // Send BuyTrigger notificaiton
                                ThresholdReachedEventArgs args = new ThresholdReachedEventArgs();
                                args.Threshold = Convert.ToDouble(BuyTrigger.Threshold);
                                args.Price = price;
                                args.TimeReached = DateTime.Now;
                                BuyTrigger.OnTriggerThresholdReached(args);
                                BuyTrigger.LastNotificationPrice = price;
                            }
                        }
                    }
                    // If price drops
                    else if (price < _price && price <= BuyTrigger.Threshold && BuyTrigger.TriggerDirection != TriggerDirectionEnum.Rise)
                    {
                        bool shouldTrigger = true;

                        // if fluctuations less than Sensitivity value, do not send notification
                        if (BuyTrigger.LastNotificationPrice != null && Math.Abs(price - Convert.ToDouble(BuyTrigger.LastNotificationPrice)) < Math.Abs(BuyTrigger.Sensitivity))
                        {
                            shouldTrigger = false;
                        }

                        if (shouldTrigger)
                        {
                            // Send BuyTrigger notificaiton
                            ThresholdReachedEventArgs args = new ThresholdReachedEventArgs();
                            args.Threshold = Convert.ToDouble(BuyTrigger.Threshold);
                            args.Price = price;
                            args.TimeReached = DateTime.Now;
                            BuyTrigger.OnTriggerThresholdReached(args);
                            BuyTrigger.LastNotificationPrice = price;
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
                            if (SellTrigger.LastNotificationPrice != null && Math.Abs(price - Convert.ToDouble(SellTrigger.LastNotificationPrice)) < Math.Abs(SellTrigger.Sensitivity))
                            {
                                shouldTrigger = false;
                            }

                            if (shouldTrigger)
                            {
                                // Send SellTrigger notificaiton
                                ThresholdReachedEventArgs args = new ThresholdReachedEventArgs();
                                args.Threshold = Convert.ToDouble(SellTrigger.Threshold);
                                args.Price = price;
                                args.TimeReached = DateTime.Now;
                                SellTrigger.OnTriggerThresholdReached(args);
                                SellTrigger.LastNotificationPrice = price;
                            }
                        }
                    }
                    // If price drops
                    else if (price < _price && price <= SellTrigger.Threshold && SellTrigger.TriggerDirection != TriggerDirectionEnum.Rise)
                    {
                        bool shouldTrigger = true;

                        // if fluctuations less than Sensitivity value, do not send notification
                        if (SellTrigger.LastNotificationPrice != null && Math.Abs(price - Convert.ToDouble(SellTrigger.LastNotificationPrice)) < Math.Abs(SellTrigger.Sensitivity))
                        {
                            shouldTrigger = false;
                        }

                        if (shouldTrigger)
                        {
                            // Send SellTrigger notificaiton
                            ThresholdReachedEventArgs args = new ThresholdReachedEventArgs();
                            args.Threshold = Convert.ToDouble(SellTrigger.Threshold);
                            args.Price = price;
                            args.TimeReached = DateTime.Now;
                            SellTrigger.OnTriggerThresholdReached(args);
                            SellTrigger.LastNotificationPrice = price;
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

        public void InitTrigger(double threshold, double sensitivity, TriggerTypeEnum triggerType, TriggerDirectionEnum? triggerDirection)
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

        public void UpdateTrigger(double threshold, double sensitivity, TriggerTypeEnum triggerType, TriggerDirectionEnum? triggerDirection)
        {
            if (triggerType == TriggerTypeEnum.Buy)
            {
                BuyTrigger.UpdateValue(threshold, sensitivity, triggerType, triggerDirection);
            }
            else if (triggerType == TriggerTypeEnum.Sell)
            {
                SellTrigger.UpdateValue(threshold, sensitivity, triggerType, triggerDirection);
            }
        }
    }
}
