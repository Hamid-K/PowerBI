using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x02001044 RID: 4164
	public struct NthLastChildFilter : IProgramNodeBuilder, IEquatable<NthLastChildFilter>
	{
		// Token: 0x17001600 RID: 5632
		// (get) Token: 0x06007B59 RID: 31577 RVA: 0x001A3242 File Offset: 0x001A1442
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007B5A RID: 31578 RVA: 0x001A324A File Offset: 0x001A144A
		private NthLastChildFilter(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007B5B RID: 31579 RVA: 0x001A3253 File Offset: 0x001A1453
		public static NthLastChildFilter CreateUnsafe(ProgramNode node)
		{
			return new NthLastChildFilter(node);
		}

		// Token: 0x06007B5C RID: 31580 RVA: 0x001A325C File Offset: 0x001A145C
		public static NthLastChildFilter? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.NthLastChildFilter)
			{
				return null;
			}
			return new NthLastChildFilter?(NthLastChildFilter.CreateUnsafe(node));
		}

		// Token: 0x06007B5D RID: 31581 RVA: 0x001A3291 File Offset: 0x001A1491
		public NthLastChildFilter(GrammarBuilders g, idx2 value0, nodeCollection value1)
		{
			this._node = g.Rule.NthLastChildFilter.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06007B5E RID: 31582 RVA: 0x001A32B7 File Offset: 0x001A14B7
		public static implicit operator nodeCollection(NthLastChildFilter arg)
		{
			return nodeCollection.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001601 RID: 5633
		// (get) Token: 0x06007B5F RID: 31583 RVA: 0x001A32C5 File Offset: 0x001A14C5
		public idx2 idx2
		{
			get
			{
				return idx2.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001602 RID: 5634
		// (get) Token: 0x06007B60 RID: 31584 RVA: 0x001A32D9 File Offset: 0x001A14D9
		public nodeCollection nodeCollection
		{
			get
			{
				return nodeCollection.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06007B61 RID: 31585 RVA: 0x001A32ED File Offset: 0x001A14ED
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007B62 RID: 31586 RVA: 0x001A3300 File Offset: 0x001A1500
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007B63 RID: 31587 RVA: 0x001A332A File Offset: 0x001A152A
		public bool Equals(NthLastChildFilter other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400335D RID: 13149
		private ProgramNode _node;
	}
}
