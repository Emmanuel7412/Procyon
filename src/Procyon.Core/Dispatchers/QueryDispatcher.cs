// Licensed to the .NET Foundation under one or more agreements.

using Core.Shared;
using Microsoft.Extensions.DependencyInjection;
using Procyon.Core.Abstractions;

namespace Core.Dispatchers
{
    public class QueryDispatcher(IServiceProvider serviceProvider) : IQueryDispatcher
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

        public Task<Result<TQueryResult>> Dispatch<TQuery, TQueryResult>(TQuery query, CancellationToken cancellation)
            where TQuery : IQuery<TQueryResult>
        {
            var handler = _serviceProvider.GetRequiredService<IQueryHandler<TQuery, TQueryResult>>();
            return handler.Handle(query, cancellation);
        }

    }
}
