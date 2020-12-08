using System.Collections.Generic;
using System.Linq;
using OfficeEcclesial.App.Database.Entities;

namespace OfficeEcclesial.App.Database.Services
{
    public sealed class LiturgiaLectoresService
    {
        #region Properties
        public MainDatabase Database { get; }
        #endregion

        #region Constructor
        public LiturgiaLectoresService(MainDatabase database)
        {
            Database = database;
        }
        #endregion

        public void Add(LiturgiaLectores item)
        {
            Database.LiturgiaLectores.Add(item);
            Database.SaveChanges();
        }

        public void Remove(int itemId)
        {
            var project = Database.LiturgiaLectores.SingleOrDefault(x => x.Id == itemId);
            if (project == null)
                return;

            Database.LiturgiaLectores.Remove(project);
            Database.SaveChanges();
        }

        public IEnumerable<LiturgiaLectores> GetAll(bool order = true)
        {
            object result = Database.LiturgiaLectores;
            if (order)
                result = ((IQueryable<LiturgiaLectores>)result).OrderBy(x => x.Name);

            return ((IQueryable<LiturgiaLectores>)result).ToList();
        }

        public void Edit(LiturgiaLectores project)
        {
            var item = Database.LiturgiaLectores.SingleOrDefault(x => x.Id == project.Id);
            if (item == null)
                return;

            item.Assign(project);

            Database.SaveChanges();
        }
    }
}
