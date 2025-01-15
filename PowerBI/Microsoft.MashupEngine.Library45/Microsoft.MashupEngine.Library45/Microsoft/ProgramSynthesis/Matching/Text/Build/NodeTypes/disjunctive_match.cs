using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes
{
	// Token: 0x020011F1 RID: 4593
	public struct disjunctive_match : IProgramNodeBuilder, IEquatable<disjunctive_match>
	{
		// Token: 0x170017BF RID: 6079
		// (get) Token: 0x06008A22 RID: 35362 RVA: 0x001D023E File Offset: 0x001CE43E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06008A23 RID: 35363 RVA: 0x001D0246 File Offset: 0x001CE446
		private disjunctive_match(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06008A24 RID: 35364 RVA: 0x001D024F File Offset: 0x001CE44F
		public static disjunctive_match CreateUnsafe(ProgramNode node)
		{
			return new disjunctive_match(node);
		}

		// Token: 0x06008A25 RID: 35365 RVA: 0x001D0258 File Offset: 0x001CE458
		public static disjunctive_match? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.disjunctive_match)
			{
				return null;
			}
			return new disjunctive_match?(disjunctive_match.CreateUnsafe(node));
		}

		// Token: 0x06008A26 RID: 35366 RVA: 0x001D0292 File Offset: 0x001CE492
		public static disjunctive_match CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new disjunctive_match(new Hole(g.Symbol.disjunctive_match, holeId));
		}

		// Token: 0x06008A27 RID: 35367 RVA: 0x001D02AA File Offset: 0x001CE4AA
		public bool Is_NoMatch(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.NoMatch;
		}

		// Token: 0x06008A28 RID: 35368 RVA: 0x001D02C4 File Offset: 0x001CE4C4
		public bool Is_NoMatch(GrammarBuilders g, out NoMatch value)
		{
			if (this.Node.GrammarRule == g.Rule.NoMatch)
			{
				value = NoMatch.CreateUnsafe(this.Node);
				return true;
			}
			value = default(NoMatch);
			return false;
		}

		// Token: 0x06008A29 RID: 35369 RVA: 0x001D02FC File Offset: 0x001CE4FC
		public NoMatch? As_NoMatch(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.NoMatch)
			{
				return null;
			}
			return new NoMatch?(NoMatch.CreateUnsafe(this.Node));
		}

		// Token: 0x06008A2A RID: 35370 RVA: 0x001D033C File Offset: 0x001CE53C
		public NoMatch Cast_NoMatch(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.NoMatch)
			{
				return NoMatch.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_NoMatch is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06008A2B RID: 35371 RVA: 0x001D0391 File Offset: 0x001CE591
		public bool Is_Disjunction(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Disjunction;
		}

		// Token: 0x06008A2C RID: 35372 RVA: 0x001D03AB File Offset: 0x001CE5AB
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

		// Token: 0x06008A2D RID: 35373 RVA: 0x001D03E0 File Offset: 0x001CE5E0
		public Disjunction? As_Disjunction(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Disjunction)
			{
				return null;
			}
			return new Disjunction?(Disjunction.CreateUnsafe(this.Node));
		}

		// Token: 0x06008A2E RID: 35374 RVA: 0x001D0420 File Offset: 0x001CE620
		public Disjunction Cast_Disjunction(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Disjunction)
			{
				return Disjunction.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Disjunction is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06008A2F RID: 35375 RVA: 0x001D0478 File Offset: 0x001CE678
		public T Switch<T>(GrammarBuilders g, Func<NoMatch, T> func0, Func<Disjunction, T> func1)
		{
			NoMatch noMatch;
			if (this.Is_NoMatch(g, out noMatch))
			{
				return func0(noMatch);
			}
			Disjunction disjunction;
			if (this.Is_Disjunction(g, out disjunction))
			{
				return func1(disjunction);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol disjunctive_match");
		}

		// Token: 0x06008A30 RID: 35376 RVA: 0x001D04D0 File Offset: 0x001CE6D0
		public void Switch(GrammarBuilders g, Action<NoMatch> func0, Action<Disjunction> func1)
		{
			NoMatch noMatch;
			if (this.Is_NoMatch(g, out noMatch))
			{
				func0(noMatch);
				return;
			}
			Disjunction disjunction;
			if (this.Is_Disjunction(g, out disjunction))
			{
				func1(disjunction);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol disjunctive_match");
		}

		// Token: 0x06008A31 RID: 35377 RVA: 0x001D0527 File Offset: 0x001CE727
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06008A32 RID: 35378 RVA: 0x001D053C File Offset: 0x001CE73C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06008A33 RID: 35379 RVA: 0x001D0566 File Offset: 0x001CE766
		public bool Equals(disjunctive_match other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040038A5 RID: 14501
		private ProgramNode _node;
	}
}
