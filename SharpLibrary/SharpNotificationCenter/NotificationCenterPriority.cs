using System;
using System.Collections.Generic;
using System.Text;

namespace SharpNotificationCenter
{
	/// <summary>
	/// A mapping to increase readability when setting priorities in methods like
	/// <see cref="NotificationCenter.SendSynchronousNotification(int, object, IDictionary{int, object})"/> or
	/// <see cref="NotificationCenter.SendASynchronousNotification(int, object, IDictionary{int, object})"/>
	/// </summary>
	public enum NotificationCenterPriority
	{
		/// <summary>
		/// Highest possible priority
		/// </summary>
		HIGHEST = 1,
		/// <summary>
		/// Subscriber with higher priority
		/// </summary>
		HIGH = 10,
		/// <summary>
		/// Subscriber with medium priority
		/// </summary>
		MEDIUM = 100,
		/// <summary>
		/// Subscriber with low priority
		/// </summary>
		LOWER = 1000,
		/// <summary>
		/// Subscriber with very low priority
		/// </summary>
		LOWEST = 10000
	}
}
