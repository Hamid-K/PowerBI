using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Conditionals.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes
{
	// Token: 0x02000A4E RID: 2638
	public struct baseConjunct : IProgramNodeBuilder, IEquatable<baseConjunct>
	{
		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x0600410B RID: 16651 RVA: 0x000CBB26 File Offset: 0x000C9D26
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600410C RID: 16652 RVA: 0x000CBB2E File Offset: 0x000C9D2E
		private baseConjunct(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600410D RID: 16653 RVA: 0x000CBB37 File Offset: 0x000C9D37
		public static baseConjunct CreateUnsafe(ProgramNode node)
		{
			return new baseConjunct(node);
		}

		// Token: 0x0600410E RID: 16654 RVA: 0x000CBB40 File Offset: 0x000C9D40
		public static baseConjunct? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.baseConjunct)
			{
				return null;
			}
			return new baseConjunct?(baseConjunct.CreateUnsafe(node));
		}

		// Token: 0x0600410F RID: 16655 RVA: 0x000CBB7A File Offset: 0x000C9D7A
		public static baseConjunct CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new baseConjunct(new Hole(g.Symbol.baseConjunct, holeId));
		}

		// Token: 0x06004110 RID: 16656 RVA: 0x000CBB92 File Offset: 0x000C9D92
		public bool Is_baseConjunct_pred(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.baseConjunct_pred;
		}

		// Token: 0x06004111 RID: 16657 RVA: 0x000CBBAC File Offset: 0x000C9DAC
		public bool Is_baseConjunct_pred(GrammarBuilders g, out baseConjunct_pred value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.baseConjunct_pred)
			{
				value = baseConjunct_pred.CreateUnsafe(this.Node);
				return true;
			}
			value = default(baseConjunct_pred);
			return false;
		}

		// Token: 0x06004112 RID: 16658 RVA: 0x000CBBE4 File Offset: 0x000C9DE4
		public baseConjunct_pred? As_baseConjunct_pred(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.baseConjunct_pred)
			{
				return null;
			}
			return new baseConjunct_pred?(baseConjunct_pred.CreateUnsafe(this.Node));
		}

		// Token: 0x06004113 RID: 16659 RVA: 0x000CBC24 File Offset: 0x000C9E24
		public baseConjunct_pred Cast_baseConjunct_pred(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.baseConjunct_pred)
			{
				return baseConjunct_pred.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_baseConjunct_pred is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06004114 RID: 16660 RVA: 0x000CBC79 File Offset: 0x000C9E79
		public bool Is_Conjunction(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Conjunction;
		}

		// Token: 0x06004115 RID: 16661 RVA: 0x000CBC93 File Offset: 0x000C9E93
		public bool Is_Conjunction(GrammarBuilders g, out Conjunction value)
		{
			if (this.Node.GrammarRule == g.Rule.Conjunction)
			{
				value = Conjunction.CreateUnsafe(this.Node);
				return true;
			}
			value = default(Conjunction);
			return false;
		}

		// Token: 0x06004116 RID: 16662 RVA: 0x000CBCC8 File Offset: 0x000C9EC8
		public Conjunction? As_Conjunction(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Conjunction)
			{
				return null;
			}
			return new Conjunction?(Conjunction.CreateUnsafe(this.Node));
		}

		// Token: 0x06004117 RID: 16663 RVA: 0x000CBD08 File Offset: 0x000C9F08
		public Conjunction Cast_Conjunction(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Conjunction)
			{
				return Conjunction.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Conjunction is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06004118 RID: 16664 RVA: 0x000CBD60 File Offset: 0x000C9F60
		public T Switch<T>(GrammarBuilders g, Func<baseConjunct_pred, T> func0, Func<Conjunction, T> func1)
		{
			baseConjunct_pred baseConjunct_pred;
			if (this.Is_baseConjunct_pred(g, out baseConjunct_pred))
			{
				return func0(baseConjunct_pred);
			}
			Conjunction conjunction;
			if (this.Is_Conjunction(g, out conjunction))
			{
				return func1(conjunction);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol baseConjunct");
		}

		// Token: 0x06004119 RID: 16665 RVA: 0x000CBDB8 File Offset: 0x000C9FB8
		public void Switch(GrammarBuilders g, Action<baseConjunct_pred> func0, Action<Conjunction> func1)
		{
			baseConjunct_pred baseConjunct_pred;
			if (this.Is_baseConjunct_pred(g, out baseConjunct_pred))
			{
				func0(baseConjunct_pred);
				return;
			}
			Conjunction conjunction;
			if (this.Is_Conjunction(g, out conjunction))
			{
				func1(conjunction);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol baseConjunct");
		}

		// Token: 0x0600411A RID: 16666 RVA: 0x000CBE0F File Offset: 0x000CA00F
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600411B RID: 16667 RVA: 0x000CBE24 File Offset: 0x000CA024
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600411C RID: 16668 RVA: 0x000CBE4E File Offset: 0x000CA04E
		public bool Equals(baseConjunct other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001D89 RID: 7561
		private ProgramNode _node;
	}
}
