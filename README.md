# MauiAppTester

MauiAppTester is a .NET MAUI application designed to test various functionalities within an Android emulator, including folder navigation, file listing, text input handling, and button simulations.

## Features

- **Simulate Button Click**: Simulates a button click within the app.
- **Text Input with Send Button**: Allows the user to input text and displays a confirmation pop-up showing the inputted text upon pressing the "Send" button.
- **Android Device Storage Navigation**:
  - Open device folders and navigate through the storage.
  - List files within selected folders.
  - Handle permission requests and denied permissions.
  - Return to the previous folder or screen as needed.

## Prerequisites

- **.NET MAUI**: Ensure you have the latest version of .NET MAUI installed.
- **Android SDK**: The Android SDK must be installed and configured, particularly the Android 11 (API level 30) for running the emulator.
- **Emulator**: An Android emulator running Android 11.

## Setup Instructions

### 1. Clone the Repository

```bash
git clone https://github.com/basiliohadnan/MauiAppTester.git
cd MauiAppTester
```

### 2. Open the Project

- Open the project in Visual Studio 2022 or later.

### 3. Run the Emulator

- Open Android Device Manager (AVD Manager) and start the Android 11 emulator.
- Alternatively, run the emulator via command line:

```bash
emulator -avd <your_emulator_name>
```

### 4. Deploy and Debug the App

- Build and deploy the app on the Android emulator using Visual Studio.
- Use Debug mode to catch any issues that may arise.

### 5. Interact with the App

- **Simulate Button Click**: Tap the "Click Me" button to simulate a button click event.
- **Text Input**: Enter text in the provided input field and press the "Send" button. A pop-up will display your input.
- **Folder Navigation**: Tap "Open Device Folders" to browse through the Android emulator's storage. 
  - Select a folder to view its contents.
  - Use the "OK" button to confirm selection and list files, or "Cancel" to return to the previous screen.
  - Use the "Return" button to navigate back to the parent folder.

### 6. Add Files to the Emulator (Optional)

To test file listing features, you can add files to the emulator's storage:

- **Drag and Drop**: Drag a file from your Windows Explorer directly into the running emulator window. The file will be placed in `/sdcard/Download/` by default.
- **ADB Push**: Use the following command to push a file into the emulator:
  ```bash
  adb push <local_path_to_file> /sdcard/Download/
  ```

## Troubleshooting

- **Permissions**: Ensure that the app has the necessary permissions to access storage. The app will prompt for permissions if they are not granted.
- **Directory Listing Issues**: If directories appear empty, verify that the files are present in the correct emulator storage path.

## Contributions

Contributions are welcome! Please feel free to submit a pull request or open an issue.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
