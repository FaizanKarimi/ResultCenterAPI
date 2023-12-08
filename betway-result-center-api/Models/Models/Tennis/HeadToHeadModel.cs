using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace betway_result_center_api.Models.Models.Tennis
{
    public class HeadToHeadModel
    {



        public TennisTeamsHtwoH TennisTeamsH2H { get; set; }
        public List<TennisHeadToHeadTeamBioModel> TeamBio { get; set; }
        public List<TennisHeadToHeadTeamRecentWinningModel> Team1RecentWins { get; set; }
        public List<TennisHeadToHeadTeamRecentWinningModel> Team2RecentWins { get; set; }
    }

    public class TennisTeamsHtwoH
    {
        public int TotalMatchesPlayed { get; set; }
        public int FirstWon { get; set; }
        public int SecondWon { get; set; }

    }
    public class TennisHeadToHeadTeamBioModel
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public string Age { get; set; }
        public string CountryOfBirth { get; set; }
        public bool HomeAway { get; set; }
        public string SingleRanking { get; set; }
        public string RaceToLondon { get; set; }
        public string CareerSingleTitle { get; set; }
        public string YTDMatchesLost { get; set; }
        public string YTDMatchWon { get; set; }
        public string CareerMatchesWon { get; set; }
        public string CareerMatchesLost { get; set; }
        public string YTDServiceGamesWon { get; set; }
        public string YTDReturnGamesWon { get; set; }

    }
    public class TennisHeadToHeadTeamRecentWinningModel
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
