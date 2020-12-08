using Microsoft.EntityFrameworkCore;
using OfficeEcclesial.App.Database.Entities;
using OfficeEcclesial.Database.Entities;

namespace OfficeEcclesial.App.Database
{
    public sealed class MainDatabase : DbContext
    {

        public MainDatabase(DbContextOptions options) : base(options)
        {
        }

        public DbSet<CaritasMember> CaritasMembers { get; set; }
        public DbSet<CaritasProject> CaritasProject { get; set; }

        public DbSet<CatecumenosAdultos> CatecumenosAdultos { get; set; }
        public DbSet<CatequistaAdultos> CatequistasAdultos { get; set; }

        public DbSet<CatequesisMember> CatequesisMiembros { get; set; }
        public DbSet<CatequesisCatequista> CatequesisCatequistas { get; set; }

        public DbSet<LiturgiaCantores> LiturgiaCantores { get; set; }
        public DbSet<LiturgiaLectores> LiturgiaLectores { get; set; }
        public DbSet<LiturgiaMonaguillos> LiturgiaMonaguillos { get; set; }
        public DbSet<LiturgiaMembers> LiturgiaMembers { get; set; }

        public DbSet<ConsejoParroquialMembers> ConsejoParroquialMembers { get; set; }
        public DbSet<EmausMembers> EmausMembers { get; set; }

        public DbSet<PastoralPenitenciariaMembers> PastoralPenitenciariaMembers { get; set; }
        public DbSet<PastoralPenitenciariaBeneficiaries> PastoralPenitenciariaBeneficiaries { get; set; }


    }
}
