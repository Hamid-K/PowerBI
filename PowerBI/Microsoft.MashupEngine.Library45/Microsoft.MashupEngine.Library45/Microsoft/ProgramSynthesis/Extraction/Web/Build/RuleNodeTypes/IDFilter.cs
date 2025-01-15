using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x02001040 RID: 4160
	public struct IDFilter : IProgramNodeBuilder, IEquatable<IDFilter>
	{
		// Token: 0x170015F4 RID: 5620
		// (get) Token: 0x06007B2D RID: 31533 RVA: 0x001A2E52 File Offset: 0x001A1052
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007B2E RID: 31534 RVA: 0x001A2E5A File Offset: 0x001A105A
		private IDFilter(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007B2F RID: 31535 RVA: 0x001A2E63 File Offset: 0x001A1063
		public static IDFilter CreateUnsafe(ProgramNode node)
		{
			return new IDFilter(node);
		}

		// Token: 0x06007B30 RID: 31536 RVA: 0x001A2E6C File Offset: 0x001A106C
		public static IDFilter? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.IDFilter)
			{
				return null;
			}
			return new IDFilter?(IDFilter.CreateUnsafe(node));
		}

		// Token: 0x06007B31 RID: 31537 RVA: 0x001A2EA1 File Offset: 0x001A10A1
		public IDFilter(GrammarBuilders g, idName value0, nodeCollection value1)
		{
			this._node = g.Rule.IDFilter.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06007B32 RID: 31538 RVA: 0x001A2EC7 File Offset: 0x001A10C7
		public static implicit operator nodeCollection(IDFilter arg)
		{
			return nodeCollection.CreateUnsafe(arg.Node);
		}

		// Token: 0x170015F5 RID: 5621
		// (get) Token: 0x06007B33 RID: 31539 RVA: 0x001A2ED5 File Offset: 0x001A10D5
		public idName idName
		{
			get
			{
				return idName.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170015F6 RID: 5622
		// (get) Token: 0x06007B34 RID: 31540 RVA: 0x001A2EE9 File Offset: 0x001A10E9
		public nodeCollection nodeCollection
		{
			get
			{
				return nodeCollection.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06007B35 RID: 31541 RVA: 0x001A2EFD File Offset: 0x001A10FD
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007B36 RID: 31542 RVA: 0x001A2F10 File Offset: 0x001A1110
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007B37 RID: 31543 RVA: 0x001A2F3A File Offset: 0x001A113A
		public bool Equals(IDFilter other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003359 RID: 13145
		private ProgramNode _node;
	}
}
