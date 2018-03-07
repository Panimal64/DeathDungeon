using System.IO;
using Xamarin.Forms;
using DeathDungeon.UWP;
using Windows.Storage;
using DeathDungeon.Services;

[assembly: Dependency(typeof(FileHelper))]
namespace DeathDungeon.UWP
{
    public class FileHelper : IFileHelper
{
    public string GetLocalFilePath(string filename)
    {
        return Path.Combine(ApplicationData.Current.LocalFolder.Path, filename);
    }
}
}


