using Microsoft.EntityFrameworkCore;
using AutoSallon.Domain; // Assuming your Car and User classes are in the Domain layer

namespace AutoSallon.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        // Konstruktor i duhur për DbContext
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


    }
}