namespace MauiAppTester
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            CreateMockDirectoriesAndFiles(); // Create mock directories and files for testing
        }

        private void CreateMockDirectoriesAndFiles()
        {
            string rootPath = "/storage/emulated/0/MockTest";
            string[] directories = {
        Path.Combine(rootPath, "Folder1"),
        Path.Combine(rootPath, "Folder2"),
        Path.Combine(rootPath, "Folder3")
    };

            string[] files = {
        Path.Combine(directories[0], "file1.txt"),
        Path.Combine(directories[0], "file2.txt"),
        Path.Combine(directories[1], "file3.txt"),
        Path.Combine(directories[2], "file4.txt")
    };

            foreach (var dir in directories)
            {
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
            }

            foreach (var file in files)
            {
                if (!File.Exists(file))
                {
                    File.WriteAllText(file, "This is a test file.");
                }
            }
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

        private void OnSendButtonClick(object sender, EventArgs e)
        {
            string inputText = TextInput.Text;
            DisplayAlert("You Typed:", inputText, "OK");
        }

        private async void OnOpenDeviceFoldersClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FolderSelectionPage());
        }
    }
}
