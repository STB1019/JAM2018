using System;
using System.Collections.Generic;
using System.Text;

namespace SharpNotificationCenter
{
	/// <summary>
	/// A concrete implementation of <see cref="INotification"/>. This concrete class is used by default inside a <see cref="NotificationCenter"/>
	/// </summary>
	internal class DefaultNotification : INotification
	{
		/// <summary>
		/// The topic of the notification
		/// </summary>
		public int Type { get; private set; }
		/// <summary>
		/// Represents the object that requested to send a notification by the notification center
		/// </summary>
		public object Source { get; private set; }
		/// <summary>
		/// An optional payload of data to send with the message
		/// </summary>
		public IDictionary<int, object> Data { get; private set; }

		public DefaultNotification(object source, int type, IDictionary<int, object> data)
		{
			this.Source = source;
			this.Type = type;
			this.Data = data;
		}

		public bool IsEmpty { get { return this.Data.Count == 0; } }

		public int DataSize { get
			{
				return this.Data.Count;
			}
			}

		public bool Contains(int key)
		{
			return this.Data.ContainsKey(key);
		}

		public object Get(int key)
		{
			return this.Data[key];
		}

		public object this[int key] {
			get {
				return this.Get(key);
			}
		}
	}
}
