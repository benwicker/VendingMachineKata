using System;

namespace VendingMachineKata
{
    class Program
    {
        private static VendingMachine machine;
        private static bool display;

        static void Main(string[] args)
        {
            display = true;
            machine = new VendingMachine();
            var input = "";

            while (display)
            {
                PrintMenu(machine.CheckDisplay());
                input = Console.ReadLine();
                ParseInput(input);
            }
        }

        public static void PrintMenu(string display)
        {
            Console.Clear();
            Console.WriteLine("-- VENDING MACHINE --");
            Console.WriteLine("Display: " + display);
            Console.WriteLine();
            Console.WriteLine("Vend Options (press the number to the left of the option to select)");
            for (var i = 0; i < machine.VendItems.Count; i++)
            {
                Console.WriteLine((i + 1) + ": " + machine.VendItems[i].Name);
            }
            Console.WriteLine();
            Console.WriteLine("-- Other Actions --");
            Console.WriteLine("n => insert a nickel");
            Console.WriteLine("d => insert a dime");
            Console.WriteLine("q => insert a quarter");
            Console.WriteLine("return => return coins");
            Console.WriteLine("quit => exit the program");
            Console.WriteLine("all other keys just pressing enter will refresh");
            
            if (machine.ItemReturn.Count > 0)
            {
                Console.WriteLine();
                Console.WriteLine("Vend Slot:");
                foreach (var item in machine.ItemReturn)
                {
                    Console.WriteLine(item);
                }
            }

            if (machine.CoinReturn.Count > 0)
            {
                Console.WriteLine();
                Console.WriteLine("Coin Return Slot:");
                foreach (var coin in machine.CoinReturn)
                {
                    Console.WriteLine(coin);
                }
            }

            Console.WriteLine();
            Console.WriteLine("Enter an action then press enter:");
        }

        public static void ParseInput(string input)
        {
            if (Int32.TryParse(input, out int numericInput))
            {
                numericInput--; //adjust for 0 based index
                if (numericInput < machine.VendItems.Count)
                {
                    machine.SelectItem(numericInput);
                }
            }

            switch (input)
            {
                case "n":
                    machine.InsertCoin("nickel");
                    break;
                case "d":
                    machine.InsertCoin("dime");
                    break;
                case "q":
                    machine.InsertCoin("quarter");
                    break;
                case "return":
                    machine.ReturnCoins();
                    break;
                case "quit":
                    display = false;
                    Console.Clear();
                    break;
                default:
                    break;
            }
        }
    }
}
