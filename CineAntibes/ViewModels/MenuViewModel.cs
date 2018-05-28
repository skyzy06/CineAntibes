using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using CineAntibes.Models;
using CineAntibes.Resources;
using CineAntibes.Views;
using Xamarin.Forms;

namespace CineAntibes.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        // Reference to the MasterDetailPage to change the current page
        public Main MainPage { protected get; set; }

        public MenuViewModel()
        {
            MenuItemList = new ObservableCollection<MenuEntry>
            {
                new MenuEntry{
                    Title = AppResources.ScheduleTitle,
                    IconSource = "ic_access_time.png",
                    TargetType = typeof(Schedule)
                },
                new MenuEntry{
                    Title = AppResources.WhatsOnTitle,
                    IconSource = "ic_on_display.png",
                    TargetType = typeof(WhatsOn)
                },
                new MenuEntry{
                    Title = AppResources.ComingSoonTitle,
                    IconSource = "ic_movie_creation.png",
                    TargetType = typeof(ComingSoon)
                },
                new MenuEntry{
                    Title = AppResources.WithinAnHourTitle,
                    IconSource = "ic_exposure_plus_1.png",
                    TargetType = typeof(WithinAnHour)
                }
            };
        }

        #region Properties
        // menu list
        ObservableCollection<MenuEntry> menuItemList = new ObservableCollection<MenuEntry>();
        public ObservableCollection<MenuEntry> MenuItemList
        {
            get { return menuItemList; }
            set
            {
                menuItemList = value;
                OnPropertyChanged();
            }
        }

        // active page
        MenuEntry selectedMenuItem;
        public MenuEntry SelectedMenuItem
        {
            get { return selectedMenuItem; }
            set
            {
                if (selectedMenuItem != value)
                {
                    selectedMenuItem = value;
                    OpenMenuPage();
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Methods
        void OpenMenuPage()
        {
            try
            {
                MainPage.Detail = new NavigationPage((Page)Activator.CreateInstance(SelectedMenuItem.TargetType));
                SelectedMenuItem = null;
                MainPage.IsPresented = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
        #endregion
    }
}
