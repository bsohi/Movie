using Common.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using API.Controller;
using Common.Dto;
using Common.Helper;

namespace API.Controllers
{
    //[Route("api/[controller]/[action]")]
    public class MovieController : BaseController
    {
        private readonly IMovieService _movieService;
        public MovieController(IConfiguration configuration, IMovieService movieService)
            :base(configuration)
        {
            _movieService = movieService;
        }
        
        public ActionResult ListMovies()
        {
            var movies = _movieService.ListMovies();
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var movie = _movieService.GetMovieDtoById(id);
            return Ok(movie);
        }
        [HttpPost]
        public ActionResult Post(MovieDto movieDto)
        {
            var apiResponse = new ApiResponse();
            if (movieDto.Id == 0)
            {
                apiResponse = _movieService.AddMovie(movieDto);
            }
            else if(movieDto.Id > 0)
            {
                apiResponse = _movieService.UpdateMovie(movieDto);
            }

            return Ok(apiResponse);
        }
    }    
}
