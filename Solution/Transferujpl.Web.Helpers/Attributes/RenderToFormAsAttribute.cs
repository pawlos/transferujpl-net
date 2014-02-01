using System;

namespace Transferujpl.Web.Helpers.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple=false)]
    public class RenderToFormAsAttribute : Attribute
    {
        public string Name { get; set; }
        public RenderToFormAsAttribute(string name)
        {
            Name = name;
        }
    }
}
