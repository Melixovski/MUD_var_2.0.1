using Autodesk.Revit.UI;
using mud;
using System.Collections.Generic;
using System.Windows;
using static MUD.Mud_start;

namespace MUD
{

    public partial class MUDView : Window
    {
        private ViewModel viewModel;
        public MUDView()
        {
            InitializeComponent();
            viewModel = new ViewModel();
            DataContext = viewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ViewModel viewModel = (sender as FrameworkElement)?.DataContext as ViewModel;
            string message = null;
            viewModel.Execute(CommonData.CommandData, ref message, null);


        }
    }
}
