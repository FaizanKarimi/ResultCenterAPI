using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace betway_result_center_api.Models.Models.Football
{
    public class ContestGroupStatsModel
    {
        public Int16 CountryId { get; set; }
        public string CountryName { get; set; }
        public int ContestGroupId { get; set; }
        public string ContestGroupName { get; set; }
        public Int16 SeasonId { get; set; }
        public string SeasonName { get; set; }

        public decimal HomeResult { get; set; }
        public decimal DrawResult { get; set; }
        public decimal AwayResult { get; set; }

        public decimal TotalAvg { get; set; }
        public decimal HomeAvg { get; set; }
        public decimal AwayAvg { get; set; }

        public decimal Over15Value { get; set; }
        public decimal Over25Value { get; set; }
        public decimal Over35Value { get; set; }

        public int? TeamId_HighestGoalsPerMatch { get; set; }
        public string Team_HighestGoalsPerMatch { get; set; }
        public decimal? Highest_HighestGoalsPerMatch { get; set; }

        public int? TeamId_LowestGoalsPerMatch { get; set; }
        public string Team_LowestGoalsPerMatch { get; set; }
        public decimal? Highest_LowestGoalsPerMatch { get; set; }

        public decimal? Over05Value1H { get; set; }
        public decimal? Over15Value1H { get; set; }
        public decimal? Over25Value1H { get; set; }

        public int? TeamId_HighestGoalsPerMatchIn1stHalf { get; set; }
        public string Team_HighestGoalsPerMatchIn1stHalf { get; set; }
        public decimal? Highest_HighestGoalsPerMatchIn1stHalf { get; set; }

        public int? TeamId_LowestGoalsPerMatchIn1stHalf { get; set; }
        public string Team_LowestGoalsPerMatchIn1stHalf { get; set; }
        public decimal? Highest_LowestGoalsPerMatchIn1stHalf { get; set; }

        public decimal? BTTSPercentage { get; set; }

        public int? TeamId_BothTeamsToScore1 { get; set; }
        public string Team_BothTeamsToScore1 { get; set; }
        public decimal? Highest_BothTeamsToScore1 { get; set; }
        public int? MatchesPlayed_BothTeamsToScore1 { get; set; }
        public int? TeamId_BothTeamsToScore2 { get; set; }
        public string Team_BothTeamsToScore2 { get; set; }
        public decimal? Highest_BothTeamsToScore2 { get; set; }
        public int? MatchesPlayed_BothTeamsToScore2 { get; set; }
        public int? TeamId_BothTeamsToScore3 { get; set; }
        public string Team_BothTeamsToScore3 { get; set; }
        public decimal? Highest_BothTeamsToScore3 { get; set; }
        public int? MatchesPlayed_BothTeamsToScore3 { get; set; }

        public int? TeamId_LowestBothTeamsToScore1 { get; set; }
        public string Team_LowestBothTeamsToScore1 { get; set; }
        public decimal? Highest_LowestBothTeamsToScore1 { get; set; }
        public int? MatchesPlayed_LowestBothTeamsToScore1 { get; set; }

        public int? TeamId_HighestCleanSheets1 { get; set; }
        public string Team_HighestCleanSheets1 { get; set; }
        public decimal? Highest_HighestCleanSheets1 { get; set; }
        public int? MatchesPlayed_HighestCleanSheets1 { get; set; }
        public int? TeamId_HighestCleanSheets2 { get; set; }
        public string Team_HighestCleanSheets2 { get; set; }
        public decimal? Highest_HighestCleanSheets2 { get; set; }
        public int? MatchesPlayed_HighestCleanSheets2 { get; set; }

        public int? TeamId_LowestCleanSheets1 { get; set; }
        public string Team_LowestCleanSheets1 { get; set; }
        public decimal? Highest_LowestCleanSheets1 { get; set; }
        public int? MatchesPlayed_LowestCleanSheets1 { get; set; }

        public int? TeamId_HighestFailedToScore1 { get; set; }
        public string Team_HighestFailedToScore1 { get; set; }
        public decimal? Highest_HighestFailedToScore1 { get; set; }
        public int? MatchesPlayed_HighestFailedToScore1 { get; set; }

        public int? TeamId_LowestFailedToScore1 { get; set; }
        public string Team_LowestFailedToScore1 { get; set; }
        public decimal? Highest_LowestFailedToScore1 { get; set; }
        public int? MatchesPlayed_LowestFailedToScore1 { get; set; }

        public int? TeamId_HighestScoreTheFirstGoal1 { get; set; }
        public string Team_HighestScoreTheFirstGoal1 { get; set; }
        public decimal? Highest_HighestScoreTheFirstGoal1 { get; set; }
        public int? MatchesPlayed_HighestScoreTheFirstGoal1 { get; set; }

        public int? TeamId_FewestScoreTheFirstGoal1 { get; set; }
        public string Team_FewestScoreTheFirstGoal1 { get; set; }
        public decimal? Highest_FewestScoreTheFirstGoal1 { get; set; }
        public int? MatchesPlayed_FewestScoreTheFirstGoal1 { get; set; }

        public int? TeamId_HighestConsecutiveLoses1 { get; set; }
        public string Team_HighestConsecutiveLoses1 { get; set; }
        public decimal? Highest_HighestConsecutiveLoses1 { get; set; }
        public int? MatchesPlayed_HighestConsecutiveLoses1 { get; set; }

        public int? TeamId_HighestConsecutiveWins1 { get; set; }
        public string Team_HighestConsecutiveWins1 { get; set; }
        public decimal? Highest_HighestConsecutiveWins1 { get; set; }
        public int? MatchesPlayed_HighestConsecutiveWins1 { get; set; }

        public int? TeamId_HighestConsecutiveWinsAsHome1 { get; set; }
        public string Team_HighestConsecutiveWinsAsHome1 { get; set; }
        public decimal? Highest_HighestConsecutiveWinsAsHome1 { get; set; }
        public int? MatchesPlayed_HighestConsecutiveWinsAsHome1 { get; set; }

        public int? TeamId_HighestConsecutiveWinsAsAway1 { get; set; }
        public string Team_HighestConsecutiveWinsAsAway1 { get; set; }
        public decimal? Highest_HighestConsecutiveWinsAsAway1 { get; set; }
        public int? MatchesPlayed_HighestConsecutiveWinsAsAway1 { get; set; }

        public int? TeamId_MatchesSinceLastWin1 { get; set; }
        public string Team_MatchesSinceLastWin1 { get; set; }
        public decimal? Highest_MatchesSinceLastWin1 { get; set; }
        public int? MatchesPlayed_MatchesSinceLastWin1 { get; set; }

        public int? TeamId_Unbeaten1 { get; set; }
        public string Team_Unbeaten1 { get; set; }
        public decimal? Highest_Unbeaten1 { get; set; }
        public int? MatchesPlayed_Unbeaten1 { get; set; }
        public List<LongestActiveStreaks> LongestActiveStreaks { get; set; }
    }

    public class LongestActiveStreaks
    {
        public int TeamId { get; set; }
        public string Team { get; set; }
        public string MarketName { get; set; }
        public decimal Highest { get; set; }
        public int MatchesPlayed { get; set; }
    }
}