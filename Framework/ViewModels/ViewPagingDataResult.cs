using System.Collections.Generic;

namespace Framework.ViewModels
{
    public class ViewPagingDataResult<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public IEnumerable<T> Result { get; set; } = new List<T>();
    }
}
