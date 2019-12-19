using Prism.Regions;

namespace Security.Server.Views
{
    public partial class MainWindow
    {
        public MainWindow(IRegionManager regionManager)
        {
            InitializeComponent();

            regionManager.RegisterViewWithRegion("DoorsRegion", typeof(Doors));
            regionManager.RegisterViewWithRegion("ReaderRegion", typeof(MessageViewer));
            regionManager.RegisterViewWithRegion("CreateRegion", typeof(DoorRegistration));
        }
    }
}
