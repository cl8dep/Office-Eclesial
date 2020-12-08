using System.Collections.Generic;
using System.Linq;
using OfficeEcclesial.App.Database.Entities;

namespace OfficeEcclesial.App.Database.Services
{
    public sealed class LiturgiaMonaguillosService
    {
        #region Properties
        public MainDatabase Database { get; }
        #endregion

        #region Constructor
        public LiturgiaMonaguillosService(MainDatabase database)
        {
            Database = database;
        }
        #endregion

        public void Add(LiturgiaMonaguillos item)
        {
            Database.LiturgiaMonaguillos.Add(item);
            Database.SaveChanges();
        }

        public void Remove(int itemId)
        {
            var project = Database.LiturgiaMonaguillos.SingleOrDefault(x => x.Id == itemId);
            if (project == null)
                return;

            Database.LiturgiaMonaguillos.Remove(project);
            Database.SaveChanges();
        }

        public IEnumerable<LiturgiaMonaguillos> GetAll(bool order = true)
        {
            object result = Database.LiturgiaMonaguillos;
            if (order)
                result = ((IQueryable<LiturgiaMonaguillos>)result).OrderBy(x => x.Name);

            return ((IQueryable<LiturgiaMonaguillos>)result).ToList();
        }

        public void Edit(LiturgiaMonaguillos project)
        {
            var item = Database.LiturgiaMonaguillos.SingleOrDefault(x => x.Id == project.Id);
            if (item == null)
                return;

            item.Assign(project);

            Database.SaveChanges();
        }
    }
}
