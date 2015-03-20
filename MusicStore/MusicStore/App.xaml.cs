using System.Windows;
using MusicStore.Domain;

namespace MusicStore
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Bootstrapper.Start();
        }
    }
}