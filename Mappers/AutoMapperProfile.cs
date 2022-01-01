using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MovieApi.Dtos;
using MovieApi.Models;

namespace MovieApi.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<GetMovieDTO,Movie>().ReverseMap();
            CreateMap<UpdateMovieDTO,Movie>().ReverseMap();  
        }      
    }
}