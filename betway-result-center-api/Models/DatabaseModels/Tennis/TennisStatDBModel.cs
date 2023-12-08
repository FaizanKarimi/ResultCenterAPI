using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace betway_result_center_api.Models.DatabaseModels.Tennis
{
    public class TennisStatDBModel
    {
        public decimal SimplifiedPoints { get; set; }
        public string TeamName { get; set; }
        public string Points { get; set; }
        public string SeasonName { get; set; }
        public int PlayerId { get; set; }
    }

    public class TennisDrawDBModel
    {
        public Decimal MatchId { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string HomeScore { get; set; }
        public string AwayScore { get; set; }
        public Int16? ScoreInfoTypeId { get; set; }
        public bool HomeTeamWin { get; set; }
        public bool AwayTeamWin { get; set; }
        public int? HomeTeamRanking { get; set; }
        public int? AwayTeamRanking { get; set; }
        public int MatchStatusId { get; set; }
        public DateTime MatchDate { get; set; }
    }

    public class TennisDrawModel
    {
        public decimal MatchId { get; set; }
        public int MatchStatusId { get; set; }
        public DateTime MatchDate { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public bool HomeTeamWin { get; set; }
        public bool AwayTeamWin { get; set; }
        public int? HomeTeamRanking { get; set; }
        public int? AwayTeamRanking { get; set; }
        public List<TennisMatchScores> MatchScores { get; set; }
    }

    public class TennisMatchScores
    {
        public int? ScoreInfoTypeId { get; set; }
        public string HomeScore { get; set; }
        public string AwayScore { get; set; }
    }
}