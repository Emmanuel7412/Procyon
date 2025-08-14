// Licensed to the .NET Foundation under one or more agreements.

using Core.Abstractions;
using Core.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Dispatchers
{
    public class CommandDispatcher(IServiceProvider serviceProvider) : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

        public Task<Result<TCommandResult>> Dispatch<TCommand, TCommandResult>(TCommand command, CancellationToken cancellation) where TCommand : ICommand<TCommandResult>
        {
            var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand, TCommandResult>>();
            return handler.Handle(command, cancellation);
        }
    }
}
