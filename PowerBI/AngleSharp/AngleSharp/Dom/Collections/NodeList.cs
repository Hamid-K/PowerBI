using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace AngleSharp.Dom.Collections
{
	// Token: 0x020003FF RID: 1023
	internal sealed class NodeList : INodeList, IEnumerable<INode>, IEnumerable, IMarkupFormattable
	{
		// Token: 0x06002083 RID: 8323 RVA: 0x000569EC File Offset: 0x00054BEC
		internal NodeList()
		{
			this._entries = new List<Node>();
		}

		// Token: 0x17000A3B RID: 2619
		public Node this[int index]
		{
			get
			{
				return this._entries[index];
			}
			set
			{
				this._entries[index] = value;
			}
		}

		// Token: 0x17000A3C RID: 2620
		INode INodeList.this[int index]
		{
			get
			{
				return this[index];
			}
		}

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x06002087 RID: 8327 RVA: 0x00056A25 File Offset: 0x00054C25
		public int Length
		{
			get
			{
				return this._entries.Count;
			}
		}

		// Token: 0x06002088 RID: 8328 RVA: 0x00056A32 File Offset: 0x00054C32
		internal void Add(Node node)
		{
			this._entries.Add(node);
		}

		// Token: 0x06002089 RID: 8329 RVA: 0x00056A40 File Offset: 0x00054C40
		internal void AddRange(NodeList nodeList)
		{
			this._entries.AddRange(nodeList._entries);
		}

		// Token: 0x0600208A RID: 8330 RVA: 0x00056A53 File Offset: 0x00054C53
		internal void Insert(int index, Node node)
		{
			this._entries.Insert(index, node);
		}

		// Token: 0x0600208B RID: 8331 RVA: 0x00056A62 File Offset: 0x00054C62
		internal void Remove(Node node)
		{
			this._entries.Remove(node);
		}

		// Token: 0x0600208C RID: 8332 RVA: 0x00056A71 File Offset: 0x00054C71
		internal void RemoveAt(int index)
		{
			this._entries.RemoveAt(index);
		}

		// Token: 0x0600208D RID: 8333 RVA: 0x00056A7F File Offset: 0x00054C7F
		internal bool Contains(Node node)
		{
			return this._entries.Contains(node);
		}

		// Token: 0x0600208E RID: 8334 RVA: 0x00056A90 File Offset: 0x00054C90
		public void ToHtml(TextWriter writer, IMarkupFormatter formatter)
		{
			for (int i = 0; i < this._entries.Count; i++)
			{
				this._entries[i].ToHtml(writer, formatter);
			}
		}

		// Token: 0x0600208F RID: 8335 RVA: 0x00056AC6 File Offset: 0x00054CC6
		public IEnumerator<INode> GetEnumerator()
		{
			return this._entries.GetEnumerator();
		}

		// Token: 0x06002090 RID: 8336 RVA: 0x00056AC6 File Offset: 0x00054CC6
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._entries.GetEnumerator();
		}

		// Token: 0x04000D1A RID: 3354
		private readonly List<Node> _entries;

		// Token: 0x04000D1B RID: 3355
		internal static readonly NodeList Empty = new NodeList();
	}
}
