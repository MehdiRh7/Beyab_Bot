using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DataLayer
{
    public class BlockRepository : IBlockRepository
    {
        BeyabContext db;
        public BlockRepository(BeyabContext context)
        {
            this.db = context;
        }
        public IEnumerable<BlockList> GetAllBlockList()
        {
            return db.BlockLists.ToList();
        }

        public bool InsertBlock(BlockList block)
        {
            try
            {
                db.BlockLists.Add(block);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteBlock(int blockID)
        {
            try
            {
                var block = db.BlockLists.Find(blockID);
                DeleteBlock(block);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteBlock(BlockList block)
        {
            try
            {
                db.Entry(block).State = EntityState.Deleted;
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool UpdateBlock(BlockList block)
        {
            try
            {
                db.Entry(block).State = EntityState.Modified;
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
