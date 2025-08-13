<p align="center">
  <img src="https://github.com/BerndHagen/Transmit-Receive/blob/main/img/v1.0.2-receiver-logo.png" alt="Transmit-Receive Logo" width="128" />
</p>
<h1 align="center">Transmit/Receive - Data Sync Tool</h1>
<p align="center">
  <b>Reliable and secure file transfer across networks using TCP protocol.</b><br>
  <b>Experience seamless data synchronization with real-time monitoring and error-checked transfers.</b>
</p>
<p align="center">
  <a href="https://github.com/BerndHagen/Transmit-Receive/releases"><img src="https://img.shields.io/github/v/release/BerndHagen/Transmit-Receive?include_prereleases&style=flat-square&color=CD853F" alt="Latest Release"></a>&nbsp;&nbsp;<a href="https://github.com/BerndHagen/Transmit-Receive/blob/main/LICENSE"><img src="https://img.shields.io/badge/License-MIT-blue?style=flat-square" alt="License"></a>&nbsp;&nbsp;<a href="https://dotnet.microsoft.com/en-us/download/dotnet-framework/net472"><img src="https://img.shields.io/badge/.NET-4.7.2-512BD4?style=flat-square" alt=".NET Version"></a>&nbsp;&nbsp;<img src="https://img.shields.io/badge/Platform-Windows-0078D6?style=flat-square" alt="Platform">&nbsp;&nbsp;<img src="https://img.shields.io/badge/Architecture-x64-lightgrey?style=flat-square" alt="Architecture">&nbsp;&nbsp;<img src="https://img.shields.io/badge/Status-Active-brightgreen?style=flat-square" alt="Status">&nbsp;&nbsp;<a href="https://github.com/BerndHagen/Transmit-Receive/issues"><img src="https://img.shields.io/badge/Issues-0_open-orange?style=flat-square" alt="Open Issues"></a>
</p>

**Transmit-Receive** is a C# application designed for reliable and secure file transfer across networks using the Transmission Control Protocol (TCP). This toolset includes two components: the **Transmitter** and the **Receiver**, which work together to exchange data between systems locally, across networks, or even over the internet. Both applications run on `Windows` and provide a user-friendly interface for reliable, orderly and error-checked file transfers with MD5 checksum verification. You can easily select files, specify target IP addresses, choose save locations and monitor transfer progress in real-time.

### How It Works

1. **Transmitter:**
   - Enter the IP address of the target Receiver.
   - Select the files you want to send using the file browser.
   - File transfer starts automatically with progress monitoring and checksum verification.

2. **Receiver:**
   - Displays its IP address for easy sharing with Transmitter users.
   - Listens on port 1234 for incoming file transfers from any network location.
   - Reconstructs received packets, verifies file integrity, and saves to the specified directory.

In order to use Transmit-Receive, ensure you have [.NET Framework 4.7.2](https://dotnet.microsoft.com/download/dotnet-framework/net472) or higher installed on your system. You can build and run the application using **Visual Studio 2022** or any compatible development environment.

## Table of Contents

1. [System Requirements](#system-requirements)
2. [Network Configuration](#network-configuration)
3. [Getting Started](#getting-started)
    - [Application: Receiver](#application-receiver)
    - [Application: Transmitter](#application-transmitter)
4. [Connection Types](#connection-types)
5. [Troubleshooting](#troubleshooting)
6. [Copyright](#copyright)
7. [Screenshots](#screenshots)

## System Requirements

- **Operating System:** Windows 7 or higher
- **Framework:** [.NET Framework 4.7.2](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net472) or higher
- **Architecture:** x64 compatible
- **Network:** TCP connectivity on port 1234
- **Permissions:** Administrator privileges recommended

## Network Configuration

- **Default Port:** 1234 (TCP)
- **Connection Type:** Local network, internet, or localhost
- **Protocol:** TCP (Transmission Control Protocol)
- **Buffer Size:** 8192 bytes
- **Security:** MD5 checksum verification for file integrity

**Network Setup:**
- Receiver automatically displays its IP address for easy connection setup
- Ensure Windows Firewall allows connections on port 1234
- For internet transfers, configure router port forwarding (port 1234) on the Receiver side
- Both applications support connections across different networks and the internet

## Getting Started

### Application: Receiver

1. **Launch the Receiver Application:**
   - Open the Receiver application. Once launched, it will automatically start listening on `Port 1234` for incoming file transfers.
   - The application will display its IP address (e.g., "Server IP: 192.168.1.100:1234") in the message log.

2. **Select a Save Location:**
   - Click the `Browse` button to open a folder selection dialog.
   - Navigate to and choose the folder where you want to save incoming files.
   - After selecting the folder, the path will be displayed in the text box, indicating the destination for received files.

3. **Share Your IP Address:**
   - Provide the displayed IP address to users who want to send files to your system.

### Application: Transmitter

1. **Launch the Transmitter Application:**
   - Start the Transmitter application. Your local IP address will be displayed for reference.

2. **Enter Target IP Address:**
   - In the top text field, enter the IP address of the Receiver you want to connect to.
   - This can be a local IP (e.g., 192.168.1.100), localhost (127.0.0.1), or an internet IP address.

3. **Select and Send a File:**
   - Click the `Browse` button to open a file selection dialog.
   - Navigate to and select the file you wish to send.
   - The file transfer will start automatically once a file is selected.
   - Monitor the transfer progress and wait for confirmation.

After the Receiver displays the message **File received successfully**, the file will be available in the selected save location with verified integrity.

## Connection Types

**Local Connection (Same Computer):**
- IP Address: `127.0.0.1` or `localhost`

**Local Network Connection:**
- IP Address: Local network IP (e.g., `192.168.1.100`)
- Both devices must be on the same network

**Internet Connection:**
- IP Address: Public IP address of the Receiver
- Requires port forwarding configuration on the Receiver's router
- Firewall rules must allow incoming connections on port 1234

> **Important:** Make sure to run both applications as an administrator. Otherwise, Windows might block the file transfers, causing the applications not to work properly.

## Troubleshooting

**Connection Issues:**
- Verify the IP address is entered correctly in the Transmitter
- Ensure the Receiver is running and displays "Listening for connections..."
- Check Windows Firewall settings for port 1234
- For internet connections, verify port forwarding is configured correctly

**File Transfer Errors:**
- Run both applications as administrator
- Ensure sufficient disk space at the destination
- Check file permissions for both source and destination folders
- Verify network stability for large file transfers

**Application Not Responding:**
- Restart the Receiver application first, then the Transmitter
- Verify .NET Framework 4.7.2 is properly installed
- Check Windows Event Viewer for detailed error messages

**Network Connectivity:**
- Test basic network connectivity between machines (ping command)
- Ensure TCP port 1234 is not blocked by antivirus software
- For internet transfers, verify the public IP address is accessible
- Check if VPN or proxy settings interfere with the connection

**IP Address Issues:**
- Use the IP address displayed by the Receiver application
- For internet connections, use the public IP address, not the local one
- Ensure the target device is reachable from your network location

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
  <img src="https://github.com/BerndHagen/Transmit-Receive/raw/main/img/v1.0.2-receiver.png" width="388px">
</details>

<details>
  <summary>Transmitter Application</summary>
</br>
  <img src="https://github.com/BerndHagen/Transmit-Receive/raw/main/img/v1.0.2-transmitter.png" width="388px">
</details>