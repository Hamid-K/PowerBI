using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E39 RID: 3641
	public struct MTrimRightSingleCellColumns : IProgramNodeBuilder, IEquatable<MTrimRightSingleCellColumns>
	{
		// Token: 0x170011B0 RID: 4528
		// (get) Token: 0x06006182 RID: 24962 RVA: 0x0013F3A6 File Offset: 0x0013D5A6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006183 RID: 24963 RVA: 0x0013F3AE File Offset: 0x0013D5AE
		private MTrimRightSingleCellColumns(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006184 RID: 24964 RVA: 0x0013F3B7 File Offset: 0x0013D5B7
		public static MTrimRightSingleCellColumns CreateUnsafe(ProgramNode node)
		{
			return new MTrimRightSingleCellColumns(node);
		}

		// Token: 0x06006185 RID: 24965 RVA: 0x0013F3C0 File Offset: 0x0013D5C0
		public static MTrimRightSingleCellColumns? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.MTrimRightSingleCellColumns)
			{
				return null;
			}
			return new MTrimRightSingleCellColumns?(MTrimRightSingleCellColumns.CreateUnsafe(node));
		}

		// Token: 0x06006186 RID: 24966 RVA: 0x0013F3F5 File Offset: 0x0013D5F5
		public MTrimRightSingleCellColumns(GrammarBuilders g, mTable value0)
		{
			this._node = g.Rule.MTrimRightSingleCellColumns.BuildASTNode(value0.Node);
		}

		// Token: 0x06006187 RID: 24967 RVA: 0x0013F414 File Offset: 0x0013D614
		public static implicit operator mTable(MTrimRightSingleCellColumns arg)
		{
			return mTable.CreateUnsafe(arg.Node);
		}

		// Token: 0x170011B1 RID: 4529
		// (get) Token: 0x06006188 RID: 24968 RVA: 0x0013F422 File Offset: 0x0013D622
		public mTable mTable
		{
			get
			{
				return mTable.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06006189 RID: 24969 RVA: 0x0013F436 File Offset: 0x0013D636
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600618A RID: 24970 RVA: 0x0013F44C File Offset: 0x0013D64C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600618B RID: 24971 RVA: 0x0013F476 File Offset: 0x0013D676
		public bool Equals(MTrimRightSingleCellColumns other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BE3 RID: 11235
		private ProgramNode _node;
	}
}
