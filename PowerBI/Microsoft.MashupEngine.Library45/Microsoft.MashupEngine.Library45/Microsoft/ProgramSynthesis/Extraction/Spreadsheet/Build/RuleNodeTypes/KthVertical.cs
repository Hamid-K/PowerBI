using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E4B RID: 3659
	public struct KthVertical : IProgramNodeBuilder, IEquatable<KthVertical>
	{
		// Token: 0x170011DA RID: 4570
		// (get) Token: 0x0600623C RID: 25148 RVA: 0x00140452 File Offset: 0x0013E652
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600623D RID: 25149 RVA: 0x0014045A File Offset: 0x0013E65A
		private KthVertical(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600623E RID: 25150 RVA: 0x00140463 File Offset: 0x0013E663
		public static KthVertical CreateUnsafe(ProgramNode node)
		{
			return new KthVertical(node);
		}

		// Token: 0x0600623F RID: 25151 RVA: 0x0014046C File Offset: 0x0013E66C
		public static KthVertical? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.KthVertical)
			{
				return null;
			}
			return new KthVertical?(KthVertical.CreateUnsafe(node));
		}

		// Token: 0x06006240 RID: 25152 RVA: 0x001404A1 File Offset: 0x0013E6A1
		public KthVertical(GrammarBuilders g, verticalSheetSplits value0, k value1)
		{
			this._node = g.Rule.KthVertical.BuildConceptASTFromDslAST(new ProgramNode[] { value0.Node, value1.Node });
		}

		// Token: 0x06006241 RID: 25153 RVA: 0x001404D3 File Offset: 0x0013E6D3
		public static implicit operator verticalSheetSection(KthVertical arg)
		{
			return verticalSheetSection.CreateUnsafe(arg.Node);
		}

		// Token: 0x170011DB RID: 4571
		// (get) Token: 0x06006242 RID: 25154 RVA: 0x001404E1 File Offset: 0x0013E6E1
		public verticalSheetSplits verticalSheetSplits
		{
			get
			{
				return verticalSheetSplits.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170011DC RID: 4572
		// (get) Token: 0x06006243 RID: 25155 RVA: 0x001404F5 File Offset: 0x0013E6F5
		public k k
		{
			get
			{
				return k.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06006244 RID: 25156 RVA: 0x00140509 File Offset: 0x0013E709
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006245 RID: 25157 RVA: 0x0014051C File Offset: 0x0013E71C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006246 RID: 25158 RVA: 0x00140546 File Offset: 0x0013E746
		public bool Equals(KthVertical other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BF5 RID: 11253
		private ProgramNode _node;
	}
}
