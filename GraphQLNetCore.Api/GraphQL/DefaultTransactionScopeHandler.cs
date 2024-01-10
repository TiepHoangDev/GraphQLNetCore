using HotChocolate.Execution;
using HotChocolate.Execution.Processing;
using System.Transactions;

namespace GraphQLNetCore.Api.GraphQL
{
    public class DefaultTransactionScopeHandler : ITransactionScopeHandler
    {
        public ITransactionScope Create(IRequestContext context)
        {
            var transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted
            });
            return new DefaultTransactionScope(context, transactionScope);
        }
    }
}
