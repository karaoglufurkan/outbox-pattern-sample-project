using System;

namespace Shared.Managers
{
    public static class TypeManager
    {
        public static string GetTypeString(Type type)
        {
            return type.FullName + ", " + type.Assembly.GetName().Name;
        }
    }
}