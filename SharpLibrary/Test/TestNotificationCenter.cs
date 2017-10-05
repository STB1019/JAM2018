using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpNotificationCenter;
using System.Collections.Generic;

namespace Test
{
	internal class Foo {

		public NotificationEventHandler Handler { get; set; }

		public Foo(NotificationEventHandler handler)
		{
			this.Handler = handler;
		}
	}

	internal class Bar
	{

	}

	[TestClass]
	public class TestNotificationCenter
	{
		internal Foo foo1;
		internal Foo foo2;
		internal Bar bar;
		internal int state;

		[TestInitialize()]
		public void Setup()
		{
			this.state = 0;
			this.foo1 = new Foo((ns, i) => { state += 1; });
			this.foo2 = new Foo((ns, i) => { state += (int)i[1]; });
			this.bar = new Bar();
		}

		[TestMethod]
		public void TestSubscribeReceiveNoPayload()
		{
			Assert.IsTrue(this.state == 0);
			//subscribe
			DefaultNotificationCenter.Get.SubscribeFor(this.foo1, 1, 0, this.foo1.Handler);
			//now notify
			IDictionary<int, object> d = new Dictionary<int, object>();
			DefaultNotificationCenter.Get.SendSynchronousNotification(1, bar, d);
			Assert.IsTrue(this.state == 1);
		}

		[TestMethod]
		public void TestSubscribeReceiveWithPayload()
		{
			Assert.IsTrue(this.state == 0);
			//subscribe
			DefaultNotificationCenter.Get.SubscribeFor(this, 1, 0, this.foo2.Handler);
			//now notify
			IDictionary<int, object> d = new Dictionary<int, object>();
			d[1] = 4;
			DefaultNotificationCenter.Get.SendSynchronousNotification(1, bar, d);
			Assert.IsTrue(this.state == 4);
		}
	}
}
