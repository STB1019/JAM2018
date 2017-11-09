using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SharpUtilities
{

	/// <summary>
	/// The class mimicks <b>event</b> C# reserved word, but introduced an optional order between the callers.
	/// This list order the data added within it by an "int" priority. When scrolled over, the list will return the elements
	/// with lower priority first. Note that this structure can contain several items with the same priority; in this case there
	/// is no <b>garantuee</b> about which item will be picked first (among the one sharing the same priority of course!).
	/// </summary>
	/// <example>
	/// The example shows how to properly use this class
	/// <code>
	/// public delegate void Callback();
	/// public SortedEventList &lt; Callback &gt; OnAwesomeEvent = new SortedEventList &lt; Callback &gt; (5);
	/// //normal subscription to the event
	/// OnAwesomeEvent += callback1;
	/// OnAwesomeEvent += callback2;
	/// //you can also give a priority 
	/// OnAwesomeEvent += Pair&lt;int, Callback&gt;.build(5, callback1);
	/// //you can remove preexistents event as well
	/// OnAwesomeEvent -= callback1;
	/// //finally you can tell everyone a new event is available (the listeners will be called from the one with lowest key id to the one with the highest
	/// OnAwesomeEvent.FireEvents();
	/// </code>
	/// 
	/// Another example shows how you can use complex delegate to pass values around
	/// <code>
	/// public delegate void Callback(object source, int value);
	/// public void callback(object source, int value) {
	///		Console.Writeln("very useful callback");
	/// }
	/// public SortedEventList&lt;Callback &gt; OnAwesomeEvent = new SortedEventList &lt; Callback &gt; (5);
	/// OnAwesomeEvent += callback;
	/// OnAwesomeEvent.FireEvents(this, 5);
	/// </code>
	/// 
	/// </example>
	/// <typeparam name="VALUE">The type we need to insert in the event list</typeparam>
	public class SortedEventList<VALUE> : IEnumerable<VALUE>
	{
		private IDictionary<VALUE, int> ReverseEvents { get; set; }
		private SortedDictionary<int, IList<VALUE>> Events { get; set; }

		/// <summary>
		/// The default <see cref="Priority"/> an event will have if the developer doesn't specify it while
		/// adding a new element
		/// </summary>
		public Priority DefaultPriority
		{
			get; private set;
		}

		/// <summary>
		/// Initialize a new sorted list
		/// </summary>
		/// <param name="defaultPriority">the default priority to assign to every element no priority is explicitly given</param>
		public SortedEventList(Priority defaultPriority)
		{
			this.Events = new SortedDictionary<int, IList<VALUE>>();
			this.ReverseEvents = new Dictionary<VALUE, int>();
			this.DefaultPriority = defaultPriority;
		}

		/// <summary>
		/// Initialize a new event lsit with default priority set to <see cref="Priority.MEDIUM"/>
		/// </summary>
		public SortedEventList() : this(Priority.MEDIUM)
		{

		}

		/// <summary>
		/// The ordered list of the available priorities in the list
		/// </summary>
		public ICollection<int> Keys { get
			{
				return this.Events.Keys;
			}
		}

		/// <summary>
		/// Notify every listener a new event has been dispatched
		/// </summary>
		/// <example>
		/// This example shows how to use this method
		/// <code>
		/// public delegate int IntOperation(int x, int y);
		/// SortedEventList &lt IntOperation &gt; sel = new SortedEventList();
		/// 
		/// public int add(int x, int y) { return x+y;}
		/// public int times(int x, int y) { return x*y;}
		/// 
		/// sel += add;
		/// sel += times;
		/// sel.FireEvents(1,2);
		/// //the return value of every destination delegate is discarded
		/// </code>
		/// </example>
		/// <param name="obj">the parameters every destination delegate will accept</param>
		public void FireEvents(params object[] obj)
		{
			foreach (var k in this.Events.Keys)
			{
				foreach (var del in this.Events[k]) {
					(del as Delegate).DynamicInvoke(obj);
				}
			}
		}

		/// <summary>
		/// Add a new element inside the list
		/// </summary>
		/// <param name="key">the priority of the element to add</param>
		/// <param name="value">the element to add</param>
		private void Add(int key, VALUE value)
		{
			IList<VALUE> l = null;
			if (!this.Events.ContainsKey(key))
			{
				l = new List<VALUE>();
				this.Events[key] = l;
			} else
			{
				l = this.Events[key];
			}

			l.Add(value);
			this.ReverseEvents[value] = key;
		}

		/// <summary>
		/// Removes an element from the list, regardless of its priority.
		/// </summary>
		/// <param name="value">the value to remove</param>
		/// <exception cref="KeyNotFoundException">If value is not inside the list</exception>
		private void Remove(VALUE value)
		{
			var key = this.ReverseEvents[value];
			this.ReverseEvents.Remove(value);
			var l = this.Events[key];
			l.Remove(value);
			if (l.Count == 0)
			{
				this.Events.Remove(key);
			}
		}

		/// <summary>
		/// Appends the value inside the sorted list
		/// </summary>
		/// <param name="sel">the list where we need to add the element into</param>
		/// <param name="x">a pair representing the priority and the element involved</param>
		/// <returns>sel itself</returns>
		public static SortedEventList<VALUE> operator +(SortedEventList<VALUE> sel, Pair<Priority, VALUE> x)
		{
			sel.Add((int)x.X, x.Y);
			return sel;
		}

		/// <summary>
		/// Appends the value inside the sorted list
		/// </summary>
		/// <param name="sel">the list where we need to add the element into</param>
		/// <param name="x">a pair representing the priority and the element involved</param>
		/// <returns>sel itself</returns>
		public static SortedEventList<VALUE> operator +(SortedEventList<VALUE> sel, Pair<int, VALUE> x)
		{
			sel.Add(x.X, x.Y);
			return sel;
		}

		/// <summary>
		/// Appends the value inside the sorted list
		/// </summary>
		/// <param name="sel">the list where we need to add the element into</param>
		/// <param name="x">element to add. The priority will be the one set in <see cref="DefaultPriority"/></param>
		/// <returns>sel itself</returns>
		public static SortedEventList<VALUE> operator +(SortedEventList<VALUE> sel, VALUE x)
		{
			sel.Add((int)sel.DefaultPriority, x);
			return sel;
		}

		/// <summary>
		/// Removes an element from the list, regardless of its priority.
		/// </summary>
		/// <param name="sel">the list involved</param>
		/// <param name="x">a pair containing the item to remove.</param>
		/// <exception cref="KeyNotFoundException">If value is not inside the list</exception>
		/// <returns>sel itself</returns>
		public static SortedEventList<VALUE> operator -(SortedEventList<VALUE> sel, Pair<Priority, VALUE> x)
		{
			return (sel - x.Y);
		}

		/// <summary>
		/// Removes an element from the list, regardless of its priority.
		/// </summary>
		/// <param name="sel">the list involved</param>
		/// <param name="x">a pair containing the item to remove.</param>
		/// <exception cref="KeyNotFoundException">If value is not inside the list</exception>
		/// <returns>sel itself</returns>
		public static SortedEventList<VALUE> operator -(SortedEventList<VALUE> sel, Pair<int, VALUE> x)
		{
			return (sel - x.Y);
		}

		/// <summary>
		/// Removes an element from the list, regardless of its priority.
		/// </summary>
		/// <param name="sel">the list involved</param>
		/// <param name="x">The value to remove</param>
		/// <exception cref="KeyNotFoundException">If value is not inside the list</exception>
		/// <returns>sel itself</returns>
		public static SortedEventList<VALUE> operator -(SortedEventList<VALUE> sel, VALUE x)
		{
			sel.Remove(x);
			return sel;
		}

		#region IEnumerable

		/// <summary>
		/// All the values inside the list, ordered by priority
		/// </summary>
		/// <returns></returns>
		public IEnumerator<VALUE> GetEnumerator()
		{
			foreach (var k in this.Events.Keys)
			{
				foreach (var v in this.Events[k])
				{
					yield return v;
				}
			}
		}

		/// <inheritdoc/>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		#endregion
	}
}
