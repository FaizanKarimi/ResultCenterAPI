﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace betway_result_center_api.Models.DatabaseModels.IceHockey
{
    public class IceHockeyMatchesByRoundModel
    {
        public decimal MatchId { get; set; }
        public DateTime MatchDate { get; set; }
        public int ContestGroupRoundId { get; set; }
        public int MatchStatusId { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }
        public decimal? MatchscoreId { get; set; }
        public Int16? ScoreInfoTypeId { get; set; }
        public string HomeScore { get; set; }
        public string AwayScore { get; set; }
        public bool? HomeTeamWin { get; set; }
        public bool? AwayTeamWin { get; set; }
    }
}