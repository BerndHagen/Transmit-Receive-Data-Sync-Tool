# Transmit-Receive

Transmit-Receive is a networked file transfer application suite developed in C# that employs the Transmission Control Protocol (TCP) for the reliable, ordered, and error-checked exchange of data between a Transmitter and a Receiver. This suite is designed to enable users to effortlessly send and receive files across a network, ensuring data integrity and security through TCP's robust communication standards. The Transmitter component allows users to select files for transfer and initiates the connection to the Receiver, which listens on a predefined port for incoming file transfers. Upon establishing a connection, the Transmitter sends the file, broken down into packets, to the Receiver. The Receiver then reconstructs these packets back into the original file, saving it to a designated directory.

The code meticulously handles the network stream, ensuring that all transmitted data is accurately received and reconstructed into the original file. This includes careful management of byte arrays for file names and content, ensuring compatibility and efficiency in data handling. Moreover, both applications implement user interface elements that allow for dynamic interaction, such as selecting files to send or directories to save received files, and displaying real-time status updates about the transfer process.

Designed in Visual Studio 2022, the Transmitter and Receiver applications are versatile software solutions that support both `Windows` and `Linux` systems. They rely on the `.NET Framework 4.7.2` or higher to provide a smooth and efficient user experience across different platforms.

# Getting Started

### Application: Receiver

1. **Start the Application:** Launch the Receiver application. Upon starting, it automatically begins listening on `Port 1234` for incoming files .

2. **Select a Save Location:**
   - Click on the `Browse` button to open a folder selection dialog.
   - Navigate to and select the desired folder where you want to save incoming files.
   - Confirm the folder selection and the path will now be displayed in the text box, indicating where received files will be saved.

### Receiving Files
- The application continuously listens for incoming connections and files in the background.
- When a file is received, its progress and completion status will be displayed in the application's UI, typically in a list or log view.

### Application: Transmitter

1. **Start the Application:** Now launch the Transmitter application after setting up the Receiver.

2. **Select a File to Send:**
   - Click on the `Browse` button to open a file selection dialog.
   - Navigate to and select the file you wish to send.
   - Confirm the file selection and the path of the file will now be displayed in the text box.

### Sending Files
   - After selecting a file, the application automatically initiate the file transfer.
   - The application will connect to the receiver's listening `Port 1234` and begin transferring the file.
   - Monitor the progress in the application's UI, where it will display status messages about the sending process.

> **Note:** When the Receiver's Message Box displays File has been received, the file will be available at the save location.

# Copyright

This Transmit and Receive application is licensed under the [MIT License](LICENSE).

**MIT License**
Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files, to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense and/or sell copies of the Software, subject to the following conditions:

1. The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

2. Users of the Software should provide proper attribution to the original creator in their projects, documentation or any other materials that make use of this Software. Additionally, users should include a link to the original Repository created by me when providing attribution.

3. The Software is provided "as is," without warranty of any kind, express or implied, including but not limited to the warranties of merchantability, fitness for a particular purpose, and non-infringement. In no event shall the authors or copyright holders be liable for any claim, damages, or other liability, whether in an action of contract, tort, or otherwise, arising from, out of, or in connection with the Software or the use or other dealings in the Software.

For more details, please refer to the [MIT License](LICENSE).

# Screenshots

Before downloading either the Setup or Project folder for the Transmit-Receive Software, you can choose to preview its appearance via the screenshots presented below. These images showcase the visual aspects of both the applications. Please be aware that forthcoming updates may bring in additional features to further enrich the user experience.

<details>
  <summary>Receiver Application</summary>
  <img src="https://github.com/BerndHagen/Transmit-Receive/raw/main/img/v1.0.0-receiver.png" width="388px">
</details>

<details>
  <summary>Transmitter Application</summary>
  <img src="https://github.com/BerndHagen/Transmit-Receive/raw/main/img/v1.0.0-transmitter.png" width="388px">
</details>
