using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E1B RID: 3611
	public struct TrimLeftSingleCellColumns : IProgramNodeBuilder, IEquatable<TrimLeftSingleCellColumns>
	{
		// Token: 0x1700116D RID: 4461
		// (get) Token: 0x0600604F RID: 24655 RVA: 0x0013D816 File Offset: 0x0013BA16
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006050 RID: 24656 RVA: 0x0013D81E File Offset: 0x0013BA1E
		private TrimLeftSingleCellColumns(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006051 RID: 24657 RVA: 0x0013D827 File Offset: 0x0013BA27
		public static TrimLeftSingleCellColumns CreateUnsafe(ProgramNode node)
		{
			return new TrimLeftSingleCellColumns(node);
		}

		// Token: 0x06006052 RID: 24658 RVA: 0x0013D830 File Offset: 0x0013BA30
		public static TrimLeftSingleCellColumns? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.TrimLeftSingleCellColumns)
			{
				return null;
			}
			return new TrimLeftSingleCellColumns?(TrimLeftSingleCellColumns.CreateUnsafe(node));
		}

		// Token: 0x06006053 RID: 24659 RVA: 0x0013D865 File Offset: 0x0013BA65
		public TrimLeftSingleCellColumns(GrammarBuilders g, trimBottom value0)
		{
			this._node = g.Rule.TrimLeftSingleCellColumns.BuildASTNode(value0.Node);
		}

		// Token: 0x06006054 RID: 24660 RVA: 0x0013D884 File Offset: 0x0013BA84
		public static implicit operator trimLeft(TrimLeftSingleCellColumns arg)
		{
			return trimLeft.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700116E RID: 4462
		// (get) Token: 0x06006055 RID: 24661 RVA: 0x0013D892 File Offset: 0x0013BA92
		public trimBottom trimBottom
		{
			get
			{
				return trimBottom.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06006056 RID: 24662 RVA: 0x0013D8A6 File Offset: 0x0013BAA6
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006057 RID: 24663 RVA: 0x0013D8BC File Offset: 0x0013BABC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006058 RID: 24664 RVA: 0x0013D8E6 File Offset: 0x0013BAE6
		public bool Equals(TrimLeftSingleCellColumns other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BC5 RID: 11205
		private ProgramNode _node;
	}
}
