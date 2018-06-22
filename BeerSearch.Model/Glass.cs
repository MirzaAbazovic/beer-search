using System;

namespace BeerSearch.Model
{
    public class Glass : BaseEntity<long>
    {
        public string Name { get; set; }
        public DateTimeOffset CreateDate { get; set; }
    }
}