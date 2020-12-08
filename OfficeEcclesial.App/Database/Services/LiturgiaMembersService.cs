using System.Collections.Generic;
using System.Linq;
using OfficeEcclesial.App.Database.Entities;

namespace OfficeEcclesial.App.Database.Services
{
    public sealed class LiturgiaMembersService
    {
        #region Properties
        public MainDatabase Database { get; }
        #endregion

        #region Constructor
        public LiturgiaMembersService(MainDatabase database)
        {
            Database = database;
        }
        #endregion

        public void Add(LiturgiaMembers item)
        {
            Database.LiturgiaMembers.Add(item);
            Database.SaveChanges();
        }

        public void Remove(int itemId)
        {
            var project = Database.LiturgiaMembers.SingleOrDefault(x => x.Id == itemId);
            if (project == null)
                return;

            Database.LiturgiaMembers.Remove(project);
            Database.SaveChanges();
        }

        public IEnumerable<LiturgiaMembers> GetAll(bool order = true)
        {
            object result = Database.LiturgiaMembers;
            if (order)
                result = ((IQueryable<LiturgiaMembers>)result).OrderBy(x => x.Name);

            return ((IQueryable<LiturgiaMembers>)result).ToList();
        }

        public void Edit(LiturgiaMembers project)
        {
            var item = Database.LiturgiaMembers.SingleOrDefault(x => x.Id == project.Id);
            if (item == null)
                return;

            item.Assign(project);

            Database.SaveChanges();
        }
    }
}
