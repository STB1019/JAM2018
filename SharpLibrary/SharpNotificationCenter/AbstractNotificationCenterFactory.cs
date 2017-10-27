using System;
using System.Collections.Generic;
using System.Text;

namespace SharpNotificationCenter
{
	/// <summary>
	/// Represents an abstract factory used to build every component needed inside a <see cref="NotificationCenter"/>
	/// </summary>
	public abstract class AbstractNotificationCenterFactory
	{
		/// <summary>
		/// Method called whenever a <see cref="NotificationCenter"/> needs to create a new instance of <see cref="INotification"/>
		/// </summary>
		/// <param name="source">the object that requested to create a notification to dispatch with the notification center</param>
		/// <param name="type">the topic of the notification</param>
		/// <param name="data">an optional dictionary used to attach data to the notification</param>
		/// <returns>an instance of a notification</returns>
		public abstract INotification BuildNotification(object source, int type, IDictionary<int, object> data);
	}
}
