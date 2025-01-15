# WebNotifier (Deprecated)

**Note: This project has been marked as deprecated as it relies on Internet Explorer, which is no longer supported by Microsoft. Users are encouraged to explore modern alternatives.**

## Overview
WebNotifier is a Windows application designed to monitor changes on homepages such as news sites or blogs.

**Status:** Early alpha stage (proof of concept complete)

## Getting Started
1. Visit the [Releases](#) section of the project's GitHub repository to download the latest version.
2. Ensure your system has at least .NET Framework 4.5 installed. You can download it from [Microsoft's website](https://www.microsoft.com/en-US/download/details.aspx?id=30653).

## Motivation
The primary goal of WebNotifier is to inform users about changes on websites, reducing the need for users to manually check for updates. 

While similar solutions exist, such as the Firefox plug-in Distill or various online services, they often require active monitoring or consume significant bandwidth. WebNotifier aims to be a standalone, low-bandwidth solution that operates mostly in the background.

## Prerequisites
- This program is written in C#. You can compile it using Microsoft Visual Studio or another C# compiler if you wish to build it yourself.
- Dependencies:
  - **WatiN** ([License](https://www.codeproject.com/info/cpol10.aspx))  
  - **.NET Framework 4.5 (or newer)**

> Note: The official domain for WatiN has expired, but a backup is available on [CodeProject](https://www.codeproject.com/Articles/17064/WatiN-Web-Application-Testing-In-NET).

## Future Plans (Discontinued)
While the following plans were outlined for future development, the project has been deprecated:

- Full documentation.
- Fixing existing bugs.
- Improving software design and functionality.
- Replacing WatiN with Mozilla's Gecko engine or a socket-based implementation with HTTPS.
- Implementing additional filters for more customization.
- Potential Android export by separating the program's core engine for reuse.

---

### Final Note
Due to its reliance on Internet Explorer and outdated technologies, WebNotifier is no longer recommended for use. Consider modern solutions like RSS feed readers or browser extensions for monitoring website changes.
