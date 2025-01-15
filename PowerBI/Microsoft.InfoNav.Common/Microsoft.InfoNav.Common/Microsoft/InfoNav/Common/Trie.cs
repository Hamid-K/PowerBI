using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x02000075 RID: 117
	internal sealed class Trie<TKey, TValue>
	{
		// Token: 0x0600043A RID: 1082 RVA: 0x0000AFF5 File Offset: 0x000091F5
		internal Trie(IEqualityComparer<TKey> comparer = null, Func<ISet<TValue>> valueSetCreator = null)
		{
			this._valueSetCreator = valueSetCreator ?? Trie<TKey, TValue>._defaultValueSetCreator;
			this._rootNode = new Trie<TKey, TValue>.Node(comparer);
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x0000B019 File Offset: 0x00009219
		internal Trie<TKey, TValue>.Node RootNode
		{
			get
			{
				return this._rootNode;
			}
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0000B024 File Offset: 0x00009224
		internal void Insert(IEnumerable<TKey> keys, TValue value, bool includeValueOnAllTransitions = false)
		{
			Trie<TKey, TValue>.Node node = this._rootNode;
			foreach (TKey tkey in keys)
			{
				node = node.GetOrAdd(tkey);
				if (includeValueOnAllTransitions)
				{
					node.AddValue(value, this._valueSetCreator);
				}
			}
			if (!includeValueOnAllTransitions)
			{
				node.AddValue(value, this._valueSetCreator);
			}
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0000B094 File Offset: 0x00009294
		internal ISet<TValue> Find(IEnumerable<TKey> sequence)
		{
			Trie<TKey, TValue>.Node rootNode = this._rootNode;
			foreach (TKey tkey in sequence)
			{
				if (!rootNode.TryGetNext(tkey, out rootNode))
				{
					return ReadOnlySet<TValue>.Empty;
				}
			}
			return rootNode.Values;
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0000B0F8 File Offset: 0x000092F8
		internal ISet<TValue> FindFirstValues(IEnumerable<TKey> sequence)
		{
			Trie<TKey, TValue>.Node rootNode = this._rootNode;
			foreach (TKey tkey in sequence)
			{
				if (!rootNode.TryGetNext(tkey, out rootNode))
				{
					return ReadOnlySet<TValue>.Empty;
				}
				if (rootNode.Values.Count > 0)
				{
					return rootNode.Values;
				}
			}
			return rootNode.Values;
		}

		// Token: 0x040000EE RID: 238
		private static readonly Func<ISet<TValue>> _defaultValueSetCreator = () => new HashSet<TValue>();

		// Token: 0x040000EF RID: 239
		private readonly Trie<TKey, TValue>.Node _rootNode;

		// Token: 0x040000F0 RID: 240
		private readonly Func<ISet<TValue>> _valueSetCreator;

		// Token: 0x020000CA RID: 202
		internal sealed class Node
		{
			// Token: 0x06000602 RID: 1538 RVA: 0x0000FAB1 File Offset: 0x0000DCB1
			internal Node(IEqualityComparer<TKey> comparer)
			{
				this._children = new Dictionary<TKey, Trie<TKey, TValue>.Node>(comparer);
			}

			// Token: 0x170000D5 RID: 213
			// (get) Token: 0x06000603 RID: 1539 RVA: 0x0000FAC8 File Offset: 0x0000DCC8
			internal ISet<TValue> Values
			{
				get
				{
					if (this._values != null)
					{
						return this._values;
					}
					return ReadOnlySet<TValue>.Empty;
				}
			}

			// Token: 0x06000604 RID: 1540 RVA: 0x0000FAEB File Offset: 0x0000DCEB
			internal bool TryGetNext(TKey key, out Trie<TKey, TValue>.Node next)
			{
				return this._children.TryGetValue(key, out next);
			}

			// Token: 0x06000605 RID: 1541 RVA: 0x0000FAFC File Offset: 0x0000DCFC
			internal Trie<TKey, TValue>.Node GetOrAdd(TKey key)
			{
				Trie<TKey, TValue>.Node node;
				if (!this._children.TryGetValue(key, out node))
				{
					node = new Trie<TKey, TValue>.Node(this._children.Comparer);
					this._children.Add(key, node);
				}
				return node;
			}

			// Token: 0x06000606 RID: 1542 RVA: 0x0000FB38 File Offset: 0x0000DD38
			internal void AddValue(TValue value, Func<ISet<TValue>> valueSetCreator)
			{
				if (this._values == null)
				{
					this._values = valueSetCreator();
				}
				this._values.Add(value);
			}

			// Token: 0x04000210 RID: 528
			private readonly Dictionary<TKey, Trie<TKey, TValue>.Node> _children;

			// Token: 0x04000211 RID: 529
			private ISet<TValue> _values;
		}
	}
}
