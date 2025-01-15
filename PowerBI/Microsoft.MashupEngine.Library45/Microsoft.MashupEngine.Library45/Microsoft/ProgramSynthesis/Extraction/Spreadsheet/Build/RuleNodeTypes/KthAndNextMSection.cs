using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E34 RID: 3636
	public struct KthAndNextMSection : IProgramNodeBuilder, IEquatable<KthAndNextMSection>
	{
		// Token: 0x170011A5 RID: 4517
		// (get) Token: 0x0600614F RID: 24911 RVA: 0x0013EF1A File Offset: 0x0013D11A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006150 RID: 24912 RVA: 0x0013EF22 File Offset: 0x0013D122
		private KthAndNextMSection(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006151 RID: 24913 RVA: 0x0013EF2B File Offset: 0x0013D12B
		public static KthAndNextMSection CreateUnsafe(ProgramNode node)
		{
			return new KthAndNextMSection(node);
		}

		// Token: 0x06006152 RID: 24914 RVA: 0x0013EF34 File Offset: 0x0013D134
		public static KthAndNextMSection? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.KthAndNextMSection)
			{
				return null;
			}
			return new KthAndNextMSection?(KthAndNextMSection.CreateUnsafe(node));
		}

		// Token: 0x06006153 RID: 24915 RVA: 0x0013EF69 File Offset: 0x0013D169
		public KthAndNextMSection(GrammarBuilders g, mSection value0, k value1)
		{
			this._node = g.Rule.KthAndNextMSection.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06006154 RID: 24916 RVA: 0x0013EF8F File Offset: 0x0013D18F
		public static implicit operator mTable(KthAndNextMSection arg)
		{
			return mTable.CreateUnsafe(arg.Node);
		}

		// Token: 0x170011A6 RID: 4518
		// (get) Token: 0x06006155 RID: 24917 RVA: 0x0013EF9D File Offset: 0x0013D19D
		public mSection mSection
		{
			get
			{
				return mSection.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170011A7 RID: 4519
		// (get) Token: 0x06006156 RID: 24918 RVA: 0x0013EFB1 File Offset: 0x0013D1B1
		public k k
		{
			get
			{
				return k.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06006157 RID: 24919 RVA: 0x0013EFC5 File Offset: 0x0013D1C5
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006158 RID: 24920 RVA: 0x0013EFD8 File Offset: 0x0013D1D8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006159 RID: 24921 RVA: 0x0013F002 File Offset: 0x0013D202
		public bool Equals(KthAndNextMSection other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BDE RID: 11230
		private ProgramNode _node;
	}
}
