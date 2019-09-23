using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace VendingMachineKata
{
    public class VendingMachine
    {
        public string Display { get; set; }
        public int ACU { get; set; } // use integers to avoid double math precision errors
        public List<string> Bank = new List<string>();
        
        // coin reserves
        public int Nickels { get; set; }
        public int Dimes { get; set; }
        public int Quarters { get; set; }

        // allow for different machine options as well as the addition and remove of options
        public List<VendItem> VendItems { get; set; }
        
        // simulate a connect coin return and item return
        public List<string> CoinReturn { get; set; } = new List<string>();
        public List<string> ItemReturn { get; set; } = new List<string>();

        private string ConvertToCurrency(int value)
        {
            return ((double)value / 100).ToString("C", new CultureInfo("en-US"));
        }

        public VendingMachine() 
            : this(new List<VendItem>
                {
                    new VendItem("cola", 100, 10),
                    new VendItem("chips", 50, 10),
                    new VendItem("candy", 65, 10)
                }
            )
        {}

        public VendingMachine(List<VendItem> vendItems)
        {
            ACU = 0;
            VendItems = vendItems;
            Nickels = 50;
            Dimes = 50;
            Quarters = 50;
        }

        public string CheckDisplay() {
            if (!string.IsNullOrEmpty(Display))
            {
                // clear out the display
                var tempDisplay = Display;
                Display = "";
                return tempDisplay;
            }

            return ACU == 0 ? 
                (CanMakeChange() ? "INSERT COIN" : "EXACT CHANGE ONLY") : 
                ConvertToCurrency(ACU);
        }

        public void InsertCoin(string coin) {
            Bank.Add(coin);
            switch (coin)
            {
                case "nickel":
                    ACU += 5;
                    break;
                case "dime":
                    ACU += 10;
                    break;
                case "quarter":
                    ACU += 25;
                    break;
                default:
                    Bank.RemoveAt(Bank.Count - 1); // remove last
                    ReturnCoin(coin);
                    break;
            }
        }

        // simulate the return of a coin
        public void ReturnCoin(string coin)
        {
            CoinReturn.Add(coin);
        }

        // simulate the vending of an item
        public void VendItem(string item)
        {
            ItemReturn.Add(item);
        }

        public void SelectItem(int index)
        {
            var item = VendItems[index];

            if (ACU >= item.Price)
            {
                if (item.Amount == 0)
                {
                    Display = "SOLD OUT";
                    return;
                }

                VendItem(item.Name);
                Bank.Clear();
                ACU -= item.Price;
                MakeChange();
                Display = "THANK YOU";
                return;
            }

            Display = "PRICE " + ConvertToCurrency(item.Price);
        }

        public void ReturnCoins()
        {
            ACU = 0;
            foreach (var coin in Bank)
            {
                ReturnCoin(coin);
            }
            Bank.Clear();
        }

        public bool CanMakeChange()
        {
            // determine max amount we would need to make
            int maxChangeNeeded = 0;
            foreach (var item in VendItems)
            {
                if (item.Price % 25 > maxChangeNeeded)
                    maxChangeNeeded = item.Price;
            }

            var numDimesAvailable = Dimes;
            var numNickelsAvailable = Nickels;

            while (maxChangeNeeded > 0)
            {
                if (maxChangeNeeded >= 10 && numDimesAvailable > 0)
                {
                    maxChangeNeeded -= 10;
                    numDimesAvailable--;
                }

                else if (maxChangeNeeded >= 5 && numNickelsAvailable > 0)
                {
                    maxChangeNeeded -= 5;
                    numNickelsAvailable--;
                }
                else
                {
                    return false;
                }
            }

            return maxChangeNeeded == 0;
        }

        public void MakeChange()
        { 
            while (ACU != 0)
            {
                if (ACU >= 25)
                {
                    ReturnCoin("quarter");
                    ACU -= 25;
                }
                else if (ACU >= 10)
                {
                    ReturnCoin("dime");
                    ACU -= 10;
                }
                else if (ACU >= 05) {
                    ReturnCoin("nickel");
                    ACU -= 05;
                }
                else
                {
                    // an error occured
                    Display = "An error occured";
                    return;
                }
            }
        }
    }
}

