using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NamespaceMedia.Infrastructure.Abstract;
using NamespaceMedia.Infrastructure.Models;
using NamespaceMedia.Infrastructure.Models.Shared;

namespace NamespaceMedia.Infrastructure.Concrete
{
    public class EFMenuOptionRepository : IMenuOptionRepository 
    {

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger
            (typeof(EFMenuOptionRepository));

        private EFContext db = new EFContext();

        public IQueryable<MenuOption> GetAll()
        {
            try
            {
                return db.MenuOptions;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public IQueryable<MenuOption> GetTopLevel(string application)
        {
            try
            {
                return db.MenuOptions.Where(o => string.IsNullOrEmpty(o.parent_value) && o.application.ToLower() == application.ToLower());
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public MenuOption Find(long id)
        {
            try
            {
                return db.MenuOptions.Find(id);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public void Save(MenuOption option)
        {
           try
           {
               db.MenuOptions.Add(option);
               db.SaveChanges();
           }
           catch (Exception ex)
           {
               Log.Error(ex.Message, ex);
               throw ex;
           }
        }

        public void Delete(MenuOption option)
        {
            try
            {
                db.Entry(option).State = EntityState.Deleted;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public void Delete(long id)
        {
            try
            {
                db.Entry(db.MenuOptions.Find(id)).State = EntityState.Deleted;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public void Update(MenuOption option)
        {
            try
            {
                db.Entry(option).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public IQueryable<MenuOption> SearchByApplication(string application)
        {
            try
            {
                return db.MenuOptions.Where(o => o.application.ToLower() == application.ToLower());
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public IQueryable<MenuOption> GetChildren(string value)
        {
            try
            {
                return db.MenuOptions.Where(o => o.parent_value.ToLower() == value.ToLower());
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw ex;
            }
        }
    }
}