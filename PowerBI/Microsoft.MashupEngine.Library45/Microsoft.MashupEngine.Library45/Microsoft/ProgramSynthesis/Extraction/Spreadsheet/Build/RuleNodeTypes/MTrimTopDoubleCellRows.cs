using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E3A RID: 3642
	public struct MTrimTopDoubleCellRows : IProgramNodeBuilder, IEquatable<MTrimTopDoubleCellRows>
	{
		// Token: 0x170011B2 RID: 4530
		// (get) Token: 0x0600618C RID: 24972 RVA: 0x0013F48A File Offset: 0x0013D68A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600618D RID: 24973 RVA: 0x0013F492 File Offset: 0x0013D692
		private MTrimTopDoubleCellRows(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600618E RID: 24974 RVA: 0x0013F49B File Offset: 0x0013D69B
		public static MTrimTopDoubleCellRows CreateUnsafe(ProgramNode node)
		{
			return new MTrimTopDoubleCellRows(node);
		}

		// Token: 0x0600618F RID: 24975 RVA: 0x0013F4A4 File Offset: 0x0013D6A4
		public static MTrimTopDoubleCellRows? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.MTrimTopDoubleCellRows)
			{
				return null;
			}
			return new MTrimTopDoubleCellRows?(MTrimTopDoubleCellRows.CreateUnsafe(node));
		}

		// Token: 0x06006190 RID: 24976 RVA: 0x0013F4D9 File Offset: 0x0013D6D9
		public MTrimTopDoubleCellRows(GrammarBuilders g, mTable value0)
		{
			this._node = g.Rule.MTrimTopDoubleCellRows.BuildASTNode(value0.Node);
		}

		// Token: 0x06006191 RID: 24977 RVA: 0x0013F4F8 File Offset: 0x0013D6F8
		public static implicit operator mTable(MTrimTopDoubleCellRows arg)
		{
			return mTable.CreateUnsafe(arg.Node);
		}

		// Token: 0x170011B3 RID: 4531
		// (get) Token: 0x06006192 RID: 24978 RVA: 0x0013F506 File Offset: 0x0013D706
		public mTable mTable
		{
			get
			{
				return mTable.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06006193 RID: 24979 RVA: 0x0013F51A File Offset: 0x0013D71A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006194 RID: 24980 RVA: 0x0013F530 File Offset: 0x0013D730
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006195 RID: 24981 RVA: 0x0013F55A File Offset: 0x0013D75A
		public bool Equals(MTrimTopDoubleCellRows other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BE4 RID: 11236
		private ProgramNode _node;
	}
}
