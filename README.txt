This is a learning diary app with a console user interface.

You need to be running an SQL server at your localhost (edit in Learning_Diary_ELContext.cs) with appropriate tables for it to work.

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