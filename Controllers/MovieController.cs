using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieApi.Dtos;
using MovieApi.Models;
using MovieApi.Repository.Interfaces;

namespace MovieApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _repo;
        public MovieController(IMovieRepository repo)
        {
            _repo = repo;
        }

        // GET : all movies
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _repo.GetMovies());
        }

        // GET : by id
        [HttpGet("{id}")]
        public async Task<ActionResult<GetMovieDTO>> Get(int id)
        {
            if(id <= 0) return BadRequest();
            var movieDTO = await _repo.GetMovieById(id);
            if(movieDTO != null)return movieDTO;
            return NotFound();
        }

        // GET : by name
        [HttpGet("name/{name}")]
        public async Task<ActionResult<GetMovieDTO>> Get(string name)
        {
            var movieDTO = await _repo.GetMovieByName(name);
            if(movieDTO != null)return movieDTO;
            return NotFound();
        }

        // POST : create movie
        [HttpPost]
        public async Task<ActionResult<GetMovieDTO>> Post(Movie movie)
        {
            if(movie != null)
            {
                await _repo.CreateMovie(movie);
                return CreatedAtAction(nameof(Get), new {id = movie.Id}, movie);
            }
            return BadRequest();
        }

        // PUT : edit movie
        [HttpPut("{id}")]
        public async Task<ActionResult<GetMovieDTO>> Put(int id, UpdateMovieDTO movieDTO)
        {
            if(id <= 0)NotFound("id incorrecto");
            if(id != movieDTO.Id)BadRequest("los id no coinciden");
            
            if(movieDTO != null)
            {
                if(await _repo.EditMovie(id,movieDTO) == 1)return Ok(movieDTO);
                return BadRequest("metodo edit retorno un error");
            }
            return NotFound("objeto no valido");
            
        }

        // DELETE : delete movie
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delete(int id)
        {
            if(id == 0) return NotFound();
            if(await _repo.DeleteMovie(id) > 0)return Ok(id);
            return BadRequest();
        }

    }
}