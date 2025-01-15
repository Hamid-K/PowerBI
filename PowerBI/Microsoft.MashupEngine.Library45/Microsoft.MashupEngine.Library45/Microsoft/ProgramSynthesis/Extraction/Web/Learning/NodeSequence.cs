using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Extraction.Web.Semantics;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Learning
{
	// Token: 0x020010C8 RID: 4296
	public class NodeSequence
	{
		// Token: 0x0600810E RID: 33038 RVA: 0x001AFF00 File Offset: 0x001AE100
		public NodeSequence(IEnumerable<IDomNode> nodes)
		{
			this.Nodes = nodes.ToList<IDomNode>();
			this.MaxUncommonAncestors = NodeSequence.GetMaxUncommonAncestorNodes(this.Nodes);
			this.UniqueCommonAncestor = null;
			if (this.Nodes.Count > 0)
			{
				IDomNode a = this.MaxUncommonAncestors[0].Parent;
				if (this.MaxUncommonAncestors.All((IDomNode n) => n.Parent == a))
				{
					this.UniqueCommonAncestor = a;
				}
			}
		}

		// Token: 0x170016A7 RID: 5799
		// (get) Token: 0x0600810F RID: 33039 RVA: 0x001AFF87 File Offset: 0x001AE187
		public Dictionary<IDomNode, IDomNode> MaxUncommonAncestorToNodeMap
		{
			get
			{
				if (this._maxUncommonAncestorToNodeMap == null)
				{
					this._maxUncommonAncestorToNodeMap = this.Nodes.Select((IDomNode n, int i) => new KeyValuePair<IDomNode, IDomNode>(this.MaxUncommonAncestors[i], n)).ToDictionary<IDomNode, IDomNode>();
				}
				return this._maxUncommonAncestorToNodeMap;
			}
		}

		// Token: 0x06008110 RID: 33040 RVA: 0x001AFFBC File Offset: 0x001AE1BC
		public static List<IDomNode> GetMaxUncommonAncestorNodes(IReadOnlyList<IDomNode> nodeSequence)
		{
			List<IDomNode> list = new List<IDomNode>(nodeSequence);
			int i;
			int j;
			for (i = 0; i < list.Count; i = j + 1)
			{
				IDomNode parent = list[i].Parent;
				while (parent != null && nodeSequence.All((IDomNode n) => n == nodeSequence[i] || !n.IsAncestor(parent)))
				{
					list[i] = parent;
					parent = parent.Parent;
				}
				j = i;
			}
			return list;
		}

		// Token: 0x06008111 RID: 33041 RVA: 0x001B008C File Offset: 0x001AE28C
		public bool IsAlignedWithinMaxUncommonAncestors(NodeSequence other)
		{
			bool? flag = null;
			for (int i = 0; i < other.MaxUncommonAncestors.Count; i++)
			{
				IDomNode domNode = other.MaxUncommonAncestors[i];
				if (this.MaxUncommonAncestorToNodeMap.ContainsKey(domNode))
				{
					IDomNode domNode2 = this.MaxUncommonAncestorToNodeMap[domNode];
					IDomNode domNode3 = other.Nodes[i];
					if (domNode2.Start == domNode3.Start)
					{
						return false;
					}
					bool flag2 = domNode2.Start < domNode3.Start;
					if (flag == null)
					{
						flag = new bool?(flag2);
					}
					else
					{
						bool flag3 = flag2;
						bool? flag4 = flag;
						if (!((flag3 == flag4.GetValueOrDefault()) & (flag4 != null)))
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x04003440 RID: 13376
		private Dictionary<IDomNode, IDomNode> _maxUncommonAncestorToNodeMap;

		// Token: 0x04003441 RID: 13377
		public List<IDomNode> MaxUncommonAncestors;

		// Token: 0x04003442 RID: 13378
		public List<IDomNode> Nodes;

		// Token: 0x04003443 RID: 13379
		public IDomNode UniqueCommonAncestor;
	}
}
