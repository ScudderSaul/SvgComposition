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
    public partial class ChooseAttribute : DialogWindow
    {
        private ObservableCollection<string> _svgAttributeNameObservableCollection = new ObservableCollection<string>();

        public ChooseAttribute()
        {
            InitializeComponent();
            DataContext = this;
        }

        public string ChosenAttribute { get; set; }

        public ObservableCollection<string> SvgAttributeNameObservableCollection
        {
            get => _svgAttributeNameObservableCollection;
            set
            {
                if (Equals(value, _svgAttributeNameObservableCollection)) return;
                _svgAttributeNameObservableCollection = value;
                foreach (var se in _svgAttributeNameObservableCollection)
                {
                    if (ChosenAttribute != null)
                    {
                        var bb = new Button
                        {
                            MaxWidth = 120,
                            MinWidth = 60,
                        //    Width = 100.0,
                            Height = 23.0,
                            VerticalAlignment = VerticalAlignment.Top,
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
                      //  bb.Width = 100.0;
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
                this.InvalidateMeasure();
                this.InvalidateArrange();
                this.InvalidateVisual();
            }
        }

        private void ChooseSvgButton_OnClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                ChosenAttribute = button.Tag as string;
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
            if (ChosenAttribute != null)
            {
                this.DialogResult = true;
            }
            else
            {
                this.DialogResult = false;
            }

            Close();
        }
    }


}