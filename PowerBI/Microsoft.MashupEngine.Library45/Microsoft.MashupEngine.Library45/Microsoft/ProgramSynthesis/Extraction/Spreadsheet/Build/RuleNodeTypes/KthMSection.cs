using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E4D RID: 3661
	public struct KthMSection : IProgramNodeBuilder, IEquatable<KthMSection>
	{
		// Token: 0x170011E0 RID: 4576
		// (get) Token: 0x06006252 RID: 25170 RVA: 0x00140662 File Offset: 0x0013E862
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006253 RID: 25171 RVA: 0x0014066A File Offset: 0x0013E86A
		private KthMSection(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006254 RID: 25172 RVA: 0x00140673 File Offset: 0x0013E873
		public static KthMSection CreateUnsafe(ProgramNode node)
		{
			return new KthMSection(node);
		}

		// Token: 0x06006255 RID: 25173 RVA: 0x0014067C File Offset: 0x0013E87C
		public static KthMSection? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.KthMSection)
			{
				return null;
			}
			return new KthMSection?(KthMSection.CreateUnsafe(node));
		}

		// Token: 0x06006256 RID: 25174 RVA: 0x001406B1 File Offset: 0x0013E8B1
		public KthMSection(GrammarBuilders g, mSection value0, k value1)
		{
			this._node = g.Rule.KthMSection.BuildConceptASTFromDslAST(new ProgramNode[] { value0.Node, value1.Node });
		}

		// Token: 0x06006257 RID: 25175 RVA: 0x001406E3 File Offset: 0x0013E8E3
		public static implicit operator mTable(KthMSection arg)
		{
			return mTable.CreateUnsafe(arg.Node);
		}

		// Token: 0x170011E1 RID: 4577
		// (get) Token: 0x06006258 RID: 25176 RVA: 0x001406F1 File Offset: 0x0013E8F1
		public mSection mSection
		{
			get
			{
				return mSection.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170011E2 RID: 4578
		// (get) Token: 0x06006259 RID: 25177 RVA: 0x00140705 File Offset: 0x0013E905
		public k k
		{
			get
			{
				return k.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600625A RID: 25178 RVA: 0x00140719 File Offset: 0x0013E919
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600625B RID: 25179 RVA: 0x0014072C File Offset: 0x0013E92C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600625C RID: 25180 RVA: 0x00140756 File Offset: 0x0013E956
		public bool Equals(KthMSection other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BF7 RID: 11255
		private ProgramNode _node;
	}
}
