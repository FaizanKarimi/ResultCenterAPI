using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace betway_result_center_api.Models.Models.Tennis
{
    public class TennisLeaguesListModel
    {

        public int ContestGroupId { get; set; }
        public string ContestGroupName { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int SportOrgId { get; set; }
        public string OrgName { get; set; }
        public string ContestType { get; set; }
        public int IsOrder { get; set; }
        public string LeagueName { get; set; }
    }
}