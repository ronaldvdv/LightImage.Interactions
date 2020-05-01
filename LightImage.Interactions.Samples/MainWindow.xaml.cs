using ReactiveUI;

namespace LightImage.Interactions.Samples
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MainWindowBase
    {
        public MainWindow(AppViewModel app)
        {
            InitializeComponent();
            ViewModel = app;

            this.BindCommand(ViewModel, vm => vm.Info, v => v.InfoButton);
            this.BindCommand(ViewModel, vm => vm.YesNo, v => v.YesNoButton);
            this.BindCommand(ViewModel, vm => vm.File, v => v.FileButton);
            this.BindCommand(ViewModel, vm => vm.Enum, v => v.EnumButton);
            this.BindCommand(ViewModel, vm => vm.Number, v => v.NumberButton);
            this.BindCommand(ViewModel, vm => vm.NumberRange, v => v.NumberRangeButton);
            this.BindCommand(ViewModel, vm => vm.String, v => v.StringButton);
        }
    }

    public class MainWindowBase : ReactiveWindow<AppViewModel>
    {
    }
}