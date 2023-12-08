using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace betway_result_center_api.Models.DatabaseModels.Tennis
{
    public class TennisMatchInfoDBModel
    {
        public Int16 CountryId { get; set; }
        public string CountryName { get; set; }
        public int ContestGroupId { get; set; }
        public string ContestGroupName { get; set; }
        public string ContestType { get; set; }
        public string OrgName { get; set; }
        public int SportOrgId { get; set; }
        public string LeagueName { get; set; }
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
        public Nullable<int> MatchInfoMatchId { get; set; }
        public Nullable<int> FirstServePointsWonDividerTeam1 { get; set; }
        public Nullable<int> FirstServePointsWonDividendTeam1 { get; set; }
        public Nullable<int> FirstServePointsWonPercentTeam1 { get; set; }
        public Nullable<int> FirstServePointsWonDividerTeam2 { get; set; }
        public Nullable<int> FirstServePointsWonDividendTeam2 { get; set; }
        public Nullable<int> FirstServePointsWonPercentTeam2 { get; set; }
        public Nullable<int> BreakPointsConvertedPercentTeam1 { get; set; }
        public Nullable<int> BreakPointsConvertedDividendTeam1 { get; set; }
        public Nullable<int> BreakPointsConvertedDivisorTeam1 { get; set; }
        public Nullable<int> BreakPointsConvertedPercentTeam2 { get; set; }
        public Nullable<int> BreakPointsConvertedDividendTeam2 { get; set; }
        public Nullable<int> BreakPointsConvertedDivisorTeam2 { get; set; }
        public Nullable<int> ServicePointsWonPercentTeam1 { get; set; }
        public Nullable<int> ServicePointsWonDividendTeam1 { get; set; }
        public Nullable<int> ServicePointsWonDivisorTeam1 { get; set; }
        public Nullable<int> ServicePointsWonPercentTeam2 { get; set; }
        public Nullable<int> ServicePointsWonDividendTeam2 { get; set; }
        public Nullable<int> ServicePointsWonDivisorTeam2 { get; set; }
        public Nullable<int> TotalGamesWonPercentTeam1 { get; set; }
        public Nullable<int> TotalGamesWonDividendTeam1 { get; set; }
        public Nullable<int> TotalGamesWonDivisorTeam1 { get; set; }
        public Nullable<int> TotalGamesWonPercentTeam2 { get; set; }
        public Nullable<int> TotalGamesWonDividendTeam2 { get; set; }
        public Nullable<int> TotalGamesWonDivisorTeam2 { get; set; }
        public Nullable<int> TotalPointsWonPercentTeam1 { get; set; }
        public Nullable<int> TotalPointsWonDividendTeam1 { get; set; }
        public Nullable<int> TotalPointsWonDivisorTeam1 { get; set; }
        public Nullable<int> TotalPointsWonPercentTeam2 { get; set; }
        public Nullable<int> TotalPointsWonDividendTeam2 { get; set; }
        public Nullable<int> TotalPointsWonDivisorTeam2 { get; set; }

    }
}