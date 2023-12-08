using betway_result_center_api.Models;
using betway_result_center_api.Models.DatabaseModels.Tennis;
using betway_result_center_api.Models.Models.Tennis;
using betway_result_center_api.Repository;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace betway_result_center_api.BLL
{
    public class TennisBLL
    {
        private static string _connectionString = SqlConnectionConfig.getConnectionString();

        #region Public Methods
        public static TennisMatchListModel GetMatchListByDate(GlobalParametersModel globalParametersModel)
        {
            int pageNumber = globalParametersModel.PageIndex;
            int pageSize = globalParametersModel.PageSize;
            TennisMatchListModel tennisMatchListModel = new TennisMatchListModel();
            List<TennisMatchListDBModel> tennisMatchListDBModel = DBManager.Execute<TennisMatchListDBModel>("BetwayWidget_GetMatchList",
                new
                {
                    SportId = globalParametersModel.SportId,
                    FromDate = globalParametersModel.FromDate,
                    ToDate = globalParametersModel.FromDate.AddDays(1),
                    LanguageCode = globalParametersModel.LanguageCode
                });

            #region  ADO.Net code
            //List<TennisMatchListDBModel> tennisMatchListDBModel = new List<TennisMatchListDBModel>();
            //using (DataTable _dataTable = new DataTable())
            //{
            //    using (SqlConnection _connection = new SqlConnection(_connectionString))
            //    {
            //        _connection.Open();
            //        using (SqlCommand _command = new SqlCommand("BetwayWidget_GetMatchList", _connection))
            //        {
            //            _command.CommandType = CommandType.StoredProcedure;
            //            _command.Parameters.Add(new SqlParameter("@SportId", sid));
            //            _command.Parameters.Add(new SqlParameter("@FromDate", fromDate));
            //            _command.Parameters.Add(new SqlParameter("@ToDate", fromDate.AddDays(1)));

            //            using (SqlDataAdapter _sqlDataAdapter = new SqlDataAdapter(_command))
            //            {
            //                _sqlDataAdapter.Fill(_dataTable);
            //            }
            //        }
            //    }

            //    foreach (DataRow _dataRow in _dataTable.Rows)
            //    {
            //        TennisMatchListDBModel _tennisMatchListDBModel = new TennisMatchListDBModel()
            //        {
            //            SportId = Convert.ToInt16(_dataRow["SportId"]),
            //            SportName = Convert.ToString(_dataRow["SportName"]),
            //            ContestGroupId = Convert.ToInt32(_dataRow["ContestGroupId"]),
            //            ContestGroupName = Convert.ToString(_dataRow["ContestGroupName"]),
            //            ContestType = Convert.ToString(_dataRow["ContestType"]),
            //            CountryId = Convert.ToInt16(_dataRow["CountryId"]),
            //            CountryName = Convert.ToString(_dataRow["CountryName"]),
            //            OrgName = Convert.ToString(_dataRow["OrgName"]),
            //            SportOrgId = Convert.ToInt32(_dataRow["SportOrgId"]),
            //            MatchId = Convert.ToInt32(_dataRow["MatchId"]),
            //            MatchDate = Convert.ToDateTime(_dataRow["MatchDate"]),
            //            MatchStatusId = Convert.ToInt16(_dataRow["MatchStatusId"]),
            //            MatchStatus = Convert.ToString(_dataRow["MatchStatus"]),
            //            HomeTeamId = Convert.ToInt32(_dataRow["HomeTeamId"]),
            //            HomeTeamName = Convert.ToString(_dataRow["HomeTeamName"]),
            //            AwayTeamId = Convert.ToInt32(_dataRow["AwayTeamId"]),
            //            AwayTeamName = Convert.ToString(_dataRow["AwayTeamName"]),
            //            MatchscoreId = _dataRow["MatchscoreId"] != DBNull.Value ? Convert.ToInt32(_dataRow["MatchScoreId"]) : (Int32?)null,
            //            ScoreInfoTypeId = _dataRow["ScoreInfoTypeId"] != DBNull.Value ? Convert.ToInt16(_dataRow["ScoreInfoTypeId"]) : (Int16?)null,
            //            HomeScore = Convert.ToString(_dataRow["HomeScore"]),
            //            AwayScore = Convert.ToString(_dataRow["AwayScore"]),
            //            TieBreak = _dataRow["TieBreak"] != DBNull.Value ? Convert.ToBoolean(_dataRow["TieBreak"]) : (bool?)null,
            //            HomeTieBreakScore = Convert.ToString(_dataRow["HomeTieBreakScore"]),
            //            AwayTieBreakScore = Convert.ToString(_dataRow["AwayTieBreakScore"]),
            //            HomeTeamWin = _dataRow["HomeTeamWin"] != DBNull.Value ? Convert.ToBoolean(_dataRow["HomeTeamWin"]) : false,
            //            AwayTeamWin = _dataRow["AwayTeamWin"] != DBNull.Value ? Convert.ToBoolean(_dataRow["AwayTeamWin"]) : false
            //        };
            //        tennisMatchListDBModel.Add(_tennisMatchListDBModel);
            //    }
            //} 
            #endregion

            if (tennisMatchListDBModel != null && tennisMatchListDBModel.Count > 0)
            {
                tennisMatchListModel = (from _sport in tennisMatchListDBModel.ToList()
                                        group _sport by new
                                        {
                                            _sport.SportId,
                                            _sport.SportName,
                                        } into sport
                                        select new TennisMatchListModel
                                        {
                                            SportId = sport.Key.SportId,
                                            SportName = sport.Key.SportName,
                                            ContestGroups = (from _contestGroup in tennisMatchListDBModel.ToList()
                                                             where _contestGroup.SportId == sport.Key.SportId
                                                             group _contestGroup by new
                                                             {
                                                                 _contestGroup.ContestGroupId,
                                                                 _contestGroup.ContestGroupName,
                                                                 _contestGroup.CountryId,
                                                                 _contestGroup.CountryName,
                                                                 _contestGroup.OrgName,
                                                                 _contestGroup.SportOrgId,
                                                                 _contestGroup.ContestType,
                                                                 _contestGroup.LeagueName
                                                             } into contestGroup
                                                             select new TennisContestGroup
                                                             {
                                                                 ContestGroupId = contestGroup.Key.ContestGroupId,
                                                                 ContestGroupName = contestGroup.Key.ContestGroupName,
                                                                 ContestType = contestGroup.Key.ContestType,
                                                                 CountryId = contestGroup.Key.CountryId,
                                                                 CountryName = contestGroup.Key.CountryName,
                                                                 LeagueName = contestGroup.Key.LeagueName,
                                                                 OrgName = contestGroup.Key.OrgName,
                                                                 SportOrgId = contestGroup.Key.SportOrgId,
                                                                 Matches = (from _match in tennisMatchListDBModel.ToList()
                                                                            where _match.ContestGroupId == contestGroup.Key.ContestGroupId
                                                                            group _match by new
                                                                            {
                                                                                _match.MatchId,
                                                                                _match.MatchStatus,
                                                                                _match.MatchDate,
                                                                                _match.HomeTeamId,
                                                                                _match.HomeTeamName,
                                                                                _match.AwayTeamId,
                                                                                _match.AwayTeamName,
                                                                                _match.MatchStatusId,
                                                                                _match.MatchInfo
                                                                            } into match
                                                                            select new Match
                                                                            {
                                                                                MatchId = match.Key.MatchId,
                                                                                MatchStatus = match.Key.MatchStatus,
                                                                                MatchDate = Convert.ToDateTime(match.Key.MatchDate),
                                                                                HomeTeamId = match.Key.HomeTeamId,
                                                                                HomeTeamName = match.Key.HomeTeamName,
                                                                                AwayTeamId = match.Key.AwayTeamId,
                                                                                AwayTeamName = match.Key.AwayTeamName,
                                                                                MatchStatusId = match.Key.MatchStatusId,
                                                                                HomeTeamWin = match.FirstOrDefault(x => x.ScoreInfoTypeId == 17) != null ? match.FirstOrDefault(x => x.ScoreInfoTypeId == 17).HomeTeamWin : false,
                                                                                AwayTeamWin = match.FirstOrDefault(x => x.ScoreInfoTypeId == 17) != null ? match.FirstOrDefault(x => x.ScoreInfoTypeId == 17).AwayTeamWin : false,
                                                                                IsMatchScoreAvailable = match.Key.MatchInfo,
                                                                                MatchScores = (from _matchScore in tennisMatchListDBModel.ToList()
                                                                                               where _matchScore.MatchId == match.Key.MatchId
                                                                                               group _matchScore by new
                                                                                               {
                                                                                                   _matchScore.MatchscoreId,
                                                                                                   _matchScore.ScoreInfoTypeId,
                                                                                                   _matchScore.HomeScore,
                                                                                                   _matchScore.AwayScore,
                                                                                                   _matchScore.TieBreak,
                                                                                                   _matchScore.HomeTieBreakScore,
                                                                                                   _matchScore.AwayTieBreakScore
                                                                                               } into matchScore
                                                                                               select new MatchScore
                                                                                               {
                                                                                                   MatchScoreId = matchScore.Key.MatchscoreId,
                                                                                                   ScoreInfoTypeId = matchScore.Key.ScoreInfoTypeId,
                                                                                                   HomeScore = matchScore.Key.HomeScore,
                                                                                                   AwayScore = matchScore.Key.AwayScore,
                                                                                                   TieBreak = matchScore.Key.TieBreak,
                                                                                                   HomeTieBreak = matchScore.Key.HomeTieBreakScore,
                                                                                                   AwayTieBreak = matchScore.Key.AwayTieBreakScore
                                                                                               }).ToList()
                                                                            }).ToList()
                                                             }).ToList()
                                        }).FirstOrDefault();

                tennisMatchListModel.ContestGroups = tennisMatchListModel.ContestGroups.Skip(pageNumber * pageSize).Take(pageSize).ToList();
            }
            return tennisMatchListModel;
        }

        public static TennisMatchInfoModel GetMatchInfo(GlobalParametersModel globalParametersModel)
        {
            TennisMatchInfoModel tennisMatchInfoModel = new TennisMatchInfoModel();
            List<TennisMatchInfoDBModel> tennisMatchInfoDBModel = DBManager.Execute<TennisMatchInfoDBModel>("BWTennisGetMatchInfo",
                new
                {
                    SportId = globalParametersModel.SportId,
                    MatchId = globalParametersModel.MatchId,
                    LanguageCode = globalParametersModel.LanguageCode
                });

            #region ADO.NET CODE
            //List<TennisMatchInfoDBModel> tennisMatchInfoDBModel = new List<TennisMatchInfoDBModel>();
            //using (DataTable _dataTable = new DataTable())
            //{
            //    using (SqlConnection _connection = new SqlConnection(_connectionString))
            //    {
            //        _connection.Open();
            //        using (SqlCommand _command = new SqlCommand("BWTennisGetMatchInfo", _connection))
            //        {
            //            _command.CommandType = CommandType.StoredProcedure;
            //            _command.Parameters.Add(new SqlParameter("@SportId", sid));
            //            _command.Parameters.Add(new SqlParameter("@MatchId", MatchId));

            //            using (SqlDataAdapter _sqlDataAdapter = new SqlDataAdapter(_command))
            //            {
            //                _sqlDataAdapter.Fill(_dataTable);
            //            }
            //        }

            //        foreach (DataRow _dataRow in _dataTable.Rows)
            //        {
            //            TennisMatchInfoDBModel _query = new TennisMatchInfoDBModel()
            //            {
            //                ContestGroupId = Convert.ToInt32(_dataRow["ContestGroupId"]),
            //                ContestGroupName = Convert.ToString(_dataRow["ContestGroupName"]),
            //                ContestType = Convert.ToString(_dataRow["ContestType"]),
            //                CountryId = Convert.ToInt16(_dataRow["CountryId"]),
            //                CountryName = Convert.ToString(_dataRow["CountryName"]),
            //                OrgName = Convert.ToString(_dataRow["OrgName"]),
            //                SportOrgId = Convert.ToInt32(_dataRow["SportOrgId"]),
            //                MatchId = Convert.ToInt32(_dataRow["MatchId"]),
            //                MatchDate = Convert.ToString(_dataRow["MatchDate"]),
            //                MatchStatusId = Convert.ToInt16(_dataRow["MatchStatusId"]),
            //                MatchStatus = Convert.ToString(_dataRow["MatchStatus"]),
            //                HomeTeamId = Convert.ToInt32(_dataRow["HomeTeamId"]),
            //                HomeTeamName = Convert.ToString(_dataRow["HomeTeamName"]),
            //                AwayTeamId = Convert.ToInt32(_dataRow["AwayTeamId"]),
            //                AwayTeamName = Convert.ToString(_dataRow["AwayTeamName"]),
            //                MatchscoreId = _dataRow["MatchscoreId"] != DBNull.Value ? Convert.ToInt32(_dataRow["MatchScoreId"]) : (Int32?)null,
            //                ScoreInfoTypeId = _dataRow["ScoreInfoTypeId"] != DBNull.Value ? Convert.ToInt16(_dataRow["ScoreInfoTypeId"]) : (Int16?)null,
            //                HomeScore = Convert.ToString(_dataRow["HomeScore"]),
            //                AwayScore = Convert.ToString(_dataRow["AwayScore"]),
            //                TieBreak = _dataRow["TieBreak"] != DBNull.Value ? Convert.ToBoolean(_dataRow["TieBreak"]) : (bool?)null,
            //                HomeTieBreakScore = Convert.ToString(_dataRow["HomeTieBreakScore"]),
            //                AwayTieBreakScore = Convert.ToString(_dataRow["AwayTieBreakScore"]),
            //                HomeTeamWin = _dataRow["HomeTeamWin"] != DBNull.Value ? Convert.ToBoolean(_dataRow["HomeTeamWin"]) : false,
            //                AwayTeamWin = _dataRow["AwayTeamWin"] != DBNull.Value ? Convert.ToBoolean(_dataRow["AwayTeamWin"]) : false,
            //                MatchInfoMatchId = _dataRow["MatchInfoMatchId"] != DBNull.Value ? Convert.ToInt32(_dataRow["MatchInfoMatchId"]) : (int?)null,
            //                FirstServePointsWonDividerTeam1 = _dataRow["FirstServePointsWonDividerTeam1"] != DBNull.Value ? Convert.ToInt32(_dataRow["FirstServePointsWonDividerTeam1"]) : (Int32?)null,
            //                FirstServePointsWonDividendTeam1 = _dataRow["FirstServePointsWonDividendTeam1"] != DBNull.Value ? Convert.ToInt32(_dataRow["FirstServePointsWonDividendTeam1"]) : (Int32?)null,
            //                FirstServePointsWonPercentTeam1 = _dataRow["FirstServePointsWonPercentTeam1"] != DBNull.Value ? Convert.ToInt32(_dataRow["FirstServePointsWonPercentTeam1"]) : (Int32?)null,
            //                FirstServePointsWonDividerTeam2 = _dataRow["FirstServePointsWonDividerTeam2"] != DBNull.Value ? Convert.ToInt32(_dataRow["FirstServePointsWonDividerTeam2"]) : (Int32?)null,
            //                FirstServePointsWonDividendTeam2 = _dataRow["FirstServePointsWonDividendTeam2"] != DBNull.Value ? Convert.ToInt32(_dataRow["FirstServePointsWonDividendTeam2"]) : (Int32?)null,
            //                FirstServePointsWonPercentTeam2 = _dataRow["FirstServePointsWonPercentTeam2"] != DBNull.Value ? Convert.ToInt32(_dataRow["FirstServePointsWonPercentTeam2"]) : (Int32?)null,
            //                BreakPointsConvertedPercentTeam1 = _dataRow["BreakPointsConvertedPercentTeam1"] != DBNull.Value ? Convert.ToInt32(_dataRow["BreakPointsConvertedPercentTeam1"]) : (Int32?)null,
            //                BreakPointsConvertedDividendTeam1 = _dataRow["BreakPointsConvertedDividendTeam1"] != DBNull.Value ? Convert.ToInt32(_dataRow["BreakPointsConvertedDividendTeam1"]) : (Int32?)null,
            //                BreakPointsConvertedDivisorTeam1 = _dataRow["BreakPointsConvertedDivisorTeam1"] != DBNull.Value ? Convert.ToInt32(_dataRow["BreakPointsConvertedDivisorTeam1"]) : (Int32?)null,
            //                BreakPointsConvertedPercentTeam2 = _dataRow["BreakPointsConvertedPercentTeam2"] != DBNull.Value ? Convert.ToInt32(_dataRow["BreakPointsConvertedPercentTeam2"]) : (Int32?)null,
            //                BreakPointsConvertedDividendTeam2 = _dataRow["BreakPointsConvertedDividendTeam2"] != DBNull.Value ? Convert.ToInt32(_dataRow["BreakPointsConvertedDividendTeam2"]) : (Int32?)null,
            //                BreakPointsConvertedDivisorTeam2 = _dataRow["BreakPointsConvertedDivisorTeam2"] != DBNull.Value ? Convert.ToInt32(_dataRow["BreakPointsConvertedDivisorTeam2"]) : (Int32?)null,
            //                ServicePointsWonPercentTeam1 = _dataRow["ServicePointsWonPercentTeam1"] != DBNull.Value ? Convert.ToInt32(_dataRow["ServicePointsWonPercentTeam1"]) : (Int32?)null,
            //                ServicePointsWonDividendTeam1 = _dataRow["ServicePointsWonDividendTeam1"] != DBNull.Value ? Convert.ToInt32(_dataRow["ServicePointsWonDividendTeam1"]) : (Int32?)null,
            //                ServicePointsWonDivisorTeam1 = _dataRow["ServicePointsWonDivisorTeam1"] != DBNull.Value ? Convert.ToInt32(_dataRow["ServicePointsWonDivisorTeam1"]) : (Int32?)null,
            //                ServicePointsWonPercentTeam2 = _dataRow["ServicePointsWonPercentTeam2"] != DBNull.Value ? Convert.ToInt32(_dataRow["ServicePointsWonPercentTeam2"]) : (Int32?)null,
            //                ServicePointsWonDividendTeam2 = _dataRow["ServicePointsWonDividendTeam2"] != DBNull.Value ? Convert.ToInt32(_dataRow["ServicePointsWonDividendTeam2"]) : (Int32?)null,
            //                ServicePointsWonDivisorTeam2 = _dataRow["ServicePointsWonDivisorTeam2"] != DBNull.Value ? Convert.ToInt32(_dataRow["ServicePointsWonDivisorTeam2"]) : (Int32?)null,
            //                TotalGamesWonPercentTeam1 = _dataRow["TotalGamesWonPercentTeam1"] != DBNull.Value ? Convert.ToInt32(_dataRow["TotalGamesWonPercentTeam1"]) : (Int32?)null,
            //                TotalGamesWonDividendTeam1 = _dataRow["TotalGamesWonDividendTeam1"] != DBNull.Value ? Convert.ToInt32(_dataRow["TotalGamesWonDividendTeam1"]) : (Int32?)null,
            //                TotalGamesWonDivisorTeam1 = _dataRow["TotalGamesWonDivisorTeam1"] != DBNull.Value ? Convert.ToInt32(_dataRow["TotalGamesWonDivisorTeam1"]) : (Int32?)null,
            //                TotalGamesWonPercentTeam2 = _dataRow["TotalGamesWonPercentTeam2"] != DBNull.Value ? Convert.ToInt32(_dataRow["TotalGamesWonPercentTeam2"]) : (Int32?)null,
            //                TotalGamesWonDividendTeam2 = _dataRow["TotalGamesWonDividendTeam2"] != DBNull.Value ? Convert.ToInt32(_dataRow["TotalGamesWonDividendTeam2"]) : (Int32?)null,
            //                TotalGamesWonDivisorTeam2 = _dataRow["TotalGamesWonDivisorTeam2"] != DBNull.Value ? Convert.ToInt32(_dataRow["TotalGamesWonDivisorTeam2"]) : (Int32?)null,
            //                TotalPointsWonPercentTeam1 = _dataRow["TotalPointsWonPercentTeam1"] != DBNull.Value ? Convert.ToInt32(_dataRow["TotalPointsWonPercentTeam1"]) : (Int32?)null,
            //                TotalPointsWonDividendTeam1 = _dataRow["TotalPointsWonDividendTeam1"] != DBNull.Value ? Convert.ToInt32(_dataRow["TotalPointsWonDividendTeam1"]) : (Int32?)null,
            //                TotalPointsWonDivisorTeam1 = _dataRow["TotalPointsWonDivisorTeam1"] != DBNull.Value ? Convert.ToInt32(_dataRow["TotalPointsWonDivisorTeam1"]) : (Int32?)null,
            //                TotalPointsWonPercentTeam2 = _dataRow["TotalPointsWonPercentTeam2"] != DBNull.Value ? Convert.ToInt32(_dataRow["TotalPointsWonPercentTeam2"]) : (Int32?)null,
            //                TotalPointsWonDividendTeam2 = _dataRow["TotalPointsWonDividendTeam2"] != DBNull.Value ? Convert.ToInt32(_dataRow["TotalPointsWonDividendTeam2"]) : (Int32?)null,
            //                TotalPointsWonDivisorTeam2 = _dataRow["TotalPointsWonDivisorTeam2"] != DBNull.Value ? Convert.ToInt32(_dataRow["TotalPointsWonDivisorTeam2"]) : (Int32?)null,
            //            };
            //            _tennisMatchInfoDBModel.Add(_query);
            //        }
            //    }
            //} 
            #endregion

            if (tennisMatchInfoDBModel != null && tennisMatchInfoDBModel.Count > 0)
            {
                tennisMatchInfoModel = (from _match in tennisMatchInfoDBModel.ToList()
                                        group _match by new
                                        {
                                            _match.MatchId
                                        } into match
                                        select new TennisMatchInfoModel
                                        {
                                            MatchId = match.Key.MatchId,
                                            ContestGroupId = match.FirstOrDefault().ContestGroupId,
                                            ContestGroupName = match.FirstOrDefault().ContestGroupName,
                                            ContestType = match.FirstOrDefault().ContestType,
                                            CountryId = match.FirstOrDefault().CountryId,
                                            CountryName = match.FirstOrDefault().CountryName,
                                            OrgName = match.FirstOrDefault().OrgName,
                                            LeagueName = match.FirstOrDefault().LeagueName,
                                            SportOrgId = match.FirstOrDefault().SportOrgId,
                                            MatchStatus = match.FirstOrDefault().MatchStatus,
                                            MatchDate = Convert.ToDateTime(match.FirstOrDefault().MatchDate),
                                            HomeTeamId = match.FirstOrDefault().HomeTeamId,
                                            HomeTeamName = match.FirstOrDefault().HomeTeamName,
                                            AwayTeamId = match.FirstOrDefault().AwayTeamId,
                                            AwayTeamName = match.FirstOrDefault().AwayTeamName,
                                            MatchStatusId = match.FirstOrDefault().MatchStatusId,
                                            HomeTeamWin = match.FirstOrDefault(x => x.ScoreInfoTypeId == 17) != null ? match.FirstOrDefault(x => x.ScoreInfoTypeId == 17).HomeTeamWin : false,
                                            AwayTeamWin = match.FirstOrDefault(x => x.ScoreInfoTypeId == 17) != null ? match.FirstOrDefault(x => x.ScoreInfoTypeId == 17).AwayTeamWin : false,
                                            MatchStats = new TennisMatchStats
                                            {
                                                MatchInfoMatchId = match.FirstOrDefault().MatchInfoMatchId,

                                                FirstServePointsWonDividerTeam1 = match.FirstOrDefault().FirstServePointsWonDividerTeam1,
                                                FirstServePointsWonDividendTeam1 = match.FirstOrDefault().FirstServePointsWonDividendTeam1,
                                                FirstServePointsWonPercentTeam1 = match.FirstOrDefault().FirstServePointsWonPercentTeam1,

                                                FirstServePointsWonDividerTeam2 = match.FirstOrDefault().FirstServePointsWonDividerTeam2,
                                                FirstServePointsWonDividendTeam2 = match.FirstOrDefault().FirstServePointsWonDividendTeam2,
                                                FirstServePointsWonPercentTeam2 = match.FirstOrDefault().FirstServePointsWonPercentTeam2,

                                                BreakPointsConvertedPercentTeam1 = match.FirstOrDefault().BreakPointsConvertedPercentTeam1,
                                                BreakPointsConvertedDividendTeam1 = match.FirstOrDefault().BreakPointsConvertedDividendTeam1,
                                                BreakPointsConvertedDivisorTeam1 = match.FirstOrDefault().BreakPointsConvertedDivisorTeam1,

                                                BreakPointsConvertedPercentTeam2 = match.FirstOrDefault().BreakPointsConvertedPercentTeam2,
                                                BreakPointsConvertedDividendTeam2 = match.FirstOrDefault().BreakPointsConvertedDividendTeam2,
                                                BreakPointsConvertedDivisorTeam2 = match.FirstOrDefault().BreakPointsConvertedDivisorTeam2,

                                                ServicePointsWonPercentTeam1 = match.FirstOrDefault().ServicePointsWonPercentTeam1,
                                                ServicePointsWonDividendTeam1 = match.FirstOrDefault().ServicePointsWonDividendTeam1,
                                                ServicePointsWonDivisorTeam1 = match.FirstOrDefault().ServicePointsWonDivisorTeam1,

                                                ServicePointsWonPercentTeam2 = match.FirstOrDefault().ServicePointsWonPercentTeam2,
                                                ServicePointsWonDividendTeam2 = match.FirstOrDefault().ServicePointsWonDividendTeam2,
                                                ServicePointsWonDivisorTeam2 = match.FirstOrDefault().ServicePointsWonDivisorTeam2,

                                                TotalGamesWonPercentTeam1 = match.FirstOrDefault().TotalGamesWonPercentTeam1,
                                                TotalGamesWonDividendTeam1 = match.FirstOrDefault().TotalGamesWonDividendTeam1,
                                                TotalGamesWonDivisorTeam1 = match.FirstOrDefault().TotalGamesWonDivisorTeam1,

                                                TotalGamesWonPercentTeam2 = match.FirstOrDefault().TotalGamesWonPercentTeam2,
                                                TotalGamesWonDividendTeam2 = match.FirstOrDefault().TotalGamesWonDividendTeam2,
                                                TotalGamesWonDivisorTeam2 = match.FirstOrDefault().TotalGamesWonDivisorTeam2,

                                                TotalPointsWonPercentTeam1 = match.FirstOrDefault().TotalPointsWonPercentTeam1,
                                                TotalPointsWonDividendTeam1 = match.FirstOrDefault().TotalPointsWonDividendTeam1,
                                                TotalPointsWonDivisorTeam1 = match.FirstOrDefault().TotalPointsWonDivisorTeam1,

                                                TotalPointsWonPercentTeam2 = match.FirstOrDefault().TotalPointsWonPercentTeam2,
                                                TotalPointsWonDividendTeam2 = match.FirstOrDefault().TotalPointsWonDividendTeam2,
                                                TotalPointsWonDivisorTeam2 = match.FirstOrDefault().TotalPointsWonDivisorTeam2,
                                            },
                                            MatchScores = (from _matchScore in tennisMatchInfoDBModel.ToList()
                                                           where _matchScore.MatchId == match.Key.MatchId
                                                           group _matchScore by new
                                                           {
                                                               _matchScore.MatchscoreId,
                                                               _matchScore.ScoreInfoTypeId,
                                                               _matchScore.HomeScore,
                                                               _matchScore.AwayScore,
                                                               _matchScore.TieBreak,
                                                               _matchScore.HomeTieBreakScore,
                                                               _matchScore.AwayTieBreakScore
                                                           } into matchScore
                                                           select new MatchScore
                                                           {
                                                               MatchScoreId = matchScore.Key.MatchscoreId,
                                                               ScoreInfoTypeId = matchScore.Key.ScoreInfoTypeId,
                                                               HomeScore = matchScore.Key.HomeScore,
                                                               AwayScore = matchScore.Key.AwayScore,
                                                               TieBreak = matchScore.Key.TieBreak,
                                                               HomeTieBreak = matchScore.Key.HomeTieBreakScore,
                                                               AwayTieBreak = matchScore.Key.AwayTieBreakScore
                                                           }).ToList()
                                        }).FirstOrDefault();
            }

            return tennisMatchInfoModel;
        }

        public static List<TennisLeaguesListModel> GetContestGroupList(GlobalParametersModel globalParametersModel)
        {
            List<TennisLeaguesListModel> tennisLeaguesListModel = null;
            //If OrgId is not zero than it is not a challenger type.
            if (globalParametersModel.SportOrgTypeId == 0)
            {
                tennisLeaguesListModel = DBManager.Execute<TennisLeaguesListModel>("BetwayWidget_Tennis_GetContestGroupList",
                    new
                    {
                        SportId = globalParametersModel.SportId,
                        SportOrgId = globalParametersModel.SportOrgId,
                        GenderId = globalParametersModel.GenderId,
                        LanguageCode = globalParametersModel.LanguageCode
                    });
            }
            else
            {
                tennisLeaguesListModel = DBManager.Execute<TennisLeaguesListModel>("BetwayWidget_Tennis_GetContestGroupListChallenger",
                    new
                    {
                        SportId = globalParametersModel.SportId,
                        WinPoints = 1,  //This is saved in WinPoints column for checking whether it is challenger or not.
                        GenderId = globalParametersModel.GenderId,
                        LanguageCode = globalParametersModel.LanguageCode
                    });
            }

            #region ADO.NET Code
            //using (DataTable _dataTable = new DataTable())
            //{
            //    using (SqlConnection _connection = new SqlConnection(_connectionString))
            //    {
            //        _connection.Open();
            //        using (SqlCommand _command = new SqlCommand("BetwayWidget_Tennis_GetContestGroupList", _connection))
            //        {
            //            _command.CommandType = CommandType.StoredProcedure;
            //            _command.Parameters.Add(new SqlParameter("@SportId", sid));
            //            _command.Parameters.Add(new SqlParameter("@SportOrgId", orgId));
            //            using (SqlDataAdapter _sqlDataAdapter = new SqlDataAdapter(_command))
            //            {
            //                _sqlDataAdapter.Fill(_dataTable);
            //            }
            //        }
            //    }

            //    foreach (DataRow _dataRow in _dataTable.Rows)
            //    {
            //        TennisLeaguesListModel _tennisLeaguesListModel = new TennisLeaguesListModel()
            //        {
            //            ContestGroupId = Convert.ToInt32(_dataRow["ContestGroupId"]),
            //            ContestGroupName = Convert.ToString(_dataRow["ContestGroupName"]),
            //            ContestType = Convert.ToString(_dataRow["ContestType"]),
            //            CountryId = Convert.ToInt16(_dataRow["CountryId"]),
            //            CountryName = Convert.ToString(_dataRow["CountryName"]),
            //            SportOrgId = Convert.ToInt16(_dataRow["SportOrgId"]),
            //            OrgName = Convert.ToString(_dataRow["OrgName"]),
            //            IsOrder = Convert.ToInt32(_dataRow["IsOrder"])
            //        };
            //        tennisLeaguesListModel.Add(_tennisLeaguesListModel);
            //    }
            //} 
            #endregion

            return tennisLeaguesListModel;
        }

        public static List<SportsOrganizationModel> GetSportOrganizationList(GlobalParametersModel globalParametersModel)
        {
            List<SportsOrganizationModel> sportsOrganizationModel = DBManager.Execute<SportsOrganizationModel>("GetSportOrganizations",
                new { SportId = globalParametersModel.SportId });

            #region ADO.Net code
            //using (DataTable _dataTable = new DataTable())
            //{
            //    using (SqlConnection _connection = new SqlConnection(_connectionString))
            //    {
            //        _connection.Open();
            //        using (SqlCommand _command = new SqlCommand("GetSportOrganizationDropdownItems", _connection))
            //        {
            //            _command.CommandType = CommandType.StoredProcedure;
            //            _command.Parameters.Add(new SqlParameter("@SportId", sid));
            //            using (SqlDataAdapter _sqlDataAdapter = new SqlDataAdapter(_command))
            //            {
            //                _sqlDataAdapter.Fill(_dataTable);
            //            }
            //        }
            //    }

            //    foreach (DataRow _dataRow in _dataTable.Rows)
            //    {
            //        SportsOrganizationModel _sportsOrganizationModel = new SportsOrganizationModel()
            //        {
            //            SportOrgId = Convert.ToInt16(_dataRow["Value"]),
            //            OrgName = Convert.ToString(_dataRow["Text"])
            //        };
            //        sportsOrganizationModel.Add(_sportsOrganizationModel);
            //    }
            //} 
            #endregion

            return sportsOrganizationModel;
        }

        public static TennisContestGroup GetMatchListByContestGroupId(GlobalParametersModel globalParametersModel)
        {
            TennisContestGroup tennisContestGroup = new TennisContestGroup();
            List<TennisMatchListDBModel> tennisMatchListDBModel = DBManager.Execute<TennisMatchListDBModel>("BetwayWidget_TennisMatchesByContestId",
                new
                {
                    SportId = globalParametersModel.SportId,
                    ContestGroupId = globalParametersModel.ContestGroupId,
                    LanguageCode = globalParametersModel.LanguageCode
                });

            #region ADO.NET code

            //List <TennisMatchListDBModel> tennisMatchListDBModel = new List<TennisMatchListDBModel>();
            // using (DataTable _dataTable = new DataTable())
            // {
            //     using (SqlConnection _connection = new SqlConnection(_connectionString))
            //     {
            //         _connection.Open();
            //         using (SqlCommand _command = new SqlCommand("BetwayWidget_TennisMatchesByContestId", _connection))
            //         {
            //             _command.CommandType = CommandType.StoredProcedure;
            //             _command.Parameters.Add(new SqlParameter("@SportId", sid));
            //             _command.Parameters.Add(new SqlParameter("@ContestGroupId", cgid));
            //             using (SqlDataAdapter _sqlDataAdapter = new SqlDataAdapter(_command))
            //             {
            //                 _sqlDataAdapter.Fill(_dataTable);
            //             }
            //         }
            //     }

            //     foreach (DataRow _dataRow in _dataTable.Rows)
            //     {
            //         TennisMatchListDBModel _tennisMatchListDBModel = new TennisMatchListDBModel()
            //         {
            //             SportId = Convert.ToInt16(_dataRow["SportId"]),
            //             SportName = _dataRow["SportName"].ToString(),
            //             ContestGroupId = Convert.ToInt32(_dataRow["ContestGroupId"]),
            //             ContestGroupName = _dataRow["ContestGroupName"].ToString(),
            //             ContestType = _dataRow["ContestType"].ToString(),
            //             CountryId = Convert.ToInt16(_dataRow["CountryId"]),
            //             CountryName = _dataRow["CountryName"].ToString(),
            //             OrgName = _dataRow["OrgName"].ToString(),
            //             MatchId = Convert.ToInt32(_dataRow["MatchId"]),
            //             MatchDate = _dataRow["MatchDate"].ToString(),
            //             MatchStatusId = Convert.ToInt16(_dataRow["MatchStatusId"]),
            //             MatchStatus = _dataRow["MatchStatus"].ToString(),
            //             HomeTeamId = Convert.ToInt32(_dataRow["HomeTeamId"]),
            //             HomeTeamName = _dataRow["HomeTeamName"].ToString(),
            //             AwayTeamId = Convert.ToInt32(_dataRow["AwayTeamId"]),
            //             AwayTeamName = _dataRow["AwayTeamName"].ToString(),
            //             MatchscoreId = _dataRow["MatchscoreId"] != DBNull.Value ? Convert.ToInt32(_dataRow["MatchScoreId"]) : (Int32?)null,
            //             ScoreInfoTypeId = _dataRow["ScoreInfoTypeId"] != DBNull.Value ? Convert.ToInt16(_dataRow["ScoreInfoTypeId"]) : (Int16?)null,
            //             HomeScore = _dataRow["HomeScore"].ToString(),
            //             AwayScore = _dataRow["AwayScore"].ToString(),
            //             TieBreak = _dataRow["TieBreak"] != DBNull.Value ? Convert.ToBoolean(_dataRow["TieBreak"]) : (bool?)null,
            //             HomeTieBreakScore = _dataRow["HomeTieBreakScore"].ToString(),
            //             AwayTieBreakScore = _dataRow["AwayTieBreakScore"].ToString(),
            //             HomeTeamWin = _dataRow["HomeTeamWin"] != DBNull.Value ? Convert.ToBoolean(_dataRow["HomeTeamWin"]) : false,
            //             AwayTeamWin = _dataRow["AwayTeamWin"] != DBNull.Value ? Convert.ToBoolean(_dataRow["AwayTeamWin"]) : false
            //         };
            //         tennisMatchListDBModel.Add(_tennisMatchListDBModel);
            //     }
            // } 
            #endregion

            if (tennisMatchListDBModel != null && tennisMatchListDBModel.Count > 0)
            {
                tennisContestGroup = (from _contestGroup in tennisMatchListDBModel.ToList()
                                      group _contestGroup by new
                                      {
                                          _contestGroup.ContestGroupId,
                                          _contestGroup.ContestGroupName,
                                          _contestGroup.ContestType,
                                          _contestGroup.CountryId,
                                          _contestGroup.CountryName,
                                          _contestGroup.OrgName,
                                          _contestGroup.SportOrgId,
                                          _contestGroup.LeagueName
                                      } into contestGroup
                                      select new TennisContestGroup
                                      {
                                          ContestGroupId = contestGroup.Key.ContestGroupId,
                                          ContestGroupName = contestGroup.Key.ContestGroupName,
                                          ContestType = contestGroup.Key.ContestType,
                                          CountryId = contestGroup.Key.CountryId,
                                          CountryName = contestGroup.Key.CountryName,
                                          OrgName = contestGroup.Key.OrgName,
                                          SportOrgId = contestGroup.Key.SportOrgId,
                                          LeagueName = contestGroup.Key.LeagueName,
                                          Matches = (from _match in tennisMatchListDBModel.ToList()
                                                     where _match.ContestGroupId == contestGroup.Key.ContestGroupId
                                                     group _match by new
                                                     {
                                                         _match.MatchId,
                                                         _match.MatchStatus,
                                                         _match.MatchDate,
                                                         _match.HomeTeamId,
                                                         _match.HomeTeamName,
                                                         _match.AwayTeamId,
                                                         _match.AwayTeamName,
                                                         _match.MatchStatusId,
                                                         _match.MatchInfo
                                                     } into match
                                                     select new Match
                                                     {
                                                         MatchId = match.Key.MatchId,
                                                         MatchStatus = match.Key.MatchStatus,
                                                         MatchDate = Convert.ToDateTime(match.Key.MatchDate),
                                                         HomeTeamId = match.Key.HomeTeamId,
                                                         HomeTeamName = match.Key.HomeTeamName,
                                                         AwayTeamId = match.Key.AwayTeamId,
                                                         AwayTeamName = match.Key.AwayTeamName,
                                                         MatchStatusId = match.Key.MatchStatusId,
                                                         HomeTeamWin = match.FirstOrDefault(x => x.ScoreInfoTypeId == 17) != null ? match.FirstOrDefault(x => x.ScoreInfoTypeId == 17).HomeTeamWin : false,
                                                         AwayTeamWin = match.FirstOrDefault(x => x.ScoreInfoTypeId == 17) != null ? match.FirstOrDefault(x => x.ScoreInfoTypeId == 17).AwayTeamWin : false,
                                                         IsMatchScoreAvailable = match.Key.MatchInfo,
                                                         MatchScores = (from _matchScore in tennisMatchListDBModel.ToList()
                                                                        where _matchScore.MatchId == match.Key.MatchId
                                                                        group _matchScore by new
                                                                        {
                                                                            _matchScore.MatchscoreId,
                                                                            _matchScore.ScoreInfoTypeId,
                                                                            _matchScore.HomeScore,
                                                                            _matchScore.AwayScore,
                                                                            _matchScore.TieBreak,
                                                                            _matchScore.HomeTieBreakScore,
                                                                            _matchScore.AwayTieBreakScore
                                                                        } into matchScores
                                                                        select new MatchScore
                                                                        {
                                                                            MatchScoreId = matchScores.Key.MatchscoreId,
                                                                            ScoreInfoTypeId = matchScores.Key.ScoreInfoTypeId,
                                                                            HomeScore = matchScores.Key.HomeScore,
                                                                            AwayScore = matchScores.Key.AwayScore,
                                                                            TieBreak = matchScores.Key.TieBreak,
                                                                            HomeTieBreak = matchScores.Key.HomeTieBreakScore,
                                                                            AwayTieBreak = matchScores.Key.AwayTieBreakScore
                                                                        }).ToList()
                                                     }).ToList()
                                      }).FirstOrDefault();
            }
            return tennisContestGroup;
        }

        public static TennisStatsModel GetTennisStats(GlobalParametersModel globalParametersModel)
        {
            TennisStatsModel tennisStatsModel = new TennisStatsModel();
            List<TennisStatDBModel> tennisStatDBModel = DBManager.Execute<TennisStatDBModel>("BWTennisStats",
                new
                {
                    StatsTypeId = globalParametersModel.StatsTypeId,
                    ContestGroupId = globalParametersModel.ContestGroupId
                });

            #region ADO.NET code
            //List<TennisStatDBModel> tennisStatDBModel = new List<TennisStatDBModel>();
            //using (DataTable _dataTable = new DataTable())
            //{
            //    using (SqlConnection _connection = new SqlConnection(_connectionString))
            //    {
            //        _connection.Open();
            //        using (SqlCommand _command = new SqlCommand("BWTennisStats", _connection))
            //        {
            //            _command.CommandType = CommandType.StoredProcedure;
            //            _command.Parameters.Add(new SqlParameter("@StatsTypeId", statsTypeId));
            //            _command.Parameters.Add(new SqlParameter("@ContestGroupId", contestGroupId));
            //            using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(_command))
            //            {
            //                sqlDataAdapter.Fill(_dataTable);
            //            }
            //        }
            //    }

            //    foreach (DataRow _dataRow in _dataTable.Rows)
            //    {
            //        TennisStatDBModel _tennisStatDBModel = new TennisStatDBModel()
            //        {
            //            TeamName = Convert.ToString(_dataRow["TeamName"]),
            //            SeasonName = Convert.ToString(_dataRow["SeasonName"]),
            //            Points = _dataRow["Points"] != DBNull.Value ? Convert.ToString(_dataRow["Points"]) : "-",
            //            PlayerId = Convert.ToInt32(_dataRow["PlayerId"]),
            //            SimplifiedPoints = getSimplifiedPoints((_dataRow["Points"]))
            //        };
            //        tennisStatDBModel.Add(_tennisStatDBModel);
            //    }
            //}
            #endregion

            if (tennisStatDBModel != null && tennisStatDBModel.Count > 0)
            {
                tennisStatsModel = (from _stats in tennisStatDBModel.ToList()
                                    group _stats by new { _stats.SeasonName } into season
                                    select new TennisStatsModel
                                    {
                                        SeasonName = season.Key.SeasonName,
                                        Teams = (from _tennisStats in tennisStatDBModel.ToList()
                                                 group _tennisStats by new
                                                 {
                                                     _tennisStats.TeamName,
                                                     _tennisStats.Points,
                                                     _tennisStats.PlayerId,
                                                     _tennisStats.SimplifiedPoints
                                                 } into team
                                                 select new TennisStatsTeam
                                                 {
                                                     TeamName = team.Key.TeamName,
                                                     Points = team.Key.Points,
                                                     PlayerId = team.Key.PlayerId,
                                                     SimplifiedPoints = team.Key.SimplifiedPoints
                                                 }).OrderByDescending(x => x.SimplifiedPoints).ToList()
                                    }).FirstOrDefault();
            }
            return tennisStatsModel;
        }

        public static HeadToHeadModel GetTennisHeadToHead(GlobalParametersModel globalParametersModel)
        {
            TennisHeadToHeadDBModel tennisHeadToHeadDBModel = DBManager.Execute<TennisTeamsH2H, TennisHeadToHeadTeamBioDBModel, TennisHeadToHeadTeamRecentWinningDBModel, TennisHeadToHeadTeamRecentWinningDBModel, TennisHeadToHeadDBModel>("BW_Tennis_HeadToHead",
                new
                {
                    firstTeamId = globalParametersModel.firstTeamId,
                    secondTeamId = globalParametersModel.secondTeamId
                });
            HeadToHeadModel headToHeadModel = new HeadToHeadModel();
            if (tennisHeadToHeadDBModel != null)
            {
                headToHeadModel.TeamBio = (from t in tennisHeadToHeadDBModel.TeamBio
                                           select new TennisHeadToHeadTeamBioModel
                                           {
                                               Age = string.IsNullOrEmpty(t.Age) ? "-" : t.Age,
                                               TeamId = t.TeamId,
                                               YTDServiceGamesWon = string.IsNullOrEmpty(t.YTDServiceGamesWon) ? "-" : t.YTDServiceGamesWon + " %",
                                               YTDMatchesLost = t.YTDMatchesLost == null ? "-" : Convert.ToString(t.YTDMatchesLost),
                                               YTDMatchWon = t.YTDMatchWon == null ? "-" : Convert.ToString(t.YTDMatchWon),
                                               CareerMatchesLost = t.CareerMatchesLost == null ? "-" : Convert.ToString(t.CareerMatchesLost),
                                               CareerMatchesWon = t.CareerMatchesWon == null ? "-" : Convert.ToString(t.CareerMatchesWon),
                                               CareerSingleTitle = t.CareerSingleTitle == null ? "-" : Convert.ToString(t.CareerSingleTitle),
                                               CountryOfBirth = string.IsNullOrEmpty(t.CountryOfBirth) ? "-" : Convert.ToString(t.CountryOfBirth),
                                               HomeAway = Convert.ToBoolean(t.HomeAway),
                                               RaceToLondon = t.RaceToLondon == null ? "-" : Convert.ToString(t.RaceToLondon),
                                               SingleRanking = t.SingleRanking == null ? "-" : Convert.ToString(t.SingleRanking),
                                               TeamName = Convert.ToString(t.TeamName),
                                               YTDReturnGamesWon = string.IsNullOrEmpty(t.YTDReturnGamesWon) ? "-" : Convert.ToString(t.YTDReturnGamesWon) + " %"
                                           }).ToList();
            }

            if (tennisHeadToHeadDBModel != null)
            {
                headToHeadModel.Team1RecentWins = (from t in tennisHeadToHeadDBModel.Team1RecentWins
                                                   select new TennisHeadToHeadTeamRecentWinningModel
                                                   {
                                                       MatchId = t.MatchId,
                                                       MatchDate = t.MatchDate,
                                                       HomeTeamId = t.HomeTeamId,
                                                       HomeTeamName = t.HomeTeamName,
                                                       AwayTeamId = t.AwayTeamId,
                                                       AwayTeamName = t.AwayTeamName,
                                                       HomeScore = t.HomeScore,
                                                       AwayScore = t.AwayScore,
                                                   }).ToList();
            }

            if (tennisHeadToHeadDBModel != null)
            {
                headToHeadModel.Team2RecentWins = (from t in tennisHeadToHeadDBModel.Team2RecentWins
                                                   select new TennisHeadToHeadTeamRecentWinningModel
                                                   {
                                                       MatchId = t.MatchId,
                                                       MatchDate = t.MatchDate,
                                                       HomeTeamId = t.HomeTeamId,
                                                       HomeTeamName = t.HomeTeamName,
                                                       AwayTeamId = t.AwayTeamId,
                                                       AwayTeamName = t.AwayTeamName,
                                                       HomeScore = t.HomeScore,
                                                       AwayScore = t.AwayScore,
                                                   }).ToList();
            }

            if (tennisHeadToHeadDBModel != null)
            {
                headToHeadModel.TennisTeamsH2H = (from t in tennisHeadToHeadDBModel.TennisTeamsH2H
                                                  select new TennisTeamsHtwoH
                                                  {
                                                      FirstWon = Convert.ToInt32(t.FirstWon),
                                                      SecondWon = Convert.ToInt32(t.SecondWon),
                                                      TotalMatchesPlayed = Convert.ToInt32(t.TotalMatchesPlayed)
                                                  }).FirstOrDefault();
            }

            #region ADO.Net
            //TennisHeadToHeadDBModel tennisHeadToHeadDBModel = new TennisHeadToHeadDBModel();
            //tennisHeadToHeadDBModel.TennisTeamsH2H = new TennisTeamsH2H();
            //tennisHeadToHeadDBModel.TeamBio = new List<TennisHeadToHeadTeamBioDBModel>();
            //tennisHeadToHeadDBModel.Team1RecentWins = new List<TennisHeadToHeadTeamRecentWinningDBModel>();
            //tennisHeadToHeadDBModel.Team2RecentWins = new List<TennisHeadToHeadTeamRecentWinningDBModel>();

            //DataSet _dataSet = new DataSet();
            //using (SqlConnection _connection = new SqlConnection(_connectionString))
            //{
            //    _connection.Open();
            //    using (SqlCommand _command = new SqlCommand("BW_Tennis_HeadToHead", _connection))
            //    {
            //        _command.CommandType = CommandType.StoredProcedure;
            //        _command.Parameters.Add(new SqlParameter("@firstTeamId", team1Id));
            //        _command.Parameters.Add(new SqlParameter("@secondTeamId", team2Id));
            //        using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(_command))
            //        {
            //            sqlDataAdapter.Fill(_dataSet);
            //        }
            //    }

            //    if (_dataSet.Tables.Count > 0)
            //    {
            //        if (_dataSet.Tables[0].Rows.Count > 0)
            //        {
            //            DataRow dr = _dataSet.Tables[0].Rows[0];
            //            TennisTeamsH2H h2h = new TennisTeamsH2H();
            //            h2h.TotalMatchesPlayed = Convert.ToInt32(dr["TotalMatchesPlayed"]);
            //            h2h.FirstWon = Convert.ToInt32(dr["FirstWon"]);
            //            h2h.SecondWon = Convert.ToInt32(dr["SecondWon"]);
            //            tennisHeadToHeadDBModel.TennisTeamsH2H = h2h;
            //        }

            //        if (_dataSet.Tables[1].Rows.Count > 0)
            //        {
            //            foreach (DataRow dr in _dataSet.Tables[1].Rows)
            //            {
            //                TennisHeadToHeadTeamBioDBModel query = new TennisHeadToHeadTeamBioDBModel();
            //                query.TeamId = Convert.ToInt32(dr["TeamId"]);
            //                query.TeamName = dr["TeamName"].ToString();
            //                query.Age = dr["Age"] == DBNull.Value ? "-" : Convert.ToString(dr["Age"]);
            //                query.CountryOfBirth = dr["CountryOfBirth"] == DBNull.Value ? "-" : Convert.ToString(dr["CountryOfBirth"]);
            //                query.HomeAway = Convert.ToBoolean(dr["HomeAway"]);
            //                query.SingleRanking = dr["SingleRanking"] == DBNull.Value ? "-" : Convert.ToString(dr["SingleRanking"]);
            //                query.RaceToLondon = dr["RaceToLondon"] == DBNull.Value ? "-" : Convert.ToString(dr["RaceToLondon"]);
            //                query.CareerSingleTitle = dr["CareerSingleTitle"] == DBNull.Value ? "-" : Convert.ToString(dr["CareerSingleTitle"]);
            //                query.YTDMatchesLost = dr["YTDMatchesLost"] == DBNull.Value ? "-" : Convert.ToString(dr["YTDMatchesLost"]);
            //                query.YTDMatchWon = dr["YTDMatchWon"] == DBNull.Value ? "-" : Convert.ToString(dr["YTDMatchWon"]);
            //                query.CareerMatchesLost = dr["CareerMatchesLost"] == DBNull.Value ? "-" : Convert.ToString(dr["CareerMatchesLost"]);
            //                query.CareerMatchesWon = dr["CareerMatchesWon"] == DBNull.Value ? "-" : Convert.ToString(dr["CareerMatchesWon"]);
            //                query.YTDServiceGamesWon = dr["YTDServiceGamesWon"] == DBNull.Value ? "-" : Convert.ToString(dr["YTDServiceGamesWon"]) + " %";
            //                query.YTDReturnGamesWon = dr["YTDReturnGamesWon"] == DBNull.Value ? "-" : Convert.ToString(dr["YTDReturnGamesWon"]) + " %";
            //                tennisHeadToHeadDBModel.TeamBio.Add(query);
            //            }
            //        }

            //        if (_dataSet.Tables[2].Rows.Count > 0)
            //        {
            //            foreach (DataRow dr in _dataSet.Tables[2].Rows)
            //            {
            //                TennisHeadToHeadTeamRecentWinningDBModel t1 = new TennisHeadToHeadTeamRecentWinningDBModel();
            //                t1.TeamId = Convert.ToInt32(dr["TeamId"]);
            //                t1.WinLoss = dr["WinLoss"] != DBNull.Value ? Convert.ToBoolean(dr["WinLoss"]) : (bool?)null;
            //                tennisHeadToHeadDBModel.Team1RecentWins.Add(t1);
            //            }
            //        }

            //        if (_dataSet.Tables[3].Rows.Count > 0)
            //        {
            //            foreach (DataRow dr in _dataSet.Tables[3].Rows)
            //            {
            //                TennisHeadToHeadTeamRecentWinningDBModel t2 = new TennisHeadToHeadTeamRecentWinningDBModel();
            //                t2.TeamId = Convert.ToInt32(dr["TeamId"]);
            //                t2.WinLoss = dr["WinLoss"] != DBNull.Value ? Convert.ToBoolean(dr["WinLoss"]) : (bool?)null;
            //                tennisHeadToHeadDBModel.Team2RecentWins.Add(t2);
            //            }
            //        }
            //    }
            //} 
            #endregion

            return headToHeadModel;
        }

        public static List<TennisDrawModel> GetTennisDrawsByRoundId(GlobalParametersModel globalParametersModel)
        {
            List<TennisDrawModel> tennisDrawModel = new List<TennisDrawModel>();
            List<TennisDrawDBModel> tennisDrawDBModel = DBManager.Execute<TennisDrawDBModel>("BetwayWidget_GetDrawMatches",
                new
                {
                    SportId = globalParametersModel.SportId,
                    ContestGroupId = globalParametersModel.ContestGroupId,
                    RoundId = globalParametersModel.ContestGroupRoundId
                });

            #region ADO.NET Code
            //using (DataTable _dataTable = new DataTable())
            //{
            //    using (SqlConnection _connection = new SqlConnection(_connectionString))
            //    {
            //        _connection.Open();
            //        using (SqlCommand _command = new SqlCommand("BetwayWidget_GetDrawMatches", _connection))
            //        {
            //            _command.CommandType = CommandType.StoredProcedure;
            //            _command.Parameters.Add(new SqlParameter("@SportId", sportId));
            //            _command.Parameters.Add(new SqlParameter("@ContestGroupId", contestGroupId));
            //            _command.Parameters.Add(new SqlParameter("@RoundId", roundId));
            //            using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(_command))
            //            {
            //                sqlDataAdapter.Fill(_dataTable);
            //            }
            //        }
            //    }

            //    foreach (DataRow _dataRow in _dataTable.Rows)
            //    {
            //        TennisDrawDBModel _tennisDrawDbModel = new TennisDrawDBModel()
            //        {
            //            MatchId = Convert.ToInt32(_dataRow["MatchId"]),
            //            HomeTeamId = Convert.ToInt32(_dataRow["HomeTeamId"]),
            //            AwayTeamId = Convert.ToInt32(_dataRow["AwayTeamId"]),
            //            HomeTeam = Convert.ToString(_dataRow["HomeTeam"]),
            //            AwayTeam = Convert.ToString(_dataRow["AwayTeam"]),
            //            HomeScore = _dataRow["HomeScore"] != DBNull.Value ? Convert.ToInt32(_dataRow["HomeScore"]) : (int?)null,
            //            AwayScore = _dataRow["AwayScore"] != DBNull.Value ? Convert.ToInt32(_dataRow["AwayScore"]) : (int?)null,
            //            ScoreInfoTypeId = _dataRow["ScoreInfoTypeId"] != DBNull.Value ? Convert.ToInt32(_dataRow["ScoreInfoTypeId"]) : (int?)null,
            //            HomeTeamWin = Convert.ToBoolean(_dataRow["HomeTeamWin"]),
            //            AwayTeamWin = Convert.ToBoolean(_dataRow["AwayTeamWin"]),
            //            HomeTeamRanking = _dataRow["HomeTeamRanking"] != DBNull.Value ? Convert.ToInt32(_dataRow["HomeTeamRanking"]) : (int?)null,
            //            AwayTeamRanking = _dataRow["AwayTeamRanking"] != DBNull.Value ? Convert.ToInt32(_dataRow["AwayTeamRanking"]) : (int?)null
            //        };
            //        tennisDrawDBModel.Add(_tennisDrawDbModel);
            //    }
            //} 
            #endregion

            if (tennisDrawDBModel != null && tennisDrawDBModel.Count > 0)
            {
                tennisDrawModel = (from _draw in tennisDrawDBModel.ToList()
                                   group _draw by new
                                   {
                                       _draw.MatchId
                                   } into draw
                                   select new TennisDrawModel
                                   {
                                       MatchId = draw.Key.MatchId,
                                       MatchStatusId = draw.FirstOrDefault(x => x.MatchId == draw.Key.MatchId).MatchStatusId,
                                       MatchDate = draw.FirstOrDefault(x => x.MatchId == draw.Key.MatchId).MatchDate,
                                       AwayTeam = draw.FirstOrDefault(x => x.MatchId == draw.Key.MatchId).AwayTeam,
                                       AwayTeamId = draw.FirstOrDefault(x => x.MatchId == draw.Key.MatchId).AwayTeamId,
                                       HomeTeam = draw.FirstOrDefault(x => x.MatchId == draw.Key.MatchId).HomeTeam,
                                       HomeTeamId = draw.FirstOrDefault(x => x.MatchId == draw.Key.MatchId).HomeTeamId,
                                       HomeTeamWin = draw.FirstOrDefault(x => x.ScoreInfoTypeId == 17) != null ? draw.FirstOrDefault(x => x.ScoreInfoTypeId == 17).HomeTeamWin : false,
                                       AwayTeamWin = draw.FirstOrDefault(x => x.ScoreInfoTypeId == 17) != null ? draw.FirstOrDefault(x => x.ScoreInfoTypeId == 17).AwayTeamWin : false,
                                       HomeTeamRanking = draw.FirstOrDefault(x => x.MatchId == draw.Key.MatchId).HomeTeamRanking,
                                       AwayTeamRanking = draw.FirstOrDefault(x => x.MatchId == draw.Key.MatchId).AwayTeamRanking,
                                       MatchScores = (from matchesScore in tennisDrawDBModel.ToList()
                                                      where draw.Key.MatchId == matchesScore.MatchId
                                                      group matchesScore by new
                                                      {
                                                          matchesScore.ScoreInfoTypeId,
                                                          matchesScore.AwayScore,
                                                          matchesScore.HomeScore
                                                      } into _matchesScore
                                                      select new TennisMatchScores
                                                      {
                                                          ScoreInfoTypeId = _matchesScore.Key.ScoreInfoTypeId,
                                                          AwayScore = _matchesScore.Key.AwayScore,
                                                          HomeScore = _matchesScore.Key.HomeScore
                                                      }).ToList()
                                   }).DistinctBy(x => x.MatchId).ToList();
            }

            return tennisDrawModel;
        }

        public static List<ContestRoundsModel> GetRoundListByContestGroupId(GlobalParametersModel globalParametersModel)
        {
            List<ContestRoundsModel> ContestRoundsModel = DBManager.Execute<ContestRoundsModel>("BWTennis_GetContestGroupRoundById",
                new
                {
                    ContestGroupId = globalParametersModel.ContestGroupId,
                    LanguageCode = globalParametersModel.LanguageCode
                });

            #region ADO.Net Code
            //using (DataTable _dataTable = new DataTable())
            //{
            //    using (SqlConnection _connection = new SqlConnection(_connectionString))
            //    {
            //        _connection.Open();
            //        using (SqlCommand _command = new SqlCommand("BWTennis_GetContestGroupRoundById", _connection))
            //        {
            //            _command.CommandType = CommandType.StoredProcedure;
            //            _command.Parameters.Add(new SqlParameter("@ContestGroupId", contestId));
            //            using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(_command))
            //            {
            //                sqlDataAdapter.Fill(_dataTable);
            //            }
            //        }
            //    }

            //    foreach (DataRow _dataRow in _dataTable.Rows)
            //    {
            //        ContestRoundsModel _contestRoundsModel = new ContestRoundsModel
            //        {
            //            ContestGroupId = Convert.ToInt32(_dataRow["ContestGroupId"]),
            //            ContestGroupName = _dataRow["ContestGroupName"].ToString(),
            //            ContestGroupRoundId = Convert.ToInt32(_dataRow["ContestGroupRoundId"]),
            //            IsSelected = Convert.ToInt32(_dataRow["IsSelected"]),
            //            Round = _dataRow["Round"].ToString(),
            //            IsRound = Convert.ToInt32(_dataRow["IsRound"])
            //        };
            //        ContestRoundsModel.Add(_contestRoundsModel);
            //    }
            //} 
            #endregion

            return ContestRoundsModel;
        }

        public static List<TennisPlayerModel> GetPlayerListBySportOrganizationId(GlobalParametersModel globalParametersModel)
        {
            List<TennisPlayerModel> tennisPlayers = DBManager.Execute<TennisPlayerModel>("GoalCCAPI_GetContestTeamsBySportOrganizationId",
                new
                {
                    SportOrgId = globalParametersModel.SportOrgId
                });
            return tennisPlayers;
        }
        #endregion

        #region Private Methods
        private static decimal _GetSimplifiedPoints(object point)
        {
            // Checking for null value from db
            string convertedPoint = (point == DBNull.Value) ? "0" : Convert.ToString(point);
            //Getting only decimal value from string using REGEX
            string numericpoint = System.Text.RegularExpressions.Regex.Match(convertedPoint, @"\d+(\.\d+)?").Value;
            // Checking for empty string after Regex
            return !string.IsNullOrEmpty(numericpoint) ? Convert.ToDecimal(numericpoint) : 0;
        }
        #endregion
    }
}