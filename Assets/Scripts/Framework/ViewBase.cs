using System.Reflection;
using UnityEngine;

namespace Framework
{
    public class ViewBase : MonoBehaviour
    {
        public T CreateInstance<T>() where T : ViewBase
        {
            var path = typeof(T).GetCustomAttribute<PrefabPathAttribute>().Path;
            var obj = Resources.Load<T>(path);
            return Instantiate(obj);
        }
    }
}