using System;
using System.Collections.Generic;

namespace Scripts.Game.Model
{
	/// <summary>
	/// Represents a class storing some values indexed by strings.
	/// </summary>
	public abstract class AbstractTypes<TYPE>
	{
		/// <summary>
		/// The values stored in the class
		/// </summary>
		private readonly IList<TYPE> typesList;
		/// <summary>
		/// Same as <see cref="AbstractTypes.typesList"/>, but indexed by string
		/// </summary>
		private readonly IDictionary<string, TYPE> types;

		internal AbstractTypes() {
			this.types = new SortedDictionary<string, TYPE>();
			this.typesList = new List<TYPE> ();
		}

		/// <summary>
		/// Procedure to call each time we need to build a new TYPE.
		/// </summary>
		/// <returns>The type to build</returns>
		/// <param name="key">the key we're going to use to index the newly built TYPE</param>
		protected abstract TYPE BuildType(string key);

		/// <summary>
		/// Every TYPE stored in the class
		/// </summary>
		/// <value>The AL.</value>
		public IList<TYPE> ALL {
			get {
				return this.typesList;
			}
		}

		/// <summary>
		/// Retrieve the n-th TYPE stored in this instance
		/// </summary>
		/// <param name="indexer">Indexer.</param>
		public TYPE this[int indexer]
		{
			get
			{
				return this.typesList[indexer];
			}
		}

		/// <summary>
		/// Get the value stored in this instance associated to the given key
		/// </summary>
		/// <param name="indexer">the key whose value we want to fetch</param>
		public TYPE this[string indexer]
		{
			get {
				return this.types[indexer];
			}
		}

		/// <summary>
		/// Like <see cref="AbstractTypes.this[string indexer]"/> but we will first compute the name of
		/// the given type
		/// </summary>
		/// <param name="indexer">the type whose name will be the key of the object to retrieve</param>
		public TYPE this[Type indexer]
		{
			get {
				return this.types [indexer.Name];
			}
		}

		/// <summary>
		/// Adds a new type indexed by the given string.
		/// We will call <see cref="AbstractTypes.BuildType"/> to build the TYPE itself
		/// </summary>
		/// <param name="name">The key the object built will be indexed with</param>
		public void Add(string name) {
			this.types [name] = this.BuildType (name);
			this.typesList.Add (this.types [name]);
		}

		/// <summary>
		/// Like <see cref="AbstractTypes.Add(string name)"/> but we will use a Type instead
		/// </summary>
		/// 
		/// <example>
		/// For exampel you can do somehting like this:
		/// <code>
		/// PropTypes.Add(typeof(Fountain));
		/// </code>
		/// </example>
		/// 
		/// <param name="t">the type we need to convert to a string; we will use the string conversion as key</param>
		public void Add(Type t) {
			this.Add (t.Name);
		}

		/// <summary>
		/// Like <see cref="AbstractTypes.Contains(string type)"/> but it will use a Type to fetch the key to check
		/// </summary>
		/// <param name="type">the type whose Name we will pick as key</param>
		public bool Contains(Type type) {
			return this.Contains (type.Name);
		}

		/// <summary>
		/// Check if there is an object in the structure indexed by the given key
		/// </summary>
		/// <param name="type">the key to check</param>
		public bool Contains(string type) {
			return this.types.ContainsKey (type);
		}

	}
}

