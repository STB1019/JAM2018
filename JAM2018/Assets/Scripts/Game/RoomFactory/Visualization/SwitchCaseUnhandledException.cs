using System;

namespace AssemblyCSharp
{
	public class SwitchCaseUnhandledException : Exception
	{
		public SwitchCaseUnhandledException () : base()
		{

		}

		public SwitchCaseUnhandledException (String message) : base(message)
		{

		}
	}
}

