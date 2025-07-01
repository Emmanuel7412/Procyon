// Licensed to the .NET Foundation under one or more agreements.

using Core.Abstractions;
using ManageUser.Domain.Entities;

namespace ManageUser.API.Features.Users.GetUserByEmail
{
    public class GetUserByEmailQueryHandler : IQueryHandler<GetUserByEmailQuery, UserResponse>
    {
        private readonly List<User> _users = new List<User>()
        {
            new(){Id = UserId.Of(Guid.NewGuid()), FirstName = "Manu", LastName="Poirier", Email="manu@gmail.com"},
            new(){Id = UserId.Of(Guid.NewGuid()), FirstName = "Anna", LastName="Albino", Email="anna@gmail.com"},
        };

        public GetUserByEmailQueryHandler() { }

        public async Task<UserResponse> Handle(GetUserByEmailQuery query, CancellationToken cancellationToken)
        {
            var user = _users
                .Where(u => u.Email == query.Email)
                .Select(u => new UserResponse
                {
                    Id = u.Id.Value,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email
                })
                .SingleOrDefault();

            return user;
        }
    }
}

