namespace CineAntibes.ViewModels
{
    public class ReservationViewModel : BaseViewModel
    {
        #region Properties
        // the reservation url
        private string reservationUrl = string.Empty;
        public string ReservationUrl
        {
            get
            {
                return reservationUrl;
            }
            set
            {
                reservationUrl = value;
                OnPropertyChanged();
            }
        }
        #endregion
    }
}
