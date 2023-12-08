using betway_result_center_api.Models;
using betway_result_center_api.Models.DatabaseModels;
using betway_result_center_api.Models.DatabaseModels.IceHockey;
using betway_result_center_api.Models.Models;
using betway_result_center_api.Models.Models.IceHockey;
using betway_result_center_api.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace betway_result_center_api.BLL
{
    public class IceHockeyBLL
    {
        #region Public Methods
        public static CountriesListModel GetIceHockeyCountryList(GlobalParametersModel globalParametersModel)
        {
            CountriesListModel countriesListModel = new CountriesListModel();
            List<CountriesListDBModel> countriesListDBModel = DBManager.Execute<CountriesListDBModel>("Betway_IceHockey_CountryList",
                new
                {
                    SportId = globalParametersModel.SportId,
                    LanguageCode = globalParametersModel.LanguageCode
                });

            if (countriesListDBModel != null && countriesListDBModel.Count > 0)
            {
                countriesListModel = (from _countriesListModel in countriesListDBModel.ToList()
                                      group _countriesListModel by
                                      new
                                      {
                                          _countriesListModel.CountryId,
                                          _countriesListModel.CountryName
                                      } into sports
                                      select new CountriesListModel
                                      {
                                          Countries = (from _countriesListDBModel in countriesListDBModel.ToList()
                                                       group _countriesListDBModel by new { _countriesListDBModel.CountryId, _countriesListDBModel.CountryName } into country
                                                       select new Countrieslist
                                                       {
                                                           CountryId = country.Key.CountryId,
                                                           CountryName = country.Key.CountryName
                                                       }).ToList()
                                      }).FirstOrDefault();
            }
            return countriesListModel;
        }

        public static ContestGroupsListModel GetIceHockeyContestGroupList(GlobalParametersModel globalParametersModel)
        {
            ContestGroupsListModel contestGroupsListModel = new ContestGroupsListModel();
            List<ContestGroupsListDBModel> contestGroupsListDBModel = DBManager.Execute<ContestGroupsListDBModel>("Betway_IceHockey_ContestGroupList",
                new
                {
                    SportId = globalParametersModel.SportId,
                    CountryId = globalParametersModel.CountryId,
                    LanguageCode = globalParametersModel.LanguageCode
                });

            if (contestGroupsListDBModel != null && contestGroupsListDBModel.Count > 0)
            {
                contestGroupsListModel = (from _contestGroup in contestGroupsListDBModel.ToList()
                                          group _contestGroup by new
                                          {
                                              _contestGroup.ContestGroupName,
                                              _contestGroup.ContestGroupId
                                          } into contestGroup
                                          select new ContestGroupsListModel
                                          {
                                              ContestGroups = (from _contestGroup in contestGroupsListDBModel.ToList()
                                                               select new ContestGroupList
                                                               {
                                                                   ContestGroupId = _contestGroup.ContestGroupId,
                                                                   ContestGroupName = _contestGroup.ContestGroupName
                                                               }).ToList()
                                          }).FirstOrDefault();
            }
            return contestGroupsListModel;
        }

        public static IceHockeyMatches GetIceHockeyMatchListByDate(GlobalParametersModel globalParametersModel)
        {
            int pageNumber = globalParametersModel.PageIndex;
            int pageSize = globalParametersModel.PageSize;
            IceHockeyMatches iceHockeyMatches = new IceHockeyMatches();
            List<IceHockeyMatchesModel> iceHockeyMatchesModel = DBManager.Execute<IceHockeyMatchesModel>("Betway_IceHockey_Matches",
                new
                {
                    SportId = globalParametersModel.SportId,
                    FromDate = globalParametersModel.FromDate,
                    ToDate = globalParametersModel.FromDate.AddDays(1),
                    LanguageCode = globalParametersModel.LanguageCode
                });

            if (iceHockeyMatchesModel != null && iceHockeyMatchesModel.Count > 0)
            {
                iceHockeyMatches = (from _result in iceHockeyMatchesModel.ToList()
                                    group _result by new
                                    {
                                        _result.ContestGroupId
                                    } into result
                                    select new IceHockeyMatches
                                    {
                                        IceHockeyContestGroups = (from _contestGroup in iceHockeyMatchesModel.ToList()
                                                                  group _contestGroup by new
                                                                  {
                                                                      _contestGroup.ContestGroupId
                                                                  } into contestGroups
                                                                  select new IceHockeyContestGroup
                                                                  {
                                                                      ContestGroupId = contestGroups.Key.ContestGroupId,
                                                                      ContestGroupName = contestGroups.FirstOrDefault(x => x.ContestGroupId == contestGroups.Key.ContestGroupId)?.ContestGroupName,
                                                                      CountryId = contestGroups.FirstOrDefault(x => x.ContestGroupId == contestGroups.Key.ContestGroupId)?.CountryId,
                                                                      CountryName = contestGroups.FirstOrDefault(x => x.ContestGroupId == contestGroups.Key.ContestGroupId)?.CountryName,
                                                                      IceHockeyMatches = (from _match in iceHockeyMatchesModel.ToList()
                                                                                          where _match.ContestGroupId == contestGroups.Key.ContestGroupId
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
                                                                                          } into match
                                                                                          select new IceHockeyMatch
                                                                                          {
                                                                                              MatchId = match.Key.MatchId,
                                                                                              MatchDate = match.Key.MatchDate,
                                                                                              MatchStatusId = match.Key.MatchStatusId,
                                                                                              MatchStatus = match.Key.MatchStatus,
                                                                                              HomeTeamId = match.Key.HomeTeamId,
                                                                                              HomeTeamName = match.Key.HomeTeamName,
                                                                                              AwayTeamId = match.Key.AwayTeamId,
                                                                                              AwayTeamName = match.Key.AwayTeamName,
                                                                                              HomeTeamWin = match.FirstOrDefault(x => x.ScoreInfoTypeId == 17 || x.ScoreInfoTypeId == 23 || x.ScoreInfoTypeId == 24) != null ? match.FirstOrDefault(x => x.ScoreInfoTypeId == 17 || x.ScoreInfoTypeId == 23 || x.ScoreInfoTypeId == 24).HomeTeamWin : false,
                                                                                              AwayTeamWin = match.FirstOrDefault(x => x.ScoreInfoTypeId == 17 || x.ScoreInfoTypeId == 23 || x.ScoreInfoTypeId == 24) != null ? match.FirstOrDefault(x => x.ScoreInfoTypeId == 17 || x.ScoreInfoTypeId == 23 || x.ScoreInfoTypeId == 24).AwayTeamWin : false,
                                                                                              IceHockeyMatchScores = (from _matchScores in iceHockeyMatchesModel.ToList()
                                                                                                                      where _matchScores.MatchId == match.Key.MatchId
                                                                                                                      group _matchScores by new
                                                                                                                      {
                                                                                                                          _matchScores.MatchscoreId,
                                                                                                                          _matchScores.ScoreInfoTypeId,
                                                                                                                          _matchScores.HomeScore,
                                                                                                                          _matchScores.AwayScore
                                                                                                                      } into matchScores
                                                                                                                      select new IceHockeyMatchScores
                                                                                                                      {
                                                                                                                          MatchscoreId = matchScores.Key.MatchscoreId,
                                                                                                                          ScoreInfoTypeId = matchScores.Key.ScoreInfoTypeId,
                                                                                                                          HomeScore = matchScores.Key.HomeScore,
                                                                                                                          AwayScore = matchScores.Key.AwayScore,
                                                                                                                      }
                                                                                                                      ).ToList()
                                                                                          }).ToList()
                                                                  }).ToList()
                                    }).FirstOrDefault();
                iceHockeyMatches.IceHockeyContestGroups = iceHockeyMatches.IceHockeyContestGroups.Skip(pageNumber * pageSize).Take(pageSize).ToList();
            }
            return iceHockeyMatches;
        }

        public static List<IceHockeyMatchesbyRound> GetIceHockeyMatchListByRoundId(GlobalParametersModel globalParameterModel)
        {
            List<IceHockeyMatchesbyRound> matchesByRound = new List<IceHockeyMatchesbyRound>();
            List<IceHockeyMatchesByRoundModel> matchesByRoundModel = DBManager.Execute<IceHockeyMatchesByRoundModel>("Betway_IceHockey_Matches_By_RoundId",
                new
                {
                    ContestId = globalParameterModel.ContestGroupId,
                    ContestGroupRoundId = globalParameterModel.ContestGroupRoundId
                });

            if (matchesByRoundModel != null && matchesByRoundModel.Count > 0)
            {
                matchesByRound = (from _match in matchesByRoundModel.ToList()
                                  group _match by new
                                  {
                                      _match.MatchId
                                  } into match
                                  select new IceHockeyMatchesbyRound
                                  {
                                      MatchId = match.Key.MatchId,
                                      MatchDate = match.FirstOrDefault(x => x.MatchId == match.Key.MatchId).MatchDate,
                                      ContestGroupRoundId = match.FirstOrDefault(x => x.MatchId == match.Key.MatchId).ContestGroupRoundId,
                                      MatchStatusId = match.FirstOrDefault(x => x.MatchId == match.Key.MatchId).MatchStatusId,
                                      HomeTeamId = match.FirstOrDefault(x => x.MatchId == match.Key.MatchId).HomeTeamId,
                                      AwayTeamId = match.FirstOrDefault(x => x.MatchId == match.Key.MatchId).AwayTeamId,
                                      HomeTeamName = match.FirstOrDefault(x => x.MatchId == match.Key.MatchId).HomeTeamName,
                                      AwayTeamName = match.FirstOrDefault(x => x.MatchId == match.Key.MatchId).AwayTeamName,
                                      HomeTeamWin = match.FirstOrDefault(x => x.ScoreInfoTypeId == 17 || x.ScoreInfoTypeId == 23 || x.ScoreInfoTypeId == 24) != null ? match.FirstOrDefault(x => x.ScoreInfoTypeId == 17 || x.ScoreInfoTypeId == 23 || x.ScoreInfoTypeId == 24).HomeTeamWin : false,
                                      AwayTeamWin = match.FirstOrDefault(x => x.ScoreInfoTypeId == 17 || x.ScoreInfoTypeId == 23 || x.ScoreInfoTypeId == 24) != null ? match.FirstOrDefault(x => x.ScoreInfoTypeId == 17 || x.ScoreInfoTypeId == 23 || x.ScoreInfoTypeId == 24).AwayTeamWin : false,
                                      IceHockeyMatchScores = (from _matchScore in matchesByRoundModel.ToList()
                                                              where _matchScore.MatchId == match.Key.MatchId
                                                              group _matchScore by new
                                                              {
                                                                  _matchScore.MatchscoreId,
                                                                  _matchScore.ScoreInfoTypeId,
                                                                  _matchScore.HomeScore,
                                                                  _matchScore.AwayScore
                                                              } into matchScore
                                                              select new IceHockeyMatchScores
                                                              {
                                                                  MatchscoreId = matchScore.Key.MatchscoreId,
                                                                  ScoreInfoTypeId = matchScore.Key.ScoreInfoTypeId,
                                                                  HomeScore = matchScore.Key.HomeScore,
                                                                  AwayScore = matchScore.Key.AwayScore,
                                                              }).ToList()
                                  }).ToList();
            }
            return matchesByRound;
        }

        public static List<IceHockeyStats> GetIceHockeyStats(GlobalParametersModel globalParametersModel)
        {
            List<IceHockeyStats> iceHockeyStats = new List<IceHockeyStats>();
            List<IceHockeyStatsModel> iceHockeyStatsModel = DBManager.Execute<IceHockeyStatsModel>("Betway_IceHockey_Stats",
                new
                {
                    ContestId = globalParametersModel.ContestGroupId
                });

            if (iceHockeyStatsModel != null && iceHockeyStatsModel.Count > 0)
            {
                iceHockeyStats = (from stats in iceHockeyStatsModel.ToList()
                                  group stats by new { stats.MatchId } into result
                                  select new IceHockeyStats
                                  {
                                      MatchId = result.Key.MatchId,
                                      SeasonName = result.FirstOrDefault(r => r.MatchId == result.Key.MatchId)?.SeasonName,
                                      HomeTeamId = result.FirstOrDefault(r => r.MatchId == result.Key.MatchId).HomeTeamId,
                                      AwayTeamId = result.FirstOrDefault(r => r.MatchId == result.Key.MatchId).AwayTeamId,
                                      HomeTeamName = result.FirstOrDefault(r => r.MatchId == result.Key.MatchId)?.HomeTeamName,
                                      AwayTeamName = result.FirstOrDefault(r => r.MatchId == result.Key.MatchId)?.AwayTeamName,
                                      FirstPeriodScoreHome = result.FirstOrDefault(r => r.MatchId == result.Key.MatchId)?.FirstPeriodScoreHome,
                                      FirstPeriodScoreAway = result.FirstOrDefault(r => r.MatchId == result.Key.MatchId)?.FirstPeriodScoreAway,
                                      FinishedScoreHome = result.FirstOrDefault(r => r.MatchId == result.Key.MatchId)?.FinishedScoreHome,
                                      FinishedScoreAway = result.FirstOrDefault(r => r.MatchId == result.Key.MatchId)?.FinishedScoreAway,
                                      FinishedOTScoreHome = result.FirstOrDefault(r => r.MatchId == result.Key.MatchId)?.FinishedOTScoreHome,
                                      FinishedOTScoreAway = result.FirstOrDefault(r => r.MatchId == result.Key.MatchId)?.FinishedOTScoreAway,
                                      FinishedAPScoreHome = result.FirstOrDefault(r => r.MatchId == result.Key.MatchId)?.FinishedAPScoreHome,
                                      FinishedAPScoreAway = result.FirstOrDefault(r => r.MatchId == result.Key.MatchId)?.FinishedAPScoreAway,
                                      TotalHomeScore = _GetTotalHomeScore(result.FirstOrDefault(r => r.MatchId == result.Key.MatchId)),
                                      TotalAwayScore = _GetTotalAwayScore(result.FirstOrDefault(r => r.MatchId == result.Key.MatchId)),
                                      HomeTeamWin = _IsHomeTeamWin(result.FirstOrDefault(r => r.MatchId == result.Key.MatchId)),
                                      AwayTeamWin = _IsAwayTeamWin(result.FirstOrDefault(r => r.MatchId == result.Key.MatchId)),
                                      TotalFirstPeriodGoals = result.FirstOrDefault(x => x.MatchId == result.Key.MatchId)?.getTotalFirstPeriodGoals(),
                                      TotalGoals = _GetTotalHomeScore(result.FirstOrDefault(r => r.MatchId == result.Key.MatchId)) + _GetTotalAwayScore(result.FirstOrDefault(r => r.MatchId == result.Key.MatchId))
                                  }).ToList();
            }
            return iceHockeyStats;
        }

        public static List<IceHockeyStandingsModel> GetIceHockeyStanding(GlobalParametersModel globalParameterModel)
        {
            List<IceHockeyStandingsModel> iceHockeyStandingModel = DBManager.Execute<IceHockeyStandingsModel>("Betway_IceHockey_GetStandings_By_ContestId",
                new
                {
                    ContestGroupId = globalParameterModel.ContestGroupId
                });
            return iceHockeyStandingModel;
        }

        public static List<IceHockeyContestTeamModel> GetIceHockeyContestTeamList(GlobalParametersModel globalParameterModel)
        {
            List<IceHockeyContestTeamModel> iceHockeyContestTeamModel = DBManager.Execute<IceHockeyContestTeamModel>("Betway_IceHockey_GetTeams_By_ContestId",
                new
                {
                    ContestGroupId = globalParameterModel.ContestGroupId
                });
            return iceHockeyContestTeamModel;
        }

        public static List<IceHockeyRounds> GetIceHockeyRoundList(GlobalParametersModel globalParameterModel)
        {
            List<IceHockeyRounds> iceHockeyRounds = DBManager.Execute<IceHockeyRounds>("Betway_IceHockey_Rounds",
                new
                {
                    ContestGroupId = globalParameterModel.ContestGroupId
                });
            return iceHockeyRounds;
        }

        public static IceHockeyHead2Head GetIceHockeyHeadtoHead(GlobalParametersModel globalParameterModel)
        {
            IceHockeyHead2Head iceHockeyHead2Head = DBManager.Execute<IceHockeyMatchForH2H, IceHockeyMatchForH2H, IceHockeyHead2Head>("Betway_IceHockey_HeadToHead", new
            {
                ContestGroupId = globalParameterModel.ContestGroupId,
                homeTeamId = globalParameterModel.HomeTeamId,
                awayTeamId = globalParameterModel.AwayTeamId,
            });
            return iceHockeyHead2Head;
        }
        #endregion

        #region Private Methods
        private static int _GetTotalHomeScore(IceHockeyStatsModel model)
        {
            int outFinishedScoreHome;
            int.TryParse(model.FinishedScoreHome, out outFinishedScoreHome);
            int outFinishedOTScoreHome;
            int.TryParse(model.FinishedOTScoreHome, out outFinishedOTScoreHome);
            int outFinishedAPScoreHome;
            int.TryParse(model.FinishedAPScoreHome, out outFinishedAPScoreHome);
            return outFinishedScoreHome + outFinishedOTScoreHome + outFinishedAPScoreHome;
        }

        private static int _GetTotalAwayScore(IceHockeyStatsModel model)
        {
            int outFinishedScoreAway;
            int.TryParse(model.FinishedScoreAway, out outFinishedScoreAway);
            int outFinishedOTScoreAway;
            int.TryParse(model.FinishedOTScoreAway, out outFinishedOTScoreAway);
            int outFinishedAPScoreAway;
            int.TryParse(model.FinishedAPScoreAway, out outFinishedAPScoreAway);
            return outFinishedScoreAway + outFinishedOTScoreAway + outFinishedAPScoreAway;
        }

        private static bool _IsHomeTeamWin(IceHockeyStatsModel model)
        {
            return _GetTotalHomeScore(model) > _GetTotalAwayScore(model);
        }

        private static bool _IsAwayTeamWin(IceHockeyStatsModel model)
        {
            return _GetTotalAwayScore(model) > _GetTotalHomeScore(model);
        }

        private static IceHockeyMatchForH2H _MapH2HMatch(DataRow dr)
        {
            IceHockeyMatchForH2H match = new IceHockeyMatchForH2H();
            match.MatchId = Convert.ToDecimal(dr["MatchId"]);
            match.MatchDate = Convert.ToDateTime(dr["MatchDate"]);
            match.HomeTeamId = Convert.ToInt32(dr["HomeTeamId"]);
            match.AwayTeamId = Convert.ToInt32(dr["AwayTeamId"]);
            match.HomeTeamName = Convert.ToString(dr["HomeTeamName"]);
            match.AwayTeamName = Convert.ToString(dr["AwayTeamName"]);
            match.IsHomeWin = Convert.ToBoolean(dr["IsHomeWin"]);
            match.IsAwayWin = Convert.ToBoolean(dr["IsAwayWin"]);
            match.HomeScore = Convert.ToString(dr["HomeScore"]);
            match.AwayScore = Convert.ToString(dr["AwayScore"]);
            match.ScoreInfoTypeId = Convert.ToInt16(dr["ScoreInfoTypeId"]);
            return match;
        }
        #endregion
    }
}