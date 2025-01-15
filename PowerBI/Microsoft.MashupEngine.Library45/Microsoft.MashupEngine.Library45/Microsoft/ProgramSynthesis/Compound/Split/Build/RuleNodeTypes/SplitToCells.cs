using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes
{
	// Token: 0x02000955 RID: 2389
	public struct SplitToCells : IProgramNodeBuilder, IEquatable<SplitToCells>
	{
		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x060037ED RID: 14317 RVA: 0x000AE976 File Offset: 0x000ACB76
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060037EE RID: 14318 RVA: 0x000AE97E File Offset: 0x000ACB7E
		private SplitToCells(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060037EF RID: 14319 RVA: 0x000AE987 File Offset: 0x000ACB87
		public static SplitToCells CreateUnsafe(ProgramNode node)
		{
			return new SplitToCells(node);
		}

		// Token: 0x060037F0 RID: 14320 RVA: 0x000AE990 File Offset: 0x000ACB90
		public static SplitToCells? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.SplitToCells)
			{
				return null;
			}
			return new SplitToCells?(SplitToCells.CreateUnsafe(node));
		}

		// Token: 0x060037F1 RID: 14321 RVA: 0x000AE9C5 File Offset: 0x000ACBC5
		public SplitToCells(GrammarBuilders g, splitTextProg value0, records value1)
		{
			this._node = g.Rule.SplitToCells.BuildConceptASTFromDslAST(new ProgramNode[] { value0.Node, value1.Node });
		}

		// Token: 0x060037F2 RID: 14322 RVA: 0x000AE9F7 File Offset: 0x000ACBF7
		public static implicit operator delimiterSplit(SplitToCells arg)
		{
			return delimiterSplit.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x060037F3 RID: 14323 RVA: 0x000AEA05 File Offset: 0x000ACC05
		public splitTextProg splitTextProg
		{
			get
			{
				return splitTextProg.CreateUnsafe(this.Node.Children[0].Children[0]);
			}
		}

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x060037F4 RID: 14324 RVA: 0x000AEA20 File Offset: 0x000ACC20
		public records records
		{
			get
			{
				return records.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x060037F5 RID: 14325 RVA: 0x000AEA34 File Offset: 0x000ACC34
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060037F6 RID: 14326 RVA: 0x000AEA48 File Offset: 0x000ACC48
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060037F7 RID: 14327 RVA: 0x000AEA72 File Offset: 0x000ACC72
		public bool Equals(SplitToCells other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A75 RID: 6773
		private ProgramNode _node;
	}
}
