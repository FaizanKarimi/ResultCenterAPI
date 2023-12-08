using betway_result_center_api.Models.DatabaseModels.IceHockey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace betway_result_center_api.Models.Models.IceHockey
{
    public class IceHockeyHead2Head
    {
        public List<IceHockeyMatchForH2H> MeetingsMatchesList { get; set; }
        public List<IceHockeyMatchForH2H> AllMatchesList { get; set; }
        //public List<IceHockeyMatchForH2H> FormMatchesListHomeTeam { get; set; }
        //public List<IceHockeyMatchForH2H> FormMatchesListAwayTeam { get; set; }
        //public List<IceHockeyMatchForH2H> SeasonStatsMatchesList { get; set; }

    }
}