using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E1C RID: 3612
	public struct TrimBottomSingleCellRows : IProgramNodeBuilder, IEquatable<TrimBottomSingleCellRows>
	{
		// Token: 0x1700116F RID: 4463
		// (get) Token: 0x06006059 RID: 24665 RVA: 0x0013D8FA File Offset: 0x0013BAFA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600605A RID: 24666 RVA: 0x0013D902 File Offset: 0x0013BB02
		private TrimBottomSingleCellRows(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600605B RID: 24667 RVA: 0x0013D90B File Offset: 0x0013BB0B
		public static TrimBottomSingleCellRows CreateUnsafe(ProgramNode node)
		{
			return new TrimBottomSingleCellRows(node);
		}

		// Token: 0x0600605C RID: 24668 RVA: 0x0013D914 File Offset: 0x0013BB14
		public static TrimBottomSingleCellRows? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.TrimBottomSingleCellRows)
			{
				return null;
			}
			return new TrimBottomSingleCellRows?(TrimBottomSingleCellRows.CreateUnsafe(node));
		}

		// Token: 0x0600605D RID: 24669 RVA: 0x0013D949 File Offset: 0x0013BB49
		public TrimBottomSingleCellRows(GrammarBuilders g, trimTop value0)
		{
			this._node = g.Rule.TrimBottomSingleCellRows.BuildASTNode(value0.Node);
		}

		// Token: 0x0600605E RID: 24670 RVA: 0x0013D968 File Offset: 0x0013BB68
		public static implicit operator trimBottom(TrimBottomSingleCellRows arg)
		{
			return trimBottom.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001170 RID: 4464
		// (get) Token: 0x0600605F RID: 24671 RVA: 0x0013D976 File Offset: 0x0013BB76
		public trimTop trimTop
		{
			get
			{
				return trimTop.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06006060 RID: 24672 RVA: 0x0013D98A File Offset: 0x0013BB8A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006061 RID: 24673 RVA: 0x0013D9A0 File Offset: 0x0013BBA0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006062 RID: 24674 RVA: 0x0013D9CA File Offset: 0x0013BBCA
		public bool Equals(TrimBottomSingleCellRows other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BC6 RID: 11206
		private ProgramNode _node;
	}
}
