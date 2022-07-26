using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SvgComposition.Model
{
    public enum SvgAttributeType
    {
        Value,
        None,
        Url,
        Inherit,
        Special,
        Default,
        Text,
        ValueAndOptional,
        LengthPercent,
        Color,
    }

    public enum SvgAttributeUnit
    {
        Em, // The default font size - usually the height of a character.
        Ex, //The height of the character x
        Px, //Pixels
        Pt, //Points (1 / 72 of an inch)
        Pc, //Picas (1 / 6 of an inch)
        Cm, //Centimeters
        Mm, //Millimeters
        In, //Inches
        Percent, // %
    }

    [Table("SvgAttribute")]
    public partial class SvgAttribute
    {

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        public string AttributeName { get; set; } // the name like clipRule or cx

        
        public string AttributeValue { get; set; }

        public string SecondaryValue { get; set; }

        public SvgAttributeType AttributeType
        {
            get;
            set;
            
        } = SvgAttributeType.Value;

        public string SvgUnit { get; set; } = "px";

        public virtual ICollection<SvgColor> SvgColors
        {
            get;
            set;
        } = new HashSet<SvgColor>();


        public static Dictionary<string, SvgAttributeType> StringToTypeDictionary =
            new Dictionary<string, SvgAttributeType>
            {
                {"Value", SvgAttributeType.Value },
                {"None", SvgAttributeType.None },
                {"Url", SvgAttributeType.Url },
                {"Inherit", SvgAttributeType.Inherit },
                {"Special", SvgAttributeType.Special },
                {"Default", SvgAttributeType.Default },
                {"Text", SvgAttributeType.Text },
                {"ValueAndOptional", SvgAttributeType.ValueAndOptional },
                {"LengthPercent", SvgAttributeType.LengthPercent },
                {"Color", SvgAttributeType.Color },
            };


        public static List<string> SvgAttributeTypeText = new List<string>
        {
            "Value",
            "None",
            "Url",
            "Inherit",
            "Special",
            "Default",
            "Text",
            "ValueAndOptional",
            "LengthPercent",
            "Color"
        };

        public static SvgAttributeType TextToSvgAttributeType(string ss)
        {
            SvgAttributeType rv;
            string si = ss.ToLower();
            try
            {
                rv = StringToTypeDictionary[si];
            }
            catch (Exception ex)
            {
                rv = SvgAttributeType.None;
            }

            return (rv);
        }

        public static string SvgAttributeTypeToText(SvgAttributeType tt)
        {
            string st = string.Empty;

            try
            {
                st = SvgAttributeTypeText[(int)tt];
            }
            catch (Exception ex)
            {
                st = "None";
            }

            return (st);
        }


        //public static Dictionary<string, SvgAttributeUnit> StringToUnitDictionary = new Dictionary<string, SvgAttributeUnit>
        //{
           
        //    { "em", SvgAttributeUnit.Em}, // The default font size - usually the height of a character.
        //    { "ex", SvgAttributeUnit.Ex}, // The height of the character x
        //    { "px", SvgAttributeUnit.Px}, // Pixels
        //    { "pt", SvgAttributeUnit.Pt}, // Points (1 / 72 of an inch)
        //    { "pc", SvgAttributeUnit.Pc}, // Picas (1 / 6 of an inch)
        //    { "cm", SvgAttributeUnit.Cm}, // Centimeters
        //    { "mm", SvgAttributeUnit.Mm}, // Millimeters
        //    { "in", SvgAttributeUnit.In}, // Inches
        //    { "%", SvgAttributeUnit.Percent},
        //};

        public static List<string> SvgAttributeUnitsText = new List<string>
        {
            "em", // The default font size - usually the height of a character.
            "ex", // The height of the character x
            "px", // Pixels
            "pt", // Points (1 / 72 of an inch)
            "pc", // Picas (1 / 6 of an inch)
            "cm", // Centimeters
            "mm", // Millimeters
            "in", // Inches
            "%"
        };

        //public static SvgAttributeUnit  TextToSvgAttributeUnit(string ss)
        //{
        //    SvgAttributeUnit rv;
        //    string si = ss.ToLower();
        //    try
        //    {
        //        rv = StringToUnitDictionary[si];
        //    }
        //    catch (Exception ex)
        //    {
        //        rv = SvgAttributeUnit.Px;
        //    }

        //    return (rv);
        //}

        //public static string SvgAttributeUnitToText(SvgAttributeUnit unit)
        //{
        //    string st = string.Empty;
           
        //    try
        //    {
        //        st = SvgAttributeUnitsText[(int)unit];
        //    }
        //    catch (Exception ex)
        //    {
        //        st = "px";
        //    }

        //    return (st);
        //}

        public int? SvgElementId { get; set; } = null;

        public virtual SvgElement SvgElement { get; set; }
    }
}