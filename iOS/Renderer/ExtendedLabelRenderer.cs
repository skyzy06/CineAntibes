using CineAntibes.Controls;
using CineAntibes.iOS.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedLabel), typeof(ExtendedLabelRenderer))]
namespace CineAntibes.iOS.Renderer
{
    public class ExtendedLabelRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            ExtendedLabel extendedLabel = (ExtendedLabel)Element;

            if (extendedLabel != null && extendedLabel.MaxLines != -1)
            {
                Control.Lines = extendedLabel.MaxLines;
            }
        }
    }
}
