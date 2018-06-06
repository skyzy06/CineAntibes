using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CineAntibes.Models;
using CineAntibes.Views;

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
                    if (value != null)
                    {
                        OpenMovieDetailAsync();
                    }
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


        async void OpenMovieDetailAsync()
        {
            MovieDetail moviePage = new MovieDetail();
            moviePage.GetContext().CurrentMovie = SelectedMovie;

            await GetCurrentNavigation().PushAsync(moviePage);

            SelectedMovie = null;
        }
        #endregion
    }
}
