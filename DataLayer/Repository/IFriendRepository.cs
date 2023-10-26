using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IFriendRepository:IDisposable
    {
        IEnumerable<FriendsList> GetAllFriendList();
        bool InsertFriend(FriendsList friend);
        bool DeleteFriend(int friendId);
        bool DeleteFriend(FriendsList friend);
        bool UpdateFriend(FriendsList friend);
        void Save();
    }
}
