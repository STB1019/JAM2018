using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpUtilities;

namespace TestSharpUtilities
{
	[TestClass]
	public class TestSharpEventList
	{

		public delegate void ActionCallback();

		private int Id { get; set; }
		private SortedEventList<ActionCallback> Sel { get; set; }

		private void ActionPlus1()
		{
			this.Id += 1;
		}

		private void ActionPlus2()
		{
			this.Id += 2;
		}

		private void ActionTimes1()
		{
			this.Id *= 1;
		}

		private void ActionTimes2()
		{
			this.Id *= 2;
		}

		[TestInitialize]
		public void Setup()
		{
			this.Id = 0;
			this.Sel = new SortedEventList<ActionCallback>(5);
		}

		[TestMethod]
		public void TestFireEvents()
		{
			this.Sel += this.ActionPlus1;
			this.Sel += this.ActionPlus2;

			this.Sel.FireEvents();

			Assert.AreEqual(this.Id, 3);
		}

		[TestMethod]
		public void TestFireEventsWithPriority1()
		{
			this.Sel += Pair<int, ActionCallback>.Build(2, this.ActionPlus1);
			this.Sel += Pair<int, ActionCallback>.Build(3, this.ActionTimes2);
			this.Sel.FireEvents();
			Assert.AreEqual(this.Id, 2);
		}

		[TestMethod]
		public void TestFireEventsWithPriority2()
		{
			this.Sel += Pair<int, ActionCallback>.Build(3, this.ActionPlus1);
			this.Sel += Pair<int, ActionCallback>.Build(2, this.ActionTimes2);
			this.Sel.FireEvents();
			Assert.AreEqual(this.Id, 1);
		}

		[TestMethod]
		public void TestFireEventsWithPriority3()
		{
			this.Sel += this.ActionPlus1;
			this.Sel += Pair<int, ActionCallback>.Build(2, this.ActionTimes2);
			this.Sel.FireEvents();
			Assert.AreEqual(this.Id, 1);
		}

		[TestMethod]
		public void TestFireEventsWithPriority4()
		{
			this.Sel += Pair<int, ActionCallback>.Build(2, this.ActionPlus1);
			this.Sel += this.ActionTimes2;
			this.Sel.FireEvents();
			Assert.AreEqual(this.Id, 2);
		}

		[TestMethod]
		public void TestFireEventsWithPriority5()
		{
			var action = Pair<int, ActionCallback>.Build(2, this.ActionPlus1);

			this.Sel += action;
			this.Sel += this.ActionTimes2;
			this.Sel.FireEvents();
			Assert.AreEqual(this.Id, 2);

			this.Id = 5;
			this.Sel -= action;

			this.Sel.FireEvents();
			Assert.AreEqual(this.Id, 10);
		}
	}
}
