using System;
using System.Collections.Generic;
using System.Text;

namespace SharpGraphs
{
	/// <summary>
	/// A generic exception raise when something went wrong during graph computation
	/// </summary>
	public class GraphException : Exception
    {
		/// <summary>
		/// A generic exception raise when something went wrong during graph computation
		/// </summary>
		/// <param name="message"></param>
		public GraphException(string message): base(message)
		{
		}

		/// <summary>
		/// A generic exception raised when something goes wrong during graph computation
		/// </summary>
		/// <param name="message">the message of the graph</param>
		/// <param name="innerException">the exception that caused this error</param>
		public GraphException(string message, Exception innerException) : base(message, innerException)
		{
		}

		/// <summary>
		/// A generic exception raised when something goes wrong during graph computation
		/// </summary>
		/// <param name="innerException">the exception that caused this error</param>
		public GraphException(Exception innerException) : base("", innerException)
		{

		}
	}
}
