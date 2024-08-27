# Transmit/Receive - Data Sync Tool

**Transmit-Receive** is a C# application designed for reliable and secure file transfer across a network using the Transmission Control Protocol (TCP). This toolset includes two components: the **Transmitter** and the **Receiver**, which work together to exchange data between systems. Both applications run on `Windows` and provide a user-friendly interface for reliable, orderly and error-checked file transfers. You can easily select files, choose save locations and monitor transfer progress in real-time.

### How It Works

1. **Transmitter:**
   - Selects the files you want to send.
   - Initiates a connection to the Receiver.
   - Breaks down the file into packets and sends them across the network.

2. **Receiver:**
   - Listens on a predefined port for incoming file transfers.
   - Reconstructs the received packets back into the original file.
   - Saves the file to a user-specified directory.

In order to use Transmit-Receive, ensure you have [.NET Framework 4.7.2](https://dotnet.microsoft.com/download/dotnet-framework/net472) or higher installed on your system. You can build and run the application using **Visual Studio 2022** or any compatible development environment.

## Table of Contents

1. [Getting Started](#getting-started)
    - [Application: Receiver](#application-receiver)
    - [Application: Transmitter](#application-transmitter)
2. [Copyright](#copyright)
3. [Screenshots](#screenshots)

## Getting Started

### Application: Receiver

1. **Launch the Receiver Application:**
   - Open the Receiver application. Once launched, it will automatically start listening on `Port 1234` for incoming file transfers.

2. **Select a Save Location:**
   - Click the `Browse` button to open a folder selection dialog.
   - Navigate to and choose the folder where you want to save incoming files.
   - After selecting the folder, the path will be displayed in the text box, indicating the destination for received files.

### Application: Transmitter

1. **Launch the Transmitter Application:**
   - Start the Transmitter application after the Receiver is set up and ready.

2. **Select a File to Send:**
   - Click the `Browse` button to open a file selection dialog.
   - Navigate to and select the file you wish to send.
   - The selected file's path will be shown in the text box.

After the Receiver displays the message **File has been received**, the file will be available in the selected save location.

# Copyright

Transmit-Receive is licensed under the **MIT License**. You are granted permission, free of charge, to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of this project and its associated documentation files, under the following conditions:

1. **Copyright Notice:** The above copyright notice and this permission notice must be included in all copies or substantial portions of the project.

2. **Attribution:** If you use this project, you should credit the original creator in your work, documentation, or any materials that incorporate or use this project. Additionally, please include a link to the original repository created by the author when giving attribution.

3. **No Warranty:** This project is provided "as is," without any warranties, whether express or implied, including but not limited to implied warranties of merchantability, fitness for a particular purpose, or non-infringement. In no case shall the author or copyright holder be liable for any claims, damages, or other liabilities, whether in a contract, tort, or otherwise, arising from the use of this project or any other dealings with the Software.

For complete license details, please refer to the [MIT License](LICENSE).

# Screenshots
If you're curious about what the **Transmitter** and **Receiver** applications look like before downloading, take a look at the screenshots below. These images provide a visual preview of the software's appearance. 

<details>
  <summary>Receiver Application</summary>
</br>
  <img src="https://github.com/BerndHagen/Transmit-Receive/raw/main/img/v1.0.0-receiver.png" width="388px">
</details>

<details>
  <summary>Transmitter Application</summary>
</br>
  <img src="https://github.com/BerndHagen/Transmit-Receive/raw/main/img/v1.0.0-transmitter.png" width="388px">
</details>
