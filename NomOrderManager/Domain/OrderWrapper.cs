namespace NomOrderManager.Domain
{
    public class OrderWrapper
    {
        public OrderWrapper(Order order, string host)
        {
            Order = order;
            CanCancel = order.Host.Equals(host);
        }

        public Order Order { get; }

        public bool CanCancel { get; }
    }
}
