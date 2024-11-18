using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class MailBoxList : List<MailBox>
    {
        public MailBoxList() { }

        public MailBoxList(IEnumerable<MailBox> list) : base(list) { }


    }
}
