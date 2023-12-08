using betway_result_center_api.Models;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace betway_result_center_api.Handlers
{
    public class CustomMessageHandler : DelegatingHandler
    {
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            ResponseModel responseModel = new ResponseModel();
            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var result = response.Content.ReadAsStringAsync().Result;
                    responseModel = JsonConvert.DeserializeObject<ResponseModel>(result);
                    response.RequestMessage.CreateResponse(HttpStatusCode.OK, responseModel);
                    break;
                case HttpStatusCode.BadRequest:
                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.Forbidden:
                case HttpStatusCode.InternalServerError:
                    var content = response.Content.ReadAsStringAsync().Result;
                    responseModel = JsonConvert.DeserializeObject<ResponseModel>(content);
                    response.RequestMessage.CreateResponse(HttpStatusCode.InternalServerError, responseModel);
                    break;
                default:
                    //Do nothing.
                    break;
            }
            return response;
        }
    }
}