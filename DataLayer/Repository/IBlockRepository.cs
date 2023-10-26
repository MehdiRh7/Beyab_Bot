using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IBlockRepository:IDisposable
    {
        IEnumerable<BlockList> GetAllBlockList();
        bool InsertBlock(BlockList block);
        bool DeleteBlock(int blockID);
        bool DeleteBlock(BlockList block);
        bool UpdateBlock(BlockList block);
        void Save();
    }
}
