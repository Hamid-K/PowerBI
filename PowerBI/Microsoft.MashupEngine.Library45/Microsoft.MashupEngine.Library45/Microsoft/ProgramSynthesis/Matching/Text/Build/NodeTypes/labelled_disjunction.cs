using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Matching.Text.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes
{
	// Token: 0x020011F5 RID: 4597
	public struct labelled_disjunction : IProgramNodeBuilder, IEquatable<labelled_disjunction>
	{
		// Token: 0x170017C3 RID: 6083
		// (get) Token: 0x06008A68 RID: 35432 RVA: 0x001D0DEE File Offset: 0x001CEFEE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06008A69 RID: 35433 RVA: 0x001D0DF6 File Offset: 0x001CEFF6
		private labelled_disjunction(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06008A6A RID: 35434 RVA: 0x001D0DFF File Offset: 0x001CEFFF
		public static labelled_disjunction CreateUnsafe(ProgramNode node)
		{
			return new labelled_disjunction(node);
		}

		// Token: 0x06008A6B RID: 35435 RVA: 0x001D0E08 File Offset: 0x001CF008
		public static labelled_disjunction? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.labelled_disjunction)
			{
				return null;
			}
			return new labelled_disjunction?(labelled_disjunction.CreateUnsafe(node));
		}

		// Token: 0x06008A6C RID: 35436 RVA: 0x001D0E42 File Offset: 0x001CF042
		public static labelled_disjunction CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new labelled_disjunction(new Hole(g.Symbol.labelled_disjunction, holeId));
		}

		// Token: 0x06008A6D RID: 35437 RVA: 0x001D0E5A File Offset: 0x001CF05A
		public bool Is_labelled_disjunction_label(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.labelled_disjunction_label;
		}

		// Token: 0x06008A6E RID: 35438 RVA: 0x001D0E74 File Offset: 0x001CF074
		public bool Is_labelled_disjunction_label(GrammarBuilders g, out labelled_disjunction_label value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.labelled_disjunction_label)
			{
				value = labelled_disjunction_label.CreateUnsafe(this.Node);
				return true;
			}
			value = default(labelled_disjunction_label);
			return false;
		}

		// Token: 0x06008A6F RID: 35439 RVA: 0x001D0EAC File Offset: 0x001CF0AC
		public labelled_disjunction_label? As_labelled_disjunction_label(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.labelled_disjunction_label)
			{
				return null;
			}
			return new labelled_disjunction_label?(labelled_disjunction_label.CreateUnsafe(this.Node));
		}

		// Token: 0x06008A70 RID: 35440 RVA: 0x001D0EEC File Offset: 0x001CF0EC
		public labelled_disjunction_label Cast_labelled_disjunction_label(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.labelled_disjunction_label)
			{
				return labelled_disjunction_label.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_labelled_disjunction_label is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06008A71 RID: 35441 RVA: 0x001D0F41 File Offset: 0x001CF141
		public bool Is_IfThenElse(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.IfThenElse;
		}

		// Token: 0x06008A72 RID: 35442 RVA: 0x001D0F5B File Offset: 0x001CF15B
		public bool Is_IfThenElse(GrammarBuilders g, out IfThenElse value)
		{
			if (this.Node.GrammarRule == g.Rule.IfThenElse)
			{
				value = IfThenElse.CreateUnsafe(this.Node);
				return true;
			}
			value = default(IfThenElse);
			return false;
		}

		// Token: 0x06008A73 RID: 35443 RVA: 0x001D0F90 File Offset: 0x001CF190
		public IfThenElse? As_IfThenElse(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.IfThenElse)
			{
				return null;
			}
			return new IfThenElse?(IfThenElse.CreateUnsafe(this.Node));
		}

		// Token: 0x06008A74 RID: 35444 RVA: 0x001D0FD0 File Offset: 0x001CF1D0
		public IfThenElse Cast_IfThenElse(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.IfThenElse)
			{
				return IfThenElse.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_IfThenElse is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06008A75 RID: 35445 RVA: 0x001D1028 File Offset: 0x001CF228
		public T Switch<T>(GrammarBuilders g, Func<labelled_disjunction_label, T> func0, Func<IfThenElse, T> func1)
		{
			labelled_disjunction_label labelled_disjunction_label;
			if (this.Is_labelled_disjunction_label(g, out labelled_disjunction_label))
			{
				return func0(labelled_disjunction_label);
			}
			IfThenElse ifThenElse;
			if (this.Is_IfThenElse(g, out ifThenElse))
			{
				return func1(ifThenElse);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol labelled_disjunction");
		}

		// Token: 0x06008A76 RID: 35446 RVA: 0x001D1080 File Offset: 0x001CF280
		public void Switch(GrammarBuilders g, Action<labelled_disjunction_label> func0, Action<IfThenElse> func1)
		{
			labelled_disjunction_label labelled_disjunction_label;
			if (this.Is_labelled_disjunction_label(g, out labelled_disjunction_label))
			{
				func0(labelled_disjunction_label);
				return;
			}
			IfThenElse ifThenElse;
			if (this.Is_IfThenElse(g, out ifThenElse))
			{
				func1(ifThenElse);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol labelled_disjunction");
		}

		// Token: 0x06008A77 RID: 35447 RVA: 0x001D10D7 File Offset: 0x001CF2D7
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06008A78 RID: 35448 RVA: 0x001D10EC File Offset: 0x001CF2EC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06008A79 RID: 35449 RVA: 0x001D1116 File Offset: 0x001CF316
		public bool Equals(labelled_disjunction other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040038A9 RID: 14505
		private ProgramNode _node;
	}
}
