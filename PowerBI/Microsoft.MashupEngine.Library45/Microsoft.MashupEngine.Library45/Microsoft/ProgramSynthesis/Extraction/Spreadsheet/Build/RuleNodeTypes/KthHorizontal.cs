using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E4A RID: 3658
	public struct KthHorizontal : IProgramNodeBuilder, IEquatable<KthHorizontal>
	{
		// Token: 0x170011D7 RID: 4567
		// (get) Token: 0x06006231 RID: 25137 RVA: 0x0014034A File Offset: 0x0013E54A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006232 RID: 25138 RVA: 0x00140352 File Offset: 0x0013E552
		private KthHorizontal(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006233 RID: 25139 RVA: 0x0014035B File Offset: 0x0013E55B
		public static KthHorizontal CreateUnsafe(ProgramNode node)
		{
			return new KthHorizontal(node);
		}

		// Token: 0x06006234 RID: 25140 RVA: 0x00140364 File Offset: 0x0013E564
		public static KthHorizontal? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.KthHorizontal)
			{
				return null;
			}
			return new KthHorizontal?(KthHorizontal.CreateUnsafe(node));
		}

		// Token: 0x06006235 RID: 25141 RVA: 0x00140399 File Offset: 0x0013E599
		public KthHorizontal(GrammarBuilders g, horizontalSheetSplits value0, k value1)
		{
			this._node = g.Rule.KthHorizontal.BuildConceptASTFromDslAST(new ProgramNode[] { value0.Node, value1.Node });
		}

		// Token: 0x06006236 RID: 25142 RVA: 0x001403CB File Offset: 0x0013E5CB
		public static implicit operator horizontalSheetSection(KthHorizontal arg)
		{
			return horizontalSheetSection.CreateUnsafe(arg.Node);
		}

		// Token: 0x170011D8 RID: 4568
		// (get) Token: 0x06006237 RID: 25143 RVA: 0x001403D9 File Offset: 0x0013E5D9
		public horizontalSheetSplits horizontalSheetSplits
		{
			get
			{
				return horizontalSheetSplits.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170011D9 RID: 4569
		// (get) Token: 0x06006238 RID: 25144 RVA: 0x001403ED File Offset: 0x0013E5ED
		public k k
		{
			get
			{
				return k.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06006239 RID: 25145 RVA: 0x00140401 File Offset: 0x0013E601
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600623A RID: 25146 RVA: 0x00140414 File Offset: 0x0013E614
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600623B RID: 25147 RVA: 0x0014043E File Offset: 0x0013E63E
		public bool Equals(KthHorizontal other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BF4 RID: 11252
		private ProgramNode _node;
	}
}
