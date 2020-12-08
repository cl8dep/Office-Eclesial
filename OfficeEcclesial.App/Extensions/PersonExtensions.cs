using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeEcclesial.App.Database.Entities;

namespace OfficeEcclesial.App.Extensions
{
    public static class PersonExtensions
    {
        public static PastoralPenitenciariaMembers ToPastoralPenitenciariaMembers(this IPerson person)
        {
            return new PastoralPenitenciariaMembers()
            {
                Address = person.Address,
                Age = person.Age,
                Cellular = person.Cellular,
               Genre = person.Genre,
               LastName = person.LastName,
               Name = person.Name,
               Id = person.Id,
               Phone = person.Phone
            };
        }
        public static LiturgiaCantores ToLiturgiaCantores(this IPerson person)
        {
            return new LiturgiaCantores()
            {
                Address = person.Address,
                Age = person.Age,
                Cellular = person.Cellular,
                Genre = person.Genre,
                LastName = person.LastName,
                Name = person.Name,
                Id = person.Id,
                Phone = person.Phone
            };
        }
        public static LiturgiaLectores ToLiturgiaLectores(this IPerson person)
        {
            return new LiturgiaLectores
            {
                Address = person.Address,
                Age = person.Age,
                Cellular = person.Cellular,
                Genre = person.Genre,
                LastName = person.LastName,
                Name = person.Name,
                Id = person.Id,
                Phone = person.Phone
            };
        }
        public static LiturgiaMembers ToLiturgiaMembers(this IPerson person)
        {
            return new LiturgiaMembers
            {
                Address = person.Address,
                Age = person.Age,
                Cellular = person.Cellular,
                Genre = person.Genre,
                LastName = person.LastName,
                Name = person.Name,
                Id = person.Id,
                Phone = person.Phone
            };
        }
        public static LiturgiaMonaguillos ToLiturgiaMonaguillos(this IPerson person)
        {
            return new LiturgiaMonaguillos
            {
                Address = person.Address,
                Age = person.Age,
                Cellular = person.Cellular,
                Genre = person.Genre,
                LastName = person.LastName,
                Name = person.Name,
                Id = person.Id,
                Phone = person.Phone
            };
        }
        public static ConsejoParroquialMembers ToConsejoParroquialMember(this IPerson person)
        {
            return new ConsejoParroquialMembers
            {
                Address = person.Address,
                Age = person.Age,
                Cellular = person.Cellular,
                Genre = person.Genre,
                LastName = person.LastName,
                Name = person.Name,
                Id = person.Id,
                Phone = person.Phone
            };
        }
        public static EmausMembers ToEmausMembers(this IPerson person)
        {
            return new EmausMembers
            {
                Address = person.Address,
                Age = person.Age,
                Cellular = person.Cellular,
                Genre = person.Genre,
                LastName = person.LastName,
                Name = person.Name,
                Id = person.Id,
                Phone = person.Phone
            };
        }
        public static CatequesisCatequista ToCatequesisCatequista(this IPerson person)
        {
            return new CatequesisCatequista
            {
                Address = person.Address,
                Age = person.Age,
                Cellular = person.Cellular,
                Genre = person.Genre,
                LastName = person.LastName,
                Name = person.Name,
                Id = person.Id,
                Phone = person.Phone
            };
        }
        public static CatequistaAdultos ToCatequistaAdultos(this IPerson person)
        {
            return new CatequistaAdultos
            {
                Address = person.Address,
                Age = person.Age,
                Cellular = person.Cellular,
                Genre = person.Genre,
                LastName = person.LastName,
                Name = person.Name,
                Id = person.Id,
                Phone = person.Phone
            };
        }
    
    }
}
