using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes
{
	// Token: 0x02000A4C RID: 2636
	public struct disjunct : IProgramNodeBuilder, IEquatable<disjunct>
	{
		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x060040ED RID: 16621 RVA: 0x000CB6FA File Offset: 0x000C98FA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060040EE RID: 16622 RVA: 0x000CB702 File Offset: 0x000C9902
		private disjunct(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060040EF RID: 16623 RVA: 0x000CB70B File Offset: 0x000C990B
		public static disjunct CreateUnsafe(ProgramNode node)
		{
			return new disjunct(node);
		}

		// Token: 0x060040F0 RID: 16624 RVA: 0x000CB714 File Offset: 0x000C9914
		public static disjunct? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.disjunct)
			{
				return null;
			}
			return new disjunct?(disjunct.CreateUnsafe(node));
		}

		// Token: 0x060040F1 RID: 16625 RVA: 0x000CB74E File Offset: 0x000C994E
		public static disjunct CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new disjunct(new Hole(g.Symbol.disjunct, holeId));
		}

		// Token: 0x060040F2 RID: 16626 RVA: 0x000CB766 File Offset: 0x000C9966
		public bool Is_ConvertDisjunctConjunct(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.ConvertDisjunctConjunct;
		}

		// Token: 0x060040F3 RID: 16627 RVA: 0x000CB780 File Offset: 0x000C9980
		public bool Is_ConvertDisjunctConjunct(GrammarBuilders g, out ConvertDisjunctConjunct value)
		{
			if (this.Node.GrammarRule == g.Rule.ConvertDisjunctConjunct)
			{
				value = ConvertDisjunctConjunct.CreateUnsafe(this.Node);
				return true;
			}
			value = default(ConvertDisjunctConjunct);
			return false;
		}

		// Token: 0x060040F4 RID: 16628 RVA: 0x000CB7B8 File Offset: 0x000C99B8
		public ConvertDisjunctConjunct? As_ConvertDisjunctConjunct(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.ConvertDisjunctConjunct)
			{
				return null;
			}
			return new ConvertDisjunctConjunct?(ConvertDisjunctConjunct.CreateUnsafe(this.Node));
		}

		// Token: 0x060040F5 RID: 16629 RVA: 0x000CB7F8 File Offset: 0x000C99F8
		public ConvertDisjunctConjunct Cast_ConvertDisjunctConjunct(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.ConvertDisjunctConjunct)
			{
				return ConvertDisjunctConjunct.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_ConvertDisjunctConjunct is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060040F6 RID: 16630 RVA: 0x000CB84D File Offset: 0x000C9A4D
		public bool Is_Disjunction(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Disjunction;
		}

		// Token: 0x060040F7 RID: 16631 RVA: 0x000CB867 File Offset: 0x000C9A67
		public bool Is_Disjunction(GrammarBuilders g, out Disjunction value)
		{
			if (this.Node.GrammarRule == g.Rule.Disjunction)
			{
				value = Disjunction.CreateUnsafe(this.Node);
				return true;
			}
			value = default(Disjunction);
			return false;
		}

		// Token: 0x060040F8 RID: 16632 RVA: 0x000CB89C File Offset: 0x000C9A9C
		public Disjunction? As_Disjunction(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Disjunction)
			{
				return null;
			}
			return new Disjunction?(Disjunction.CreateUnsafe(this.Node));
		}

		// Token: 0x060040F9 RID: 16633 RVA: 0x000CB8DC File Offset: 0x000C9ADC
		public Disjunction Cast_Disjunction(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Disjunction)
			{
				return Disjunction.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Disjunction is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060040FA RID: 16634 RVA: 0x000CB934 File Offset: 0x000C9B34
		public T Switch<T>(GrammarBuilders g, Func<ConvertDisjunctConjunct, T> func0, Func<Disjunction, T> func1)
		{
			ConvertDisjunctConjunct convertDisjunctConjunct;
			if (this.Is_ConvertDisjunctConjunct(g, out convertDisjunctConjunct))
			{
				return func0(convertDisjunctConjunct);
			}
			Disjunction disjunction;
			if (this.Is_Disjunction(g, out disjunction))
			{
				return func1(disjunction);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol disjunct");
		}

		// Token: 0x060040FB RID: 16635 RVA: 0x000CB98C File Offset: 0x000C9B8C
		public void Switch(GrammarBuilders g, Action<ConvertDisjunctConjunct> func0, Action<Disjunction> func1)
		{
			ConvertDisjunctConjunct convertDisjunctConjunct;
			if (this.Is_ConvertDisjunctConjunct(g, out convertDisjunctConjunct))
			{
				func0(convertDisjunctConjunct);
				return;
			}
			Disjunction disjunction;
			if (this.Is_Disjunction(g, out disjunction))
			{
				func1(disjunction);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol disjunct");
		}

		// Token: 0x060040FC RID: 16636 RVA: 0x000CB9E3 File Offset: 0x000C9BE3
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060040FD RID: 16637 RVA: 0x000CB9F8 File Offset: 0x000C9BF8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060040FE RID: 16638 RVA: 0x000CBA22 File Offset: 0x000C9C22
		public bool Equals(disjunct other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001D87 RID: 7559
		private ProgramNode _node;
	}
}
