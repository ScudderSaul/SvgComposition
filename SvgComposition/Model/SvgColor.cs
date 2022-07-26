using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Editor.Commanding.Commands;

namespace SvgComposition.Model
{
    public enum ColorRepresentation
    {
        Hex,
    }

    [Table("SvgColor")]
    public partial class SvgColor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public SvgColor()
        {
            System.Windows.Media.Color cn = Colors.Black;
            ResetColor(cn);
        }

        public SvgColor(System.Windows.Media.Color cn)
        {
            ResetColor(cn);

        }

        public void ResetColor(System.Windows.Media.Color cn)
        {
            ArgbValue = cn.ToString();
            Red = cn.R;
            Green = cn.G;
            Blue = cn.B;
            Alpha = cn.A;

            //    System.Windows.Media.Color rgb = System.Windows.Media.Color.FromRgb(Red, Green, Blue);

            ColorValue = $"#{Red:x2}{Green:x2}{Blue:x2}";
            //  ColorValue = $"#{Red:X2}{Green:X2}{Blue:X2}";

            //  ColorValue = rgb.ToString();
            ColorForm = ColorRepresentation.Hex;
            double op = ((double)Alpha) / 255.0;
            Opacity = op.ToString();
        }

        public SvgColor(byte a, byte r, byte g, byte b)
        {
            var cn = System.Windows.Media.Color.FromArgb(a, r, g, b);
            ResetColor(cn);
        }

        public SvgColor(byte r, byte g, byte b)
        {
            var cn = System.Windows.Media.Color.FromRgb(r, g, b);
            ResetColor(cn);
        }

        public System.Windows.Media.Color Color()
        {
            return (System.Windows.Media.Color.FromArgb(Alpha, Red, Green, Blue));
        }


       
        public ColorRepresentation ColorForm { get; set; }

        public string ColorValue
        {
            get;
            set;
        }
        public string ArgbValue { get; set; }
        public string Opacity { get; set; }

        public byte Red
        {
            get;
            set;
        }
        public byte Green { get; set; }
        public byte Blue { get; set; }
        public byte Alpha { get; set; }
        public string Stop { get; set; }

        public int? SvgAttributeId { get; set; }

        public virtual SvgAttribute SvgAttribute { get; set; }

        public int ColorIndex { get; set; } = 0;

    }
}