using GraphQLNetCore.Api.GraphQL.Objects;

namespace GraphQLNetCore.Api.GraphQL
{
    public static class ConfigDIExtention
    {
        public static IServiceCollection ConfigGrapQL(this IServiceCollection services)
        {
            services.AddGraphQLServer()
                .AddQueryType<QueryBook>()
                .AddMutationType<MutationBook>();

            return services;
        }

        public static void ApplyGrapQL(this IEndpointRouteBuilder app)
        {
            app.MapGraphQL();
        }
    }
}
