using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OfficeEcclesial.App.Database.Entities;

namespace OfficeEcclesial.App.Database.Services
{
    public class CaritasMembersService
    {
        public MainDatabase Database { get; }

        public CaritasMembersService(MainDatabase db)
        {
            Database = db;
        }

        public void AddCaritasMember(CaritasMember member)
        {
            Database.CaritasMembers.Add(member);
            Database.SaveChanges();
        }

        public IEnumerable<CaritasMember> GetAll(bool order = true)
        {
            var result = Database.CaritasMembers;
            if (!order) return result.ToList();

            var ordered = result.OrderBy(x => x.Name);
            return ordered;
        }

        public void EditCaritasMember(CaritasMember member)
        {
            var item = Database.CaritasMembers.SingleOrDefault(x => x.Id == member.Id);

            if (item == null) return;

            item.Assign(member);

            Database.SaveChanges();
        }

        public void RemoveCaritasMember(int memberId)
        {
            var item = Database.CaritasMembers.SingleOrDefault(x => x.Id == memberId);

            if (item == null) return;

            Database.CaritasMembers.Remove(item);
            Database.SaveChanges();
        }


    }
}
