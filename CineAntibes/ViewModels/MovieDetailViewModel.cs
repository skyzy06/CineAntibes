using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
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
            Reserve = new Command<Session>(ReserveCommand);
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
                    ListVFSessions = GenerateSessions(value.VFSessions, "VF");
                    ListVOSessions = GenerateSessions(value.VOSessions, "VO");
                    ListLogos = new ObservableCollection<string>(value.Logos);
                    ListPhotos = new ObservableCollection<string>(value.Photos);
                    OnPropertyChanged();
                }
            }
        }

        ObservableCollection<KeyValuePair<DateTime, List<Session>>> listVFSessions;
        public ObservableCollection<KeyValuePair<DateTime, List<Session>>> ListVFSessions
        {
            get { return listVFSessions; }
            set
            {
                listVFSessions = value;
                OnPropertyChanged();
            }
        }

        ObservableCollection<KeyValuePair<DateTime, List<Session>>> listVOSessions;
        public ObservableCollection<KeyValuePair<DateTime, List<Session>>> ListVOSessions
        {
            get { return listVOSessions; }
            set
            {
                listVOSessions = value;
                OnPropertyChanged();
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
        async void ReserveCommand(Session session = null)
        {
            try
            {
                string reservationUrl = App.CinemaTable.GetCurrentCinema().ReservationUrl;
                string movieUrl = reservationUrl.Insert(reservationUrl.IndexOf('?'), "/F" + CurrentMovie.ID);

                if (session != null)
                {
                    double sessiondate = (session.Date - new DateTime(1970, 1, 1)).TotalMilliseconds;
                    sessiondate /= 1000;
                    sessiondate -= 7200;
                    movieUrl = movieUrl.Insert(movieUrl.IndexOf('?'), "/D" + sessiondate + "/" + session.Language);
                }

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

        #region Methods
        ObservableCollection<KeyValuePair<DateTime, List<Session>>> GenerateSessions(List<DateTime> listSessions, string language)
        {
            ObservableCollection<KeyValuePair<DateTime, List<Session>>> result = new ObservableCollection<KeyValuePair<DateTime, List<Session>>>();

            try
            {
                if (listSessions != null)
                {
                    List<DateTime> orderedSessions = listSessions.Where(d => d > DateTime.Now).OrderBy(d => d).ToList();
                    DateTime currentKey = DateTime.MinValue.Date;

                    foreach (DateTime dt in orderedSessions)
                    {
                        if (!dt.Date.Equals(currentKey))
                        {
                            currentKey = dt.Date;
                            result.Add(new KeyValuePair<DateTime, List<Session>>(dt.Date, new List<Session> {
                                new Session {
                                    Date = dt,
                                    Language = language
                                }
                            }));
                        }
                        else
                        {
                            result.Last().Value.Add(new Session
                            {
                                Date = dt,
                                Language = language
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            return result;
        }
        #endregion
    }
}
