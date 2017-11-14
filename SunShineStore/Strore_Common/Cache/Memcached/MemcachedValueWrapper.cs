using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack;
using ServiceStack.Text;

namespace Strore_Common.Caching.Memcached
{
    [Serializable]
    public class MemcachedValueWrapper
    {
        public Type ValueType { get; set; }
        public string JsonString { get; set; }

        [NonSerialized]
        private object _value;

        public object Value
        {
            get
            {
                if (_value == null && !string.IsNullOrEmpty(JsonString))
                    _value = JsonSerializer.DeserializeFromString(JsonString, ValueType);
                return _value;
            }
        }

        public MemcachedValueWrapper() { }

        public MemcachedValueWrapper(object value)
        {
            ValueType = value.GetType();
            _value = value;
            JsonString = value.ToJson();
        }
    }
}
