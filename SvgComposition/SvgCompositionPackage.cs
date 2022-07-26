using System;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.VisualStudio.Shell;
using Microsoft.Win32;
using Task = System.Threading.Tasks.Task;

namespace SvgComposition
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the
    /// IVsPackage interface and uses the registration attributes defined in the framework to
    /// register itself and its components with the shell. These attributes tell the pkgdef creation
    /// utility what data to put into .pkgdef file.
    /// </para>
    /// <para>
    /// To get loaded into VS, the package must be referred by &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt; in .vsixmanifest file.
    /// </para>
    /// </remarks>
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [Guid(SvgCompositionPackage.PackageGuidString)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideToolWindow(typeof(SvgToolWindow))]
    public sealed class SvgCompositionPackage : AsyncPackage
    {
        /// <summary>
        /// SvgCompositionPackage GUID string.
        /// </summary>
        public const string PackageGuidString = "7d0bdabc-2417-4b95-9b8f-0b994257c114";

        public static string ConnectionOther = string.Empty;

        #region Package Members

        public void GetSqlConnection()
        {
            const string userRoot = "HKEY_CURRENT_USER";
            const string subkey = "SvgCompositionExt";
            const string keyName = userRoot + @"\Software\_Zen_Soft\" + subkey;

            // Your default value is returned if the name/value pair
            // does not exist.
            ConnectionOther = (string)Registry.GetValue(keyName,
                "SqlConnection",
                "Empty");


            if (ConnectionOther == "Empty")
            {
                ConnectionOther =
                    @"Server=(localdb)\mssqllocaldb;Database=SvgComposition.db;Trusted_Connection=True;";
            }

            if (string.IsNullOrEmpty(ConnectionOther))
            {
                ConnectionOther = @"Server=(localdb)\mssqllocaldb;Database=SvgComposition.db;Trusted_Connection=True;";
            }
        }

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token to monitor for initialization cancellation, which can occur when VS is shutting down.</param>
        /// <param name="progress">A provider for progress updates.</param>
        /// <returns>A task representing the async work of package initialization, or an already completed task if there is none. Do not return null from this method.</returns>
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            // When initialized asynchronously, the current thread may be a background thread at this point.
            // Do any initialization that requires the UI thread after switching to the UI thread.
            await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
            GetSqlConnection();
            await SvgToolWindowCommand.InitializeAsync(this);

        }

        #endregion
    }
}
