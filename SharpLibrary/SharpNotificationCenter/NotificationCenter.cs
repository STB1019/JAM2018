using System;
using System.Collections.Generic;
using System.Text;

namespace SharpNotificationCenter
{
	/// <summary>
	/// Contains every information to uniquely identify a subscriber
	/// </summary>
	internal struct SubscriptionInfo
	{
		/// <summary>
		/// The subscriber itself
		/// </summary>
		public object Source { get; private set; }
		/// <summary>
		/// The action to perform when a new notification happens
		/// </summary>
		public NotificationEventHandler Action { get; private set; }

		internal SubscriptionInfo(object source, NotificationEventHandler action)
		{
			this.Source = source;
			this.Action = action;
		}
	}

	/// <summary>
	/// Every subscription action needs to be compliant with this delegate
	/// </summary>
	/// <param name="nc">the notification center that dispatched the notification</param>
	/// <param name="notification">the actual notification</param>
	public delegate void NotificationEventHandler(NotificationCenter nc, INotification notification);

	/// <summary>
	/// Represents an object that accepts subscriptions and dispatches notifications to other objects.
	/// See <see cref="DefaultNotificationCenter"/> for an example on how to use a notification center.
	/// 
	/// </summary>
	/// <remarks>
	/// You can tell the notification center how to actually build the istance of <see cref="INotification"/> via "Factory" method:
	/// if you want to customize such build process, just create an instance of <see cref="AbstractNotificationCenterFactory"/> and set it
	/// via the setter "Method".
	/// </remarks>
	public class NotificationCenter
	{
		/// <summary>
		/// A factory used to determine how to build <see cref="INotification"/> instances
		/// </summary>
		public AbstractNotificationCenterFactory Factory {internal get; set; }
		/// <summary>
		/// The structure containing every subscriber. The dictionary is indexed by topic while each sorted list
		/// represents the list of subscribers for that particular topic. In the sorted list the keys are the priority of each subscriber
		/// while the values are the subscription information.
		/// </summary>
		private IDictionary<int, SortedList<int, SubscriptionInfo>> Subscribers { get; set; }

		internal NotificationCenter()
		{
			this.Factory = new DefaultNotificationCenterFactory();
			this.Subscribers = new Dictionary<int, SortedList<int,SubscriptionInfo>>();
		}

		/// <summary>
		/// Register "you" as someone he's interested in a particular topic
		/// </summary>
		/// <param name="you">the object insterested in receiving notifications</param>
		/// <param name="type">the topic "you" is interested in</param>
		/// <param name="priority">lower values mean that among all the subscribers of a particular topic, "you" will be notified first.
		/// Contrarly, big values means that "you" will be notified at last</param>
		/// <param name="action">the action to perform</param>
		/// <returns>the number of subscribers of the specified topic</returns>
		public int SubscribeFor(object you, int type, int priority, NotificationEventHandler action)
		{
			lock (this.Subscribers)
			{
				SortedList<int, SubscriptionInfo> subscriberForType = null;
				if (this.Subscribers.ContainsKey(type))
				{
					subscriberForType = this.Subscribers[type];
				} else
				{
					subscriberForType = new SortedList<int, SubscriptionInfo>();
					this.Subscribers[type] = subscriberForType;
				}
				subscriberForType.Add(priority, new SubscriptionInfo(you, action));
				return subscriberForType.Count;
			}
		}

		/// <summary>
		/// Unsubscribe a particular object from an event type.
		/// If "you" is not subscribed to the topic, the function does nothing
		/// </summary>
		/// <param name="type">the type of the event you want to subscribe to</param>
		/// <param name="you">the object we need to unsubscribe</param>
		/// <returns>the number of object still subscribed to the particular topic</returns>
		public int UnsubscribeFor(int type, object you)
		{
			lock (this.Subscribers)
			{
				if (!this.Subscribers.ContainsKey(type))
				{
					return 0;
				}
				int index = -1;
				foreach (var si in this.Subscribers[type])
				{
					if (si.Value.Source == you)
					{
						index = si.Key;
						break;
					}
				}
				if (index < -1)
				{
					//we didn't find the listener "you". We ignore this call
					return this.Subscribers[type].Count;
				}
				//we need to remove the subscriber
				this.Subscribers[type].RemoveAt(index);
				return this.Subscribers[type].Count;
			}
		}

		private int sendNotification(bool async, int type, object source, IDictionary<int, object> data)
		{
			lock (this.Subscribers)
			{
				SortedList<int, SubscriptionInfo> subscriberForType = null;
				if (!this.Subscribers.ContainsKey(type))
				{
					//no subscribers for this event. Do nothing
					return 0;
				}
				subscriberForType = this.Subscribers[type];
				INotification n = this.Factory.BuildNotification(source, type, data);
				int retVal = 0;
				foreach (var sortedListKeyPair in subscriberForType)
				{
					if (async)
					{
						sortedListKeyPair.Value.Action.BeginInvoke(this, n, cb =>
						{
							if (cb.IsCompleted && cb.AsyncWaitHandle != null)
							{	
								cb.AsyncWaitHandle.Close();
							}
						}, null);
					} else
					{
						sortedListKeyPair.Value.Action(this, n);
					}
					
					retVal++;
				}
				return retVal;
			}
		}

		/// <summary>
		/// Sends a notification to every suscribed objects <b>synchronously</b>.
		/// This means that the notification center will wait until the callback for the subscriber "i" fully completes before starting the
		/// execution of the callback of the subscriber "i+1".
		/// </summary>
		/// <param name="type">the topic of the notification</param>
		/// <param name="source">who has sent the notification</param>
		/// <param name="data">an optional paylaod to send attached to the notification</param>
		/// <returns>the number of object notified by this message</returns>
		public int SendSynchronousNotification(int type, object source, IDictionary<int, object> data)
		{
			return this.sendNotification(false, type, source, data);
		}

		/// <summary>
		/// Sends a notification to every suscribed objects <b>asynchronously</b>.
		/// This means that the notification center will <b>not</b> wait until the callback for the subscriber "i" fully completes before starting the
		/// execution of the callback of the subscriber "i+1". Instead, all callbacks will start at the same time.
		/// </summary>
		/// <param name="type">the topic of the notification</param>
		/// <param name="source">who has sent the notification</param>
		/// <param name="data">an optional paylaod to send attached to the notification</param>
		/// <returns>the number of object notified by this message</returns>
		public int SendASynchronousNotification(int type, object source, IDictionary<int, object> data)
		{
			return this.sendNotification(true, type, source, data);
		}
	}
}
