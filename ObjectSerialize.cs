using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebNotifier
{
    static class ObjectBinarySerialize
    {

        static public void SaveOListasFile<T>(string filepath, List<T> list,  bool append = false)
        {
            //serialize
            using (Stream stream = File.Open(filepath, append ? FileMode.Append : FileMode.Create))
            {
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                bformatter.Serialize(stream, list);
            }
        }
        static public List<T> LoadOListasFile<T>(string filename)
        {
            List<T> refile;
            //deserialize
            using (Stream stream = File.Open(filename, FileMode.Open))
            {
                if(stream.Length == 0)
                {
                    return new List<T>();
                }
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

             refile   = (List<T>)bformatter.Deserialize(stream);
             
            }
            return refile;
        }
        static public void SaveOasFile<T>(string filepath, T obfoo, bool append = false)
        {
            //serialize
            using (Stream stream = File.Open(filepath, append ? FileMode.Append : FileMode.Create))
            {
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                bformatter.Serialize(stream, obfoo);
            }
        }
        static public T  LoadOasFile<T>(string filename)
        {
            T refile;
            //deserialize
            using (Stream stream = File.Open(filename, FileMode.Open))
            {
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

             refile  = (T)bformatter.Deserialize(stream);
               
            }
            return refile;
        }
    }

}
