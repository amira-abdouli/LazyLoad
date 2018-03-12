using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public static class DataStore<T> where T: BaseTable
    {
        public static IEnumerable<T> Get(IQueryable query=null)
        {
            var time1 = DateTime.Now;
            if (query == null)
            {
                var result = new DBcon<T>().Table.Where(c=>c.Deleted==false).ToList();
                var time2 = DateTime.Now;
                var totaltime = time2 - time1;
                return result;
            }
            else
            {
                var result = new DBcon<T>().Table.Where(c => c.Deleted == false).ToList();
                var time2 = DateTime.Now;
                var totaltime = time2 - time1;
                return result;
            }
        }
        public static T Find(params object[] id)
        {
            var value = new DBcon<T>().Table.Find(id);
            if (value.Deleted == false) return value; else return null;
        }
        public static int Add(T model)
        {
            model.CreateDate = DateTime.Now;
            model.UpdateDate = DateTime.Now;
            model.Deleted = false;
            var db = new DBcon<T>();
            db.Table.Add(model);
            return db.SaveChanges();
        }
        public static int Update(T model)
        {
            model.UpdateDate = DateTime.Now;
            var db = new DBcon<T>();
            db.Entry(model).State = EntityState.Modified;
            return db.SaveChanges();
        }
        public static int Delete(params object[] id)
        {
            var db = new DBcon<T>();
            var Table = db.Table.Find(id);
            Table.UpdateDate = DateTime.Now;
            Table.Deleted = true;
            return db.SaveChanges();
        }
    }
    public class DBcon<T> :DbContext where T : class
    {
        public DBcon(): base("DefaultConnection")
        { }
        public DbSet<T> Table { get; set; }
    }
}
