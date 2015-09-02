using System;

namespace __NAME__.Messages
{
    public static class MessageTypeConventions
    {
        public static Func<Type, bool> EndsWith(string name)
        {
            return t => t.Namespace != null && t.Namespace.StartsWith("AllInOne") && t.Name.EndsWith(name);
        }
    }
}
