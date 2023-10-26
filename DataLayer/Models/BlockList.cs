using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class BlockList
    {
        [Key]
        public int ListID { get; set; }
        public int PersonId { get; set; }
        public long P1Chatid { get; set; }
        public long P2Chatid { get; set; }


        public virtual Person Person { get; set; }
    }
}
