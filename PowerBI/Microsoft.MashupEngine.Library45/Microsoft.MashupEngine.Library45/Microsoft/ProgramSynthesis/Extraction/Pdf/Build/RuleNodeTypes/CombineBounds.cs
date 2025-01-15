using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes
{
	// Token: 0x02000BF9 RID: 3065
	public struct CombineBounds : IProgramNodeBuilder, IEquatable<CombineBounds>
	{
		// Token: 0x17000E1E RID: 3614
		// (get) Token: 0x06004EBE RID: 20158 RVA: 0x000F928E File Offset: 0x000F748E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004EBF RID: 20159 RVA: 0x000F9296 File Offset: 0x000F7496
		private CombineBounds(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004EC0 RID: 20160 RVA: 0x000F929F File Offset: 0x000F749F
		public static CombineBounds CreateUnsafe(ProgramNode node)
		{
			return new CombineBounds(node);
		}

		// Token: 0x06004EC1 RID: 20161 RVA: 0x000F92A8 File Offset: 0x000F74A8
		public static CombineBounds? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.CombineBounds)
			{
				return null;
			}
			return new CombineBounds?(CombineBounds.CreateUnsafe(node));
		}

		// Token: 0x06004EC2 RID: 20162 RVA: 0x000F92DD File Offset: 0x000F74DD
		public CombineBounds(GrammarBuilders g, selectedBounds value0, selectedBounds value1)
		{
			this._node = g.Rule.CombineBounds.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06004EC3 RID: 20163 RVA: 0x000F9303 File Offset: 0x000F7503
		public static implicit operator expandedBounds(CombineBounds arg)
		{
			return expandedBounds.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000E1F RID: 3615
		// (get) Token: 0x06004EC4 RID: 20164 RVA: 0x000F9311 File Offset: 0x000F7511
		public selectedBounds selectedBounds1
		{
			get
			{
				return selectedBounds.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17000E20 RID: 3616
		// (get) Token: 0x06004EC5 RID: 20165 RVA: 0x000F9325 File Offset: 0x000F7525
		public selectedBounds selectedBounds2
		{
			get
			{
				return selectedBounds.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06004EC6 RID: 20166 RVA: 0x000F9339 File Offset: 0x000F7539
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004EC7 RID: 20167 RVA: 0x000F934C File Offset: 0x000F754C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004EC8 RID: 20168 RVA: 0x000F9376 File Offset: 0x000F7576
		public bool Equals(CombineBounds other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002321 RID: 8993
		private ProgramNode _node;
	}
}
