﻿using System;
using Scripts.Game.Model;
using System.Collections.Generic;

namespace Scripts.Game.ActualProps
{
	public class Fountain : AbstractDefaultProp
	{
		static Fountain() {
			PropTypes.INSTANCE.Add(typeof(Fountain));
		}

		public Fountain() : base(PropTypes.INSTANCE[typeof(Fountain)]) {
		}
	}
}

