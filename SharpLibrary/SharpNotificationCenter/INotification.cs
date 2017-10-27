using System;
using System.Collections.Generic;
using System.Text;

namespace SharpNotificationCenter
{
	/// <summary>
	/// Represents a notification that can be dispatched by a <see cref="NotificationCenter"/>
	/// </summary>
	public interface INotification
	{
		/// <summary>
		/// The topic this notification is about. For example it might be "MouseMove", "AchivementThread", "DifficultyUpdate", "SpawnedEnemy"
		/// </summary>
		int Type { get; }

		/// <summary>
		/// The object which requested the creation of this notification and sent it via the notification center
		/// </summary>
		object Source { get; }

		/// <summary>
		/// An optional dictionary with additional data to send with the notification.
		/// If you don't want to send data, set this field as a enmpty Dictionary (<b>not as null!</b>)
		/// </summary>
		IDictionary<int, object> Data { get; }

		/// <summary>
		/// True if <see cref="INotification.Data"/> is empty
		/// </summary>
		bool IsEmpty { get; }

		/// <summary>
		/// The number of elements inside <see cref="INotification.Data"/>
		/// </summary>
		int DataSize { get; }

		/// <summary>
		/// Check if the additional data has a particular key
		/// </summary>
		/// <param name="key">the key to check</param>
		/// <returns>true if <see cref="INotification.Data"/> contains the given key, false otherwise</returns>
		bool Contains(int key);

		/// <summary>
		/// Get a value inside the additional data
		/// </summary>
		/// <param name="key">the key whose value we need to retrieve</param>
		/// <returns>the object associated (inside <see cref="INotification"/>) with the given key</returns>
		/// <exception cref="KeyNotFoundException">if the key does not exist</exception>
		object Get(int key);

		/// <summary>
		/// Alias for <see cref="INotification.Get(int)"/>
		/// </summary>
		/// <param name="key">>the key whose value we need to retrieve</param>
		/// <returns>the object associated (inside <see cref="INotification"/>) with the given key</returns>
		/// <exception cref="KeyNotFoundException">if the key does not exist</exception>
		object this[int key] { get; }
	}
}
