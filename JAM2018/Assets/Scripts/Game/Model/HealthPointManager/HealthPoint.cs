using System;
using System.Collections.Generic;
using System.Text;

namespace Scripts.Game.Model
{
        /// <summary>
        /// A structure representing a Health Point.
        /// It's featured by an integer value.
	/// </summary>
        public class HealthPoint {
                /// <summary>
		/// The value of a health point
		/// </summary>
        int HPValue { get; set; }

                public HealthPoint(int value)
                {
                        this.HPValue = value;
                }
        }
}
