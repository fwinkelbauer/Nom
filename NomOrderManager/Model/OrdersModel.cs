using System.Collections.Generic;
using System.Linq;
using NomOrderManager.Domain;

namespace NomOrderManager.Model
{
    public class OrdersModel : IOrdersModel
    {
        public OrdersModel(string serviceName, string phoneNumber, IEnumerable<Order> orders, string host)
        {
            Name = $"{serviceName} - Bestellungen";
            ServiceName = serviceName;
            PhoneNumber = phoneNumber;
            var list = new List<OrderWrapper>();

            foreach (var order in orders)
            {
                var wrap = new OrderWrapper(order, host);
                list.Add(wrap);

                if (wrap.Order.HasComment)
                {
                    HasComment = true;
                }
            }

            WrappedOrders = list;
        }

        public string ServiceName { get; }

        public string Name { get; }

        public string PhoneNumber { get; }

        public IEnumerable<OrderWrapper> WrappedOrders { get; }

        public bool HasData { get { return WrappedOrders.Any(); } }

        public bool HasComment { get; }
    }
}
