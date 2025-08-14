using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Shared;
using Microsoft.AspNetCore.Http;

namespace Procyon.Core.Shared
{
    public static class CustomResults
    {
        public static IResult Problem(Result result)
        {
            if (result.IsSuccess)
            {
                throw new InvalidOperationException();
            }
            return Results.Problem(title: result.Error.Code, detail: result.Error.Description, statusCode: result.Error.StatusCode);
        }
    }
}
