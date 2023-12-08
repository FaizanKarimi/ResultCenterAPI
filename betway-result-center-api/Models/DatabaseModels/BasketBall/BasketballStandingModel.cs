using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace betway_result_center_api.Models.DatabaseModels.BasketBall
{
    public class BasketballStandingModel
    {
        public string TeamName { get; set; }
        public Int16? Played { get; set; }
        public Int16? Won { get; set; }
        public Int16? Lost { get; set; }
        public Int16? Scored { get; set; }
        public Int16? Conceded { get; set; }
        public Int16? Place { get; set; }
    }
}