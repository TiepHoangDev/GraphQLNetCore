namespace GraphQLNetCore.Api.GraphQL.Objects
{
    //https://chillicream.com/docs/hotchocolate/v13/get-started-with-graphql-in-net-core

    public class Book
    {
        public string Id { get; set; } = "Id";
        public string Title { get; set; } = "Title";

        public Author Author { get; set; } = new Author();
    }

    public class Author
    {
        public string Name { get; set; } = "Author";
    }

    public class BookDatabase
    {
        public List<Book> Books { get; set; } = new List<Book>();
    }

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

    //https://chillicream.com/docs/hotchocolate/v13/defining-a-schema/mutations
    public class MutationBook
    {
        private BookDatabase _bookDatabase;
        public MutationBook(BookDatabase bookDatabase)
        {
            _bookDatabase = bookDatabase;
        }

        public Book? AddBook(Book book)
        {
            _bookDatabase.Books.Add(book);
            return new QueryBook(_bookDatabase).GetBooks(book.Id).FirstOrDefault();
        }

        public Book? UpdateBook(string id, Book book)
        {
            var b = new QueryBook(_bookDatabase).GetBooks(id).FirstOrDefault();
            if (b is Book)
            {
                DeleteBook(id);
                AddBook(book);
                return book;
            }
            return default;
        }

        public bool DeleteBook(string id)
        {
            var b = new QueryBook(_bookDatabase).GetBooks(id).FirstOrDefault();
            if (b is Book) return _bookDatabase.Books.Remove(b);
            return false;
        }

    }


}
