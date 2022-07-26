using SvgComposition.Controls;

namespace SvgComposition
{
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for SvgToolWindowControl.
    /// </summary>
    public partial class SvgToolWindowControl : UserControl
    {
        private SvgCompositionControl _mainControl = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="SvgToolWindowControl"/> class.
        /// </summary>
        public SvgToolWindowControl()
        {
            this.InitializeComponent();
            MainPanel.Children.Add(MainControl);
        }

        public SvgCompositionControl MainControl
        {
            get
            {
                if (_mainControl == null)
                {
                    _mainControl = new SvgCompositionControl();
                }

                return (_mainControl);
            }
        }
    }
}