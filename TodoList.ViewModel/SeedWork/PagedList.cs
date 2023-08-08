using TodoList.ViewModel.SeedWork;

namespace TodoListBlazorWasm
{
    public class PagedList<T> where T : class
    {
        public MetaData MetaData { get; set; }
        public List<T> Items { get; set; }

        public PagedList()
        {

        }

        public PagedList(List<T> items, int totalCount, int pageSize, int pageNumber)
        {
            MetaData = new MetaData()
            {
                TotalCount = totalCount,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPage = (int)Math.Ceiling(totalCount / (double)pageSize)
            };
            Items = items;
        }
    }
}
