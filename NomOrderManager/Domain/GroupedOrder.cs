namespace NomOrderManager.Domain
{
    public class GroupedOrder
    {
        public int Amount { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string PriceText
        {
            get
            {
                return Price.ToString(Item.PriceFormatString);
            }
        }

        public decimal PriceSum { get; set; }

        public string PriceSumText
        {
            get
            {
                return PriceSum.ToString(Item.PriceFormatString);
            }
        }

        public string Comment { get; set; }

        public bool HasComment
        {
            get
            {
                return !string.IsNullOrEmpty(Comment);
            }
        }
    }
}
