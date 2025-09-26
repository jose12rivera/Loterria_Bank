using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Loterria_Bank.DAL
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options)
            : base(options) { }

        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Loterias> Loterias { get; set; }
        public DbSet<Jugadas> Jugadas { get; set; }
        public DbSet<DetalleJugadas> DetalleJugadas { get; set; }
    }
}
