using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;

namespace CineAntibes.Controls
{
    public class HorizontalListView : ScrollView
    {
        double _width;
        double _height;

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(HorizontalListView), default(IEnumerable), propertyChanged: OnItemsSourceChanged);

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(HorizontalListView), default(DataTemplate));

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        public static readonly BindableProperty SpacingProperty =
            BindableProperty.Create(nameof(Spacing), typeof(double), typeof(HorizontalListView), 6.0);

        public double Spacing
        {
            get { return (double)GetValue(SpacingProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        public void Render()
        {
            if (ItemTemplate == null || ItemsSource == null)
                return;

            StackLayout layout = new StackLayout
            {
                Orientation = Orientation == ScrollOrientation.Vertical
                    ? StackOrientation.Vertical : StackOrientation.Horizontal,
                Spacing = Spacing
            };

            foreach (var item in ItemsSource)
            {
                var viewCell = ItemTemplate.CreateContent() as ViewCell;
                viewCell.View.BindingContext = item;
                layout.Children.Add(viewCell.View);
            }

            ScrollView container = new ScrollView
            {
                Content = layout
            };

            Content = container;
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

        void HandleCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Render();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            double oldWidth = _width;
            const double sizenotallocated = -1;

            base.OnSizeAllocated(width, height);
            if (Equals(_width, width) && Equals(_height, height)) return;

            _width = width;
            _height = height;

            // ignore if the previous height was size unallocated
            if (Equals(oldWidth, sizenotallocated)) return;

            // Has the device been rotated ?
            if (!Equals(width, oldWidth))
            {
                Render();
            }
        }
    }
}
