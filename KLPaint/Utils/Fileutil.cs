using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace KLPaint.Utils
{
    public static class Fileutil
    {
        public static void Write<T>(T obj,String file= "data.klp")
        {
            using(FileStream fs = new FileStream(file, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fs, obj);
                 
            }
           
        }

        public static T read<T>(String file)where T:class
        {

            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                T data = (T)formatter.Deserialize(fs);
                return data;
            }
        }

    }
}
