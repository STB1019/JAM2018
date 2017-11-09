using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Game.Commons
{
	/// <summary>
	/// Represents a collection of every tag inside project JAM2018
	/// </summary>
	public class Tags
	{
		/// <summary>
		/// Every gameobject tagged with this string is by default a terrain
		/// </summary>
		/// <remarks>Colliding with a terrain allows the player to jump</remarks>
		public const string TERRAIN = "Terrain";
	}
}
