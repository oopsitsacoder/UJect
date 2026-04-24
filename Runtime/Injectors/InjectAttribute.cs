// Copyright (c) 2026 OopsItsACoder
using System;

namespace UJect.Injection
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter)]
    public class InjectAttribute : Attribute
    {
        internal readonly string CustomId;

        public InjectAttribute(string customId = null)
        {
            CustomId = customId;
        }
    }
}
