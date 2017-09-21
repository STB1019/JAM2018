using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace SharpUtilities
{
	/// <summary>
	/// A set of methods really useful in order to easily manage external process calls
	/// </summary>
    public static class ProcessUtils
    {
		/// <summary>
		/// Run the following command in the CMD and wait for the command to end. The output of the program will be totally ignored
		/// </summary>
		/// <param name="programToCall">the name of the program to execute. Remember to put the ".exe" at the end of the program!</param>
		/// <param name="argumentsFormat">template of the command argumentsto execute</param>
		/// <param name="objs">objects to use inside argumentsFormat</param>
		/// <exception cref="FileNotFoundException">If the "programToCall" you required can't be found within the PATH environment variable</exception>
		public static void ExecuteCommandAndWait(string programToCall, string argumentsFormat, params object[] objs)
		{
			var proc = new Process
			{
				StartInfo = new ProcessStartInfo
				{
					FileName = WhichIsExecutable(programToCall), //@"C:\Program Files (x86)\Graphviz2.38\bin\dot.exe",
					Arguments = string.Format(argumentsFormat, objs),
					UseShellExecute = true,
					//RedirectStandardOutput = false,
					WorkingDirectory = Directory.GetCurrentDirectory(),
					CreateNoWindow = true
				}
			};
			proc.Start();
			proc.WaitForExit();
		}

		/// <summary>
		/// Computes the full path of the given executable present inside PATH environment variable
		/// </summary>
		/// <param name="executable">the name of the executable to look for. Needs to have ".exe" extension explcitly written!</param>
		/// <returns>the full path of the executable you're looking for. This only works if the executable "executable" is inside one of 
		/// the folders defined in PATH environment variable</returns>
		/// <exception cref="FileNotFoundException">if the executable isn't present inside PATH environment variable</exception>
		/// <seealso cref="IsExecutableInPath(string)"/>
		public static string WhichIsExecutable(string executable)
		{
			string path = Environment.GetEnvironmentVariable("PATH");
			var paths = path.Split(';');
			for (int i=0; i<paths.Length; i++)
			{
				var possibleExecutableFullPath = Path.Combine(paths[i], executable);
				if (File.Exists(possibleExecutableFullPath))
				{
					return possibleExecutableFullPath;
				}
			}
			throw new FileNotFoundException(string.Format("Couldn't find executalbe \"{0}\" within the PTATH environment variable.", executable));
		}

		/// <summary>
		/// Check if an executable is within the reach of PATH environment variable
		/// </summary>
		/// <param name="executable">the name of the executable to look for. ".exe" needs to be included!</param>
		/// <returns>true if the executable is inside at least one path in your PATH environment variable. False otherwise</returns>
		public static bool IsExecutableInPath(string executable)
		{
			try
			{
				WhichIsExecutable(executable);
				return true;
			} catch (FileNotFoundException e)
			{
				return false;
			}
		}
	}
}
