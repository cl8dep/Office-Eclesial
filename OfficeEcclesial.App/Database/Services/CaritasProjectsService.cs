using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OfficeEcclesial.App.Database.Entities;
using OfficeEcclesial.Database.Entities;

namespace OfficeEcclesial.App.Database.Services
{
    public sealed class CaritasProjectsService
    {
        #region Properties
        public MainDatabase Database { get; }
        #endregion

        #region Constructor
        public CaritasProjectsService(MainDatabase database)
        {
            Database = database;
        }
        #endregion

        public void AddProject(CaritasProject newCaritasProject)
        {
            Database.CaritasProject.Add(newCaritasProject);
            Database.SaveChanges();
        }

        public void RemoveProject(int itemId)
        {
            var project = Database.CaritasProject.SingleOrDefault(x => x.Id == itemId);
            if (project == null)
                return;

            Database.CaritasProject.Remove(project);
            Database.SaveChanges();
        }

        public IEnumerable<CaritasProject> GetAll(bool order = true)
        {
            object result = Database.CaritasProject
                .Include(x => x.Representant)
                .Include(x => x.Beneficiaries);
            if (order)
                result = ((IQueryable<CaritasProject>) result).OrderBy(x => x.Name);

            return ((IQueryable<CaritasProject>)result).ToList();
        }

        public void EditCaritasProject(CaritasProject project)
        {
            var item = Database.CaritasProject.SingleOrDefault(x => x.Id == project.Id);
            if (item == null)
                return;

            item.Assign(project);

            Database.SaveChanges();
        }
    }
}
