namespace VendingMachineKata
{
    public class VendItem
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; }

        public VendItem(string name, int price, int amount)
        {
            Name = name;
            Price = price;
            Amount = amount;
        }
    }
}