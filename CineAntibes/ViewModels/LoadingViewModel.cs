namespace CineAntibes.ViewModels
{
    public class LoadingViewModel : BaseViewModel
    {
        #region Properties
        string appVersion = "0.0.1";
        public string AppVersion
        {
            get { return appVersion; }
            set
            {
                if (appVersion != value)
                {
                    appVersion = value;
                    OnPropertyChanged();
                }
            }
        }

        string loadingMessage = Resources.AppResources.LoadingMessage;
        public string LoadingMessage
        {
            get { return loadingMessage; }
            set
            {
                if (loadingMessage != value)
                {
                    loadingMessage = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion
    }
}
