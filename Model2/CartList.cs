using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class CartList : List<Cart>
    {
        public CartList() { }

        public CartList(IEnumerable<Cart> list) : base(list) { }

    }
}
