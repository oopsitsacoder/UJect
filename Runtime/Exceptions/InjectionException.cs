// Copyright (c) 2024 OopsItsACoder

using System;

namespace UJect.Exceptions
{
    public class InjectionException : InvalidOperationException
    {
        public InjectionException(Type injectedType, string message) : base($"Error injecting {injectedType}: {message}")
        {
        }
    }
}