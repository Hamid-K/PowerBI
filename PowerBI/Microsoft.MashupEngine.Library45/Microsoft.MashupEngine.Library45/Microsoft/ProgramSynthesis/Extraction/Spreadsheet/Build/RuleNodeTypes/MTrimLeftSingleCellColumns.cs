using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E38 RID: 3640
	public struct MTrimLeftSingleCellColumns : IProgramNodeBuilder, IEquatable<MTrimLeftSingleCellColumns>
	{
		// Token: 0x170011AE RID: 4526
		// (get) Token: 0x06006178 RID: 24952 RVA: 0x0013F2C2 File Offset: 0x0013D4C2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006179 RID: 24953 RVA: 0x0013F2CA File Offset: 0x0013D4CA
		private MTrimLeftSingleCellColumns(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600617A RID: 24954 RVA: 0x0013F2D3 File Offset: 0x0013D4D3
		public static MTrimLeftSingleCellColumns CreateUnsafe(ProgramNode node)
		{
			return new MTrimLeftSingleCellColumns(node);
		}

		// Token: 0x0600617B RID: 24955 RVA: 0x0013F2DC File Offset: 0x0013D4DC
		public static MTrimLeftSingleCellColumns? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.MTrimLeftSingleCellColumns)
			{
				return null;
			}
			return new MTrimLeftSingleCellColumns?(MTrimLeftSingleCellColumns.CreateUnsafe(node));
		}

		// Token: 0x0600617C RID: 24956 RVA: 0x0013F311 File Offset: 0x0013D511
		public MTrimLeftSingleCellColumns(GrammarBuilders g, mTable value0)
		{
			this._node = g.Rule.MTrimLeftSingleCellColumns.BuildASTNode(value0.Node);
		}

		// Token: 0x0600617D RID: 24957 RVA: 0x0013F330 File Offset: 0x0013D530
		public static implicit operator mTable(MTrimLeftSingleCellColumns arg)
		{
			return mTable.CreateUnsafe(arg.Node);
		}

		// Token: 0x170011AF RID: 4527
		// (get) Token: 0x0600617E RID: 24958 RVA: 0x0013F33E File Offset: 0x0013D53E
		public mTable mTable
		{
			get
			{
				return mTable.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600617F RID: 24959 RVA: 0x0013F352 File Offset: 0x0013D552
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006180 RID: 24960 RVA: 0x0013F368 File Offset: 0x0013D568
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006181 RID: 24961 RVA: 0x0013F392 File Offset: 0x0013D592
		public bool Equals(MTrimLeftSingleCellColumns other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BE2 RID: 11234
		private ProgramNode _node;
	}
}
