using System.Collections.Generic;
using System.Linq;
using OfficeEcclesial.App.Database.Entities;

namespace OfficeEcclesial.App.Database.Services
{
    public sealed class CatequesisMembersService
    {
        #region Properties
        public MainDatabase Database { get; }
        #endregion

        #region Constructor
        public CatequesisMembersService(MainDatabase database)
        {
            Database = database;
        }
        #endregion

        public void Add(CatequesisMember item)
        {
            Database.CatequesisMiembros.Add(item);
            Database.SaveChanges();
        }

        public void Remove(int itemId)
        {
            var project = Database.CatequesisMiembros.SingleOrDefault(x => x.Id == itemId);
            if (project == null)
                return;

            Database.CatequesisMiembros.Remove(project);
            Database.SaveChanges();
        }

        public IEnumerable<CatequesisMember> GetAll(bool order = true)
        {
            object result = Database.CatequesisMiembros;
            if (order)
                result = ((IQueryable<CatequesisMember>)result).OrderBy(x => x.Name);

            return ((IQueryable<CatequesisMember>)result).ToList();
        }

        public void Edit(CatequesisMember project)
        {
            var item = Database.CatequesisMiembros.SingleOrDefault(x => x.Id == project.Id);
            if (item == null)
                return;

            item.Assign(project);

            Database.SaveChanges();
        }
    }
}
