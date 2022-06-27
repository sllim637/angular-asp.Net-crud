using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
namespace WebApplication1.data
{
    public class CardsDbContext : DbContext

    {
        public CardsDbContext (DbContextOptions options) : base(options)
        {

        }
        public DbSet<Card> cards { get; set; }
    }
}
