using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes
{
	// Token: 0x020011E9 RID: 4585
	public struct IfThenElse : IProgramNodeBuilder, IEquatable<IfThenElse>
	{
		// Token: 0x170017A8 RID: 6056
		// (get) Token: 0x060089C8 RID: 35272 RVA: 0x001CFA4E File Offset: 0x001CDC4E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060089C9 RID: 35273 RVA: 0x001CFA56 File Offset: 0x001CDC56
		private IfThenElse(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060089CA RID: 35274 RVA: 0x001CFA5F File Offset: 0x001CDC5F
		public static IfThenElse CreateUnsafe(ProgramNode node)
		{
			return new IfThenElse(node);
		}

		// Token: 0x060089CB RID: 35275 RVA: 0x001CFA68 File Offset: 0x001CDC68
		public static IfThenElse? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.IfThenElse)
			{
				return null;
			}
			return new IfThenElse?(IfThenElse.CreateUnsafe(node));
		}

		// Token: 0x060089CC RID: 35276 RVA: 0x001CFA9D File Offset: 0x001CDC9D
		public IfThenElse(GrammarBuilders g, match value0, label value1, labelled_disjunction value2)
		{
			this._node = g.Rule.IfThenElse.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x060089CD RID: 35277 RVA: 0x001CFACA File Offset: 0x001CDCCA
		public static implicit operator labelled_disjunction(IfThenElse arg)
		{
			return labelled_disjunction.CreateUnsafe(arg.Node);
		}

		// Token: 0x170017A9 RID: 6057
		// (get) Token: 0x060089CE RID: 35278 RVA: 0x001CFAD8 File Offset: 0x001CDCD8
		public match match
		{
			get
			{
				return match.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170017AA RID: 6058
		// (get) Token: 0x060089CF RID: 35279 RVA: 0x001CFAEC File Offset: 0x001CDCEC
		public label label
		{
			get
			{
				return label.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x170017AB RID: 6059
		// (get) Token: 0x060089D0 RID: 35280 RVA: 0x001CFB00 File Offset: 0x001CDD00
		public labelled_disjunction labelled_disjunction
		{
			get
			{
				return labelled_disjunction.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x060089D1 RID: 35281 RVA: 0x001CFB14 File Offset: 0x001CDD14
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060089D2 RID: 35282 RVA: 0x001CFB28 File Offset: 0x001CDD28
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060089D3 RID: 35283 RVA: 0x001CFB52 File Offset: 0x001CDD52
		public bool Equals(IfThenElse other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400389D RID: 14493
		private ProgramNode _node;
	}
}
