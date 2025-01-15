using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x02001043 RID: 4163
	public struct NthChildFilter : IProgramNodeBuilder, IEquatable<NthChildFilter>
	{
		// Token: 0x170015FD RID: 5629
		// (get) Token: 0x06007B4E RID: 31566 RVA: 0x001A3146 File Offset: 0x001A1346
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007B4F RID: 31567 RVA: 0x001A314E File Offset: 0x001A134E
		private NthChildFilter(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007B50 RID: 31568 RVA: 0x001A3157 File Offset: 0x001A1357
		public static NthChildFilter CreateUnsafe(ProgramNode node)
		{
			return new NthChildFilter(node);
		}

		// Token: 0x06007B51 RID: 31569 RVA: 0x001A3160 File Offset: 0x001A1360
		public static NthChildFilter? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.NthChildFilter)
			{
				return null;
			}
			return new NthChildFilter?(NthChildFilter.CreateUnsafe(node));
		}

		// Token: 0x06007B52 RID: 31570 RVA: 0x001A3195 File Offset: 0x001A1395
		public NthChildFilter(GrammarBuilders g, idx1 value0, nodeCollection value1)
		{
			this._node = g.Rule.NthChildFilter.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06007B53 RID: 31571 RVA: 0x001A31BB File Offset: 0x001A13BB
		public static implicit operator nodeCollection(NthChildFilter arg)
		{
			return nodeCollection.CreateUnsafe(arg.Node);
		}

		// Token: 0x170015FE RID: 5630
		// (get) Token: 0x06007B54 RID: 31572 RVA: 0x001A31C9 File Offset: 0x001A13C9
		public idx1 idx1
		{
			get
			{
				return idx1.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170015FF RID: 5631
		// (get) Token: 0x06007B55 RID: 31573 RVA: 0x001A31DD File Offset: 0x001A13DD
		public nodeCollection nodeCollection
		{
			get
			{
				return nodeCollection.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06007B56 RID: 31574 RVA: 0x001A31F1 File Offset: 0x001A13F1
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007B57 RID: 31575 RVA: 0x001A3204 File Offset: 0x001A1404
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007B58 RID: 31576 RVA: 0x001A322E File Offset: 0x001A142E
		public bool Equals(NthChildFilter other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400335C RID: 13148
		private ProgramNode _node;
	}
}
