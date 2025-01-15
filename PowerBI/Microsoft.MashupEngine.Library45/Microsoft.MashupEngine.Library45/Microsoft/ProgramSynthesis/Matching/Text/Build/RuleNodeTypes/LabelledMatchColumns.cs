using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes
{
	// Token: 0x020011EA RID: 4586
	public struct LabelledMatchColumns : IProgramNodeBuilder, IEquatable<LabelledMatchColumns>
	{
		// Token: 0x170017AC RID: 6060
		// (get) Token: 0x060089D4 RID: 35284 RVA: 0x001CFB66 File Offset: 0x001CDD66
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060089D5 RID: 35285 RVA: 0x001CFB6E File Offset: 0x001CDD6E
		private LabelledMatchColumns(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060089D6 RID: 35286 RVA: 0x001CFB77 File Offset: 0x001CDD77
		public static LabelledMatchColumns CreateUnsafe(ProgramNode node)
		{
			return new LabelledMatchColumns(node);
		}

		// Token: 0x060089D7 RID: 35287 RVA: 0x001CFB80 File Offset: 0x001CDD80
		public static LabelledMatchColumns? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.LabelledMatchColumns)
			{
				return null;
			}
			return new LabelledMatchColumns?(LabelledMatchColumns.CreateUnsafe(node));
		}

		// Token: 0x060089D8 RID: 35288 RVA: 0x001CFBB5 File Offset: 0x001CDDB5
		public LabelledMatchColumns(GrammarBuilders g, labelled_disjunction value0, labelled_multi_result value1)
		{
			this._node = g.Rule.LabelledMatchColumns.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x060089D9 RID: 35289 RVA: 0x001CFBDB File Offset: 0x001CDDDB
		public static implicit operator labelled_multi_result(LabelledMatchColumns arg)
		{
			return labelled_multi_result.CreateUnsafe(arg.Node);
		}

		// Token: 0x170017AD RID: 6061
		// (get) Token: 0x060089DA RID: 35290 RVA: 0x001CFBE9 File Offset: 0x001CDDE9
		public labelled_disjunction labelled_disjunction
		{
			get
			{
				return labelled_disjunction.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170017AE RID: 6062
		// (get) Token: 0x060089DB RID: 35291 RVA: 0x001CFBFD File Offset: 0x001CDDFD
		public labelled_multi_result labelled_multi_result
		{
			get
			{
				return labelled_multi_result.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x060089DC RID: 35292 RVA: 0x001CFC11 File Offset: 0x001CDE11
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060089DD RID: 35293 RVA: 0x001CFC24 File Offset: 0x001CDE24
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060089DE RID: 35294 RVA: 0x001CFC4E File Offset: 0x001CDE4E
		public bool Equals(LabelledMatchColumns other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400389E RID: 14494
		private ProgramNode _node;
	}
}
