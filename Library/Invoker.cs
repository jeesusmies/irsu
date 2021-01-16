using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace irsu.Library
{
    public static class Invoker
    {
        internal static async Task InvokeMethod(this Dictionary<string, MethodInfo> handlers, string method, object[] parameters = null)
        {
            try
            {
                await ((Task)handlers[method].Invoke(null, parameters));
            }
            catch
            {
                // not probably assigned
            }
        }
    }
}
