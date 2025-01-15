using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E3B RID: 3643
	public struct MTrimBottomDoubleCellRows : IProgramNodeBuilder, IEquatable<MTrimBottomDoubleCellRows>
	{
		// Token: 0x170011B4 RID: 4532
		// (get) Token: 0x06006196 RID: 24982 RVA: 0x0013F56E File Offset: 0x0013D76E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006197 RID: 24983 RVA: 0x0013F576 File Offset: 0x0013D776
		private MTrimBottomDoubleCellRows(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006198 RID: 24984 RVA: 0x0013F57F File Offset: 0x0013D77F
		public static MTrimBottomDoubleCellRows CreateUnsafe(ProgramNode node)
		{
			return new MTrimBottomDoubleCellRows(node);
		}

		// Token: 0x06006199 RID: 24985 RVA: 0x0013F588 File Offset: 0x0013D788
		public static MTrimBottomDoubleCellRows? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.MTrimBottomDoubleCellRows)
			{
				return null;
			}
			return new MTrimBottomDoubleCellRows?(MTrimBottomDoubleCellRows.CreateUnsafe(node));
		}

		// Token: 0x0600619A RID: 24986 RVA: 0x0013F5BD File Offset: 0x0013D7BD
		public MTrimBottomDoubleCellRows(GrammarBuilders g, mTable value0)
		{
			this._node = g.Rule.MTrimBottomDoubleCellRows.BuildASTNode(value0.Node);
		}

		// Token: 0x0600619B RID: 24987 RVA: 0x0013F5DC File Offset: 0x0013D7DC
		public static implicit operator mTable(MTrimBottomDoubleCellRows arg)
		{
			return mTable.CreateUnsafe(arg.Node);
		}

		// Token: 0x170011B5 RID: 4533
		// (get) Token: 0x0600619C RID: 24988 RVA: 0x0013F5EA File Offset: 0x0013D7EA
		public mTable mTable
		{
			get
			{
				return mTable.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600619D RID: 24989 RVA: 0x0013F5FE File Offset: 0x0013D7FE
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600619E RID: 24990 RVA: 0x0013F614 File Offset: 0x0013D814
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600619F RID: 24991 RVA: 0x0013F63E File Offset: 0x0013D83E
		public bool Equals(MTrimBottomDoubleCellRows other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BE5 RID: 11237
		private ProgramNode _node;
	}
}
