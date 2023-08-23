using MaxiShop.Application.DTO.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Application.Services.Interface
{
    public interface ICategoryService
    {
        Task<CategoryDto> GetByIdAsync(int id);

        Task<IEnumerable<CategoryDto>> GetAllAsync();

        Task<CategoryDto> CreateAsync(CreateCategoryDto createCategoryDto);

        Task UpdateAsync(UpdateCategoryDto updateCategoryDto);

        Task DeleteAsync(int id);
    }
}
