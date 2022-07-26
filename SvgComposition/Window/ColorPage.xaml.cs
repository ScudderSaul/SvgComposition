using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SvgComposition.Window
{
    /// <summary>
    ///     Interaction logic for ColorPage.xaml
    /// </summary>
    public partial class ColorPage : System.Windows.Window, INotifyPropertyChanged
    {
        public ColorPage()
        {
            InitializeComponent();
            DataContext = this;
        }

        public bool IsColorPage = true;

        public ColorSelector Selector
        {
            get { return (Content as ColorSelector); }
            set { Content = value; }
        }


        public event PropertyChangedEventHandler PropertyChanged;

       // [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}