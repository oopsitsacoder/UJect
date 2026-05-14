// Copyright (c) 2026 OopsItsACoder
using System;

namespace UJect.Utilities
{
    /// <summary>
    /// Unity treats any attribute named "PreserveAttribute" the same way for code stripping, so include one here
    /// that doesn't depend on UnityEngine for use in NoEngine assemblies
    /// </summary>
    public class PreserveAttribute : Attribute
    {
        
    }
}
