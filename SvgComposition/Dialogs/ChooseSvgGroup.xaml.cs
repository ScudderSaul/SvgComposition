using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.VisualStudio.PlatformUI;
using SvgComposition.Model;

namespace SvgCompositionTool.Dialogs
{
    /// <summary>
    ///     Interaction logic for ChooseStyle.xaml
    /// </summary>
    public partial class ChooseSvgGroup : DialogWindow
    {
        private ObservableCollection<SvgElement> _svgGroupObservableCollection;

        public ChooseSvgGroup()
        {
            InitializeComponent();
        }

        public SvgElement ChosenSvg { get; set; }

        public ObservableCollection<SvgElement> SvgElementObservableCollection
        {
            get => _svgGroupObservableCollection;
            set
            {
                if (Equals(value, _svgGroupObservableCollection)) return;
                _svgGroupObservableCollection = value;
                foreach (var se in _svgGroupObservableCollection)
                {
                    if (se.Id == ChosenSvg.Id)
                    {
                        var bb = new Button
                        {
                            Width = 100.0,
                            Height = 30.0,
                            Margin = new Thickness(2.0),
                            Content = se.SvgElementName,
                            BorderBrush = new SolidColorBrush(Colors.Green),
                            BorderThickness = new Thickness(3.0),
                            Tag = se
                        };
                        bb.Click += ChooseSvgButton_OnClick;
                        ChooseWrapPanel.Children.Add(bb);
                    }
                    else
                    {
                        var bb = new Button();
                        bb.Width = 100.0;
                        bb.Height = 30.0;
                        bb.Margin = new Thickness(2.0);
                        bb.Content = se.SvgElementName;
                        bb.BorderBrush = new SolidColorBrush(Colors.Blue);
                        bb.BorderThickness = new Thickness(1.0);
                        bb.Tag = se;
                        bb.Click += ChooseSvgButton_OnClick;
                        ChooseWrapPanel.Children.Add(bb);
                    }
                }
            }
        }

        private void ChooseSvgButton_OnClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                ChosenSvg = button.Tag as SvgElement;
                foreach (var ob in ChooseWrapPanel.Children)
                {
                    var bt = ob as Button;
                    bt.BorderBrush = new SolidColorBrush(Colors.Blue);
                    bt.BorderThickness = new Thickness(1.0);
                }
                button.BorderBrush = new SolidColorBrush(Colors.Green);
                button.BorderThickness = new Thickness(3.0);
            }
        }

        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }


}