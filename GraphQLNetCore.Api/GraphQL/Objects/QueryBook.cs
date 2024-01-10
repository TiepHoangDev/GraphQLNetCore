using GraphQLNetCore.Api.GraphQL.ShopDb;
using HotChocolate.Types.Pagination;
using Microsoft.EntityFrameworkCore;

namespace GraphQLNetCore.Api.GraphQL.Objects
{
    public class QueryBook
    {
        public Book GetBookDemo() => new Book
        {
            Title = "C# in depth.",
            Author = new Author
            {
                Name = "Jon Skeet"
            }
        };

        public IEnumerable<Book> GetBooks([Service] ShopDbContext _dbContext, params string[] ids) => _dbContext.Books.Where(q => ids.Length == 0 || ids.Contains(q.Id)).ToList();

        IEnumerable<Book> AllBook => Enumerable.Range(1, 10000).Select(idx => new Book
        {
            Id = idx.ToString(),
            Title = $"Book {idx}",
            Author = new Author
            {
                Name = "SearchBooks"
            }
        });

        [UsePaging]
        public Connection<Book> BookPaging(int after = 0, int first = 10, int last = 0, int before = 0)
        {
            var data = AllBook;
            if (before > 0) data = data.Take(before);
            if (after > 0) data = data.Skip(after);
            var edges = data
                .Take(first)
                .Concat(data.TakeLast(last))
                .Select(u => new Edge<Book>(u, u.Id))
                .ToList();
            var pageInfo = new ConnectionPageInfo(false, false, null, null);
            var connection = new Connection<Book>(edges, pageInfo, ct => ValueTask.FromResult(AllBook.Count()));
            return connection;
        }

        [UsePaging]
        [UseFiltering]
        public IEnumerable<Book> EfCoreGetBooks([Service] ShopDbContext dbContext) => dbContext.Books;
    }

}
