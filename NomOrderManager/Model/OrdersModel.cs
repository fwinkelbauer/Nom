using System.Collections.Generic;
using System.Linq;
using NomOrderManager.Domain;

namespace NomOrderManager.Model
{
    public class OrdersModel : IDeliveryServiceModel
    {
        public OrdersModel(string serviceName, string phoneNumber, IEnumerable<Order> orders, string host)
        {
            Name = $"{serviceName} - Bestellungen";
            ServiceName = serviceName;
            PhoneNumber = phoneNumber;
            WrappedOrders = orders.Select(o => new OrderWrapper(o, host));
        }

        public string ServiceName { get; }

        public string Name { get; }

        public string PhoneNumber { get; }

        public IEnumerable<OrderWrapper> WrappedOrders { get; }

        public bool HasData { get { return WrappedOrders.Any(); } }
    }
}
