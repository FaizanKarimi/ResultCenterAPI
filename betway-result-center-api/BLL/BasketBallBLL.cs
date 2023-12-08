using betway_result_center_api.Models;
using betway_result_center_api.Models.DatabaseModels;
using betway_result_center_api.Models.DatabaseModels.BasketBall;
using betway_result_center_api.Models.Models.BasketBall;
using betway_result_center_api.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace betway_result_center_api.BLL
{
    public class BasketBallBLL
    {
        #region Public Methods
        public static BasketBallModel GetBasketBallMatchListByDate(GlobalParametersModel globalParametersModel)
        {
            int pageNumber = globalParametersModel.PageIndex;
            int pageSize = globalParametersModel.PageSize;
            BasketBallModel basketBallMatches = new BasketBallModel();
            List<BasketBallMatchesModel> basketBallMatchesModel = DBManager.Execute<BasketBallMatchesModel>("Betway_BasketBall_Matches",
                new
                {
                    SportId = globalParametersModel.SportId,
                    FromDate = globalParametersModel.FromDate,
                    ToDate = globalParametersModel.FromDate.AddDays(1),
                    LanguageCode = globalParametersModel.LanguageCode
                });
            if (basketBallMatchesModel != null && basketBallMatchesModel.Count > 0)
            {
                basketBallMatches = (from _result in basketBallMatchesModel.ToList()
                                     group _result by new
                                     {
                                         _result.ContestGroupId
                                     } into result
                                     select new BasketBallModel
                                     {
                                         BasketballContestGroups = (from _contestGroup in basketBallMatchesModel.ToList()
                                                                    group _contestGroup by new
                                                                    {
                                                                        _contestGroup.ContestGroupId
                                                                    } into contestGroups
                                                                    select new BaeketBallContestGroup
                                                                    {
                                                                        ContestGroupId = contestGroups.Key.ContestGroupId,
                                                                        ContestGroupName = contestGroups.FirstOrDefault(x => x.ContestGroupId == contestGroups.Key.ContestGroupId)?.ContestGroupName,
                                                                        CountryId = contestGroups.FirstOrDefault(x => x.ContestGroupId == contestGroups.Key.ContestGroupId)?.CountryId,
                                                                        CountryName = contestGroups.FirstOrDefault(x => x.ContestGroupId == contestGroups.Key.ContestGroupId)?.CountryName,
                                                                        BasketballMatches = (from _match in basketBallMatchesModel.ToList()
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
                                                                                             select new BasketBallMatch
                                                                                             {
                                                                                                 MatchId = match.Key.MatchId,
                                                                                                 MatchDate = match.Key.MatchDate,
                                                                                                 MatchStatusId = match.Key.MatchStatusId,
                                                                                                 MatchStatus = match.Key.MatchStatus,
                                                                                                 HomeTeamId = match.Key.HomeTeamId,
                                                                                                 HomeTeamName = match.Key.HomeTeamName,
                                                                                                 AwayTeamId = match.Key.AwayTeamId,
                                                                                                 AwayTeamName = match.Key.AwayTeamName,
                                                                                                 HomeTeamWin = match.FirstOrDefault(x => x.ScoreInfoTypeId == 5 || x.ScoreInfoTypeId == 23) != null ? match.FirstOrDefault(x => x.ScoreInfoTypeId == 5 || x.ScoreInfoTypeId == 23).HomeTeamWin : false,
                                                                                                 AwayTeamWin = match.FirstOrDefault(x => x.ScoreInfoTypeId == 5 || x.ScoreInfoTypeId == 23) != null ? match.FirstOrDefault(x => x.ScoreInfoTypeId == 5 || x.ScoreInfoTypeId == 23).AwayTeamWin : false,
                                                                                                 BasketballMatchScores = (from _matchScore in basketBallMatchesModel.ToList()
                                                                                                                          where _matchScore.MatchId == match.Key.MatchId
                                                                                                                          group _matchScore by new
                                                                                                                          {
                                                                                                                              _matchScore.MatchscoreId,
                                                                                                                              _matchScore.ScoreInfoTypeId,
                                                                                                                              _matchScore.HomeScore,
                                                                                                                              _matchScore.AwayScore
                                                                                                                          } into matchScores
                                                                                                                          select new BasketBallMatchScores
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
                basketBallMatches.BasketballContestGroups = basketBallMatches.BasketballContestGroups.Skip(pageNumber * pageSize).Take(pageSize).ToList();
            }
            return basketBallMatches;
        }

        public static List<BasketballStanding> GetBasketballStandings(GlobalParametersModel globalParameterModel)
        {
            List<BasketballStandingModel> basketballStandingModel = DBManager.Execute<BasketballStandingModel>("Betway_BasketBall_GetStandings_By_ContestId",
                new
                {
                    ContestGroupId = globalParameterModel.ContestGroupId
                });
            List<BasketballStanding> basketBallStanding = new List<BasketballStanding>();
            if (basketballStandingModel != null && basketballStandingModel.Count > 0)
            {
                BasketballStanding _topteamStanding = new BasketballStanding()
                {
                    Place = basketballStandingModel.FirstOrDefault().Place,
                    Won = basketballStandingModel.FirstOrDefault().Won,
                    Lost = basketballStandingModel.FirstOrDefault().Lost
                };

                basketBallStanding = (from standing in basketballStandingModel
                                      select new BasketballStanding
                                      {
                                          TeamName = standing.TeamName,
                                          Won = standing.Won,
                                          Lost = standing.Lost,
                                          Pct = Math.Round(Convert.ToDouble(standing.Won) / Convert.ToDouble(standing.Played), 3),
                                          PAG = Convert.ToDecimal(standing.Conceded / standing.Played),
                                          PSG = Convert.ToDecimal(standing.Scored / standing.Played),
                                          GB = _CalculateGB(standing, _topteamStanding)
                                      }).ToList();
            }
            return basketBallStanding;
        }

        public static List<CountriesListDBModel> GetbasketBallCountryList(GlobalParametersModel globalParametersModel)
        {
            List<CountriesListDBModel> countriesListModel = DBManager.Execute<CountriesListDBModel>("Betway_BasketBall_CountryList",
                new
                {
                    SportId = globalParametersModel.SportId,
                    LanguageCode = globalParametersModel.LanguageCode
                });
            return countriesListModel;
        }

        public static List<ContestGroupsListDBModel> GetBasketBallContestGroupList(GlobalParametersModel globalParametersModel)
        {
            List<ContestGroupsListDBModel> contestGroupsListDBModel = DBManager.Execute<ContestGroupsListDBModel>("Betway_Basketball_ContestGroupList",
                new
                {
                    SportId = globalParametersModel.SportId,
                    CountryId = globalParametersModel.CountryId,
                    LanguageCode = globalParametersModel.LanguageCode
                });
            return contestGroupsListDBModel;
        }

        public static List<BasketballRoundsModel> GetBasketballRoundList(GlobalParametersModel globalParameterModel)
        {
            List<BasketballRoundsModel> basketballRoundModel = DBManager.Execute<BasketballRoundsModel>("Betway_BasketBall_Rounds",
                new
                {
                    ContestGroupId = globalParameterModel.ContestGroupId
                });
            return basketballRoundModel;
        }

        public static List<BasketballStats> GetBasketballStats(GlobalParametersModel globalParameterModel)
        {
            List<BasketballStats> basketballStats = new List<BasketballStats>();
            List<BasketballStatsModel> basketballStatsModel = DBManager.Execute<BasketballStatsModel>("Betway_Basketball_Stats",
                new
                {
                    ContestId = globalParameterModel.ContestGroupId
                });
            if (basketballStatsModel != null && basketballStatsModel.Count > 0)
            {
                basketballStats = (from stats in basketballStatsModel.ToList()
                                   group stats by new { stats.MatchId } into result
                                   select new BasketballStats
                                   {
                                       MatchId = result.Key.MatchId,
                                       SeasonName = result.FirstOrDefault(r => r.MatchId == result.Key.MatchId)?.SeasonName,
                                       HomeTeamId = result.FirstOrDefault(r => r.MatchId == result.Key.MatchId).HomeTeamId,
                                       AwayTeamId = result.FirstOrDefault(r => r.MatchId == result.Key.MatchId).AwayTeamId,
                                       HomeTeamName = result.FirstOrDefault(r => r.MatchId == result.Key.MatchId)?.HomeTeamName,
                                       AwayTeamName = result.FirstOrDefault(r => r.MatchId == result.Key.MatchId)?.AwayTeamName,
                                       FinishedScoreHome = result.FirstOrDefault(r => r.MatchId == result.Key.MatchId)?.FinishedScoreHome,
                                       FinishedScoreAway = result.FirstOrDefault(r => r.MatchId == result.Key.MatchId)?.FinishedScoreAway,
                                       FinishedOTScoreHome = result.FirstOrDefault(r => r.MatchId == result.Key.MatchId)?.FinishedOTScoreHome,
                                       FinishedOTScoreAway = result.FirstOrDefault(r => r.MatchId == result.Key.MatchId)?.FinishedOTScoreAway,
                                       TotalHomeScore = _GetTotalHomeScore(result.FirstOrDefault(r => r.MatchId == result.Key.MatchId)),
                                       TotalAwayScore = _GetTotalAwayScore(result.FirstOrDefault(r => r.MatchId == result.Key.MatchId)),
                                       HomeTeamWin = _IsHomeTeamWin(result.FirstOrDefault(r => r.MatchId == result.Key.MatchId)),
                                       AwayTeamWin = _IsAwayTeamWin(result.FirstOrDefault(r => r.MatchId == result.Key.MatchId)),
                                       TotalPoints = _GetTotalHomeScore(result.FirstOrDefault(r => r.MatchId == result.Key.MatchId)) + _GetTotalAwayScore(result.FirstOrDefault(r => r.MatchId == result.Key.MatchId))
                                   }).ToList();
            }
            return basketballStats;
        }

        public static List<BasketBallMatchesByRound> GetBasketballMatchListByRoundId(GlobalParametersModel globalParameterModel)
        {
            List<BasketBallMatchesByRound> matchesByRound = new List<BasketBallMatchesByRound>();
            List<BasketballMatchesByRoundModel> matchesByRoundModel = DBManager.Execute<BasketballMatchesByRoundModel>("Betway_Basketball_Matches_By_RoundId",
                new
                {
                    ContestId = globalParameterModel.ContestGroupId,
                    ContestGroupRoundId = globalParameterModel.ContestGroupRoundId,
                });
            if (matchesByRoundModel != null && matchesByRoundModel.Count > 0)
            {
                matchesByRound = (from _match in matchesByRoundModel.ToList()
                                  group _match by new
                                  {
                                      _match.MatchId
                                  } into match
                                  select new BasketBallMatchesByRound
                                  {
                                      MatchId = match.Key.MatchId,
                                      MatchDate = match.FirstOrDefault(x => x.MatchId == match.Key.MatchId).MatchDate,
                                      ContestGroupRoundId = match.FirstOrDefault(x => x.MatchId == match.Key.MatchId).ContestGroupRoundId,
                                      MatchStatusId = match.FirstOrDefault(x => x.MatchId == match.Key.MatchId).MatchStatusId,
                                      HomeTeamId = match.FirstOrDefault(x => x.MatchId == match.Key.MatchId).HomeTeamId,
                                      AwayTeamId = match.FirstOrDefault(x => x.MatchId == match.Key.MatchId).AwayTeamId,
                                      HomeTeamName = match.FirstOrDefault(x => x.MatchId == match.Key.MatchId).HomeTeamName,
                                      AwayTeamName = match.FirstOrDefault(x => x.MatchId == match.Key.MatchId).AwayTeamName,
                                      HomeTeamWin = match.FirstOrDefault(x => x.ScoreInfoTypeId == 5 || x.ScoreInfoTypeId == 23) != null ? match.FirstOrDefault(x => x.ScoreInfoTypeId == 5 || x.ScoreInfoTypeId == 23).HomeTeamWin : false,
                                      AwayTeamWin = match.FirstOrDefault(x => x.ScoreInfoTypeId == 5 || x.ScoreInfoTypeId == 23) != null ? match.FirstOrDefault(x => x.ScoreInfoTypeId == 5 || x.ScoreInfoTypeId == 23).AwayTeamWin : false,
                                      BasketballMatchScores = (from _matchScore in matchesByRoundModel.ToList()
                                                               where _matchScore.MatchId == match.Key.MatchId
                                                               group _matchScore by new
                                                               {
                                                                   _matchScore.MatchscoreId,
                                                                   _matchScore.ScoreInfoTypeId,
                                                                   _matchScore.HomeScore,
                                                                   _matchScore.AwayScore
                                                               } into matchScores
                                                               select new BasketBallMatchScores
                                                               {
                                                                   MatchscoreId = matchScores.Key.MatchscoreId,
                                                                   ScoreInfoTypeId = matchScores.Key.ScoreInfoTypeId,
                                                                   HomeScore = matchScores.Key.HomeScore,
                                                                   AwayScore = matchScores.Key.AwayScore,
                                                               }).ToList()
                                  }).ToList();
            }
            return matchesByRound;
        }


        public static List<BasketballContestTeamModel> GetBasketballContestTeamList(GlobalParametersModel globalParametersModel)
        {
            List<BasketballContestTeamModel> BasketballContestTeamModel = DBManager.Execute<BasketballContestTeamModel>("Betway_Basketball_GetTeams_By_ContestId",
                new
                {
                    ContestGroupId = globalParametersModel.ContestGroupId
                });
            return BasketballContestTeamModel;
        }

        public static BasketballTotalH2H GetBasketballHeadToHead(GlobalParametersModel globalParameterModel)
        {
            BasketballTotalH2H basketballheadtoHead = DBManager.Execute<BasketballHeadtoHeadModel, Basketballh2h, BasketballTotalH2H>("Betway_Basketball_HeadToHead",
                new
                {
                    ContestGroupId = globalParameterModel.ContestGroupId,
                    homeTeamId = globalParameterModel.HomeTeamId,
                    awayTeamId = globalParameterModel.AwayTeamId
                });
            return basketballheadtoHead;
        }

        public static BasketballMatchInfoWithVenue GetBasketballMatchInfo(GlobalParametersModel globalParameterModel)
        {
            BasketballMatchInfoWithVanueModel basketballmatchinfo = DBManager.Execute<BasketballMatchInfoModel, BasketballMatchVanueInfo, BasketballMatchInfoWithVanueModel>("Betway_BasketBall_MatchInfo",
                new
                {
                    MatchId = globalParameterModel.MatchId,
                    LanguageCode = globalParameterModel.LanguageCode
                });
            BasketballMatchInfoWithVenue basketballMatchInfoWithVenue = new BasketballMatchInfoWithVenue();
            if (basketballmatchinfo != null && basketballmatchinfo.basketballMatchInfo.Count > 0)
            {
                basketballMatchInfoWithVenue = (from _match in basketballmatchinfo.basketballMatchInfo.ToList()
                                                group _match by
                                                new
                                                {
                                                    _match.MatchId
                                                } into match
                                                select new BasketballMatchInfoWithVenue
                                                {
                                                    MatchId = match.Key.MatchId,
                                                    MatchDate = match.FirstOrDefault(m => m.MatchId == match.Key.MatchId).MatchDate,
                                                    ContestGroupName = match.FirstOrDefault(m => m.MatchId == match.Key.MatchId).ContestGroupName,
                                                    CountryId = match.FirstOrDefault(m => m.MatchId == match.Key.MatchId).CountryId,
                                                    CountryName = match.FirstOrDefault(m => m.MatchId == match.Key.MatchId).CountryName,
                                                    HomeTeamName = match.FirstOrDefault(m => m.MatchId == match.Key.MatchId).HomeTeamName,
                                                    AwayTeamName = match.FirstOrDefault(m => m.MatchId == match.Key.MatchId).AwayTeamName,
                                                    MatchStatus = match.FirstOrDefault(m => m.MatchId == match.Key.MatchId).MatchStatus,
                                                    MatchStatusId = match.FirstOrDefault(m => m.MatchId == match.Key.MatchId).MatchStatusId,
                                                    BasketballMatchScores = (from _matchScore in basketballmatchinfo.basketballMatchInfo.ToList()

                                                                             group _matchScore by new
                                                                             {
                                                                                 _matchScore.ScoreInfoTypeId,
                                                                                 _matchScore.HomeScore,
                                                                                 _matchScore.AwayScore
                                                                             } into matchScores
                                                                             select new BasketBallMatchScores
                                                                             {
                                                                                 ScoreInfoTypeId = matchScores.Key.ScoreInfoTypeId,
                                                                                 HomeScore = matchScores.Key.HomeScore,
                                                                                 AwayScore = matchScores.Key.AwayScore
                                                                             }).ToList(),
                                                    MatchVenue = (from _venue in basketballmatchinfo.basketballMatchVanue.ToList()
                                                                  group _venue by new
                                                                  {
                                                                      _venue.VenueName,
                                                                      _venue.Attendence
                                                                  } into venue
                                                                  select new BasketballMatchVanueInfo
                                                                  {
                                                                      VenueName = venue.Key.VenueName,
                                                                      Attendence = venue.Key.Attendence
                                                                  }).FirstOrDefault(),
                                                }).FirstOrDefault();
            }

            return basketballMatchInfoWithVenue;
        }
        #endregion

        #region Private Methods
        private static string _CalculateGB(BasketballStandingModel standing, BasketballStanding topteamStanding)
        {
            string gb = string.Empty;
            if (standing.Place == topteamStanding.Place)
                gb = "-";
            else
                gb = Convert.ToString(((topteamStanding.Won - standing.Won) + (standing.Lost - topteamStanding.Lost)) / 2);
            return gb;
        }

        private static int _GetTotalHomeScore(BasketballStatsModel model)
        {
            int outFinishedScoreHome;
            int.TryParse(model.FinishedScoreHome, out outFinishedScoreHome);
            int outFinishedOTScoreHome;
            int.TryParse(model.FinishedOTScoreHome, out outFinishedOTScoreHome);
            return outFinishedScoreHome + outFinishedOTScoreHome;
        }

        private static int _GetTotalAwayScore(BasketballStatsModel model)
        {
            int outFinishedScoreAway;
            int.TryParse(model.FinishedScoreAway, out outFinishedScoreAway);
            int outFinishedOTScoreAway;
            int.TryParse(model.FinishedOTScoreAway, out outFinishedOTScoreAway);
            return outFinishedScoreAway + outFinishedOTScoreAway;
        }

        private static bool _IsHomeTeamWin(BasketballStatsModel model)
        {
            return _GetTotalHomeScore(model) > _GetTotalAwayScore(model);
        }

        private static bool _IsAwayTeamWin(BasketballStatsModel model)
        {
            return _GetTotalAwayScore(model) > _GetTotalHomeScore(model);
        }
        #endregion
    }
}