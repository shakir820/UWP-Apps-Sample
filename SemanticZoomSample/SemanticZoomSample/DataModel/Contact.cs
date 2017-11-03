using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticZoomSample.DataModel
{
    public class Contact
    {
       
        public Contact(string name, string num, string path)
        {
            Name = name;
            Number = num;
            ImagePath = path;
        }

        public string Name { get; set; }
        public string Number { get; set; }
        public string ImagePath { get; set; }
    }
}
