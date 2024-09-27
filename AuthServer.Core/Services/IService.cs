﻿using AuthServer.SharedLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Core.Services
{
    public interface IService<Entity, Dto> where Entity : class where Dto : class
    {
        Task<Response<Dto>> GetByIdAsync(int id);
        Task<Response<IEnumerable<Dto>>> GetAllAsync();
        Task<Response<Dto>> AddAsync(Entity entity);
        Task<Response<NoContentDto>> Remove(Entity entity);
        Task<Response<NoContentDto>> Update(Entity entity);
        Task<Response<IEnumerable<Dto>>> Where(Expression<Func<Entity, bool>> expression);
    }
}
