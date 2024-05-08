using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Bank_Desktop_UI
{
    public class NavButton : ListBoxItem
    {
        static NavButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NavButton), new FrameworkPropertyMetadata(typeof(NavButton)));
        }

        public Uri Navlink
        {
            get { return (Uri)GetValue(NavlinkProperty); }
            set { SetValue(NavlinkProperty, value); }
        }
        public static readonly DependencyProperty NavlinkProperty = DependencyProperty.Register("Navlink", typeof(Uri), typeof(NavButton), new PropertyMetadata(null));


        public ImageSource ImgUrl
        {
            get { return (ImageSource)GetValue(ImgUrlProperty); }
            set { SetValue(ImgUrlProperty, value); }
        }
        public static readonly DependencyProperty ImgUrlProperty = DependencyProperty.Register("ImgUrl", typeof(ImageSource), typeof(NavButton), new PropertyMetadata(null));

        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }
        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(string), typeof(NavButton), new PropertyMetadata(null));
    }
}
