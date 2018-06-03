using Xamarin.Forms;

namespace CineAntibes.Controls
{
    /// <summary>
    /// Add MaxLines control to the default label
    /// </summary>
    public class ExtendedLabel : Label
    {
        public static readonly BindableProperty MaxLinesProperty = BindableProperty.Create("MaxLines", typeof(int), typeof(ExtendedLabel), -1);

        public static readonly BindableProperty IsStrikeThroughProperty = BindableProperty.Create("IsStrikeThrough", typeof(bool), typeof(ExtendedLabel), false);

        public int MaxLines
        {
            get { return (int)GetValue(MaxLinesProperty); }
            set { SetValue(MaxLinesProperty, value); }
        }

        public bool IsStrikeThrough
        {
            get { return (bool)GetValue(IsStrikeThroughProperty); }
            set { SetValue(IsStrikeThroughProperty, value); }
        }
    }
}
