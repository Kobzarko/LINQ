using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqApp2
{
    public class User
    {

        public string Name { get; set; }
        public int Age { get; set; }
        public List<string> Languages { get; set; }
        // создает список при создании объекта
        public User()
        {
            Languages = new List<string>();
        }

    }
}
