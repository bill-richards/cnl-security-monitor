using System.Windows.Controls;
using Security.Server.ViewModels;

namespace Security.Server.Views
{
    public partial class Doors : UserControl
    {
        public Doors()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e) 
            => ((DoorsViewModel)DataContext).InitializeModels();
    }
}
