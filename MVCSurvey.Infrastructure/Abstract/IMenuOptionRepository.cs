using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NamespaceMedia.Infrastructure.Models.Shared;

namespace NamespaceMedia.Infrastructure.Abstract
{
    public interface IMenuOptionRepository
    {
        IQueryable<MenuOption> GetAll();
        IQueryable<MenuOption> GetTopLevel(string application);
        MenuOption Find(long id);
        void Save(MenuOption option);
        void Delete(MenuOption option);
        void Delete(long id);
        void Update(MenuOption option);
        IQueryable<MenuOption> SearchByApplication(string application);
        IQueryable<MenuOption> GetChildren(string value);
    }
}