// Licensed to the .NET Foundation under one or more agreements.

using Core.Abstractions;
using ManageUser.API.Features.Users.GetUserByEmail;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(IQueryDispatcher queryDispatcher) : ControllerBase
    {
        private readonly IQueryDispatcher _queryDispatcher = queryDispatcher ?? throw new ArgumentNullException(nameof(queryDispatcher));

        [HttpGet("{email}")]
        public async Task<IActionResult> GetUserByEmail(string email, CancellationToken cancellationToken)
        {
            var query = new GetUserByEmailQuery(email);
            var user = await _queryDispatcher.Dispatch<GetUserByEmailQuery, UserResponse>(query, cancellationToken);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}
