using GraphQLNetCore.Api.GraphQL.ShopDb;
using HotChocolate.Subscriptions;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace GraphQLNetCore.Api.GraphQL.Objects
{
    //https://chillicream.com/docs/hotchocolate/v13/defining-a-schema/mutations
    public class MutationBook
    {
        public async Task<Book?> AddBook(Book book, [Service] ITopicEventSender sender, [Service] ShopDbContext _dbContext)
        {
            if (book.Id == "") book.Id = DateTime.Now.Ticks.ToString();
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
            await sender.SendAsync(topicName: nameof(SubscriptionBook.BookAdded), message: book);
            return new QueryBook().GetBooks(_dbContext, book.Id).FirstOrDefault();
        }

        public async Task<Book?> UpdateBook(string id, Book book, [Service] ITopicEventSender sender, [Service] ShopDbContext _dbContext)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException(nameof(id));

            var b = new QueryBook().GetBooks(_dbContext, id).FirstOrDefault();
            if (b is Book)
            {
                await DeleteBook(id, sender, _dbContext);
                await AddBook(book, sender, _dbContext);
                return book;
            }
            return default;
        }

        public async Task<bool> DeleteBook(string id, [Service] ITopicEventSender sender, [Service] ShopDbContext _dbContext)
        {
            var b = new QueryBook().GetBooks(_dbContext, id).FirstOrDefault();
            if (b is Book)
            {
                await sender.SendAsync($"BookDelete_by_{b.Author?.Name}", b);
                _dbContext.Books.Remove(b);
                return _dbContext.SaveChanges() > 0;

            }
            return false;
        }

    }


}
