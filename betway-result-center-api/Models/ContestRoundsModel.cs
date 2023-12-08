using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace betway_result_center_api.Models
{
    public class ContestRoundsModel
    {
        public int ContestGroupId { get; set; }
        public string ContestGroupName { get; set; }
        public int ContestGroupRoundId { get; set; }
        public string Round { get; set; }
        public int IsSelected { get; set; }
        public int IsRound { get; set; }
    }
}