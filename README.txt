This is a learning diary app with a console user interface.

To run the app, clone the repository and make sure you have the following packages installed with Nuget Package Manager:
	- Microsoft.AspNetCore.Mvc.Core Version 2.2.5
	- Microsoft.EntityFrameworkCore.SqlServer Version 5.0.17
	- Microsoft.EntityFrameworkCore.Tools Version 5.0.17

You also need an SQL client installed (such as Microsoft SQL Server Management Studio)

Browse to the folder containing Program.cs and use the command 'dotnet run', or use Visual Studio or another IDE to run the solution file.

The diary holds topics among other information, topics hold tasks among other information. Tasks hold notes among other information.

You navigate giving number commands and there are three levels to the app:

1 - main menu
	2 - topic menu
		3 - task menu

The app is missing lots of functionality still, but it should be usable.

The data is read from and saved to a Microsoft SQL Server.

The program is localized to English and Finnish.

Localization functionality in Localization.cs
UserUI functionality (input validation, banner prints) is in UserUI.cs

Previous file writing/reading functionality in FileIO.cs