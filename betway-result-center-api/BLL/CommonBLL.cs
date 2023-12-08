using betway_result_center_api.Models.DatabaseModels;
using betway_result_center_api.Repository;
using System.Collections.Generic;

namespace betway_result_center_api.BLL
{
    public class CommonBLL
    {
        public static List<LiveSportDBModel> GetLiveSports()
        {
            List<LiveSportDBModel> liveSportDBModel = DBManager.Execute<LiveSportDBModel>("Betway_Live_Sports", new { });
            return liveSportDBModel;
        }
    }
}