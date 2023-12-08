using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace betway_result_center_api.Models.DatabaseModels.IceHockey
{
    public class IceHockeyStandingsModel
    {

       
        public Int16? Ranking { get; set; }
        public string TeamName { get; set; }
        public Int16? Played { get; set; }
        public Int16? Won { get; set; }
        public Int16? Lost { get; set; }
        public Int16? LostOT { get; set; }
        public Int16? Difference { get; set; }
        public Int16? Points { get; set; }



    }
}