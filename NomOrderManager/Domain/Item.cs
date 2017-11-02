namespace NomOrderManager.Domain
{
    public class Item
    {
        public const string PriceFormatString = "C";

        public Item(int id, string name, string information, decimal price)
        {
            Id = id;
            Name = name;
            Information = information;
            Price = price;
        }

        public int Id { get; }

        public string Name { get; }

        public string Information { get; }

        public decimal Price { get; }

        public string PriceText
        {
            get
            {
                return Price.ToString(PriceFormatString);
            }
        }

        public override string ToString()
        {
            return $"{Id}; {Name}; {Information}; {Price}";
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var item = obj as Item;

            if (item == null)
            {
                return false;
            }

            return Name.Equals(item.Name)
                && Information.Equals(item.Information)
                && Price == item.Price;
        }
    }
}
