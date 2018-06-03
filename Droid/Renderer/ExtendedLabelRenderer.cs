using Android.Content;
using CineAntibes.Controls;
using CineAntibes.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ExtendedLabel), typeof(ExtendedLabelRenderer))]
namespace CineAntibes.Droid.Renderer
{
    public class ExtendedLabelRenderer : LabelRenderer
    {
        public ExtendedLabelRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            ExtendedLabel extendedLabel = (ExtendedLabel)Element;
            if (extendedLabel != null && extendedLabel.MaxLines != -1)
            {
                Control.SetSingleLine(false);
                Control.SetMaxLines(extendedLabel.MaxLines);
            }
        }
    }
}