using System.Collections.Generic;
using System.Linq;

using OfficeEcclesial.App.Database.Entities;

namespace OfficeEcclesial.App.Database.Services
{
    public sealed class PastoralPenitenciariaBeneficiariesService
    {
        #region Properties
        public MainDatabase Database { get; }
        #endregion

        #region Constructor
        public PastoralPenitenciariaBeneficiariesService(MainDatabase database)
        {
            Database = database;
        }
        #endregion

        public void AddMember(PastoralPenitenciariaBeneficiaries item)
        {
            Database.PastoralPenitenciariaBeneficiaries.Add(item);
            Database.SaveChanges();
        }

        public void RemoveMember(int itemId)
        {
            var project = Database.PastoralPenitenciariaBeneficiaries.Find(itemId);
            if (project == null)
                return;

            Database.PastoralPenitenciariaBeneficiaries.Remove(project);
            Database.SaveChanges();
        }

        public IEnumerable<PastoralPenitenciariaBeneficiaries> GetAll(bool order = true)
        {
            object result = Database.PastoralPenitenciariaBeneficiaries;
            if (order)
                result = ((IQueryable<PastoralPenitenciariaBeneficiaries>)result).OrderBy(x => x.Name);

            return ((IQueryable<PastoralPenitenciariaBeneficiaries>)result).ToList();
        }

        public void EditMember(PastoralPenitenciariaBeneficiaries project)
        {
            var item = Database.PastoralPenitenciariaBeneficiaries.Find(project.Id);
            if (item == null)
                return;

            item.Assign(project);

            Database.SaveChanges();
        }
    }
}
