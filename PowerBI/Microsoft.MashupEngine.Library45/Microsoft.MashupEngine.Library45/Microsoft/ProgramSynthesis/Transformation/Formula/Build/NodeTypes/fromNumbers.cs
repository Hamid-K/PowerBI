using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015C8 RID: 5576
	public struct fromNumbers : IProgramNodeBuilder, IEquatable<fromNumbers>
	{
		// Token: 0x17001FEE RID: 8174
		// (get) Token: 0x0600B8E6 RID: 47334 RVA: 0x0028062A File Offset: 0x0027E82A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B8E7 RID: 47335 RVA: 0x00280632 File Offset: 0x0027E832
		private fromNumbers(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B8E8 RID: 47336 RVA: 0x0028063B File Offset: 0x0027E83B
		public static fromNumbers CreateUnsafe(ProgramNode node)
		{
			return new fromNumbers(node);
		}

		// Token: 0x0600B8E9 RID: 47337 RVA: 0x00280644 File Offset: 0x0027E844
		public static fromNumbers? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.fromNumbers)
			{
				return null;
			}
			return new fromNumbers?(fromNumbers.CreateUnsafe(node));
		}

		// Token: 0x0600B8EA RID: 47338 RVA: 0x0028067E File Offset: 0x0027E87E
		public static fromNumbers CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new fromNumbers(new Hole(g.Symbol.fromNumbers, holeId));
		}

		// Token: 0x0600B8EB RID: 47339 RVA: 0x00280696 File Offset: 0x0027E896
		public FromNumbers Cast_FromNumbers()
		{
			return FromNumbers.CreateUnsafe(this.Node);
		}

		// Token: 0x0600B8EC RID: 47340 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_FromNumbers(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600B8ED RID: 47341 RVA: 0x002806A3 File Offset: 0x0027E8A3
		public bool Is_FromNumbers(GrammarBuilders g, out FromNumbers value)
		{
			value = FromNumbers.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600B8EE RID: 47342 RVA: 0x002806B7 File Offset: 0x0027E8B7
		public FromNumbers? As_FromNumbers(GrammarBuilders g)
		{
			return new FromNumbers?(FromNumbers.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B8EF RID: 47343 RVA: 0x002806C9 File Offset: 0x0027E8C9
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B8F0 RID: 47344 RVA: 0x002806DC File Offset: 0x0027E8DC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B8F1 RID: 47345 RVA: 0x00280706 File Offset: 0x0027E906
		public bool Equals(fromNumbers other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004676 RID: 18038
		private ProgramNode _node;
	}
}
