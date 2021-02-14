using System;
using PriceMonitor.Enums;
using PriceMonitor.Events;

namespace PriceMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            PriceMoniter priceMoniter = new PriceMoniter();

            while (!exit)
            {
                PrintPrompt();

                try
                {
                    // Assumption: user input all positive numbers
                    switch (Console.ReadKey(true).KeyChar)
                    {
                        case '1':
                            Console.WriteLine("Please set the current price");
                            double price = Convert.ToDouble(Console.ReadLine());
                            priceMoniter.SetPrice(price);
                            Console.WriteLine($"The current price is: {price}");
                            Console.WriteLine();
                            break;
                        case '2':
                            Console.WriteLine("Please select the trigger type");
                            Console.WriteLine("1: Buy Trigger");
                            Console.WriteLine("2: Sell Trigger");
                            TriggerTypeEnum triggerType = (TriggerTypeEnum)Convert.ToInt32(Console.ReadLine());

                            Console.WriteLine("Please set the trigger direction");
                            Console.WriteLine("1: Both");
                            Console.WriteLine("2: Only send notification when price rises");
                            Console.WriteLine("3: Only send notification when price drops");
                            TriggerDirectionEnum triggerDirection = (TriggerDirectionEnum)Convert.ToInt32(Console.ReadLine());

                            Console.WriteLine("Please set the trigger threshold");
                            double threshold = Convert.ToDouble(Console.ReadLine());

                            Console.WriteLine("Please set the trigger sensitivity");
                            double sensitivity = Convert.ToDouble(Console.ReadLine());
                            
                            if (triggerType == TriggerTypeEnum.Buy)
                            {
                                if (priceMoniter.BuyTrigger == null)
                                {
                                    priceMoniter.InitTrigger(threshold, sensitivity, triggerType, triggerDirection);
                                    priceMoniter.BuyTrigger.TriggerThresholdReached += BuyTriggerThresholdReachedHandler;
                                }
                                else
                                {
                                    priceMoniter.UpdateTrigger(threshold, sensitivity, triggerType, triggerDirection);
                                }
                            }
                            else if (triggerType == TriggerTypeEnum.Sell)
                            {
                                if (priceMoniter.SellTrigger == null)
                                {
                                    priceMoniter.InitTrigger(threshold, sensitivity, triggerType, triggerDirection);
                                    priceMoniter.SellTrigger.TriggerThresholdReached += SellTriggerThresholdReachedHandler;
                                }
                                else
                                {
                                    priceMoniter.UpdateTrigger(threshold, sensitivity, triggerType, triggerDirection);
                                }
                            }
                            
                            Console.WriteLine($"Trigger Type: {triggerType}, Trigger Direction: {triggerDirection}, Trigger Threshold: {threshold}, Trigger Sensitivity: {sensitivity}");
                            Console.WriteLine();
                            break;
                        case '3':
                            if (priceMoniter.BuyTrigger == null && priceMoniter.SellTrigger == null)
                            {
                                Console.WriteLine($"There is no trigger available");
                            }
                            else if (priceMoniter.BuyTrigger == null)
                            {
                                Console.WriteLine($"Trigger Type: {priceMoniter.SellTrigger.TriggerType}, Trigger Direction: {priceMoniter.SellTrigger.TriggerDirection}, Trigger Threshold: {priceMoniter.SellTrigger.Threshold}, Trigger Sensitivity: {priceMoniter.SellTrigger.Sensitivity}");
                            }
                            else if (priceMoniter.SellTrigger == null)
                            {
                                Console.WriteLine($"Trigger Type: {priceMoniter.BuyTrigger.TriggerType}, Trigger Direction: {priceMoniter.BuyTrigger.TriggerDirection}, Trigger Threshold: {priceMoniter.BuyTrigger.Threshold}, Trigger Sensitivity: {priceMoniter.BuyTrigger.Sensitivity}");
                            }
                            else
                            {
                                Console.WriteLine($"Trigger Type: {priceMoniter.SellTrigger.TriggerType}, Trigger Direction: {priceMoniter.SellTrigger.TriggerDirection}, Trigger Threshold: {priceMoniter.SellTrigger.Threshold}, Trigger Sensitivity: {priceMoniter.SellTrigger.Sensitivity}");
                                Console.WriteLine($"Trigger Type: {priceMoniter.BuyTrigger.TriggerType}, Trigger Direction: {priceMoniter.BuyTrigger.TriggerDirection}, Trigger Threshold: {priceMoniter.BuyTrigger.Threshold}, Trigger Sensitivity: {priceMoniter.BuyTrigger.Sensitivity}");
                            }
                            Console.WriteLine();
                            break;
                        case '4':
                            exit = true;
                            Environment.Exit(0);
                            break;
                        default:
                            break;
                    }
                }
                catch(Exception ex)
                {
                    // Log Error
                }
            }
        }

        static void PrintPrompt()
        {
            Console.WriteLine("Please select one of the following options:");
            Console.WriteLine("1: Set price");
            Console.WriteLine("2: Set trigger");
            Console.WriteLine("3: View current triggers");
            Console.WriteLine("4: Exit");
            Console.WriteLine();
        }

        static void BuyTriggerThresholdReachedHandler(object sender, ThresholdReachedEventArgs e)
        {
            Console.WriteLine($"The buy trigger threshold of {e.Threshold} was reached at {e.TimeReached}, current price is {e.Price}.");

            // User plugin buy logic here
        }

        static void SellTriggerThresholdReachedHandler(object sender, ThresholdReachedEventArgs e)
        {
            Console.WriteLine($"The sell trigger threshold of {e.Threshold} was reached at {e.TimeReached}.");

            // User plugin Sell logic here
        }
    }
}
