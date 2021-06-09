using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Presentation.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMsg = "The resource cannot be found";
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

            ViewBag.ExceptionPath = exceptionHandler.Path;
            ViewBag.ExceptionMsg  = exceptionHandler.Error.Message;
            ViewBag.StackTrace    = exceptionHandler.Error.StackTrace;

            return View("Error");
        }
    }
}
