using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes
{
	// Token: 0x02000958 RID: 2392
	public struct SplitTextProg : IProgramNodeBuilder, IEquatable<SplitTextProg>
	{
		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x0600380E RID: 14350 RVA: 0x000AECA6 File Offset: 0x000ACEA6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600380F RID: 14351 RVA: 0x000AECAE File Offset: 0x000ACEAE
		private SplitTextProg(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06003810 RID: 14352 RVA: 0x000AECB7 File Offset: 0x000ACEB7
		public static SplitTextProg CreateUnsafe(ProgramNode node)
		{
			return new SplitTextProg(node);
		}

		// Token: 0x06003811 RID: 14353 RVA: 0x000AECC0 File Offset: 0x000ACEC0
		public static SplitTextProg? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.SplitTextProg)
			{
				return null;
			}
			return new SplitTextProg?(SplitTextProg.CreateUnsafe(node));
		}

		// Token: 0x06003812 RID: 14354 RVA: 0x000AECF5 File Offset: 0x000ACEF5
		public SplitTextProg(GrammarBuilders g, regionSplit value0)
		{
			this._node = g.Rule.SplitTextProg.BuildASTNode(value0.Node);
		}

		// Token: 0x06003813 RID: 14355 RVA: 0x000AED14 File Offset: 0x000ACF14
		public static implicit operator splitTextProg(SplitTextProg arg)
		{
			return splitTextProg.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x06003814 RID: 14356 RVA: 0x000AED22 File Offset: 0x000ACF22
		public regionSplit regionSplit
		{
			get
			{
				return regionSplit.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06003815 RID: 14357 RVA: 0x000AED36 File Offset: 0x000ACF36
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06003816 RID: 14358 RVA: 0x000AED4C File Offset: 0x000ACF4C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06003817 RID: 14359 RVA: 0x000AED76 File Offset: 0x000ACF76
		public bool Equals(SplitTextProg other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A78 RID: 6776
		private ProgramNode _node;
	}
}
