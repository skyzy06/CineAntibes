using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using CineAntibes.Models;
using CineAntibes.Views;
using Xamarin.Forms;

namespace CineAntibes.ViewModels
{
    public class MovieDetailViewModel : BaseViewModel
    {
        public MovieDetailViewModel()
        {
            Reserve = new Command(ReserveCommand);
        }
        #region Properties
        Movie currentMovie;
        public Movie CurrentMovie
        {
            get { return currentMovie; }
            set
            {
                if (currentMovie != value)
                {
                    currentMovie = value;
                    ListLogos = new ObservableCollection<string>(value.Logos);
                    ListPhotos = new ObservableCollection<string>(value.Photos);
                    OnPropertyChanged();
                }
            }
        }

        ObservableCollection<string> listLogos;
        public ObservableCollection<string> ListLogos
        {
            get { return listLogos; }
            set
            {
                listLogos = value;
                OnPropertyChanged();
            }
        }

        ObservableCollection<string> listPhotos;
        public ObservableCollection<string> ListPhotos
        {
            get { return listPhotos; }
            set
            {
                listPhotos = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands
        public ICommand Reserve { get; private set; }
        async void ReserveCommand()
        {
            try
            {
                string reservationUrl = App.CinemaTable.GetCurrentCinema().ReservationUrl;
                string movieUrl = reservationUrl.Insert(reservationUrl.IndexOf('?'), "/F" + CurrentMovie.ID);

                Debug.WriteLine(movieUrl);

                Reservation reservationPage = new Reservation();
                reservationPage.GetContext().ReservationUrl = movieUrl;

                await GetCurrentNavigation().PushAsync(reservationPage);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
        #endregion
    }
}
