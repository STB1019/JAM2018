using System;

namespace Scripts.Game.Model
{
	public interface IQualitas
	{
		/// <summary>
		/// The type of the given qualitas
		/// </summary>
		/// <value>The class.</value>
		IQualitasType Class { get; }

		/// <summary>
		/// The prop this qualitas is attached to
		/// </summary>
		/// <value>If NULL the qualitas is attached to nothing</value>
		IProp Prop {get; set;}

		/// <summary>
		/// An ID that uniquely identify the qualitas instance throughout the runtime. 2 qualitates with the same class has different id
		/// </summary>
		int Id {get; }
	}
}

