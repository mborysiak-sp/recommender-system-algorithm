using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace wyrzynarka
{
    class Serializer
    {

        public static void Pack(Object obj, String FileName)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(FileName, FileMode.Create, FileAccess.Write);

            formatter.Serialize(stream, obj);
            stream.Close();
        }

        public static void PackMatrix(Object obj, String matrixName)
        {
            Pack(obj, matrixName + ".wyrznieteZAmazona");
        }


        public static Object Unpack(String FileName)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(FileName + ".wyrznieteZAmazona", FileMode.Open, FileAccess.Read);
            return formatter.Deserialize(stream);
        }
    }
}
