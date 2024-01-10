using Microsoft.EntityFrameworkCore;

namespace GraphQLNetCore.Api.GraphQL.Objects
{
    //https://chillicream.com/docs/hotchocolate/v13/get-started-with-graphql-in-net-core

    public class Book
    {
        public string Id { get; set; } = "Id";
        public string? Title { get; set; } = "Title";
        public string? IdAuthor { get; set; } = "IdAuthor";

        public Author? Author { get; set; } = new Author();
    }

    public class Author
    {
        public string Id { get; set; } = "Id";
        public string? Name { get; set; } = "Author";
    }
}
