using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace betway_result_center_api.Models.DatabaseModels.Football
{
    public class TeamsStatsDBModel
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