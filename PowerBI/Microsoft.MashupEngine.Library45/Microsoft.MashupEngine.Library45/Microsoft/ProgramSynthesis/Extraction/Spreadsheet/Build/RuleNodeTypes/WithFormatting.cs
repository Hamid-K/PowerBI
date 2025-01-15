using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E2C RID: 3628
	public struct WithFormatting : IProgramNodeBuilder, IEquatable<WithFormatting>
	{
		// Token: 0x17001193 RID: 4499
		// (get) Token: 0x060060FD RID: 24829 RVA: 0x0013E7C6 File Offset: 0x0013C9C6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060060FE RID: 24830 RVA: 0x0013E7CE File Offset: 0x0013C9CE
		private WithFormatting(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060060FF RID: 24831 RVA: 0x0013E7D7 File Offset: 0x0013C9D7
		public static WithFormatting CreateUnsafe(ProgramNode node)
		{
			return new WithFormatting(node);
		}

		// Token: 0x06006100 RID: 24832 RVA: 0x0013E7E0 File Offset: 0x0013C9E0
		public static WithFormatting? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.WithFormatting)
			{
				return null;
			}
			return new WithFormatting?(WithFormatting.CreateUnsafe(node));
		}

		// Token: 0x06006101 RID: 24833 RVA: 0x0013E815 File Offset: 0x0013CA15
		public WithFormatting(GrammarBuilders g, sheetPair value0)
		{
			this._node = g.Rule.WithFormatting.BuildASTNode(value0.Node);
		}

		// Token: 0x06006102 RID: 24834 RVA: 0x0013E834 File Offset: 0x0013CA34
		public static implicit operator sheet(WithFormatting arg)
		{
			return sheet.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001194 RID: 4500
		// (get) Token: 0x06006103 RID: 24835 RVA: 0x0013E842 File Offset: 0x0013CA42
		public sheetPair sheetPair
		{
			get
			{
				return sheetPair.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06006104 RID: 24836 RVA: 0x0013E856 File Offset: 0x0013CA56
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006105 RID: 24837 RVA: 0x0013E86C File Offset: 0x0013CA6C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006106 RID: 24838 RVA: 0x0013E896 File Offset: 0x0013CA96
		public bool Equals(WithFormatting other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BD6 RID: 11222
		private ProgramNode _node;
	}
}
