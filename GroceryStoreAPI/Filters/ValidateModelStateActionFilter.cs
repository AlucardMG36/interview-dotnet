using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Mime;

namespace GroceryStoreAPI.Filters
{
    public class ValidateModelStateActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (!filterContext.ModelState.IsValid)
            {
                if (filterContext.HttpContext.Request.Method is "GET")
                {
                    var result = new BadRequestResult();

                    filterContext.Result = result;
                }
                else
                {
                    var serializerSettings = new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    };

                    var content = JsonConvert.SerializeObject(filterContext.ModelState, serializerSettings);

                    var result = new ContentResult()
                    {
                        Content = content,
                        ContentType = MediaTypeNames.Application.Json
                    };

                    filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                    filterContext.Result = result;
                }
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
