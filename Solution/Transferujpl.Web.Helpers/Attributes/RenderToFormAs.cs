using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transferujpl.Web.Helpers.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple=false)]
    public class RenderToFormAs : Attribute
    {
        public string Name { get; set; }
        public RenderToFormAs(string name)
        {
            Name = name;
        }
    }
}
