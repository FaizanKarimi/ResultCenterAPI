using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace betway_result_center_api.Models.Models.BasketBall
{
    public class BasketBallModel
    {
        public List<BaeketBallContestGroup> BasketballContestGroups { get; set; }
    }
    public class BaeketBallContestGroup
    {
        public int ContestGroupId { get; set; }
        public string ContestGroupName { get; set; }
        public int? CountryId { get; set; }
        public string CountryName { get; set; }
        public List<BasketBallMatch> BasketballMatches { get; set; }
    }

    public class BasketBallMatchScores
    {
        public decimal? MatchscoreId { get; set; }
        public int? ScoreInfoTypeId { get; set; }
        public string HomeScore { get; set; }
        public string AwayScore { get; set; }
    }

    public class BasketBallMatch
    {
        public decimal MatchId { get; set; }
        public DateTime MatchDate { get; set; }
        public int MatchStatusId { get; set; }
        public string MatchStatus { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }
        public bool HomeTeamWin { get; set; }
        public bool AwayTeamWin { get; set; }
        public List<BasketBallMatchScores> BasketballMatchScores { get; set; }
    }
}