﻿using System;
using System.IO;

namespace Wire.ValueSerializers
{
    public abstract class ValueSerializer
    {
        public abstract void WriteManifest(Stream stream, SerializerSession session);
        public abstract void WriteValue(Stream stream, object value, SerializerSession session);
        public abstract object ReadValue(Stream stream, DeserializerSession session);
        public abstract Type GetElementType();
    }
}