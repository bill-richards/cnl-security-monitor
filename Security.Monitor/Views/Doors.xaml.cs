using System.Windows;
using System.Windows.Controls;
using Security.Monitor.ViewModels;

namespace Security.Monitor.Views
{
    public partial class Doors : UserControl
    {
        public Doors() => InitializeComponent();

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
            => ((DoorsViewModel)DataContext).InitializeModels();
    }
}
