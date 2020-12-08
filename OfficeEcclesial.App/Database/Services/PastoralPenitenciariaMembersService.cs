using System.Collections.Generic;
using System.Linq;
using OfficeEcclesial.App.Database.Entities;

namespace OfficeEcclesial.App.Database.Services
{
    public sealed class PastoralPenitenciariaMembersService
    {
        #region Properties
        public MainDatabase Database { get; }
        #endregion

        #region Constructor
        public PastoralPenitenciariaMembersService(MainDatabase database)
        {
            Database = database;
        }
        #endregion

        public void AddMember(PastoralPenitenciariaMembers item)
        {
            Database.PastoralPenitenciariaMembers.Add(item);
            Database.SaveChanges();
        }

        public void RemoveMember(int itemId)
        {
            var project = Database.PastoralPenitenciariaMembers.SingleOrDefault(x => x.Id == itemId);
            if (project == null)
                return;

            Database.PastoralPenitenciariaMembers.Remove(project);
            Database.SaveChanges();
        }

        public IEnumerable<PastoralPenitenciariaMembers> GetAll(bool order = true)
        {
            object result = Database.PastoralPenitenciariaMembers;
            if (order)
                result = ((IQueryable<PastoralPenitenciariaMembers>)result).OrderBy(x => x.Name);

            return ((IQueryable<PastoralPenitenciariaMembers>)result).ToList();
        }

        public void EditMember(PastoralPenitenciariaMembers project)
        {
            var item = Database.PastoralPenitenciariaMembers.SingleOrDefault(x => x.Id == project.Id);
            if (item == null)
                return;

            item.Assign(project);

            Database.SaveChanges();
        }
    }
}
