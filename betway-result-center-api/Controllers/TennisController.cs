using betway_result_center_api.BLL;
using betway_result_center_api.Filters;
using betway_result_center_api.Models;
using System.Web.Http;
using System.Web.Http.Cors;

namespace betway_result_center_api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api")]
    public class TennisController : ApiController
    {
        [Route("tennis-matches-by-date")]
        [AcceptVerbs("POST")]
        [CacheFilter(false)]
        public IHttpActionResult GetMatchListByDate(GlobalParametersModel globalParametersModel)
        {
            ResponseModel responseModel = new ResponseModel();
            responseModel.data = TennisBLL.GetMatchListByDate(globalParametersModel);
            return Ok(responseModel);
        }

        [Route("tennis-draw-by-round-id")]
        [AcceptVerbs("POST")]
        [CacheFilter(false)]
        public IHttpActionResult GetTennisDrawsByRoundId(GlobalParametersModel globalParametersModel)
        {
            ResponseModel responseModel = new ResponseModel();
            responseModel.data = TennisBLL.GetTennisDrawsByRoundId(globalParametersModel);
            return Ok(responseModel);
        }

        [Route("tennis-matchinfo")]
        [AcceptVerbs("POST")]
        [CacheFilter(false)]
        public IHttpActionResult GetMatchInfo(GlobalParametersModel globalParametersModel)
        {
            ResponseModel responseModel = new ResponseModel();
            responseModel.data = TennisBLL.GetMatchInfo(globalParametersModel);
            return Ok(responseModel);
        }

        [Route("tennis-contestgroups-by-type")]
        [AcceptVerbs("POST")]
        [CacheFilter(false)]
        public IHttpActionResult GetContestGroupList(GlobalParametersModel globalParametersModel)
        {
            ResponseModel responseModel = new ResponseModel();
            responseModel.data = TennisBLL.GetContestGroupList(globalParametersModel);
            return Ok(responseModel);
        }

        [Route("tennis-sport-organizations")]
        [AcceptVerbs("POST")]
        [CacheFilter(false)]
        public IHttpActionResult GetSportOrganizationList(GlobalParametersModel globalParametersModel)
        {
            ResponseModel responseModel = new ResponseModel();
            responseModel.data = TennisBLL.GetSportOrganizationList(globalParametersModel);
            return Ok(responseModel);
        }

        [Route("tennis-matches-by-contestgroup-id")]
        [AcceptVerbs("POST")]
        [CacheFilter(false)]
        public IHttpActionResult GetMatchListByContestGroupId(GlobalParametersModel globalParametersModel)
        {
            ResponseModel responseModel = new ResponseModel();
            responseModel.data = TennisBLL.GetMatchListByContestGroupId(globalParametersModel);
            return Ok(responseModel);
        }

        [Route("tennis-stats")]
        [AcceptVerbs("POST")]
        [CacheFilter(false)]
        public IHttpActionResult GetTennisStats(GlobalParametersModel globalParametersModel)
        {
            ResponseModel responseModel = new ResponseModel();
            responseModel.data = TennisBLL.GetTennisStats(globalParametersModel);
            return Ok(responseModel);
        }

        [Route("tennis-head-to-head")]
        [AcceptVerbs("POST")]
        [CacheFilter(false)]
        public IHttpActionResult GetTennisHeadToHead(GlobalParametersModel globalParametersModel)
        {
            ResponseModel responseModel = new ResponseModel();
            responseModel.data = TennisBLL.GetTennisHeadToHead(globalParametersModel);
            return Ok(responseModel);
        }

        [Route("tennis-rounds-by-contest-group-id")]
        [CacheFilter(false)]
        [AcceptVerbs("POST")]
        public IHttpActionResult GetRoundListByContestGroupId(GlobalParametersModel globalParametersModel)
        {
            ResponseModel responseModel = new ResponseModel();
            responseModel.data = TennisBLL.GetRoundListByContestGroupId(globalParametersModel);
            return Ok(responseModel);
        }

        [Route("tennis-players-by-sport-organization-id")]
        [AcceptVerbs("POST")]
        [CacheFilter(false)]
        public IHttpActionResult GetPlayerListBySportOrganizationId(GlobalParametersModel globalParametersModel)
        {
            ResponseModel responseModel = new ResponseModel();
            responseModel.data = TennisBLL.GetPlayerListBySportOrganizationId(globalParametersModel);
            return Ok(responseModel);
        }
    }
}