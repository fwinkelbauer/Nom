using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using NomOrderManager.Domain;
using Serilog;

namespace NomOrderManager
{
    public class NomBootstrapper : DefaultNancyBootstrapper
    {
        private static readonly ILogger _log = Log.ForContext<NomBootstrapper>();

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            pipelines.OnError += (ctx, ex) =>
            {
                _log.Error(ex, ex.Message);

                if (ex.GetType() == typeof(ResourceNotFoundException))
                {
                    return HttpStatusCode.NotFound;
                }
                else if (ex.GetType() == typeof(NoUsernameException))
                {
                    return HttpStatusCode.Unauthorized;
                }

                return null;
            };
        }
    }
}
