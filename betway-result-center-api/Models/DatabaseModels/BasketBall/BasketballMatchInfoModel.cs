using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace betway_result_center_api.Models.DatabaseModels.BasketBall
{
    public class BasketballMatchInfoModel
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
        public Int16? ScoreInfoTypeId { get; set; }
        public string HomeScore { get; set; }
        public string AwayScore { get; set; }
       
    }
    public class BasketballMatchVanueInfo {
        public int Attendence { get; set; }
        public string VenueName { get; set; }
    }
    public class BasketballMatchInfoWithVanueModel
    {
        public List<BasketballMatchInfoModel> basketballMatchInfo { get; set; }
        public List<BasketballMatchVanueInfo> basketballMatchVanue { get; set; }
    }
}