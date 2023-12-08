using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace betway_result_center_api.Models
{
    public class ContestGroupsListModel
    {
      
        public List<ContestGroupList> ContestGroups { get; set; }
    }
    public class ContestGroupList
    {
        public int ContestGroupId { get; set; }
        public string ContestGroupName { get; set; }

    }
}