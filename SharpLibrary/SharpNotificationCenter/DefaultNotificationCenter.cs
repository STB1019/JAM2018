using System;
using System.Collections.Generic;
using System.Text;

namespace SharpNotificationCenter
{
	/// <summary>
	/// A static class allowing you to retrieve an instance of a <see cref="NotificationCenter"/>.
	/// A NotificationCenter is used in 2 steps: first all object needs to subscribe to particular "topics" (eg. MouseMove, Achivements, EnemySpawn).
	/// After that, when something remarkable happen, an object needs to call the NotificationCenter and forward a message: everyone subscribed to the topic
	/// where the message was sent will receive the notification.
	/// 
	/// You can order subscribers to the same topic with different priorities too: this can be used to ensure some subscribers is notifies before others.
	/// You can use <see cref="NotificationCenterPriority"/> to convenitionally set some standard priorities.
	/// Finally you can send notification in a synchronous way as well in a asynchronous way: the former will notify one subscriber per time and
	/// it will wait for a subscriber to finish its callback before executing the next subscriber callback; Asynchronous notification will execute them
	/// asynchronously.
	/// </summary>
	/// <example> 
	/// This sample shows how to use a notification center.
	/// <code>
	/// class TestClass 
	/// {
	///     static int Main() 
	///     {
	///		
	///			TestClass tc = new TestClass();
	///			//subscription
	///			DefaultNotificationCenter.Get.SubscribeFor(tc, 1, 0, tc.OnNotification);
	///			//create notification (may happen in a different thread as well)
	///			DefaultNotificationCenter.Get.SendSynchronousNotification(1, tc, new Dictionary());
	///			//OnNotification should be called
	///     }
	///     
	///		void OnNotification(NotificationCenter nc, INotification n) {
	///			Console.Writeln("Receive notification of type " + n.Type);
	///		}
	/// }
	/// </code>
	/// </example>
	public static class DefaultNotificationCenter
	{
		private static readonly NotificationCenter NC = new NotificationCenter();

		/// <summary>
		/// Retrieve an instance of a NotificationCenter
		/// </summary>
		public static NotificationCenter Get { get { return NC; } }
	}
}
