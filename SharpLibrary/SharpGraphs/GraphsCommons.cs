using log4net;
using SharpGraphs;
using SharpUtilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SharpGraphs
{
	/// <summary>
	/// A class containing all methods shared by all the different graphs implementations
	/// </summary>
	internal class GraphsCommons
	{
		private static readonly ILog LOG = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		/// <summary>
		/// Generate a png containing the graph specified
		/// </summary>
		/// <remarks>
		/// In order to work, this method requires you to have graphviz software installed on your system and "dot.exe" program available on you path.
		/// If this is not satisfied, <b>a warning will be raised</b>.
		/// </remarks>
		/// <typeparam name="NODE">the type of the node payload</typeparam>
		/// <typeparam name="EDGE">the type of the edge payload</typeparam>
		/// <param name="g">the graph to plot</param>
		/// <param name="format">a string format (as in string.Format method) representing the filename of the png to generate</param>
		/// <param name="list">parameters of the string format (as in string.Format method)</param>
		/// <returns>the png filename of the graph just computer</returns>
		/// <see cref="string.Format(string, object[])"/>
		public static string DrawGraph<NODE,EDGE>(IGraph<NODE,EDGE> g, string format, params object[] list)
		{
			string dotfilename = string.Format(format, list);
			string pngfilename = string.Format(format, list);

			if (!dotfilename.EndsWith(".dot"))
			{
				dotfilename += ".dot";
			}

			if (!pngfilename.EndsWith(".png"))
			{
				pngfilename += ".png";
			}

			using (StreamWriter sw = new StreamWriter(new FileStream(dotfilename, FileMode.Create, FileAccess.ReadWrite)))
			{
				sw.WriteLine("digraph {");
				sw.WriteLine(string.Format("	label=\"{0}\";", g.Name));
				//print  nodes
				foreach (Pair<long, NODE> pair in g.GetNodesEnumerable())
				{
					NODE n = g[pair.X];
					sw.WriteLine("	N{0,5:D5} [label=\"{1}\\n{2}\"];", pair.X, pair.X, n.ToString());
				}
				//print edges
				foreach (Triple<long, long, EDGE> e in g.GetEdgesEnumerable())
				{
					sw.WriteLine("N{0,5:D5} -> N{1,5:D5} [label=\"{2}\"];", e.X, e.Y, e.Z.ToString());
				}

				sw.WriteLine("}");
			}

			try
			{
				ProcessUtils.ExecuteCommandAndWait("dot.exe", "-Tpng -o {0} {1}", pngfilename, dotfilename);
			}
			catch (FileNotFoundException e)
			{
				LOG.Warn("Can't create graph " + pngfilename + " because graphviz is not installed! Please install graphviz at http://www.graphviz.org/Download_windows.php and add bin directory to your PATH!");
			}

			File.Delete(dotfilename);

			return pngfilename;
		}

	}
}
