using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes
{
	// Token: 0x02000952 RID: 2386
	public struct SplitSequence : IProgramNodeBuilder, IEquatable<SplitSequence>
	{
		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x060037CD RID: 14285 RVA: 0x000AE686 File Offset: 0x000AC886
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060037CE RID: 14286 RVA: 0x000AE68E File Offset: 0x000AC88E
		private SplitSequence(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060037CF RID: 14287 RVA: 0x000AE697 File Offset: 0x000AC897
		public static SplitSequence CreateUnsafe(ProgramNode node)
		{
			return new SplitSequence(node);
		}

		// Token: 0x060037D0 RID: 14288 RVA: 0x000AE6A0 File Offset: 0x000AC8A0
		public static SplitSequence? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.SplitSequence)
			{
				return null;
			}
			return new SplitSequence?(SplitSequence.CreateUnsafe(node));
		}

		// Token: 0x060037D1 RID: 14289 RVA: 0x000AE6D5 File Offset: 0x000AC8D5
		public SplitSequence(GrammarBuilders g, r value0, ls value1)
		{
			this._node = g.Rule.SplitSequence.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x060037D2 RID: 14290 RVA: 0x000AE6FB File Offset: 0x000AC8FB
		public static implicit operator splitSequence(SplitSequence arg)
		{
			return splitSequence.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x060037D3 RID: 14291 RVA: 0x000AE709 File Offset: 0x000AC909
		public r r
		{
			get
			{
				return r.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x060037D4 RID: 14292 RVA: 0x000AE71D File Offset: 0x000AC91D
		public ls ls
		{
			get
			{
				return ls.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x060037D5 RID: 14293 RVA: 0x000AE731 File Offset: 0x000AC931
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060037D6 RID: 14294 RVA: 0x000AE744 File Offset: 0x000AC944
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060037D7 RID: 14295 RVA: 0x000AE76E File Offset: 0x000AC96E
		public bool Equals(SplitSequence other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A72 RID: 6770
		private ProgramNode _node;
	}
}
