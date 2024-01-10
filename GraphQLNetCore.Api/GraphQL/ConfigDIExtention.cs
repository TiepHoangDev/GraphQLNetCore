using GraphQLNetCore.Api.GraphQL.Objects;

namespace GraphQLNetCore.Api.GraphQL
{
    public static class ConfigDIExtention
    {
        public static IServiceCollection ConfigGrapQL(this IServiceCollection services)
        {
            services.AddGraphQLServer()
                .AddQueryType<QueryBook>()              // add query
                .AddMutationType<MutationBook>()        // add mutation
                .AddDefaultTransactionScopeHandler()    // add transaction
                .AddSubscriptionType<SubscriptionBook>()// add subscription
                .AddInMemorySubscriptions()             // add In-Memory Provider for subscription
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
