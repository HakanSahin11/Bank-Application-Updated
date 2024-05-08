using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Desktop_UI.Models
{
    public class ComboboxItemObj
    {
        public ComboboxItemObj(string name, long value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public long Value { get; set; }
    }
}
