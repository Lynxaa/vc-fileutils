using EnvDTE80;
using VCFileUtils.Integration;
using VCFileUtils.Integration.Commands;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio;

namespace VCFileUtils
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    [ProvideAutoLoad(VSConstants.UICONTEXT.SolutionExistsAndFullyLoaded_string, PackageAutoLoadFlags.BackgroundLoad)]
    [ProvideMenuResource(1000, 1)]
    [Guid(GuidList.GuidPackageString)]
    public sealed class VCFileUtilsPackage : AsyncPackage
    {
        #region Fields

        private DTE2 _ide;
        private OleMenuCommandService _menuCommandService;

        #endregion

        #region Constructors

        public VCFileUtilsPackage()
        {
            // Inside this method you can place any initialization code that does not require
            // any Visual Studio service because at this point the package object is created but
            // not sited yet inside Visual Studio environment. The place to do all the other
            // initialization is the Initialize method.
        }

        #endregion

        #region Public Integration Properties

        public DTE2 IDE
        {
            get
            {
                return _ide ?? (_ide = GetService(typeof(SApplicationObject)) as DTE2);
            }
        }

        public OleMenuCommandService MenuCommandService
        {
            get
            {
                return _menuCommandService ?? (_menuCommandService = GetService(typeof(IMenuCommandService)) as OleMenuCommandService);
            }
        }

        #endregion

        #region Package Members

        /*protected override void Initialize()
        {
            base.Initialize();

            RegisterCommands();
        }*/

        protected override async System.Threading.Tasks.Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            RegisterCommands();
        }


        #endregion

        #region Private Methods

        private void RegisterCommands()
        {
            var menuCommandService = MenuCommandService;

            if (menuCommandService != null)
            {
                menuCommandService.AddCommand(new OrganizeInProjectCommand(this));
                menuCommandService.AddCommand(new SetProjectRootCommand(this));
                menuCommandService.AddCommand(new RemoveEmptyFiltersCommand(this));
                menuCommandService.AddCommand(new ReAddFilesCommand(this));
                menuCommandService.AddCommand(new OrganizeOnDiskCommand(this));
            }
        }

        #endregion
    }
}
