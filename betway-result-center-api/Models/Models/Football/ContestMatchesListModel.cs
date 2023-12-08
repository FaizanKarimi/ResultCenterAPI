using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace betway_result_center_api.Models.Models.Football
{
    public class ContestMatchesListModel
    {
        public Int16 SportId { get; set; }
        public Int16 CountryId { get; set; }
        public string CountryName { get; set; }
        public int ContestGroupId { get; set; }
        public string ContestGroup { get; set; }
        public string ContestGroupShortName { get; set; }
        public int LeagueId { get; set; }
        public string LeagueName { get; set; }
        public List<ContestMatchesModel> Matches { get; set; }
    }

    public class ContestMatchesModel
    {
        public decimal MatchId { get; set; }
        public string MatchDate { get; set; }
        public string MatchTime { get; set; }
        public Int16 MatchStatusId { get; set; }
        public string MatchStatus { get; set; }
        public string FirstToServe { get; set; }
        //public bool MinutePlusBit { get; set; }
        //public Int16 CurrentMinutes { get; set; }
        //public string PlusMinutes { get; set; }
        public string CurrentMinutes { get; set; }
        public MatchCompetitorModel HomeTeam { get; set; }
        public MatchCompetitorModel AwayTeam { get; set; }
        public MatchScoreModel MatchScore { get; set; }
    }
}