using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001BF8 RID: 7160
	public abstract class Graph<TKey, TNode, TEdge> where TKey : IEquatable<TKey>
	{
		// Token: 0x0600B2B5 RID: 45749 RVA: 0x0024610C File Offset: 0x0024430C
		public Graph()
		{
			this.nodes = new Dictionary<TKey, TNode>();
		}

		// Token: 0x17002CD9 RID: 11481
		// (get) Token: 0x0600B2B6 RID: 45750 RVA: 0x0024611F File Offset: 0x0024431F
		public IEnumerable<TKey> Keys
		{
			get
			{
				return this.nodes.Keys;
			}
		}

		// Token: 0x0600B2B7 RID: 45751 RVA: 0x0024612C File Offset: 0x0024432C
		public bool ContainsNode(TKey key)
		{
			return this.nodes.ContainsKey(key);
		}

		// Token: 0x0600B2B8 RID: 45752 RVA: 0x0024613C File Offset: 0x0024433C
		public TNode GetNode(TKey key)
		{
			TNode tnode;
			if (this.nodes.TryGetValue(key, out tnode))
			{
				return tnode;
			}
			throw new KeyNotFoundException();
		}

		// Token: 0x0600B2B9 RID: 45753 RVA: 0x00246160 File Offset: 0x00244360
		public TEdge GetEdge(TKey fromKey, TKey toKey)
		{
			TEdge tedge;
			if (this.TryGetEdge(fromKey, toKey, out tedge))
			{
				return tedge;
			}
			throw new KeyNotFoundException();
		}

		// Token: 0x0600B2BA RID: 45754 RVA: 0x00246180 File Offset: 0x00244380
		public bool TryGetEdge(TKey fromKey, TKey toKey, out TEdge edge)
		{
			Dictionary<TKey, TEdge> dictionary;
			if (this.fromToEdges != null && this.fromToEdges.TryGetValue(fromKey, out dictionary) && dictionary.TryGetValue(toKey, out edge))
			{
				return true;
			}
			edge = default(TEdge);
			return false;
		}

		// Token: 0x0600B2BB RID: 45755 RVA: 0x002461BC File Offset: 0x002443BC
		public IEnumerable<TKey> KeysFrom(TKey fromKey)
		{
			Dictionary<TKey, TEdge> dictionary;
			if (this.fromToEdges != null && this.fromToEdges.TryGetValue(fromKey, out dictionary))
			{
				return dictionary.Keys;
			}
			return EmptyArray<TKey>.Instance;
		}

		// Token: 0x0600B2BC RID: 45756 RVA: 0x002461F0 File Offset: 0x002443F0
		public IEnumerable<TKey> KeysTo(TKey toKey)
		{
			Dictionary<TKey, TEdge> dictionary;
			if (this.toFromEdges != null && this.toFromEdges.TryGetValue(toKey, out dictionary))
			{
				return dictionary.Keys;
			}
			return EmptyArray<TKey>.Instance;
		}

		// Token: 0x0600B2BD RID: 45757 RVA: 0x00246224 File Offset: 0x00244424
		public void Add(TKey key, TNode node)
		{
			TNode tnode;
			if (this.nodes.TryGetValue(key, out tnode))
			{
				tnode = this.Merge(tnode, node);
			}
			else
			{
				tnode = node;
			}
			this.nodes[key] = tnode;
		}

		// Token: 0x0600B2BE RID: 45758 RVA: 0x0024625C File Offset: 0x0024445C
		public void Add(TKey fromKey, TKey toKey, TEdge edge)
		{
			if (!this.nodes.ContainsKey(fromKey) || !this.nodes.ContainsKey(toKey))
			{
				throw new InvalidOperationException();
			}
			this.Add(ref this.fromToEdges, fromKey, toKey, edge);
			this.Add(ref this.toFromEdges, toKey, fromKey, edge);
		}

		// Token: 0x0600B2BF RID: 45759
		protected abstract TNode Merge(TNode node1, TNode node2);

		// Token: 0x0600B2C0 RID: 45760
		protected abstract TEdge Merge(TEdge edge1, TEdge edge2);

		// Token: 0x0600B2C1 RID: 45761 RVA: 0x002462AC File Offset: 0x002444AC
		private void Add(ref Dictionary<TKey, Dictionary<TKey, TEdge>> key1Key2Edges, TKey key1, TKey key2, TEdge edge)
		{
			if (key1Key2Edges == null)
			{
				key1Key2Edges = new Dictionary<TKey, Dictionary<TKey, TEdge>>();
			}
			Dictionary<TKey, TEdge> dictionary;
			if (!key1Key2Edges.TryGetValue(key1, out dictionary))
			{
				dictionary = new Dictionary<TKey, TEdge>();
				key1Key2Edges.Add(key1, dictionary);
			}
			TEdge tedge;
			if (dictionary.TryGetValue(key2, out tedge))
			{
				tedge = this.Merge(tedge, edge);
			}
			else
			{
				tedge = edge;
			}
			dictionary[key2] = tedge;
		}

		// Token: 0x04005B4F RID: 23375
		private readonly Dictionary<TKey, TNode> nodes;

		// Token: 0x04005B50 RID: 23376
		private Dictionary<TKey, Dictionary<TKey, TEdge>> fromToEdges;

		// Token: 0x04005B51 RID: 23377
		private Dictionary<TKey, Dictionary<TKey, TEdge>> toFromEdges;
	}
}
