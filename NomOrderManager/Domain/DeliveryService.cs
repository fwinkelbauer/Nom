using System.Collections.Generic;

namespace NomOrderManager.Domain
{
    public class DeliveryService
    {
        private static readonly object MyLock = new object();

        private readonly List<Order> _orders;

        private int _latestId;

        public DeliveryService(string name, string phoneNumber, IEnumerable<Item> menu)
        {
            _orders = new List<Order>();
            _latestId = 0;

            Name = name;
            PhoneNumber = phoneNumber;
            Menu = menu;
        }

        public string Name { get; }

        public string PhoneNumber { get; }

        public IEnumerable<Item> Menu { get; }

        public IEnumerable<Order> Orders
        {
            get
            {
                lock (MyLock)
                {
                    return new List<Order>(_orders);
                }
            }
        }

        public Order AddOrder(int itemId, string comment, string host, string username)
        {
            lock (MyLock)
            {
                var item = GetItemById(itemId);
                var order = new Order(_latestId, item, comment, host, username);
                _latestId++;

                _orders.Add(order);

                return order;
            }
        }

        public Order RemoveOrder(int orderId, string host)
        {
            lock (MyLock)
            {
                Order order = GetOrderById(orderId);

                if (!order.Host.Equals(host))
                {
                    throw new CancelOrderException($"{host} cannot delete {order.Host}'s order");
                }

                _orders.Remove(order);

                return order;
            }
        }

        private Item GetItemById(int id)
        {
            foreach (var item in Menu)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }

            throw new ResourceNotFoundException($"Item #{id} not found");
        }

        private Order GetOrderById(int id)
        {
            foreach (var order in _orders)
            {
                if (order.Id == id)
                {
                    return order;
                }
            }

            throw new ResourceNotFoundException($"Order #{id} not found");
        }
    }
}
