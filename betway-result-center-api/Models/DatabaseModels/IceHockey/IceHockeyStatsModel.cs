using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace betway_result_center_api.Models.DatabaseModels.IceHockey
{
    public class IceHockeyStatsModel
    {
        public decimal MatchId { get; set; }
        public string SeasonName { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }
        public string FirstPeriodScoreHome { get; set; }
        public string FirstPeriodScoreAway { get; set; }
        public string FinishedScoreHome { get; set; }
        public string FinishedScoreAway { get; set; }
        public string FinishedOTScoreHome { get; set; }
        public string FinishedOTScoreAway { get; set; }
        public string FinishedAPScoreHome { get; set; }
        public string FinishedAPScoreAway { get; set; }

        public int getTotalFirstPeriodGoals()
        {
            int outFirstPeriodScoreHome;
            int.TryParse(this.FirstPeriodScoreHome, out outFirstPeriodScoreHome);
            int outFirstPeriodScoreAway;
            int.TryParse(this.FirstPeriodScoreAway, out outFirstPeriodScoreAway);
            return outFirstPeriodScoreHome + outFirstPeriodScoreAway;
        }

    }
}