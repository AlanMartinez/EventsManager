using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace EventService.Filters
{
    public class ApiResponseWrapperAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null) return;

            if (context.Result is ObjectResult objectResult)
            {
                var wrappedResponse = new
                {
                    success = true,
                    data = objectResult.Value
                };

                context.Result = new ObjectResult(wrappedResponse)
                {
                    StatusCode = objectResult.StatusCode
                };
            }
            else if (context.Result is EmptyResult)
            {
                var wrappedResponse = new
                {
                    success = true,
                    data = null as object
                };

                context.Result = new ObjectResult(wrappedResponse)
                {
                    StatusCode = 200
                };
            }

            base.OnActionExecuted(context);
        }
    }
}