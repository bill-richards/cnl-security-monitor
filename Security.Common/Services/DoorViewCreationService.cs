using System;
using Prism.Regions;
using Security.Common.ViewModels;
using Security.Common.Views;

namespace Security.Common.Services
{
    public class DoorViewCreationService : IDoorViewCreationService
    {
        private readonly IRegionManager _regionManager;
        private readonly Func<DoorView> _createDoorView;

        public DoorViewCreationService(IRegionManager regionManager, Func<DoorView> doorViewFactory)
        {
            _regionManager = regionManager;
            _createDoorView = doorViewFactory;
        }

        public void CreateDoorView(IDoor door)
        {
            var region = _regionManager.Regions["DoorViewsRegion"];
            var view = _createDoorView();
            var viewModel = ((DoorViewModel)view.DataContext);
            viewModel.SetDoorModel(door);
            region.Add(view, $"Door_{door.Id}", true);
        }
    }
}