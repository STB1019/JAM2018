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

		public Priority DefaultPriority
		{
			get; private set;
		}

		public SortedEventList(Priority defaultPriority)
		{
			this.Events = new SortedDictionary<int, IList<VALUE>>();
			this.ReverseEvents = new Dictionary<VALUE, int>();
			this.DefaultPriority = defaultPriority;
		}

		public SortedEventList() : this(Priority.MEDIUM)
		{

		}

		public ICollection<int> Keys { get
			{
				return this.Events.Keys;
			}
		}

		public void FireEvents(params object[] obj)
		{
			foreach (var k in this.Events.Keys)
			{
				foreach (var del in this.Events[k]) {
					(del as Delegate).DynamicInvoke(obj);
				}
			}
		}

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

		public static SortedEventList<VALUE> operator +(SortedEventList<VALUE> sel, Pair<Priority, VALUE> x)
		{
			sel.Add((int)x.X, x.Y);
			return sel;
		}

		public static SortedEventList<VALUE> operator +(SortedEventList<VALUE> sel, Pair<int, VALUE> x)
		{
			sel.Add(x.X, x.Y);
			return sel;
		}

		public static SortedEventList<VALUE> operator +(SortedEventList<VALUE> sel, VALUE x)
		{
			sel.Add((int)sel.DefaultPriority, x);
			return sel;
		}

		public static SortedEventList<VALUE> operator -(SortedEventList<VALUE> sel, Pair<Priority, VALUE> x)
		{
			return (sel - x.Y);
		}

		public static SortedEventList<VALUE> operator -(SortedEventList<VALUE> sel, Pair<int, VALUE> x)
		{
			return (sel - x.Y);
		}

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
