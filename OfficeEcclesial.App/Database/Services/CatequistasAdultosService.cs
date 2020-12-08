using System.Collections.Generic;
using System.Linq;
using OfficeEcclesial.App.Database.Entities;

namespace OfficeEcclesial.App.Database.Services
{
    public sealed class CatequistasAdultosService
    {
        #region Properties
        public MainDatabase Database { get; }
        #endregion

        #region Constructor
        public CatequistasAdultosService(MainDatabase database)
        {
            Database = database;
        }
        #endregion

        public void Add(CatequistaAdultos item)
        {
            Database.CatequistasAdultos.Add(item);
            Database.SaveChanges();
        }

        public void Remove(int itemId)
        {
            var project = Database.CatequistasAdultos.SingleOrDefault(x => x.Id == itemId);
            if (project == null)
                return;

            Database.CatequistasAdultos.Remove(project);
            Database.SaveChanges();
        }

        public IEnumerable<CatequistaAdultos> GetAll(bool order = true)
        {
            object result = Database.CatequistasAdultos;
            if (order)
                result = ((IQueryable<CatequistaAdultos>)result).OrderBy(x => x.Name);

            return ((IQueryable<CatequistaAdultos>)result).ToList();
        }

        public void Edit(CatequistaAdultos project)
        {
            var item = Database.CatequistasAdultos.SingleOrDefault(x => x.Id == project.Id);
            if (item == null)
                return;

            item.Assign(project);

            Database.SaveChanges();
        }
    }
}
