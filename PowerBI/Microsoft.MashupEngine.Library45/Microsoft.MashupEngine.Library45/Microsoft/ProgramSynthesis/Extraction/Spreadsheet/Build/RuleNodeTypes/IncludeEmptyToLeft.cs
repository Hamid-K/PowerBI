using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E49 RID: 3657
	public struct IncludeEmptyToLeft : IProgramNodeBuilder, IEquatable<IncludeEmptyToLeft>
	{
		// Token: 0x170011D5 RID: 4565
		// (get) Token: 0x06006227 RID: 25127 RVA: 0x00140266 File Offset: 0x0013E466
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006228 RID: 25128 RVA: 0x0014026E File Offset: 0x0013E46E
		private IncludeEmptyToLeft(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006229 RID: 25129 RVA: 0x00140277 File Offset: 0x0013E477
		public static IncludeEmptyToLeft CreateUnsafe(ProgramNode node)
		{
			return new IncludeEmptyToLeft(node);
		}

		// Token: 0x0600622A RID: 25130 RVA: 0x00140280 File Offset: 0x0013E480
		public static IncludeEmptyToLeft? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.IncludeEmptyToLeft)
			{
				return null;
			}
			return new IncludeEmptyToLeft?(IncludeEmptyToLeft.CreateUnsafe(node));
		}

		// Token: 0x0600622B RID: 25131 RVA: 0x001402B5 File Offset: 0x0013E4B5
		public IncludeEmptyToLeft(GrammarBuilders g, output value0)
		{
			this._node = g.Rule.IncludeEmptyToLeft.BuildASTNode(value0.Node);
		}

		// Token: 0x0600622C RID: 25132 RVA: 0x001402D4 File Offset: 0x0013E4D4
		public static implicit operator titleOf(IncludeEmptyToLeft arg)
		{
			return titleOf.CreateUnsafe(arg.Node);
		}

		// Token: 0x170011D6 RID: 4566
		// (get) Token: 0x0600622D RID: 25133 RVA: 0x001402E2 File Offset: 0x0013E4E2
		public output output
		{
			get
			{
				return output.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600622E RID: 25134 RVA: 0x001402F6 File Offset: 0x0013E4F6
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600622F RID: 25135 RVA: 0x0014030C File Offset: 0x0013E50C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006230 RID: 25136 RVA: 0x00140336 File Offset: 0x0013E536
		public bool Equals(IncludeEmptyToLeft other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BF3 RID: 11251
		private ProgramNode _node;
	}
}
