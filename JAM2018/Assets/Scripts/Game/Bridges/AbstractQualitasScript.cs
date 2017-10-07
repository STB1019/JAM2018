using System;
using UnityEngine;
using System.Collections.Generic;
using Scripts.Game.Model;

namespace Scripts.Game.Bridges
{
	/// <summary>
	/// The script is a bridge between unity environment and game model
	/// </summary>
	[DisallowMultipleComponent()]
	public abstract class AbstractQualitasScript<QUALITAS> : MonoBehaviour, IQualitas where QUALITAS : IQualitas
	{
		/// <summary>
		/// Retirve the prop this scripts represents
		/// </summary>
		/// <value>the prop represents</value>
		public QUALITAS Qualitas { get; protected set; }
		public IQualitasType Class { get { return this.Qualitas.Class; } }
		public IProp Prop {get { return this.Qualitas.Prop; } set { this.Qualitas.Prop = value; }}
		public int Id { get { return this.Qualitas.Id; } }

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

