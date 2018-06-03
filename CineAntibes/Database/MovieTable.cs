using System.Collections.Generic;
using CineAntibes.Models;

namespace CineAntibes.Database
{
    public class MovieTable : ApplicationDatabase<Movie>
    {
        public void Save(Movie movie)
        {
            database.RunInTransaction(() =>
            {
                if (Find(mov => mov.ID == movie.ID) != null)
                {
                    database.Update(movie);
                }
                else
                {
                    database.Insert(movie);
                }
            });
        }

        public Movie GetMovieByID(int movieID)
        {
            return Find(mov => mov.ID == movieID);
        }

        public Movie GetMovieByTitle(string title)
        {
            return Find(Movie => Movie.Title == title);
        }

        public List<Movie> GetWhatsOnMovie()
        {
            List<Movie> queryResult = new List<Movie>();
            if (App.WhatsOnList != null)
            {
                foreach (string title in App.WhatsOnList)
                {
                    queryResult.Add(GetMovieByTitle(title));
                }

                queryResult.Sort((mov1, mov2) =>
                {
                    return mov2.ReleaseDate.CompareTo(mov1.ReleaseDate);
                });
            }

            return queryResult;
        }
    }
}
