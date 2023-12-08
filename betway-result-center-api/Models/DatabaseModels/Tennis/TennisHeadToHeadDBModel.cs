using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace betway_result_center_api.Models.DatabaseModels.Tennis
{
    public class TennisHeadToHeadDBModel
    {
        public List<TennisTeamsH2H> TennisTeamsH2H { get; set; }
        public List<TennisHeadToHeadTeamBioDBModel> TeamBio { get; set; }
        public List<TennisHeadToHeadTeamRecentWinningDBModel> Team1RecentWins { get; set; }
        public List<TennisHeadToHeadTeamRecentWinningDBModel> Team2RecentWins { get; set; }
    }

    public class TennisTeamsH2H
    {
        public int TotalMatchesPlayed { get; set; }
        public int FirstWon { get; set; }
        public int SecondWon { get; set; }
    }

    public class TennisHeadToHeadTeamBioDBModel
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public string Age { get; set; }
        public string CountryOfBirth { get; set; }
        public int HomeAway { get; set; }
        public int? SingleRanking { get; set; }
        public int? RaceToLondon { get; set; }
        public int? CareerSingleTitle { get; set; }
        public int? YTDMatchesLost { get; set; }
        public int? YTDMatchWon { get; set; }
        public int? CareerMatchesWon { get; set; }
        public int? CareerMatchesLost { get; set; }
        public string YTDServiceGamesWon { get; set; }
        public string YTDReturnGamesWon { get; set; }
    }

    public class TennisHeadToHeadTeamRecentWinningDBModel
    {
        public decimal MatchId { get; set; }
        public DateTime MatchDate { get; set; }
        public int HomeTeamId { get; set; }
        public string HomeTeamName { get; set; }
        public int AwayTeamId { get; set; }
        public string AwayTeamName { get; set; }
        public string HomeScore { get; set; }
        public string AwayScore { get; set; }
    }
}