using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.ApplicationModel;

namespace MauiAppTester
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnSimulateButtonClick(object sender, EventArgs e)
        {
            // Simulate button click logic here
            DisplayAlert("Button Click", "Simulate Button Clicked", "OK");
        }

        private async void OnOpenDeviceFolderClick(object sender, EventArgs e)
        {
            // Open device folder and read files
            var folderPath = "/storage/emulated/0/Download"; // Example folder path
            var files = await ReadFilesFromFolderAsync(folderPath);

            if (files.Count == 0)
            {
                files = GetMockFiles();
            }

            DisplayAlert("Files in Folder", string.Join(", ", files), "OK");
        }

        private Task<List<string>> ReadFilesFromFolderAsync(string folderPath)
        {
            return Task.Run(() =>
            {
                var fileList = new List<string>();
                if (Directory.Exists(folderPath))
                {
                    var files = Directory.GetFiles(folderPath);
                    fileList.AddRange(files);
                }
                return fileList;
            });
        }

        private List<string> GetMockFiles()
        {
            // Return a list of mock files for testing
            return new List<string>
            {
                "file1.txt",
                "file2.txt",
                "file3.txt"
            };
        }
    }
}
