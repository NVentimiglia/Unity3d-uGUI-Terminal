# Foundation Terminal (v4.0)

Nicholas Ventimiglia | AvariceOnline.com

## Terminal for in game debugging

The goal of this library to provide a UI for testing low level libraries and debugging in game. Built using uGUI. Is Unity3d 5 ready !

 - Log many message types with color coding
 - Hooks into Debug.Log
 - A command button bar for testing methods
 - A text input with optional input handling (text processors)
 - Hide and close with the ` key

![alt tag](https://github.com/NVentimiglia/Unity3d-uGUI-Terminal/blob/master/Terminal.gif)

## Setup

Drop the Terminal Prefab into your scene.

## Example Usage

````

	// Write
	Terminal.Log("blag blah");
	Terminal.LogError("blag blah");
	Terminal.LogSuccess("blag blah");
	Terminal.LogWarning("blag blah");
	Terminal.LogImportant("blag blah");
	// Wired to Application Log
	Debug.Log("blah");

	// Register button commands. (Do this in Awake)
	Terminal.Add(new TerminalCommand
		{
			Label = "Main",
			Method = MainTest
		});

	void MainTest()
	{
	   // Run When Clicked
	}

	// Register new Text Processors (invoked when text is submitted)
	Terminal.Add(new TerminalInterpreter
		{
			Label = "Chat",
			Method = ChatExample
		});

	void ChatExample(string text)
	{
	   // Run When inputted
	}
````

## More

Part of the Unity3d Foundation toolkit. A collection of utilities for making high quality data driven games. http://unity3dFoundation.com

- [**Tasks**](https://github.com/NVentimiglia/Unity3d-Async-Task) : An async task library for doing background work or extending coroutines with return results.
- [**Messenger**](https://github.com/NVentimiglia/Unity3d-Event-Messenger) : Listener pattern. A message broker for relaying events in a loosely coupled way. Supports auto subscription via the [Subscribe] annotation.
- [**Terminal**](https://github.com/NVentimiglia/Unity3d-uGUI-Terminal): A in game terminal for debugging !
- [**Injector**](https://github.com/NVentimiglia/Unity3d-Service-Injector): Service Injector for resolving services and other components. Supports auto injection using the [Inject] annotation
- [**DataBinding**](https://github.com/NVentimiglia/Unity3d-Databinding-Mvvm-Mvc) : For MVVM / MVC style databinding. Supports the new uGUI ui library.
- [**Localization**](https://github.com/NVentimiglia/Unity3d-Localization)   : Supports in editor translation, multiple files and automatic translation of scripts using the [Localized] annotation.
- **Cloud** : Parse-like storage and account services using a ASP.NET MVC back end. Need to authenticate your users? Reset passwords with branded emails? Save high scores or character data in a database? Maybe write your own authoritative back end? This is it.
- **Lobby** : The ultimate example scene. Everything you need to deploy for a game, minus the actual game play.

## Donations
[I accept donations via papal. Your money is an objective measure of my self esteem.](https://www.paypal.com/us/cgi-bin/webscr?cmd=_send-money&nav=1&email=nick@simplesys.us)
