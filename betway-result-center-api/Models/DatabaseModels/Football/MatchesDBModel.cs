using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace betway_result_center_api.Models.DatabaseModels.Football
{
    public class MatchesDBModel
    {
        public Int16 SportId { get; set; }
        public decimal MatchId { get; set; }
        public Int16 CountryId { get; set; }
        public string CountryName { get; set; }
        public int ContestGroupId { get; set; }
        public int LeagueId { get; set; }
        public string LeagueName { get; set; }
        public string ContestGroup { get; set; }
        public string ContestGroupShortName { get; set; }
        public string MatchDate { get; set; }
        public string MatchTime { get; set; }
        public Int16 MatchStatusId { get; set; }
        public string MatchStatus { get; set; }
        public string FirstToServe { get; set; }
        public decimal MatchCompetitorId { get; set; }
        public int CompetitorId { get; set; }
        public string Competitor { get; set; }
        public string CompetitorShortName { get; set; }
        public bool HomeAway { get; set; }
        public Int16? ScoreInfoTypeId { get; set; }
        public string HomeScore { get; set; }
        public string AwayScore { get; set; }
        public string HomeTieBreakScore { get; set; }
        public string AwayTieBreakScore { get; set; }
        public bool? MinutePlusBit { get; set; }
        public Int16? CurrentMinutes { get; set; }
        public string PlusMinutes { get; set; }
    }
}