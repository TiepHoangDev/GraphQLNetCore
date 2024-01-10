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

        public List<Book> GetBooks(params string[] ids) => _bookDatabase.Books.Where(q => ids.Length == 0 || ids.Contains(q.Id)).ToList();
    }


}
