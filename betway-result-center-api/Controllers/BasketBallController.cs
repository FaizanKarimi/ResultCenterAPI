using betway_result_center_api.BLL;
using betway_result_center_api.Filters;
using betway_result_center_api.Models;
using System.Web.Http;
using System.Web.Http.Cors;

namespace betway_result_center_api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api")]
    public class BasketBallController : ApiController
    {
        [Route("basketball-countries")]
        [AcceptVerbs("POST")]
        [CacheFilter(false)]
        public IHttpActionResult GetbasketBallCountryList(GlobalParametersModel globalParametersModel)
        {
            ResponseModel responseModel = new ResponseModel();
            responseModel.data = BasketBallBLL.GetbasketBallCountryList(globalParametersModel);
            return Ok(responseModel);
        }

        [Route("basketball-matches-by-date")]
        [AcceptVerbs("POST")]
        [CacheFilter(false)]
        public IHttpActionResult GetBasketBallMatchListByDate(GlobalParametersModel globalParametersModel)
        {
            ResponseModel responseModel = new ResponseModel();
            responseModel.data = BasketBallBLL.GetBasketBallMatchListByDate(globalParametersModel);
            return Ok(responseModel);
        }

        [Route("basketball-contest-groups")]
        [AcceptVerbs("POST")]
        [CacheFilter(false)]
        public IHttpActionResult GetBasketBallContestGroupList(GlobalParametersModel globalParametersModel)
        {
            ResponseModel responseModel = new ResponseModel();
            responseModel.data = BasketBallBLL.GetBasketBallContestGroupList(globalParametersModel);
            return Ok(responseModel);
        }

        [Route("basketball-standing")]
        [AcceptVerbs("POST")]
        [CacheFilter(false)]
        public IHttpActionResult GetBasketballStandings(GlobalParametersModel globalParameterModel)
        {
            ResponseModel responseModel = new ResponseModel();
            responseModel.data = BasketBallBLL.GetBasketballStandings(globalParameterModel);
            return Ok(responseModel);
        }

        [Route("basketball-rounds")]
        [AcceptVerbs("POST")]
        [CacheFilter(false)]
        public IHttpActionResult GetBasketballRoundList(GlobalParametersModel globalParameterModel)
        {
            ResponseModel responseModel = new ResponseModel();
            responseModel.data = BasketBallBLL.GetBasketballRoundList(globalParameterModel);
            return Ok(responseModel);
        }

        [Route("basketball-stats")]
        [AcceptVerbs("POST")]
        [CacheFilter(false)]
        public IHttpActionResult GetBasketballStats(GlobalParametersModel globalParameterModel)
        {
            ResponseModel responseModel = new ResponseModel();
            responseModel.data = BasketBallBLL.GetBasketballStats(globalParameterModel);
            return Ok(responseModel);
        }

        [Route("basketball-matches-by-round-id")]
        [AcceptVerbs("POST")]
        [CacheFilter(false)]
        public IHttpActionResult GetBasketballMatchListByRoundId(GlobalParametersModel globalParameterModel)
        {
            ResponseModel responseModel = new ResponseModel();
            responseModel.data = BasketBallBLL.GetBasketballMatchListByRoundId(globalParameterModel);
            return Ok(responseModel);
        }

        [Route("basketball-teams")]
        [AcceptVerbs("POST")]
        [CacheFilter(false)]
        public IHttpActionResult GetBasketballContestTeamList(GlobalParametersModel globalParameterModel)
        {
            ResponseModel responseModel = new ResponseModel();
            responseModel.data = BasketBallBLL.GetBasketballContestTeamList(globalParameterModel);
            return Ok(responseModel);
        }

        [Route("basketball-head-to-head")]
        [AcceptVerbs("POST")]
        [CacheFilter(false)]
        public IHttpActionResult GetBasketballHeadToHead(GlobalParametersModel globalParameterModel)
        {
            ResponseModel responseModel = new ResponseModel();
            responseModel.data = BasketBallBLL.GetBasketballHeadToHead(globalParameterModel);
            return Ok(responseModel);
        }

        [Route("basketball-match-info")]
        [AcceptVerbs("POST")]
        [CacheFilter(false)]
        public IHttpActionResult GetBasketballMatchInfo(GlobalParametersModel globalParameterModel)
        {
            ResponseModel responseModel = new ResponseModel();
            responseModel.data = BasketBallBLL.GetBasketballMatchInfo(globalParameterModel);
            return Ok(responseModel);
        }
    }
}