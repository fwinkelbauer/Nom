using Nancy;
using Nancy.ErrorHandling;
using Nancy.ViewEngines;
using NomOrderManager.Model;

namespace NomOrderManager
{
    public class UnauthorizedHandler : DefaultViewRenderer, IStatusCodeHandler
    {
        public UnauthorizedHandler(IViewFactory factory)
            : base(factory)
        {
        }

        public void Handle(HttpStatusCode statusCode, NancyContext context)
        {
            var response = RenderView(context, "Register", new RegisterModel(string.Empty));
            response.StatusCode = HttpStatusCode.Unauthorized;
            context.Response = response;
        }

        public bool HandlesStatusCode(HttpStatusCode statusCode, NancyContext context)
        {
            return statusCode == HttpStatusCode.Unauthorized;
        }
    }
}
