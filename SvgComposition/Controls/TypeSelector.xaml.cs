using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SvgComposition.Model;

namespace SvgComposition.Controls
{
    /// <summary>
    /// Interaction logic for TypeSelector.xaml
    /// </summary>
    public partial class TypeSelector : UserControl, INotifyPropertyChanged
    {
        private ObservableCollection<string> _nTypeTypes;
      
        public TypeSelector()
        {
            DataContext = this;

            InitializeComponent();
           
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (NTypeTypes != null)
            {
                NTypeTypes.Clear();
            }

            NTypeTypes = new ObservableCollection<string>(SvgAttribute.SvgAttributeTypeText);
            NTypeCombo.SelectedIndex = 0;
        }


        public static readonly DependencyProperty IsTypeTextProperty =
            DependencyProperty.Register(
                "IsTypeText",
                typeof(string),
                typeof(TypeSelector), new FrameworkPropertyMetadata("None",
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    null)
            );

        public string IsTypeText
        {
            get { return (string)GetValue(IsTypeTextProperty); }
            set
            {
                if((string)GetValue(IsTypeTextProperty) != value)
                {
                    SetValue(IsTypeTextProperty, value);
                    OnPropertyChanged("IsTypeText");
                }
            }
        }

        public ObservableCollection<string> NTypeTypes
        {
            get => _nTypeTypes;
            set
            {
                _nTypeTypes = value;
                OnPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }


        private void NTypeCombo_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IsTypeText = NTypeCombo.SelectedItem as string;
        }
    }


}
