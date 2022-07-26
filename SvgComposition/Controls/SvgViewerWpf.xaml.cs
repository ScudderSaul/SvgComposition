using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
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
using Svg;
using System.IO;
using System.Windows.Controls.Primitives;
using Image = System.Drawing.Image;


namespace SvgComposition.Controls
{
    /// <summary>
    /// Interaction logic for SvgViewerWpf.xaml
    /// </summary>
    public partial class SvgViewerWpf : UserControl
    {
        public SvgViewerWpf()
        {
            InitializeComponent();
            DataContext = this;
        }

        public void InsertImage(BitmapSource bmo)
        {
            ImageOne.Stretch = Stretch.Fill;
            ImageOne.Source = bmo;
            ImageOne.Visibility = Visibility.Visible;
            
            ImageOne.InvalidateVisual();
        } 

        public void InSvgDoc(string path)
        {


            SvgDocument svgDoc = SvgDocument.Open(path);

            Bitmap vv = svgDoc.Draw();
            //     vv.Save("Q:\\code\\Code_2019\\tst.bmp");

            BitmapSource bms = BitmapConversion.ToWpfBitmap(vv);

            string userdir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string bmppath = userdir + @"\svgImg.bmp";
            using (FileStream stream = new FileStream(bmppath, FileMode.Create))
            {
                BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bms));
                encoder.Save(stream);
            }

            BitmapImage bmo = new BitmapImage();
            bmo.BeginInit();
            bmo.UriSource = new Uri("bmppath", UriKind.Relative);
            bmo.EndInit();

            ImageOne.Stretch = Stretch.Fill;
            ImageOne.Source = bmo;
            ImageOne.Visibility = Visibility.Visible;
            ImageOne.Width = vv.Width;
            ImageOne.Height = vv.Height;
            ImageOne.InvalidateVisual();
           
            InvalidateArrange();
            InvalidateVisual();
        }

       private void CloseButton_OnClick(object sender, RoutedEventArgs e)
       {
           (this.Parent as Popup).IsOpen = false;
       }

       private void ImageOne_OnImageFailed(object sender, ExceptionRoutedEventArgs e)
       {
           string gg = e.ErrorException.Message;
       }
    }
}
