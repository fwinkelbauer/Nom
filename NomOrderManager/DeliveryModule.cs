using System;
using System.Collections.Generic;
using System.Linq;
using Nancy;
using Nancy.Cookies;
using NomOrderManager.Domain;
using NomOrderManager.Model;
using Serilog;

namespace NomOrderManager
{
    public class DeliveryModule : NancyModule
    {
        private const string UsernameCookie = "nom.username";

        private static readonly ILogger _log = Log.ForContext<DeliveryModule>();

        // This is kinda hacky, but I guess it is fine for now
        private static readonly IEnumerable<DeliveryService> _deliveryServices = ContentLoader.LoadDeliveryServices();

        public DeliveryModule()
        {
            Get["/"] = _ =>
            {
                EnsureUserameCookie();
                return View[new IndexModel(_deliveryServices.Select(d => d.Name))];
            };

            Get["/register"] = _ =>
            {
                var usernameAttribute = Request.Query.username.HasValue ? Request.Query.username : string.Empty;
                var username = GetUsermameCookie();

                if (string.IsNullOrEmpty(usernameAttribute))
                {
                    return View[new RegisterModel(username)];
                }
                else
                {
                    Log.Information("Registering {host} as {username}", Request.UserHostAddress, usernameAttribute);
                    return Response.AsRedirect("/").WithCookie(new NancyCookie(UsernameCookie, usernameAttribute, DateTime.Today.AddYears(1)));
                }
            };

            Get["/{service}"] = parameters =>
            {
                EnsureUserameCookie();
                var deliveryService = EnsureDeliveryService(parameters.service);

                return View["Menu", new MenuModel(deliveryService.Name, deliveryService.PhoneNumber, deliveryService.Menu)];
            };

            Get["/{service}/order"] = parameters =>
            {
                EnsureUserameCookie();
                var deliveryService = EnsureDeliveryService(parameters.service);

                return View["Orders", new OrdersModel(deliveryService.Name, deliveryService.PhoneNumber, deliveryService.Orders, Request.UserHostAddress)];
            };

            Get["/{service}/order/add/{id:int}"] = parameters =>
            {
                EnsureUserameCookie();
                var deliveryService = EnsureDeliveryService(parameters.service);
                var comment = Request.Query.comment.HasValue ? Request.Query.comment : string.Empty;
                var host = Request.UserHostAddress;
                var username = GetUsermameCookie();

                var order = deliveryService.AddOrder(parameters.id, comment, host, username);
                LogOrder("Add", order, host);

                return Response.AsRedirect($"/{parameters.service}/order");
            };

            Get["/{service}/order/cancel/{id:int}"] = parameters =>
            {
                EnsureUserameCookie();
                var deliveryService = EnsureDeliveryService(parameters.service);
                var host = Request.UserHostAddress;

                try
                {
                    var order = deliveryService.RemoveOrder(parameters.id, host);
                    LogOrder("Cancel", order, host);

                }
                catch (CancelOrderException e)
                {
                    _log.Error(e, e.Message);
                    return View["CancelOrder", new ErrorModel()];
                }

                return Response.AsRedirect($"/{parameters.service}/order");
            };

            Get["/{service}/order/overview"] = parameters =>
            {
                EnsureUserameCookie();
                var deliveryService = EnsureDeliveryService(parameters.service);

                return View["Overview", new OverviewModel(deliveryService.Name, deliveryService.PhoneNumber, deliveryService.Orders)];
            };
        }

        private string GetUsermameCookie()
        {
            string username = null;
            Request.Cookies.TryGetValue(UsernameCookie, out username);

            if (username == null)
            {
                username = string.Empty;
            }

            return username;
        }

        private void EnsureUserameCookie()
        {
            if (string.IsNullOrEmpty(GetUsermameCookie()))
            {
                throw new NoUsernameException($"No username cookie found for '{Request.UserHostAddress}'");
            }
        }

        private void LogOrder(string operation, Order order, string host)
        {
            _log.Information("{Operation} - {id}, {Name}, {Comment}, {Host}, {Username} ({Host})", operation, order.Id, order.Item.Name, order.Comment, order.Host, order.Username, host);
        }

        private DeliveryService EnsureDeliveryService(string name)
        {
            foreach (var service in _deliveryServices)
            {
                if (service.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase))
                {
                    return service;
                }
            }

            throw new ResourceNotFoundException($"Delivery service '{name}' not found");
        }
    }
}
