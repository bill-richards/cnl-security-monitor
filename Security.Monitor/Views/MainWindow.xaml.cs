using Prism.Regions;
using Security.Common.Views;

namespace Security.Monitor.Views
{
    public partial class MainWindow
    {
        public MainWindow(IRegionManager regionManager)
        {
            InitializeComponent();
            regionManager.RegisterViewWithRegion("DoorsRegion", typeof(Doors));
        }
    }
}
