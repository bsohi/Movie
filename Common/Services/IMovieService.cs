using Common.Dto;
using Common.Helper;
using System.Collections.Generic;

namespace Common.Services
{
    public interface IMovieService
    {
        ApiResponse<IEnumerable<MovieDto>> ListMovies();
        ApiResponse<MovieDto> GetMovieDtoById(int id);
        ApiResponse AddMovie(MovieDto userDto);
        ApiResponse UpdateMovie(MovieDto userDto);
    }
}
