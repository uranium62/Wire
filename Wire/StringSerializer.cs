﻿using System;
using System.IO;
using System.Text;
using Wire.ValueSerializers;

namespace Wire
{
    public class StringSerializer : ValueSerializer
    {
        public static readonly ValueSerializer Instance = new StringSerializer();

        private readonly byte[] _manifest = { 7 };
        public override void WriteManifest(Stream stream, Type type, SerializerSession session)
        {
            stream.Write(_manifest, 0, _manifest.Length);
        }

        public override void WriteValue(Stream stream, object value, SerializerSession session)
        {
            if (value == null)
            {
                Int32Serializer.Instance.WriteValue(stream, -1, session);
            }
            else
            {
                var bytes = Encoding.UTF8.GetBytes((string)value);
                Int32Serializer.Instance.WriteValue(stream, bytes.Length, session);
                stream.Write(bytes, 0, bytes.Length);
            }
        }

        public override object ReadValue(Stream stream, SerializerSession session)
        {
            var length = (int)Int32Serializer.Instance.ReadValue(stream, session);
            if (length == -1)
                return null;

            byte[] buffer = session.GetBuffer(length);

            stream.Read(buffer, 0, length);
            var res = Encoding.UTF8.GetString(buffer, 0, length);
            return res;
        }
    }
}