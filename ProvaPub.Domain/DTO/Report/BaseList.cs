namespace ProvaPub.Domain.DTO.Report
{
    public class BaseList
    {
        public BaseList()
        {
                
        }

        public BaseList(int count, int rows)
        {
            HasNext = count > rows;
            TotalCount = HasNext? rows : count;
        }

        public int TotalCount { get; set; }
        public bool HasNext { get; set; }
    }
}
