
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpNotificationCenter
{
	/// <summary>
	/// A standard implementation of <see cref="AbstractNotificationCenterFactory"/>, building <see cref="DefaultNotification"/>
	/// </summary>
	public class DefaultNotificationCenterFactory : AbstractNotificationCenterFactory
	{
		/// <inheritDoc/>
		public override INotification BuildNotification(object source, int type, IDictionary<int, object> data)
		{
			return new DefaultNotification(source, type, data);
		}
	}
}
