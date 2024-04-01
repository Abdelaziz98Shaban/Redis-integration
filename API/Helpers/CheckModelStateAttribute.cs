using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Helpers
{
    public class CheckModelStateAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string message = "";
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Select(x => x.Value.Errors)
                 .Where(y => y.Count > 0)
                 .Select(s => s.Select(s => s.ErrorMessage).FirstOrDefault())
                .ToList();
                message = string.Join(",", errors);

                context.Result = new BadRequestObjectResult(new  { Result = false, Msg = message });

                return; //short circuit the request
            }

            message = "The model is valid";

            await next();
        }
    }
}
