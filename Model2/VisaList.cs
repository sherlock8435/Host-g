using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class VisaList : List<Visa>
    {

        public VisaList() { }

        public VisaList(IEnumerable<Visa> List) : base(List) { }





    }
}
