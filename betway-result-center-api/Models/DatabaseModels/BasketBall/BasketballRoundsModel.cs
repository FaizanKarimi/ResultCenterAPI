using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace betway_result_center_api.Models.DatabaseModels.BasketBall
{
    public class BasketballRoundsModel
    {
        public string Round { get; set; }
        public int ContestGroupRoundId { get; set; }

        public bool IsSelected { get; set; }
        public bool IsRound { get; set; }
    }
}