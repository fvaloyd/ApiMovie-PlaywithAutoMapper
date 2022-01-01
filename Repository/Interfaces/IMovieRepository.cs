using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieApi.Dtos;
using MovieApi.Models;

namespace MovieApi.Repository.Interfaces
{
    public interface IMovieRepository
    {
        // Get all moview
        Task<IEnumerable<GetMovieDTO>> GetMovies();
        // Get by id
        Task<GetMovieDTO> GetMovieById(int id);
        // Get by name
        Task<GetMovieDTO> GetMovieByName(string name);
        // Post/Create movie
        Task<int> CreateMovie(Movie movie);
        // Put/Edit movie
        Task<int> EditMovie(int id,UpdateMovieDTO movieDTO);
        // Delete movie
        Task<int> DeleteMovie(int id);
    }
}