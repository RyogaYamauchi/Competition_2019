using System;

namespace Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PrefabPathAttribute : Attribute
    {
        
        public string Path { get; set; }

        public PrefabPathAttribute(string path)
        {
            Path = path;
        }
    }
}