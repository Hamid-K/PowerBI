using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E1A RID: 3610
	public struct DefinedRange : IProgramNodeBuilder, IEquatable<DefinedRange>
	{
		// Token: 0x1700116A RID: 4458
		// (get) Token: 0x06006044 RID: 24644 RVA: 0x0013D71A File Offset: 0x0013B91A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006045 RID: 24645 RVA: 0x0013D722 File Offset: 0x0013B922
		private DefinedRange(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006046 RID: 24646 RVA: 0x0013D72B File Offset: 0x0013B92B
		public static DefinedRange CreateUnsafe(ProgramNode node)
		{
			return new DefinedRange(node);
		}

		// Token: 0x06006047 RID: 24647 RVA: 0x0013D734 File Offset: 0x0013B934
		public static DefinedRange? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.DefinedRange)
			{
				return null;
			}
			return new DefinedRange?(DefinedRange.CreateUnsafe(node));
		}

		// Token: 0x06006048 RID: 24648 RVA: 0x0013D769 File Offset: 0x0013B969
		public DefinedRange(GrammarBuilders g, sheet value0, rangeName value1)
		{
			this._node = g.Rule.DefinedRange.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06006049 RID: 24649 RVA: 0x0013D78F File Offset: 0x0013B98F
		public static implicit operator area(DefinedRange arg)
		{
			return area.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700116B RID: 4459
		// (get) Token: 0x0600604A RID: 24650 RVA: 0x0013D79D File Offset: 0x0013B99D
		public sheet sheet
		{
			get
			{
				return sheet.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x1700116C RID: 4460
		// (get) Token: 0x0600604B RID: 24651 RVA: 0x0013D7B1 File Offset: 0x0013B9B1
		public rangeName rangeName
		{
			get
			{
				return rangeName.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600604C RID: 24652 RVA: 0x0013D7C5 File Offset: 0x0013B9C5
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600604D RID: 24653 RVA: 0x0013D7D8 File Offset: 0x0013B9D8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600604E RID: 24654 RVA: 0x0013D802 File Offset: 0x0013BA02
		public bool Equals(DefinedRange other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BC4 RID: 11204
		private ProgramNode _node;
	}
}
