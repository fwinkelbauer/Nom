using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using NomOrderManager.Domain;

namespace NomOrderManager
{
    public static class ContentLoader
    {
        public static IEnumerable<DeliveryService> LoadDeliveryServices()
        {
            var deliveryServices = new List<DeliveryService>();
            var dir = new DirectoryInfo(
                Path.Combine(
                    Path.GetDirectoryName(Assembly.GetEntryAssembly().Location),
                    "Content"));

            if (!dir.Exists)
            {
                return deliveryServices;
            }

            foreach (var serviceName in dir.GetDirectories())
            {
                var phoneNumber = File.ReadAllText($"{serviceName.FullName}/meta");
                var menuLines = File.ReadAllLines($"{serviceName.FullName}/menu.csv");
                var menu = new List<Item>();

                foreach (var line in menuLines)
                {
                    var content = line.Split(';').Select(l => l.Trim()).ToArray();

                    menu.Add(new Item(Convert.ToInt32(content[0]), content[1], content[2], Convert.ToDecimal(content[3].Replace('.', ','))));
                }

                deliveryServices.Add(new DeliveryService(serviceName.Name, phoneNumber, menu));
            }

            return deliveryServices;
        }
    }
}
