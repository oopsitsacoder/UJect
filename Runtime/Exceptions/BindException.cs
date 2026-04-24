// Copyright (c) 2026 OopsItsACoder
using System;

namespace UJect.Exceptions
{
    public class BindException : Exception
    {
        public BindException()
        {
        }

        public BindException(string message) : base(message)
        {
        }

        public BindException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
