using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.VisualStudio.PlatformUI;
using SvgComposition.Model;

namespace SvgCompositionTool.Dialogs
{
    /// <summary>
    ///     Interaction logic for ChooseChildElement.xaml
    /// </summary>
    public partial class ChooseChildElement : DialogWindow
    {
        private ObservableCollection<string> _svgChildElementObservableCollection = new ObservableCollection<string>();

        public ChooseChildElement()
        {
            InitializeComponent();
        }

        public string ChosenChild { get; set; }

        public ObservableCollection<string> SvgChildElementObservableCollection
        {
            get => _svgChildElementObservableCollection;
            set
            {
                if (Equals(value, _svgChildElementObservableCollection)) return;
                _svgChildElementObservableCollection = value;
                foreach (var se in _svgChildElementObservableCollection)
                {
                    if (ChosenChild != null)
                    {
                        var bb = new Button
                        {
                            MaxWidth = 120,
                            MinWidth = 60,
                            //    Width = 100.0,
                            Height = 23.0,
                            Margin = new Thickness(2.0),
                            Content = se,
                            BorderBrush = new SolidColorBrush(Colors.Green),
                            BorderThickness = new Thickness(2.0),
                            Tag = se
                        };
                        bb.Click += ChooseSvgButton_OnClick;
                        ChooseWrapPanel.Children.Add(bb);
                    }
                    else
                    {
                        var bb = new Button();
                        bb.MaxWidth = 120;
                        bb.MinWidth = 60;
                        bb.VerticalAlignment = VerticalAlignment.Top;
                        bb.Height = 23.0;
                        bb.Margin = new Thickness(2.0);
                        bb.Content = se;
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
                ChosenChild = button.Tag as string;
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