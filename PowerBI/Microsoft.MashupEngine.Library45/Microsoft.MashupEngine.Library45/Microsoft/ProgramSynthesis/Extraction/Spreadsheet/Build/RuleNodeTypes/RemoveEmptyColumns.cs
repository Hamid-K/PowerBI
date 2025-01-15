using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E32 RID: 3634
	public struct RemoveEmptyColumns : IProgramNodeBuilder, IEquatable<RemoveEmptyColumns>
	{
		// Token: 0x170011A1 RID: 4513
		// (get) Token: 0x0600613B RID: 24891 RVA: 0x0013ED52 File Offset: 0x0013CF52
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600613C RID: 24892 RVA: 0x0013ED5A File Offset: 0x0013CF5A
		private RemoveEmptyColumns(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600613D RID: 24893 RVA: 0x0013ED63 File Offset: 0x0013CF63
		public static RemoveEmptyColumns CreateUnsafe(ProgramNode node)
		{
			return new RemoveEmptyColumns(node);
		}

		// Token: 0x0600613E RID: 24894 RVA: 0x0013ED6C File Offset: 0x0013CF6C
		public static RemoveEmptyColumns? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.RemoveEmptyColumns)
			{
				return null;
			}
			return new RemoveEmptyColumns?(RemoveEmptyColumns.CreateUnsafe(node));
		}

		// Token: 0x0600613F RID: 24895 RVA: 0x0013EDA1 File Offset: 0x0013CFA1
		public RemoveEmptyColumns(GrammarBuilders g, mProgram value0)
		{
			this._node = g.Rule.RemoveEmptyColumns.BuildASTNode(value0.Node);
		}

		// Token: 0x06006140 RID: 24896 RVA: 0x0013EDC0 File Offset: 0x0013CFC0
		public static implicit operator mProgram(RemoveEmptyColumns arg)
		{
			return mProgram.CreateUnsafe(arg.Node);
		}

		// Token: 0x170011A2 RID: 4514
		// (get) Token: 0x06006141 RID: 24897 RVA: 0x0013EDCE File Offset: 0x0013CFCE
		public mProgram mProgram
		{
			get
			{
				return mProgram.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06006142 RID: 24898 RVA: 0x0013EDE2 File Offset: 0x0013CFE2
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006143 RID: 24899 RVA: 0x0013EDF8 File Offset: 0x0013CFF8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006144 RID: 24900 RVA: 0x0013EE22 File Offset: 0x0013D022
		public bool Equals(RemoveEmptyColumns other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BDC RID: 11228
		private ProgramNode _node;
	}
}
