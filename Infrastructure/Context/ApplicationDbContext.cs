using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> UserTable => Set<User>();
        public DbSet<UserSalt> UserSaltTable => Set<UserSalt>();
        public DbSet<CurrencyType> CurrencyTypeTable => Set<CurrencyType>();
        public DbSet<ExpenseCategories> ExpenseCategoryTable => Set<ExpenseCategories>();
        public DbSet<Expenses> ExpensesTable => Set<Expenses>();
    }
}
