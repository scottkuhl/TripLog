using TripLog.Controls;
using TripLog.UWP.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(DatePickerEntryCell), typeof(DatePickerEntryCellRenderer))]

namespace TripLog.UWP.Renderers
{
    public class DatePickerEntryCellRenderer : EntryCellRenderer
    {
        public override Windows.UI.Xaml.DataTemplate GetTemplate(Cell item)
        {
            return App.Current.Resources["DatePickerEntryCell"] as Windows.UI.Xaml.DataTemplate;
        }
    }
}