using betway_result_center_api.Models;
using betway_result_center_api.Models.DatabaseModels;
using betway_result_center_api.Models.DatabaseModels.Football;
using betway_result_center_api.Models.Models.Football;
using betway_result_center_api.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;

namespace betway_result_center_api.BLL
{
    public class FootballBLL
    {
        private static string _connectionString = SqlConnectionConfig.getConnectionString();

        #region Public Methods
        public static CountriesListModel GetCountriesList(GlobalParametersModel globalParametersModel)
        {
            List<CountriesListDBModel> countriesListDBModel = DBManager.Execute<CountriesListDBModel>("BetwayAPI_GetCountryList",
              new
              {
                  SportId = globalParametersModel.SportId,
                  LanguageCode = globalParametersModel.LanguageCode
              });
            CountriesListModel countriesListModel = new CountriesListModel();

            #region ADO.Net Code
            // List<CountriesListDBModel> countriesListDBModel = new List<CountriesListDBModel>();

            //using (DataTable _dataTable = new DataTable())
            //{
            //    using (SqlConnection _connection = new SqlConnection(_connectionString))
            //    {
            //        _connection.Open();
            //        using (SqlCommand _command = new SqlCommand("BetwayAPI_GetCountryList", _connection))
            //        {
            //            _command.CommandType = CommandType.StoredProcedure;
            //            _command.Parameters.Add(new SqlParameter("@SportId", sportId));
            //            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(_command);
            //            sqlDataAdapter.Fill(_dataTable);
            //        }
            //    }

            //    foreach (DataRow _dataRow in _dataTable.Rows)
            //    {
            //        CountriesListDBModel _countriesListDBModel = new CountriesListDBModel()
            //        {
            //            CountryId = Convert.ToInt16(_dataRow["Countryid"]),
            //            CountryName = _dataRow["CountryName"].ToString()
            //        };
            //        countriesListDBModel.Add(_countriesListDBModel);
            //    }
            //} 
            #endregion

            if (countriesListDBModel != null && countriesListDBModel.Count > 0)
            {
                countriesListModel = (from _sport in countriesListDBModel.ToList()
                                      group _sport by new { _sport.CountryId, _sport.CountryName } into sport
                                      select new CountriesListModel
                                      {
                                          Countries = (from _country in countriesListDBModel.ToList()
                                                       group _country by new { _country.CountryId, _country.CountryName } into country
                                                       select new Countrieslist
                                                       {
                                                           CountryId = country.Key.CountryId,
                                                           CountryName = country.Key.CountryName
                                                       }).ToList()
                                      }).FirstOrDefault();
            }
            return countriesListModel;
        }

        public static ContestGroupsListModel GetContestGroupsBycountry(GlobalParametersModel globalParametersModel)
        {
            List<ContestGroupsListDBModel> contestGroupsListDBModel = DBManager.Execute<ContestGroupsListDBModel>("BetWayAPI_GetContestGroupList",
             new
             {
                 SportId = globalParametersModel.SportId,
                 CountryId = globalParametersModel.CountryId,
                 LanguageCode = globalParametersModel.LanguageCode
             });
            ContestGroupsListModel contestGroupsListModel = new ContestGroupsListModel();

            #region  ADO.Net Code
            // List<ContestGroupsListDBModel> contestGroupsListDBModel = new List<ContestGroupsListDBModel>();

            //using (DataTable _dataTable = new DataTable())
            //{
            //    using (SqlConnection _connection = new SqlConnection(_connectionString))
            //    {
            //        _connection.Open();
            //        using (SqlCommand _command = new SqlCommand("BetWayAPI_GetContestGroupList", _connection))
            //        {
            //            _command.CommandType = CommandType.StoredProcedure;
            //            _command.Parameters.Add(new SqlParameter("@SportId", sportId));
            //            _command.Parameters.Add(new SqlParameter("@CountryId", countryId));
            //            SqlDataAdapter sqlDataApdapter = new SqlDataAdapter(_command);
            //            sqlDataApdapter.Fill(_dataTable);
            //        }
            //    }

            //    foreach (DataRow _dataRow in _dataTable.Rows)
            //    {
            //        ContestGroupsListDBModel _contestGroupsListDBModel = new ContestGroupsListDBModel()
            //        {
            //            ContestGroupId = Convert.ToInt32(_dataRow["ContestGroupId"]),
            //            ContestGroupName = _dataRow["ContestGroupName"].ToString(),
            //        };
            //        contestGroupsListDBModel.Add(_contestGroupsListDBModel);
            //    }
            //}
            #endregion

            if (contestGroupsListDBModel != null && contestGroupsListDBModel.Count > 0)
            {
                contestGroupsListModel = (from _contestGroup in contestGroupsListDBModel.ToList()
                                          group _contestGroup by new { _contestGroup.ContestGroupName, _contestGroup.ContestGroupId } into contestGroup
                                          select new ContestGroupsListModel
                                          {
                                              ContestGroups = (from _contestGroup in contestGroupsListDBModel.ToList()
                                                               select new ContestGroupList
                                                               {
                                                                   ContestGroupId = _contestGroup.ContestGroupId,
                                                                   ContestGroupName = _contestGroup.ContestGroupName,
                                                               }).ToList()
                                          }).FirstOrDefault();
            }
            return contestGroupsListModel;
        }

        public static ContestHead2HeadModel GetCompetitorsHeadToHead(GlobalParametersModel globalParametersModel)
        {
            ContestHeadToHeadDBModel contestHeadtoHeadDbModel = DBManager.Execute<FootBallLeagueTableDBModel, FootBallLastFifteenMatchesDBModel, FootBallLastFifteenMatchesDBModel, FootBallHead2HeadMatchDetailDBModel, FootBallTeamsStatsModelDBModel, FootBallTeamsStatsModelDBModel, ContestHeadToHeadDBModel>("GoalccAPI_Head2HeadAndLastTeamsMatchesData",
             new
             {
                 ContestGroupId = globalParametersModel.ContestGroupId,
                 HomeTeamId = globalParametersModel.HomeTeamId,
                 AwayTeamId = globalParametersModel.AwayTeamId,
                 LanguageCode = globalParametersModel.LanguageCode
             });
            ContestHead2HeadModel model = new ContestHead2HeadModel();

            #region Entity FrameWork
            //responseStatus = "Cache";
            //ContestHead2HeadModel cacheModel = HttpRuntime.Cache["h2h-ha-matches-" + contestId + "-" + homeTeamId + "-" + awayTeamId] as ContestHead2HeadModel;

            //if (cacheModel == null)
            //{

            //  var leagueTable = new List<LeagueTableDBModel>();
            //    var head2Head = new List<Head2HeadMatchDetailDBModel>();
            //    var homeTeamMatches = new List<Head2HeadMatchDetailDBModel>();
            //    var awayTeamMatches = new List<Head2HeadMatchDetailDBModel>();
            //    var homeTeamStats = new List<TeamsStatsDBModel>();
            //    var awayTeamStats = new List<TeamsStatsDBModel>();
            //    var db = new SportsDataPanelEntities();
            //    using (var connection = db.Database.Connection)
            //    {
            //        connection.Open();
            //        var command = connection.CreateCommand();
            //        command.CommandText = "EXEC [dbo].[GoalccAPI_Head2HeadAndLastTeamsMatchesData]'" + contestId + "','" + homeTeamId + "','" + awayTeamId + "'";

            //        using (var reader = command.ExecuteReader())
            //        {
            //            leagueTable = ((IObjectContextAdapter)db)
            //                            .ObjectContext
            //                            .Translate<LeagueTableDBModel>(reader)
            //                            .ToList();

            //            reader.NextResult();

            //            homeTeamMatches = ((IObjectContextAdapter)db)
            //                                .ObjectContext
            //                                .Translate<Head2HeadMatchDetailDBModel>(reader)
            //                                .ToList();

            //            reader.NextResult();

            //            awayTeamMatches = ((IObjectContextAdapter)db)
            //                                .ObjectContext
            //                                .Translate<Head2HeadMatchDetailDBModel>(reader)
            //                                .ToList();

            //            reader.NextResult();

            //            head2Head = ((IObjectContextAdapter)db)
            //                            .ObjectContext
            //                            .Translate<Head2HeadMatchDetailDBModel>(reader)
            //                            .ToList();

            //            reader.NextResult();

            //            homeTeamStats = ((IObjectContextAdapter)db)
            //                                .ObjectContext
            //                                .Translate<TeamsStatsDBModel>(reader)
            //                                .ToList();

            //            reader.NextResult();

            //            awayTeamStats = ((IObjectContextAdapter)db)
            //                                .ObjectContext
            //                                .Translate<TeamsStatsDBModel>(reader)
            //                                .ToList();
            //            reader.Close();
            //        }
            //        command.Dispose();
            //        connection.Close();
            //        connection.Dispose();
            //    } 
            #endregion

            #region Table
            model.LeagueTable = (from _league in contestHeadtoHeadDbModel.LeagueTeble.ToList()
                                 select new LeagueTableModel
                                 {
                                     LeagueTableId = _league.LeagueTableId,
                                     ContestGroupId = _league.ContestGroupId,
                                     SeasonId = _league.SeasonId,
                                     LeagueCompetitors = (from _competitor in contestHeadtoHeadDbModel.LeagueTeble.ToList()
                                                          where _competitor.LeagueTableId == _league.LeagueTableId
                                                          group _competitor by new { _competitor.LeagueTableCompetitorId, _competitor.TeamId, _competitor.TeamName, _competitor.Place } into competitor
                                                          select new LeagueTableCompetitorModel
                                                          {
                                                              LeagueTableCompetitorId = competitor.Key.LeagueTableCompetitorId,
                                                              TeamId = competitor.Key.TeamId,
                                                              Team = competitor.Key.TeamName,
                                                              Place = competitor.Key.Place,
                                                              LeagueTablesMatches = (from _leagueTableMatch in contestHeadtoHeadDbModel.LeagueTeble.ToList()
                                                                                     where _leagueTableMatch.LeagueTableCompetitorId == competitor.Key.LeagueTableCompetitorId
                                                                                     select new LeagueTableMatchesModel
                                                                                     {
                                                                                         LeagueTableMatchId = _leagueTableMatch.LeagueTableMatchId,
                                                                                         Played = _leagueTableMatch.Played,
                                                                                         Won = _leagueTableMatch.Won,
                                                                                         Lost = _leagueTableMatch.Lost,
                                                                                         Draws = _leagueTableMatch.Draws,
                                                                                         Scored = _leagueTableMatch.Scored,
                                                                                         Conceded = _leagueTableMatch.Conceded,
                                                                                         Difference = _leagueTableMatch.Difference,
                                                                                         Points = _leagueTableMatch.Points,
                                                                                         Type = _leagueTableMatch.Type
                                                                                     }).ToList()
                                                          }).OrderBy(lc => lc.Place).ToList()
                                 }).FirstOrDefault();
            if (model.LeagueTable == null)
            {
                model.LeagueTable = new LeagueTableModel();
            }
            #endregion

            #region Last 15 Home Team Matches
            model.LastFifteenHomeMatches = (from _match in contestHeadtoHeadDbModel.LastFifteenHomeMatches
                                            select new LastFifteenMatchesModel
                                            {
                                                MatchId = _match.MatchId,
                                                MatchDate = _match.MatchDate.ToString("MM/dd/yyyy"),
                                                MatchTime = _match.MatchDate.ToString("HH:mm"),
                                                HomeTeamId = _match.HomeTeamId,
                                                HomeTeam = _match.HomeTeam,
                                                HomeTeamShortName = !string.IsNullOrEmpty(_match.HomeTeamShortName) ?
                                                                        _match.HomeTeamShortName : _match.HomeTeam,
                                                AwayTeamId = _match.AwayTeamId,
                                                AwayTeam = _match.AwayTeam,
                                                AwayTeamShortName = !string.IsNullOrEmpty(_match.AwayTeamShortName) ?
                                                                        _match.AwayTeamShortName : _match.AwayTeam,
                                                HomeScore = _match.HomeScore,
                                                AwayScore = _match.AwayScore,
                                                CountryId = _match.CountryId,
                                                CountryName = _match.CountryName,
                                                ContestGroupId = _match.ContestGroupId,
                                                ContestGroupName = _match.ContestGroupName
                                            }).ToList();
            #endregion

            #region Last 15 Away Team Matches
            model.LastFifteenAwayMatches = (from _match in contestHeadtoHeadDbModel.LastFifteenAwayMatches
                                            select new LastFifteenMatchesModel
                                            {
                                                MatchId = _match.MatchId,
                                                MatchDate = _match.MatchDate.ToString("MM/dd/yyyy"),
                                                MatchTime = _match.MatchDate.ToString("HH:mm"),
                                                HomeTeamId = _match.HomeTeamId,
                                                HomeTeam = _match.HomeTeam,
                                                HomeTeamShortName = !string.IsNullOrEmpty(_match.HomeTeamShortName) ?
                                                                        _match.HomeTeamShortName : _match.HomeTeam,
                                                AwayTeamId = _match.AwayTeamId,
                                                AwayTeam = _match.AwayTeam,
                                                AwayTeamShortName = !string.IsNullOrEmpty(_match.AwayTeamShortName) ?
                                                                        _match.AwayTeamShortName : _match.AwayTeam,
                                                HomeScore = _match.HomeScore,
                                                AwayScore = _match.AwayScore,
                                                CountryId = _match.CountryId,
                                                CountryName = _match.CountryName,
                                                ContestGroupId = _match.ContestGroupId,
                                                ContestGroupName = _match.ContestGroupName
                                            }).ToList();
            #endregion

            #region Head 2 Head Matches
            model.Head2HeadMatches = (from _match in contestHeadtoHeadDbModel.Head2HeadMatches
                                      select new Head2HeadMatchDetailModel
                                      {
                                          MatchId = _match.MatchId,
                                          MatchDate = _match.MatchDate.ToString("MM/dd/yyyy"),
                                          MatchTime = _match.MatchDate.ToString("HH:mm"),
                                          HomeTeamId = _match.HomeTeamId,
                                          HomeTeam = _match.HomeTeam,
                                          HomeTeamShortName = !string.IsNullOrEmpty(_match.HomeTeamShortName) ?
                                                                        _match.HomeTeamShortName : _match.HomeTeam,
                                          AwayTeamId = _match.AwayTeamId,
                                          AwayTeam = _match.AwayTeam,
                                          AwayTeamShortName = !string.IsNullOrEmpty(_match.AwayTeamShortName) ?
                                                                        _match.AwayTeamShortName : _match.AwayTeam,
                                          HomeScore = _match.HomeScore,
                                          AwayScore = _match.AwayScore,
                                          CountryId = _match.CountryId,
                                          CountryName = _match.CountryName,
                                          ContestGroupId = _match.ContestGroupId,
                                          ContestGroupName = _match.ContestGroupName,
                                          SeasonId = _match.SeasonId,
                                          SeasonName = _match.SeasonName
                                      }).ToList();
            #endregion

            #region Home Team Stats
            model.HomeTeamStatsMarkets = (from _teamStat in contestHeadtoHeadDbModel.homeTeamStats
                                          select new ContestTeamsStatsModel
                                          {
                                              TeamId = _teamStat.TeamId,
                                              TeamName = _teamStat.TeamName,
                                              MatchesPlayed = _teamStat.MatchesPlayed,
                                              MarketId = _teamStat.MarketId,
                                              MarketName = _teamStat.MarketName,
                                              Position = _teamStat.Position + (_teamStat.Position == 1 ? "st" : _teamStat.Position == 2 ? "nd" : _teamStat.Position == 3 ? "rd" : "th"),
                                              Percentage = _teamStat.Percentage
                                          }).ToList();
            #endregion

            #region Away Team Stats
            model.AwayTeamStatsMarkets = (from _teamStat in contestHeadtoHeadDbModel.awayTeamStats
                                          select new ContestTeamsStatsModel
                                          {
                                              TeamId = _teamStat.TeamId,
                                              TeamName = _teamStat.TeamName,
                                              MatchesPlayed = _teamStat.MatchesPlayed,
                                              MarketId = _teamStat.MarketId,
                                              MarketName = _teamStat.MarketName,
                                              Position = _teamStat.Position + (_teamStat.Position == 1 ? "st" : _teamStat.Position == 2 ? "nd" : _teamStat.Position == 3 ? "rd" : "th"),
                                              Percentage = _teamStat.Percentage
                                          }).ToList();
            #endregion

            #region Cache
            //    cacheModel = model;

            //    HttpRuntime.Cache.Insert("h2h-ha-matches-" + contestId + "-" + homeTeamId + "-" + awayTeamId,
            //        cacheModel, null, DateTime.Now.AddMinutes(15), System.Web.Caching.Cache.NoSlidingExpiration,
            //                System.Web.Caching.CacheItemPriority.Default, null);

            //    responseStatus = "Database";
            //}

            //return cacheModel; 
            #endregion

            return model;
        }

        public static MatchDetailBetwayModel GetMatchDetail(GlobalParametersModel globalParametersModel)
        {
            MatchDetailBetwayModel matchDetailBetwayDBModel = new MatchDetailBetwayModel();
            var dbModel = _GetMatchDetailForBetWayFromDBByMatchId(globalParametersModel);

            #region Last15HomeAway
            if (dbModel.LastestFifteenMatchesAwayTeamDB != null)
            {
                matchDetailBetwayDBModel.LastFifteenAwayTeamMatches = dbModel.LastestFifteenMatchesAwayTeamDB.GroupBy(x => x.MatchId).Select(_matchDetail => new LastFifteenMatchesDetailModel()
                {
                    MatchId = _matchDetail.Key,
                    AwayTeam = _matchDetail.FirstOrDefault(z => z.MatchId == _matchDetail.Key).AwayTeam,
                    MatchDate = Convert.ToString(_matchDetail.FirstOrDefault(z => z.MatchId == _matchDetail.Key).MatchDate),
                    AwayScore = _matchDetail.FirstOrDefault(z => z.MatchId == _matchDetail.Key).AwayScore,
                    ContestGroupId = _matchDetail.FirstOrDefault(z => z.MatchId == _matchDetail.Key).ContestGroupId,
                    ContestGroupName = _matchDetail.FirstOrDefault(z => z.MatchId == _matchDetail.Key).ContestGroupName,
                    AwayTeamId = _matchDetail.FirstOrDefault(z => z.MatchId == _matchDetail.Key).AwayTeamId,
                    AwayTeamShortName = _matchDetail.FirstOrDefault(z => z.MatchId == _matchDetail.Key).AwayTeamShortName,
                    HomeScore = _matchDetail.FirstOrDefault(z => z.MatchId == _matchDetail.Key).HomeScore,
                    HomeTeamId = _matchDetail.FirstOrDefault(z => z.MatchId == _matchDetail.Key).HomeTeamId,
                    HomeTeam = _matchDetail.FirstOrDefault(z => z.MatchId == _matchDetail.Key).HomeTeam,
                    HomeTeamShortName = _matchDetail.FirstOrDefault(z => z.MatchId == _matchDetail.Key).HomeTeamShortName,
                    CountryId = Convert.ToInt16(_matchDetail.FirstOrDefault(z => z.MatchId == _matchDetail.Key).CountryId),
                    CountryName = _matchDetail.FirstOrDefault(z => z.MatchId == _matchDetail.Key).CountryName,
                }).Take(15).ToList();
            }

            if (dbModel.LastestFifteenMatchesHomeTeamDB != null)
            {
                matchDetailBetwayDBModel.LastFifteenHomeTeamMatches = dbModel.LastestFifteenMatchesHomeTeamDB.GroupBy(x => x.MatchId).Select(_matchDetial => new LastFifteenMatchesDetailModel()
                {
                    MatchId = _matchDetial.Key,
                    AwayTeam = _matchDetial.FirstOrDefault(z => z.MatchId == _matchDetial.Key).AwayTeam,
                    MatchDate = Convert.ToString(_matchDetial.FirstOrDefault(z => z.MatchId == _matchDetial.Key).MatchDate),
                    AwayScore = _matchDetial.FirstOrDefault(z => z.MatchId == _matchDetial.Key).AwayScore,
                    ContestGroupId = _matchDetial.FirstOrDefault(z => z.MatchId == _matchDetial.Key).ContestGroupId,
                    ContestGroupName = _matchDetial.FirstOrDefault(z => z.MatchId == _matchDetial.Key).ContestGroupName,
                    AwayTeamId = _matchDetial.FirstOrDefault(z => z.MatchId == _matchDetial.Key).AwayTeamId,
                    AwayTeamShortName = _matchDetial.FirstOrDefault(z => z.MatchId == _matchDetial.Key).AwayTeamShortName,
                    HomeScore = _matchDetial.FirstOrDefault(z => z.MatchId == _matchDetial.Key).HomeScore,
                    HomeTeamId = _matchDetial.FirstOrDefault(z => z.MatchId == _matchDetial.Key).HomeTeamId,
                    HomeTeam = _matchDetial.FirstOrDefault(z => z.MatchId == _matchDetial.Key).HomeTeam,
                    HomeTeamShortName = _matchDetial.FirstOrDefault(z => z.MatchId == _matchDetial.Key).HomeTeamShortName,
                    CountryId = Convert.ToInt16(_matchDetial.FirstOrDefault(z => z.MatchId == _matchDetial.Key).CountryId),
                    CountryName = _matchDetial.FirstOrDefault(z => z.MatchId == _matchDetial.Key).CountryName,
                }).Take(15).ToList();
            }
            #endregion

            #region Match Score Info
            if (dbModel.MatchScoreInfoDB != null)
            {
                matchDetailBetwayDBModel.MatchId = dbModel.MatchScoreInfoDB.MatchId;
                matchDetailBetwayDBModel.OddsMatchId = dbModel.MatchScoreInfoDB.OddsMatchId;
                matchDetailBetwayDBModel.MatchDate = dbModel.MatchScoreInfoDB.MatchDate.ToString("MM/dd/yyyy");
                matchDetailBetwayDBModel.MatchTime = dbModel.MatchScoreInfoDB.MatchDate.ToString("HH:mm");
                matchDetailBetwayDBModel.MatchStatusId = dbModel.MatchScoreInfoDB.MatchStatusId;
                matchDetailBetwayDBModel.MatchStatus = dbModel.MatchScoreInfoDB.MatchStatus;
                matchDetailBetwayDBModel.CurrentMinutes = (!string.IsNullOrEmpty(dbModel.MatchScoreInfoDB.CurrentMinutes.ToString()) ?
                                        (dbModel.MatchScoreInfoDB.MinutePlusBit == true ?
                                            dbModel.MatchScoreInfoDB.CurrentMinutes + "+" + (!string.IsNullOrEmpty(dbModel.MatchScoreInfoDB.PlusMinutes) ? dbModel.MatchScoreInfoDB.PlusMinutes : string.Empty) : dbModel.MatchScoreInfoDB.CurrentMinutes.ToString())
                                            : "0");
                matchDetailBetwayDBModel.CountryId = dbModel.MatchScoreInfoDB.CountryId;
                matchDetailBetwayDBModel.CountryName = dbModel.MatchScoreInfoDB.CountryName;
                matchDetailBetwayDBModel.ContestGroupId = dbModel.MatchScoreInfoDB.ContestGroupId;
                matchDetailBetwayDBModel.ContestGroupName = dbModel.MatchScoreInfoDB.ContestGroupName;
                matchDetailBetwayDBModel.ContestGroupShortName = !string.IsNullOrEmpty(dbModel.MatchScoreInfoDB.ContestGroupShortName) ?
                                                dbModel.MatchScoreInfoDB.ContestGroupShortName : dbModel.MatchScoreInfoDB.ContestGroupName;
                matchDetailBetwayDBModel.HomeTeamId = dbModel.MatchScoreInfoDB.HomeTeamId;
                matchDetailBetwayDBModel.HomeTeam = dbModel.MatchScoreInfoDB.HomeTeam;
                matchDetailBetwayDBModel.HomeTeamShortName = !string.IsNullOrEmpty(dbModel.MatchScoreInfoDB.HomeTeamShortName)
                                                ? dbModel.MatchScoreInfoDB.HomeTeamShortName : dbModel.MatchScoreInfoDB.HomeTeam;
                matchDetailBetwayDBModel.AwayTeamId = dbModel.MatchScoreInfoDB.AwayTeamId;
                matchDetailBetwayDBModel.AwayTeam = dbModel.MatchScoreInfoDB.AwayTeam;
                matchDetailBetwayDBModel.AwayTeamShortName = !string.IsNullOrEmpty(dbModel.MatchScoreInfoDB.AwayTeamShortName)
                                                ? dbModel.MatchScoreInfoDB.AwayTeamShortName : dbModel.MatchScoreInfoDB.AwayTeam;
                matchDetailBetwayDBModel.HalfTimeScore = !string.IsNullOrEmpty(dbModel.MatchScoreInfoDB.HalfTimeScore) ? dbModel.MatchScoreInfoDB.HalfTimeScore : string.Empty;
                matchDetailBetwayDBModel.HTHomeScore = !string.IsNullOrEmpty(dbModel.MatchScoreInfoDB.HalfTimeScore) ? dbModel.MatchScoreInfoDB.HalfTimeScore.Replace(" ", string.Empty).Split('-').First() : string.Empty;
                matchDetailBetwayDBModel.HTAwayScore = !string.IsNullOrEmpty(dbModel.MatchScoreInfoDB.HalfTimeScore) ? dbModel.MatchScoreInfoDB.HalfTimeScore.Replace(" ", string.Empty).Split('-').Last() : string.Empty;
                matchDetailBetwayDBModel.FullTimeScore = !string.IsNullOrEmpty(dbModel.MatchScoreInfoDB.FullTimeScore) ? dbModel.MatchScoreInfoDB.FullTimeScore : string.Empty;
                matchDetailBetwayDBModel.FTHomeScore = !string.IsNullOrEmpty(dbModel.MatchScoreInfoDB.FullTimeScore) ? dbModel.MatchScoreInfoDB.FullTimeScore.Replace(" ", string.Empty).Split('-').First() : string.Empty;
                matchDetailBetwayDBModel.FTAwayScore = !string.IsNullOrEmpty(dbModel.MatchScoreInfoDB.FullTimeScore) ? dbModel.MatchScoreInfoDB.FullTimeScore.Replace(" ", string.Empty).Split('-').Last() : string.Empty;
                matchDetailBetwayDBModel.LiveScore = !string.IsNullOrEmpty(dbModel.MatchScoreInfoDB.LiveScore) ? dbModel.MatchScoreInfoDB.LiveScore : string.Empty;
                matchDetailBetwayDBModel.LiveHomeScore = !string.IsNullOrEmpty(dbModel.MatchScoreInfoDB.LiveScore) ? dbModel.MatchScoreInfoDB.LiveScore.Replace(" ", string.Empty).Split('-').First() : string.Empty;
                matchDetailBetwayDBModel.LiveAwayScore = !string.IsNullOrEmpty(dbModel.MatchScoreInfoDB.LiveScore) ? dbModel.MatchScoreInfoDB.LiveScore.Replace(" ", string.Empty).Split('-').Last() : string.Empty;
            }
            #endregion

            #region MatchEvents
            matchDetailBetwayDBModel.MatchEvents = (from _matchEvent in dbModel.MatchDetailEventsDB.ToList()
                                                    where _matchEvent.MatchId == matchDetailBetwayDBModel.MatchId
                                                    select new MatchDetailEventsModel
                                                    {
                                                        PlayerId = _matchEvent.PlayerId,
                                                        PlayerName = (!string.IsNullOrEmpty(_matchEvent.OtherName) ?
                                                                            (_matchEvent.OtherName.Length > 18 ? _matchEvent.OtherName.Substring(0, 18) : _matchEvent.OtherName) :
                                                                            !string.IsNullOrEmpty(_matchEvent.Player) ?
                                                                                (_matchEvent.Player.Length > 18 ? _matchEvent.Player.Substring(0, 18) : _matchEvent.Player) :
                                                                                (_matchEvent.PlayerName.Length > 18 ? _matchEvent.PlayerName.Substring(0, 18) : _matchEvent.PlayerName)),
                                                        EventMinute = !string.IsNullOrEmpty(_matchEvent.EventMinute) ? _matchEvent.EventMinute : string.Empty,
                                                        HomeAway = _matchEvent.HomeAway,
                                                        EventType = _matchEvent.EventType,
                                                        EventTypeId = _matchEvent.EventTypeId,
                                                        Score = !string.IsNullOrEmpty(_matchEvent.Score) ? _matchEvent.Score : string.Empty
                                                    }).ToList();
            #endregion

            #region Home Lineup
            matchDetailBetwayDBModel.HomeTeamLineupPlayers = (from _player in dbModel.HomeTeamLinupPlayersDB.ToList()
                                                              select new TeamLinupPlayersModel
                                                              {
                                                                  TeamId = _player.TeamId,
                                                                  MatchLineupId = _player.MatchLineupId,
                                                                  MatchCompetitorId = _player.MatchCompetitorId,
                                                                  FormationId = _player.FormationId,
                                                                  Formation = _player.Formation,
                                                                  MatchLineupPlayerId = _player.MatchLineupPlayerId,
                                                                  PlayerId = _player.PlayerId,
                                                                  PlayerName = _player.PlayerName,
                                                                  PlayerPosition = !string.IsNullOrEmpty(_player.PlayerPosition) ? _player.PlayerPosition : string.Empty,
                                                                  DateOfBirth = _player.DateOfBirth == null ? string.Empty : DateTime.Parse(_player.DateOfBirth.ToString()).ToString("MM/dd/yyy"),
                                                                  ShirtNo = !string.IsNullOrEmpty(_player.ShirtNo) ? _player.ShirtNo : string.Empty,
                                                                  IsSubstitute = _player.IsSubstitute,
                                                                  CoachId = _player.CoachId == null ? _player.CoachId : 0,
                                                                  CoachName = !string.IsNullOrEmpty(_player.CoachName) ? _player.CoachName : string.Empty,
                                                                  CountryId = _player.CountryId == null ? _player.CountryId : 0,
                                                                  CountryName = !string.IsNullOrEmpty(_player.CountryName) ? _player.CountryName : string.Empty
                                                              }).ToList();

            if (matchDetailBetwayDBModel.HomeTeamLineupPlayers.Count > 0)
            {
                int rowIndex = 0;
                var playerList = matchDetailBetwayDBModel.HomeTeamLineupPlayers.Where(p => p.PlayerPosition != "gk").ToList();
                matchDetailBetwayDBModel.HomeLineUpFormation = new LineupFormationModel()
                {
                    OuterCount = matchDetailBetwayDBModel.HomeTeamLineupPlayers.FirstOrDefault().Formation.Split('-').Count(),
                    InnerCountValue = (from _innerCountValue in matchDetailBetwayDBModel.HomeTeamLineupPlayers.FirstOrDefault().Formation.Split('-').ToList()
                                       select new LineupFormationInnerCountModel
                                       {
                                           OuterCount = int.Parse(_innerCountValue),
                                       }).ToList()
                };

                foreach (var item in matchDetailBetwayDBModel.HomeLineUpFormation.InnerCountValue)
                {
                    var numberList = new List<int>();
                    for (int i = 0; i < item.OuterCount; i++)
                    {
                        numberList.Add(playerList[rowIndex].PlayerId);
                        rowIndex = rowIndex + 1;
                    }
                    item.InnerCountValue = numberList;
                }
            }
            #endregion

            #region Away Lineup
            matchDetailBetwayDBModel.AwayTeamLineupPlayers = (from _player in dbModel.AwayTeamLinupPlayersDB.ToList()
                                                              select new TeamLinupPlayersModel
                                                              {
                                                                  TeamId = _player.TeamId,
                                                                  MatchLineupId = _player.MatchLineupId,
                                                                  MatchCompetitorId = _player.MatchCompetitorId,
                                                                  FormationId = _player.FormationId,
                                                                  Formation = _player.Formation,
                                                                  MatchLineupPlayerId = _player.MatchLineupPlayerId,
                                                                  PlayerId = _player.PlayerId,
                                                                  PlayerName = _player.PlayerName,
                                                                  PlayerPosition = !string.IsNullOrEmpty(_player.PlayerPosition) ? _player.PlayerPosition : string.Empty,
                                                                  DateOfBirth = _player.DateOfBirth == null ? string.Empty : DateTime.Parse(_player.DateOfBirth.ToString()).ToString("MM/dd/yyy"),
                                                                  ShirtNo = !string.IsNullOrEmpty(_player.ShirtNo) ? _player.ShirtNo : string.Empty,
                                                                  IsSubstitute = _player.IsSubstitute,
                                                                  CoachId = _player.CoachId == null ? _player.CoachId : 0,
                                                                  CoachName = !string.IsNullOrEmpty(_player.CoachName) ? _player.CoachName : string.Empty,
                                                                  CountryId = _player.CountryId == null ? _player.CountryId : 0,
                                                                  CountryName = !string.IsNullOrEmpty(_player.CountryName) ? _player.CountryName : string.Empty
                                                              }).ToList();

            if (matchDetailBetwayDBModel.AwayTeamLineupPlayers.Count > 0)
            {
                int rowIndex = 0;
                var playerList = matchDetailBetwayDBModel.AwayTeamLineupPlayers.Where(p => p.PlayerPosition != "gk").ToList();
                matchDetailBetwayDBModel.AwayLineUpFormation = new LineupFormationModel()
                {
                    OuterCount = matchDetailBetwayDBModel.AwayTeamLineupPlayers.FirstOrDefault().Formation.Split('-').Count(),
                    InnerCountValue = (from _innerCountValue in matchDetailBetwayDBModel.AwayTeamLineupPlayers.FirstOrDefault().Formation.Split('-').ToList()
                                       select new LineupFormationInnerCountModel
                                       {
                                           OuterCount = int.Parse(_innerCountValue),
                                       }).ToList()
                };

                foreach (var item in matchDetailBetwayDBModel.AwayLineUpFormation.InnerCountValue)
                {
                    var numberList = new List<int>();
                    for (int i = 0; i < item.OuterCount; i++)
                    {
                        numberList.Add(playerList[rowIndex].PlayerId);
                        rowIndex = rowIndex + 1;
                    }
                    item.InnerCountValue = numberList;
                }
            }
            #endregion

            #region LeagueTable
            matchDetailBetwayDBModel.LeagueTable = dbModel.LeagueTable;
            #endregion

            #region ExtraTable
            //#region Match Commentary
            ////MatchCommentary
            //Int16[] audioCommentryIds = new Int16[] { 93, 94, 95, 96, 97, 98, 99 };
            //model.MatchCommentary = (from com in dbModel.MatchCommentaryDetailDB.ToList()
            //                         where !audioCommentryIds.Contains(com.CommentType)
            //                         select new MatchCommentaryDetailModel
            //                         {
            //                             MatchCommentaryId = com.MatchCommentaryId,
            //                             MatchMinute = com.MatchMinute,
            //                             CommentType = com.CommentType,
            //                             CommenTypeText = com.CommenTypeText,
            //                             Comment = com.Comment,
            //                             Duration = com.Duration,
            //                             AuthorName = com.AuthorName,
            //                             AuthorUserName = com.AuthorUserName,
            //                             AuthorPageURL = com.AuthorPageURL,
            //                             AuthorProfileImage = com.AuthorProfileImage,
            //                             MediaImageURL = !string.IsNullOrEmpty(com.MediaImageURL) ? com.MediaImageURL : string.Empty,
            //                             CurrentDate = com.CurrentDate,
            //                             ProviderId = com.ProviderId
            //                         }).ToList();
            //#endregion

            //#region Head2Head
            //model.Head2HeadMatches = (from m in dbModel.Head2HeadMatchesDB.ToList()
            //                          select new Head2HeadMatchDetailModel
            //                          {
            //                              MatchId = m.MatchId,
            //                              MatchDate = m.MatchDate.ToString("MM/dd/yyyy"),
            //                              MatchTime = m.MatchDate.ToString("HH:mm"),
            //                              HomeTeamId = m.HomeTeamId,
            //                              HomeTeam = m.HomeTeam,
            //                              HomeTeamShortName = !string.IsNullOrEmpty(m.HomeTeamShortName) ?
            //                                                    m.HomeTeamShortName : m.HomeTeam,
            //                              AwayTeamId = m.AwayTeamId,
            //                              AwayTeam = m.AwayTeam,
            //                              AwayTeamShortName = !string.IsNullOrEmpty(m.AwayTeamShortName) ?
            //                                                    m.AwayTeamShortName : m.AwayTeam,
            //                              HomeScore = m.HomeScore,
            //                              AwayScore = m.AwayScore,
            //                              CountryId = m.CountryId,
            //                              CountryName = m.CountryName,
            //                              ContestGroupId = m.ContestGroupId,
            //                              ContestGroupName = m.ContestGroupName,
            //                              SeasonId = m.SeasonId,
            //                              SeasonName = m.SeasonName
            //                          }).ToList();
            //#endregion

            //#region Home Team Matches
            //model.LastFifteenHomeTeamMatches = (from m in dbModel.LastestFifteenMatchesHomeTeamDB.ToList()
            //                                    select new LastFifteenMatchesDetailModel
            //                                    {
            //                                        MatchId = m.MatchId,
            //                                        MatchDate = m.MatchDate.ToString("MM/dd/yyyy"),
            //                                        MatchTime = m.MatchDate.ToString("HH:mm"),
            //                                        HomeTeamId = m.HomeTeamId,
            //                                        HomeTeam = m.HomeTeam,
            //                                        HomeTeamShortName = !string.IsNullOrEmpty(m.HomeTeamShortName) ?
            //                                                              m.HomeTeamShortName : m.HomeTeam,
            //                                        AwayTeamId = m.AwayTeamId,
            //                                        AwayTeam = m.AwayTeam,
            //                                        AwayTeamShortName = !string.IsNullOrEmpty(m.AwayTeamShortName) ?
            //                                                              m.AwayTeamShortName : m.AwayTeam,
            //                                        HomeScore = m.HomeScore,
            //                                        AwayScore = m.AwayScore,
            //                                        CountryId = (short)m.CountryId,
            //                                        CountryName = m.CountryName,
            //                                        ContestGroupId = m.ContestGroupId,
            //                                        ContestGroupName = m.ContestGroupName
            //                                    }).ToList();
            //#endregion

            //#region Away Team Matches
            //model.LastFifteenAwayTeamMatches = (from m in dbModel.LastestFifteenMatchesAwayTeamDB.ToList()
            //                                    select new LastFifteenMatchesDetailModel
            //                                    {
            //                                        MatchId = m.MatchId,
            //                                        MatchDate = m.MatchDate.ToString("MM/dd/yyyy"),
            //                                        MatchTime = m.MatchDate.ToString("HH:mm"),
            //                                        HomeTeamId = m.HomeTeamId,
            //                                        HomeTeam = m.HomeTeam,
            //                                        HomeTeamShortName = !string.IsNullOrEmpty(m.HomeTeamShortName) ?
            //                                                                m.HomeTeamShortName : m.HomeTeam,
            //                                        AwayTeamId = m.AwayTeamId,
            //                                        AwayTeam = m.AwayTeam,
            //                                        AwayTeamShortName = !string.IsNullOrEmpty(m.AwayTeamShortName) ?
            //                                                                m.AwayTeamShortName : m.AwayTeam,
            //                                        HomeScore = m.HomeScore,
            //                                        AwayScore = m.AwayScore,
            //                                        CountryId = (short)m.CountryId,
            //                                        CountryName = m.CountryName,
            //                                        ContestGroupId = m.ContestGroupId,
            //                                        ContestGroupName = m.ContestGroupName
            //                                    }).ToList();
            //#endregion

            //#region Match News
            //model.MatchNews = (from n in dbModel.MatchNewsDB.ToList()
            //                   select new MatchNewsDetailModel
            //                   {
            //                       NewsId = n.NewsId,
            //                       CategoryId = n.CategoryId,
            //                       CategoryName = n.CategoryName,
            //                       ModifiedDate = DateTime.Parse(n.ModifiedDate.ToString()).ToString("MM/dd/yyyy HH:mm"),
            //                       Title = n.Title,
            //                       Headline = n.Headline,
            //                       Strapline = n.Strapline,
            //                       ImageURL = System.IO.File.Exists(@"C:\inetpub\wwwroot\languages.feedsxml.com\news-images\" + n.ProviderNewsId + "-detail.jpg") ? "http://img.sixlogics.com/news-images/" + n.ProviderNewsId + "-detail.jpg" : n.ImageURL,
            //                       BodyText = n.BodyText,
            //                       ProviderNewsId = n.ProviderNewsId
            //                   }).ToList();
            //#endregion

            //#region Match Previews
            //model.MatchPreviews = model.MatchNews.Where(n => n.CategoryId == 46 || n.CategoryId == 76).ToList();
            //#endregion

            //#region Match Reports
            //model.MatchReports = model.MatchNews.Where(n => n.CategoryId == 47).ToList();
            //#endregion
            #endregion

            #region Statistics
            if (dbModel.MatchCompetitorsStatsComDetailDB.MatchId != 0)
            {
                MatchCompetitorsStatsComDetailModel stats = new MatchCompetitorsStatsComDetailModel();

                #region Corners Info
                stats.HtCorners = dbModel.MatchCompetitorsStatsComDetailDB.HtCornerKicks == null ? 0 : (int)dbModel.MatchCompetitorsStatsComDetailDB.HtCornerKicks;
                stats.AtCorners = dbModel.MatchCompetitorsStatsComDetailDB.AtCornerKicks == null ? 0 : (int)dbModel.MatchCompetitorsStatsComDetailDB.AtCornerKicks;
                stats.HtCornersPercentage = dbModel.MatchCompetitorsStatsComDetailDB.HtCornerKicks != 0 || dbModel.MatchCompetitorsStatsComDetailDB.AtCornerKicks != 0 ?
                                            (int)((dbModel.MatchCompetitorsStatsComDetailDB.HtCornerKicks * 100) / (dbModel.MatchCompetitorsStatsComDetailDB.HtCornerKicks + dbModel.MatchCompetitorsStatsComDetailDB.AtCornerKicks)) : 0;
                stats.AtCornersPercentage = dbModel.MatchCompetitorsStatsComDetailDB.HtCornerKicks != 0 || dbModel.MatchCompetitorsStatsComDetailDB.AtCornerKicks != 0 ?
                                            (int)((dbModel.MatchCompetitorsStatsComDetailDB.AtCornerKicks * 100) / (dbModel.MatchCompetitorsStatsComDetailDB.HtCornerKicks + dbModel.MatchCompetitorsStatsComDetailDB.AtCornerKicks)) : 0;
                #endregion

                #region Shots On Target
                stats.HtShotsOnTarget = dbModel.MatchCompetitorsStatsComDetailDB.HtShotsOnGoal == null ? 0 : (int)dbModel.MatchCompetitorsStatsComDetailDB.HtShotsOnGoal;
                stats.AtShotsOnTarget = dbModel.MatchCompetitorsStatsComDetailDB.AtShotsOnGoal == null ? 0 : (int)dbModel.MatchCompetitorsStatsComDetailDB.AtShotsOnGoal;
                stats.HtShotsOnTargetPercentage = dbModel.MatchCompetitorsStatsComDetailDB.HtShotsOnGoal != 0 || dbModel.MatchCompetitorsStatsComDetailDB.AtShotsOnGoal != 0 ?
                                                    (int)((dbModel.MatchCompetitorsStatsComDetailDB.HtShotsOnGoal * 100) / (dbModel.MatchCompetitorsStatsComDetailDB.HtShotsOnGoal + dbModel.MatchCompetitorsStatsComDetailDB.AtShotsOnGoal)) : 0;
                stats.AtShotsOnTargetPercentage = dbModel.MatchCompetitorsStatsComDetailDB.HtShotsOnGoal != 0 || dbModel.MatchCompetitorsStatsComDetailDB.AtShotsOnGoal != 0 ?
                                                    (int)((dbModel.MatchCompetitorsStatsComDetailDB.AtShotsOnGoal * 100) / (dbModel.MatchCompetitorsStatsComDetailDB.HtShotsOnGoal + dbModel.MatchCompetitorsStatsComDetailDB.AtShotsOnGoal)) : 0;

                stats.HtPossessionPercentage = (double)dbModel.MatchCompetitorsStatsComDetailDB.HtPossessionPercentage;
                stats.AtPossessionPercentage = (double)dbModel.MatchCompetitorsStatsComDetailDB.AtPossessionPercentage;
                #endregion

                #region Yellow Cards
                stats.HtYellowCards = dbModel.MatchCompetitorsStatsComDetailDB.HtYellowCards == null ? 0 : (int)dbModel.MatchCompetitorsStatsComDetailDB.HtYellowCards;
                stats.AtYellowCards = dbModel.MatchCompetitorsStatsComDetailDB.AtYellowCards == null ? 0 : (int)dbModel.MatchCompetitorsStatsComDetailDB.AtYellowCards;
                stats.HtYellowCardsPercentage = dbModel.MatchCompetitorsStatsComDetailDB.HtYellowCards != 0 || dbModel.MatchCompetitorsStatsComDetailDB.AtYellowCards != 0 ?
                                                    (int)((dbModel.MatchCompetitorsStatsComDetailDB.HtYellowCards * 100) / (dbModel.MatchCompetitorsStatsComDetailDB.HtYellowCards + dbModel.MatchCompetitorsStatsComDetailDB.AtYellowCards)) : 0;
                stats.AtYellowCardsPercentage = dbModel.MatchCompetitorsStatsComDetailDB.HtYellowCards != 0 || dbModel.MatchCompetitorsStatsComDetailDB.AtYellowCards != 0 ?
                                                    (int)((dbModel.MatchCompetitorsStatsComDetailDB.AtYellowCards * 100) / (dbModel.MatchCompetitorsStatsComDetailDB.HtYellowCards + dbModel.MatchCompetitorsStatsComDetailDB.AtYellowCards)) : 0;
                #endregion

                #region Red Cards
                stats.HtRedCards = dbModel.MatchCompetitorsStatsComDetailDB.HtRedCards == null ? 0 : (int)dbModel.MatchCompetitorsStatsComDetailDB.HtRedCards;
                stats.AtRedCards = dbModel.MatchCompetitorsStatsComDetailDB.AtRedCards == null ? 0 : (int)dbModel.MatchCompetitorsStatsComDetailDB.AtRedCards;
                stats.HtRedCardsPercentage = dbModel.MatchCompetitorsStatsComDetailDB.HtRedCards != 0 || dbModel.MatchCompetitorsStatsComDetailDB.AtRedCards != 0 ?
                                                    (int)((dbModel.MatchCompetitorsStatsComDetailDB.HtRedCards * 100) / (dbModel.MatchCompetitorsStatsComDetailDB.HtRedCards + dbModel.MatchCompetitorsStatsComDetailDB.AtRedCards)) : 0;
                stats.AtRedCardsPercentage = dbModel.MatchCompetitorsStatsComDetailDB.HtRedCards != 0 || dbModel.MatchCompetitorsStatsComDetailDB.AtRedCards != 0 ?
                                                    (int)((dbModel.MatchCompetitorsStatsComDetailDB.AtRedCards * 100) / (dbModel.MatchCompetitorsStatsComDetailDB.HtRedCards + dbModel.MatchCompetitorsStatsComDetailDB.AtRedCards)) : 0;
                #endregion

                #region Shots Off Target
                stats.HtShotsOffTarget = dbModel.MatchCompetitorsStatsComDetailDB.HtTotalAssists == null ? 0 : (int)dbModel.MatchCompetitorsStatsComDetailDB.HtTotalAssists;
                stats.AtShotsOffTarget = dbModel.MatchCompetitorsStatsComDetailDB.AtTotalAssists == null ? 0 : (int)dbModel.MatchCompetitorsStatsComDetailDB.AtTotalAssists;
                stats.HtShotsOffTargetPercentage = dbModel.MatchCompetitorsStatsComDetailDB.HtTotalAssists != 0 || dbModel.MatchCompetitorsStatsComDetailDB.AtTotalAssists != 0 ?
                                                    (int)((dbModel.MatchCompetitorsStatsComDetailDB.HtTotalAssists * 100) / (dbModel.MatchCompetitorsStatsComDetailDB.HtTotalAssists + dbModel.MatchCompetitorsStatsComDetailDB.AtTotalAssists)) : 0;
                stats.AtShotsOffTargetPercentage = dbModel.MatchCompetitorsStatsComDetailDB.HtTotalAssists != 0 || dbModel.MatchCompetitorsStatsComDetailDB.AtTotalAssists != 0 ?
                                                    (int)((dbModel.MatchCompetitorsStatsComDetailDB.AtTotalAssists * 100) / (dbModel.MatchCompetitorsStatsComDetailDB.HtTotalAssists + dbModel.MatchCompetitorsStatsComDetailDB.AtTotalAssists)) : 0;
                #endregion

                #region Attacks
                stats.HtAttacks = dbModel.MatchCompetitorsStatsComDetailDB.HtAttacks == null ? 0 : (int)dbModel.MatchCompetitorsStatsComDetailDB.HtAttacks;
                stats.AtAttacks = dbModel.MatchCompetitorsStatsComDetailDB.AtAttacks == null ? 0 : (int)dbModel.MatchCompetitorsStatsComDetailDB.AtAttacks;
                stats.HtAttacksPercentage = dbModel.MatchCompetitorsStatsComDetailDB.HtAttacks != 0 || dbModel.MatchCompetitorsStatsComDetailDB.AtAttacks != 0 ?
                                                (int)((dbModel.MatchCompetitorsStatsComDetailDB.HtAttacks * 100) / (dbModel.MatchCompetitorsStatsComDetailDB.HtAttacks + dbModel.MatchCompetitorsStatsComDetailDB.AtAttacks)) : 0;
                stats.AtAttacksPercentage = dbModel.MatchCompetitorsStatsComDetailDB.HtAttacks != 0 || dbModel.MatchCompetitorsStatsComDetailDB.AtAttacks != 0 ?
                                                (int)((dbModel.MatchCompetitorsStatsComDetailDB.AtAttacks * 100) / (dbModel.MatchCompetitorsStatsComDetailDB.HtAttacks + dbModel.MatchCompetitorsStatsComDetailDB.AtAttacks)) : 0;
                #endregion

                #region Dangerous Attacks
                stats.HtDangerousAttacks = dbModel.MatchCompetitorsStatsComDetailDB.HtDangerousAttacks == null ? 0 : (int)dbModel.MatchCompetitorsStatsComDetailDB.HtDangerousAttacks;
                stats.AtDangerousAttacks = dbModel.MatchCompetitorsStatsComDetailDB.AtDangerousAttacks == null ? 0 : (int)dbModel.MatchCompetitorsStatsComDetailDB.AtDangerousAttacks;
                stats.HtDangerousAttacksPercentage = dbModel.MatchCompetitorsStatsComDetailDB.HtDangerousAttacks != 0 || dbModel.MatchCompetitorsStatsComDetailDB.AtDangerousAttacks != 0 ?
                                                        (int)((dbModel.MatchCompetitorsStatsComDetailDB.HtDangerousAttacks * 100) / (dbModel.MatchCompetitorsStatsComDetailDB.HtDangerousAttacks + dbModel.MatchCompetitorsStatsComDetailDB.AtDangerousAttacks)) : 0;
                stats.AtDangerousAttacksPercentage = dbModel.MatchCompetitorsStatsComDetailDB.HtDangerousAttacks != 0 || dbModel.MatchCompetitorsStatsComDetailDB.AtDangerousAttacks != 0 ?
                                                        (int)((dbModel.MatchCompetitorsStatsComDetailDB.AtDangerousAttacks * 100) / (dbModel.MatchCompetitorsStatsComDetailDB.HtDangerousAttacks + dbModel.MatchCompetitorsStatsComDetailDB.AtDangerousAttacks)) : 0;
                matchDetailBetwayDBModel.Statistics = stats;
                #endregion
            }
            #endregion

            #region Match Venue
            if (dbModel.MatchVenueDetailDB != null)
            {
                matchDetailBetwayDBModel.MatchVenue = new MatchVenueDetailModel()
                {
                    VenueId = dbModel.MatchVenueDetailDB.VenueId,
                    VenueName = dbModel.MatchVenueDetailDB.VenueName,
                    Spectators = dbModel.MatchVenueDetailDB.Spectators
                };
            }
            else
            {
                matchDetailBetwayDBModel.MatchVenue = new MatchVenueDetailModel();
            }
            #endregion

            #region Match Officials
            matchDetailBetwayDBModel.MatchOfficials = (from official in dbModel.MatchOfficialsDB
                                                       select new MatchOfficialsDetailModel
                                                       {
                                                           OfficialId = official.OfficialId,
                                                           OfficialName = official.OfficialName,
                                                           OfficialType = official.OfficialType,
                                                           OfficialTypeId = official.OfficialTypeId
                                                       }).ToList();
            #endregion

            #region Home Team Stats
            matchDetailBetwayDBModel.HomeTeamStatsMarkets = (from _teamStats in dbModel.HomeTeamStatsMarkets
                                                             select new ContestTeamsStatsModel
                                                             {
                                                                 TeamId = _teamStats.TeamId,
                                                                 TeamName = _teamStats.TeamName,
                                                                 MatchesPlayed = _teamStats.MatchesPlayed,
                                                                 MarketId = _teamStats.MarketId,
                                                                 MarketName = _teamStats.MarketName,
                                                                 Position = _teamStats.Position + (_teamStats.Position == 1 ? "st" : _teamStats.Position == 2 ? "nd" : _teamStats.Position == 3 ? "rd" : "th"),
                                                                 Percentage = _teamStats.Percentage
                                                             }).ToList();
            #endregion

            #region Away Team Stats
            matchDetailBetwayDBModel.AwayTeamStatsMarkets = (from _teamStats in dbModel.AwayTeamStatsMarkets
                                                             select new ContestTeamsStatsModel
                                                             {
                                                                 TeamId = _teamStats.TeamId,
                                                                 TeamName = _teamStats.TeamName,
                                                                 MatchesPlayed = _teamStats.MatchesPlayed,
                                                                 MarketId = _teamStats.MarketId,
                                                                 MarketName = _teamStats.MarketName,
                                                                 Position = _teamStats.Position + (_teamStats.Position == 1 ? "st" : _teamStats.Position == 2 ? "nd" : _teamStats.Position == 3 ? "rd" : "th"),
                                                                 Percentage = _teamStats.Percentage
                                                             }).ToList();
            #endregion

            #region ActiveTabs
            ActiveTabsInfoModel activeTabs = new ActiveTabsInfoModel();
            activeTabs.IsInfo = ((matchDetailBetwayDBModel.MatchEvents != null && matchDetailBetwayDBModel.MatchEvents.Count > 0)) ? true : false;
            activeTabs.IsCommentary = (matchDetailBetwayDBModel.MatchCommentary != null && matchDetailBetwayDBModel.MatchCommentary.Count > 0) ? true : false;
            activeTabs.IsHead2Head = (matchDetailBetwayDBModel.Head2HeadMatches != null && matchDetailBetwayDBModel.Head2HeadMatches.Count > 0) ? true : false;
            activeTabs.IsNews = (matchDetailBetwayDBModel.MatchNews != null && matchDetailBetwayDBModel.MatchNews.Count > 0) ? true : false;
            activeTabs.IsPreviews = (matchDetailBetwayDBModel.MatchPreviews != null && matchDetailBetwayDBModel.MatchPreviews.Count > 0) ? true : false;
            activeTabs.IsReports = (matchDetailBetwayDBModel.MatchReports != null && matchDetailBetwayDBModel.MatchReports.Count > 0) ? true : false;
            activeTabs.IsStatistics = matchDetailBetwayDBModel.Statistics != null ? true : false;
            activeTabs.IsOdds = matchDetailBetwayDBModel.OddsMatchId != 0 ? true : false;
            activeTabs.IsLeagueTable = (matchDetailBetwayDBModel.LeagueTable != null && matchDetailBetwayDBModel.LeagueTable.LeagueCompetitors != null && matchDetailBetwayDBModel.LeagueTable.LeagueCompetitors.Count > 0) ? true : false;
            activeTabs.IsLineup = ((matchDetailBetwayDBModel.HomeTeamLineupPlayers != null && matchDetailBetwayDBModel.HomeTeamLineupPlayers.Count > 0) || (matchDetailBetwayDBModel.AwayTeamLineupPlayers != null && matchDetailBetwayDBModel.AwayTeamLineupPlayers.Count > 0)) ? true : false;
            matchDetailBetwayDBModel.ActiveTabs = activeTabs;
            #endregion

            return matchDetailBetwayDBModel;
        }

        public static LeagueTableModel GetLeagueTableByContestGroupId(GlobalParametersModel globalParametersModel)
        {
            List<LeagueTableDBModel> leagueTableDBModel = DBManager.Execute<LeagueTableDBModel>("GoalCC_LeagueTableByContestGroupId",
            new
            {
                ContestGroupId = globalParametersModel.ContestGroupId
            });

            #region ADO.Net Code
            //  List<LeagueTableDBModel> leagueTableDBModel = new List<LeagueTableDBModel>();

            //using (DataTable _dataTable = new DataTable())
            //{
            //    using (SqlConnection _connection = new SqlConnection(_connectionString))
            //    {
            //        _connection.Open();
            //        using (SqlCommand _command = new SqlCommand("GoalCC_LeagueTableByContestGroupId", _connection))
            //        {
            //            _command.CommandType = CommandType.StoredProcedure;
            //            _command.Parameters.Add(new SqlParameter("@ContestGroupId", contestGroupId));
            //            using (SqlDataAdapter _sqlDataAdapter = new SqlDataAdapter(_command))
            //            {
            //                _sqlDataAdapter.Fill(_dataTable);
            //            }
            //        }
            //    }

            //    foreach (DataRow _dataRow in _dataTable.Rows)
            //    {
            //        LeagueTableDBModel _leagueTableDBModel = new LeagueTableDBModel()
            //        {
            //            LeagueTableId = Convert.ToInt32(_dataRow["LeagueTableId"]),
            //            ContestGroupId = Convert.ToInt32(_dataRow["ContestGroupId"]),
            //            ContestGroupName = _dataRow["ContestGroupName"].ToString(),
            //            SeasonId = Convert.ToInt16(_dataRow["SeasonId"]),
            //            LeagueTableCompetitorId = Convert.ToDecimal(_dataRow["LeagueTableCompetitorId"]),
            //            TeamId = Convert.ToInt32(_dataRow["TeamId"]),
            //            TeamName = _dataRow["TeamName"].ToString(),
            //            Place = Convert.ToInt16(_dataRow["Place"]),
            //            LeagueTableMatchId = Convert.ToDecimal(_dataRow["LeagueTableMatchId"]),
            //            Played = Convert.ToInt16(_dataRow["Played"]),
            //            Won = Convert.ToInt16(_dataRow["Won"]),
            //            Lost = Convert.ToInt16(_dataRow["Lost"]),
            //            Draws = Convert.ToInt16(_dataRow["Draws"]),
            //            Scored = Convert.ToInt16(_dataRow["Scored"]),
            //            Conceded = Convert.ToInt16(_dataRow["Conceded"]),
            //            Difference = Convert.ToInt16(_dataRow["Difference"]),
            //            Points = Convert.ToInt16(_dataRow["Points"]),
            //            Type = _dataRow["Type"].ToString()
            //        };
            //        leagueTableDBModel.Add(_leagueTableDB
            //);
            //    }
            //} 
            #endregion

            LeagueTableModel leagueTableModel = new LeagueTableModel();
            leagueTableModel = (from _leagueTableDbModel in leagueTableDBModel.ToList()
                                select new LeagueTableModel
                                {
                                    LeagueTableId = _leagueTableDbModel.LeagueTableId,
                                    ContestGroupId = _leagueTableDbModel.ContestGroupId,
                                    SeasonId = _leagueTableDbModel.SeasonId,
                                    LeagueCompetitors = (from _competitor in leagueTableDBModel.ToList()
                                                         where _competitor.LeagueTableId == _leagueTableDbModel.LeagueTableId
                                                         group _competitor by new { _competitor.LeagueTableCompetitorId, _competitor.TeamId, _competitor.TeamName, _competitor.Place } into competitor
                                                         select new LeagueTableCompetitorModel
                                                         {
                                                             LeagueTableCompetitorId = competitor.Key.LeagueTableCompetitorId,
                                                             TeamId = competitor.Key.TeamId,
                                                             Team = competitor.Key.TeamName,
                                                             Place = competitor.Key.Place,
                                                             LeagueTablesMatches = (from _leagueTableMatch in leagueTableDBModel.ToList()
                                                                                    where _leagueTableMatch.LeagueTableCompetitorId == competitor.Key.LeagueTableCompetitorId
                                                                                    select new LeagueTableMatchesModel
                                                                                    {
                                                                                        LeagueTableMatchId = _leagueTableMatch.LeagueTableMatchId,
                                                                                        Played = _leagueTableMatch.Played,
                                                                                        Won = _leagueTableMatch.Won,
                                                                                        Lost = _leagueTableMatch.Lost,
                                                                                        Draws = _leagueTableMatch.Draws,
                                                                                        Scored = _leagueTableMatch.Scored,
                                                                                        Conceded = _leagueTableMatch.Conceded,
                                                                                        Difference = _leagueTableMatch.Difference,
                                                                                        Points = _leagueTableMatch.Points,
                                                                                        Type = _leagueTableMatch.Type
                                                                                    }).ToList()
                                                         }).OrderBy(lc => lc.Place).ToList()
                                }).FirstOrDefault();

            if (leagueTableModel == null)
                leagueTableModel = new LeagueTableModel();

            return leagueTableModel;
        }

        public static List<TeamsModel> GetContestTeams(GlobalParametersModel globalParametersModel)
        {
            List<TeamsModel> teamModel = DBManager.Execute<TeamsModel>("GoalCCAPI_GetContestTeamsByContestId", new
            {
                ContestGroupId = globalParametersModel.ContestGroupId
            });

            #region  ADO.Net Code
            //responseSource = "Cache";
            //var model = HttpRuntime.Cache["contest-teams-" + contestId] as List<TeamsModel>;

            //if (model == null)
            //{
            //    List<TeamsModel> query = new List<TeamsModel>();
            //    DataTable dt = new DataTable();
            //    using (SqlConnection con = new SqlConnection(_connectionString))
            //    {
            //        con.Open();
            //        using (SqlCommand cmd = new SqlCommand("GoalCCAPI_GetContestTeamsByContestId", con))
            //        {
            //            cmd.CommandType = CommandType.StoredProcedure;
            //            cmd.Parameters.Add(new SqlParameter("@ContestGroupId", contestId));
            //            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            //            adapter.Fill(dt);
            //            cmd.Dispose();
            //        }

            //        foreach (DataRow dr in dt.Rows)
            //        {
            //            TeamsModel m = new TeamsModel();

            //            m.TeamId = Convert.ToInt32(dr["TeamId"]);
            //            m.Team = dr["Team"].ToString();

            //            query.Add(m);

            //        }

            //        model = query;
            //        HttpRuntime.Cache.Insert("contest-teams-" + contestId, model, null, DateTime.Now.AddMinutes(15), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Default, null);
            //        responseSource = "Database";
            //    }
            //}
            #endregion

            return teamModel;
        }

        public static List<ContestMatchesListModel> GetMatchesGroupByContest(GlobalParametersModel globalParametersModel)
        {
            string matchStatuses = _ReturnMatchStatuses(globalParametersModel.SportId, globalParametersModel.MatchType);
            List<ContestMatchesListModel> contestMatchesListModel = new List<ContestMatchesListModel>();
            List<MatchesDBModel> matchesDBModel = DBManager.Execute<MatchesDBModel>("GoalccAPI_GetAllMatchListingNew", new
            {
                SportId = globalParametersModel.SportId,
                FromDate = globalParametersModel.FromDate,
                ToDate = globalParametersModel.FromDate.AddDays(1),
                MatchStatusIds = matchStatuses,
                LanguageCode = globalParametersModel.LanguageCode
            });

            matchesDBModel = (from _match in matchesDBModel
                              select new MatchesDBModel
                              {
                                  ScoreInfoTypeId = _match.ScoreInfoTypeId != null ? _match.ScoreInfoTypeId : (Int16?)null,
                                  MinutePlusBit = _match.MinutePlusBit != null ? Convert.ToBoolean(_match.MinutePlusBit) : (bool?)null,
                                  CurrentMinutes = _match.CurrentMinutes != null ? _match.CurrentMinutes : (Int16?)null,
                                  SportId = _match.SportId,
                                  AwayScore = _match.AwayScore,
                                  AwayTieBreakScore = _match.AwayTieBreakScore,
                                  HomeAway = _match.HomeAway,
                                  HomeScore = _match.HomeScore,
                                  HomeTieBreakScore = _match.HomeTieBreakScore,
                                  Competitor = _match.Competitor,
                                  CompetitorId = _match.CompetitorId,
                                  CompetitorShortName = _match.CompetitorShortName,
                                  ContestGroup = _match.ContestGroup,
                                  ContestGroupId = _match.ContestGroupId,
                                  ContestGroupShortName = _match.ContestGroupShortName,
                                  CountryId = _match.CountryId,
                                  MatchCompetitorId = _match.MatchCompetitorId,
                                  MatchDate = _match.MatchDate,
                                  MatchId = _match.MatchId,
                                  CountryName = _match.CountryName,
                                  FirstToServe = _match.FirstToServe,
                                  LeagueId = _match.LeagueId,
                                  LeagueName = _match.LeagueName,
                                  MatchStatus = _match.MatchStatus,
                                  MatchStatusId = _match.MatchStatusId,
                                  MatchTime = _match.MatchTime,
                                  PlusMinutes = _match.PlusMinutes
                              }).ToList();

            #region  ADO.Net Code
            //  List<ContestMatchesListModel> contestMatchesListModel = new List<ContestMatchesListModel>();
            //using (DataTable _dataTable = new DataTable())
            //{
            //    using (SqlConnection _connection = new SqlConnection(_connectionString))
            //    {
            //        _connection.Open();
            //        using (SqlCommand _command = new SqlCommand("GoalccAPI_GetAllMatchListingNew", _connection))
            //        {
            //            _command.CommandType = CommandType.StoredProcedure;
            //            _command.Parameters.Add(new SqlParameter("@SportId", sportId));
            //            _command.Parameters.Add(new SqlParameter("@FromDate", fromDate));
            //            _command.Parameters.Add(new SqlParameter("@ToDate", fromDate.AddDays(1)));
            //            _command.Parameters.Add(new SqlParameter("@MatchStatusIds", matchStatuses));
            //            _command.Parameters.Add(new SqlParameter("@PageNo", pagenumber));
            //            _command.Parameters.Add(new SqlParameter("@RowCountPerPage", pageSize));
            //            using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(_command))
            //            {
            //                sqlDataAdapter.Fill(_dataTable);
            //            }
            //        }
            //    }

            //    foreach (DataRow _dataRow in _dataTable.Rows)
            //    {
            //        MatchesDBModel _matchesDBModel = new MatchesDBModel()
            //        {
            //            SportId = Convert.ToInt16(_dataRow["SportId"]),
            //            MatchId = Convert.ToInt32(_dataRow["MatchId"]),
            //            CountryId = Convert.ToInt16(_dataRow["CountryId"]),
            //            CountryName = _dataRow["CountryName"].ToString(),
            //            ContestGroupId = Convert.ToInt32(_dataRow["ContestGroupId"]),
            //            ContestGroup = _dataRow["ContestGroup"].ToString(),
            //            LeagueId = Convert.ToInt32(_dataRow["LeagueId"]),
            //            LeagueName = Convert.ToString(_dataRow["LeagueName"]),
            //            ContestGroupShortName = _dataRow["ContestGroupShortName"].ToString(),
            //            MatchDate = _dataRow["MatchDate"].ToString(),
            //            MatchTime = _dataRow["MatchTime"].ToString(),
            //            MatchStatusId = Convert.ToInt16(_dataRow["MatchStatusId"].ToString()),
            //            MatchStatus = _dataRow["MatchStatus"].ToString(),
            //            FirstToServe = _dataRow["FirstToServe"].ToString(),
            //            MatchCompetitorId = Convert.ToDecimal(_dataRow["MatchCompetitorId"]),
            //            CompetitorId = Convert.ToInt32(_dataRow["CompetitorId"]),
            //            Competitor = _dataRow["Competitor"].ToString(),
            //            CompetitorShortName = _dataRow["CompetitorShortName"].ToString(),
            //            HomeAway = Convert.ToBoolean(_dataRow["HomeAway"]),
            //            ScoreInfoTypeId = _dataRow["ScoreInfoTypeId"] != DBNull.Value ? Convert.ToInt16(_dataRow["ScoreInfoTypeId"]) : (Int16?)null,
            //            HomeScore = _dataRow["HomeScore"].ToString(),
            //            AwayScore = _dataRow["AwayScore"].ToString(),
            //            HomeTieBreakScore = _dataRow["HomeTieBreakScore"].ToString(),
            //            AwayTieBreakScore = _dataRow["AwayTieBreakScore"].ToString(),
            //            MinutePlusBit = _dataRow["MinutePlusBit"] != DBNull.Value ? Convert.ToBoolean(_dataRow["MinutePlusBit"]) : (bool?)null,
            //            CurrentMinutes = _dataRow["CurrentMinutes"] != DBNull.Value ? Convert.ToInt16(_dataRow["CurrentMinutes"]) : (Int16?)null,
            //            PlusMinutes = _dataRow["PlusMinutes"].ToString()
            //        };
            //        matchesDBModel.Add(_matchesDBModel);
            //    }
            //} 
            #endregion

            if (matchesDBModel != null && matchesDBModel.Count > 0)
            {
                contestMatchesListModel = (from _contestGroup in matchesDBModel.ToList()
                                           group _contestGroup by new
                                           {
                                               _contestGroup.SportId,
                                               _contestGroup.CountryId,
                                               _contestGroup.CountryName,
                                               _contestGroup.ContestGroupId,
                                               _contestGroup.ContestGroup,
                                               _contestGroup.ContestGroupShortName,
                                               _contestGroup.LeagueId,
                                               _contestGroup.LeagueName
                                           } into contest
                                           select new ContestMatchesListModel
                                           {
                                               SportId = contest.Key.SportId,
                                               CountryId = contest.Key.CountryId,
                                               CountryName = contest.Key.CountryName,
                                               ContestGroupId = contest.Key.ContestGroupId,
                                               ContestGroup = contest.Key.ContestGroup,
                                               ContestGroupShortName = !string.IsNullOrEmpty(contest.Key.ContestGroupShortName) ? contest.Key.ContestGroupShortName : contest.Key.ContestGroup,
                                               LeagueId = contest.Key.LeagueId,
                                               LeagueName = contest.Key.LeagueName,
                                               Matches = (from _match in matchesDBModel.ToList()
                                                          where _match.ContestGroupId == contest.Key.ContestGroupId
                                                          group _match by new
                                                          {
                                                              _match.MatchId,
                                                              _match.MatchDate,
                                                              _match.MatchTime,
                                                              _match.MatchStatusId,
                                                              _match.MatchStatus,
                                                              _match.FirstToServe,
                                                              _match.MinutePlusBit,
                                                              _match.CurrentMinutes,
                                                              _match.PlusMinutes
                                                          } into matches
                                                          select new ContestMatchesModel
                                                          {
                                                              MatchId = matches.Key.MatchId,
                                                              MatchDate = matches.Key.MatchDate,
                                                              MatchTime = matches.Key.MatchTime,
                                                              MatchStatusId = matches.Key.MatchStatusId,
                                                              MatchStatus = matches.Key.MatchStatus,
                                                              FirstToServe = !string.IsNullOrEmpty(matches.Key.FirstToServe) ? matches.Key.FirstToServe : string.Empty,
                                                              CurrentMinutes = !string.IsNullOrEmpty(matches.Key.CurrentMinutes.ToString()) ? (matches.Key.MinutePlusBit == true ? matches.Key.CurrentMinutes + "+" + (!string.IsNullOrEmpty(matches.Key.PlusMinutes) ? matches.Key.PlusMinutes : string.Empty) : matches.Key.CurrentMinutes.ToString()) : "0",
                                                              HomeTeam = (from _homeTeam in matchesDBModel.ToList()
                                                                          where _homeTeam.MatchId == matches.Key.MatchId
                                                                              && _homeTeam.HomeAway == true
                                                                          select new MatchCompetitorModel
                                                                          {
                                                                              MatchCompetitorId = _homeTeam.MatchCompetitorId,
                                                                              TeamId = _homeTeam.CompetitorId,
                                                                              TeamName = _homeTeam.Competitor,
                                                                              TeamShortName = !string.IsNullOrEmpty(_homeTeam.CompetitorShortName) ? _homeTeam.CompetitorShortName : _homeTeam.Competitor
                                                                          }).FirstOrDefault(),
                                                              AwayTeam = (from _awayTeam in matchesDBModel.ToList()
                                                                          where _awayTeam.MatchId == matches.Key.MatchId
                                                                                  && _awayTeam.HomeAway == false
                                                                          select new MatchCompetitorModel
                                                                          {
                                                                              MatchCompetitorId = _awayTeam.MatchCompetitorId,
                                                                              TeamId = _awayTeam.CompetitorId,
                                                                              TeamName = _awayTeam.Competitor,
                                                                              TeamShortName = !string.IsNullOrEmpty(_awayTeam.CompetitorShortName) ? _awayTeam.CompetitorShortName : _awayTeam.Competitor
                                                                          }).FirstOrDefault(),
                                                              MatchScore = (from _matchScore in matchesDBModel.ToList()
                                                                            where _matchScore.MatchId == matches.Key.MatchId
                                                                                    && _matchScore.ScoreInfoTypeId != null
                                                                            select new MatchScoreModel
                                                                            {
                                                                                ScoreInfoTypeId = Convert.ToInt16(_matchScore.ScoreInfoTypeId),
                                                                                HomeScore = _matchScore.HomeScore,
                                                                                AwayScore = _matchScore.AwayScore,
                                                                                HomeTieBreakScore = !string.IsNullOrEmpty(_matchScore.HomeTieBreakScore) ? _matchScore.HomeTieBreakScore : string.Empty,
                                                                                AwayTieBreakScore = !string.IsNullOrEmpty(_matchScore.AwayTieBreakScore) ? _matchScore.AwayTieBreakScore : string.Empty
                                                                            }).FirstOrDefault()
                                                          }).ToList()
                                           }).ToList();
            }
            return contestMatchesListModel;
        }

        public static List<ContestMatchesListModel> GetAllMatchesByContestRound(GlobalParametersModel globalParametersModel)
        {
            List<MatchesByRoundDbModel> matchesByRoundDBModel = DBManager.Execute<MatchesByRoundDbModel>("GoalccAPI_GetAllMatchListingByContestGroupIdAndContestGroupRoundId", new
            {
                ContestGroupId = globalParametersModel.ContestGroupId,
                ContestGroupRoundId = globalParametersModel.ContestGroupRoundId,
                LanguageCode = globalParametersModel.LanguageCode
            });
            List<ContestMatchesListModel> contestMatchesListModel = new List<ContestMatchesListModel>();

            #region  ADO.Net Code
            //using (DataTable _dataTable = new DataTable())
            //{
            //    using (SqlConnection _connection = new SqlConnection(_connectionString))
            //    {
            //        _connection.Open();
            //        using (SqlCommand _command = new SqlCommand("GoalccAPI_GetAllMatchListingByContestGroupIdAndContestGroupRoundId", _connection))
            //        {
            //            _command.CommandType = CommandType.StoredProcedure;
            //            _command.Parameters.Add(new SqlParameter("@ContestGroupId", contestId));
            //            _command.Parameters.Add(new SqlParameter("@ContestGroupRoundId", contestRoundId));
            //            using (SqlDataAdapter _sqlDataAdapter = new SqlDataAdapter(_command))
            //            {
            //                _sqlDataAdapter.Fill(_dataTable);
            //            }
            //        }
            //    }

            //    foreach (DataRow _dataRow in _dataTable.Rows)
            //    {
            //        MatchesDBModel _matchesDBModel = new MatchesDBModel()
            //        {
            //            SportId = Convert.ToInt16(_dataRow["SportId"]),
            //            MatchId = Convert.ToInt32(_dataRow["MatchId"]),
            //            CountryId = Convert.ToInt16(_dataRow["CountryId"]),
            //            CountryName = _dataRow["CountryName"].ToString(),
            //            ContestGroupId = Convert.ToInt32(_dataRow["ContestGroupId"]),
            //            ContestGroup = _dataRow["ContestGroup"].ToString(),
            //            ContestGroupShortName = _dataRow["ContestGroupShortName"].ToString(),
            //            MatchDate = _dataRow["MatchDate"].ToString(),
            //            MatchTime = _dataRow["MatchTime"].ToString(),
            //            MatchStatusId = Convert.ToInt16(_dataRow["MatchStatusId"].ToString()),
            //            MatchStatus = _dataRow["MatchStatus"].ToString(),
            //            FirstToServe = _dataRow["FirstToServe"].ToString(),
            //            MatchCompetitorId = Convert.ToDecimal(_dataRow["MatchCompetitorId"]),
            //            CompetitorId = Convert.ToInt32(_dataRow["CompetitorId"]),
            //            Competitor = _dataRow["Competitor"].ToString(),
            //            CompetitorShortName = _dataRow["CompetitorShortName"].ToString(),
            //            HomeAway = Convert.ToBoolean(_dataRow["HomeAway"]),
            //            ScoreInfoTypeId = _dataRow["ScoreInfoTypeId"] != DBNull.Value ? Convert.ToInt16(_dataRow["ScoreInfoTypeId"]) : (Int16?)null,
            //            HomeScore = _dataRow["HomeScore"].ToString(),
            //            AwayScore = _dataRow["AwayScore"].ToString(),
            //            HomeTieBreakScore = _dataRow["HomeTieBreakScore"].ToString(),
            //            AwayTieBreakScore = _dataRow["AwayTieBreakScore"].ToString(),
            //            MinutePlusBit = _dataRow["MinutePlusBit"] != DBNull.Value ? Convert.ToBoolean(_dataRow["MinutePlusBit"]) : (bool?)null,
            //            CurrentMinutes = _dataRow["CurrentMinutes"] != DBNull.Value ? Convert.ToInt16(_dataRow["CurrentMinutes"]) : (Int16?)null,
            //            PlusMinutes = _dataRow["PlusMinutes"].ToString()
            //        };
            //        matchesDBModel.Add(_matchesDBModel);
            //    }
            //} 
            #endregion

            matchesByRoundDBModel = (from _match in matchesByRoundDBModel
                                     select new MatchesByRoundDbModel
                                     {
                                         ScoreInfoTypeId = _match.ScoreInfoTypeId != null ? _match.ScoreInfoTypeId : (Int16?)null,
                                         MinutePlusBit = _match.MinutePlusBit != null ? Convert.ToBoolean(_match.MinutePlusBit) : (bool?)null,
                                         CurrentMinutes = _match.CurrentMinutes != null ? _match.CurrentMinutes : (Int16?)null,
                                         SportId = _match.SportId,
                                         AwayScore = _match.AwayScore,
                                         AwayTieBreakScore = _match.AwayTieBreakScore,
                                         HomeAway = _match.HomeAway,
                                         HomeScore = _match.HomeScore,
                                         HomeTieBreakScore = _match.HomeTieBreakScore,
                                         Competitor = _match.Competitor,
                                         CompetitorId = _match.CompetitorId,
                                         CompetitorShortName = _match.CompetitorShortName,
                                         ContestGroup = _match.ContestGroup,
                                         ContestGroupId = _match.ContestGroupId,
                                         ContestGroupShortName = _match.ContestGroupShortName,
                                         CountryId = _match.CountryId,
                                         MatchCompetitorId = _match.MatchCompetitorId,
                                         MatchDate = _match.MatchDate,
                                         MatchId = _match.MatchId,
                                         CountryName = _match.CountryName,
                                         FirstToServe = _match.FirstToServe,
                                         MatchStatus = _match.MatchStatus,
                                         MatchStatusId = _match.MatchStatusId,
                                         MatchTime = _match.MatchTime,
                                         PlusMinutes = _match.PlusMinutes
                                     }).ToList();

            if (matchesByRoundDBModel != null && matchesByRoundDBModel.Count > 0)
            {
                contestMatchesListModel = (from _contestGroup in matchesByRoundDBModel.ToList()
                                           group _contestGroup by new
                                           {
                                               _contestGroup.SportId,
                                               _contestGroup.CountryId,
                                               _contestGroup.CountryName,
                                               _contestGroup.ContestGroupId,
                                               _contestGroup.ContestGroup,
                                               _contestGroup.ContestGroupShortName
                                           } into contest
                                           select new ContestMatchesListModel
                                           {
                                               SportId = contest.Key.SportId,
                                               CountryId = contest.Key.CountryId,
                                               CountryName = contest.Key.CountryName,
                                               ContestGroupId = contest.Key.ContestGroupId,
                                               ContestGroup = contest.Key.ContestGroup,
                                               ContestGroupShortName = !string.IsNullOrEmpty(contest.Key.ContestGroupShortName) ? contest.Key.ContestGroupShortName : contest.Key.ContestGroup,
                                               Matches = (from _match in matchesByRoundDBModel.ToList()
                                                          where _match.ContestGroupId == contest.Key.ContestGroupId
                                                          group _match by new
                                                          {
                                                              _match.MatchId,
                                                              _match.MatchDate,
                                                              _match.MatchTime,
                                                              _match.MatchStatusId,
                                                              _match.MatchStatus,
                                                              _match.FirstToServe,
                                                              _match.MinutePlusBit,
                                                              _match.CurrentMinutes,
                                                              _match.PlusMinutes
                                                          } into matches
                                                          select new ContestMatchesModel
                                                          {
                                                              MatchId = matches.Key.MatchId,
                                                              MatchDate = matches.Key.MatchDate,
                                                              MatchTime = matches.Key.MatchTime,
                                                              MatchStatusId = matches.Key.MatchStatusId,
                                                              MatchStatus = matches.Key.MatchStatus,
                                                              FirstToServe = !string.IsNullOrEmpty(matches.Key.FirstToServe) ? matches.Key.FirstToServe : string.Empty,
                                                              CurrentMinutes = !string.IsNullOrEmpty(matches.Key.CurrentMinutes.ToString()) ? (matches.Key.MinutePlusBit == true ? matches.Key.CurrentMinutes + "+" + (!string.IsNullOrEmpty(matches.Key.PlusMinutes) ? matches.Key.PlusMinutes : string.Empty) : matches.Key.CurrentMinutes.ToString()) : "0",
                                                              HomeTeam = (from _homeTeam in matchesByRoundDBModel.ToList()
                                                                          where _homeTeam.MatchId == matches.Key.MatchId
                                                                              && _homeTeam.HomeAway == true
                                                                          select new MatchCompetitorModel
                                                                          {
                                                                              MatchCompetitorId = _homeTeam.MatchCompetitorId,
                                                                              TeamId = _homeTeam.CompetitorId,
                                                                              TeamName = _homeTeam.Competitor,
                                                                              TeamShortName = !string.IsNullOrEmpty(_homeTeam.CompetitorShortName) ? _homeTeam.CompetitorShortName : _homeTeam.Competitor
                                                                          }).FirstOrDefault(),
                                                              AwayTeam = (from _awayTeam in matchesByRoundDBModel.ToList()
                                                                          where _awayTeam.MatchId == matches.Key.MatchId
                                                                                  && _awayTeam.HomeAway == false
                                                                          select new MatchCompetitorModel
                                                                          {
                                                                              MatchCompetitorId = _awayTeam.MatchCompetitorId,
                                                                              TeamId = _awayTeam.CompetitorId,
                                                                              TeamName = _awayTeam.Competitor,
                                                                              TeamShortName = !string.IsNullOrEmpty(_awayTeam.CompetitorShortName) ? _awayTeam.CompetitorShortName : _awayTeam.Competitor
                                                                          }).FirstOrDefault(),
                                                              MatchScore = (from _matchScore in matchesByRoundDBModel.ToList()
                                                                            where _matchScore.MatchId == matches.Key.MatchId
                                                                                    && _matchScore.ScoreInfoTypeId != null
                                                                            select new MatchScoreModel
                                                                            {
                                                                                ScoreInfoTypeId = (Int16)_matchScore.ScoreInfoTypeId,
                                                                                HomeScore = _matchScore.HomeScore,
                                                                                AwayScore = _matchScore.AwayScore,
                                                                                HomeTieBreakScore = !string.IsNullOrEmpty(_matchScore.HomeTieBreakScore) ? _matchScore.HomeTieBreakScore : string.Empty,
                                                                                AwayTieBreakScore = !string.IsNullOrEmpty(_matchScore.AwayTieBreakScore) ? _matchScore.AwayTieBreakScore : string.Empty
                                                                            }).FirstOrDefault()
                                                          }).ToList()
                                           }).ToList();
            }
            return contestMatchesListModel;
        }

        public static List<ContestRoundsModel> GetMatchesByContestRound(GlobalParametersModel globalParametersModel)
        {
            List<ContestRoundsModel> ContestRoundsModel = DBManager.Execute<ContestRoundsModel>("GoalccAPI_GetContestGroupRoundById", new
            {
                ContestGroupId = globalParametersModel.ContestGroupId,
                LanguageCode = globalParametersModel.LanguageCode
            });

            #region  ADO.Net Code
            // List<ContestRoundsModel> ContestRoundsModel = new List<ContestRoundsModel>();
            //using (DataTable _dataTable = new DataTable())
            //{
            //    using (SqlConnection _connection = new SqlConnection(_connectionString))
            //    {
            //        _connection.Open();
            //        using (SqlCommand _command = new SqlCommand("GoalccAPI_GetContestGroupRoundById", _connection))
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

            return ContestRoundsModel.OrderBy(cg => cg.IsRound == 0 ? cg.ContestGroupRoundId : cg.Round.Any(Char.IsDigit) ? Convert.ToInt32(Regex.Match(cg.Round, @"\d+").Value) : cg.ContestGroupRoundId).ToList();
        }

        public static List<ContestRoundsModel> GetContestRoundsById(GlobalParametersModel globalParametersModel)
        {
            List<ContestRoundsModel> ContestRoundsModel = DBManager.Execute<ContestRoundsModel>("GoalccAPI_GetContestGroupRoundById", new
            {
                ContestGroupId = globalParametersModel.ContestGroupId,
                LanguageCode = globalParametersModel.LanguageCode
            });

            #region  ADO.Net Code
            // List<ContestRoundsModel> ContestRoundsModel = new List<ContestRoundsModel>();
            //using (DataTable _dataTable = new DataTable())
            //{
            //    using (SqlConnection _connection = new SqlConnection(_connectionString))
            //    {
            //        _connection.Open();
            //        using (SqlCommand _command = new SqlCommand("GoalccAPI_GetContestGroupRoundById", _connection))
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

            return ContestRoundsModel.OrderBy(cg => cg.IsRound == 0 ? cg.ContestGroupRoundId : cg.Round.Any(Char.IsDigit) ? Convert.ToInt32(Regex.Match(cg.Round, @"\d+").Value) : cg.ContestGroupRoundId).ToList();
        }

        public static ContestGroupStatsModel GetContestGroupStats(GlobalParametersModel globalParametersModel)
        {
            if (globalParametersModel.ContestGroupId == 0)
                return null;

            List<ContestGroupStatsDBModel> contestGroupStatsDBModel = DBManager.Execute<ContestGroupStatsDBModel>("GoalccAPI_GetContestGroupStatsByContestGroupId", new
            {
                ContestGroupId = globalParametersModel.ContestGroupId,
                LanguageCode = globalParametersModel.LanguageCode
            });
            ContestGroupStatsModel contestGroupStatsModel = new ContestGroupStatsModel();
            contestGroupStatsDBModel = (from _stats in contestGroupStatsDBModel
                                        select new ContestGroupStatsDBModel
                                        {
                                            CountryId = _stats.CountryId,
                                            CountryName = _stats.CountryName,
                                            ContestGroupId = _stats.ContestGroupId,
                                            ContestGroupName = _stats.ContestGroupName,
                                            SeasonId = _stats.SeasonId,
                                            SeasonName = _stats.SeasonName,
                                            HomeResult = _stats.HomeResult != null ? _stats.HomeResult : (decimal?)null,
                                            DrawResult = _stats.DrawResult != null ? _stats.DrawResult : (decimal?)null,
                                            AwayResult = _stats.AwayResult != null ? _stats.AwayResult : (decimal?)null,
                                            Over15Value = _stats.Over15Value != null ? _stats.Over15Value : (decimal?)null,
                                            Over25Value = _stats.Over25Value != null ? _stats.Over25Value : (decimal?)null,
                                            Over35Value = _stats.Over35Value != null ? _stats.Over35Value : (decimal?)null,
                                            TeamIdHGPM = _stats.TeamIdHGPM != null ? _stats.TeamIdHGPM : (int?)null,
                                            TeamLGPM = _stats.TeamLGPM,
                                            HighestHGPM = _stats.HighestHGPM != null ? _stats.HighestHGPM : (decimal?)null,
                                            TeamIdLGPM = _stats.TeamIdLGPM != null ? _stats.TeamIdLGPM : (int?)null,
                                            TeamHGPM = _stats.TeamHGPM,
                                            HighestLGPM = _stats.HighestLGPM != null ? _stats.HighestLGPM : (decimal?)null,
                                            Over05Value1H = _stats.Over05Value1H != null ? _stats.Over05Value1H : (decimal?)null,
                                            Over15Value1H = _stats.Over15Value1H != null ? _stats.Over15Value1H : (decimal?)null,
                                            Over25Value1H = _stats.Over25Value1H != null ? _stats.Over25Value1H : (decimal?)null,
                                            TeamIdHGPM1H = _stats.TeamIdHGPM1H != null ? _stats.TeamIdHGPM1H : (int?)null,
                                            TeamHGPM1H = _stats.TeamHGPM1H,
                                            HighestHGPM1H = _stats.HighestHGPM1H != null ? _stats.HighestHGPM1H : (decimal?)null,
                                            TeamIdLGPM1H = _stats.TeamIdLGPM1H != null ? _stats.TeamIdLGPM1H : (int?)null,
                                            TeamLGPM1H = _stats.TeamLGPM1H,
                                            HighestLGPM1H = _stats.HighestLGPM1H != null ? _stats.HighestLGPM1H : (decimal?)null,
                                            BTTSPercentage = _stats.BTTSPercentage != null ? _stats.BTTSPercentage : (decimal?)null,
                                            TeamIdBTTSM1 = _stats.TeamIdBTTSM1 != null ? _stats.TeamIdBTTSM1 : (int?)null,
                                            TeamBTTSM1 = _stats.TeamBTTSM1,
                                            HighestBTTSM1 = _stats.HighestBTTSM1 != null ? _stats.HighestBTTSM1 : (decimal?)null,
                                            MatchesPlayedBTTSM1 = _stats.MatchesPlayedBTTSM1 != null ? _stats.MatchesPlayedBTTSM1 : (int?)null,
                                            TeamIdBTTSM2 = _stats.TeamIdBTTSM2 != null ? _stats.TeamIdBTTSM2 : (int?)null,
                                            TeamBTTSM2 = _stats.TeamBTTSM2,
                                            HighestBTTSM2 = _stats.HighestBTTSM2 != null ? _stats.HighestBTTSM2 : (decimal?)null,
                                            MatchesPlayedBTTSM2 = _stats.MatchesPlayedBTTSM2 != null ? _stats.MatchesPlayedBTTSM2 : (int?)null,
                                            TeamIdBTTSM3 = _stats.TeamIdBTTSM3 != null ? _stats.TeamIdBTTSM3 : (int?)null,
                                            TeamBTTSM3 = _stats.TeamBTTSM3,
                                            HighestBTTSM3 = _stats.HighestBTTSM3 != null ? _stats.HighestBTTSM3 : (decimal?)null,
                                            MatchesPlayedBTTSM3 = _stats.MatchesPlayedBTTSM3 != null ? _stats.TeamIdBTTSM3 : (int?)null,
                                            TeamIdBTTSF1 = _stats.TeamIdBTTSF1 != null ? _stats.TeamIdBTTSF1 : (int?)null,
                                            TeamBTTSF1 = _stats.TeamBTTSF1,
                                            HighestBTTSF1 = _stats.HighestBTTSF1 != null ? _stats.HighestBTTSF1 : (decimal?)null,
                                            MatchesPlayedBTTSF1 = _stats.MatchesPlayedBTTSF1 != null ? _stats.MatchesPlayedBTTSF1 : (int?)null,
                                            TeamIdCSM1 = _stats.TeamIdCSM1 != null ? _stats.TeamIdCSM1 : (int?)null,
                                            TeamCSM1 = _stats.TeamCSM1,
                                            HighestCSM1 = _stats.HighestCSM1 != null ? _stats.HighestCSM1 : (decimal?)null,
                                            MatchesPlayedCSM1 = _stats.MatchesPlayedCSM1 != null ? _stats.MatchesPlayedCSM1 : (int?)null,
                                            TeamIdCSM2 = _stats.TeamIdCSM2 != null ? _stats.TeamIdCSM2 : (int?)null,
                                            TeamCSM2 = _stats.TeamCSM2,
                                            HighestCSM2 = _stats.HighestCSM2 != null ? _stats.HighestCSM2 : (decimal?)null,
                                            MatchesPlayedCSM2 = _stats.MatchesPlayedCSM2 != null ? _stats.MatchesPlayedCSM2 : (int?)null,
                                            TeamIdCSF1 = _stats.TeamIdCSF1 != null ? _stats.TeamIdCSF1 : (int?)null,
                                            TeamCSF1 = _stats.TeamCSF1,
                                            HighestCSF1 = _stats.HighestCSF1 != null ? _stats.HighestCSF1 : (decimal?)null,
                                            MatchesPlayedCSF1 = _stats.MatchesPlayedCSF1 != null ? _stats.MatchesPlayedCSF1 : (int?)null,
                                            TeamIdFTSM1 = _stats.TeamIdFTSM1 != null ? _stats.TeamIdFTSM1 : (int?)null,
                                            TeamFTSM1 = _stats.TeamFTSM1,
                                            HighestFTSM1 = _stats.HighestFTSM1 != null ? _stats.HighestFTSM1 : (decimal?)null,
                                            MatchesPlayedFTSM1 = _stats.MatchesPlayedFTSM1 != null ? _stats.MatchesPlayedFTSM1 : (int?)null,
                                            TeamIdFTSF1 = _stats.TeamIdFTSF1 != null ? _stats.TeamIdFTSF1 : (int?)null,
                                            TeamFTSF1 = _stats.TeamFTSF1,
                                            HighestFTSF1 = _stats.HighestFTSF1 != null ? _stats.HighestFTSF1 : (decimal?)null,
                                            MatchesPlayedFTSF1 = _stats.MatchesPlayedFTSF1 != null ? _stats.MatchesPlayedFTSF1 : (int?)null,
                                            TeamIdSTFGM1 = _stats.TeamIdSTFGM1 != null ? _stats.TeamIdSTFGM1 : (int?)null,
                                            TeamSTFGM1 = _stats.TeamSTFGM1,
                                            HighestSTFGM1 = _stats.HighestSTFGM1 != null ? _stats.HighestSTFGM1 : (decimal?)null,
                                            MatchesPlayedSTFGM1 = _stats.MatchesPlayedSTFGM1 != null ? _stats.MatchesPlayedSTFGM1 : (int?)null,
                                            TeamIdSTFGF1 = _stats.TeamIdSTFGF1 != null ? _stats.TeamIdSTFGF1 : (int?)null,
                                            TeamSTFGF1 = _stats.TeamSTFGF1,
                                            HighestSTFGF1 = _stats.HighestSTFGF1 != null ? _stats.HighestSTFGF1 : (decimal?)null,
                                            MatchesPlayedSTFGF1 = _stats.MatchesPlayedSTFGF1 != null ? _stats.MatchesPlayedSTFGF1 : (int?)null,
                                            TeamIdCLM1 = _stats.TeamIdCLM1 != null ? _stats.TeamIdCLM1 : (int?)null,
                                            TeamCLM1 = _stats.TeamCLM1,
                                            HighestCLM1 = _stats.HighestCLM1 != null ? _stats.HighestCLM1 : (decimal?)null,
                                            MatchesPlayedCLM1 = _stats.MatchesPlayedCLM1 != null ? _stats.MatchesPlayedCLM1 : (int?)null,
                                            TeamIdCWM1 = _stats.TeamIdCWM1 != null ? _stats.TeamIdCWM1 : (int?)null,
                                            TeamCWM1 = _stats.TeamCWM1,
                                            HighestCWM1 = _stats.HighestCWM1 != null ? _stats.HighestCWM1 : (decimal?)null,
                                            MatchesPlayedCWM1 = _stats.MatchesPlayedCWM1 != null ? _stats.MatchesPlayedCWM1 : (int?)null,
                                            TeamIdCWAHM1 = _stats.TeamIdCWAHM1 != null ? _stats.TeamIdCWAHM1 : (int?)null,
                                            TeamCWAHM1 = _stats.TeamCWAHM1,
                                            HighestCWAHM1 = _stats.HighestCWAHM1 != null ? _stats.HighestCWAHM1 : (decimal?)null,
                                            MatchesPlayedCWAHM1 = _stats.MatchesPlayedCWAHM1 != null ? _stats.MatchesPlayedCWAHM1 : (int?)null,
                                            TeamIdCWAAM1 = _stats.TeamIdCWAAM1 != null ? _stats.TeamIdCWAAM1 : (int?)null,
                                            TeamCWAAM1 = _stats.TeamCWAAM1,
                                            HighestCWAAM1 = _stats.HighestCWAAM1 != null ? _stats.HighestCWAAM1 : (decimal?)null,
                                            MatchesPlayedCWAAM1 = _stats.MatchesPlayedCWAAM1 != null ? _stats.MatchesPlayedCWAAM1 : (int?)null,
                                            TeamIdMSLWM1 = _stats.TeamIdMSLWM1 != null ? _stats.TeamIdMSLWM1 : (int?)null,
                                            TeamMSLWM1 = _stats.TeamMSLWM1,
                                            HighestMSLWM1 = _stats.HighestMSLWM1 != null ? _stats.HighestMSLWM1 : (decimal?)null,
                                            MatchesPlayedMSLWM1 = _stats.MatchesPlayedMSLWM1 != null ? _stats.MatchesPlayedMSLWM1 : (int?)null,
                                            TeamIdUBM1 = _stats.TeamIdUBM1 != null ? _stats.TeamIdUBM1 : (int?)null,
                                            TeamUBM1 = _stats.TeamUBM1,
                                            HighestUBM1 = _stats.HighestUBM1 != null ? _stats.HighestUBM1 : (decimal?)null,
                                            MatchesPlayedUBM1 = _stats.MatchesPlayedUBM1 != null ? _stats.MatchesPlayedUBM1 : (int?)null,
                                            AwayAvg = _stats.AwayAvg != null ? _stats.AwayAvg : (decimal?)null,
                                            HomeAvg = _stats.HomeAvg != null ? _stats.HomeAvg : (decimal?)null,
                                            TotalAvg = _stats.TotalAvg != null ? _stats.TotalAvg : (decimal?)null
                                        }).ToList();

            contestGroupStatsModel = (from _contestGroup in contestGroupStatsDBModel
                                      select new ContestGroupStatsModel
                                      {
                                          CountryId = _contestGroup.CountryId,
                                          CountryName = _contestGroup.CountryName,
                                          ContestGroupId = _contestGroup.ContestGroupId,
                                          ContestGroupName = _contestGroup.ContestGroupName,
                                          SeasonId = _contestGroup.SeasonId,
                                          SeasonName = _contestGroup.SeasonName,
                                          HomeResult = _contestGroup.HomeResult == null ? (decimal)0 : (decimal)_contestGroup.HomeResult,
                                          DrawResult = _contestGroup.DrawResult == null ? (decimal)0 : (decimal)_contestGroup.DrawResult,
                                          AwayResult = _contestGroup.AwayResult == null ? (decimal)0 : (decimal)_contestGroup.AwayResult,
                                          TotalAvg = _contestGroup.TotalAvg == null ? (decimal)0 : (decimal)_contestGroup.TotalAvg,
                                          HomeAvg = _contestGroup.HomeAvg == null ? (decimal)0 : (decimal)_contestGroup.HomeAvg,
                                          AwayAvg = _contestGroup.AwayAvg == null ? (decimal)0 : (decimal)_contestGroup.AwayAvg,
                                          Over15Value = _contestGroup.Over15Value == null ? (decimal)0 : (decimal)_contestGroup.Over15Value,
                                          Over25Value = _contestGroup.Over25Value == null ? (decimal)0 : (decimal)_contestGroup.Over25Value,
                                          Over35Value = _contestGroup.Over35Value == null ? (decimal)0 : (decimal)_contestGroup.Over35Value,
                                          TeamId_HighestGoalsPerMatch = _contestGroup.TeamIdHGPM == null ? (int)0 : (int)_contestGroup.TeamIdHGPM,
                                          Team_HighestGoalsPerMatch = !string.IsNullOrEmpty(_contestGroup.TeamHGPM) ? _contestGroup.TeamHGPM : string.Empty,
                                          Highest_HighestGoalsPerMatch = _contestGroup.HighestHGPM == null ? (decimal)0 : (decimal)_contestGroup.HighestHGPM,
                                          TeamId_LowestGoalsPerMatch = _contestGroup.TeamIdLGPM == null ? (int)0 : (int)_contestGroup.TeamIdLGPM,
                                          Team_LowestGoalsPerMatch = !string.IsNullOrEmpty(_contestGroup.TeamLGPM) ? _contestGroup.TeamLGPM : string.Empty,
                                          Highest_LowestGoalsPerMatch = _contestGroup.HighestLGPM == null ? (decimal)0 : (decimal)_contestGroup.HighestLGPM,
                                          Over05Value1H = _contestGroup.Over05Value1H == null ? (decimal)0 : (decimal)_contestGroup.Over05Value1H,
                                          Over15Value1H = _contestGroup.Over15Value1H == null ? (decimal)0 : (decimal)_contestGroup.Over15Value1H,
                                          Over25Value1H = _contestGroup.Over25Value1H == null ? (decimal)0 : (decimal)_contestGroup.Over25Value1H,
                                          TeamId_HighestGoalsPerMatchIn1stHalf = _contestGroup.TeamIdHGPM1H == null ? (int)0 : (int)_contestGroup.TeamIdHGPM1H,
                                          Team_HighestGoalsPerMatchIn1stHalf = !string.IsNullOrEmpty(_contestGroup.TeamHGPM1H) ? _contestGroup.TeamHGPM1H : string.Empty,
                                          Highest_HighestGoalsPerMatchIn1stHalf = _contestGroup.HighestHGPM1H == null ? (decimal)0 : (decimal)_contestGroup.HighestHGPM1H,
                                          TeamId_LowestGoalsPerMatchIn1stHalf = _contestGroup.TeamIdLGPM1H == null ? (int)0 : (int)_contestGroup.TeamIdLGPM1H,
                                          Team_LowestGoalsPerMatchIn1stHalf = !string.IsNullOrEmpty(_contestGroup.TeamLGPM1H) ? _contestGroup.TeamLGPM1H : string.Empty,
                                          Highest_LowestGoalsPerMatchIn1stHalf = _contestGroup.HighestLGPM1H == null ? (decimal)0 : (decimal)_contestGroup.HighestLGPM1H,
                                          BTTSPercentage = _contestGroup.BTTSPercentage == null ? (decimal)0 : (decimal)_contestGroup.BTTSPercentage,
                                          TeamId_BothTeamsToScore1 = _contestGroup.TeamIdBTTSM1 == null ? (int)0 : (int)_contestGroup.TeamIdBTTSM1,
                                          Team_BothTeamsToScore1 = !string.IsNullOrEmpty(_contestGroup.TeamBTTSM1) ? _contestGroup.TeamBTTSM1 : string.Empty,
                                          Highest_BothTeamsToScore1 = _contestGroup.HighestBTTSM1 == null ? (decimal)0 : (decimal)_contestGroup.HighestBTTSM1,
                                          MatchesPlayed_BothTeamsToScore1 = _contestGroup.MatchesPlayedBTTSM1 == null ? (int)0 : (int)_contestGroup.MatchesPlayedBTTSM1,
                                          TeamId_BothTeamsToScore2 = _contestGroup.TeamIdBTTSM2 == null ? (int)0 : (int)_contestGroup.TeamIdBTTSM2,
                                          Team_BothTeamsToScore2 = !string.IsNullOrEmpty(_contestGroup.TeamBTTSM2) ? _contestGroup.TeamBTTSM2 : string.Empty,
                                          Highest_BothTeamsToScore2 = _contestGroup.HighestBTTSM2 == null ? (decimal)0 : (decimal)_contestGroup.HighestBTTSM2,
                                          MatchesPlayed_BothTeamsToScore2 = _contestGroup.MatchesPlayedBTTSM2 == null ? (int)0 : (int)_contestGroup.MatchesPlayedBTTSM2,
                                          TeamId_BothTeamsToScore3 = _contestGroup.TeamIdBTTSM3 == null ? (int)0 : (int)_contestGroup.TeamIdBTTSM3,
                                          Team_BothTeamsToScore3 = !string.IsNullOrEmpty(_contestGroup.TeamBTTSM3) ? _contestGroup.TeamBTTSM3 : string.Empty,
                                          Highest_BothTeamsToScore3 = _contestGroup.HighestBTTSM3 == null ? (decimal)0 : (decimal)_contestGroup.HighestBTTSM3,
                                          MatchesPlayed_BothTeamsToScore3 = _contestGroup.MatchesPlayedBTTSM3 == null ? (int)0 : (int)_contestGroup.MatchesPlayedBTTSM3,
                                          TeamId_LowestBothTeamsToScore1 = _contestGroup.TeamIdBTTSF1 == null ? (int)0 : (int)_contestGroup.TeamIdBTTSF1,
                                          Team_LowestBothTeamsToScore1 = !string.IsNullOrEmpty(_contestGroup.TeamBTTSF1) ? _contestGroup.TeamBTTSF1 : string.Empty,
                                          Highest_LowestBothTeamsToScore1 = _contestGroup.HighestBTTSF1 == null ? (decimal)0 : (decimal)_contestGroup.HighestBTTSF1,
                                          MatchesPlayed_LowestBothTeamsToScore1 = _contestGroup.MatchesPlayedBTTSF1 == null ? (int)0 : (int)_contestGroup.MatchesPlayedBTTSF1,
                                          TeamId_HighestCleanSheets1 = _contestGroup.TeamIdCSM1 == null ? (int)0 : (int)_contestGroup.TeamIdCSM1,
                                          Team_HighestCleanSheets1 = !string.IsNullOrEmpty(_contestGroup.TeamCSM1) ? _contestGroup.TeamCSM1 : string.Empty,
                                          Highest_HighestCleanSheets1 = _contestGroup.HighestCSM1 == null ? (decimal)0 : (decimal)_contestGroup.HighestCSM1,
                                          MatchesPlayed_HighestCleanSheets1 = _contestGroup.MatchesPlayedCSM1 == null ? (int)0 : (int)_contestGroup.MatchesPlayedCSM1,
                                          TeamId_HighestCleanSheets2 = _contestGroup.TeamIdCSM2 == null ? (int)0 : (int)_contestGroup.TeamIdCSM2,
                                          Team_HighestCleanSheets2 = !string.IsNullOrEmpty(_contestGroup.TeamCSM2) ? _contestGroup.TeamCSM2 : string.Empty,
                                          Highest_HighestCleanSheets2 = _contestGroup.HighestCSM2 == null ? (decimal)0 : (decimal)_contestGroup.HighestCSM2,
                                          MatchesPlayed_HighestCleanSheets2 = _contestGroup.MatchesPlayedCSM2 == null ? (int)0 : (int)_contestGroup.MatchesPlayedCSM2,
                                          TeamId_LowestCleanSheets1 = _contestGroup.TeamIdCSF1 == null ? (int)0 : (int)_contestGroup.TeamIdCSF1,
                                          Team_LowestCleanSheets1 = !string.IsNullOrEmpty(_contestGroup.TeamCSF1) ? _contestGroup.TeamCSF1 : string.Empty,
                                          Highest_LowestCleanSheets1 = _contestGroup.HighestCSF1 == null ? (decimal)0 : (decimal)_contestGroup.HighestCSF1,
                                          MatchesPlayed_LowestCleanSheets1 = _contestGroup.MatchesPlayedCSF1 == null ? (int)0 : (int)_contestGroup.MatchesPlayedCSF1,
                                          TeamId_HighestFailedToScore1 = _contestGroup.TeamIdFTSM1 == null ? (int)0 : (int)_contestGroup.TeamIdFTSM1,
                                          Team_HighestFailedToScore1 = !string.IsNullOrEmpty(_contestGroup.TeamFTSM1) ? _contestGroup.TeamFTSM1 : string.Empty,
                                          Highest_HighestFailedToScore1 = _contestGroup.HighestFTSM1 == null ? (decimal)0 : (decimal)_contestGroup.HighestFTSM1,
                                          MatchesPlayed_HighestFailedToScore1 = _contestGroup.MatchesPlayedFTSM1 == null ? (int)0 : (int)_contestGroup.MatchesPlayedFTSM1,
                                          TeamId_LowestFailedToScore1 = _contestGroup.TeamIdFTSF1 == null ? (int)0 : (int)_contestGroup.TeamIdFTSF1,
                                          Team_LowestFailedToScore1 = !string.IsNullOrEmpty(_contestGroup.TeamFTSF1) ? _contestGroup.TeamFTSF1 : string.Empty,
                                          Highest_LowestFailedToScore1 = _contestGroup.HighestFTSF1 == null ? (decimal)0 : (decimal)_contestGroup.HighestFTSF1,
                                          MatchesPlayed_LowestFailedToScore1 = _contestGroup.MatchesPlayedFTSF1 == null ? (int)0 : (int)_contestGroup.MatchesPlayedFTSF1,
                                          TeamId_HighestConsecutiveLoses1 = _contestGroup.TeamIdCLM1 == null ? (int)0 : (int)_contestGroup.TeamIdCLM1,
                                          Team_HighestConsecutiveLoses1 = !string.IsNullOrEmpty(_contestGroup.TeamCLM1) ? _contestGroup.TeamCLM1 : string.Empty,
                                          Highest_HighestConsecutiveLoses1 = _contestGroup.HighestCLM1 == null ? (decimal)0 : (decimal)_contestGroup.HighestCLM1,
                                          MatchesPlayed_HighestConsecutiveLoses1 = _contestGroup.MatchesPlayedCLM1 == null ? (int)0 : (int)_contestGroup.MatchesPlayedCLM1,
                                          TeamId_HighestConsecutiveWins1 = _contestGroup.TeamIdCWM1 == null ? (int)0 : (int)_contestGroup.TeamIdCWM1,
                                          Team_HighestConsecutiveWins1 = !string.IsNullOrEmpty(_contestGroup.TeamCWM1) ? _contestGroup.TeamCWM1 : string.Empty,
                                          Highest_HighestConsecutiveWins1 = _contestGroup.HighestCWM1 == null ? (decimal)0 : (decimal)_contestGroup.HighestCWM1,
                                          MatchesPlayed_HighestConsecutiveWins1 = _contestGroup.MatchesPlayedCWM1 == null ? (int)0 : (int)_contestGroup.MatchesPlayedCWM1,
                                          TeamId_HighestConsecutiveWinsAsHome1 = _contestGroup.TeamIdCWAHM1 == null ? (int)0 : (int)_contestGroup.TeamIdCWAHM1,
                                          Team_HighestConsecutiveWinsAsHome1 = !string.IsNullOrEmpty(_contestGroup.TeamCWAHM1) ? _contestGroup.TeamCWAHM1 : string.Empty,
                                          Highest_HighestConsecutiveWinsAsHome1 = _contestGroup.HighestCWAHM1 == null ? (decimal)0 : (decimal)_contestGroup.HighestCWAHM1,
                                          MatchesPlayed_HighestConsecutiveWinsAsHome1 = _contestGroup.MatchesPlayedCWAHM1 == null ? (int)0 : (int)_contestGroup.MatchesPlayedCWAHM1,
                                          TeamId_HighestConsecutiveWinsAsAway1 = _contestGroup.TeamIdCWAAM1 == null ? (int)0 : (int)_contestGroup.TeamIdCWAAM1,
                                          Team_HighestConsecutiveWinsAsAway1 = !string.IsNullOrEmpty(_contestGroup.TeamCWAAM1) ? _contestGroup.TeamCWAAM1 : string.Empty,
                                          Highest_HighestConsecutiveWinsAsAway1 = _contestGroup.HighestCWAAM1 == null ? (decimal)0 : (decimal)_contestGroup.HighestCWAAM1,
                                          MatchesPlayed_HighestConsecutiveWinsAsAway1 = _contestGroup.MatchesPlayedCWAAM1 == null ? (int)0 : (int)_contestGroup.MatchesPlayedCWAAM1,
                                          TeamId_HighestScoreTheFirstGoal1 = _contestGroup.TeamIdSTFGM1 == null ? (int)0 : (int)_contestGroup.TeamIdSTFGM1,
                                          Team_HighestScoreTheFirstGoal1 = !string.IsNullOrEmpty(_contestGroup.TeamSTFGM1) ? _contestGroup.TeamSTFGM1 : string.Empty,
                                          Highest_HighestScoreTheFirstGoal1 = _contestGroup.HighestSTFGM1 == null ? (decimal)0 : (decimal)_contestGroup.HighestSTFGM1,
                                          MatchesPlayed_HighestScoreTheFirstGoal1 = _contestGroup.MatchesPlayedSTFGM1 == null ? (int)0 : (int)_contestGroup.MatchesPlayedSTFGM1,
                                          TeamId_FewestScoreTheFirstGoal1 = _contestGroup.TeamIdSTFGF1 == null ? (int)0 : (int)_contestGroup.TeamIdSTFGF1,
                                          Team_FewestScoreTheFirstGoal1 = !string.IsNullOrEmpty(_contestGroup.TeamSTFGF1) ? _contestGroup.TeamSTFGF1 : string.Empty,
                                          Highest_FewestScoreTheFirstGoal1 = _contestGroup.HighestSTFGF1 == null ? (decimal)0 : (decimal)_contestGroup.HighestSTFGF1,
                                          MatchesPlayed_FewestScoreTheFirstGoal1 = _contestGroup.MatchesPlayedSTFGF1 == null ? (int)0 : (int)_contestGroup.MatchesPlayedSTFGF1,
                                          TeamId_MatchesSinceLastWin1 = _contestGroup.TeamIdMSLWM1 == null ? (int)0 : (int)_contestGroup.TeamIdMSLWM1,
                                          Team_MatchesSinceLastWin1 = !string.IsNullOrEmpty(_contestGroup.TeamMSLWM1) ? _contestGroup.TeamMSLWM1 : string.Empty,
                                          Highest_MatchesSinceLastWin1 = _contestGroup.HighestMSLWM1 == null ? (decimal)0 : (decimal)_contestGroup.HighestMSLWM1,
                                          MatchesPlayed_MatchesSinceLastWin1 = _contestGroup.MatchesPlayedMSLWM1 == null ? (int)0 : (int)_contestGroup.MatchesPlayedMSLWM1,
                                          TeamId_Unbeaten1 = _contestGroup.TeamIdUBM1 == null ? (int)0 : (int)_contestGroup.TeamIdUBM1,
                                          Team_Unbeaten1 = !string.IsNullOrEmpty(_contestGroup.TeamUBM1) ? _contestGroup.TeamUBM1 : string.Empty,
                                          Highest_Unbeaten1 = _contestGroup.HighestUBM1 == null ? (decimal)0 : (decimal)_contestGroup.HighestUBM1,
                                          MatchesPlayed_Unbeaten1 = _contestGroup.MatchesPlayedUBM1 == null ? (int)0 : (int)_contestGroup.MatchesPlayedUBM1
                                      }).FirstOrDefault();

            #region  ADO.Net Code
            //ContestGroupStatsDBModel contestGroupStatsDBModel = new ContestGroupStatsDBModel();
            //using (DataTable _dataTable = new DataTable())
            //{
            //    using (SqlConnection _connection = new SqlConnection(_connectionString))
            //    {
            //        _connection.Open();
            //        using (SqlCommand _command = new SqlCommand("GoalccAPI_GetContestGroupStatsByContestGroupId ", _connection))
            //        {
            //            _command.CommandType = CommandType.StoredProcedure;
            //            _command.Parameters.Add(new SqlParameter("@ContestGroupId", contestGroupId));
            //            using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(_command))
            //            {
            //                sqlDataAdapter.Fill(_dataTable);
            //            }
            //        }
            //    }

            //    foreach (DataRow _dataRow in _dataTable.Rows)
            //    {
            //        contestGroupStatsDBModel.CountryId = Convert.ToInt16(_dataRow["CountryId"]);
            //        contestGroupStatsDBModel.CountryName = _dataRow["CountryName"].ToString();
            //        contestGroupStatsDBModel.ContestGroupId = Convert.ToInt32(_dataRow["ContestGroupId"]);
            //        contestGroupStatsDBModel.ContestGroupName = _dataRow["ContestGroupName"].ToString();
            //        contestGroupStatsDBModel.SeasonId = Convert.ToInt16(_dataRow["SeasonId"]);
            //        contestGroupStatsDBModel.SeasonName = _dataRow["SeasonName"].ToString();
            //        contestGroupStatsDBModel.HomeResult = _dataRow["HomeResult"] != DBNull.Value ? Convert.ToDecimal(_dataRow["HomeResult"]) : (decimal?)null;
            //        contestGroupStatsDBModel.DrawResult = _dataRow["DrawResult"] != DBNull.Value ? Convert.ToDecimal(_dataRow["DrawResult"]) : (decimal?)null;
            //        contestGroupStatsDBModel.AwayResult = _dataRow["AwayResult"] != DBNull.Value ? Convert.ToDecimal(_dataRow["AwayResult"]) : (decimal?)null;
            //        contestGroupStatsDBModel.Over15Value = _dataRow["Over15Value"] != DBNull.Value ? Convert.ToDecimal(_dataRow["Over15Value"]) : (decimal?)null;
            //        contestGroupStatsDBModel.Over25Value = _dataRow["Over25Value"] != DBNull.Value ? Convert.ToDecimal(_dataRow["Over25Value"]) : (decimal?)null;
            //        contestGroupStatsDBModel.Over35Value = _dataRow["Over35Value"] != DBNull.Value ? Convert.ToDecimal(_dataRow["Over35Value"]) : (decimal?)null;
            //        contestGroupStatsDBModel.TeamIdHGPM = _dataRow["TeamIdHGPM"] != DBNull.Value ? Convert.ToInt32(_dataRow["TeamIdHGPM"]) : (int?)null;
            //        contestGroupStatsDBModel.TeamHGPM = _dataRow["TeamHGPM"].ToString();
            //        contestGroupStatsDBModel.HighestHGPM = _dataRow["HighestHGPM"] != DBNull.Value ? Convert.ToDecimal(_dataRow["HighestHGPM"]) : (decimal?)null;
            //        contestGroupStatsDBModel.TeamIdLGPM = _dataRow["TeamIdLGPM"] != DBNull.Value ? Convert.ToInt32(_dataRow["TeamIdLGPM"]) : (int?)null;
            //        contestGroupStatsDBModel.TeamLGPM = _dataRow["TeamLGPM"].ToString();
            //        contestGroupStatsDBModel.HighestLGPM = _dataRow["HighestLGPM"] != DBNull.Value ? Convert.ToDecimal(_dataRow["HighestLGPM"]) : (decimal?)null;
            //        contestGroupStatsDBModel.Over05Value1H = _dataRow["Over05Value1H"] != DBNull.Value ? Convert.ToDecimal(_dataRow["Over05Value1H"]) : (decimal?)null;
            //        contestGroupStatsDBModel.Over15Value1H = _dataRow["Over15Value1H"] != DBNull.Value ? Convert.ToDecimal(_dataRow["Over15Value1H"]) : (decimal?)null;
            //        contestGroupStatsDBModel.Over25Value1H = _dataRow["Over25Value1H"] != DBNull.Value ? Convert.ToDecimal(_dataRow["Over25Value1H"]) : (decimal?)null;
            //        contestGroupStatsDBModel.TeamIdHGPM1H = _dataRow["TeamIdHGPM1H"] != DBNull.Value ? Convert.ToInt32(_dataRow["TeamIdHGPM1H"]) : (int?)null;
            //        contestGroupStatsDBModel.TeamHGPM = _dataRow["TeamHGPM"].ToString();
            //        contestGroupStatsDBModel.HighestHGPM1H = _dataRow["HighestHGPM1H"] != DBNull.Value ? Convert.ToDecimal(_dataRow["HighestHGPM1H"]) : (decimal?)null;
            //        contestGroupStatsDBModel.TeamIdLGPM1H = _dataRow["TeamIdLGPM1H"] != DBNull.Value ? Convert.ToInt32(_dataRow["TeamIdLGPM1H"]) : (int?)null;
            //        contestGroupStatsDBModel.TeamLGPM1H = _dataRow["TeamLGPM1H"].ToString();
            //        contestGroupStatsDBModel.HighestLGPM1H = _dataRow["HighestLGPM1H"] != DBNull.Value ? Convert.ToDecimal(_dataRow["HighestLGPM1H"]) : (decimal?)null;
            //        contestGroupStatsDBModel.BTTSPercentage = _dataRow["BTTSPercentage"] != DBNull.Value ? Convert.ToDecimal(_dataRow["BTTSPercentage"]) : (decimal?)null;
            //        contestGroupStatsDBModel.TeamIdBTTSM1 = _dataRow["TeamIdBTTSM1"] != DBNull.Value ? Convert.ToInt32(_dataRow["TeamIdBTTSM1"]) : (int?)null;
            //        contestGroupStatsDBModel.TeamBTTSM1 = _dataRow["TeamBTTSM1"].ToString();
            //        contestGroupStatsDBModel.HighestBTTSM1 = _dataRow["HighestBTTSM1"] != DBNull.Value ? Convert.ToDecimal(_dataRow["HighestBTTSM1"]) : (decimal?)null;
            //        contestGroupStatsDBModel.MatchesPlayedBTTSM1 = _dataRow["MatchesPlayedBTTSM1"] != DBNull.Value ? Convert.ToInt32(_dataRow["MatchesPlayedBTTSM1"]) : (int?)null;
            //        contestGroupStatsDBModel.TeamIdBTTSM2 = _dataRow["TeamIdBTTSM2"] != DBNull.Value ? Convert.ToInt32(_dataRow["TeamIdBTTSM2"]) : (int?)null;
            //        contestGroupStatsDBModel.TeamBTTSM2 = _dataRow["TeamBTTSM2"].ToString();
            //        contestGroupStatsDBModel.HighestBTTSM2 = _dataRow["HighestBTTSM2"] != DBNull.Value ? Convert.ToDecimal(_dataRow["HighestBTTSM2"]) : (decimal?)null;
            //        contestGroupStatsDBModel.MatchesPlayedBTTSM2 = _dataRow["MatchesPlayedBTTSM2"] != DBNull.Value ? Convert.ToInt32(_dataRow["MatchesPlayedBTTSM2"]) : (int?)null;
            //        contestGroupStatsDBModel.TeamIdBTTSM3 = _dataRow["TeamIdBTTSM3"] != DBNull.Value ? Convert.ToInt32(_dataRow["TeamIdBTTSM3"]) : (int?)null; contestGroupStatsDBModel.TeamBTTSM3 = _dataRow["TeamBTTSM3"].ToString();
            //        contestGroupStatsDBModel.HighestBTTSM3 = _dataRow["HighestBTTSM3"] != DBNull.Value ? Convert.ToDecimal(_dataRow["HighestBTTSM3"]) : (decimal?)null;
            //        contestGroupStatsDBModel.MatchesPlayedBTTSM3 = _dataRow["MatchesPlayedBTTSM3"] != DBNull.Value ? Convert.ToInt32(_dataRow["MatchesPlayedBTTSM3"]) : (int?)null;
            //        contestGroupStatsDBModel.TeamIdBTTSF1 = _dataRow["TeamIdBTTSF1"] != DBNull.Value ? Convert.ToInt32(_dataRow["TeamIdBTTSF1"]) : (int?)null;
            //        contestGroupStatsDBModel.TeamBTTSF1 = _dataRow["TeamBTTSF1"].ToString();
            //        contestGroupStatsDBModel.HighestBTTSF1 = _dataRow["HighestBTTSF1"] != DBNull.Value ? Convert.ToDecimal(_dataRow["HighestBTTSF1"]) : (decimal?)null;
            //        contestGroupStatsDBModel.MatchesPlayedBTTSF1 = _dataRow["MatchesPlayedBTTSF1"] != DBNull.Value ? Convert.ToInt32(_dataRow["MatchesPlayedBTTSF1"]) : (int?)null;
            //        contestGroupStatsDBModel.TeamIdCSM1 = _dataRow["TeamIdCSM1"] != DBNull.Value ? Convert.ToInt32(_dataRow["TeamIdCSM1"]) : (int?)null;
            //        contestGroupStatsDBModel.TeamCSM1 = _dataRow["TeamCSM1"].ToString();
            //        contestGroupStatsDBModel.HighestCSM1 = _dataRow["HighestCSM1"] != DBNull.Value ? Convert.ToDecimal(_dataRow["HighestCSM1"]) : (decimal?)null;
            //        contestGroupStatsDBModel.MatchesPlayedCSM1 = _dataRow["MatchesPlayedCSM1"] != DBNull.Value ? Convert.ToInt32(_dataRow["MatchesPlayedCSM1"]) : (int?)null;
            //        contestGroupStatsDBModel.TeamIdCSM2 = _dataRow["TeamIdCSM2"] != DBNull.Value ? Convert.ToInt32(_dataRow["TeamIdCSM2"]) : (int?)null;
            //        contestGroupStatsDBModel.TeamCSM2 = _dataRow["TeamCSM2"].ToString();
            //        contestGroupStatsDBModel.HighestCSM2 = _dataRow["HighestCSM2"] != DBNull.Value ? Convert.ToDecimal(_dataRow["HighestCSM2"]) : (decimal?)null;
            //        contestGroupStatsDBModel.MatchesPlayedCSM2 = _dataRow["MatchesPlayedCSM2"] != DBNull.Value ? Convert.ToInt32(_dataRow["MatchesPlayedCSM2"]) : (int?)null;
            //        contestGroupStatsDBModel.TeamIdCSF1 = _dataRow["TeamIdCSF1"] != DBNull.Value ? Convert.ToInt32(_dataRow["TeamIdCSF1"]) : (int?)null;
            //        contestGroupStatsDBModel.TeamCSF1 = _dataRow["TeamCSF1"].ToString();
            //        contestGroupStatsDBModel.HighestCSF1 = _dataRow["HighestCSF1"] != DBNull.Value ? Convert.ToDecimal(_dataRow["HighestCSF1"]) : (decimal?)null;
            //        contestGroupStatsDBModel.MatchesPlayedCSF1 = _dataRow["MatchesPlayedCSF1"] != DBNull.Value ? Convert.ToInt32(_dataRow["MatchesPlayedCSF1"]) : (int?)null;
            //        contestGroupStatsDBModel.TeamIdFTSM1 = _dataRow["TeamIdFTSM1"] != DBNull.Value ? Convert.ToInt32(_dataRow["TeamIdFTSM1"]) : (int?)null;
            //        contestGroupStatsDBModel.TeamFTSM1 = _dataRow["TeamFTSM1"].ToString();
            //        contestGroupStatsDBModel.HighestFTSM1 = _dataRow["HighestFTSM1"] != DBNull.Value ? Convert.ToDecimal(_dataRow["HighestFTSM1"]) : (decimal?)null;
            //        contestGroupStatsDBModel.MatchesPlayedFTSM1 = _dataRow["MatchesPlayedFTSM1"] != DBNull.Value ? Convert.ToInt32(_dataRow["MatchesPlayedFTSM1"]) : (int?)null;
            //        contestGroupStatsDBModel.TeamIdFTSF1 = _dataRow["TeamIdFTSF1"] != DBNull.Value ? Convert.ToInt32(_dataRow["TeamIdFTSF1"]) : (int?)null;
            //        contestGroupStatsDBModel.TeamFTSF1 = _dataRow["TeamFTSF1"].ToString();
            //        contestGroupStatsDBModel.HighestFTSF1 = _dataRow["HighestFTSF1"] != DBNull.Value ? Convert.ToDecimal(_dataRow["HighestFTSF1"]) : (decimal?)null;
            //        contestGroupStatsDBModel.MatchesPlayedFTSF1 = _dataRow["MatchesPlayedFTSF1"] != DBNull.Value ? Convert.ToInt32(_dataRow["MatchesPlayedFTSF1"]) : (int?)null;
            //        contestGroupStatsDBModel.TeamIdSTFGM1 = _dataRow["TeamIdSTFGM1"] != DBNull.Value ? Convert.ToInt32(_dataRow["TeamIdSTFGM1"]) : (int?)null;
            //        contestGroupStatsDBModel.TeamSTFGM1 = _dataRow["TeamSTFGM1"].ToString();
            //        contestGroupStatsDBModel.HighestSTFGM1 = _dataRow["HighestSTFGM1"] != DBNull.Value ? Convert.ToDecimal(_dataRow["HighestSTFGM1"]) : (decimal?)null;
            //        contestGroupStatsDBModel.MatchesPlayedSTFGM1 = _dataRow["MatchesPlayedSTFGM1"] != DBNull.Value ? Convert.ToInt32(_dataRow["MatchesPlayedSTFGM1"]) : (int?)null;
            //        contestGroupStatsDBModel.TeamIdSTFGF1 = _dataRow["TeamIdSTFGF1"] != DBNull.Value ? Convert.ToInt32(_dataRow["TeamIdSTFGF1"]) : (int?)null;
            //        contestGroupStatsDBModel.TeamSTFGF1 = _dataRow["TeamSTFGF1"].ToString();
            //        contestGroupStatsDBModel.HighestSTFGF1 = _dataRow["HighestSTFGF1"] != DBNull.Value ? Convert.ToDecimal(_dataRow["HighestSTFGF1"]) : (decimal?)null;
            //        contestGroupStatsDBModel.MatchesPlayedSTFGF1 = _dataRow["MatchesPlayedSTFGF1"] != DBNull.Value ? Convert.ToInt32(_dataRow["MatchesPlayedSTFGF1"]) : (int?)null;
            //        contestGroupStatsDBModel.TeamIdCLM1 = _dataRow["TeamIdCLM1"] != DBNull.Value ? Convert.ToInt32(_dataRow["TeamIdCLM1"]) : (int?)null;
            //        contestGroupStatsDBModel.TeamCLM1 = _dataRow["TeamCLM1"].ToString();
            //        contestGroupStatsDBModel.HighestCLM1 = _dataRow["HighestCLM1"] != DBNull.Value ? Convert.ToDecimal(_dataRow["HighestCLM1"]) : (decimal?)null;
            //        contestGroupStatsDBModel.MatchesPlayedCLM1 = _dataRow["MatchesPlayedCLM1"] != DBNull.Value ? Convert.ToInt32(_dataRow["MatchesPlayedCLM1"]) : (int?)null;
            //        contestGroupStatsDBModel.TeamIdCWM1 = _dataRow["TeamIdCWM1"] != DBNull.Value ? Convert.ToInt32(_dataRow["TeamIdCWM1"]) : (int?)null;
            //        contestGroupStatsDBModel.TeamCWM1 = _dataRow["TeamCWM1"].ToString();
            //        contestGroupStatsDBModel.HighestCWM1 = _dataRow["HighestCWM1"] != DBNull.Value ? Convert.ToDecimal(_dataRow["HighestCWM1"]) : (decimal?)null;
            //        contestGroupStatsDBModel.MatchesPlayedCWM1 = _dataRow["MatchesPlayedCWM1"] != DBNull.Value ? Convert.ToInt32(_dataRow["MatchesPlayedCWM1"]) : (int?)null;
            //        contestGroupStatsDBModel.TeamIdCWAHM1 = _dataRow["TeamIdCWAHM1"] != DBNull.Value ? Convert.ToInt32(_dataRow["TeamIdCWAHM1"]) : (int?)null;
            //        contestGroupStatsDBModel.TeamCWAHM1 = _dataRow["TeamCWAHM1"].ToString();
            //        contestGroupStatsDBModel.HighestCWAHM1 = _dataRow["HighestCWAHM1"] != DBNull.Value ? Convert.ToDecimal(_dataRow["HighestCWAHM1"]) : (decimal?)null;
            //        contestGroupStatsDBModel.MatchesPlayedCWAHM1 = _dataRow["MatchesPlayedCWAHM1"] != DBNull.Value ? Convert.ToInt32(_dataRow["MatchesPlayedCWAHM1"]) : (int?)null;
            //        contestGroupStatsDBModel.TeamIdCWAAM1 = _dataRow["TeamIdCWAAM1"] != DBNull.Value ? Convert.ToInt32(_dataRow["TeamIdCWAAM1"]) : (int?)null;
            //        contestGroupStatsDBModel.TeamCWAAM1 = _dataRow["TeamCWAAM1"].ToString();
            //        contestGroupStatsDBModel.HighestCWAAM1 = _dataRow["HighestCWAAM1"] != DBNull.Value ? Convert.ToDecimal(_dataRow["HighestCWAAM1"]) : (decimal?)null;
            //        contestGroupStatsDBModel.MatchesPlayedCWAAM1 = _dataRow["MatchesPlayedCWAAM1"] != DBNull.Value ? Convert.ToInt32(_dataRow["MatchesPlayedCWAAM1"]) : (int?)null;
            //        contestGroupStatsDBModel.TeamIdMSLWM1 = _dataRow["TeamIdMSLWM1"] != DBNull.Value ? Convert.ToInt32(_dataRow["TeamIdMSLWM1"]) : (int?)null;
            //        contestGroupStatsDBModel.TeamMSLWM1 = _dataRow["TeamMSLWM1"].ToString();
            //        contestGroupStatsDBModel.HighestMSLWM1 = _dataRow["HighestMSLWM1"] != DBNull.Value ? Convert.ToDecimal(_dataRow["HighestMSLWM1"]) : (decimal?)null;
            //        contestGroupStatsDBModel.MatchesPlayedMSLWM1 = _dataRow["MatchesPlayedMSLWM1"] != DBNull.Value ? Convert.ToInt32(_dataRow["MatchesPlayedMSLWM1"]) : (int?)null;
            //        contestGroupStatsDBModel.TeamIdUBM1 = _dataRow["TeamIdUBM1"] != DBNull.Value ? Convert.ToInt32(_dataRow["TeamIdUBM1"]) : (int?)null;
            //        contestGroupStatsDBModel.TeamUBM1 = _dataRow["TeamUBM1"].ToString();
            //        contestGroupStatsDBModel.HighestUBM1 = _dataRow["HighestUBM1"] != DBNull.Value ? Convert.ToDecimal(_dataRow["HighestUBM1"]) : (decimal?)null;
            //        contestGroupStatsDBModel.MatchesPlayedUBM1 = _dataRow["MatchesPlayedUBM1"] != DBNull.Value ? Convert.ToInt32(_dataRow["MatchesPlayedUBM1"]) : (int?)null;
            //        contestGroupStatsDBModel.HomeAvg = _dataRow["HomeAvg"] != DBNull.Value ? Convert.ToDecimal(_dataRow["HomeAvg"]) : (decimal?)null;
            //        contestGroupStatsDBModel.AwayAvg = _dataRow["AwayAvg"] != DBNull.Value ? Convert.ToDecimal(_dataRow["AwayAvg"]) : (decimal?)null;
            //        contestGroupStatsDBModel.TotalAvg = _dataRow["TotalAvg"] != DBNull.Value ? Convert.ToDecimal(_dataRow["TotalAvg"]) : (decimal?)null;
            //    }
            //}

            //contestGroupStatsModel = new ContestGroupStatsModel()
            //{
            //    CountryId = contestGroupStatsDBModel.CountryId,
            //    CountryName = contestGroupStatsDBModel.CountryName,
            //    ContestGroupId = contestGroupStatsDBModel.ContestGroupId,
            //    ContestGroupName = contestGroupStatsDBModel.ContestGroupName,
            //    SeasonId = contestGroupStatsDBModel.SeasonId,
            //    SeasonName = contestGroupStatsDBModel.SeasonName,
            //    HomeResult = contestGroupStatsDBModel.HomeResult == null ? (decimal)0 : (decimal)contestGroupStatsDBModel.HomeResult,
            //    DrawResult = contestGroupStatsDBModel.DrawResult == null ? (decimal)0 : (decimal)contestGroupStatsDBModel.DrawResult,
            //    AwayResult = contestGroupStatsDBModel.AwayResult == null ? (decimal)0 : (decimal)contestGroupStatsDBModel.AwayResult,
            //    TotalAvg = contestGroupStatsDBModel.TotalAvg == null ? (decimal)0 : (decimal)contestGroupStatsDBModel.TotalAvg,
            //    HomeAvg = contestGroupStatsDBModel.HomeAvg == null ? (decimal)0 : (decimal)contestGroupStatsDBModel.HomeAvg,
            //    AwayAvg = contestGroupStatsDBModel.AwayAvg == null ? (decimal)0 : (decimal)contestGroupStatsDBModel.AwayAvg,
            //    Over15Value = contestGroupStatsDBModel.Over15Value == null ? (decimal)0 : (decimal)contestGroupStatsDBModel.Over15Value,
            //    Over25Value = contestGroupStatsDBModel.Over25Value == null ? (decimal)0 : (decimal)contestGroupStatsDBModel.Over25Value,
            //    Over35Value = contestGroupStatsDBModel.Over35Value == null ? (decimal)0 : (decimal)contestGroupStatsDBModel.Over35Value,
            //    TeamId_HighestGoalsPerMatch = contestGroupStatsDBModel.TeamIdHGPM == null ? (int)0 : (int)contestGroupStatsDBModel.TeamIdHGPM,
            //    Team_HighestGoalsPerMatch = !string.IsNullOrEmpty(contestGroupStatsDBModel.TeamHGPM) ? contestGroupStatsDBModel.TeamHGPM : string.Empty,
            //    Highest_HighestGoalsPerMatch = contestGroupStatsDBModel.HighestHGPM == null ? (decimal)0 : (decimal)contestGroupStatsDBModel.HighestHGPM,
            //    TeamId_LowestGoalsPerMatch = contestGroupStatsDBModel.TeamIdLGPM == null ? (int)0 : (int)contestGroupStatsDBModel.TeamIdLGPM,
            //    Team_LowestGoalsPerMatch = !string.IsNullOrEmpty(contestGroupStatsDBModel.TeamLGPM) ? contestGroupStatsDBModel.TeamLGPM : string.Empty,
            //    Highest_LowestGoalsPerMatch = contestGroupStatsDBModel.HighestLGPM == null ? (decimal)0 : (decimal)contestGroupStatsDBModel.HighestLGPM,
            //    Over05Value1H = contestGroupStatsDBModel.Over05Value1H == null ? (decimal)0 : (decimal)contestGroupStatsDBModel.Over05Value1H,
            //    Over15Value1H = contestGroupStatsDBModel.Over15Value1H == null ? (decimal)0 : (decimal)contestGroupStatsDBModel.Over15Value1H,
            //    Over25Value1H = contestGroupStatsDBModel.Over25Value1H == null ? (decimal)0 : (decimal)contestGroupStatsDBModel.Over25Value1H,
            //    TeamId_HighestGoalsPerMatchIn1stHalf = contestGroupStatsDBModel.TeamIdHGPM1H == null ? (int)0 : (int)contestGroupStatsDBModel.TeamIdHGPM1H,
            //    Team_HighestGoalsPerMatchIn1stHalf = !string.IsNullOrEmpty(contestGroupStatsDBModel.TeamHGPM1H) ? contestGroupStatsDBModel.TeamHGPM1H : string.Empty,
            //    Highest_HighestGoalsPerMatchIn1stHalf = contestGroupStatsDBModel.HighestHGPM1H == null ? (decimal)0 : (decimal)contestGroupStatsDBModel.HighestHGPM1H,
            //    TeamId_LowestGoalsPerMatchIn1stHalf = contestGroupStatsDBModel.TeamIdLGPM1H == null ? (int)0 : (int)contestGroupStatsDBModel.TeamIdLGPM1H,
            //    Team_LowestGoalsPerMatchIn1stHalf = !string.IsNullOrEmpty(contestGroupStatsDBModel.TeamLGPM1H) ? contestGroupStatsDBModel.TeamLGPM1H : string.Empty,
            //    Highest_LowestGoalsPerMatchIn1stHalf = contestGroupStatsDBModel.HighestLGPM1H == null ? (decimal)0 : (decimal)contestGroupStatsDBModel.HighestLGPM1H,
            //    BTTSPercentage = contestGroupStatsDBModel.BTTSPercentage == null ? (decimal)0 : (decimal)contestGroupStatsDBModel.BTTSPercentage,
            //    TeamId_BothTeamsToScore1 = contestGroupStatsDBModel.TeamIdBTTSM1 == null ? (int)0 : (int)contestGroupStatsDBModel.TeamIdBTTSM1,
            //    Team_BothTeamsToScore1 = !string.IsNullOrEmpty(contestGroupStatsDBModel.TeamBTTSM1) ? contestGroupStatsDBModel.TeamBTTSM1 : string.Empty,
            //    Highest_BothTeamsToScore1 = contestGroupStatsDBModel.HighestBTTSM1 == null ? (decimal)0 : (decimal)contestGroupStatsDBModel.HighestBTTSM1,
            //    MatchesPlayed_BothTeamsToScore1 = contestGroupStatsDBModel.MatchesPlayedBTTSM1 == null ? (int)0 : (int)contestGroupStatsDBModel.MatchesPlayedBTTSM1,
            //    TeamId_BothTeamsToScore2 = contestGroupStatsDBModel.TeamIdBTTSM2 == null ? (int)0 : (int)contestGroupStatsDBModel.TeamIdBTTSM2,
            //    Team_BothTeamsToScore2 = !string.IsNullOrEmpty(contestGroupStatsDBModel.TeamBTTSM2) ? contestGroupStatsDBModel.TeamBTTSM2 : string.Empty,
            //    Highest_BothTeamsToScore2 = contestGroupStatsDBModel.HighestBTTSM2 == null ? (decimal)0 : (decimal)contestGroupStatsDBModel.HighestBTTSM2,
            //    MatchesPlayed_BothTeamsToScore2 = contestGroupStatsDBModel.MatchesPlayedBTTSM2 == null ? (int)0 : (int)contestGroupStatsDBModel.MatchesPlayedBTTSM2,
            //    TeamId_BothTeamsToScore3 = contestGroupStatsDBModel.TeamIdBTTSM3 == null ? (int)0 : (int)contestGroupStatsDBModel.TeamIdBTTSM3,
            //    Team_BothTeamsToScore3 = !string.IsNullOrEmpty(contestGroupStatsDBModel.TeamBTTSM3) ? contestGroupStatsDBModel.TeamBTTSM3 : string.Empty,
            //    Highest_BothTeamsToScore3 = contestGroupStatsDBModel.HighestBTTSM3 == null ? (decimal)0 : (decimal)contestGroupStatsDBModel.HighestBTTSM3,
            //    MatchesPlayed_BothTeamsToScore3 = contestGroupStatsDBModel.MatchesPlayedBTTSM3 == null ? (int)0 : (int)contestGroupStatsDBModel.MatchesPlayedBTTSM3,
            //    TeamId_LowestBothTeamsToScore1 = contestGroupStatsDBModel.TeamIdBTTSF1 == null ? (int)0 : (int)contestGroupStatsDBModel.TeamIdBTTSF1,
            //    Team_LowestBothTeamsToScore1 = !string.IsNullOrEmpty(contestGroupStatsDBModel.TeamBTTSF1) ? contestGroupStatsDBModel.TeamBTTSF1 : string.Empty,
            //    Highest_LowestBothTeamsToScore1 = contestGroupStatsDBModel.HighestBTTSF1 == null ? (decimal)0 : (decimal)contestGroupStatsDBModel.HighestBTTSF1,
            //    MatchesPlayed_LowestBothTeamsToScore1 = contestGroupStatsDBModel.MatchesPlayedBTTSF1 == null ? (int)0 : (int)contestGroupStatsDBModel.MatchesPlayedBTTSF1,
            //    TeamId_HighestCleanSheets1 = contestGroupStatsDBModel.TeamIdCSM1 == null ? (int)0 : (int)contestGroupStatsDBModel.TeamIdCSM1,
            //    Team_HighestCleanSheets1 = !string.IsNullOrEmpty(contestGroupStatsDBModel.TeamCSM1) ? contestGroupStatsDBModel.TeamCSM1 : string.Empty,
            //    Highest_HighestCleanSheets1 = contestGroupStatsDBModel.HighestCSM1 == null ? (decimal)0 : (decimal)contestGroupStatsDBModel.HighestCSM1,
            //    MatchesPlayed_HighestCleanSheets1 = contestGroupStatsDBModel.MatchesPlayedCSM1 == null ? (int)0 : (int)contestGroupStatsDBModel.MatchesPlayedCSM1,
            //    TeamId_HighestCleanSheets2 = contestGroupStatsDBModel.TeamIdCSM2 == null ? (int)0 : (int)contestGroupStatsDBModel.TeamIdCSM2,
            //    Team_HighestCleanSheets2 = !string.IsNullOrEmpty(contestGroupStatsDBModel.TeamCSM2) ? contestGroupStatsDBModel.TeamCSM2 : string.Empty,
            //    Highest_HighestCleanSheets2 = contestGroupStatsDBModel.HighestCSM2 == null ? (decimal)0 : (decimal)contestGroupStatsDBModel.HighestCSM2,
            //    MatchesPlayed_HighestCleanSheets2 = contestGroupStatsDBModel.MatchesPlayedCSM2 == null ? (int)0 : (int)contestGroupStatsDBModel.MatchesPlayedCSM2,
            //    TeamId_LowestCleanSheets1 = contestGroupStatsDBModel.TeamIdCSF1 == null ? (int)0 : (int)contestGroupStatsDBModel.TeamIdCSF1,
            //    Team_LowestCleanSheets1 = !string.IsNullOrEmpty(contestGroupStatsDBModel.TeamCSF1) ? contestGroupStatsDBModel.TeamCSF1 : string.Empty,
            //    Highest_LowestCleanSheets1 = contestGroupStatsDBModel.HighestCSF1 == null ? (decimal)0 : (decimal)contestGroupStatsDBModel.HighestCSF1,
            //    MatchesPlayed_LowestCleanSheets1 = contestGroupStatsDBModel.MatchesPlayedCSF1 == null ? (int)0 : (int)contestGroupStatsDBModel.MatchesPlayedCSF1,
            //    TeamId_HighestFailedToScore1 = contestGroupStatsDBModel.TeamIdFTSM1 == null ? (int)0 : (int)contestGroupStatsDBModel.TeamIdFTSM1,
            //    Team_HighestFailedToScore1 = !string.IsNullOrEmpty(contestGroupStatsDBModel.TeamFTSM1) ? contestGroupStatsDBModel.TeamFTSM1 : string.Empty,
            //    Highest_HighestFailedToScore1 = contestGroupStatsDBModel.HighestFTSM1 == null ? (decimal)0 : (decimal)contestGroupStatsDBModel.HighestFTSM1,
            //    MatchesPlayed_HighestFailedToScore1 = contestGroupStatsDBModel.MatchesPlayedFTSM1 == null ? (int)0 : (int)contestGroupStatsDBModel.MatchesPlayedFTSM1,
            //    TeamId_LowestFailedToScore1 = contestGroupStatsDBModel.TeamIdFTSF1 == null ? (int)0 : (int)contestGroupStatsDBModel.TeamIdFTSF1,
            //    Team_LowestFailedToScore1 = !string.IsNullOrEmpty(contestGroupStatsDBModel.TeamFTSF1) ? contestGroupStatsDBModel.TeamFTSF1 : string.Empty,
            //    Highest_LowestFailedToScore1 = contestGroupStatsDBModel.HighestFTSF1 == null ? (decimal)0 : (decimal)contestGroupStatsDBModel.HighestFTSF1,
            //    MatchesPlayed_LowestFailedToScore1 = contestGroupStatsDBModel.MatchesPlayedFTSF1 == null ? (int)0 : (int)contestGroupStatsDBModel.MatchesPlayedFTSF1,
            //    TeamId_HighestConsecutiveLoses1 = contestGroupStatsDBModel.TeamIdCLM1 == null ? (int)0 : (int)contestGroupStatsDBModel.TeamIdCLM1,
            //    Team_HighestConsecutiveLoses1 = !string.IsNullOrEmpty(contestGroupStatsDBModel.TeamCLM1) ? contestGroupStatsDBModel.TeamCLM1 : string.Empty,
            //    Highest_HighestConsecutiveLoses1 = contestGroupStatsDBModel.HighestCLM1 == null ? (decimal)0 : (decimal)contestGroupStatsDBModel.HighestCLM1,
            //    MatchesPlayed_HighestConsecutiveLoses1 = contestGroupStatsDBModel.MatchesPlayedCLM1 == null ? (int)0 : (int)contestGroupStatsDBModel.MatchesPlayedCLM1,
            //    TeamId_HighestConsecutiveWins1 = contestGroupStatsDBModel.TeamIdCWM1 == null ? (int)0 : (int)contestGroupStatsDBModel.TeamIdCWM1,
            //    Team_HighestConsecutiveWins1 = !string.IsNullOrEmpty(contestGroupStatsDBModel.TeamCWM1) ? contestGroupStatsDBModel.TeamCWM1 : string.Empty,
            //    Highest_HighestConsecutiveWins1 = contestGroupStatsDBModel.HighestCWM1 == null ? (decimal)0 : (decimal)contestGroupStatsDBModel.HighestCWM1,
            //    MatchesPlayed_HighestConsecutiveWins1 = contestGroupStatsDBModel.MatchesPlayedCWM1 == null ? (int)0 : (int)contestGroupStatsDBModel.MatchesPlayedCWM1,
            //    TeamId_HighestConsecutiveWinsAsHome1 = contestGroupStatsDBModel.TeamIdCWAHM1 == null ? (int)0 : (int)contestGroupStatsDBModel.TeamIdCWAHM1,
            //    Team_HighestConsecutiveWinsAsHome1 = !string.IsNullOrEmpty(contestGroupStatsDBModel.TeamCWAHM1) ? contestGroupStatsDBModel.TeamCWAHM1 : string.Empty,
            //    Highest_HighestConsecutiveWinsAsHome1 = contestGroupStatsDBModel.HighestCWAHM1 == null ? (decimal)0 : (decimal)contestGroupStatsDBModel.HighestCWAHM1,
            //    MatchesPlayed_HighestConsecutiveWinsAsHome1 = contestGroupStatsDBModel.MatchesPlayedCWAHM1 == null ? (int)0 : (int)contestGroupStatsDBModel.MatchesPlayedCWAHM1,
            //    TeamId_HighestConsecutiveWinsAsAway1 = contestGroupStatsDBModel.TeamIdCWAAM1 == null ? (int)0 : (int)contestGroupStatsDBModel.TeamIdCWAAM1,
            //    Team_HighestConsecutiveWinsAsAway1 = !string.IsNullOrEmpty(contestGroupStatsDBModel.TeamCWAAM1) ? contestGroupStatsDBModel.TeamCWAAM1 : string.Empty,
            //    Highest_HighestConsecutiveWinsAsAway1 = contestGroupStatsDBModel.HighestCWAAM1 == null ? (decimal)0 : (decimal)contestGroupStatsDBModel.HighestCWAAM1,
            //    MatchesPlayed_HighestConsecutiveWinsAsAway1 = contestGroupStatsDBModel.MatchesPlayedCWAAM1 == null ? (int)0 : (int)contestGroupStatsDBModel.MatchesPlayedCWAAM1,
            //    TeamId_HighestScoreTheFirstGoal1 = contestGroupStatsDBModel.TeamIdSTFGM1 == null ? (int)0 : (int)contestGroupStatsDBModel.TeamIdSTFGM1,
            //    Team_HighestScoreTheFirstGoal1 = !string.IsNullOrEmpty(contestGroupStatsDBModel.TeamSTFGM1) ? contestGroupStatsDBModel.TeamSTFGM1 : string.Empty,
            //    Highest_HighestScoreTheFirstGoal1 = contestGroupStatsDBModel.HighestSTFGM1 == null ? (decimal)0 : (decimal)contestGroupStatsDBModel.HighestSTFGM1,
            //    MatchesPlayed_HighestScoreTheFirstGoal1 = contestGroupStatsDBModel.MatchesPlayedSTFGM1 == null ? (int)0 : (int)contestGroupStatsDBModel.MatchesPlayedSTFGM1,
            //    TeamId_FewestScoreTheFirstGoal1 = contestGroupStatsDBModel.TeamIdSTFGF1 == null ? (int)0 : (int)contestGroupStatsDBModel.TeamIdSTFGF1,
            //    Team_FewestScoreTheFirstGoal1 = !string.IsNullOrEmpty(contestGroupStatsDBModel.TeamSTFGF1) ? contestGroupStatsDBModel.TeamSTFGF1 : string.Empty,
            //    Highest_FewestScoreTheFirstGoal1 = contestGroupStatsDBModel.HighestSTFGF1 == null ? (decimal)0 : (decimal)contestGroupStatsDBModel.HighestSTFGF1,
            //    MatchesPlayed_FewestScoreTheFirstGoal1 = contestGroupStatsDBModel.MatchesPlayedSTFGF1 == null ? (int)0 : (int)contestGroupStatsDBModel.MatchesPlayedSTFGF1,
            //    TeamId_MatchesSinceLastWin1 = contestGroupStatsDBModel.TeamIdMSLWM1 == null ? (int)0 : (int)contestGroupStatsDBModel.TeamIdMSLWM1,
            //    Team_MatchesSinceLastWin1 = !string.IsNullOrEmpty(contestGroupStatsDBModel.TeamMSLWM1) ? contestGroupStatsDBModel.TeamMSLWM1 : string.Empty,
            //    Highest_MatchesSinceLastWin1 = contestGroupStatsDBModel.HighestMSLWM1 == null ? (decimal)0 : (decimal)contestGroupStatsDBModel.HighestMSLWM1,
            //    MatchesPlayed_MatchesSinceLastWin1 = contestGroupStatsDBModel.MatchesPlayedMSLWM1 == null ? (int)0 : (int)contestGroupStatsDBModel.MatchesPlayedMSLWM1,
            //    TeamId_Unbeaten1 = contestGroupStatsDBModel.TeamIdUBM1 == null ? (int)0 : (int)contestGroupStatsDBModel.TeamIdUBM1,
            //    Team_Unbeaten1 = !string.IsNullOrEmpty(contestGroupStatsDBModel.TeamUBM1) ? contestGroupStatsDBModel.TeamUBM1 : string.Empty,
            //    Highest_Unbeaten1 = contestGroupStatsDBModel.HighestUBM1 == null ? (decimal)0 : (decimal)contestGroupStatsDBModel.HighestUBM1,
            //    MatchesPlayed_Unbeaten1 = contestGroupStatsDBModel.MatchesPlayedUBM1 == null ? (int)0 : (int)contestGroupStatsDBModel.MatchesPlayedUBM1
            //};
            #endregion

            List<LongestActiveStreaks> markets = new List<LongestActiveStreaks>();

            #region Both Teams Scored
            LongestActiveStreaks bothTeamScored = new LongestActiveStreaks();
            bothTeamScored.TeamId = (int)contestGroupStatsModel.TeamId_BothTeamsToScore1;
            bothTeamScored.Team = contestGroupStatsModel.Team_BothTeamsToScore1;
            bothTeamScored.MarketName = "Both Teams Scored";
            bothTeamScored.Highest = (decimal)contestGroupStatsModel.Highest_BothTeamsToScore1;
            bothTeamScored.MatchesPlayed = (int)contestGroupStatsModel.MatchesPlayed_BothTeamsToScore1;
            markets.Add(bothTeamScored);
            #endregion

            #region Consecutive Losses 
            LongestActiveStreaks consecutiveLosses = new LongestActiveStreaks();
            consecutiveLosses.TeamId = (int)contestGroupStatsModel.TeamId_HighestConsecutiveLoses1;
            consecutiveLosses.Team = contestGroupStatsModel.Team_HighestConsecutiveLoses1;
            consecutiveLosses.MarketName = "Consecutive Losses";
            consecutiveLosses.Highest = (decimal)contestGroupStatsModel.Highest_HighestConsecutiveLoses1;
            consecutiveLosses.MatchesPlayed = (int)contestGroupStatsModel.MatchesPlayed_HighestConsecutiveLoses1;
            markets.Add(consecutiveLosses);
            #endregion

            #region  Consecutive Wins 
            LongestActiveStreaks consecutiveWins = new LongestActiveStreaks();
            consecutiveWins.TeamId = (int)contestGroupStatsModel.TeamId_HighestConsecutiveWins1;
            consecutiveWins.Team = contestGroupStatsModel.Team_HighestConsecutiveWins1;
            consecutiveWins.MarketName = "Consecutive Wins";
            consecutiveWins.Highest = (decimal)contestGroupStatsModel.Highest_HighestConsecutiveWins1;
            consecutiveWins.MatchesPlayed = (int)contestGroupStatsModel.MatchesPlayed_HighestConsecutiveWins1;
            markets.Add(consecutiveWins);
            #endregion

            #region Consecutive Wins At Home
            LongestActiveStreaks consectiveWinsAtHome = new LongestActiveStreaks();
            consectiveWinsAtHome.TeamId = (int)contestGroupStatsModel.TeamId_HighestConsecutiveWinsAsHome1;
            consectiveWinsAtHome.Team = contestGroupStatsModel.Team_HighestConsecutiveWinsAsHome1;
            consectiveWinsAtHome.MarketName = "Consecutive Wins At Home";
            consectiveWinsAtHome.Highest = (decimal)contestGroupStatsModel.Highest_HighestConsecutiveWinsAsHome1;
            consectiveWinsAtHome.MatchesPlayed = (int)contestGroupStatsModel.MatchesPlayed_HighestConsecutiveWinsAsHome1;
            markets.Add(consectiveWinsAtHome);
            #endregion

            #region Consecutive Wins At Away                        
            LongestActiveStreaks consectiveWinsAtAway = new LongestActiveStreaks();
            consectiveWinsAtAway.TeamId = (int)contestGroupStatsModel.TeamId_HighestConsecutiveWinsAsAway1;
            consectiveWinsAtAway.Team = contestGroupStatsModel.Team_HighestConsecutiveWinsAsAway1;
            consectiveWinsAtAway.MarketName = "Consecutive Wins At Away";
            consectiveWinsAtAway.Highest = (decimal)contestGroupStatsModel.Highest_HighestConsecutiveWinsAsAway1;
            consectiveWinsAtAway.MatchesPlayed = (int)contestGroupStatsModel.MatchesPlayed_HighestConsecutiveWinsAsAway1;
            markets.Add(consectiveWinsAtAway);
            #endregion

            #region Failed to Score
            LongestActiveStreaks failedToScore = new LongestActiveStreaks();
            failedToScore.TeamId = (int)contestGroupStatsModel.TeamId_HighestFailedToScore1;
            failedToScore.Team = contestGroupStatsModel.Team_HighestFailedToScore1;
            failedToScore.MarketName = "Failed to Score";
            failedToScore.Highest = (decimal)contestGroupStatsModel.Highest_HighestFailedToScore1;
            failedToScore.MatchesPlayed = (int)contestGroupStatsModel.MatchesPlayed_HighestFailedToScore1;
            markets.Add(failedToScore);
            #endregion

            #region Matches Since Last Win
            LongestActiveStreaks matchesSinceLastWin = new LongestActiveStreaks();
            matchesSinceLastWin.TeamId = (int)contestGroupStatsModel.TeamId_MatchesSinceLastWin1;
            matchesSinceLastWin.Team = contestGroupStatsModel.Team_MatchesSinceLastWin1;
            matchesSinceLastWin.MarketName = "Matches Since Last Win";
            matchesSinceLastWin.Highest = (decimal)contestGroupStatsModel.Highest_MatchesSinceLastWin1;
            matchesSinceLastWin.MatchesPlayed = (int)contestGroupStatsModel.MatchesPlayed_MatchesSinceLastWin1;
            markets.Add(matchesSinceLastWin);
            #endregion

            #region Unbeaten
            LongestActiveStreaks unbeaten = new LongestActiveStreaks();
            unbeaten.TeamId = (int)contestGroupStatsModel.TeamId_Unbeaten1;
            unbeaten.Team = contestGroupStatsModel.Team_Unbeaten1;
            unbeaten.MarketName = "Unbeaten";
            unbeaten.Highest = (decimal)contestGroupStatsModel.Highest_Unbeaten1;
            unbeaten.MatchesPlayed = (int)contestGroupStatsModel.MatchesPlayed_Unbeaten1;
            markets.Add(unbeaten);
            #endregion

            contestGroupStatsModel.LongestActiveStreaks = markets.OrderByDescending(m => m.Highest).ToList();
            return contestGroupStatsModel;
        }
        #endregion

        #region Private Methods
        private static string _ReturnMatchStatuses(short sportId, string filterType)
        {
            string retStatuses = string.Empty;
            if (sportId == 2)//Tennis
            {

            }
            else //Soccer
            {
                if (filterType == "Upcoming")
                    retStatuses = "6, 8";
                else if (filterType == "Live")
                    retStatuses = "13, 14, 15, 16, 17, 18";
                else
                    retStatuses = "5, 6, 7, 8, 9, 11, 12, 13, 14, 15, 16, 17, 18, 20, 21, 22, 23, 55, 123";
            }

            return retStatuses;
        }

        private static MatchDetailBetwayDBModel _GetMatchDetailForBetWayFromDBByMatchId(GlobalParametersModel globalParametersModel)
        {
            MatchDetailBetwayDBModel matchDetailBetwayDBModel = new MatchDetailBetwayDBModel();
            matchDetailBetwayDBModel.MatchDetailEventsDB = new List<MatchDetailEventsDBModel>();
            matchDetailBetwayDBModel.HomeTeamLinupPlayersDB = new List<TeamLinupPlayersDBModel>();
            matchDetailBetwayDBModel.AwayTeamLinupPlayersDB = new List<TeamLinupPlayersDBModel>();
            matchDetailBetwayDBModel.LastestFifteenMatchesHomeTeamDB = new List<LatestFifteenMatchesDBModel>();
            matchDetailBetwayDBModel.LastestFifteenMatchesAwayTeamDB = new List<LatestFifteenMatchesDBModel>();
            matchDetailBetwayDBModel.MatchSubstitutionsDB = new List<MatchSubstitutionsDBModel>();
            matchDetailBetwayDBModel.MatchStatsDB = new List<StatsMarketsDetailDBModel>();
            matchDetailBetwayDBModel.MatchVenueDetailDB = new MatchVenueDetailDBModel();
            matchDetailBetwayDBModel.HomeTeamStatsMarkets = new List<TeamsStatsDBModel>();
            matchDetailBetwayDBModel.AwayTeamStatsMarkets = new List<TeamsStatsDBModel>();
            matchDetailBetwayDBModel.HomeTeamSubsPlayersDB = new List<TeamLinupPlayersDBModel>();
            matchDetailBetwayDBModel.AwayTeamSubsPlayersDB = new List<TeamLinupPlayersDBModel>();
            matchDetailBetwayDBModel.MatchCompetitorsStatsComDetailDB = new MatchCompetitorsStatsComDetailDBModel();
            matchDetailBetwayDBModel.MatchOfficialsDB = new List<MatchOfficialsDBModel>();

            #region  ADO.Net Code
            using (DataSet _dataSet = new DataSet())
            {
                using (SqlConnection _connection = new SqlConnection(_connectionString))
                {
                    _connection.Open();
                    using (SqlCommand _command = new SqlCommand("GoalCCAPI_GetMatchDetailByMatchId", _connection))
                    {
                        _command.CommandType = CommandType.StoredProcedure;
                        _command.Parameters.Add(new SqlParameter("@MatchId", globalParametersModel.MatchId));
                        _command.Parameters.Add(new SqlParameter("@LanguageCode", globalParametersModel.LanguageCode));
                        using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(_command))
                        {
                            sqlDataAdapter.Fill(_dataSet);
                        }
                    }
                    if (_dataSet.Tables.Count > 0)
                    {
                        if (_dataSet.Tables[0].Rows.Count > 0)
                        {
                            DataRow _dataRow = _dataSet.Tables[0].Rows[0];
                            MatchScoreInfoDBModel matchScoreInfoDBModel = new MatchScoreInfoDBModel();
                            matchScoreInfoDBModel.CountryName = Convert.ToString(_dataRow["CountryName"]);
                            matchScoreInfoDBModel.AwayTeam = Convert.ToString(_dataRow["AwayTeam"]);
                            matchScoreInfoDBModel.AwayTeamId = Convert.ToInt32(_dataRow["AwayTeamId"]);
                            matchScoreInfoDBModel.AwayTeamShortName = Convert.ToString(_dataRow["AwayTeamShortName"]);
                            matchScoreInfoDBModel.ContestGroupId = Convert.ToInt32(_dataRow["ContestGroupId"]);
                            matchScoreInfoDBModel.ContestGroupName = Convert.ToString(_dataRow["ContestGroupName"]);
                            matchScoreInfoDBModel.ContestGroupShortName = Convert.ToString(_dataRow["ContestGroupShortName"]);
                            matchScoreInfoDBModel.CountryId = Convert.ToInt16(_dataRow["CountryId"]);
                            matchScoreInfoDBModel.CurrentMinutes = _dataRow["CurrentMinutes"] != DBNull.Value ? Convert.ToInt16(_dataRow["CurrentMinutes"]) : (Int16?)null;
                            matchScoreInfoDBModel.FullTimeScore = Convert.ToString(_dataRow["FullTimeScore"]);
                            matchScoreInfoDBModel.HalfTimeScore = Convert.ToString(_dataRow["HalfTimeScore"]);
                            matchScoreInfoDBModel.HomeTeam = Convert.ToString(_dataRow["HomeTeam"]);
                            matchScoreInfoDBModel.HomeTeamId = Convert.ToInt32(_dataRow["HomeTeamId"]);
                            matchScoreInfoDBModel.HomeTeamShortName = Convert.ToString(_dataRow["HomeTeamShortName"]);
                            matchScoreInfoDBModel.LiveScore = Convert.ToString(_dataRow["LiveScore"]);
                            matchScoreInfoDBModel.MatchDate = Convert.ToDateTime(_dataRow["MatchDate"]);
                            matchScoreInfoDBModel.MatchId = Convert.ToInt32(_dataRow["MatchId"]);
                            matchScoreInfoDBModel.MatchStatus = Convert.ToString(_dataRow["MatchStatus"]);
                            matchScoreInfoDBModel.MatchStatusId = Convert.ToInt16(_dataRow["MatchStatusId"]);
                            matchScoreInfoDBModel.MinutePlusBit = _dataRow["MinutePlusBit"] != DBNull.Value ? Convert.ToBoolean(_dataRow["MinutePlusBit"]) : (bool?)null;
                            matchScoreInfoDBModel.OddsMatchId = Convert.ToInt64(_dataRow["OddsMatchId"]);
                            matchScoreInfoDBModel.PlusMinutes = Convert.ToString(_dataRow["PlusMinutes"]);
                            matchDetailBetwayDBModel.MatchScoreInfoDB = matchScoreInfoDBModel;
                        }
                        foreach (DataRow _dataRow in _dataSet.Tables[1].Rows)
                        {
                            MatchDetailEventsDBModel matchDetailEventsDBModel = new MatchDetailEventsDBModel();
                            matchDetailEventsDBModel.EventId = Convert.ToDecimal(_dataRow["EventId"]);
                            matchDetailEventsDBModel.EventMinute = Convert.ToString(_dataRow["EventMinute"]);
                            matchDetailEventsDBModel.EventType = Convert.ToString(_dataRow["EventType"]);
                            matchDetailEventsDBModel.EventTypeId = Convert.ToInt16(_dataRow["EventTypeId"]);
                            matchDetailEventsDBModel.HomeAway = Convert.ToBoolean(_dataRow["HomeAway"]);
                            matchDetailEventsDBModel.MatchId = Convert.ToDecimal(_dataRow["MatchId"]);
                            matchDetailEventsDBModel.OtherName = Convert.ToString(_dataRow["OtherName"]);
                            matchDetailEventsDBModel.Player = Convert.ToString(_dataRow["Player"]);
                            matchDetailEventsDBModel.PlayerId = Convert.ToInt32(_dataRow["PlayerId"]);
                            matchDetailEventsDBModel.PlayerName = Convert.ToString(_dataRow["PlayerName"]);
                            matchDetailEventsDBModel.Score = Convert.ToString(_dataRow["Score"]);
                            matchDetailBetwayDBModel.MatchDetailEventsDB.Add(matchDetailEventsDBModel);
                        }
                        foreach (DataRow _dataRow in _dataSet.Tables[2].Rows)
                        {
                            TeamLinupPlayersDBModel teamLineUpPlayersDBModel = new TeamLinupPlayersDBModel();
                            teamLineUpPlayersDBModel.CoachId = _dataRow["CoachId"] != DBNull.Value ? Convert.ToInt16(_dataRow["CoachId"]) : (Int16?)null;
                            teamLineUpPlayersDBModel.CoachName = Convert.ToString(_dataRow["CoachName"]);
                            teamLineUpPlayersDBModel.CountryName = Convert.ToString(_dataRow["CountryName"]);
                            teamLineUpPlayersDBModel.CountryId = _dataRow["CountryId"] != DBNull.Value ? Convert.ToInt16(_dataRow["CountryId"]) : (Int16?)null;
                            teamLineUpPlayersDBModel.DateOfBirth = _dataRow["DateOfBirth"] != DBNull.Value ? Convert.ToDateTime(_dataRow["DateOfBirth"]) : (DateTime?)null;
                            teamLineUpPlayersDBModel.Formation = Convert.ToString(_dataRow["Formation"]);
                            teamLineUpPlayersDBModel.FormationId = Convert.ToInt16(_dataRow["FormationId"]);
                            teamLineUpPlayersDBModel.IsSubstitute = Convert.ToBoolean(_dataRow["IsSubstitute"]);
                            teamLineUpPlayersDBModel.MatchCompetitorId = Convert.ToDecimal(_dataRow["MatchCompetitorId"]);
                            teamLineUpPlayersDBModel.MatchLineupId = Convert.ToDecimal(_dataRow["MatchLineupId"]);
                            teamLineUpPlayersDBModel.MatchLineupPlayerId = Convert.ToDecimal(_dataRow["MatchLineupPlayerId"]);
                            teamLineUpPlayersDBModel.PlayerId = Convert.ToInt32(_dataRow["PlayerId"]);
                            teamLineUpPlayersDBModel.PlayerName = Convert.ToString(_dataRow["PlayerName"]);
                            teamLineUpPlayersDBModel.PlayerPosition = Convert.ToString(_dataRow["PlayerPosition"]);
                            teamLineUpPlayersDBModel.ShirtNo = Convert.ToString(_dataRow["ShirtNo"]);
                            teamLineUpPlayersDBModel.TeamId = Convert.ToInt32(_dataRow["TeamId"]);
                            matchDetailBetwayDBModel.HomeTeamLinupPlayersDB.Add(teamLineUpPlayersDBModel);
                        }
                        foreach (DataRow _dataRow in _dataSet.Tables[3].Rows)
                        {
                            TeamLinupPlayersDBModel teamLineUpPlayersDBModel = new TeamLinupPlayersDBModel();
                            teamLineUpPlayersDBModel.CoachId = _dataRow["CoachId"] != DBNull.Value ? Convert.ToInt16(_dataRow["CoachId"]) : (Int16?)null;
                            teamLineUpPlayersDBModel.CoachName = Convert.ToString(_dataRow["CoachName"]);
                            teamLineUpPlayersDBModel.CountryName = Convert.ToString(_dataRow["CountryName"]);
                            teamLineUpPlayersDBModel.CountryId = _dataRow["CountryId"] != DBNull.Value ? Convert.ToInt16(_dataRow["CountryId"]) : (Int16?)null;
                            teamLineUpPlayersDBModel.DateOfBirth = _dataRow["DateOfBirth"] != DBNull.Value ? Convert.ToDateTime(_dataRow["DateOfBirth"]) : (DateTime?)null;
                            teamLineUpPlayersDBModel.Formation = Convert.ToString(_dataRow["Formation"]);
                            teamLineUpPlayersDBModel.FormationId = Convert.ToInt16(_dataRow["FormationId"]);
                            teamLineUpPlayersDBModel.IsSubstitute = Convert.ToBoolean(_dataRow["IsSubstitute"]);
                            teamLineUpPlayersDBModel.MatchCompetitorId = Convert.ToDecimal(_dataRow["MatchCompetitorId"]);
                            teamLineUpPlayersDBModel.MatchLineupId = Convert.ToDecimal(_dataRow["MatchLineupId"]);
                            teamLineUpPlayersDBModel.MatchLineupPlayerId = Convert.ToDecimal(_dataRow["MatchLineupPlayerId"]);
                            teamLineUpPlayersDBModel.PlayerId = Convert.ToInt32(_dataRow["PlayerId"]);
                            teamLineUpPlayersDBModel.PlayerName = Convert.ToString(_dataRow["PlayerName"]);
                            teamLineUpPlayersDBModel.PlayerPosition = Convert.ToString(_dataRow["PlayerPosition"]);
                            teamLineUpPlayersDBModel.ShirtNo = Convert.ToString(_dataRow["ShirtNo"]);
                            teamLineUpPlayersDBModel.TeamId = Convert.ToInt32(_dataRow["TeamId"]);
                            matchDetailBetwayDBModel.AwayTeamLinupPlayersDB.Add(teamLineUpPlayersDBModel);
                        }
                        foreach (DataRow _dataRow in _dataSet.Tables[4].Rows)
                        {
                            LatestFifteenMatchesDBModel lastFifteenMatchesDBModel = new LatestFifteenMatchesDBModel();
                            lastFifteenMatchesDBModel.AwayScore = Convert.ToString(_dataRow["AwayScore"]);
                            lastFifteenMatchesDBModel.AwayTeam = Convert.ToString(_dataRow["AwayTeam"]);
                            lastFifteenMatchesDBModel.AwayTeamId = Convert.ToInt32(_dataRow["AwayTeamId"]);
                            lastFifteenMatchesDBModel.AwayTeamShortName = Convert.ToString(_dataRow["AwayTeamShortName"]);
                            lastFifteenMatchesDBModel.ContestGroupId = Convert.ToInt32(_dataRow["ContestGroupId"]);
                            lastFifteenMatchesDBModel.ContestGroupName = Convert.ToString(_dataRow["ContestGroupName"]);
                            lastFifteenMatchesDBModel.CountryId = _dataRow["CountryId"] != DBNull.Value ? Convert.ToInt16(_dataRow["CountryId"]) : (Int16?)null;
                            lastFifteenMatchesDBModel.CountryName = Convert.ToString(_dataRow["CountryName"]);
                            lastFifteenMatchesDBModel.HomeScore = Convert.ToString(_dataRow["HomeScore"]);
                            lastFifteenMatchesDBModel.HomeTeam = Convert.ToString(_dataRow["HomeTeam"]);
                            lastFifteenMatchesDBModel.HomeTeamId = Convert.ToInt16(_dataRow["HomeTeamId"]);
                            lastFifteenMatchesDBModel.HomeTeamShortName = Convert.ToString(_dataRow["HomeTeamShortName"]);
                            lastFifteenMatchesDBModel.MatchDate = Convert.ToDateTime(_dataRow["MatchDate"]);
                            lastFifteenMatchesDBModel.MatchId = Convert.ToInt32(_dataRow["MatchId"]);
                            matchDetailBetwayDBModel.LastestFifteenMatchesHomeTeamDB.Add(lastFifteenMatchesDBModel);
                        }
                        foreach (DataRow _dataRow in _dataSet.Tables[5].Rows)
                        {
                            LatestFifteenMatchesDBModel lastFifteenMatchesDBModel = new LatestFifteenMatchesDBModel();
                            lastFifteenMatchesDBModel.AwayScore = Convert.ToString(_dataRow["AwayScore"]);
                            lastFifteenMatchesDBModel.AwayTeam = Convert.ToString(_dataRow["AwayTeam"]);
                            lastFifteenMatchesDBModel.AwayTeamId = Convert.ToInt32(_dataRow["AwayTeamId"]);
                            lastFifteenMatchesDBModel.AwayTeamShortName = Convert.ToString(_dataRow["AwayTeamShortName"]);
                            lastFifteenMatchesDBModel.ContestGroupId = Convert.ToInt32(_dataRow["ContestGroupId"]);
                            lastFifteenMatchesDBModel.ContestGroupName = Convert.ToString(_dataRow["ContestGroupName"]);
                            lastFifteenMatchesDBModel.CountryId = _dataRow["CountryId"] != DBNull.Value ? Convert.ToInt16(_dataRow["CountryId"]) : (Int16?)null;
                            lastFifteenMatchesDBModel.CountryName = Convert.ToString(_dataRow["CountryName"]);
                            lastFifteenMatchesDBModel.HomeScore = Convert.ToString(_dataRow["HomeScore"]);
                            lastFifteenMatchesDBModel.HomeTeam = Convert.ToString(_dataRow["HomeTeam"]);
                            lastFifteenMatchesDBModel.HomeTeamId = Convert.ToInt16(_dataRow["HomeTeamId"]);
                            lastFifteenMatchesDBModel.HomeTeamShortName = Convert.ToString(_dataRow["HomeTeamShortName"]);
                            lastFifteenMatchesDBModel.MatchDate = Convert.ToDateTime(_dataRow["MatchDate"]);
                            lastFifteenMatchesDBModel.MatchId = Convert.ToInt32(_dataRow["MatchId"]);
                            matchDetailBetwayDBModel.LastestFifteenMatchesAwayTeamDB.Add(lastFifteenMatchesDBModel);
                        }
                        foreach (DataRow _dataRow in _dataSet.Tables[6].Rows)
                        {
                            MatchSubstitutionsDBModel matchSubstitutionsDBModel = new MatchSubstitutionsDBModel();
                            matchSubstitutionsDBModel.MatchId = Convert.ToDecimal(_dataRow["MatchId"]);
                            matchSubstitutionsDBModel.MatchSubstituteId = Convert.ToDecimal(_dataRow["MatchSubstituteId"]);
                            matchSubstitutionsDBModel.PlayerInCountryId = _dataRow["PlayerInCountryId"] != DBNull.Value ? Convert.ToInt16(_dataRow["PlayerInCountryId"]) : (Int16?)null;
                            matchSubstitutionsDBModel.PlayerInCountryName = Convert.ToString(_dataRow["PlayerInCountryName"]);
                            matchSubstitutionsDBModel.PlayerInId = Convert.ToInt32(_dataRow["PlayerInId"]);
                            matchSubstitutionsDBModel.PlayerInName = Convert.ToString(_dataRow["PlayerInName"]);
                            matchSubstitutionsDBModel.PlayerOutCountryId = _dataRow["PlayerOutCountryId"] != DBNull.Value ? Convert.ToInt16(_dataRow["PlayerOutCountryId"]) : (Int16?)null;
                            matchSubstitutionsDBModel.PlayerOutCountryName = Convert.ToString(_dataRow["PlayerOutCountryName"]);
                            matchSubstitutionsDBModel.PlayerOutId = Convert.ToInt32(_dataRow["PlayerOutId"]);
                            matchSubstitutionsDBModel.PlayerOutName = Convert.ToString(_dataRow["PlayerOutName"]);
                            matchSubstitutionsDBModel.SubHomeAway = Convert.ToBoolean(_dataRow["SubHomeAway"]);
                            matchSubstitutionsDBModel.SubstitutionMinute = Convert.ToString(_dataRow["SubstitutionMinute"]);
                            matchDetailBetwayDBModel.MatchSubstitutionsDB.Add(matchSubstitutionsDBModel);
                        }
                        foreach (DataRow _dataRow in _dataSet.Tables[7].Rows)
                        {
                            StatsMarketsDetailDBModel statsMaeketsDetailDBModel = new StatsMarketsDetailDBModel();
                            statsMaeketsDetailDBModel.AwayVal = Convert.ToDecimal(_dataRow["AwayVal"]);
                            statsMaeketsDetailDBModel.BookmakerId = Convert.ToString(_dataRow["BookmakerId"]);
                            statsMaeketsDetailDBModel.HomeVal = Convert.ToDecimal(_dataRow["HomeVal"]);
                            statsMaeketsDetailDBModel.Market = Convert.ToString(_dataRow["Market"]);
                            statsMaeketsDetailDBModel.Odds = Convert.ToString(_dataRow["Odds"]);
                            statsMaeketsDetailDBModel.TotalVal = Convert.ToDecimal(_dataRow["TotalVal"]);
                            matchDetailBetwayDBModel.MatchStatsDB.Add(statsMaeketsDetailDBModel);
                        }
                        if (_dataSet.Tables[16].Rows.Count > 0)
                        {
                            DataRow _dataRow = _dataSet.Tables[16].Rows[0];
                            MatchCompetitorsStatsComDetailDBModel matchcompititotorsDBModel = new MatchCompetitorsStatsComDetailDBModel();
                            matchcompititotorsDBModel.AtAttacks = _dataRow["AtAttacks"] != DBNull.Value ? Convert.ToInt32(_dataRow["AtAttacks"]) : (Int32?)null;
                            matchcompititotorsDBModel.AtCornerKicks = _dataRow["AtCornerKicks"] != DBNull.Value ? Convert.ToInt32(_dataRow["AtCornerKicks"]) : (Int32?)null;
                            matchcompititotorsDBModel.AtDangerousAttacks = _dataRow["AtDangerousAttacks"] != DBNull.Value ? Convert.ToInt32(_dataRow["AtDangerousAttacks"]) : (Int32?)null;
                            matchcompititotorsDBModel.AtFoulsCommitted = _dataRow["AtFoulsCommitted"] != DBNull.Value ? Convert.ToInt32(_dataRow["AtFoulsCommitted"]) : (Int32?)null;
                            matchcompititotorsDBModel.AtFoulsSuffered = _dataRow["AtFoulsSuffered"] != DBNull.Value ? Convert.ToInt32(_dataRow["AtFoulsSuffered"]) : (Int32?)null;
                            matchcompititotorsDBModel.AtOffsides = _dataRow["AtOffsides"] != DBNull.Value ? Convert.ToInt32(_dataRow["AtOffsides"]) : (Int32?)null;
                            matchcompititotorsDBModel.AtPenShots = _dataRow["AtPenShots"] != DBNull.Value ? Convert.ToInt32(_dataRow["AtPenShots"]) : (Int32?)null;
                            matchcompititotorsDBModel.AtPossessionPercentage = _dataRow["AtPossessionPercentage"] != DBNull.Value ? Convert.ToInt32(_dataRow["AtPossessionPercentage"]) : (Int32?)null;
                            matchcompititotorsDBModel.AtRedCards = _dataRow["AtRedCards"] != DBNull.Value ? Convert.ToInt32(_dataRow["AtRedCards"]) : (Int32?)null;
                            matchcompititotorsDBModel.AtShots = _dataRow["AtShots"] != DBNull.Value ? Convert.ToInt32(_dataRow["AtShots"]) : (Int32?)null;
                            matchcompititotorsDBModel.AtShotsOnGoal = _dataRow["AtShotsOnGoal"] != DBNull.Value ? Convert.ToInt32(_dataRow["AtShotsOnGoal"]) : (Int32?)null;
                            matchcompititotorsDBModel.AtTotalAssists = _dataRow["AtTotalAssists"] != DBNull.Value ? Convert.ToInt32(_dataRow["AtTotalAssists"]) : (Int32?)null;
                            matchcompititotorsDBModel.AtTotalTouches = _dataRow["AtTotalTouches"] != DBNull.Value ? Convert.ToInt32(_dataRow["AtTotalTouches"]) : (Int32?)null;
                            matchcompititotorsDBModel.AtTouchesPasses = _dataRow["AtTouchesPasses"] != DBNull.Value ? Convert.ToInt32(_dataRow["AtTouchesPasses"]) : (Int32?)null;
                            matchcompititotorsDBModel.AtYellowCards = _dataRow["AtYellowCards"] != DBNull.Value ? Convert.ToInt32(_dataRow["AtYellowCards"]) : (Int32?)null;
                            matchcompititotorsDBModel.HtAttacks = _dataRow["HtAttacks"] != DBNull.Value ? Convert.ToInt32(_dataRow["HtAttacks"]) : (Int32?)null;
                            matchcompititotorsDBModel.HtCornerKicks = _dataRow["HtCornerKicks"] != DBNull.Value ? Convert.ToInt32(_dataRow["HtCornerKicks"]) : (Int32?)null;
                            matchcompititotorsDBModel.HtDangerousAttacks = _dataRow["HtDangerousAttacks"] != DBNull.Value ? Convert.ToInt32(_dataRow["HtDangerousAttacks"]) : (Int32?)null;
                            matchcompititotorsDBModel.HtFoulsCommitted = _dataRow["HtFoulsCommitted"] != DBNull.Value ? Convert.ToInt32(_dataRow["HtFoulsCommitted"]) : (Int32?)null;
                            matchcompititotorsDBModel.HtFoulsSuffered = _dataRow["HtFoulsSuffered"] != DBNull.Value ? Convert.ToInt32(_dataRow["HtFoulsSuffered"]) : (Int32?)null;
                            matchcompititotorsDBModel.HtOffsides = _dataRow["HtOffsides"] != DBNull.Value ? Convert.ToInt32(_dataRow["HtOffsides"]) : (Int32?)null;
                            matchcompititotorsDBModel.HtPenShots = _dataRow["HtPenShots"] != DBNull.Value ? Convert.ToInt32(_dataRow["HtPenShots"]) : (Int32?)null;
                            matchcompititotorsDBModel.HtPossessionPercentage = _dataRow["HtPossessionPercentage"] != DBNull.Value ? Convert.ToInt32(_dataRow["HtPossessionPercentage"]) : (Int32?)null;
                            matchcompititotorsDBModel.HtRedCards = _dataRow["HtRedCards"] != DBNull.Value ? Convert.ToInt32(_dataRow["HtRedCards"]) : (Int32?)null;
                            matchcompititotorsDBModel.HtShots = _dataRow["HtShots"] != DBNull.Value ? Convert.ToInt32(_dataRow["HtShots"]) : (Int32?)null;
                            matchcompititotorsDBModel.HtShotsOnGoal = _dataRow["HtShotsOnGoal"] != DBNull.Value ? Convert.ToInt32(_dataRow["HtShotsOnGoal"]) : (Int32?)null;
                            matchcompititotorsDBModel.HtTotalAssists = _dataRow["HtTotalAssists"] != DBNull.Value ? Convert.ToInt32(_dataRow["HtTotalAssists"]) : (Int32?)null;
                            matchcompititotorsDBModel.HtTotalTouches = _dataRow["HtTotalTouches"] != DBNull.Value ? Convert.ToInt32(_dataRow["HtTotalTouches"]) : (Int32?)null;
                            matchcompititotorsDBModel.HtTouchesPasses = _dataRow["HtTouchesPasses"] != DBNull.Value ? Convert.ToInt32(_dataRow["HtTouchesPasses"]) : (Int32?)null;
                            matchcompititotorsDBModel.HtYellowCards = _dataRow["HtYellowCards"] != DBNull.Value ? Convert.ToInt32(_dataRow["HtYellowCards"]) : (Int32?)null;
                            matchcompititotorsDBModel.MatchId = Convert.ToDecimal(_dataRow["MatchId"]);
                            matchDetailBetwayDBModel.MatchCompetitorsStatsComDetailDB = matchcompititotorsDBModel;
                        }
                        foreach (DataRow _dataRow in _dataSet.Tables[17].Rows)
                        {
                            TeamLinupPlayersDBModel teamLineUpPlayersDBModel = new TeamLinupPlayersDBModel();
                            teamLineUpPlayersDBModel.CoachId = _dataRow["CoachId"] != DBNull.Value ? Convert.ToInt16(_dataRow["CoachId"]) : (Int16?)null;
                            teamLineUpPlayersDBModel.CoachName = Convert.ToString(_dataRow["CoachName"]);
                            teamLineUpPlayersDBModel.CountryName = Convert.ToString(_dataRow["CountryName"]);
                            teamLineUpPlayersDBModel.CountryId = _dataRow["CountryId"] != DBNull.Value ? Convert.ToInt16(_dataRow["CountryId"]) : (Int16?)null;
                            teamLineUpPlayersDBModel.DateOfBirth = _dataRow["DateOfBirth"] != DBNull.Value ? Convert.ToDateTime(_dataRow["DateOfBirth"]) : (DateTime?)null;
                            teamLineUpPlayersDBModel.Formation = Convert.ToString(_dataRow["Formation"]);
                            teamLineUpPlayersDBModel.FormationId = Convert.ToInt16(_dataRow["FormationId"]);
                            teamLineUpPlayersDBModel.IsSubstitute = Convert.ToBoolean(_dataRow["IsSubstitute"]);
                            teamLineUpPlayersDBModel.MatchCompetitorId = Convert.ToDecimal(_dataRow["MatchCompetitorId"]);
                            teamLineUpPlayersDBModel.MatchLineupId = Convert.ToDecimal(_dataRow["MatchLineupId"]);
                            teamLineUpPlayersDBModel.MatchLineupPlayerId = Convert.ToDecimal(_dataRow["MatchLineupPlayerId"]);
                            teamLineUpPlayersDBModel.PlayerId = Convert.ToInt32(_dataRow["PlayerId"]);
                            teamLineUpPlayersDBModel.PlayerName = Convert.ToString(_dataRow["PlayerName"]);
                            teamLineUpPlayersDBModel.PlayerPosition = Convert.ToString(_dataRow["PlayerPosition"]);
                            teamLineUpPlayersDBModel.ShirtNo = Convert.ToString(_dataRow["ShirtNo"]);
                            teamLineUpPlayersDBModel.TeamId = Convert.ToInt32(_dataRow["TeamId"]);
                            matchDetailBetwayDBModel.HomeTeamSubsPlayersDB.Add(teamLineUpPlayersDBModel);
                        }
                        foreach (DataRow _dataRow in _dataSet.Tables[18].Rows)
                        {
                            TeamLinupPlayersDBModel teamLineUpPlayersDBModel = new TeamLinupPlayersDBModel();
                            teamLineUpPlayersDBModel.CoachId = _dataRow["CoachId"] != DBNull.Value ? Convert.ToInt16(_dataRow["CoachId"]) : (Int16?)null;
                            teamLineUpPlayersDBModel.CoachName = Convert.ToString(_dataRow["CoachName"]);
                            teamLineUpPlayersDBModel.CountryName = Convert.ToString(_dataRow["CountryName"]);
                            teamLineUpPlayersDBModel.CountryId = _dataRow["CountryId"] != DBNull.Value ? Convert.ToInt16(_dataRow["CountryId"]) : (Int16?)null;
                            teamLineUpPlayersDBModel.DateOfBirth = _dataRow["DateOfBirth"] != DBNull.Value ? Convert.ToDateTime(_dataRow["DateOfBirth"]) : (DateTime?)null;
                            teamLineUpPlayersDBModel.Formation = Convert.ToString(_dataRow["Formation"]);
                            teamLineUpPlayersDBModel.FormationId = Convert.ToInt16(_dataRow["FormationId"]);
                            teamLineUpPlayersDBModel.IsSubstitute = Convert.ToBoolean(_dataRow["IsSubstitute"]);
                            teamLineUpPlayersDBModel.MatchCompetitorId = Convert.ToDecimal(_dataRow["MatchCompetitorId"]);
                            teamLineUpPlayersDBModel.MatchLineupId = Convert.ToDecimal(_dataRow["MatchLineupId"]);
                            teamLineUpPlayersDBModel.MatchLineupPlayerId = Convert.ToDecimal(_dataRow["MatchLineupPlayerId"]);
                            teamLineUpPlayersDBModel.PlayerId = Convert.ToInt32(_dataRow["PlayerId"]);
                            teamLineUpPlayersDBModel.PlayerName = Convert.ToString(_dataRow["PlayerName"]);
                            teamLineUpPlayersDBModel.PlayerPosition = Convert.ToString(_dataRow["PlayerPosition"]);
                            teamLineUpPlayersDBModel.ShirtNo = Convert.ToString(_dataRow["ShirtNo"]);
                            teamLineUpPlayersDBModel.TeamId = Convert.ToInt32(_dataRow["TeamId"]);
                            matchDetailBetwayDBModel.AwayTeamSubsPlayersDB.Add(teamLineUpPlayersDBModel);
                        }

                        if (_dataSet.Tables[19].Rows.Count > 0)
                        {
                            DataRow _dataRow = _dataSet.Tables[19].Rows[0];
                            MatchVenueDetailDBModel matchVenueDetailDBModel = new MatchVenueDetailDBModel();
                            matchVenueDetailDBModel.MatchId = Convert.ToDecimal(_dataRow["MatchId"]);
                            matchVenueDetailDBModel.Spectators = _dataRow["Spectators"] != DBNull.Value ? Convert.ToInt32(_dataRow["Spectators"]) : (Int32?)null;
                            matchVenueDetailDBModel.VenueId = Convert.ToInt32(_dataRow["MatchId"]);
                            matchVenueDetailDBModel.VenueName = Convert.ToString(_dataRow["VenueName"]);
                            matchDetailBetwayDBModel.MatchVenueDetailDB = matchVenueDetailDBModel;
                        }

                        foreach (DataRow _dataRow in _dataSet.Tables[22].Rows)
                        {
                            MatchOfficialsDBModel matchOfficialsDBModel = new MatchOfficialsDBModel();
                            matchOfficialsDBModel.MatchId = Convert.ToDecimal(_dataRow["MatchId"]);
                            matchOfficialsDBModel.OfficialId = Convert.ToInt16(_dataRow["OfficialId"]);
                            matchOfficialsDBModel.OfficialName = Convert.ToString(_dataRow["OfficialName"]);
                            matchOfficialsDBModel.OfficialType = Convert.ToString(_dataRow["OfficialType"]);
                            matchOfficialsDBModel.OfficialTypeId = Convert.ToInt16(_dataRow["OfficialTypeId"]);
                            matchDetailBetwayDBModel.MatchOfficialsDB.Add(matchOfficialsDBModel);
                        }

                        foreach (DataRow _dataRow in _dataSet.Tables[23].Rows)
                        {
                            TeamsStatsDBModel teamStatsDBModel = new TeamsStatsDBModel();
                            teamStatsDBModel.ContestGroupId = Convert.ToInt32(_dataRow["ContestGroupId"]);
                            teamStatsDBModel.MarketId = Convert.ToInt32(_dataRow["MarketId"]);
                            teamStatsDBModel.MarketName = Convert.ToString(_dataRow["MarketName"]);
                            teamStatsDBModel.MatchesPlayed = Convert.ToInt32(_dataRow["MatchesPlayed"]);
                            teamStatsDBModel.Percentage = Convert.ToDecimal(_dataRow["Percentage"]);
                            teamStatsDBModel.Position = Convert.ToInt32(_dataRow["Position"]);
                            teamStatsDBModel.TeamId = Convert.ToInt32(_dataRow["Position"]);
                            teamStatsDBModel.TeamName = Convert.ToString(_dataRow["TeamName"]);
                            matchDetailBetwayDBModel.HomeTeamStatsMarkets.Add(teamStatsDBModel);
                        }

                        foreach (DataRow _dataRow in _dataSet.Tables[24].Rows)
                        {
                            TeamsStatsDBModel teamStatsDBModel = new TeamsStatsDBModel();
                            teamStatsDBModel.ContestGroupId = Convert.ToInt32(_dataRow["ContestGroupId"]);
                            teamStatsDBModel.MarketId = Convert.ToInt32(_dataRow["MarketId"]);
                            teamStatsDBModel.MarketName = Convert.ToString(_dataRow["MarketName"]);
                            teamStatsDBModel.MatchesPlayed = Convert.ToInt32(_dataRow["MatchesPlayed"]);
                            teamStatsDBModel.Percentage = Convert.ToDecimal(_dataRow["Percentage"]);
                            teamStatsDBModel.Position = Convert.ToInt32(_dataRow["Position"]);
                            teamStatsDBModel.TeamId = Convert.ToInt32(_dataRow["Position"]);
                            teamStatsDBModel.TeamName = Convert.ToString(_dataRow["TeamName"]);
                            matchDetailBetwayDBModel.AwayTeamStatsMarkets.Add(teamStatsDBModel);
                        }
                    }
                }
            }
            #endregion

            #region EntiyFrameWork
            //string responseStatus = string.Empty;
            ////MatchDetailBetwayDBModel model = new Models.DBModels.MatchDetailBetwayDBModel();

            //var db = new SportsDataPanelEntities();

            //using (var connection = db.Database.Connection)
            //{
            //    connection.Open();
            //    var command = connection.CreateCommand();
            //    command.CommandText = "EXEC [dbo].[GoalCCAPI_GetMatchDetailByMatchId]'" + matchId + "'";

            //    using (var reader = command.ExecuteReader())
            //    {
            //        model.MatchScoreInfoDB =
            //            ((IObjectContextAdapter)db)
            //                .ObjectContext
            //                .Translate<MatchScoreInfoDBModel>(reader)
            //                .FirstOrDefault();

            //        reader.NextResult();

            //        model.MatchDetailEventsDB =
            //            ((IObjectContextAdapter)db)
            //                .ObjectContext
            //                .Translate<MatchDetailEventsDBModel>(reader)
            //                .ToList();


            //        reader.NextResult();

            //        model.HomeTeamLinupPlayersDB =
            //            ((IObjectContextAdapter)db)
            //            .ObjectContext
            //            .Translate<TeamLinupPlayersDBModel>(reader)
            //            .ToList();

            //        reader.NextResult();

            //        model.AwayTeamLinupPlayersDB =
            //            ((IObjectContextAdapter)db)
            //            .ObjectContext
            //            .Translate<TeamLinupPlayersDBModel>(reader)
            //            .ToList();

            //        reader.NextResult();

            //        model.LastestFifteenMatchesHomeTeamDB =
            //            ((IObjectContextAdapter)db)
            //            .ObjectContext
            //            .Translate<LatestFifteenMatchesDBModel>(reader)
            //            .ToList();

            //        reader.NextResult();

            //        model.LastestFifteenMatchesAwayTeamDB =
            //            ((IObjectContextAdapter)db)
            //            .ObjectContext
            //            .Translate<LatestFifteenMatchesDBModel>(reader)
            //            .ToList();

            //        reader.NextResult();

            //        model.MatchSubstitutionsDB =
            //            ((IObjectContextAdapter)db)
            //            .ObjectContext
            //            .Translate<MatchSubstitutionsDBModel>(reader)
            //            .ToList();

            //        reader.NextResult();

            //        model.MatchStatsDB =
            //                                ((IObjectContextAdapter)db)
            //                                .ObjectContext
            //                                .Translate<StatsMarketsDetailDBModel>(reader)
            //                                .ToList();

            //        reader.NextResult();

            //        model.MatchCommentaryDetailDB =
            //                                ((IObjectContextAdapter)db)
            //                                .ObjectContext
            //                                .Translate<MatchCommentaryDetailDBModel>(reader)
            //                                .ToList();

            //        reader.NextResult();

            //        model.Head2HeadMatchesDB =
            //                                ((IObjectContextAdapter)db)
            //                                .ObjectContext
            //                                .Translate<Head2HeadMatchDetailDBModel>(reader)
            //                                .ToList();

            //        reader.NextResult();

            //        model.MatchNewsDB =
            //                        ((IObjectContextAdapter)db)
            //                        .ObjectContext
            //                        .Translate<MatchNewsDetailDBModel>(reader)
            //                        .ToList();

            //        reader.NextResult();

            //        model.MatchValueBetsDetailDB =
            //                            ((IObjectContextAdapter)db)
            //                            .ObjectContext
            //                            .Translate<StatsMarketsDetailDBModel>(reader)
            //                            .ToList();

            //        reader.NextResult();

            //        //model.HomeContestPlayers =
            //        //                    ((IObjectContextAdapter)db)
            //        //                    .ObjectContext
            //        //                    .Translate<ContestPlayersDetail>(reader)
            //        //                    .ToList();

            //        reader.NextResult();

            //        //model.AwayContestPlayers =
            //        //                    ((IObjectContextAdapter)db)
            //        //                    .ObjectContext
            //        //                    .Translate<ContestPlayersDetail>(reader)
            //        //                    .ToList();

            //        reader.NextResult();

            //        //model.HomePlayerStatsDetail =
            //        //                    ((IObjectContextAdapter)db)
            //        //                    .ObjectContext
            //        //                    .Translate<PlayerStatsMatchDetail>(reader)
            //        //                    .ToList();

            //        reader.NextResult();

            //        //model.AwayPlayerStatsDetail =
            //        //                    ((IObjectContextAdapter)db)
            //        //                    .ObjectContext
            //        //                    .Translate<PlayerStatsMatchDetail>(reader)
            //        //                    .ToList();

            //        reader.NextResult();

            //        model.MatchCompetitorsStatsComDetailDB =
            //                            ((IObjectContextAdapter)db)
            //                            .ObjectContext
            //                            .Translate<MatchCompetitorsStatsComDetailDBModel>(reader)
            //                            .FirstOrDefault();

            //        reader.NextResult();

            //        model.HomeTeamSubsPlayersDB =
            //                            ((IObjectContextAdapter)db)
            //                            .ObjectContext
            //                            .Translate<TeamLinupPlayersDBModel>(reader)
            //                            .ToList();

            //        reader.NextResult();

            //        model.AwayTeamSubsPlayersDB =
            //                            ((IObjectContextAdapter)db)
            //                            .ObjectContext
            //                            .Translate<TeamLinupPlayersDBModel>(reader)
            //                            .ToList();

            //        reader.NextResult();

            //        model.MatchVenueDetailDB =
            //                            ((IObjectContextAdapter)db)
            //                            .ObjectContext
            //                            .Translate<MatchVenueDetailDBModel>(reader)
            //                            .FirstOrDefault();

            //        reader.NextResult();
            //        reader.NextResult();
            //        reader.NextResult();

            //        model.MatchOfficialsDB =
            //               ((IObjectContextAdapter)db)
            //                   .ObjectContext
            //                   .Translate<MatchOfficialsDBModel>(reader)
            //                   .ToList();

            //        reader.NextResult();

            //        model.HomeTeamStatsMarkets = ((IObjectContextAdapter)db)
            //                                .ObjectContext
            //                                .Translate<TeamsStatsDBModel>(reader)
            //                                .ToList();

            //        reader.NextResult();

            //        model.AwayTeamStatsMarkets = ((IObjectContextAdapter)db)
            //                            .ObjectContext
            //                            .Translate<TeamsStatsDBModel>(reader)
            //                            .ToList();


            //        reader.Close();
            //        command.Dispose();


            //    }
            //    connection.Close();
            //    connection.Dispose();
            //} 
            #endregion

            globalParametersModel.ContestGroupId = matchDetailBetwayDBModel.MatchScoreInfoDB.ContestGroupId;
            matchDetailBetwayDBModel.LeagueTable = GetLeagueTableByContestGroupId(globalParametersModel);
            return matchDetailBetwayDBModel;
        }
        #endregion
    }
}