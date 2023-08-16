using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contexts
{
    public class HirdavatContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HIRDAVAT;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        public DbSet<Il> Il { get; set; }
        public DbSet<Ilce> Ilce { get; set; }
        public DbSet<Kategori> Kategori { get; set; }
        public DbSet<Marka> Marka { get; set; }
        public DbSet<Musteri> Musteri { get; set; }
        public DbSet<Siparis> Siparis { get; set; }
        public DbSet<SiparisDetay> SiparisDetay { get; set; }
        public DbSet<SiparisDurum> SiparisDurum { get; set; }
        public DbSet<Urun> Urun { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<Sepet> Sepet { get; set; }
        public DbSet<MusteriRol> MusteriRol { get; set; }
    }
}
