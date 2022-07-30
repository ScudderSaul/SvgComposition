using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;

namespace SvgComposition
{
    /// <summary>
    /// This class implements the tool window exposed by this package and hosts a user control.
    /// </summary>
    /// <remarks>
    /// In Visual Studio tool windows are composed of a frame (implemented by the shell) and a pane,
    /// usually implemented by the package implementer.
    /// <para>
    /// This class derives from the ToolWindowPane class provided from the MPF in order to use its
    /// implementation of the IVsUIElementPane interface.
    /// </para>
    /// </remarks>
    ///[Guid("4d1647a8-fa1e-4f58-b9d5-e1fb94756604")]
    [Guid("914c45b2-9ae9-47d0-88ee-c3b08564a69b")]
    public class SvgToolWindow : ToolWindowPane
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SvgToolWindow"/> class.
        /// </summary>
        public SvgToolWindow() : base(null)
        {
            this.Caption = "SvgToolWindow";

            // This is the user control hosted by the tool window; Note that, even if this class implements IDisposable,
            // we are not calling Dispose on this object. This is because ToolWindowPane calls Dispose on
            // the object returned by the Content property.
            this.Content = new SvgToolWindowControl();
        }
    }
}
