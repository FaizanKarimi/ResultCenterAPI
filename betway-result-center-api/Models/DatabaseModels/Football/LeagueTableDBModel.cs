using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace betway_result_center_api.Models.DatabaseModels.Football
{
    public class LeagueTableDBModel
    {
        public int LeagueTableId { get; set; }
        public int ContestGroupId { get; set; }
        public string ContestGroupName { get; set; }
        public Int16 SeasonId { get; set; }
        public decimal LeagueTableCompetitorId { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public Int16 Place { get; set; }
        public decimal LeagueTableMatchId { get; set; }
        public Int16 Played { get; set; }
        public Int16 Won { get; set; }
        public Int16 Lost { get; set; }
        public Int16 Draws { get; set; }
        public Int16 Scored { get; set; }
        public Int16 Conceded { get; set; }
        public Int16 Difference { get; set; }
        public Int16 Points { get; set; }
        public string Type { get; set; }
    }
}