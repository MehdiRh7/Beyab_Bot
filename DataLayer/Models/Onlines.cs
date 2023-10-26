using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Onlines
    {
        [Key]
        public int OnlineID { get; set; }
        public int PersonId { get; set; }
        public string Username { get; set; }
        public long User2 { get; set; }
        public long chatid { get; set; }

        public virtual Person Person { get; set; }
    }
}
