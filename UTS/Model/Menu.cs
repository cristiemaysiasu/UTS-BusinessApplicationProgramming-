using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTS.Model
{
    class Menu
    {
        int id { get; set; }
        DateTime createdAt { get; set; }
        DateTime modifyAt { get; set; }
        bool deleted { get; set; }
        string name { get; set; }
        int capital { get; set; }
        int price { get; set; }
        string category_id { get; set; }
        string unit_id { get; set; }
        int stock { get; set; }
        string description { get; set; } 
    }
}
