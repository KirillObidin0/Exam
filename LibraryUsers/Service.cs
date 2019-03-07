using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LibraryUsers
{
    public class Service
    {
        public static bool SerializeXml<T>(string path, T list)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (var fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    if (typeof(T).IsArray)
                        serializer.Serialize(fs, list);
                    else
                        throw new Exception("<T> mus be Array!");
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
        public static T DeSerializeXml<T>(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (FileStream fs = new FileStream(path,FileMode.OpenOrCreate))
            {
              return (T)serializer.Deserialize(fs);
            }
        }      
    }
}
