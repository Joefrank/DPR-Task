
using System.IO;
using Newtonsoft.Json;

namespace CustomReg.Utils.InputOutput
{
    /// <summary>
    /// class to manage json file input/output
    /// </summary>
    public class JsonFileManager : IFileManager
    {
        /// <summary>
        /// Generic function to read json content as specific type T
        /// </summary>
        /// <typeparam name="T">This is the type we are to cast content into</typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public T LoadContentAs<T>(string filePath)
        {
            using (var r = new StreamReader(filePath))
            {
                var json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(json);
            }
        }

        /// <summary>
        /// function to persist content into file given specific path
        /// </summary>
        /// <param name="objSerial"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool PersistContent(object objSerial, string filePath)
        {
            // serialize JSON directly to a file
            using (var file = File.CreateText(filePath))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(file, objSerial);
            }

            return true;
        }
    }
}
