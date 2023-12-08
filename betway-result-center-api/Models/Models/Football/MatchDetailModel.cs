using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace betway_result_center_api.Models.Models.Football
{

    public class MatchDetailBetwayModel
    {
        public decimal MatchId { get; set; }
        public string MatchDate { get; set; }
        public string MatchTime { get; set; }
        public Int16 MatchStatusId { get; set; }
        public string MatchStatus { get; set; }
        public Int64 OddsMatchId { get; set; }
        public string CurrentMinutes { get; set; }
        //public Int16? CurrentMinutes { get; set; }
        //public bool? MinutePlusBit { get; set; }
        //public string PlusMinutes { get; set; }
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
        public string HTHomeScore { get; set; }
        public string HTAwayScore { get; set; }
        public string FullTimeScore { get; set; }
        public string FTHomeScore { get; set; }
        public string FTAwayScore { get; set; }
        public string LiveScore { get; set; }
        public string LiveHomeScore { get; set; }
        public string LiveAwayScore { get; set; }

        public LineupFormationModel HomeLineUpFormation { get; set; }
        public LineupFormationModel AwayLineUpFormation { get; set; }

        public ActiveTabsInfoModel ActiveTabs { get; set; }
        public List<MatchDetailEventsModel> MatchEvents { get; set; }
        public List<MatchCommentaryDetailModel> MatchCommentary { get; set; }
        public List<TeamLinupPlayersModel> HomeTeamLineupPlayers { get; set; }
        public List<TeamLinupPlayersModel> AwayTeamLineupPlayers { get; set; }
        public LeagueTableModel LeagueTable { get; set; }
        public List<Head2HeadMatchDetailModel> Head2HeadMatches { get; set; }
        public List<LastFifteenMatchesDetailModel> LastFifteenHomeTeamMatches { get; set; }
        public List<LastFifteenMatchesDetailModel> LastFifteenAwayTeamMatches { get; set; }
        public List<MatchNewsDetailModel> MatchPreviews { get; set; }
        public List<MatchNewsDetailModel> MatchReports { get; set; }
        public List<MatchNewsDetailModel> MatchNews { get; set; }
        public MatchCompetitorsStatsComDetailModel Statistics { get; set; }
        public MatchVenueDetailModel MatchVenue { get; set; }
        public List<MatchOfficialsDetailModel> MatchOfficials { get; set; }
        public List<ContestTeamsStatsModel> HomeTeamStatsMarkets { get; set; }
        public List<ContestTeamsStatsModel> AwayTeamStatsMarkets { get; set; }
    }

    public class LineupFormationModel
    {
        public int OuterCount { get; set; }
        public List<LineupFormationInnerCountModel> InnerCountValue { get; set; }
    }

    public class LineupFormationInnerCountModel
    {
        public int OuterCount { get; set; }
        public List<int> InnerCountValue { get; set; }
    }

    public class ActiveTabsInfoModel
    {
        public bool IsInfo { get; set; }
        public bool IsCommentary { get; set; }
        public bool IsStatistics { get; set; }
        public bool IsLineup { get; set; }
        public bool IsLeagueTable { get; set; }
        public bool IsHead2Head { get; set; }
        public bool IsOdds { get; set; }
        public bool IsPreviews { get; set; }
        public bool IsReports { get; set; }
        public bool IsNews { get; set; }
    }

    public class MatchDetailEventsModel
    {
        public int PlayerId { get; set; }
        //public string Player { get; set; }
        //public string OtherName { get; set; }
        public string PlayerName { get; set; }
        public bool HomeAway { get; set; }
        public Int16 EventTypeId { get; set; }
        public string EventType { get; set; }
        public string EventMinute { get; set; }
        public string Score { get; set; }
    }

    public class MatchCommentaryDetailModel
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

    public class TeamLinupPlayersModel
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
        public string DateOfBirth { get; set; }
        public string ShirtNo { get; set; }
        public bool IsSubstitute { get; set; }
        public Int16? CoachId { get; set; }
        public string CoachName { get; set; }
        public Int16? CountryId { get; set; }
        public string CountryName { get; set; }
    }

    public class Head2HeadMatchDetailModel
    {
        public decimal MatchId { get; set; }
        public string MatchDate { get; set; }
        public string MatchTime { get; set; }
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

    public class LastFifteenMatchesDetailModel
    {
        public decimal MatchId { get; set; }
        public string MatchDate { get; set; }
        public string MatchTime { get; set; }
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
    }

    public class MatchNewsDetailModel
    {
        public int NewsId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ModifiedDate { get; set; }
        public string Title { get; set; }
        public string Headline { get; set; }
        public string Strapline { get; set; }
        public string ImageURL { get; set; }
        public string BodyText { get; set; }
        public int? ProviderNewsId { get; set; }
    }

    public class MatchCompetitorsStatsComDetailModel
    {
        public decimal MatchId { get; set; }
        public int HtCorners { get; set; }
        public int HtCornersPercentage { get; set; }
        public int AtCorners { get; set; }
        public int AtCornersPercentage { get; set; }

        public int HtShotsOnTarget { get; set; }
        public int HtShotsOnTargetPercentage { get; set; }
        public int AtShotsOnTarget { get; set; }
        public int AtShotsOnTargetPercentage { get; set; }

        public double HtPossessionPercentage { get; set; }
        public double AtPossessionPercentage { get; set; }

        public int HtYellowCards { get; set; }
        public int HtYellowCardsPercentage { get; set; }
        public int AtYellowCards { get; set; }
        public int AtYellowCardsPercentage { get; set; }

        public int HtRedCards { get; set; }
        public int HtRedCardsPercentage { get; set; }
        public int AtRedCards { get; set; }
        public int AtRedCardsPercentage { get; set; }

        public int HtShotsOffTarget { get; set; }
        public int HtShotsOffTargetPercentage { get; set; }
        public int AtShotsOffTarget { get; set; }
        public int AtShotsOffTargetPercentage { get; set; }

        public int HtAttacks { get; set; }
        public int HtAttacksPercentage { get; set; }
        public int AtAttacks { get; set; }
        public int AtAttacksPercentage { get; set; }

        public int HtDangerousAttacks { get; set; }
        public int HtDangerousAttacksPercentage { get; set; }
        public int AtDangerousAttacks { get; set; }
        public int AtDangerousAttacksPercentage { get; set; }
    }

    public class LastFifteenMatchesModel
    {
        public decimal MatchId { get; set; }
        public string MatchDate { get; set; }
        public string MatchTime { get; set; }
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
    }

    public class MatchVenueDetailModel
    {
        public Int32 VenueId { get; set; }
        public string VenueName { get; set; }
        public int? Spectators { get; set; }
    }

    public class MatchOfficialsDetailModel
    {
        public Int16 OfficialId { get; set; }
        public string OfficialName { get; set; }
        public Int16 OfficialTypeId { get; set; }
        public string OfficialType { get; set; }
    }
}