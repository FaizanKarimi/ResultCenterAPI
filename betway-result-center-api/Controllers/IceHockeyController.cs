using betway_result_center_api.BLL;
using betway_result_center_api.Filters;
using betway_result_center_api.Models;
using System.Web.Http;
using System.Web.Http.Cors;

namespace betway_result_center_api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api")]
    public class IceHockeyController : ApiController
    {
        [Route("icehockey-countries")]
        [AcceptVerbs("POST")]
        [CacheFilter(false)]
        public IHttpActionResult GetIceHockeyCountryList(GlobalParametersModel globalParametersModel)
        {
            ResponseModel responseModel = new ResponseModel();
            responseModel.data = IceHockeyBLL.GetIceHockeyCountryList(globalParametersModel);
            return Ok(responseModel);
        }

        [Route("icehockey-contest-groups")]
        [AcceptVerbs("POST")]
        [CacheFilter(false)]
        public IHttpActionResult GetIceHockeyContestGroupList(GlobalParametersModel globalParametersModel)
        {
            ResponseModel responseModel = new ResponseModel();
            responseModel.data = IceHockeyBLL.GetIceHockeyContestGroupList(globalParametersModel);
            return Ok(responseModel);
        }

        [Route("icehockey-matches-by-date")]
        [AcceptVerbs("POST")]
        [CacheFilter(false)]
        public IHttpActionResult GetIceHockeyMatchListByDate(GlobalParametersModel globalParametersModel)
        {
            ResponseModel responseModel = new ResponseModel();
            responseModel.data = IceHockeyBLL.GetIceHockeyMatchListByDate(globalParametersModel);
            return Ok(responseModel);
        }

        [Route("icehockey-stats")]
        [AcceptVerbs("POST")]
        [CacheFilter(false)]
        public IHttpActionResult GetIceHockeyStats(GlobalParametersModel globalParameterModel)
        {
            ResponseModel responseModel = new ResponseModel();
            responseModel.data = IceHockeyBLL.GetIceHockeyStats(globalParameterModel);
            return Ok(responseModel);
        }

        [Route("icehockey-standing")]
        [AcceptVerbs("POST")]
        [CacheFilter(false)]
        public IHttpActionResult GetIceHockeyStanding(GlobalParametersModel globalParameterModel)
        {
            ResponseModel responseModel = new ResponseModel();
            responseModel.data = IceHockeyBLL.GetIceHockeyStanding(globalParameterModel);
            return Ok(responseModel);
        }

        [Route("icehockey-rounds")]
        [AcceptVerbs("POST")]
        [CacheFilter(false)]
        public IHttpActionResult GetIceHockeyRoundList(GlobalParametersModel globalParameterModel)
        {
            ResponseModel responseModel = new ResponseModel();
            responseModel.data = IceHockeyBLL.GetIceHockeyRoundList(globalParameterModel);
            return Ok(responseModel);
        }

        [Route("icehockey-matches-by-round-id")]
        [AcceptVerbs("POST")]
        [CacheFilter(false)]
        public IHttpActionResult GetIceHockeyMatchListByRoundId(GlobalParametersModel globalParameterModel)
        {
            ResponseModel responseModel = new ResponseModel();
            responseModel.data = IceHockeyBLL.GetIceHockeyMatchListByRoundId(globalParameterModel);
            return Ok(responseModel);
        }

        [Route("icehockey-contest-teams")]
        [AcceptVerbs("POST")]
        [CacheFilter(false)]
        public IHttpActionResult GetIceHockeyContestTeamList(GlobalParametersModel globalParameterModel)
        {
            ResponseModel responseModel = new ResponseModel();
            responseModel.data = IceHockeyBLL.GetIceHockeyContestTeamList(globalParameterModel);
            return Ok(responseModel);
        }

        [Route("icehockey-head-to-head")]
        [AcceptVerbs("POST")]
        [CacheFilter(false)]
        public IHttpActionResult GetIceHockeyHeadtoHead(GlobalParametersModel globalParameterModel)
        {
            ResponseModel responseModel = new ResponseModel();
            responseModel.data = IceHockeyBLL.GetIceHockeyHeadtoHead(globalParameterModel);
            return Ok(responseModel);
        }
    }
}