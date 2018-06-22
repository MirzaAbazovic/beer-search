namespace BeerSearch.Model
{
    public class Available : BaseEntity<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}