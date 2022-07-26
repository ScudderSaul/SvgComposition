using System;
using System.IO;
using System.Text;
using System.Windows;
using Microsoft.VisualStudio.PlatformUI;

namespace SvgComposition.Dialogs
{
    /// <summary>
    ///     Interaction logic for BrowserWindow.xaml
    /// </summary>
    public partial class BrowserWindow : DialogWindow
    {
        public BrowserWindow()
        {
            InitializeComponent();
        }

        public Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public void SetBrowserText(string html, string title)
        {
            Title = title;
            try
            {
              //  string userdir = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
              //  string path = userdir + @"/scriptfiletest.html";

              //  File.WriteAllText(path, html);
              //  StreamReader sr = new StreamReader(path);
              //  Stream ss = sr.BaseStream;

                string foo = string.Empty;
               using (Stream stream = html.ToStream(Encoding.UTF8))
               {
                   stream.Position = 0;
              //   StreamReader sw = new StreamReader(stream);
              //  foo = sw.ReadToEnd();
              //  MessageBox.Show(foo);
              //  Stream ss = sw.BaseStream;
               // ss.Position = 0;
                    Browser.NavigateToStream(stream);
                }
                


            }
            catch (Exception)
            {
                MessageBox.Show("Failure opening WebBrowser Control for\r\n\r\n" + html);
            }
           
        }
    }

    public static class UtilityClass
{
        public static Stream ToStream(this string str, Encoding enc = null)
        {
            enc = enc ?? Encoding.UTF8;
            return new MemoryStream(enc.GetBytes(str ?? ""));
        }
    }
}