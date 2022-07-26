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
    public partial class ChooseSvgElement : DialogWindow
    {
        private ObservableCollection<SvgElement> _svgElementObservableCollection;

        public ChooseSvgElement()
        {
            InitializeComponent();
        }

        public SvgElement ChosenSvg { get; set; }

        public ObservableCollection<SvgElement> SvgElementObservableCollection
        {
            get => _svgElementObservableCollection;
            set
            {
                if (Equals(value, _svgElementObservableCollection)) return;
                _svgElementObservableCollection = value;
                foreach (var se in _svgElementObservableCollection)
                {
                    if (ChosenSvg != null && se.Id == ChosenSvg.Id)
                    {
                        var bb = new Button
                        {
                            Width = 140.0,
                            Height = 30.0,
                            Margin = new Thickness(2.0),
                            Content = $"{se.ElementType}({se.SvgElementName})-{se.Id}",
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
                        bb.Width = 140.0;
                        bb.Height = 30.0;
                        bb.Margin = new Thickness(2.0);
                        bb.Content = $"{se.ElementType}({se.SvgElementName}-{se.Id})";
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
            this.DialogResult = false;
            Close();
        }

        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            Close();
        }
    }


}