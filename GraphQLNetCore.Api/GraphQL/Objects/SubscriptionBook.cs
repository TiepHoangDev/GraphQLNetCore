namespace GraphQLNetCore.Api.GraphQL.Objects
{
    /// <summary>
    /// https://chillicream.com/docs/hotchocolate/v13/defining-a-schema/subscriptions
    /// </summary>
    public class SubscriptionBook
    {
        [Subscribe]
        [Topic(nameof(SubscriptionBook.BookAdded))] // override topic name (demo)
        public Book BookAdded([EventMessage] Book book)
        {
            return book;
        }


        [Subscribe]
        [Topic("BookDelete_by_{author}")]
        public string BookDelete(string author, [EventMessage] Book book)
        {
            return $"Delete book {book.Title} success book of {author}";
        }
    }

}
