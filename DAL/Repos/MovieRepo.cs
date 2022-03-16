using Common.Authentication;
using Common.Dto;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repos
{
    public interface IMovieRepo : IRepo<Movie>
    {
        IEnumerable<MovieDto> ListMovies();
        MovieDto GetMovieDtoById(int id);
        int UpsertMovie(Movie user);
        Movie GetMovieById(int id);
    }
    public class MovieRepo : BaseRepo<Movie>, IMovieRepo
    {
        public MovieRepo(IAuthenticatedUser authenticatedUser, MovieSaaSContext saasDb) : base(authenticatedUser, saasDb) { }

        public Movie GetMovieById(int id)
        {
            return _saasDB.Movies.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<MovieDto> ListMovies()
        {
            var movies = from u in _saasDB.Movies
                         join lv in _saasDB.ListValues on u.GenreId equals lv.Id
                        select new MovieDto
                        {
                            Id = u.Id,
                            Name = u.Name,
                            Cost = u.Cost,
                            SalePrice = u.SalePrice,
                            GenreId = u.GenreId,
                            Genre = lv.Name
                        };

            return movies.ToList();
        }

        public MovieDto GetMovieDtoById(int id)
        {
            var movies = from u in _saasDB.Movies
                        where u.Id == id
                        select new MovieDto
                        {
                            Id = u.Id,
                            Name = u.Name,
                            Cost = u.Cost,
                            SalePrice = u.SalePrice,
                            GenreId = u.GenreId
                        };

            return movies.FirstOrDefault();
        }
        public int UpsertMovie(Movie movie)
        {
            try
            {
                if (movie.Id == 0)
                {
                    _saasDB.Entry(movie).State = EntityState.Added;
                }
                _saasDB.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Data Layer - General Exception:", ex.InnerException);
            }
            return movie.Id;
        }
    }
}
