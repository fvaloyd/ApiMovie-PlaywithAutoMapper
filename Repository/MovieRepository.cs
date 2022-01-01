using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.Dtos;
using MovieApi.Models;
using MovieApi.Repository.Interfaces;

namespace MovieApi.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieContext _context;
        private readonly IMapper _mapper;
        public MovieRepository(MovieContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> CreateMovie(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(x => x.Id == id);
            _context.Movies.Remove(movie);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> EditMovie(int id, UpdateMovieDTO movieDTO)
        {
            var movieToUpdate = await _context.Movies.FirstOrDefaultAsync(x => x.Id == id);
            _mapper.Map(movieDTO,movieToUpdate); 
            return await _context.SaveChangesAsync();
        }

        public async Task<GetMovieDTO> GetMovieById(int id)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(x => x.Id == id);
            var movieDTO = _mapper.Map<GetMovieDTO>(movie);
            return movieDTO;        
        }

        public async Task<GetMovieDTO> GetMovieByName(string name)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(x => x.Name == name);
            var movieDTO = _mapper.Map<GetMovieDTO>(movie);
            return movieDTO; 
        }

        public async Task<IEnumerable<GetMovieDTO>> GetMovies()
        {
            return await _context.Movies.Select(x => _mapper.Map<GetMovieDTO>(x)).ToListAsync();           
        }
    }
}