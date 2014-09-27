using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using Transferujpl.Core;

namespace Transferujpl.Web.Mvc.Binders
{
    public class TransferujPlResponseModelBinder : HttpParameterBinding
    {
        public TransferujPlResponseModelBinder(HttpParameterDescriptor descriptor) : base(descriptor)
        {}

        public override Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider, HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            var binding = actionContext.ActionDescriptor.ActionBinding;

            if (actionContext.Request.Headers.Host != "195.149.229.109")
            {
                throw new InvalidOperationException("Invalid IP.");
            }

            var type = binding.ParameterBindings[0].Descriptor.ParameterType;
            if (type != typeof(TransferujPlResponse))
            {
                throw new InvalidOperationException();
            }

            return actionContext.Request.Content
                    .ReadAsStringAsync()
                    .ContinueWith((task) =>
                    {
                        var stringResult = task.Result;
                        SetValue(actionContext, new TransferujPlResponse());
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
