using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x0200156E RID: 5486
	public struct Divide : IProgramNodeBuilder, IEquatable<Divide>
	{
		// Token: 0x17001F46 RID: 8006
		// (get) Token: 0x0600B367 RID: 45927 RVA: 0x00273532 File Offset: 0x00271732
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B368 RID: 45928 RVA: 0x0027353A File Offset: 0x0027173A
		private Divide(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B369 RID: 45929 RVA: 0x00273543 File Offset: 0x00271743
		public static Divide CreateUnsafe(ProgramNode node)
		{
			return new Divide(node);
		}

		// Token: 0x0600B36A RID: 45930 RVA: 0x0027354C File Offset: 0x0027174C
		public static Divide? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Divide)
			{
				return null;
			}
			return new Divide?(Divide.CreateUnsafe(node));
		}

		// Token: 0x0600B36B RID: 45931 RVA: 0x00273581 File Offset: 0x00271781
		public Divide(GrammarBuilders g, arithmeticLeft value0, divideRight value1)
		{
			this._node = g.Rule.Divide.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600B36C RID: 45932 RVA: 0x002735A7 File Offset: 0x002717A7
		public static implicit operator arithmetic(Divide arg)
		{
			return arithmetic.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001F47 RID: 8007
		// (get) Token: 0x0600B36D RID: 45933 RVA: 0x002735B5 File Offset: 0x002717B5
		public arithmeticLeft arithmeticLeft
		{
			get
			{
				return arithmeticLeft.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001F48 RID: 8008
		// (get) Token: 0x0600B36E RID: 45934 RVA: 0x002735C9 File Offset: 0x002717C9
		public divideRight divideRight
		{
			get
			{
				return divideRight.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600B36F RID: 45935 RVA: 0x002735DD File Offset: 0x002717DD
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B370 RID: 45936 RVA: 0x002735F0 File Offset: 0x002717F0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B371 RID: 45937 RVA: 0x0027361A File Offset: 0x0027181A
		public bool Equals(Divide other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400461C RID: 17948
		private ProgramNode _node;
	}
}
