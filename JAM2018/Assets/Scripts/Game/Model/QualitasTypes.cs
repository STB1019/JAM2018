using System;
using System.Collections.Generic;

namespace Scripts.Game.Model
{
	public class QualitasTypes : AbstractTypes<IQualitasType>
	{
		public static QualitasTypes Get { get; private set;}

		internal QualitasTypes() : base() {
		}

		protected override IQualitasType BuildType(string key) {
			return new DefaultQualitasType (key);
		}
	}
}

