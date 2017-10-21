using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer10
{
    class Class1
    {
        //
        //public void store()
        //{
        //    MyObject obj = new MyObject();
        //    obj.n1 = 1;
        //    obj.n2 = 24;
        //    obj.str = "Some String";
        //    IFormatter formatter = new BinaryFormatter();
        //    Stream stream = new FileStream("MyFile.bin", FileMode.Create, FileAccess.Write, FileShare.None);
        //    formatter.Serialize(stream, obj);
        //    stream.Close();
        //}
       
    }
    [Serializable]
    public class info
    {
        public string[] FolderPath; 
    }

    //[Serializable]
    //public class MyObject
    //{
    //    public int n1 = 0;
    //    public int n2 = 0;
    //    public String str = null;
    //}
}
