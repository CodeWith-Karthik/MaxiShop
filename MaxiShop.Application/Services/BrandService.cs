using AutoMapper;
using MaxiShop.Application.DTO.Brand;
using MaxiShop.Application.Exceptions;
using MaxiShop.Application.Services.Interface;
using MaxiShop.Domain.Contracts;
using MaxiShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Application.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public BrandService(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<BrandDto> CreateAsync(CreateBrandDto createBrandDto)
        {
            var validator = new CreateBrandDtoValidator();

            var validationResult = await validator.ValidateAsync(createBrandDto);

            if (validationResult.Errors.Any())
            {
                throw new BadRequestException("Invalid Brand Input",validationResult);
            }

            var brand = _mapper.Map<Brand>(createBrandDto);

            var createdEntity = await _brandRepository.CreateAsync(brand);

            var entity = _mapper.Map<BrandDto>(createdEntity);

            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var brand = await _brandRepository.GetByIdAsync(x => x.Id == id);

            await _brandRepository.DeleteAsync(brand);
        }

        public async Task<IEnumerable<BrandDto>> GetAllAsync()
        {
            var brands = await _brandRepository.GetAllAsync();

            return _mapper.Map<List<BrandDto>>(brands);
        }

        public async Task<BrandDto> GetByIdAsync(int id)
        {
            var brand = await _brandRepository.GetByIdAsync(x => x.Id == id);

            return _mapper.Map<BrandDto>(brand);
        }

        public async Task UpdateAsync(UpdateBrandDto updateBrandDto)
        {
            var brand = _mapper.Map<Brand>(updateBrandDto);

            await _brandRepository.UpdateAsync(brand);
        }
    }
}
