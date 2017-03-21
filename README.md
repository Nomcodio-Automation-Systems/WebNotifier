# WebNotifier
An windows app which monitor changes on homepages like news or blogs.

This program is in an early alpha stage and has it's prove of concept behind it.

## Getting Started
For quickstart click on release in the github repository of the project and download the newest version.
If you don't have at least .NET 4.5 download it from here https://www.microsoft.com/en-US/download/details.aspx?id=30653 

### Motivation
```
The reason for this program is that you as a user will be informed about changes on websites and not the other way around it 
There are a few simlear systems like the Firefox plug-in Distill or online services which you need to check yourself or news aggregator apps which use a lot of bandwidth
What this projet want to be is a stand-alone solution which works mostly in the background with low bandwidth use  
```


### Prerequisites

```
This program is written in C# compile it with Microsoft Visual Studio or another C# compiler if you want to built it yourself
See also current dependencies
```

###Current Dependencies

WatiN(license https://www.codeproject.com/info/cpol10.aspx) 
.NET 4.5 (Minimum)

Unfortunately is the domain for WatiN expired but here is the backup 
https://www.codeproject.com/Articles/17064/WatiN-Web-Application-Testing-In-NET

###Future Plans
```
A) Full documentation
B) Fix some bugs
C) Improve software design and program functionality
D) Replace WatiN with Mozillas Gecko and/or a socket implementation with HTTPS
E) Implement additional filters for custom support
F) After everything works we try an export to android if we at this point we will forge an repro which seperate the 'engine' from both programs
```

## Authors

* **Niki Radinsky** - *Initial work* - [WebNotifier](https://github.com/Nomcodio-Automation-Systems/WebNotifier)

See also the list of [contributors]https://github.com/Nomcodio-Automation-Systems/WebNotifier/contributors) who participated in this project.

