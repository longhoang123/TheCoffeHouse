using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TheCoffeHouse.Services
{
    public interface ISerializerService
    {
        T Deserialize<T>(string data, string url = null);

        T DeserializeFromJsonStream<T>(Stream jsonStream, string url = null);

        string Serialize<T>(T obj);
    }
}
