using System;

namespace Transferujpl.Core
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MapFromAttribute : Attribute
    {
        public string Name { get; set; }
        
        public MapFromAttribute(string name)
        {
            Name = name;
        }
    }
}
