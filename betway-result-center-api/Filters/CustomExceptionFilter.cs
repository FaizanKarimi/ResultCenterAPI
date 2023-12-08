using betway_result_center_api.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace betway_result_center_api.Filters
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            string exceptionMessage = string.Empty;
            if (actionExecutedContext.Exception.InnerException == null)            
                exceptionMessage = actionExecutedContext.Exception.Message;            
            else            
                exceptionMessage = actionExecutedContext.Exception.InnerException.Message;
            
            ResponseModel responseModel = new ResponseModel()
            {
                data = null,
                status = "error",
                message = exceptionMessage
            };
            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.InternalServerError, responseModel);
        }
    }
}