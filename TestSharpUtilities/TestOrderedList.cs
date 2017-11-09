using System;
using SharpUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestSharpUtilities
{
	[TestClass]
	public class TestOrderedList
	{
		private OrderedList<int> OrderedList;

		[TestInitialize]
		public void Setup()
		{
			this.OrderedList = new OrderedList<int>();
		}

		[TestMethod]
		public void TestOrderedList1()
		{
			Assert.IsTrue(this.OrderedList.IsEmpty);
			this.OrderedList.Add(5);
			Assert.AreEqual(this.OrderedList.Count, 1);
			Assert.IsFalse(this.OrderedList.IsEmpty);
		}

		[TestMethod]
		public void TestOrderedList2()
		{
			this.OrderedList.Add(5);
			this.OrderedList.Add(6);

			Assert.AreEqual(this.OrderedList.Count, 2);
			Assert.AreEqual(this.OrderedList[0], 5);
			Assert.AreEqual(this.OrderedList[1], 6);
		}

		[TestMethod]
		public void TestOrderedList3()
		{
			this.OrderedList.Add(6);
			this.OrderedList.Add(5);

			Assert.AreEqual(this.OrderedList.Count, 2);
			Assert.AreEqual(this.OrderedList[0], 5);
			Assert.AreEqual(this.OrderedList[1], 6);
		}

		[TestMethod]
		public void TestOrderedList4()
		{
			this.OrderedList.Add(10);
			this.OrderedList.Add(20);
			this.OrderedList.Add(30);
			this.OrderedList.Add(40);

			Assert.AreEqual(this.OrderedList.Count, 4);

			this.OrderedList.Add(50);
			Assert.AreEqual(this.OrderedList[0], 10);
			Assert.AreEqual(this.OrderedList[1], 20);
			Assert.AreEqual(this.OrderedList[2], 30);
			Assert.AreEqual(this.OrderedList[3], 40);
			Assert.AreEqual(this.OrderedList[4], 50);
		}

		[TestMethod]
		public void TestOrderedList5()
		{
			this.OrderedList.Add(10);
			this.OrderedList.Add(20);
			this.OrderedList.Add(30);
			this.OrderedList.Add(40);

			Assert.AreEqual(this.OrderedList.Count, 4);

			this.OrderedList.Add(3);
			Assert.AreEqual(this.OrderedList[0], 3);
			Assert.AreEqual(this.OrderedList[1], 10);
			Assert.AreEqual(this.OrderedList[2], 20);
			Assert.AreEqual(this.OrderedList[3], 30);
			Assert.AreEqual(this.OrderedList[4], 40);
		}

		[TestMethod]
		public void TestOrderedList6()
		{
			this.OrderedList.Add(10);
			this.OrderedList.Add(20);
			this.OrderedList.Add(30);
			this.OrderedList.Add(40);

			Assert.AreEqual(this.OrderedList.Count, 4);

			this.OrderedList.Add(13);
			Assert.AreEqual(this.OrderedList[0], 10);
			Assert.AreEqual(this.OrderedList[1], 13);
			Assert.AreEqual(this.OrderedList[2], 20);
			Assert.AreEqual(this.OrderedList[3], 30);
			Assert.AreEqual(this.OrderedList[4], 40);
		}

		[TestMethod]
		public void TestOrderedList7()
		{
			this.OrderedList.Add(20);
			this.OrderedList.Add(10);
			this.OrderedList.Add(40);
			this.OrderedList.Add(30);

			Assert.AreEqual(this.OrderedList.Count, 4);

			var e = this.OrderedList.GetEnumerator();
			Assert.AreEqual(e.MoveNext(), true);
			Assert.AreEqual(e.Current, 10);
			Assert.AreEqual(e.MoveNext(), true);
			Assert.AreEqual(e.Current, 20);
			Assert.AreEqual(e.MoveNext(), true);
			Assert.AreEqual(e.Current, 30);
			Assert.AreEqual(e.MoveNext(), true);
			Assert.AreEqual(e.Current, 40);
			Assert.AreEqual(e.MoveNext(), false);
		}
	}
}
