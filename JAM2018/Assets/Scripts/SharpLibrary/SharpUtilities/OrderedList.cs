using System;
using System.Collections;
using System.Collections.Generic;

namespace SharpUtilities
{
	/// <summary>
	/// A comparer which compares 2 object of the same type by looking at their hashcode.
	/// Notes that if "VALUE" is indeed "int" then the hashcode of an int is its actual value.
	/// </summary>
	/// <typeparam name="VALUE">the type of objects to compare</typeparam>
	internal class DefaultComparer<VALUE> : IComparer<VALUE>
	{
		/// <inheritdoc/>
		public int Compare(VALUE x, VALUE y)
		{
			//for int32 the hashcode is the number itself
			return x.GetHashCode() - y.GetHashCode();
		}
	}

	/// <summary>
	/// Represents a collection which automatically sorts the added data.
	/// You can use the default <see cref="IComparer"/> available in the structure or you can pass
	/// you custom one. The default compararer compares the objects by their <see cref="Object.GetHashCode"/> outcomes.
	/// See <see cref="DefaultComparer{VALUE}"/> for additional information.
	/// </summary>
	/// <example>
	/// The example showcases how <see cref="OrderedList{VALUE}"/> can be used.
	/// <code>
	/// OrderedList<int> ol = new OrderedList<int>();
	/// ol.Add(5);
	/// ol.Add(1);
	/// ol.Add(2);
	/// ol.Add(4);
	/// 
	/// foreach (var i in ol) {
	///		Console.Writeln(i),
	/// }
	/// //outputs 1,2,4,5
	/// </code>
	/// </example>
	/// <typeparam name="VALUE">the value inside the collection</typeparam>
	public class OrderedList<VALUE> : ICollection<VALUE>, IEnumerable<VALUE> where VALUE : IComparable
	{
		/// <summary>
		/// The default comparer
		/// </summary>
		private static readonly IComparer<VALUE> DEFAULT_COMPARER = new DefaultComparer<VALUE>();
		/// <summary>
		/// The comparer used when adding new objects inside the list
		/// </summary>
		private IComparer<VALUE> Comparer
		{
			get; set;
		}
		/// <summary>
		/// The wrapped list. Such list is always correclty ordered
		/// </summary>
		private List<VALUE> SortedList { get; set; }

		/// <summary>
		/// Creates a new list by comparing <see cref="Object.GetHashCode"/> of the values added within it
		/// </summary>
		public OrderedList() : this(DEFAULT_COMPARER)
		{

		}

		/// <summary>
		/// Creates a new list by injecting a custom compararer
		/// </summary>
		/// <param name="comparer">The structure you want to use when sorting the elements of the list</param>
		public OrderedList(IComparer<VALUE> comparer)
		{
			this.SortedList = new List<VALUE>();
			this.Comparer = comparer;
		}

		/// <summary>
		/// Checks if the list is actually empty
		/// </summary>
		public bool IsEmpty
		{
			get
			{
				return this.Count == 0;
			}
		}

		/// <summary>
		/// Return the i-th elements of the list
		/// </summary>
		/// <param name="index">the index of the element to retrieve</param>
		/// <returns>the ith element of the list</returns>
		public VALUE this[int index]
		{
			get
			{
				return this.SortedList[index];
			}
		}

		#region ICollection

		///<inheritdoc/>
		public int Count
		{
			get
			{
				return this.SortedList.Count;
			}
		}

		///<inheritdoc/>
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>
		/// Adds an element inside the list. The element is added in a position to ensure the sorting of the list itself
		/// </summary>
		/// <param name="item">the item to add</param>
		public void Add(VALUE item)
		{
			for (var i=0; i<this.SortedList.Count; i++)
			{
				if (this.Comparer.Compare(this.SortedList[i], item) > 0)
				{
					this.SortedList.Insert(i, item);
					return;
				}
			}
			//we need to add them in tail
			this.SortedList.Add(item);
		}

		///<inheritdoc/>
		public void Clear()
		{
			this.SortedList.Clear();
		}

		///<inheritdoc/>
		public bool Contains(VALUE item)
		{
			return this.SortedList.Contains(item);
		}

		///<inheritdoc/>
		public void CopyTo(VALUE[] array, int arrayIndex)
		{
			this.SortedList.CopyTo(array, arrayIndex);
		}

		///<inheritdoc/>
		public IEnumerator<VALUE> GetEnumerator()
		{
			return this.SortedList.GetEnumerator();
		}

		///<inheritdoc/>
		public bool Remove(VALUE item)
		{
			return this.SortedList.Remove(item);
		}

		///<inheritdoc/>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.SortedList.GetEnumerator();
		}

		///<inheritdoc/>
		IEnumerator<VALUE> IEnumerable<VALUE>.GetEnumerator()
		{
			return this.SortedList.GetEnumerator();
		}

		#endregion
	}
}
