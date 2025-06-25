// Licensed to the .NET Foundation under one or more agreements.

using Application.Features.Users.GetUserByEmail;
using Core.Abstractions;
using Domain;
using Domain.Users;
using SharedKernel;

namespace Application.Features.Users.GetUserById
{
    public class GetUserByEmailQueryHandler : IQueryHandler<GetUserByEmailQuery, UserResponse>
    {
        private readonly List<User> _users = new List<User>()
        {
            new(){Id = Guid.NewGuid(), FirstName = "Manu", LastName="Poirier", Email="manu@gmail.com"},
            new(){Id = Guid.NewGuid(), FirstName = "Anna", LastName="Albino", Email="anna@gmail.com"},
        };

        public GetUserByEmailQueryHandler() { }

        public async Task<UserResponse> Handle(GetUserByEmailQuery query, CancellationToken cancellationToken)
        {
            UserResponse? user = _users
                .Where(u => u.Email == query.Email)
                .Select(u => new UserResponse
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email
                })
                .SingleOrDefault();

            return user;
        }
    }
}

