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
            Nickels = 10;
            Dimes = 10;
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
                    Nickels++;
                    break;
                case "dime":
                    ACU += 10;
                    Dimes++;
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

                if (ACU != item.Price && !CanMakeChange())
                {
                    ReturnCoins();
                    Display = "EXACT CHANGE ONLY";
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
            int changeNeeded = 0;
            foreach (var item in VendItems)
            {
                changeNeeded = item.Price % 25;

                var numDimesAvailable = Dimes;
                var numNickelsAvailable = Nickels;

                while (changeNeeded > 0)
                {
                    if (changeNeeded >= 10 && numDimesAvailable > 0)
                    {
                        changeNeeded -= 10;
                        numDimesAvailable--;
                    }

                    else if (changeNeeded >= 5 && numNickelsAvailable > 0)
                    {
                        changeNeeded -= 5;
                        numNickelsAvailable--;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return true;
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
                else if (ACU >= 10 && Dimes > 0)
                {
                    ReturnCoin("dime");
                    ACU -= 10;
                    Dimes--;
                }
                else if (ACU >= 5 && Nickels > 0) {
                    ReturnCoin("nickel");
                    ACU -= 05;
                    Nickels--;
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

