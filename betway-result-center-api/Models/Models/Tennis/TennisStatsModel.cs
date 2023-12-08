using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace betway_result_center_api.Models.Models.Tennis
{
    public class TennisStatsModel
    {
        public string SeasonName { get; set; }
        public List<TennisStatsTeam> Teams { get; set; }
    }

    public class TennisStatsTeam
    {
        public decimal SimplifiedPoints { get; set; }
        public string TeamName { get; set; }
        public string Points { get; set; }
        public int PlayerId { get; set; }
    }
}