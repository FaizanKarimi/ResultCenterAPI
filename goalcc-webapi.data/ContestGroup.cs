//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace goalcc_webapi.data
{
    using System;
    using System.Collections.Generic;
    
    public partial class ContestGroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ContestGroup()
        {
            this.LeagueTables = new HashSet<LeagueTable>();
        }
    
        public int ContestGroupId { get; set; }
        public Nullable<long> SystemId { get; set; }
        public short SportId { get; set; }
        public int LeagueId { get; set; }
        public short SeasonId { get; set; }
        public Nullable<short> CountryId { get; set; }
        public Nullable<short> SportOrganizationId { get; set; }
        public short GenderId { get; set; }
        public short LeagueTypeId { get; set; }
        public Nullable<short> LTTypeAndSurfaceId { get; set; }
        public Nullable<short> ContestTypeId { get; set; }
        public string ContestGroupName { get; set; }
        public string Code { get; set; }
        public string ShortName { get; set; }
        public string ShortCode { get; set; }
        public string MobileShortName { get; set; }
        public bool IsInternational { get; set; }
        public bool IsActive { get; set; }
        public bool IsLive { get; set; }
        public string Ranking { get; set; }
        public Nullable<bool> HasTable { get; set; }
        public string NameEnglish { get; set; }
        public string NameDanish { get; set; }
        public string NameNorwegian { get; set; }
        public string NameSwedish { get; set; }
        public string NameDeutsche { get; set; }
        public string NameItalian { get; set; }
        public string NameSpanish { get; set; }
        public string NameSerbian { get; set; }
        public string SportCCContestGroupId { get; set; }
        public string CrawlerLink { get; set; }
        public Nullable<bool> AutoUpdateMatches { get; set; }
        public Nullable<int> SharkScoresId { get; set; }
        public Nullable<bool> Goals { get; set; }
        public Nullable<bool> Cards { get; set; }
        public Nullable<bool> Subs { get; set; }
        public Nullable<bool> Lineups { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<bool> Finished { get; set; }
        public Nullable<decimal> WinPoints { get; set; }
        public Nullable<decimal> WinETPenPoints { get; set; }
        public Nullable<decimal> LossPoints { get; set; }
        public Nullable<decimal> LossETPenPoints { get; set; }
        public byte[] change_stamp { get; set; }
        public Nullable<int> TotalContestTeams { get; set; }
        public string LeagueNote { get; set; }
        public Nullable<int> Bet365LeagueId { get; set; }
        public string FixtureLink1 { get; set; }
        public string FixtureLink2 { get; set; }
        public string FixtureLink3 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LeagueTable> LeagueTables { get; set; }
    }
}
