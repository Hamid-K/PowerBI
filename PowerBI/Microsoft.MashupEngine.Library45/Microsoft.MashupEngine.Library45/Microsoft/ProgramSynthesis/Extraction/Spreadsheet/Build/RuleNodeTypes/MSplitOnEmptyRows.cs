using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E3C RID: 3644
	public struct MSplitOnEmptyRows : IProgramNodeBuilder, IEquatable<MSplitOnEmptyRows>
	{
		// Token: 0x170011B6 RID: 4534
		// (get) Token: 0x060061A0 RID: 24992 RVA: 0x0013F652 File Offset: 0x0013D852
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060061A1 RID: 24993 RVA: 0x0013F65A File Offset: 0x0013D85A
		private MSplitOnEmptyRows(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060061A2 RID: 24994 RVA: 0x0013F663 File Offset: 0x0013D863
		public static MSplitOnEmptyRows CreateUnsafe(ProgramNode node)
		{
			return new MSplitOnEmptyRows(node);
		}

		// Token: 0x060061A3 RID: 24995 RVA: 0x0013F66C File Offset: 0x0013D86C
		public static MSplitOnEmptyRows? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.MSplitOnEmptyRows)
			{
				return null;
			}
			return new MSplitOnEmptyRows?(MSplitOnEmptyRows.CreateUnsafe(node));
		}

		// Token: 0x060061A4 RID: 24996 RVA: 0x0013F6A1 File Offset: 0x0013D8A1
		public MSplitOnEmptyRows(GrammarBuilders g, mTable value0)
		{
			this._node = g.Rule.MSplitOnEmptyRows.BuildASTNode(value0.Node);
		}

		// Token: 0x060061A5 RID: 24997 RVA: 0x0013F6C0 File Offset: 0x0013D8C0
		public static implicit operator mSection(MSplitOnEmptyRows arg)
		{
			return mSection.CreateUnsafe(arg.Node);
		}

		// Token: 0x170011B7 RID: 4535
		// (get) Token: 0x060061A6 RID: 24998 RVA: 0x0013F6CE File Offset: 0x0013D8CE
		public mTable mTable
		{
			get
			{
				return mTable.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060061A7 RID: 24999 RVA: 0x0013F6E2 File Offset: 0x0013D8E2
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060061A8 RID: 25000 RVA: 0x0013F6F8 File Offset: 0x0013D8F8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060061A9 RID: 25001 RVA: 0x0013F722 File Offset: 0x0013D922
		public bool Equals(MSplitOnEmptyRows other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BE6 RID: 11238
		private ProgramNode _node;
	}
}
