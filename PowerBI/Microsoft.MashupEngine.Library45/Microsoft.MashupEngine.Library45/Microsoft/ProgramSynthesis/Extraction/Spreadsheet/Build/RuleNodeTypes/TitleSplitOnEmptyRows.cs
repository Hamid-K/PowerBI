using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E45 RID: 3653
	public struct TitleSplitOnEmptyRows : IProgramNodeBuilder, IEquatable<TitleSplitOnEmptyRows>
	{
		// Token: 0x170011C8 RID: 4552
		// (get) Token: 0x060061FA RID: 25082 RVA: 0x0013FE56 File Offset: 0x0013E056
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060061FB RID: 25083 RVA: 0x0013FE5E File Offset: 0x0013E05E
		private TitleSplitOnEmptyRows(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060061FC RID: 25084 RVA: 0x0013FE67 File Offset: 0x0013E067
		public static TitleSplitOnEmptyRows CreateUnsafe(ProgramNode node)
		{
			return new TitleSplitOnEmptyRows(node);
		}

		// Token: 0x060061FD RID: 25085 RVA: 0x0013FE70 File Offset: 0x0013E070
		public static TitleSplitOnEmptyRows? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.TitleSplitOnEmptyRows)
			{
				return null;
			}
			return new TitleSplitOnEmptyRows?(TitleSplitOnEmptyRows.CreateUnsafe(node));
		}

		// Token: 0x060061FE RID: 25086 RVA: 0x0013FEA5 File Offset: 0x0013E0A5
		public TitleSplitOnEmptyRows(GrammarBuilders g, titleOf value0)
		{
			this._node = g.Rule.TitleSplitOnEmptyRows.BuildASTNode(value0.Node);
		}

		// Token: 0x060061FF RID: 25087 RVA: 0x0013FEC4 File Offset: 0x0013E0C4
		public static implicit operator splitForTitle(TitleSplitOnEmptyRows arg)
		{
			return splitForTitle.CreateUnsafe(arg.Node);
		}

		// Token: 0x170011C9 RID: 4553
		// (get) Token: 0x06006200 RID: 25088 RVA: 0x0013FED2 File Offset: 0x0013E0D2
		public titleOf titleOf
		{
			get
			{
				return titleOf.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06006201 RID: 25089 RVA: 0x0013FEE6 File Offset: 0x0013E0E6
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006202 RID: 25090 RVA: 0x0013FEFC File Offset: 0x0013E0FC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006203 RID: 25091 RVA: 0x0013FF26 File Offset: 0x0013E126
		public bool Equals(TitleSplitOnEmptyRows other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BEF RID: 11247
		private ProgramNode _node;
	}
}
