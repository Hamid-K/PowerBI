using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015C7 RID: 5575
	public struct fromRowNumber : IProgramNodeBuilder, IEquatable<fromRowNumber>
	{
		// Token: 0x17001FED RID: 8173
		// (get) Token: 0x0600B8DA RID: 47322 RVA: 0x0028053A File Offset: 0x0027E73A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B8DB RID: 47323 RVA: 0x00280542 File Offset: 0x0027E742
		private fromRowNumber(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B8DC RID: 47324 RVA: 0x0028054B File Offset: 0x0027E74B
		public static fromRowNumber CreateUnsafe(ProgramNode node)
		{
			return new fromRowNumber(node);
		}

		// Token: 0x0600B8DD RID: 47325 RVA: 0x00280554 File Offset: 0x0027E754
		public static fromRowNumber? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.fromRowNumber)
			{
				return null;
			}
			return new fromRowNumber?(fromRowNumber.CreateUnsafe(node));
		}

		// Token: 0x0600B8DE RID: 47326 RVA: 0x0028058E File Offset: 0x0027E78E
		public static fromRowNumber CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new fromRowNumber(new Hole(g.Symbol.fromRowNumber, holeId));
		}

		// Token: 0x0600B8DF RID: 47327 RVA: 0x002805A6 File Offset: 0x0027E7A6
		public FromRowNumber Cast_FromRowNumber()
		{
			return FromRowNumber.CreateUnsafe(this.Node);
		}

		// Token: 0x0600B8E0 RID: 47328 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_FromRowNumber(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600B8E1 RID: 47329 RVA: 0x002805B3 File Offset: 0x0027E7B3
		public bool Is_FromRowNumber(GrammarBuilders g, out FromRowNumber value)
		{
			value = FromRowNumber.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600B8E2 RID: 47330 RVA: 0x002805C7 File Offset: 0x0027E7C7
		public FromRowNumber? As_FromRowNumber(GrammarBuilders g)
		{
			return new FromRowNumber?(FromRowNumber.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B8E3 RID: 47331 RVA: 0x002805D9 File Offset: 0x0027E7D9
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B8E4 RID: 47332 RVA: 0x002805EC File Offset: 0x0027E7EC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B8E5 RID: 47333 RVA: 0x00280616 File Offset: 0x0027E816
		public bool Equals(fromRowNumber other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004675 RID: 18037
		private ProgramNode _node;
	}
}
