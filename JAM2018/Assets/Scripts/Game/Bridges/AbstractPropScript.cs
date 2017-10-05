using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Scripts.Game.Model;

namespace Scripts.Game.Bridges
{
	/// <summary>
	/// The script is a bridge between unity environment and game model
	/// </summary>
	[DisallowMultipleComponent()]
	public abstract class AbstractPropScript<PROP> : MonoBehaviour, IProp where PROP : IProp
	{
		/// <summary>
		/// Retirve the prop this scripts represents
		/// </summary>
		/// <value>the prop represents</value>
		public PROP Prop { get; protected set; }
		public IPropType Class { get { return this.Prop.Class; } }
		public IList<IQualitas> Qualitates { get { return this.Prop.Qualitates; } set { this.Prop.Qualitates = value; } }
		public int Id { get { return this.Prop.Id; } }

		/// <summary>
		/// Gets the implemented interfaces of this prop
		/// </summary>
		/// <returns>The implemented interfaces.</returns>
		public IList<Type> getImplementedInterfaces ()
		{
			return this.GetType ().GetInterfaces ();
		}
	}
}
