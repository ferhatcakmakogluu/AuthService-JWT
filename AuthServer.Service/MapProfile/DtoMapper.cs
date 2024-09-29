using AuthServer.Core.DTOs;
using AuthServer.Core.Entitiy;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Service.MapProfile
{
    public class DtoMapper : Profile
    {
        public DtoMapper()
        {
            CreateMap<ProductDto,Product>().ReverseMap();
            CreateMap<UserAppDto,UserApp>().ReverseMap();
        }
    }
}
