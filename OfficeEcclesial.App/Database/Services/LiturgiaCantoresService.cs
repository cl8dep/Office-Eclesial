using System.Collections.Generic;
using System.Linq;
using OfficeEcclesial.App.Database.Entities;

namespace OfficeEcclesial.App.Database.Services
{
    public sealed class LiturgiaCantoresService
    {
        #region Properties
        public MainDatabase Database { get; }
        #endregion

        #region Constructor
        public LiturgiaCantoresService(MainDatabase database)
        {
            Database = database;
        }
        #endregion

        public void Add(LiturgiaCantores item)
        {
            Database.LiturgiaCantores.Add(item);
            Database.SaveChanges();
        }

        public void Remove(int itemId)
        {
            var project = Database.LiturgiaCantores.SingleOrDefault(x => x.Id == itemId);
            if (project == null)
                return;

            Database.LiturgiaCantores.Remove(project);
            Database.SaveChanges();
        }

        public IEnumerable<LiturgiaCantores> GetAll(bool order = true)
        {
            object result = Database.LiturgiaCantores;
            if (order)
                result = ((IQueryable<LiturgiaCantores>)result).OrderBy(x => x.Name);

            return ((IQueryable<LiturgiaCantores>)result).ToList();
        }

        public void Edit(LiturgiaCantores project)
        {
            var item = Database.LiturgiaCantores.SingleOrDefault(x => x.Id == project.Id);
            if (item == null)
                return;

            item.Assign(project);

            Database.SaveChanges();
        }
    }
}
