using Common.Authentication;
using Common.Dto;
using Common.DTO;
using Common.Helper;
using Common.Services;
using DAL.Models;
using DAL.Repos;
using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;

namespace BLL.Services
{
    public class MovieService : BaseService, IMovieService
    {
        private static HashAlgorithm passwordHasher = HashAlgorithm.Create("SHA1");        
        public readonly IMovieRepo _movieRepo;
        public readonly IListValuesRepo _listValuesRepo;

        public MovieService(IAuthenticatedUser authenticatedUser, IListValuesRepo listValuesRepo, IMovieRepo movieRepo)
            :base(authenticatedUser)
        {
            _listValuesRepo = listValuesRepo;
            _movieRepo = movieRepo;
        }
        
        public ApiResponse<IEnumerable<MovieDto>> ListMovies()
        {
            ApiResponse<IEnumerable<MovieDto>> apiResponse = new ApiResponse<IEnumerable<MovieDto>>();
            apiResponse.Success = true;
            apiResponse.Content = _movieRepo.ListMovies();
            return apiResponse;
        }
        public ApiResponse<MovieDto> GetMovieDtoById(int id)
        {
            ApiResponse<MovieDto> apiResponse = new ApiResponse<MovieDto>();
            var movieDto = new MovieDto
            {
                ListValues = new Dictionary<string, IEnumerable<SelectListValueDto>>()
            };

            if (id > 0)
            {
                movieDto = _movieRepo.GetMovieDtoById(id);
            }

            if(id > 0 && movieDto?.Id == null)
            {
                apiResponse.Success = false;
                apiResponse.ErrorMessages.Add("No Movie found");
                return apiResponse;
            }
            movieDto.ListValues = new Dictionary<string, IEnumerable<SelectListValueDto>>();
            var genres = _listValuesRepo.ListValuesByCategory(Enums.ListCategory.Genre);
            movieDto.ListValues.Add("genre", genres);

            apiResponse.Success = true;
            apiResponse.Content = movieDto;
            return apiResponse;
        }
        public ApiResponse AddMovie(MovieDto movieDto)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                Movie movie = Mapper.Map<Movie>(movieDto);
                SetUserAndDateField<Movie>(movie, true);
                var movieId = _movieRepo.UpsertMovie(movie);
                if (movieId > 0)
                {
                    apiResponse.ReferenceId = movieId;
                    apiResponse.Success = true;
                }
            }
            catch(Exception ex)
            {
                apiResponse.ErrorMessages.Add(ex.Message);
            }
            return apiResponse;
        }
        public ApiResponse UpdateMovie(MovieDto movieDto)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var existingMovie = _movieRepo.GetMovieById(movieDto.Id);
                                
                if (movieDto.PropertiesToUpdate == null || movieDto.PropertiesToUpdate.Count == 0)
                {
                    apiResponse.Success = true;
                    apiResponse.ErrorMessages.Add("No fields to update.");
                    return apiResponse;
                }

                foreach (string fieldName in movieDto.PropertiesToUpdate)
                {
                    PropertyInfo requestedUpdateProperty = movieDto.GetType().GetProperties().FirstOrDefault(x => string.Compare(x.Name, fieldName, true) == 0);
                    PropertyInfo tobeUpdated = existingMovie.GetType().GetProperties().FirstOrDefault(x => string.Compare(x.Name, fieldName, true) == 0);

                    if (requestedUpdateProperty != null && tobeUpdated != null)
                    {
                        tobeUpdated.SetValue(existingMovie, requestedUpdateProperty.GetValue(movieDto));
                    }
                }

                SetUserAndDateField<Movie>(existingMovie, false);
                var userId = _movieRepo.UpsertMovie(existingMovie);
                if (userId > 0)
                {
                    apiResponse.ReferenceId = userId;
                    apiResponse.Success = true;
                }
            }
            catch (Exception ex)
            {
                apiResponse.ErrorMessages.Add(ex.Message);
            }
            return apiResponse;
        }
    }
}
