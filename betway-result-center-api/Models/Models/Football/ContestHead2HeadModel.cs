using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace betway_result_center_api.Models.Models.Football
{
    public class ContestHead2HeadModel
    {
        public List<Head2HeadMatchDetailModel> Head2HeadMatches { get; set; }
        public List<LastFifteenMatchesModel> LastFifteenHomeMatches { get; set; }
        public List<LastFifteenMatchesModel> LastFifteenAwayMatches { get; set; }
        public LeagueTableModel LeagueTable { get; set; }
        public List<ContestTeamsStatsModel> HomeTeamStatsMarkets { get; set; }
        public List<ContestTeamsStatsModel> AwayTeamStatsMarkets { get; set; }
    }

    public class ContestTeamsStatsModel
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public int MatchesPlayed { get; set; }
        public int MarketId { get; set; }
        public string MarketName { get; set; }
        public string Position { get; set; }
        public decimal Percentage { get; set; }
    }
}