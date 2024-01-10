using GraphQLNetCore.Api.GraphQL.Objects;
using GraphQLNetCore.Api.GraphQL.ShopDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GraphQLNetCore.Api.GraphQL
{
    public static class ConfigDIExtention
    {
        public static IServiceCollection ConfigGrapQL(this IServiceCollection services)
        {
            services.AddDbContext<ShopDbContext>(options => options.UseInMemoryDatabase("ShopDbContext"));

            services.AddGraphQLServer()
                .AddQueryType<QueryBook>()              // add query
                .AddMutationType<MutationBook>()        // add mutation
                .AddDefaultTransactionScopeHandler()    // add transaction
                .AddSubscriptionType<SubscriptionBook>()// add subscription
                .AddInMemorySubscriptions()             // add In-Memory Provider for subscription
                .AddFiltering()                         // Add Filtering
                ;

            return services;
        }

        public static void ApplyGrapQL(this IEndpointRouteBuilder app)
        {
            app.MapGraphQL();
            app.MapGraphQLWebSocket();
        }
    }
}
