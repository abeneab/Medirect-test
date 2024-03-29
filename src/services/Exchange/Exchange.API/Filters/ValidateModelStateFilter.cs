﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Exchange.API.Filters
{
    public class ValidateModelStateFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
                return;
            var validationErrors = context.ModelState.Keys
                .SelectMany(k => context.ModelState[k].Errors).Select(e => e.ErrorMessage).ToArray();

            var jsonErrorMessage = new JsonErrorResponse
            {
                Messages = validationErrors,
            };
            context.Result = new BadRequestObjectResult(jsonErrorMessage);
        }
    }
}
