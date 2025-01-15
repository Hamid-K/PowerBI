using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015C1 RID: 5569
	public struct pos : IProgramNodeBuilder, IEquatable<pos>
	{
		// Token: 0x17001FE7 RID: 8167
		// (get) Token: 0x0600B876 RID: 47222 RVA: 0x0027F6D2 File Offset: 0x0027D8D2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B877 RID: 47223 RVA: 0x0027F6DA File Offset: 0x0027D8DA
		private pos(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B878 RID: 47224 RVA: 0x0027F6E3 File Offset: 0x0027D8E3
		public static pos CreateUnsafe(ProgramNode node)
		{
			return new pos(node);
		}

		// Token: 0x0600B879 RID: 47225 RVA: 0x0027F6EC File Offset: 0x0027D8EC
		public static pos? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.pos)
			{
				return null;
			}
			return new pos?(pos.CreateUnsafe(node));
		}

		// Token: 0x0600B87A RID: 47226 RVA: 0x0027F726 File Offset: 0x0027D926
		public static pos CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new pos(new Hole(g.Symbol.pos, holeId));
		}

		// Token: 0x0600B87B RID: 47227 RVA: 0x0027F73E File Offset: 0x0027D93E
		public bool Is_Find(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Find;
		}

		// Token: 0x0600B87C RID: 47228 RVA: 0x0027F758 File Offset: 0x0027D958
		public bool Is_Find(GrammarBuilders g, out Find value)
		{
			if (this.Node.GrammarRule == g.Rule.Find)
			{
				value = Find.CreateUnsafe(this.Node);
				return true;
			}
			value = default(Find);
			return false;
		}

		// Token: 0x0600B87D RID: 47229 RVA: 0x0027F790 File Offset: 0x0027D990
		public Find? As_Find(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Find)
			{
				return null;
			}
			return new Find?(Find.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B87E RID: 47230 RVA: 0x0027F7D0 File Offset: 0x0027D9D0
		public Find Cast_Find(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Find)
			{
				return Find.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Find is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B87F RID: 47231 RVA: 0x0027F825 File Offset: 0x0027DA25
		public bool Is_Abs(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Abs;
		}

		// Token: 0x0600B880 RID: 47232 RVA: 0x0027F83F File Offset: 0x0027DA3F
		public bool Is_Abs(GrammarBuilders g, out Abs value)
		{
			if (this.Node.GrammarRule == g.Rule.Abs)
			{
				value = Abs.CreateUnsafe(this.Node);
				return true;
			}
			value = default(Abs);
			return false;
		}

		// Token: 0x0600B881 RID: 47233 RVA: 0x0027F874 File Offset: 0x0027DA74
		public Abs? As_Abs(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Abs)
			{
				return null;
			}
			return new Abs?(Abs.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B882 RID: 47234 RVA: 0x0027F8B4 File Offset: 0x0027DAB4
		public Abs Cast_Abs(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Abs)
			{
				return Abs.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Abs is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B883 RID: 47235 RVA: 0x0027F909 File Offset: 0x0027DB09
		public bool Is_Match(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Match;
		}

		// Token: 0x0600B884 RID: 47236 RVA: 0x0027F923 File Offset: 0x0027DB23
		public bool Is_Match(GrammarBuilders g, out Match value)
		{
			if (this.Node.GrammarRule == g.Rule.Match)
			{
				value = Match.CreateUnsafe(this.Node);
				return true;
			}
			value = default(Match);
			return false;
		}

		// Token: 0x0600B885 RID: 47237 RVA: 0x0027F958 File Offset: 0x0027DB58
		public Match? As_Match(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Match)
			{
				return null;
			}
			return new Match?(Match.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B886 RID: 47238 RVA: 0x0027F998 File Offset: 0x0027DB98
		public Match Cast_Match(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Match)
			{
				return Match.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Match is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B887 RID: 47239 RVA: 0x0027F9ED File Offset: 0x0027DBED
		public bool Is_MatchEnd(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.MatchEnd;
		}

		// Token: 0x0600B888 RID: 47240 RVA: 0x0027FA07 File Offset: 0x0027DC07
		public bool Is_MatchEnd(GrammarBuilders g, out MatchEnd value)
		{
			if (this.Node.GrammarRule == g.Rule.MatchEnd)
			{
				value = MatchEnd.CreateUnsafe(this.Node);
				return true;
			}
			value = default(MatchEnd);
			return false;
		}

		// Token: 0x0600B889 RID: 47241 RVA: 0x0027FA3C File Offset: 0x0027DC3C
		public MatchEnd? As_MatchEnd(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.MatchEnd)
			{
				return null;
			}
			return new MatchEnd?(MatchEnd.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B88A RID: 47242 RVA: 0x0027FA7C File Offset: 0x0027DC7C
		public MatchEnd Cast_MatchEnd(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.MatchEnd)
			{
				return MatchEnd.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_MatchEnd is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B88B RID: 47243 RVA: 0x0027FAD4 File Offset: 0x0027DCD4
		public T Switch<T>(GrammarBuilders g, Func<Find, T> func0, Func<Abs, T> func1, Func<Match, T> func2, Func<MatchEnd, T> func3)
		{
			Find find;
			if (this.Is_Find(g, out find))
			{
				return func0(find);
			}
			Abs abs;
			if (this.Is_Abs(g, out abs))
			{
				return func1(abs);
			}
			Match match;
			if (this.Is_Match(g, out match))
			{
				return func2(match);
			}
			MatchEnd matchEnd;
			if (this.Is_MatchEnd(g, out matchEnd))
			{
				return func3(matchEnd);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol pos");
		}

		// Token: 0x0600B88C RID: 47244 RVA: 0x0027FB54 File Offset: 0x0027DD54
		public void Switch(GrammarBuilders g, Action<Find> func0, Action<Abs> func1, Action<Match> func2, Action<MatchEnd> func3)
		{
			Find find;
			if (this.Is_Find(g, out find))
			{
				func0(find);
				return;
			}
			Abs abs;
			if (this.Is_Abs(g, out abs))
			{
				func1(abs);
				return;
			}
			Match match;
			if (this.Is_Match(g, out match))
			{
				func2(match);
				return;
			}
			MatchEnd matchEnd;
			if (this.Is_MatchEnd(g, out matchEnd))
			{
				func3(matchEnd);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol pos");
		}

		// Token: 0x0600B88D RID: 47245 RVA: 0x0027FBD3 File Offset: 0x0027DDD3
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B88E RID: 47246 RVA: 0x0027FBE8 File Offset: 0x0027DDE8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B88F RID: 47247 RVA: 0x0027FC12 File Offset: 0x0027DE12
		public bool Equals(pos other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400466F RID: 18031
		private ProgramNode _node;
	}
}
