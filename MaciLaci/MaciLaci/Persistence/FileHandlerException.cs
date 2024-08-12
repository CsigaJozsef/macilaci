using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaciLaci.Persistence
{
    public class FileHandlerException : IOException
    {
        public FileHandlerException() { }
        public FileHandlerException(string message) : base(message) { }
        public FileHandlerException(string message, Exception innerException) : base(message, innerException) { }
        protected FileHandlerException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
