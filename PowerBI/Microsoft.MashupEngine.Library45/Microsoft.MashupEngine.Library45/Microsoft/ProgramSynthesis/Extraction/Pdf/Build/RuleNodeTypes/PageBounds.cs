using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes
{
	// Token: 0x02000BF7 RID: 3063
	public struct PageBounds : IProgramNodeBuilder, IEquatable<PageBounds>
	{
		// Token: 0x17000E18 RID: 3608
		// (get) Token: 0x06004EA8 RID: 20136 RVA: 0x000F9092 File Offset: 0x000F7292
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004EA9 RID: 20137 RVA: 0x000F909A File Offset: 0x000F729A
		private PageBounds(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004EAA RID: 20138 RVA: 0x000F90A3 File Offset: 0x000F72A3
		public static PageBounds CreateUnsafe(ProgramNode node)
		{
			return new PageBounds(node);
		}

		// Token: 0x06004EAB RID: 20139 RVA: 0x000F90AC File Offset: 0x000F72AC
		public static PageBounds? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.PageBounds)
			{
				return null;
			}
			return new PageBounds?(PageBounds.CreateUnsafe(node));
		}

		// Token: 0x06004EAC RID: 20140 RVA: 0x000F90E1 File Offset: 0x000F72E1
		public PageBounds(GrammarBuilders g, fixedBounds value0)
		{
			this._node = g.Rule.PageBounds.BuildASTNode(value0.Node);
		}

		// Token: 0x06004EAD RID: 20141 RVA: 0x000F9100 File Offset: 0x000F7300
		public static implicit operator selectedBounds(PageBounds arg)
		{
			return selectedBounds.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000E19 RID: 3609
		// (get) Token: 0x06004EAE RID: 20142 RVA: 0x000F910E File Offset: 0x000F730E
		public fixedBounds fixedBounds
		{
			get
			{
				return fixedBounds.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06004EAF RID: 20143 RVA: 0x000F9122 File Offset: 0x000F7322
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004EB0 RID: 20144 RVA: 0x000F9138 File Offset: 0x000F7338
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004EB1 RID: 20145 RVA: 0x000F9162 File Offset: 0x000F7362
		public bool Equals(PageBounds other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400231F RID: 8991
		private ProgramNode _node;
	}
}
