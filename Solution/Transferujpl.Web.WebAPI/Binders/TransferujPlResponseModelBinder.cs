using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using Transferujpl.Core;
using Transferujpl.Web.WebAPI.Helpers;

namespace Transferujpl.Web.WebAPI.Binders
{
    public class TransferujPlResponseModelBinder : HttpParameterBinding
    {
        public TransferujPlResponseModelBinder(HttpParameterDescriptor descriptor) : base(descriptor)
        {}

        public override Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider, HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            var log = NLog.LogManager.GetCurrentClassLogger();
            log.Info("ExecuteBindingAsync");
            var binding = actionContext.ActionDescriptor.ActionBinding;

            if (actionContext.Request.GetClientIpAddress() != "195.149.229.109" &&
                actionContext.Request.IsLocal() == false)
            {
                log.Info("Invalid IP - Expecting 195.149.229.109, got: {0}", 
                                        actionContext.Request.Headers.Host);
                throw new InvalidOperationException("Invalid IP.");
            }

            var type = binding.ParameterBindings[0].Descriptor.ParameterType;
            if (type != typeof(TransferujPlResponse))
            {
                log.Info("Invalid parameter type. Expecting TransferujPlResponse");
                throw new InvalidOperationException();
            }

            return actionContext.Request.Content
                    .ReadAsStringAsync()
                    .ContinueWith((task) =>
                    {
                        var stringResult = task.Result;
                        log.Debug("Detailed info: {0}", stringResult);

                        SetValue(actionContext, TransferujPlResponse.FromNameValueCollection(
                                    stringResult.ToNameValueCollection()));
                    });
        }

        public override bool WillReadBody
        {
            get
            {
                return true;
            }
        }
    }
}
