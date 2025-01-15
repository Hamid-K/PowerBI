using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E25 RID: 3621
	public struct TrimBelowTopBorder : IProgramNodeBuilder, IEquatable<TrimBelowTopBorder>
	{
		// Token: 0x17001181 RID: 4481
		// (get) Token: 0x060060B3 RID: 24755 RVA: 0x0013E0FE File Offset: 0x0013C2FE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060060B4 RID: 24756 RVA: 0x0013E106 File Offset: 0x0013C306
		private TrimBelowTopBorder(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060060B5 RID: 24757 RVA: 0x0013E10F File Offset: 0x0013C30F
		public static TrimBelowTopBorder CreateUnsafe(ProgramNode node)
		{
			return new TrimBelowTopBorder(node);
		}

		// Token: 0x060060B6 RID: 24758 RVA: 0x0013E118 File Offset: 0x0013C318
		public static TrimBelowTopBorder? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.TrimBelowTopBorder)
			{
				return null;
			}
			return new TrimBelowTopBorder?(TrimBelowTopBorder.CreateUnsafe(node));
		}

		// Token: 0x060060B7 RID: 24759 RVA: 0x0013E14D File Offset: 0x0013C34D
		public TrimBelowTopBorder(GrammarBuilders g, sheetSection value0)
		{
			this._node = g.Rule.TrimBelowTopBorder.BuildASTNode(value0.Node);
		}

		// Token: 0x060060B8 RID: 24760 RVA: 0x0013E16C File Offset: 0x0013C36C
		public static implicit operator trimTop(TrimBelowTopBorder arg)
		{
			return trimTop.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001182 RID: 4482
		// (get) Token: 0x060060B9 RID: 24761 RVA: 0x0013E17A File Offset: 0x0013C37A
		public sheetSection sheetSection
		{
			get
			{
				return sheetSection.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060060BA RID: 24762 RVA: 0x0013E18E File Offset: 0x0013C38E
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060060BB RID: 24763 RVA: 0x0013E1A4 File Offset: 0x0013C3A4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060060BC RID: 24764 RVA: 0x0013E1CE File Offset: 0x0013C3CE
		public bool Equals(TrimBelowTopBorder other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BCF RID: 11215
		private ProgramNode _node;
	}
}
