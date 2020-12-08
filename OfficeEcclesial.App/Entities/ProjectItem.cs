using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeEcclesial.App.Database.Entities;
using OfficeEcclesial.Database.Entities;

namespace OfficeEcclesial.App.Entities
{
    public sealed class ProjectItem
    {
        public ProjectItem(CaritasProject caritasProject)
        {
            Id = caritasProject.Id;
            Name = caritasProject.Name;
            Representant = caritasProject.Representant;
            Beneficiaries = caritasProject.Beneficiaries;
        }

        public int Id { get; set; }

        public IEnumerable<Person> Beneficiaries { get; set; }

        public Person Representant { get; set; }

        public string Name { get; set; }

        public bool IsSelected { get; set; }
    }
}
