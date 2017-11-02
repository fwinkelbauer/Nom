using System.Collections.Generic;
using System.Linq;
using NomOrderManager.Domain;

namespace NomOrderManager.Model
{
    public class MenuModel : IDeliveryServiceModel
    {
        public MenuModel(string serviceName, string phoneNumber, IEnumerable<Item> menu)
        {
            Name = $"{serviceName} - Menü";
            ServiceName = serviceName;
            PhoneNumber = phoneNumber;
            Menu = menu;
        }

        public string Name { get; }

        public string ServiceName { get; }

        public string PhoneNumber { get; }

        public IEnumerable<Item> Menu { get; }

        public bool HasData { get { return Menu.Any(); } }
    }
}
