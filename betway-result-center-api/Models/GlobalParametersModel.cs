using System;

namespace betway_result_center_api.Models
{
    public class GlobalParametersModel
    {
        public Int16 SportId { get; set; }
        public DateTime FromDate { get; set; }
        public string MatchType { get; set; }
        public decimal MatchId { get; set; }
        public int LeagueId { get; set; }
        public string LeagueName { get; set; }
        public Int64 OddsMatchId { get; set; }
        public Int16 BetTypeId { get; set; }
        public bool InPlay { get; set; }
        public int ContestGroupId { get; set; }
        public int ContestGroupRoundId { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public int WeekNumber { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int PlayerId { get; set; }
        public Int16 CountryId { get; set; }
        public Int16 SportOrgId { get; set; }
        public int TennisStatsRankingId { get; set; }
        public int firstTeamId { get; set; }
        public int secondTeamId { get; set; }
        public int SeasonId { get; set; }
        public int StatsTypeId { get; set; }
        public bool IsLive { get; set; }
        public Int16 GenderId { get; set; }
        public Int16 SportOrgTypeId { get; set; }
        public int LanguageCode { get; set; }
        public string Language { get; set; }
    }
}