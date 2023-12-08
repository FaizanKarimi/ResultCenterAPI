using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace betway_result_center_api.Models.DatabaseModels.Tennis
{
    public class TennisMatchListDBModel
    {
        public Int16 SportId { get; set; }
        public string SportName { get; set; }
        public Int16 CountryId { get; set; }
        public string CountryName { get; set; }
        public int ContestGroupId { get; set; }
        public string ContestGroupName { get; set; }
        public string LeagueName { get; set; }
        public string ContestType { get; set; }
        public string OrgName { get; set; }
        public int SportOrgId { get; set; }
        public decimal MatchId { get; set; }
        public DateTime MatchDate { get; set; }
        public Int16 MatchStatusId { get; set; }
        public string MatchStatus { get; set; }
        public int HomeTeamId { get; set; }
        public string HomeTeamName { get; set; }
        public int AwayTeamId { get; set; }
        public string AwayTeamName { get; set; }
        public decimal? MatchscoreId { get; set; }
        public Int16? ScoreInfoTypeId { get; set; }
        public string HomeScore { get; set; }
        public string AwayScore { get; set; }
        public bool? TieBreak { get; set; }
        public string HomeTieBreakScore { get; set; }
        public string AwayTieBreakScore { get; set; }
        public bool HomeTeamWin { get; set; }
        public bool AwayTeamWin { get; set; }
        public bool MatchInfo { get; set; }
    }
}