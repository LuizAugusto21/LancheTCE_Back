
using LancheTCE_Back.models;
using Microsoft.EntityFrameworkCore;

namespace LancheTCE.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Produtos> produtos {get; set;}

    }
}