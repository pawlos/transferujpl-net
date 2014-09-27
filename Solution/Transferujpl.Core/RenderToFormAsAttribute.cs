using System;

namespace Transferujpl.Core
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
