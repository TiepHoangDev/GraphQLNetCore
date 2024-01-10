using HotChocolate.Subscriptions;
using System.Collections;

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

        public async Task<Book?> AddBook(Book book, [Service] ITopicEventSender sender)
        {
            if (book.Id == "") book.Id = DateTime.Now.Ticks.ToString();
            _bookDatabase.Books.Add(book);
            await sender.SendAsync(topicName: nameof(SubscriptionBook.BookAdded), message: book);
            return new QueryBook(_bookDatabase).GetBooks(book.Id).FirstOrDefault();
        }

        public async Task<Book?> UpdateBook(string id, Book book, [Service] ITopicEventSender sender)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException(nameof(id));

            var b = new QueryBook(_bookDatabase).GetBooks(id).FirstOrDefault();
            if (b is Book)
            {
                await DeleteBook(id, sender);
                await AddBook(book, sender);
                return book;
            }
            return default;
        }

        public async Task<bool> DeleteBook(string id, [Service] ITopicEventSender sender)
        {
            var b = new QueryBook(_bookDatabase).GetBooks(id).FirstOrDefault();
            if (b is Book)
            {
                await sender.SendAsync($"BookDelete_by_{b.Author.Name}", b);
                return _bookDatabase.Books.Remove(b);
            }
            return false;
        }

    }


}
