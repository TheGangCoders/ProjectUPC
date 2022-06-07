using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.ManejadorError
{
    [Serializable]
    public class InstanceNotFoundException : Exception
    {
        public string InstanceName { get; }

        public InstanceNotFoundException() { }

        public InstanceNotFoundException(string message)
            : base(message) { }

        public InstanceNotFoundException(string message, Exception inner)
            : base(message, inner) { }

        public InstanceNotFoundException(string message, string instanceName)
            : this(message)
        {
            InstanceName = instanceName;
        }
    }
}
