using MessagePack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MessagePackProject.Formmater
{
    public class MsgPackMediaTypeFormatter : BufferedMediaTypeFormatter
    {
        public MsgPackMediaTypeFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/msgpack"));
        }

        public override bool CanReadType(Type type)
        {
            return !type.IsAbstract && !type.IsInterface && type.IsPublic;
        }

        public override bool CanWriteType(Type type)
        {
            return !type.IsAbstract && !type.IsInterface && type.IsPublic;
        }

        public override object ReadFromStream(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
        {
            return LZ4MessagePackSerializer.NonGeneric.Deserialize(type, readStream);
        }

        public override void WriteToStream(Type type, object value, Stream writeStream, HttpContent content)
        {
            //LZ4MessagePackSerializer.NonGeneric.Serialize(type, writeStream, value, MessagePack.Resolvers.ContractlessStandardResolver.Instance);
        }
    }
}
