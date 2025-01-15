using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes
{
	// Token: 0x020011EB RID: 4587
	public struct LetResult : IProgramNodeBuilder, IEquatable<LetResult>
	{
		// Token: 0x170017AF RID: 6063
		// (get) Token: 0x060089DF RID: 35295 RVA: 0x001CFC62 File Offset: 0x001CDE62
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060089E0 RID: 35296 RVA: 0x001CFC6A File Offset: 0x001CDE6A
		private LetResult(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060089E1 RID: 35297 RVA: 0x001CFC73 File Offset: 0x001CDE73
		public static LetResult CreateUnsafe(ProgramNode node)
		{
			return new LetResult(node);
		}

		// Token: 0x060089E2 RID: 35298 RVA: 0x001CFC7C File Offset: 0x001CDE7C
		public static LetResult? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.LetResult)
			{
				return null;
			}
			return new LetResult?(LetResult.CreateUnsafe(node));
		}

		// Token: 0x060089E3 RID: 35299 RVA: 0x001CFCB1 File Offset: 0x001CDEB1
		public LetResult(GrammarBuilders g, inputSRegion value0, disjunctive_match value1)
		{
			this._node = new LetNode(g.Rule.LetResult, value0.Node, value1.Node);
		}

		// Token: 0x060089E4 RID: 35300 RVA: 0x001CFCD7 File Offset: 0x001CDED7
		public static implicit operator result(LetResult arg)
		{
			return result.CreateUnsafe(arg.Node);
		}

		// Token: 0x170017B0 RID: 6064
		// (get) Token: 0x060089E5 RID: 35301 RVA: 0x001CFCE5 File Offset: 0x001CDEE5
		public inputSRegion inputSRegion
		{
			get
			{
				return inputSRegion.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170017B1 RID: 6065
		// (get) Token: 0x060089E6 RID: 35302 RVA: 0x001CFCF9 File Offset: 0x001CDEF9
		public disjunctive_match disjunctive_match
		{
			get
			{
				return disjunctive_match.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x060089E7 RID: 35303 RVA: 0x001CFD0D File Offset: 0x001CDF0D
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060089E8 RID: 35304 RVA: 0x001CFD20 File Offset: 0x001CDF20
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060089E9 RID: 35305 RVA: 0x001CFD4A File Offset: 0x001CDF4A
		public bool Equals(LetResult other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400389F RID: 14495
		private ProgramNode _node;
	}
}
