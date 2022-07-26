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
    /// Interaction logic for UnitSelector.xaml
    /// </summary>
    public partial class UnitSelector : UserControl, INotifyPropertyChanged
    {
        private ObservableCollection<string> _uUnitTypes;

        public UnitSelector()
        {
            DataContext = this;

            InitializeComponent();

            Loaded += OnLoaded;
        }

        public static readonly DependencyProperty IsUnitTextProperty =
            DependencyProperty.Register(
                "IsUnitText",
                typeof(string),
                typeof(UnitSelector), new FrameworkPropertyMetadata("Px",
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    null)
            );

        public string IsUnitText
        {
            get { return (string)GetValue(IsUnitTextProperty); }
            set
            {
                if ((string)GetValue(IsUnitTextProperty) != value)
                {
                    for (int ii = 0; ii < NUnitComboBox.Items.Count; ii++)
                    {
                        if ((string)NUnitComboBox.Items[ii] == value)
                        {
                            NUnitComboBox.SelectedIndex = ii;
                            SetValue(IsUnitTextProperty, value);
                            OnPropertyChanged("IsUnitText");
                        }
                    }
                }
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
           UUnitTypes = new ObservableCollection<string>(SvgAttribute.SvgAttributeUnitsText);

           NUnitComboBox.SelectedIndex = 2;
        }

        public ObservableCollection<string> UUnitTypes
        {
            get => _uUnitTypes;
            set
            {
                _uUnitTypes = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }


        private void NUnitComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IsUnitText = NUnitComboBox.SelectedItem as string;
        }
    }
}
