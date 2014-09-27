using System;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Transferujpl.Web.WebAPI.Binders
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public sealed class BindTransferujPlAttribute : ParameterBindingAttribute
    {
        public override HttpParameterBinding GetBinding(HttpParameterDescriptor parameter)
        {
            if (parameter == null)
                throw new ArgumentException("Invalid parameter");

            return new TransferujPlResponseModelBinder(parameter);
        }
    }
}
