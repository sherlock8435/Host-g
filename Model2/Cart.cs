using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Cart
    {
        public Cart() { }
        public int CartID { get; set; }
        public string ItemCount { get; set; }
        public string UserEmail { get; set; }
        public string Items { get; set; }

    }
}
