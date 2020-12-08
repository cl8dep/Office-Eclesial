using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeEcclesial.App.Database.Enums;

namespace OfficeEcclesial.App.Database.Entities
{
    public interface IPerson
    {
        string Name { get; set; }
        string LastName { get; set; }
        int Age { get; set; }
        Genre Genre { get; set; }
        string Phone { get; set; }
        string Cellular { get; set; }
        string Address { get; set; }
        int Id { get; set; }
        void Assign(IPerson edited);
    }
}
