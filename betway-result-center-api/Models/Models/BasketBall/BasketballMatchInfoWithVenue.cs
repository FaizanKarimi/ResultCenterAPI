using betway_result_center_api.Models.DatabaseModels.BasketBall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace betway_result_center_api.Models.Models.BasketBall
{
    public class BasketballMatchInfoWithVenue
    {
        public string ContestGroupName { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public decimal MatchId { get; set; }
        public DateTime MatchDate { get; set; }
        public int MatchStatusId { get; set; }
        public string MatchStatus { get; set; }
        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }
        public List<BasketBallMatchScores> BasketballMatchScores { get; set; }
        public BasketballMatchVanueInfo MatchVenue { get; set; }
    }
}