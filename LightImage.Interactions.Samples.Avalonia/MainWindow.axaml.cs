using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;

namespace LightImage.Interactions.Samples.Avalonia
{
    public partial class MainWindow : ReactiveWindow<AppViewModel>
    {
        public MainWindow() : this(null) { }

        public MainWindow(AppViewModel? app)
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            ViewModel = app;

            if (app != null)
            {
                this.WhenActivated(d =>
                {
                    this.BindCommand(ViewModel, vm => vm.Info, v => v.InfoButton);
                    this.BindCommand(ViewModel, vm => vm.YesNo, v => v.YesNoButton);
                    this.BindCommand(ViewModel, vm => vm.File, v => v.FileButton);
                    this.BindCommand(ViewModel, vm => vm.Folder, v => v.FolderButton);
                    this.BindCommand(ViewModel, vm => vm.Enum, v => v.EnumButton);
                    this.BindCommand(ViewModel, vm => vm.Number, v => v.NumberButton);
                    this.BindCommand(ViewModel, vm => vm.NumberRange, v => v.NumberRangeButton);
                    this.BindCommand(ViewModel, vm => vm.String, v => v.StringButton);
                });                
            }
        }

        /*private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }*/
    }
}
