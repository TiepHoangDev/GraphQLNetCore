using GraphQLNetCore.Api.GraphQL.ShopDb;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace GraphQLNetCore.Api.GraphQL.Objects
{
    public class BookBatchDataLoader : BatchDataLoader<string, Book>
    {
        private ShopDbContext _dbContext;

        public BookBatchDataLoader(ShopDbContext dbContext, IBatchScheduler batchScheduler, DataLoaderOptions? options = null) : base(batchScheduler, options)
        {
            this._dbContext = dbContext;
        }

        protected override async Task<IReadOnlyDictionary<string, Book>> LoadBatchAsync(IReadOnlyList<string> keys, CancellationToken cancellationToken)
        {
            return await _dbContext.Books.Where(q => keys.Contains(q.Id)).ToDictionaryAsync(x => x.Id);
        }
    }

}
