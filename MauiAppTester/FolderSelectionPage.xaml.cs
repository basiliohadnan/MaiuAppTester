using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using System;
using System.IO;
using System.Linq;

namespace MauiAppTester
{
    public partial class FolderSelectionPage : ContentPage
    {
        private string selectedFolder;

        public FolderSelectionPage()
        {
            InitializeComponent();
            CheckAndRequestStoragePermission();
        }

        private async void CheckAndRequestStoragePermission()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.StorageRead>();
            }

            if (status == PermissionStatus.Granted)
            {
                LoadFolders("/storage/emulated/0/MockTest");
            }
            else
            {
                await DisplayAlert("Permission Denied", "Storage read permission is required to load folders.", "OK");
            }
        }

        private void LoadFolders(string path)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    var folders = Directory.GetDirectories(path).ToList();
                    if (folders.Any())
                    {
                        FolderListView.ItemsSource = folders;
                    }
                    else
                    {
                        DisplayAlert("No Folders", "No subfolders found in the specified path.", "OK");
                    }
                }
                else
                {
                    DisplayAlert("Path Not Found", $"The path {path} does not exist.", "OK");
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", $"An error occurred while loading folders: {ex.Message}", "OK");
            }
        }

        private void OnFolderSelected(object sender, SelectedItemChangedEventArgs e)
        {
            selectedFolder = e.SelectedItem as string;
        }

        private async void OnOkButtonClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(selectedFolder))
            {
                try
                {
                    var files = Directory.GetFiles(selectedFolder);
                    await Navigation.PopAsync();
                    if (files.Any())
                    {
                        await DisplayAlert("Files in Folder", string.Join(", ", files), "OK");
                    }
                    else
                    {
                        await DisplayAlert("No Files", "No files found in the selected folder.", "OK");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"An error occurred while loading files: {ex.Message}", "OK");
                }
            }
        }

        private async void OnCancelButtonClick(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
