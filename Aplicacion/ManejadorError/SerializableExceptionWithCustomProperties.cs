using System.Runtime.Serialization;

namespace Aplicacion.ManejadorError
{
    public class SerializableExceptionWithCustomProperties
    {
        private SerializationInfo info;
        private StreamingContext context;

        public SerializableExceptionWithCustomProperties(string message, SerializationInfo info, StreamingContext context)
        {
            this.info = info;
            this.context = context;
        }
    }
}