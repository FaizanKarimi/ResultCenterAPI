using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace betway_result_center_api.Models.Models.Football
{
    public class LeagueTableModel
    {
        public decimal MatchId { get; set; }
        public int LeagueTableId { get; set; }
        public int ContestGroupId { get; set; }
        public Int16 SeasonId { get; set; }
        //public int LeagueTeamId { get; set; }
        //public String Team { get; set; }

        public List<LeagueTableCompetitorModel> LeagueCompetitors { get; set; }
    }

    public class LeagueTableCompetitorModel
    {
        public decimal LeagueTableCompetitorId { get; set; }
        public int TeamId { get; set; }
        public string Team { get; set; }
        public Int32 Place { get; set; }
        public List<LeagueTableMatchesModel> LeagueTablesMatches { get; set; }
    }

    public class LeagueTableMatchesModel
    {
        public decimal LeagueTableMatchId { get; set; }
        public decimal LeagueTableCompetitorId { get; set; }
        public Int16 Played { get; set; }
        public Int16 Won { get; set; }
        public Int16 Draws { get; set; }
        public Int16 Lost { get; set; }
        public Int16 Scored { get; set; }
        public Int16 Conceded { get; set; }
        public Int16 Difference { get; set; }
        public Int16 Points { get; set; }
        public string Type { get; set; }

    }
}