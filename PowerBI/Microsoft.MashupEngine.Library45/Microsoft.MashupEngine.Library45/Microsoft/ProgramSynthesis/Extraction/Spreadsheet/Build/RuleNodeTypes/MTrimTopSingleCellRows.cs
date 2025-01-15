using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E35 RID: 3637
	public struct MTrimTopSingleCellRows : IProgramNodeBuilder, IEquatable<MTrimTopSingleCellRows>
	{
		// Token: 0x170011A8 RID: 4520
		// (get) Token: 0x0600615A RID: 24922 RVA: 0x0013F016 File Offset: 0x0013D216
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600615B RID: 24923 RVA: 0x0013F01E File Offset: 0x0013D21E
		private MTrimTopSingleCellRows(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600615C RID: 24924 RVA: 0x0013F027 File Offset: 0x0013D227
		public static MTrimTopSingleCellRows CreateUnsafe(ProgramNode node)
		{
			return new MTrimTopSingleCellRows(node);
		}

		// Token: 0x0600615D RID: 24925 RVA: 0x0013F030 File Offset: 0x0013D230
		public static MTrimTopSingleCellRows? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.MTrimTopSingleCellRows)
			{
				return null;
			}
			return new MTrimTopSingleCellRows?(MTrimTopSingleCellRows.CreateUnsafe(node));
		}

		// Token: 0x0600615E RID: 24926 RVA: 0x0013F065 File Offset: 0x0013D265
		public MTrimTopSingleCellRows(GrammarBuilders g, mTable value0)
		{
			this._node = g.Rule.MTrimTopSingleCellRows.BuildASTNode(value0.Node);
		}

		// Token: 0x0600615F RID: 24927 RVA: 0x0013F084 File Offset: 0x0013D284
		public static implicit operator mTable(MTrimTopSingleCellRows arg)
		{
			return mTable.CreateUnsafe(arg.Node);
		}

		// Token: 0x170011A9 RID: 4521
		// (get) Token: 0x06006160 RID: 24928 RVA: 0x0013F092 File Offset: 0x0013D292
		public mTable mTable
		{
			get
			{
				return mTable.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06006161 RID: 24929 RVA: 0x0013F0A6 File Offset: 0x0013D2A6
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006162 RID: 24930 RVA: 0x0013F0BC File Offset: 0x0013D2BC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006163 RID: 24931 RVA: 0x0013F0E6 File Offset: 0x0013D2E6
		public bool Equals(MTrimTopSingleCellRows other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BDF RID: 11231
		private ProgramNode _node;
	}
}
