using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedveLaszlo_MAUI.Persistence
{
    internal class FileReader
    {
        public string EasyFileContent { get; private set; }
        public string MediumFileContent { get; private set; }
        public string HardFileContent { get; private set; }
        private async Task<string> LoadMauiAsset(string fileName)
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync(fileName);
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
        public async void Load()
        {
            EasyFileContent = await LoadMauiAsset("easyMap.txt");
            MediumFileContent = await LoadMauiAsset("mediumMap.txt");
            HardFileContent = await LoadMauiAsset("hardMap.txt");
        }

        public FileReader()
        {
            EasyFileContent = string.Empty;
            MediumFileContent = string.Empty;
            HardFileContent = string.Empty;
        }
    }
}
