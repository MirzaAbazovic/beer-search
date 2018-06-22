using System;

namespace BeerSearch.Model
{
    public partial class Beer : BaseEntity<string>
    {
        public string Name { get; set; }
        public string NameDisplay { get; set; }
        public string Description { get; set; }
        public string Abv { get; set; }
        public string Ibu { get; set; }
        public long GlasswareId { get; set; }
        public long SrmId { get; set; }
        public long AvailableId { get; set; }
        public long StyleId { get; set; }
        public string IsOrganic { get; set; }
        public Label Labels { get; set; }
        public string Status { get; set; }
        public string StatusDisplay { get; set; }
        public string OriginalGravity { get; set; } 
        public Glass Glass { get; set; }
        public Srm Srm { get; set; }
        public Available Available { get; set; }
        public Style Style { get; set; }
        public DateTimeOffset InsertDate { get; set; }
        public DateTimeOffset UpdateDate { get; set ; }
    }
}
