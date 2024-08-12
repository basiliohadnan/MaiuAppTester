using Microsoft.Maui.Controls;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.ApplicationModel;

namespace MauiAppTester
{
    public partial class MainPage : ContentPage
    {
        private string selectedFolder;
        private string currentPath;

        public MainPage()
        {
            InitializeComponent();
            currentPath = "/storage/emulated/0/"; // Start from root directory
            CheckAndRequestPermissionsAsync(); // Check and request permissions
        }

        private async void CheckAndRequestPermissionsAsync()
        {
            var status = await Permissions.RequestAsync<Permissions.StorageRead>();

            if (status == PermissionStatus.Granted)
            {
                LoadFolders(currentPath);
            }
            else
            {
                await DisplayAlert("Permission Denied", "Unable to access storage. Please grant permission.", "OK");
            }
        }

        private void LoadFolders(string path)
        {
            try
            {
                currentPath = path;
                var folders = Directory.GetDirectories(path);
                FolderListView.ItemsSource = folders.Select(f => f.Split('/').Last());
            }
            catch (UnauthorizedAccessException)
            {
                DisplayAlert("Permission Denied", "Unable to access this folder.", "OK");
                LoadFolders(Path.GetDirectoryName(path)); // Go back to parent directory
            }
        }

        private void OnFolderSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var folderName = e.SelectedItem as string;
            if (folderName != null)
            {
                selectedFolder = Path.Combine(currentPath, folderName);
                LoadFolders(selectedFolder); // Load the selected folder's content
            }
        }

        private async void OnOkButtonClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(selectedFolder))
            {
                var files = Directory.GetFiles(selectedFolder);
                await DisplayAlert("Files in Folder", string.Join(", ", files.Select(f => f.Split('/').Last())), "OK");
            }
        }

        private void OnCancelButtonClick(object sender, EventArgs e)
        {
            // Clear the selection and reset the folder list if needed
            selectedFolder = null;
            LoadFolders(currentPath);
        }

        private void OnReturnButtonClick(object sender, EventArgs e)
        {
            // Navigate to the parent directory
            var parentDirectory = Path.GetDirectoryName(currentPath);
            if (!string.IsNullOrEmpty(parentDirectory))
            {
                LoadFolders(parentDirectory);
            }
        }

        // New Feature: Simulate Button Click
        private async void OnSimulateButtonClick(object sender, EventArgs e)
        {
            await DisplayAlert("Button Clicked", "You have clicked the button!", "OK");
        }

        // New Feature: Handle Text Input and Send Button Click
        private async void OnSendButtonClick(object sender, EventArgs e)
        {
            string userInput = TextInput.Text;
            if (!string.IsNullOrEmpty(userInput))
            {
                await DisplayAlert("You Typed", $"You typed: {userInput}", "OK");
            }
            else
            {
                await DisplayAlert("Empty Input", "Please type something before sending.", "OK");
            }
        }
    }
}
