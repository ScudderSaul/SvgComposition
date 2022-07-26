using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SvgComposition.Controls
{
    /// <summary>
    /// Interaction logic for HelpControl.xaml
    /// </summary>
    public partial class HelpControl : UserControl
    {
        public HelpControl()
        {
            InitializeComponent();
        }

        private ExampleGenerator Exg { get; set; } = new ExampleGenerator();

        private void AboutDetailsButton_OnClick(object sender, RoutedEventArgs e)
        {
            AboutDetailsScrollViewer.Visibility = Visibility.Visible;
            BasicHelpScrollViewer.Visibility = Visibility.Collapsed;
            ExamplesScrollViewer.Visibility = Visibility.Collapsed;
            InvalidateVisual();
        }

        private void HelpDetailsButton_OnClick(object sender, RoutedEventArgs e)
        {
            BasicHelpScrollViewer.Visibility = Visibility.Visible;
            AboutDetailsScrollViewer.Visibility = Visibility.Collapsed;
            ExamplesScrollViewer.Visibility = Visibility.Collapsed;
            InvalidateVisual();
        }

        private void ExamplesButton_OnClick(object sender, RoutedEventArgs e)
        {
            AboutDetailsScrollViewer.Visibility = Visibility.Collapsed;
            BasicHelpScrollViewer.Visibility = Visibility.Collapsed;
            ExamplesScrollViewer.Visibility = Visibility.Visible;

            InvalidateVisual();
        }

        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            (this.Parent as Popup).IsOpen = false;
        }

        private void ConformButton_OnClick(object sender, RoutedEventArgs e)
        {
            
                Exg.CreateElement7354();
                MessageBox.Show("Conform Filter Example added, open it by selecting it as the top SVG", "Example Confirm");
        }

        private void LightSourceButton_OnClick(object sender, RoutedEventArgs e)
        {
            
            Exg.CreateElement8619();
            MessageBox.Show("Lights Source Example added, open it by selecting it as the top SVG", "Example Confirm");
        }

        private void TwoSquaresButton_OnClick(object sender, RoutedEventArgs e)
        {
            Exg.CreateElement8641();
            MessageBox.Show("Squares Example added, open it by selecting it as the top SVG", "Example Confirm");
        }

        private void MarkersButton_OnClick(object sender, RoutedEventArgs e)
        {
            Exg.CreateElement8852();
            MessageBox.Show("Markers Example added, open it by selecting it as the top SVG", "Example Confirm");
        }

        private void PatternButton_OnClick(object sender, RoutedEventArgs e)
        {
            Exg.CreateElement8546();
            MessageBox.Show("Pattern Example added, open it by selecting it as the top SVG", "Example Confirm");
        }

        private void AnimationButton_OnClick(object sender, RoutedEventArgs e)
        {
            Exg.CreateElement6019();
            MessageBox.Show("Animation Example added, open it by selecting it as the top SVG", "Example Confirm");
        }
    }
}
