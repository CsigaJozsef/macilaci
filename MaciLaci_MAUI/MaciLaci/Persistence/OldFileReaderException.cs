using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaciLaci.Persistence
{
    public class OldFileReaderException : IOException
    {
        public OldFileReaderException() { }
        public OldFileReaderException(string message) : base(message) { }
        public OldFileReaderException(string message, Exception innerException) : base(message, innerException) { }
        protected OldFileReaderException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
