using AuthServer.Core.Repositories;
using AuthServer.Core.Services;
using AuthServer.Core.UnitOfWork;
using AuthServer.Data.UnitOfWorks;
using AuthServer.Service.MapProfile;
using AuthServer.SharedLibrary.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Service.Services
{
    public class GenericService<Entity, Dto> : IService<Entity, Dto> where Entity : class where Dto : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Entity> _genericRepository;

        public GenericService(IUnitOfWork unitOfWork, IGenericRepository<Entity> genericRepository)
        {
            _unitOfWork = unitOfWork;
            _genericRepository = genericRepository;
        }

        public async Task<Response<Dto>> AddAsync(Dto entity)
        {
            var newEntity = ObjectMapper.Mapper.Map<Entity>(entity);
            await _genericRepository.AddAsync(newEntity);
            await _unitOfWork.CommitAsync();
            
            var newDto = ObjectMapper.Mapper.Map<Dto>(newEntity);
            return Response<Dto>.Success(newDto, 200);
        }

        public async Task<Response<IEnumerable<Dto>>> GetAllAsync()
        {
            var products = ObjectMapper.Mapper.Map<List<Dto>>(await _genericRepository.GetAllAsync());
            return Response<IEnumerable<Dto>>.Success(products, 200);
        }

        public async Task<Response<Dto>> GetByIdAsync(int id)
        {
            var product = await _genericRepository.GetByIdAsync(id);
            if(product == null)
            {
                return Response<Dto>.Fail("Id not found", 404, true);
            }
            var dataDto = ObjectMapper.Mapper.Map<Dto>(product);
            return Response<Dto>.Success(dataDto, 200);
        }

        public async Task<Response<NoContentDto>> Remove(int id)
        {
            var isExistEntity = await _genericRepository.GetByIdAsync(id);
            if(isExistEntity == null)
            {
                return Response<NoContentDto>.Fail("Id not found", 404, true);
            }
            _genericRepository.Remove(isExistEntity);
            await _unitOfWork.CommitAsync();
            return Response<NoContentDto>.Success(204);
        }

        public async Task<Response<NoContentDto>> Update(Dto entity, int id)
        {
            var isExistEntity = await _genericRepository.GetByIdAsync(id);
            if(isExistEntity == null)
            {
                return Response<NoContentDto>.Fail("Id not found", 404, true);
            }

            var updateEntity = ObjectMapper.Mapper.Map<Entity>(entity);
            _genericRepository.Update(updateEntity);
            await _unitOfWork.CommitAsync();
            return Response<NoContentDto>.Success(204);
        }

        public async Task<Response<IEnumerable<Dto>>> Where(Expression<Func<Entity, bool>> expression)
        {
            //IQueryable => data.where(x=> x>5)
            var list = _genericRepository.Where(expression);
            var listDto = ObjectMapper.Mapper.Map<IEnumerable<Dto>>(await list.ToListAsync());
            return Response<IEnumerable<Dto>>.Success(listDto, 200);

        }
    }
}
