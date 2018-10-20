using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ArcFaceClient.Controls
{
    public class XButton : Button
    {
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register(nameof(Icon), typeof(string), typeof(XButton),
                new PropertyMetadata(string.Empty));

        /// <summary> 图标 </summary>
        public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconSizeProperty =
            DependencyProperty.Register(nameof(IconSize), typeof(int), typeof(XButton),
                new PropertyMetadata(18));

        /// <summary> 图标 </summary>
        public int IconSize
        {
            get { return (int)GetValue(IconSizeProperty); }
            set { SetValue(IconSizeProperty, value); }
        }

        public static readonly DependencyProperty IconMarginProperty =
            DependencyProperty.Register(nameof(IconMargin), typeof(Thickness), typeof(XButton),
                new PropertyMetadata(new Thickness(0)));

        /// <summary> 图标 </summary>
        public Thickness IconMargin
        {
            get { return (Thickness)GetValue(IconMarginProperty); }
            set { SetValue(IconMarginProperty, value); }
        }

        public static readonly DependencyProperty HoverBackgroundProperty =
            DependencyProperty.Register(nameof(HoverBackground), typeof(Brush), typeof(XButton),
                new PropertyMetadata(Brushes.Transparent));

        public Brush HoverBackground
        {
            get { return (Brush)GetValue(HoverBackgroundProperty); }
            set { SetValue(HoverBackgroundProperty, value); }
        }

        public static readonly DependencyProperty HoverBorderProperty =
            DependencyProperty.Register(nameof(HoverBorder), typeof(Brush), typeof(XButton),
                new PropertyMetadata(Brushes.Transparent));

        public Brush HoverBorder
        {
            get { return (Brush)GetValue(HoverBorderProperty); }
            set { SetValue(HoverBorderProperty, value); }
        }

        public static readonly DependencyProperty HoverForegroundProperty = DependencyProperty.Register(
            nameof(HoverForeground), typeof(Brush), typeof(XButton), new PropertyMetadata(Brushes.Transparent));

        public Brush HoverForeground
        {
            get { return (Brush)GetValue(HoverForegroundProperty); }
            set { SetValue(HoverForegroundProperty, value); }
        }


        public static readonly DependencyProperty AllowsAnimationProperty =
            DependencyProperty.Register(nameof(AllowsAnimation), typeof(bool), typeof(XButton),
                new PropertyMetadata(true));

        public static readonly DependencyProperty HoverOpacityProperty = DependencyProperty.Register(
            nameof(HoverOpacity), typeof(double), typeof(XButton), new PropertyMetadata(1D));

        public double HoverOpacity
        {
            get { return (double)GetValue(HoverOpacityProperty); }
            set { SetValue(HoverOpacityProperty, value); }
        }

        /// <summary> 是否启用icon动画 </summary>
        public bool AllowsAnimation
        {
            get { return (bool)GetValue(AllowsAnimationProperty); }
            set { SetValue(AllowsAnimationProperty, value); }
        }
        static XButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(XButton), new FrameworkPropertyMetadata(typeof(XButton)));
        }

        public XButton()
        {
        }
    }
}
