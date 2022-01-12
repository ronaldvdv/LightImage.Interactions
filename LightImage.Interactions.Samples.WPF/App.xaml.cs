using Autofac;
using MediatR.Extensions.Autofac.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LightImage.Interactions.Samples
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var container = BuildContainer();
            var app = container.Resolve<AppViewModel>();
            var wnd = new MainWindow(app);
            wnd.Show();
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