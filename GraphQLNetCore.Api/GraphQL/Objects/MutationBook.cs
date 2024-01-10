namespace GraphQLNetCore.Api.GraphQL.Objects
{
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
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException(nameof(id));

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
