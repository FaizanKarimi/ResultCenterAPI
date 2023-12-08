using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace betway_result_center_api.Models.DatabaseModels.Football
{
    public class ContestGroupStatsDBModel
    {
        public Int16 CountryId { get; set; }
        public string CountryName { get; set; }
        public int ContestGroupId { get; set; }
        public string ContestGroupName { get; set; }
        public Int16 SeasonId { get; set; }
        public string SeasonName { get; set; }
        public decimal? HomeResult { get; set; }
        public decimal? DrawResult { get; set; }
        public decimal? AwayResult { get; set; }
        public decimal? TotalAvg { get; set; }
        public decimal? HomeAvg { get; set; }
        public decimal? AwayAvg { get; set; }
        public decimal? Over15Value { get; set; }
        public decimal? Over25Value { get; set; }
        public decimal? Over35Value { get; set; }
        public int? TeamIdHGPM { get; set; }
        public string TeamHGPM { get; set; }
        public decimal? HighestHGPM { get; set; }
        public int? TeamIdLGPM { get; set; }
        public string TeamLGPM { get; set; }
        public decimal? HighestLGPM { get; set; }
        public decimal? Over05Value1H { get; set; }
        public decimal? Over15Value1H { get; set; }
        public decimal? Over25Value1H { get; set; }
        public int? TeamIdHGPM1H { get; set; }
        public string TeamHGPM1H { get; set; }
        public decimal? HighestHGPM1H { get; set; }
        public int? TeamIdLGPM1H { get; set; }
        public string TeamLGPM1H { get; set; }
        public decimal? HighestLGPM1H { get; set; }
        public decimal? BTTSPercentage { get; set; }
        public int? TeamIdBTTSM1 { get; set; }
        public string TeamBTTSM1 { get; set; }
        public decimal? HighestBTTSM1 { get; set; }
        public int? MatchesPlayedBTTSM1 { get; set; }
        public int? TeamIdBTTSM2 { get; set; }
        public string TeamBTTSM2 { get; set; }
        public decimal? HighestBTTSM2 { get; set; }
        public int? MatchesPlayedBTTSM2 { get; set; }
        public int? TeamIdBTTSM3 { get; set; }
        public string TeamBTTSM3 { get; set; }
        public decimal? HighestBTTSM3 { get; set; }
        public int? MatchesPlayedBTTSM3 { get; set; }
        public int? TeamIdBTTSF1 { get; set; }
        public string TeamBTTSF1 { get; set; }
        public decimal? HighestBTTSF1 { get; set; }
        public int? MatchesPlayedBTTSF1 { get; set; }
        public int? TeamIdCSM1 { get; set; }
        public string TeamCSM1 { get; set; }
        public decimal? HighestCSM1 { get; set; }
        public int? MatchesPlayedCSM1 { get; set; }
        public int? TeamIdCSM2 { get; set; }
        public string TeamCSM2 { get; set; }
        public decimal? HighestCSM2 { get; set; }
        public int? MatchesPlayedCSM2 { get; set; }
        public int? TeamIdCSF1 { get; set; }
        public string TeamCSF1 { get; set; }
        public decimal? HighestCSF1 { get; set; }
        public int? MatchesPlayedCSF1 { get; set; }
        public int? TeamIdFTSM1 { get; set; }
        public string TeamFTSM1 { get; set; }
        public decimal? HighestFTSM1 { get; set; }
        public int? MatchesPlayedFTSM1 { get; set; }
        public int? TeamIdFTSF1 { get; set; }
        public string TeamFTSF1 { get; set; }
        public decimal? HighestFTSF1 { get; set; }
        public int? MatchesPlayedFTSF1 { get; set; }
        public int? TeamIdSTFGM1 { get; set; }
        public string TeamSTFGM1 { get; set; }
        public decimal? HighestSTFGM1 { get; set; }
        public int? MatchesPlayedSTFGM1 { get; set; }
        public int? TeamIdSTFGF1 { get; set; }
        public string TeamSTFGF1 { get; set; }
        public decimal? HighestSTFGF1 { get; set; }
        public int? MatchesPlayedSTFGF1 { get; set; }
        public int? TeamIdCLM1 { get; set; }
        public string TeamCLM1 { get; set; }
        public decimal? HighestCLM1 { get; set; }
        public int? MatchesPlayedCLM1 { get; set; }
        public int? TeamIdCWM1 { get; set; }
        public string TeamCWM1 { get; set; }
        public decimal? HighestCWM1 { get; set; }
        public int? MatchesPlayedCWM1 { get; set; }
        public int? TeamIdCWAHM1 { get; set; }
        public string TeamCWAHM1 { get; set; }
        public decimal? HighestCWAHM1 { get; set; }
        public int? MatchesPlayedCWAHM1 { get; set; }
        public int? TeamIdCWAAM1 { get; set; }
        public string TeamCWAAM1 { get; set; }
        public decimal? HighestCWAAM1 { get; set; }
        public int? MatchesPlayedCWAAM1 { get; set; }
        public int? TeamIdMSLWM1 { get; set; }
        public string TeamMSLWM1 { get; set; }
        public decimal? HighestMSLWM1 { get; set; }
        public int? MatchesPlayedMSLWM1 { get; set; }
        public int? TeamIdUBM1 { get; set; }
        public string TeamUBM1 { get; set; }
        public decimal? HighestUBM1 { get; set; }
        public int? MatchesPlayedUBM1 { get; set; }
    }
}