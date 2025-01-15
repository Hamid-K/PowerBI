using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Semantics
{
	// Token: 0x02001179 RID: 4473
	public class NodeCollection
	{
		// Token: 0x170016E6 RID: 5862
		// (get) Token: 0x0600850A RID: 34058 RVA: 0x001BFD75 File Offset: 0x001BDF75
		public HashSet<IDomNode> Set { get; }

		// Token: 0x170016E7 RID: 5863
		// (get) Token: 0x0600850B RID: 34059 RVA: 0x001BFD80 File Offset: 0x001BDF80
		public IDomNode[] SortedSet
		{
			get
			{
				if (this._sortedSet == null)
				{
					this._sortedSet = this.Set.OrderBy((IDomNode n) => n.Start).ToArray<IDomNode>();
				}
				return this._sortedSet;
			}
		}

		// Token: 0x170016E8 RID: 5864
		// (get) Token: 0x0600850C RID: 34060 RVA: 0x001BFDD0 File Offset: 0x001BDFD0
		public int[] SortedNodeIndices
		{
			get
			{
				if (this._sortedNodeIndices == null)
				{
					this._sortedNodeIndices = this.SortedSet.Select((IDomNode n) => n.Start).ToArray<int>();
				}
				return this._sortedNodeIndices;
			}
		}

		// Token: 0x170016E9 RID: 5865
		// (get) Token: 0x0600850D RID: 34061 RVA: 0x001BFE20 File Offset: 0x001BE020
		public NodeCollection MaxRowAncestors
		{
			get
			{
				if (this._maxRowAncestors == null)
				{
					this._maxRowAncestors = this.GetMaxRowAncestors();
				}
				return this._maxRowAncestors;
			}
		}

		// Token: 0x0600850E RID: 34062 RVA: 0x001BFE42 File Offset: 0x001BE042
		public NodeCollection()
		{
			this.Set = new HashSet<IDomNode>(Enumerable.Empty<IDomNode>());
		}

		// Token: 0x0600850F RID: 34063 RVA: 0x001BFE5A File Offset: 0x001BE05A
		public NodeCollection(IEnumerable<IDomNode> elements)
		{
			this.Set = new HashSet<IDomNode>(elements);
		}

		// Token: 0x06008510 RID: 34064 RVA: 0x001BFE6E File Offset: 0x001BE06E
		public NodeCollection Intersect(NodeCollection other)
		{
			return new NodeCollection(this.Set.Intersect(other.Set));
		}

		// Token: 0x06008511 RID: 34065 RVA: 0x001BFE86 File Offset: 0x001BE086
		public NodeCollection Union(NodeCollection other)
		{
			return new NodeCollection(this.Set.Union(other.Set));
		}

		// Token: 0x06008512 RID: 34066 RVA: 0x001BFE9E File Offset: 0x001BE09E
		public static bool IsInterleaving(NodeCollection s1, NodeCollection s2)
		{
			return NodeCollection.IsInterleaving(s1.SortedNodeIndices, s2.SortedNodeIndices);
		}

		// Token: 0x06008513 RID: 34067 RVA: 0x001BFEB4 File Offset: 0x001BE0B4
		private static bool IsInterleaving(int[] s1, int[] s2)
		{
			if (s1.Length == 0 || s2.Length == 0 || s1 == s2)
			{
				return true;
			}
			int[] array;
			int[] array2;
			if (s1.Length < s2.Length)
			{
				array = s1;
				array2 = s2;
			}
			else
			{
				array = s2;
				array2 = s1;
			}
			int num = 0;
			bool flag = array[0] > array2[0];
			for (int i = 0; i < array.Length; i++)
			{
				if (i > 0 && array[i] <= array2[num])
				{
					return false;
				}
				while (array[i] > array2[num])
				{
					num++;
					if (num == array2.Length)
					{
						return flag && i == array.Length - 1;
					}
				}
				if (array[i] == array2[num])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06008514 RID: 34068 RVA: 0x001BFF38 File Offset: 0x001BE138
		public static bool IsChildSequence(NodeCollection s1, NodeCollection s2)
		{
			if (s1.SortedSet.Length != s2.SortedSet.Length)
			{
				return false;
			}
			for (int i = 0; i < s1.SortedSet.Length; i++)
			{
				if (!s1.SortedSet[i].Contains(s2.SortedSet[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06008515 RID: 34069 RVA: 0x001BFF88 File Offset: 0x001BE188
		private NodeCollection GetMaxRowAncestors()
		{
			List<IDomNode> list = new List<IDomNode>();
			for (int i = 0; i < this.SortedSet.Length; i++)
			{
				IDomNode domNode = ((i > 0) ? this.SortedSet[i - 1] : null);
				IDomNode domNode2 = ((i < this.SortedSet.Length - 1) ? this.SortedSet[i + 1] : null);
				IDomNode domNode3 = this.SortedSet[i];
				if ((domNode != null && domNode3.Contains(domNode)) || (domNode2 != null && domNode3.Contains(domNode2)))
				{
					return null;
				}
				while (domNode3.Parent != null)
				{
					if ((domNode != null && domNode3.Parent.Contains(domNode)) || (domNode2 != null && domNode3.Parent.Contains(domNode2)))
					{
						list.Add(domNode3);
						break;
					}
					domNode3 = domNode3.Parent;
				}
			}
			return new NodeCollection(list);
		}

		// Token: 0x06008516 RID: 34070 RVA: 0x001C0050 File Offset: 0x001BE250
		public bool SatisfiesRowAncestorContainment(NodeCollection s)
		{
			if (this.MaxRowAncestors == null || s.MaxRowAncestors == null)
			{
				return false;
			}
			if (this.Set.Count == s.Set.Count)
			{
				return this.MaxRowAncestors.Equals(s.MaxRowAncestors);
			}
			return NodeCollection.SatisfiesRowAncestorContainment(this.SortedSet, s.MaxRowAncestors.SortedSet);
		}

		// Token: 0x06008517 RID: 34071 RVA: 0x001C00BC File Offset: 0x001BE2BC
		private static bool SatisfiesRowAncestorContainment(IDomNode[] nodes, IDomNode[] rowAncestors)
		{
			if (rowAncestors.Length == 0)
			{
				return false;
			}
			int i = 0;
			foreach (IDomNode domNode in nodes)
			{
				bool flag = false;
				while (i < rowAncestors.Length)
				{
					IDomNode domNode2 = rowAncestors[i];
					if (domNode2 != null && domNode2.Contains(domNode))
					{
						flag = true;
						break;
					}
					i++;
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06008518 RID: 34072 RVA: 0x001C0113 File Offset: 0x001BE313
		public bool Equals(NodeCollection other)
		{
			return this.Set.SetEquals(other.Set);
		}

		// Token: 0x06008519 RID: 34073 RVA: 0x001C0128 File Offset: 0x001BE328
		public override int GetHashCode()
		{
			if (this._hashCode != null)
			{
				return this._hashCode.Value;
			}
			int num = 0;
			foreach (IDomNode domNode in this.Set)
			{
				num ^= EqualityComparer<IDomNode>.Default.GetHashCode(domNode);
			}
			this._hashCode = new int?(num);
			return num;
		}

		// Token: 0x0600851A RID: 34074 RVA: 0x001C01AC File Offset: 0x001BE3AC
		public override bool Equals(object obj)
		{
			NodeCollection nodeCollection = obj as NodeCollection;
			return !(nodeCollection == null) && nodeCollection.Equals(this);
		}

		// Token: 0x0600851B RID: 34075 RVA: 0x001C01D2 File Offset: 0x001BE3D2
		public static bool operator ==(NodeCollection a, NodeCollection b)
		{
			return a == b || (a != null && b != null && a.Set.SetEquals(b.Set));
		}

		// Token: 0x0600851C RID: 34076 RVA: 0x001C01F3 File Offset: 0x001BE3F3
		public static bool operator !=(NodeCollection a, NodeCollection b)
		{
			return !(a == b);
		}

		// Token: 0x040036E1 RID: 14049
		private int? _hashCode;

		// Token: 0x040036E2 RID: 14050
		private IDomNode[] _sortedSet;

		// Token: 0x040036E3 RID: 14051
		private int[] _sortedNodeIndices;

		// Token: 0x040036E4 RID: 14052
		private NodeCollection _maxRowAncestors;
	}
}
