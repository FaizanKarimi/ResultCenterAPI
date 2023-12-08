using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace betway_result_center_api.Models.Models.Football
{
  
    public class MatchCompetitorModel
    {
        public decimal MatchCompetitorId { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public string TeamShortName { get; set; }
    }

    public class MatchScoreModel
    {
        public Int16 ScoreInfoTypeId { get; set; }
        public string HomeScore { get; set; }
        public string AwayScore { get; set; }
        public string HomeTieBreakScore { get; set; }
        public string AwayTieBreakScore { get; set; }
    }
}