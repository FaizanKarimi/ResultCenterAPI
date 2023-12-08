using betway_result_center_api.Models.Models.Football;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace betway_result_center_api.Models.DatabaseModels.Football
{
      public class MatchDetailBetwayDBModel
    {
        public MatchScoreInfoDBModel MatchScoreInfoDB { get; set; }
        public List<MatchDetailEventsDBModel> MatchDetailEventsDB { get; set; }
        public List<TeamLinupPlayersDBModel> HomeTeamLinupPlayersDB { get; set; }
        public List<TeamLinupPlayersDBModel> AwayTeamLinupPlayersDB { get; set; }
        public List<LatestFifteenMatchesDBModel> LastestFifteenMatchesHomeTeamDB { get; set; }
        public List<LatestFifteenMatchesDBModel> LastestFifteenMatchesAwayTeamDB { get; set; }
        public List<MatchSubstitutionsDBModel> MatchSubstitutionsDB { get; set; }
        public List<StatsMarketsDetailDBModel> MatchStatsDB { get; set; }
        public List<MatchCommentaryDetailDBModel> MatchCommentaryDetailDB { get; set; }
        public List<Head2HeadMatchDetailDBModel> Head2HeadMatchesDB { get; set; }
        public List<MatchNewsDetailDBModel> MatchNewsDB { get; set; }
        public List<StatsMarketsDetailDBModel> MatchValueBetsDetailDB { get; set; }
        public MatchCompetitorsStatsComDetailDBModel MatchCompetitorsStatsComDetailDB { get; set; }
        public List<TeamLinupPlayersDBModel> HomeTeamSubsPlayersDB { get; set; }
        public List<TeamLinupPlayersDBModel> AwayTeamSubsPlayersDB { get; set; }
        public MatchVenueDetailDBModel MatchVenueDetailDB { get; set; }
        public List<MatchOfficialsDBModel> MatchOfficialsDB { get; set; }
        public List<TeamsStatsDBModel> HomeTeamStatsMarkets { get; set; }
        public List<TeamsStatsDBModel> AwayTeamStatsMarkets { get; set; }
        public LeagueTableModel LeagueTable { get; set; }
    }

    public class MatchScoreInfoDBModel
    {
        public decimal MatchId { get; set; }
        public DateTime MatchDate { get; set; }
        public Int16 MatchStatusId { get; set; }
        public string MatchStatus { get; set; }
        public Int64 OddsMatchId { get; set; }
        public Int16? CurrentMinutes { get; set; }
        public bool? MinutePlusBit { get; set; }
        public string PlusMinutes { get; set; }
        public Int16 CountryId { get; set; }
        public string CountryName { get; set; }
        public int ContestGroupId { get; set; }
        public string ContestGroupName { get; set; }
        public string ContestGroupShortName { get; set; }
        public int HomeTeamId { get; set; }
        public string HomeTeam { get; set; }
        public string HomeTeamShortName { get; set; }
        public int AwayTeamId { get; set; }
        public string AwayTeam { get; set; }
        public string AwayTeamShortName { get; set; }
        public string HalfTimeScore { get; set; }
        public string FullTimeScore { get; set; }
        public string LiveScore { get; set; }
    }

  
 

    public class TeamLinupPlayersDBModel
    {
        public int TeamId { get; set; }
        public decimal MatchLineupId { get; set; }
        public decimal MatchCompetitorId { get; set; }
        public Int16 FormationId { get; set; }
        public string Formation { get; set; }
        public decimal MatchLineupPlayerId { get; set; }
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public string PlayerPosition { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string ShirtNo { get; set; }
        public bool IsSubstitute { get; set; }
        public Int16? CoachId { get; set; }
        public string CoachName { get; set; }
        public Int16? CountryId { get; set; }
        public string CountryName { get; set; }
    }

    public class LatestFifteenMatchesDBModel
    {
        public decimal MatchId { get; set; }
        public DateTime MatchDate { get; set; }
        public int HomeTeamId { get; set; }
        public string HomeTeam { get; set; }
        public string HomeTeamShortName { get; set; }
        public int AwayTeamId { get; set; }
        public string AwayTeam { get; set; }
        public string AwayTeamShortName { get; set; }
        public string HomeScore { get; set; }
        public string AwayScore { get; set; }
        public Int16? CountryId { get; set; }
        public string CountryName { get; set; }
        public int ContestGroupId { get; set; }
        public string ContestGroupName { get; set; }
    }

    public class MatchSubstitutionsDBModel
    {
        public decimal MatchId { get; set; }
        public decimal MatchSubstituteId { get; set; }
        public int PlayerInId { get; set; }
        public int PlayerOutId { get; set; }
        public string PlayerInName { get; set; }
        public string PlayerOutName { get; set; }
        public string SubstitutionMinute { get; set; }
        public bool SubHomeAway { get; set; }
        public Int16? PlayerInCountryId { get; set; }
        public string PlayerInCountryName { get; set; }
        public Int16? PlayerOutCountryId { get; set; }
        public string PlayerOutCountryName { get; set; }
    }

    public class StatsMarketsDetailDBModel
    {
        public decimal HomeVal { get; set; }
        public decimal AwayVal { get; set; }
        public decimal TotalVal { get; set; }
        public string Market { get; set; }
        public string Odds { get; set; }
        public string BookmakerId { get; set; }
    }

    public class MatchCommentaryDetailDBModel
    {
        public Int64 MatchCommentaryId { get; set; }
        public Int16 MatchMinute { get; set; }
        public Int16 CommentType { get; set; }
        public string CommenTypeText { get; set; }
        public string Comment { get; set; }
        public string Duration { get; set; }
        public string AuthorName { get; set; }
        public string AuthorUserName { get; set; }
        public string AuthorPageURL { get; set; }
        public string AuthorProfileImage { get; set; }
        public string MediaImageURL { get; set; }
        public string CurrentDate { get; set; }
        public string ProviderId { get; set; }
    }

    public class Head2HeadMatchDetailDBModel
    {
        public decimal MatchId { get; set; }
        public DateTime MatchDate { get; set; }
        public int HomeTeamId { get; set; }
        public string HomeTeam { get; set; }
        public string HomeTeamShortName { get; set; }
        public int AwayTeamId { get; set; }
        public string AwayTeam { get; set; }
        public string AwayTeamShortName { get; set; }
        public string HomeScore { get; set; }
        public string AwayScore { get; set; }
        public Int16 CountryId { get; set; }
        public string CountryName { get; set; }
        public int ContestGroupId { get; set; }
        public string ContestGroupName { get; set; }
        public Int16 SeasonId { get; set; }
        public string SeasonName { get; set; }
    }

    public class MatchNewsDetailDBModel
    {
        public int NewsId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Title { get; set; }
        public string Headline { get; set; }
        public string Strapline { get; set; }
        public string ImageURL { get; set; }
        public string BodyText { get; set; }
        public int? ProviderNewsId { get; set; }
    }

    public class MatchCompetitorsStatsComDetailDBModel
    {
        public decimal MatchId { get; set; }
        public int? HtFoulsCommitted { get; set; }
        public int? AtFoulsCommitted { get; set; }
        public int? HtFoulsSuffered { get; set; }
        public int? AtFoulsSuffered { get; set; }
        public int? HtCornerKicks { get; set; }
        public int? AtCornerKicks { get; set; }
        public int? HtTotalTouches { get; set; }
        public int? AtTotalTouches { get; set; }
        public int? HtTouchesPasses { get; set; }
        public int? AtTouchesPasses { get; set; }
        public int? HtShotsOnGoal { get; set; }
        public int? AtShotsOnGoal { get; set; }
        public int? HtPenShots { get; set; }
        public int? AtPenShots { get; set; }
        public int? HtShots { get; set; }
        public int? AtShots { get; set; }
        public double? HtPossessionPercentage { get; set; }
        public double? AtPossessionPercentage { get; set; }
        public int? HtOffsides { get; set; }
        public int? AtOffsides { get; set; }
        public int? HtYellowCards { get; set; }
        public int? AtYellowCards { get; set; }
        public int? HtRedCards { get; set; }
        public int? AtRedCards { get; set; }
        public int? HtTotalAssists { get; set; }
        public int? AtTotalAssists { get; set; }
        public int? HtAttacks { get; set; }
        public int? AtAttacks { get; set; }
        public int? HtDangerousAttacks { get; set; }
        public int? AtDangerousAttacks { get; set; }
    }

    public class MatchVenueDetailDBModel
    {
        public decimal MatchId { get; set; }
        public Int32 VenueId { get; set; }
        public string VenueName { get; set; }
        public int? Spectators { get; set; }
    }

    public class MatchOfficialsDBModel
    {
        public decimal MatchId { get; set; }
        public Int16 OfficialId { get; set; }
        public string OfficialName { get; set; }
        public Int16 OfficialTypeId { get; set; }
        public string OfficialType { get; set; }
    }

    public class MatchDetailEventsDBModel
    {
        public decimal MatchId { get; set; }
        public decimal EventId { get; set; }
        public int PlayerId { get; set; }
        public string Player { get; set; }
        public string OtherName { get; set; }
        public bool HomeAway { get; set; }
        public Int16 EventTypeId { get; set; }
        public string EventType { get; set; }
        public string PlayerName { get; set; }
        public string EventMinute { get; set; }
        public string Score { get; set; }
    }
}