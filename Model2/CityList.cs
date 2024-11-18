using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{

    public class CityList : List<Cities>
    {
        public CityList() { }
        public CityList(IEnumerable<Cities> list) : base(list) { }



        public List<Cities> OrderByCityName()
        {

            if (Count > 0)

                return this.OrderBy(item => item.CityName).ToList();
            return null;








        }
    }
}
