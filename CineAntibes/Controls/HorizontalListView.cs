using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;

namespace CineAntibes.Controls
{
    public class HorizontalListView : ScrollView
    {
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create("ItemsSource", typeof(IEnumerable), typeof(HorizontalListView), default(IEnumerable), propertyChanged: OnItemsSourceChanged);

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create("ItemTemplate", typeof(DataTemplate), typeof(HorizontalListView), default(DataTemplate));

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        public void Render()
        {
            if (ItemTemplate == null || ItemsSource == null)
                return;

            var layout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal
            };

            foreach (var item in ItemsSource)
            {
                var viewCell = ItemTemplate.CreateContent() as ViewCell;
                viewCell.View.BindingContext = item;
                layout.Children.Add(viewCell.View);
            }

            Content = layout;
        }

        static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            HorizontalListView hlv = bindable as HorizontalListView;


            bool? isOldObservable = oldValue?.GetType().GetTypeInfo().ImplementedInterfaces.Any(i => i == typeof(INotifyCollectionChanged));
            bool? isNewObservable = newValue?.GetType().GetTypeInfo().ImplementedInterfaces.Any(i => i == typeof(INotifyCollectionChanged));

            if (isOldObservable.GetValueOrDefault(false))
            {
                ((INotifyCollectionChanged)oldValue).CollectionChanged -= hlv.HandleCollectionChanged;
            }

            if (isNewObservable.GetValueOrDefault(false))
            {
                ((INotifyCollectionChanged)newValue).CollectionChanged += hlv.HandleCollectionChanged;
            }


            if (oldValue != newValue)
            {
                hlv.Render();
            }
        }

        private void HandleCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Render();
        }
    }
}
