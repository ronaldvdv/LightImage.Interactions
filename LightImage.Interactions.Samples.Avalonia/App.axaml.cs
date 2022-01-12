using Autofac;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using MediatR.Extensions.Autofac.DependencyInjection;

namespace LightImage.Interactions.Samples.Avalonia
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {                        
            var container = BuildContainer();
            var app = container.Resolve<AppViewModel>();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow(app);
            }

            base.OnFrameworkInitializationCompleted();
        }

        private static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<AppViewModel>().SingleInstance();
            builder.RegisterType<InteractionService>().As<IInteractionService>().SingleInstance();
            builder.AddMediatR(
                typeof(FileInteractionHandler).Assembly,
                typeof(App).Assembly
            );
            return builder.Build();
        }
    }
}
