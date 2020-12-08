using System.Collections.Generic;
using System.Linq;
using OfficeEcclesial.App.Database.Entities;

namespace OfficeEcclesial.App.Database.Services
{
    public sealed class CatequesisCatequistasService
    {
        #region Properties
        public MainDatabase Database { get; }
        #endregion

        #region Constructor
        public CatequesisCatequistasService(MainDatabase database)
        {
            Database = database;
        }
        #endregion

        public void Add(CatequesisCatequista item)
        {
            Database.CatequesisCatequistas.Add(item);
            Database.SaveChanges();
        }

        public void Remove(int itemId)
        {
            var project = Database.CatequesisCatequistas.SingleOrDefault(x => x.Id == itemId);
            if (project == null)
                return;

            Database.CatequesisCatequistas.Remove(project);
            Database.SaveChanges();
        }

        public IEnumerable<CatequesisCatequista> GetAll(bool order = true)
        {
            object result = Database.CatequesisCatequistas;
            if (order)
                result = ((IQueryable<CatequesisCatequista>)result).OrderBy(x => x.Name);

            return ((IQueryable<CatequesisCatequista>)result).ToList();
        }

        public void Edit(CatequesisCatequista project)
        {
            var item = Database.CatequesisCatequistas.SingleOrDefault(x => x.Id == project.Id);
            if (item == null)
                return;

            item.Assign(project);

            Database.SaveChanges();
        }
    }
}
