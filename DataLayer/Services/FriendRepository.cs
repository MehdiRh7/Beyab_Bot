using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class FriendRepository:IFriendRepository
    {
        BeyabContext db;
        public FriendRepository(BeyabContext context)
        {
            this.db = context;
        }
        public IEnumerable<FriendsList> GetAllFriendList()
        {
            return db.FriendsLists.ToList();
        }

        public bool InsertFriend(FriendsList friend)
        {
            try
            {
                db.FriendsLists.Add(friend);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteFriend(int friendId)
        {
            try
            {
                var fr=db.FriendsLists.Find(friendId);
                DeleteFriend(fr);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteFriend(FriendsList friend)
        {
            try
            {
                db.Entry(friend).State = EntityState.Deleted;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateFriend(FriendsList friend)
        {
            try
            {
                db.Entry(friend).State = EntityState.Modified;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
