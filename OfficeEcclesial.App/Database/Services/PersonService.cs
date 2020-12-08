using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeEcclesial.App.Database.Entities;

namespace OfficeEcclesial.App.Database.Services
{
    public sealed class PersonService
    {
        #region Properties
        private MainDatabase Database { get; }
        #endregion

        #region Constructor
        public PersonService(MainDatabase database)
        {
            Database = database;
        }
        #endregion

        public IEnumerable<IPerson> GetAll()
        {
            var list = new List<IPerson>();
            list.AddRange(Database.CaritasMembers.Cast<IPerson>().ToList());
            list.AddRange(Database.CaritasProject.Cast<IPerson>().ToList());
            list.AddRange(Database.CatecumenosAdultos.Cast<IPerson>().ToList());
            list.AddRange(Database.CatequesisCatequistas.Cast<IPerson>().ToList());
            list.AddRange(Database.CatequesisMiembros.Cast<IPerson>().ToList());
            list.AddRange(Database.ConsejoParroquialMembers.Cast<IPerson>().ToList());
            list.AddRange(Database.EmausMembers.Cast<IPerson>().ToList());
            list.AddRange(Database.LiturgiaCantores.Cast<IPerson>().ToList());
            list.AddRange(Database.LiturgiaLectores.Cast<IPerson>().ToList());
            list.AddRange(Database.LiturgiaMonaguillos.Cast<IPerson>().ToList());
            list.AddRange(Database.PastoralPenitenciariaBeneficiaries.Cast<IPerson>().ToList());
            list.AddRange(Database.PastoralPenitenciariaMembers.Cast<IPerson>().ToList());

            return list;
        }
    }
}
