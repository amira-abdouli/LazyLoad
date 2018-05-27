using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace BLL.Models
{
    public static class DataStore<T> where T : BaseTable
    {
        public static IEnumerable<T> Get(Expression<Func<T, bool>> expression = null/*IQueryable<T> query=null*/)
        {
            try
            {
                var time1 = DateTime.Now;
                if (expression == null)
                {
                    var result = new DBcon<T>().Table.Where(c => c.Deleted == false).ToList();
                    var time2 = DateTime.Now;
                    var totaltime = time2 - time1;
                    return result;
                }
                else
                {
                    var resultquery = new DBcon<T>().Table.Where(expression).ToList().Where(c => c.Deleted == false);
                    var time2 = DateTime.Now;
                    var totaltime = time2 - time1;
                    return resultquery;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.GetLog(ex, "e916a894-e3d1-4484-99de-4c92096b08e8", LoggerModels.ExcptionType.UnknownError);
                return null;
            }

        }
        public static T Find(params object[] id)
        {
            var value = new DBcon<T>().Table.Find(id);
            if (value.Deleted == false) return value; else return null;
        }
        public static int Add(T model)
        {
            if (string.IsNullOrEmpty(model.UserID) && typeof(T).Name != "Logger")
            {
                throw new DataStoreException("UserID Null");
            }
            var db = new DBcon<T>();
            model.CreateDate = DateTime.Now;
            model.UpdateDate = DateTime.Now;
            model.Deleted = false;
            db.Table.Add(model);
            try
            {
                return db.SaveChanges();
            }
            catch (Exception ex)
            {
                ExceptionHandler.GetLog(ex, model.UserID, LoggerModels.ExcptionType.UnknownError);
                throw new DataStoreException("UnKnown exception please look at the InnerException", ex);
            }
        }
        public static int AddRange(List<T> tables)
        {
            var db = new DBcon<T>();
            foreach (var table in tables)
            {
                if (string.IsNullOrEmpty(table.UserID))
                {
                    throw new Exception("UserID empty you must enter valid UserID");
                }
                table.CreateDate = DateTime.Now;
                table.UpdateDate = DateTime.Now;
                table.Deleted = false;
            }
            db.Table.AddRange(tables);
            return db.SaveChanges();
        }
        public static int Update(T model)
        {
            var db = new DBcon<T>();
            var db2 = new DBcon<T>();
            var data = db2.Table.Find(model.ID);
            if (data != null && data.Deleted != true)
            {
                model.UpdateDate = DateTime.Now;
                model.CreateDate = data.CreateDate;
                model.UserID = data.UserID;
                db.Entry(model).State = EntityState.Modified;
                return db.SaveChanges();
            }
            else
            {
                throw new DataStoreException("We can't find the selected data to update");
            }
        }
        public static int UpdateRange(IEnumerable<T> tables)
        {
            var db = new DBcon<T>();
            foreach (var table in tables)
            {
                db.Entry(table).State = EntityState.Modified;
            }
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
        public static int DeleteRange(IEnumerable<T> ids)
        {
            var db = new DBcon<T>();
            foreach (var id in ids)
            {
                var Table = db.Table.Find(id);
                Table.Deleted = true;
            }
            return db.SaveChanges();
        }
        public static object Anonymous()
        {
            var db = new DBcon<T>();
            var db1 = new DBcon<T>();
            var result = from tt in db.Table join ff in db1.Table on tt.ID equals ff.ID select new { tt.ID, tt.Deleted, ff.CreateDate };
            return result;
        }
    }
    public class DBcon<T> : IdentityDbContext<User> where T : class
    {
        public DBcon(bool lazyload = true)
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            if (!lazyload)
                this.Configuration.LazyLoadingEnabled = false;
        }

        //public DBcon() : base("DefaultConnection")
        //{ }
        public DbSet<T> Table { get; set; }
    }
}
