﻿using System;
using System.Collections.Generic;

namespace Scripts.Game.Model
{
	public static class Qualitates
	{
		public static readonly IList<IQualitasType> ALL = {
			new DefaultQualitasType ("DoSpawn"),
			new DefaultQualitasType ("DoClone")
		};
	}
}

