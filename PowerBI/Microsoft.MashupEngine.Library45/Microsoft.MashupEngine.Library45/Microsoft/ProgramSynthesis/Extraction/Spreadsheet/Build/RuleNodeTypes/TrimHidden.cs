using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E19 RID: 3609
	public struct TrimHidden : IProgramNodeBuilder, IEquatable<TrimHidden>
	{
		// Token: 0x17001168 RID: 4456
		// (get) Token: 0x0600603A RID: 24634 RVA: 0x0013D636 File Offset: 0x0013B836
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600603B RID: 24635 RVA: 0x0013D63E File Offset: 0x0013B83E
		private TrimHidden(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600603C RID: 24636 RVA: 0x0013D647 File Offset: 0x0013B847
		public static TrimHidden CreateUnsafe(ProgramNode node)
		{
			return new TrimHidden(node);
		}

		// Token: 0x0600603D RID: 24637 RVA: 0x0013D650 File Offset: 0x0013B850
		public static TrimHidden? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.TrimHidden)
			{
				return null;
			}
			return new TrimHidden?(TrimHidden.CreateUnsafe(node));
		}

		// Token: 0x0600603E RID: 24638 RVA: 0x0013D685 File Offset: 0x0013B885
		public TrimHidden(GrammarBuilders g, area value0)
		{
			this._node = g.Rule.TrimHidden.BuildASTNode(value0.Node);
		}

		// Token: 0x0600603F RID: 24639 RVA: 0x0013D6A4 File Offset: 0x0013B8A4
		public static implicit operator trim(TrimHidden arg)
		{
			return trim.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001169 RID: 4457
		// (get) Token: 0x06006040 RID: 24640 RVA: 0x0013D6B2 File Offset: 0x0013B8B2
		public area area
		{
			get
			{
				return area.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06006041 RID: 24641 RVA: 0x0013D6C6 File Offset: 0x0013B8C6
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006042 RID: 24642 RVA: 0x0013D6DC File Offset: 0x0013B8DC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006043 RID: 24643 RVA: 0x0013D706 File Offset: 0x0013B906
		public bool Equals(TrimHidden other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BC3 RID: 11203
		private ProgramNode _node;
	}
}
