using System;

namespace Scripts.Game.Model
{
	/// <summary>
	/// The default concrete class of <see cref="IProp"/>
	/// </summary>
	public class DefaultPropType : IPropType
	{
		public DefaultPropType (string name)
		{
			this.Name = name;
		}
			
		public string Name { get; private set; }

		public override int GetHashCode() {
			return this.Name.GetHashCode ();
		}
		
	}
}

