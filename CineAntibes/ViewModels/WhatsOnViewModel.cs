using System.Collections.ObjectModel;
using CineAntibes.Models;

namespace CineAntibes.ViewModels
{
    public class WhatsOnViewModel : BaseViewModel
    {
        public WhatsOnViewModel()
        {
            if (App.CinemaTable.GetCurrentCinema() != null)
            {
                ListMovies = new ObservableCollection<Movie>(App.MovieTable.GetWhatsOnMovie());
            }
        }
        #region Properties
        Movie selectedMovie;
        public Movie SelectedMovie
        {
            get { return selectedMovie; }
            set
            {
                if (selectedMovie != value)
                {
                    selectedMovie = value;
                    OnPropertyChanged();
                }
            }
        }

        ObservableCollection<Movie> listMovies;
        public ObservableCollection<Movie> ListMovies
        {
            get { return listMovies; }
            set
            {
                listMovies = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Methods

        #endregion
    }
}
