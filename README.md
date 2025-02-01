<a id="readme-top"></a>
# ProjectMaui: StrideSync

## Video Preview:
[![StrideSync](https://img.youtube.com/vi/QclJymPdt_Y/0.jpg)](https://youtube.com/shorts/QclJymPdt_Y)

## About The Project: StrideSync

StrideSync is a fitness trainer app for managing individual client routines. 
It is a .NET MAUI application using MVVM Design Pattern, SqLite Db, and external web service API. 
The app was tested on Android devices.

<b>Project Goal:</b> API integration using a Mobile platform, explore MVVM architecture

### Built With
[![Csharp][csharp-badge]][csharp-url]
[![.NET MAUI][maui-badge]][maui-url]

### API:

[![ApiNinjas](https://img.shields.io/badge/ApiNinjas-2A9D8F?style=for-the-badge&logo=api&logoColor=white)](https://api-ninjas.com/api/exercises)


## Getting Started

### Prerequisites
* Visual Studio 2022 or later with the .NET MAUI workload installed.
* You can install the required workloads via the Visual Studio installer under Mobile development with .NET or .NET desktop development.
* Android Emulator or a physical device.

### Installation
1. Clone the repo
   ```
   > git clone https://github.com/MMdevworks/ProjectMaui2.git
   > cd ProjectMaui2
   ```
3. Open the project
   ```
   Open the solution file (.sln) in Visual Studio 2022 or later.
   ```
4. Restore NuGet package dependancies
   ```
   In Visual Studio go to: 
		Tools > NuGet Package Manager > Manage NuGet Packages for Solution > Restore dependencies

   Packages Used:
    CommunityToolkit.Mvvm - version 8.2.2
    CommunityToolkit.Maui
    SQLite-net-pcl
    SQLitePCLRaw.bundle_green
    BCrypt
   ```
5. Select Your Target Device or Emulator
   
   .NET MAUI supports building for multiple platforms, including Android, iOS, macOS, and Windows. Choose the target platform from the toolbar at the top of Visual Studio:
   ```
   Choose an Android Emulator
   OR
   Connect your physical Android device
       > Enable Developer Options and USB Debugging on your Android device.
       > Connect your Android device via USB to your computer.
       > Select your physical device from the dropdown in the toolbar.
         If properly connected, your device will appear as an option.
   ```
6. Build and run the project
   ```
   With an Android emulator or physical device selected, click Run or press F5 to build and deploy the app to the selected target.
   ```


<p align="right">(<a href="#readme-top">back to top</a>)</p>

[maui-badge]: https://img.shields.io/badge/.NET_MAUI-512BD4?style=for-the-badge&logo=dotnet&logoColor=white
[maui-url]: https://learn.microsoft.com/en-us/dotnet/maui
[csharp-badge]: https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white
[csharp-url]: https://learn.microsoft.com/en-us/dotnet/csharp/
