using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Application.ViewModels
{
    public class PaginationVM<T>
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int PageSize { get; set; }

        public int TotalNoOfRecords { get; set; }

        public List<T> Items { get; set; }

        public bool HasPrevious => CurrentPage > 1;

        public bool HasNext => CurrentPage < TotalPages;

        public PaginationVM(int currentPage, int totalPages, int pageSize, int totalNoOfRecords, List<T> items)
        {
            CurrentPage = currentPage;
            TotalPages = totalPages;
            PageSize = pageSize;
            TotalNoOfRecords = totalNoOfRecords;
            Items = items;
        }
    }
}
