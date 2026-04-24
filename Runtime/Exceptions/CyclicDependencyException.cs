// Copyright (c) 2026 OopsItsACoder

using System;

namespace UJect.Exceptions
{
    public class CyclicDependencyException : BindException
    {
        internal CyclicDependencyException(string message): base(message)
        {
        }
    }
}
