using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class BeyabContext:DbContext
    {
        public DbSet<Onlines> Onlines { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<BlockList> BlockLists { get; set; }
        public DbSet<FriendsList> FriendsLists { get; set; }
        public DbSet<RequestValidation> Validations { get; set; }
    }
}
