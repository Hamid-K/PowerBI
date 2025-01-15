using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x0200103B RID: 4155
	public struct ColumnSequence : IProgramNodeBuilder, IEquatable<ColumnSequence>
	{
		// Token: 0x170015E8 RID: 5608
		// (get) Token: 0x06007AF9 RID: 31481 RVA: 0x001A29AE File Offset: 0x001A0BAE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007AFA RID: 31482 RVA: 0x001A29B6 File Offset: 0x001A0BB6
		private ColumnSequence(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007AFB RID: 31483 RVA: 0x001A29BF File Offset: 0x001A0BBF
		public static ColumnSequence CreateUnsafe(ProgramNode node)
		{
			return new ColumnSequence(node);
		}

		// Token: 0x06007AFC RID: 31484 RVA: 0x001A29C8 File Offset: 0x001A0BC8
		public static ColumnSequence? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.ColumnSequence)
			{
				return null;
			}
			return new ColumnSequence?(ColumnSequence.CreateUnsafe(node));
		}

		// Token: 0x06007AFD RID: 31485 RVA: 0x001A29FD File Offset: 0x001A0BFD
		public ColumnSequence(GrammarBuilders g, columnSelectors value0, resultSequence value1)
		{
			this._node = g.Rule.ColumnSequence.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06007AFE RID: 31486 RVA: 0x001A2A23 File Offset: 0x001A0C23
		public static implicit operator columnSelectors(ColumnSequence arg)
		{
			return columnSelectors.CreateUnsafe(arg.Node);
		}

		// Token: 0x170015E9 RID: 5609
		// (get) Token: 0x06007AFF RID: 31487 RVA: 0x001A2A31 File Offset: 0x001A0C31
		public columnSelectors columnSelectors
		{
			get
			{
				return columnSelectors.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170015EA RID: 5610
		// (get) Token: 0x06007B00 RID: 31488 RVA: 0x001A2A45 File Offset: 0x001A0C45
		public resultSequence resultSequence
		{
			get
			{
				return resultSequence.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06007B01 RID: 31489 RVA: 0x001A2A59 File Offset: 0x001A0C59
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007B02 RID: 31490 RVA: 0x001A2A6C File Offset: 0x001A0C6C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007B03 RID: 31491 RVA: 0x001A2A96 File Offset: 0x001A0C96
		public bool Equals(ColumnSequence other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003354 RID: 13140
		private ProgramNode _node;
	}
}
