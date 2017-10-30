using System;

namespace Scripts.Game.RoomFactory.Visualization
{
	/// <summary>
	/// This exception was originally conceived for "RoomCoordinatesSystem" enumeration usage into switch statements.
	/// It can be used in every "switch situation" involving some finite number of possibilities. 
	/// </summary>
	/// 
	/// <author>Michele Dusi</author>
	public class SwitchCaseUnhandledException : Exception
	{
		public SwitchCaseUnhandledException () : base() {}

		public SwitchCaseUnhandledException (String message) : base(message) {}
	}
}

