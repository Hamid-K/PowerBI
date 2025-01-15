using System;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015D3 RID: 5587
	public struct containsMatchRegex : IProgramNodeBuilder, IEquatable<containsMatchRegex>
	{
		// Token: 0x17001FFD RID: 8189
		// (get) Token: 0x0600B962 RID: 47458 RVA: 0x0028107A File Offset: 0x0027F27A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B963 RID: 47459 RVA: 0x00281082 File Offset: 0x0027F282
		private containsMatchRegex(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B964 RID: 47460 RVA: 0x0028108B File Offset: 0x0027F28B
		public static containsMatchRegex CreateUnsafe(ProgramNode node)
		{
			return new containsMatchRegex(node);
		}

		// Token: 0x0600B965 RID: 47461 RVA: 0x00281094 File Offset: 0x0027F294
		public static containsMatchRegex? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.containsMatchRegex)
			{
				return null;
			}
			return new containsMatchRegex?(containsMatchRegex.CreateUnsafe(node));
		}

		// Token: 0x0600B966 RID: 47462 RVA: 0x002810CE File Offset: 0x0027F2CE
		public static containsMatchRegex CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new containsMatchRegex(new Hole(g.Symbol.containsMatchRegex, holeId));
		}

		// Token: 0x0600B967 RID: 47463 RVA: 0x002810E6 File Offset: 0x0027F2E6
		public containsMatchRegex(GrammarBuilders g, Regex value)
		{
			this = new containsMatchRegex(new LiteralNode(g.Symbol.containsMatchRegex, value));
		}

		// Token: 0x17001FFE RID: 8190
		// (get) Token: 0x0600B968 RID: 47464 RVA: 0x002810FF File Offset: 0x0027F2FF
		public Regex Value
		{
			get
			{
				return (Regex)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600B969 RID: 47465 RVA: 0x00281116 File Offset: 0x0027F316
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B96A RID: 47466 RVA: 0x0028112C File Offset: 0x0027F32C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B96B RID: 47467 RVA: 0x00281156 File Offset: 0x0027F356
		public bool Equals(containsMatchRegex other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004681 RID: 18049
		private ProgramNode _node;
	}
}
