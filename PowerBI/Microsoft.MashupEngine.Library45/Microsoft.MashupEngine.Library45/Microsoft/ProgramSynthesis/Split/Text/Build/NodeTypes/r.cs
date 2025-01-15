using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Split.Text.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes
{
	// Token: 0x02001369 RID: 4969
	public struct r : IProgramNodeBuilder, IEquatable<r>
	{
		// Token: 0x17001A72 RID: 6770
		// (get) Token: 0x060099DD RID: 39389 RVA: 0x00209D92 File Offset: 0x00207F92
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060099DE RID: 39390 RVA: 0x00209D9A File Offset: 0x00207F9A
		private r(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060099DF RID: 39391 RVA: 0x00209DA3 File Offset: 0x00207FA3
		public static r CreateUnsafe(ProgramNode node)
		{
			return new r(node);
		}

		// Token: 0x060099E0 RID: 39392 RVA: 0x00209DAC File Offset: 0x00207FAC
		public static r? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.r)
			{
				return null;
			}
			return new r?(r.CreateUnsafe(node));
		}

		// Token: 0x060099E1 RID: 39393 RVA: 0x00209DE6 File Offset: 0x00207FE6
		public static r CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new r(new Hole(g.Symbol.r, holeId));
		}

		// Token: 0x060099E2 RID: 39394 RVA: 0x00209DFE File Offset: 0x00207FFE
		public bool Is_Empty(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Empty;
		}

		// Token: 0x060099E3 RID: 39395 RVA: 0x00209E18 File Offset: 0x00208018
		public bool Is_Empty(GrammarBuilders g, out Empty value)
		{
			if (this.Node.GrammarRule == g.Rule.Empty)
			{
				value = Empty.CreateUnsafe(this.Node);
				return true;
			}
			value = default(Empty);
			return false;
		}

		// Token: 0x060099E4 RID: 39396 RVA: 0x00209E50 File Offset: 0x00208050
		public Empty? As_Empty(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Empty)
			{
				return null;
			}
			return new Empty?(Empty.CreateUnsafe(this.Node));
		}

		// Token: 0x060099E5 RID: 39397 RVA: 0x00209E90 File Offset: 0x00208090
		public Empty Cast_Empty(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Empty)
			{
				return Empty.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Empty is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060099E6 RID: 39398 RVA: 0x00209EE5 File Offset: 0x002080E5
		public bool Is_r_regexMatch(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.r_regexMatch;
		}

		// Token: 0x060099E7 RID: 39399 RVA: 0x00209EFF File Offset: 0x002080FF
		public bool Is_r_regexMatch(GrammarBuilders g, out r_regexMatch value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.r_regexMatch)
			{
				value = r_regexMatch.CreateUnsafe(this.Node);
				return true;
			}
			value = default(r_regexMatch);
			return false;
		}

		// Token: 0x060099E8 RID: 39400 RVA: 0x00209F34 File Offset: 0x00208134
		public r_regexMatch? As_r_regexMatch(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.r_regexMatch)
			{
				return null;
			}
			return new r_regexMatch?(r_regexMatch.CreateUnsafe(this.Node));
		}

		// Token: 0x060099E9 RID: 39401 RVA: 0x00209F74 File Offset: 0x00208174
		public r_regexMatch Cast_r_regexMatch(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.r_regexMatch)
			{
				return r_regexMatch.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_r_regexMatch is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060099EA RID: 39402 RVA: 0x00209FC9 File Offset: 0x002081C9
		public bool Is_Concat(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Concat;
		}

		// Token: 0x060099EB RID: 39403 RVA: 0x00209FE3 File Offset: 0x002081E3
		public bool Is_Concat(GrammarBuilders g, out Concat value)
		{
			if (this.Node.GrammarRule == g.Rule.Concat)
			{
				value = Concat.CreateUnsafe(this.Node);
				return true;
			}
			value = default(Concat);
			return false;
		}

		// Token: 0x060099EC RID: 39404 RVA: 0x0020A018 File Offset: 0x00208218
		public Concat? As_Concat(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Concat)
			{
				return null;
			}
			return new Concat?(Concat.CreateUnsafe(this.Node));
		}

		// Token: 0x060099ED RID: 39405 RVA: 0x0020A058 File Offset: 0x00208258
		public Concat Cast_Concat(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Concat)
			{
				return Concat.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Concat is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060099EE RID: 39406 RVA: 0x0020A0B0 File Offset: 0x002082B0
		public T Switch<T>(GrammarBuilders g, Func<Empty, T> func0, Func<r_regexMatch, T> func1, Func<Concat, T> func2)
		{
			Empty empty;
			if (this.Is_Empty(g, out empty))
			{
				return func0(empty);
			}
			r_regexMatch r_regexMatch;
			if (this.Is_r_regexMatch(g, out r_regexMatch))
			{
				return func1(r_regexMatch);
			}
			Concat concat;
			if (this.Is_Concat(g, out concat))
			{
				return func2(concat);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol r");
		}

		// Token: 0x060099EF RID: 39407 RVA: 0x0020A11C File Offset: 0x0020831C
		public void Switch(GrammarBuilders g, Action<Empty> func0, Action<r_regexMatch> func1, Action<Concat> func2)
		{
			Empty empty;
			if (this.Is_Empty(g, out empty))
			{
				func0(empty);
				return;
			}
			r_regexMatch r_regexMatch;
			if (this.Is_r_regexMatch(g, out r_regexMatch))
			{
				func1(r_regexMatch);
				return;
			}
			Concat concat;
			if (this.Is_Concat(g, out concat))
			{
				func2(concat);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol r");
		}

		// Token: 0x060099F0 RID: 39408 RVA: 0x0020A187 File Offset: 0x00208387
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060099F1 RID: 39409 RVA: 0x0020A19C File Offset: 0x0020839C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060099F2 RID: 39410 RVA: 0x0020A1C6 File Offset: 0x002083C6
		public bool Equals(r other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DE0 RID: 15840
		private ProgramNode _node;
	}
}
