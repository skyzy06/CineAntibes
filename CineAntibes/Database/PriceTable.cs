using CineAntibes.Models;

namespace CineAntibes.Database
{
    public class PriceTable : ApplicationDatabase<PriceProfile>
    {
        public void Save(PriceProfile profile)
        {
            database.RunInTransaction(() =>
            {
                if (Find(prof => prof.CinemaKey == profile.CinemaKey) != null)
                {
                    database.Update(profile);
                }
                else
                {
                    database.Insert(profile);
                }
            });
        }

        public PriceProfile GetPriceProfileByKey(string cinemaKey)
        {
            return Find(prof => prof.CinemaKey == cinemaKey);
        }
    }
}
