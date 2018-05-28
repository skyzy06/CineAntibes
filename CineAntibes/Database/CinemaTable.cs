using CineAntibes.Models;

namespace CineAntibes.Database
{
    public class CinemaTable : ApplicationDatabase<Cinema>
    {
        public void Save(Cinema cinema)
        {
            database.RunInTransaction(() =>
            {
                if (Find(cin => cin.Key == cinema.Key) != null)
                {
                    database.Update(cinema);
                }
                else
                {
                    database.Insert(cinema);
                }
            });
        }

        public Cinema GetCinemaByKey(string cinemaKey)
        {
            return Find(cin => cin.Key == cinemaKey);
        }

        public Cinema GetCurrentCinema()
        {
            return GetCinemaByKey("antibes-casino");
        }
    }
}
