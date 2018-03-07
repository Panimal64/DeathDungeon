using System;
using System.IO;
using Xamarin.Forms;
using DeathDungeon.Droid;
using DeathDungeon.Services;

[assembly: Dependency(typeof(FileHelper))]
namespace DeathDungeon.Droid
{
public class FileHelper : IFileHelper
{
    public string GetLocalFilePath(string filename)
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        return Path.Combine(path, filename);
    }
}
}