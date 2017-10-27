using log4net;
using SharpGraph;
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
