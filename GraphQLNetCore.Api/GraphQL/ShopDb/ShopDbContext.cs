using GraphQLNetCore.Api.GraphQL.Objects;
using Microsoft.EntityFrameworkCore;

namespace GraphQLNetCore.Api.GraphQL.ShopDb
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
    }
}
