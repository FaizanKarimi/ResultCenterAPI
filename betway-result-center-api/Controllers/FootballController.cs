using betway_result_center_api.BLL;
using betway_result_center_api.Filters;
using betway_result_center_api.Models;
using betway_result_center_api.Models.Models.Football;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace betway_result_center_api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api")]
    public class FootballController : ApiController
    {
        [Route("football-countries")]
        [AcceptVerbs("POST")]
        [CacheFilter(false)]
        public IHttpActionResult GetCountriesList(GlobalParametersModel globalParametersModel)
        {
            ResponseModel responseModel = new ResponseModel();
            responseModel.data = FootballBLL.GetCountriesList(globalParametersModel);
            return Ok(responseModel);
        }

        [Route("football-contestgroups-by-country")]
        [AcceptVerbs("POST")]
        [CacheFilter(false)]
        public IHttpActionResult GetContestGroupsBycountry(GlobalParametersModel globalParametersModel)
        {
            ResponseModel responseModel = new ResponseModel();
            responseModel.data = FootballBLL.GetContestGroupsBycountry(globalParametersModel);
            return Ok(responseModel);
        }

        [Route("football-matches-by-date-group-by-contest")]
        [AcceptVerbs("POST")]
        [CacheFilter(false)]
        public IHttpActionResult GetMatchesGroupByContest(GlobalParametersModel globalParametersModel)
        {
            globalParametersModel.PageSize = globalParametersModel.PageSize < 10 ? 10 : globalParametersModel.PageSize;
            ResponseModel responseModel = new ResponseModel();
            List<ContestMatchesListModel> contestMatchesListModel = FootballBLL.GetMatchesGroupByContest(globalParametersModel);
            responseModel.data = contestMatchesListModel.Skip(globalParametersModel.PageIndex * globalParametersModel.PageSize).Take(globalParametersModel.PageSize).ToList();
            return Ok(responseModel);
        }

        [Route("football-matches-by-league-round")]
        [AcceptVerbs("POST")]
        [CacheFilter(false)]
        public IHttpActionResult GetMatchesByContestRound(GlobalParametersModel globalParametersModel)
        {
            ResponseModel responseModel = new ResponseModel();
            if (globalParametersModel.ContestGroupRoundId == 0)
            {
                var dbContest = FootballBLL.GetMatchesByContestRound(globalParametersModel).Where(cg => cg.IsSelected == 1).FirstOrDefault();
                if (dbContest != null)
                    globalParametersModel.ContestGroupRoundId = dbContest.ContestGroupRoundId;
            }
            responseModel.data = FootballBLL.GetAllMatchesByContestRound(globalParametersModel);
            return Ok(responseModel);
        }

        [Route("football-contest-rounds-by-id")]
        [AcceptVerbs("POST")]
        [CacheFilter(false)]
        public IHttpActionResult GetContestRoundsById(GlobalParametersModel globalParametersModel)
        {
            ResponseModel responseModel = new ResponseModel();
            responseModel.data = FootballBLL.GetContestRoundsById(globalParametersModel);
            return Ok(responseModel);
        }

        [Route("football-match-detail-by-matchid")]
        [AcceptVerbs("POST")]
        [CacheFilter(false)]
        public IHttpActionResult GetMatchDetail(GlobalParametersModel globalParametersModel)
        {
            ResponseModel responseModel = new ResponseModel();
            responseModel.data = FootballBLL.GetMatchDetail(globalParametersModel);
            return Ok(responseModel);
        }

        [Route("football-league-table")]
        [AcceptVerbs("POST")]
        [CacheFilter(false)]
        public IHttpActionResult GetLeagueTableByContestGroupId(GlobalParametersModel globalParametersModel)
        {
            ResponseModel responseModel = new ResponseModel();
            responseModel.data = FootballBLL.GetLeagueTableByContestGroupId(globalParametersModel);
            return Ok(responseModel);
        }

        [Route("football-competitor-head2head")]
        [AcceptVerbs("POST")]
        [CacheFilter(false)]
        public IHttpActionResult GetCompetitorsHeadToHead(GlobalParametersModel globalParametersModel)
        {
            ResponseModel responseModel = new ResponseModel();
            responseModel.data = FootballBLL.GetCompetitorsHeadToHead(globalParametersModel);
            return Ok(responseModel);

            #region EF
            //string status = string.Empty;
            //string message = string.Empty;
            //string responseSource = string.Empty;
            //ContestHead2HeadModel model = new ContestHead2HeadModel();
            //try
            //{
            //    model = FootballBLL.GetCompetitorsHeadToHead(globalParametersModel.ContestGroupId, globalParametersModel.HomeTeamId, globalParametersModel.AwayTeamId);
            //}
            //catch (Exception)
            //{
            //    status = "error";
            //    message = "An internal error occurred. Our supporting staff has been notified of this error and will address the issue shortly. We apologize for inconvenience.";
            //}

            //var resultObj = JObject.FromObject(new
            //{
            //    status = status,
            //    message = message,
            //    data = model
            //});

            //return Ok(resultObj); 
            #endregion
        }

        [Route("football-contest-teams")]
        [AcceptVerbs("POST")]
        [CacheFilter(false)]
        public IHttpActionResult GetContestTeams(GlobalParametersModel globalParametersModel)
        {
            ResponseModel responseModel = new ResponseModel();
            responseModel.data = FootballBLL.GetContestTeams(globalParametersModel);
            return Ok(responseModel);

            #region EF
            //string status = string.Empty;
            //string message = string.Empty;
            //string responseSource = string.Empty;
            //List<TeamsModel> model = new List<TeamsModel>();
            //try
            //{
            //    model = FootballBLL.GetContestTeams(globalParametersModel.ContestGroupId);
            //    status = "success";
            //    message = "data returned from " + responseSource;
            //}
            //catch (Exception ex)
            //{
            //    status = "error";
            //    message = "An internal error occurred. Our supporting staff has been notified of this error and will address the issue shortly. We apologize for inconvenience.";
            //    message = ex + "";
            //}

            //var resultObj = JObject.FromObject(new
            //{
            //    status = status,
            //    message = message,
            //    data = model
            //});

            //return Ok(resultObj); 
            #endregion
        }

        [Route("football-contestgroup-stats")]
        [AcceptVerbs("POST")]
        [CacheFilter(false)]
        public IHttpActionResult GetContestGroupStats(GlobalParametersModel globalParametersModel)
        {
            ResponseModel responseModel = new ResponseModel();
            responseModel.data = FootballBLL.GetContestGroupStats(globalParametersModel);
            return Ok(responseModel);
        }
    }
}