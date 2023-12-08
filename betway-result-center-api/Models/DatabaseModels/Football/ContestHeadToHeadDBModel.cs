using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace betway_result_center_api.Models.DatabaseModels.Football
{
    public class ContestHeadToHeadDBModel
    {
        public List<FootBallLeagueTableDBModel> LeagueTeble { get; set; }
        public List<FootBallLastFifteenMatchesDBModel> LastFifteenHomeMatches { get; set; }
        public List<FootBallLastFifteenMatchesDBModel> LastFifteenAwayMatches { get; set; }
        public List<FootBallHead2HeadMatchDetailDBModel> Head2HeadMatches { get; set; }
        public List<FootBallTeamsStatsModelDBModel> homeTeamStats { get; set; }
        public List<FootBallTeamsStatsModelDBModel> awayTeamStats { get; set; }
    }
    public class FootBallLeagueTableDBModel
    {
        public int LeagueTableId { get; set; }
        public int ContestGroupId { get; set; }
        public string ContestGroupName { get; set; }
        public Int16 SeasonId { get; set; }
        public decimal LeagueTableCompetitorId { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public Int16 Place { get; set; }
        public decimal LeagueTableMatchId { get; set; }
        public Int16 Played { get; set; }
        public Int16 Won { get; set; }
        public Int16 Lost { get; set; }
        public Int16 Draws { get; set; }
        public Int16 Scored { get; set; }
        public Int16 Conceded { get; set; }
        public Int16 Difference { get; set; }
        public Int16 Points { get; set; }
        public string Type { get; set; }
    }
    public class FootBallLastFifteenMatchesDBModel
    {
        public decimal MatchId { get; set; }
        public DateTime MatchDate { get; set; }
        public int HomeTeamId { get; set; }
        public string HomeTeam { get; set; }
        public string HomeTeamShortName { get; set; }
        public int AwayTeamId { get; set; }
        public string AwayTeam { get; set; }
        public string AwayTeamShortName { get; set; }
        public string HomeScore { get; set; }
        public string AwayScore { get; set; }
        public Int16 CountryId { get; set; }
        public string CountryName { get; set; }
        public int ContestGroupId { get; set; }
        public string ContestGroupName { get; set; }
    }
    public class FootBallHead2HeadMatchDetailDBModel
    {
        public decimal MatchId { get; set; }
        public DateTime MatchDate { get; set; }
        public int HomeTeamId { get; set; }
        public string HomeTeam { get; set; }
        public string HomeTeamShortName { get; set; }
        public int AwayTeamId { get; set; }
        public string AwayTeam { get; set; }
        public string AwayTeamShortName { get; set; }
        public string HomeScore { get; set; }
        public string AwayScore { get; set; }
        public Int16 CountryId { get; set; }
        public string CountryName { get; set; }
        public int ContestGroupId { get; set; }
        public string ContestGroupName { get; set; }
        public Int16 SeasonId { get; set; }
        public string SeasonName { get; set; }
    }
    public class FootBallTeamsStatsModelDBModel
    {
        public int ContestGroupId { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public int MatchesPlayed { get; set; }
        public int MarketId { get; set; }
        public string MarketName { get; set; }
        public int Position { get; set; }
        public decimal Percentage { get; set; }
    }
}