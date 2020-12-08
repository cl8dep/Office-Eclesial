using System.Collections.Generic;
using System.Linq;
using OfficeEcclesial.App.Database.Entities;

namespace OfficeEcclesial.App.Database.Services
{
    public sealed class ConsejoParroquialMembersService
    {
        #region Properties
        public MainDatabase Database { get; }
        #endregion

        #region Constructor
        public ConsejoParroquialMembersService(MainDatabase database)
        {
            Database = database;
        }
        #endregion

        public void Add(ConsejoParroquialMembers item)
        {
            Database.ConsejoParroquialMembers.Add(item);
            Database.SaveChanges();
        }

        public void Remove(int itemId)
        {
            var project = Database.ConsejoParroquialMembers.SingleOrDefault(x => x.Id == itemId);
            if (project == null)
                return;

            Database.ConsejoParroquialMembers.Remove(project);
            Database.SaveChanges();
        }

        public IEnumerable<ConsejoParroquialMembers> GetAll(bool order = true)
        {
            object result = Database.ConsejoParroquialMembers;
            if (order)
                result = ((IQueryable<ConsejoParroquialMembers>)result).OrderBy(x => x.Name);

            return ((IQueryable<ConsejoParroquialMembers>)result).ToList();
        }

        public void Edit(ConsejoParroquialMembers project)
        {
            var item = Database.ConsejoParroquialMembers.SingleOrDefault(x => x.Id == project.Id);
            if (item == null)
                return;

            item.Assign(project);

            Database.SaveChanges();
        }
    }
}
