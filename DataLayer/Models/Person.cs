using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Person
    {
        [Key]
        public int PersonId { get; set; }
        public string Username { get; set; }
        public string PersonName { get; set; }
        public string PersonAge { get; set; }
        public string PersonCity { get; set; }
        public string PersonGender { get; set; }
        public long chatid { get; set; }
        public string FilterAge { get; set; }
        public string FilterCity { get; set; }
        public string FilterGender { get; set; }

        public long LastUser { get; set; }

        public string CommandName { get; set; }
        public string Block_Ckeck { get; set; } 



        public virtual IEnumerable<Onlines> Onlines { get; set; }
        public virtual IEnumerable<BlockList> Blocks { get; set; }
        public virtual IEnumerable<FriendsList> Friends { get; set; }
        public virtual IEnumerable<RequestValidation> Validations { get; set; }
    }
}
