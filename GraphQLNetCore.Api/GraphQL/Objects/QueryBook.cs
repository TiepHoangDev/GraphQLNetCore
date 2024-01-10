using HotChocolate.Types.Pagination;

namespace GraphQLNetCore.Api.GraphQL.Objects
{
    public class QueryBook
    {
        private BookDatabase _bookDatabase;

        public QueryBook(BookDatabase bookDatabase)
        {
            _bookDatabase = bookDatabase;
        }

        public Book GetBookDemo() => new Book
        {
            Title = "C# in depth.",
            Author = new Author
            {
                Name = "Jon Skeet"
            }
        };

        public IEnumerable<Book> GetBooks(params string[] ids) => _bookDatabase.Books.Where(q => ids.Length == 0 || ids.Contains(q.Id)).ToList();

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
    }

}
