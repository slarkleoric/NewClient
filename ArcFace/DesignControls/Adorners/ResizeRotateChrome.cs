using System.Windows;
using System.Windows.Controls;

namespace ArcFaceClient.DesignControls.Adorners
{
    public class ResizeRotateChrome : Control
    {
        static ResizeRotateChrome()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ResizeRotateChrome), new FrameworkPropertyMetadata(typeof(ResizeRotateChrome)));
        }
    }
}
