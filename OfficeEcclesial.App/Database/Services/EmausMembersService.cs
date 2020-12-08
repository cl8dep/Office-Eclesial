using System.Collections.Generic;
using System.Linq;
using OfficeEcclesial.App.Database.Entities;

namespace OfficeEcclesial.App.Database.Services
{
    public sealed class EmausMembersService
    {
        #region Properties
        public MainDatabase Database { get; }
        #endregion

        #region Constructor
        public EmausMembersService(MainDatabase database)
        {
            Database = database;
        }
        #endregion

        public void Add(EmausMembers item)
        {
            Database.EmausMembers.Add(item);
            Database.SaveChanges();
        }

        public void Remove(int itemId)
        {
            var project = Database.EmausMembers.SingleOrDefault(x => x.Id == itemId);
            if (project == null)
                return;

            Database.EmausMembers.Remove(project);
            Database.SaveChanges();
        }

        public IEnumerable<EmausMembers> GetAll(bool order = true)
        {
            object result = Database.EmausMembers;
            if (order)
                result = ((IQueryable<EmausMembers>)result).OrderBy(x => x.Name);

            return ((IQueryable<EmausMembers>)result).ToList();
        }

        public void Edit(EmausMembers project)
        {
            var item = Database.EmausMembers.SingleOrDefault(x => x.Id == project.Id);
            if (item == null)
                return;

            item.Assign(project);

            Database.SaveChanges();
        }
    }
}
