using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Composition;
using System.Data.Entity.Migrations.Design;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.VisualStudio.Utilities;
using Microsoft.Win32;
using SvgComposition.Dialogs;
using SvgComposition.ElementControls;
using SvgComposition.ElementControls.ContainerElements;
using SvgComposition.Model;
using SvgCompositionTool.Dialogs;

namespace OddAndEnds
{
    public class OddsAndEnds
    {
		static Random _rnd = new Random();

        private static List<string> exnames = new List<String>
        {
            "Aepyornithiformes", "Dinornithiformes", "Apterygiformes", "Anseriformes", "Galliformes",
            "Charadriiformes", "Gruiformes", "Podicipediformes", "Cathartiformes", "Pelecaniformes",
            "Suliformes", "Procellariiformes", "Sphenisciformes", "Columbiformes", "Psittaciformes", "Cuculiformes",
            "Falconiformes", "Strigiformes", "Caprimulgiformes", "Apodiformes", "Coraciiformes",
            "Piciformes", "Passeriformes", "Struthioniformes", "Apterygiformes", "Casuariiformes", "Tinamiformes",
            "Anseriformes", "Galliformes", "Charadriiformes", "Gruiformes", "Ciconiiformes",
            "Pteroclidiformes", "Columbiformes", "Psittaciformes", "Cuculiformes", "Falconiformes", "Strigiformes",
            "Caprimulgiformes", "Apodiformes", "Coraciiformes", "Piciformes", "Passeriformes",
        };

        public static  string RandomName()
        {

            string vv = $"{exnames[_rnd.Next(exnames.Count)]}{_rnd.Next(1000)}";
        }
    }
}