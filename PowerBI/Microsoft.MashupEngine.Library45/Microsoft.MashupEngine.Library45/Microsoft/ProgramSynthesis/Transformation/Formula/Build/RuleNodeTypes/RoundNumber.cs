using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x0200156A RID: 5482
	public struct RoundNumber : IProgramNodeBuilder, IEquatable<RoundNumber>
	{
		// Token: 0x17001F3A RID: 7994
		// (get) Token: 0x0600B33B RID: 45883 RVA: 0x00273142 File Offset: 0x00271342
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B33C RID: 45884 RVA: 0x0027314A File Offset: 0x0027134A
		private RoundNumber(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B33D RID: 45885 RVA: 0x00273153 File Offset: 0x00271353
		public static RoundNumber CreateUnsafe(ProgramNode node)
		{
			return new RoundNumber(node);
		}

		// Token: 0x0600B33E RID: 45886 RVA: 0x0027315C File Offset: 0x0027135C
		public static RoundNumber? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.RoundNumber)
			{
				return null;
			}
			return new RoundNumber?(RoundNumber.CreateUnsafe(node));
		}

		// Token: 0x0600B33F RID: 45887 RVA: 0x00273191 File Offset: 0x00271391
		public RoundNumber(GrammarBuilders g, inumber value0, numberRoundDesc value1)
		{
			this._node = g.Rule.RoundNumber.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600B340 RID: 45888 RVA: 0x002731B7 File Offset: 0x002713B7
		public static implicit operator number1(RoundNumber arg)
		{
			return number1.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001F3B RID: 7995
		// (get) Token: 0x0600B341 RID: 45889 RVA: 0x002731C5 File Offset: 0x002713C5
		public inumber inumber
		{
			get
			{
				return inumber.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001F3C RID: 7996
		// (get) Token: 0x0600B342 RID: 45890 RVA: 0x002731D9 File Offset: 0x002713D9
		public numberRoundDesc numberRoundDesc
		{
			get
			{
				return numberRoundDesc.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600B343 RID: 45891 RVA: 0x002731ED File Offset: 0x002713ED
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B344 RID: 45892 RVA: 0x00273200 File Offset: 0x00271400
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B345 RID: 45893 RVA: 0x0027322A File Offset: 0x0027142A
		public bool Equals(RoundNumber other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004618 RID: 17944
		private ProgramNode _node;
	}
}
