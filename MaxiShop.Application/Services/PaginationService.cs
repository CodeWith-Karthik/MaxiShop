using AutoMapper;
using MaxiShop.Application.InputModels;
using MaxiShop.Application.Services.Interface;
using MaxiShop.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Application.Services
{
    public class PaginationService<T, S> : IPaginationService<T, S> where T : class 
    {
        private readonly IMapper _mapper;

        public PaginationService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public PaginationVM<T> GetPagination(List<S> source, PaginationInputModel pagination)
        {
            var currentPage = pagination.PageNumber;

            var pageSize = pagination.PageSize;

            var totalNoOfRecords = source.Count;

            var totalPages = (int)Math.Ceiling(totalNoOfRecords / (double)pageSize);

            var result = source
                .Skip((pagination.PageNumber - 1) * (pagination.PageSize))
                .Take(pagination.PageSize)
                .ToList();

            var items  = _mapper.Map<List<T>>(result);

            PaginationVM<T> paginationVM = new PaginationVM<T>(currentPage, totalPages, pageSize, totalNoOfRecords, items);

            return paginationVM;
        }
    }
}
