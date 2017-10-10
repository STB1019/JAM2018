# JAM2018
The main repository for the project JAM 2018

# Developer information

This section contains everything a contributor needs to know in order to partecipate in this project

## Useful resources

We provide several links that might be useful in order to efficiently code with unity. The resources adds value to the tutorial you all followed before starting the project:

 * [Implementing an Observer on Unity](http://www.habrador.com/tutorials/programming-patterns/3-observer-pattern/);
 * [Ideas when designing model in Unity](http://answers.unity3d.com/questions/365770/how-to-design-your-code-architecture-in-unity.html);
 * [should you use notification center pattern](https://softwareengineering.stackexchange.com/questions/22528/does-notification-center-pattern-encourage-good-or-bad-program-design);
 * [How to setup Sandcastle](https://randynghiem.wordpress.com/2015/06/18/how-to-set-up-sandcastle-help-file-builder-with-visual-studio-2015/)
 * [How should I organize my Unity Project?](http://blog.theknightsofunity.com/7-ways-keep-unity-project-organized/)

## How to contribute ##

Each of you should install zenhub (https://www.zenhub.com/). You need to both sign up to zenhub **and** install the browser plugin (sadly only firefox and chrome are supported).
After that you need to go to the "boards" tab in JAM2018 github repository. There you need to choose a task to do under the pipe "backlog". Click on it: a window will pop up. Go on the right of the newly opened window and select your username under the box "Assignee". Then input an estimate amount of hour you think you will need to completely solve the task: such amount should include university courses, breaks, friends dinner and such. **1 week are 56 hours**. After that write the number of hours you've estimate and put it inside "estimate time".

When you're ready to take the task, put it in the tab "in progress" and start coding. Each time you end your coding session, be sure to update the estimate time of your task. When you've completed your task, put the post under the "Review Q/A" pipe.

As far as git is concerned. each task should be a branch. See git section for further information

## Documentation

As far as documentation is concerned, we use the standard Visual Studio documentation style (aka XML documentation). You can learn about it [here](https://msdn.microsoft.com/en-us/library/b2s063f7(v=VS.100).aspx) or you can check the source of DefaultNotificationCenter.cs or NotificationCenter.cs for a working and almost exaustive example.

The documentation is actually built using Sand Castle File Helper. You can download the latest relase [here](https://github.com/EWSoftware/SHFB). Likewise,, you can learn how to setup the plugin [here](https://randynghiem.wordpress.com/2015/06/18/how-to-set-up-sandcastle-help-file-builder-with-visual-studio-2015/). The project should already have a "Documentation" project inside the repository, so in order to create the documentation you just need to install SandCastle on your computer and build the documentation by clicking "Build" on the "Documentation" project (eg. SharpLibraryDocumentation). The documentation is by default build with **html**.

## C sharp code style

As far as coding style we use raywnderlich conventions (available [here](https://github.com/raywenderlich/c-sharp-style-guide) ). However, we have some differences:

### Properties ###

Properties are written in *PascalCase*. Curly brackets conventions are the same:

**Good:**

	public int Size 
	{
		get 
		{
			...
		};
		set
		{
			...
		}
	}
	
**Bad:**

	public int size 
	{
		get 
		{
			...
		};
		set
		{
			...
		}
	}
	

### Indendation ###

Indentation should use tabs, not spaces. Tabs should be 4 spaces long

### Switch statement ###

Switch should always contain *default* statement, with an error throwing (useful to immediately intercept the unexpected behaviour)

## Git structure style ##

Developers should extensively use git features. 

 * Each feature should be coded inside its own branch. The branch name should be issue_XXXX where "XXXX" is the id of issue on github. 
	The developer can do as many commits as he wants, but at the end he must merge the solved issue into the master **himself**.
 * As for the *rebase*, the golden rule still remains: **never rebase a public branch**.
 * Developers are encouraged to create issues whenever they see fit and perform the given changes into the relative issue.
 * Developers can create issues on github: they can assign such issue to no one, since such issue assignee will be discussed on the 15 minutes meeting.
 * Commit messsage must have a **brief** description in the first line, a blank line and an optional list of code changes after.
 
 For example a good commit is:
 
	Create fountain model
	
	- animation still missing though;
	
While a bad commit is:

	Create fountain model, but the animation is still missing because I woke up too late. I'm sorry guys! :'(
	
If a commit does not solve the whole issue,you have to put the word "WIP" somewhere in the commit. No policy about redundant commits is specified.
