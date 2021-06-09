using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Presentation.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> Logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            Logger = logger;
        }

        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResult =
                HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMsg = "The resource cannot be found";
                    Logger.LogWarning($"400 error occured. Path = { statusCodeResult.OriginalPath }" +
                        $" and QueryString = { statusCodeResult.OriginalQueryString }");
                    break;
            }

            return View("404");
        }

        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            IExceptionHandlerPathFeature exceptionHandler = 
                HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            Logger.LogError($"The path " +
                $"{ exceptionHandler.Path } " +
                $"threw an exception " +
                $"{ exceptionHandler.Error }");

            return View("Error");
        }
    }
}
