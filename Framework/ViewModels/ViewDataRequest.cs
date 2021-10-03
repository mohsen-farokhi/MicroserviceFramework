namespace Framework.ViewModels
{
    public abstract class ViewDataRequest
    {
        public int PageIndex { get; set; }

        private int _pageSize;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                if (value == 0)
                {
                    _pageSize = 10;
                }
                else
                {
                    _pageSize = value;
                }
            }
        }

        public int TotalCount { get; set; }
    }
}
