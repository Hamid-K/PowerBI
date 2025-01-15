using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Conditionals.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes
{
	// Token: 0x02000A4F RID: 2639
	public struct pred : IProgramNodeBuilder, IEquatable<pred>
	{
		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x0600411D RID: 16669 RVA: 0x000CBE62 File Offset: 0x000CA062
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600411E RID: 16670 RVA: 0x000CBE6A File Offset: 0x000CA06A
		private pred(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600411F RID: 16671 RVA: 0x000CBE73 File Offset: 0x000CA073
		public static pred CreateUnsafe(ProgramNode node)
		{
			return new pred(node);
		}

		// Token: 0x06004120 RID: 16672 RVA: 0x000CBE7C File Offset: 0x000CA07C
		public static pred? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.pred)
			{
				return null;
			}
			return new pred?(pred.CreateUnsafe(node));
		}

		// Token: 0x06004121 RID: 16673 RVA: 0x000CBEB6 File Offset: 0x000CA0B6
		public static pred CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new pred(new Hole(g.Symbol.pred, holeId));
		}

		// Token: 0x06004122 RID: 16674 RVA: 0x000CBECE File Offset: 0x000CA0CE
		public bool Is_pred_match(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.pred_match;
		}

		// Token: 0x06004123 RID: 16675 RVA: 0x000CBEE8 File Offset: 0x000CA0E8
		public bool Is_pred_match(GrammarBuilders g, out pred_match value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.pred_match)
			{
				value = pred_match.CreateUnsafe(this.Node);
				return true;
			}
			value = default(pred_match);
			return false;
		}

		// Token: 0x06004124 RID: 16676 RVA: 0x000CBF20 File Offset: 0x000CA120
		public pred_match? As_pred_match(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.pred_match)
			{
				return null;
			}
			return new pred_match?(pred_match.CreateUnsafe(this.Node));
		}

		// Token: 0x06004125 RID: 16677 RVA: 0x000CBF60 File Offset: 0x000CA160
		public pred_match Cast_pred_match(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.pred_match)
			{
				return pred_match.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_pred_match is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06004126 RID: 16678 RVA: 0x000CBFB5 File Offset: 0x000CA1B5
		public bool Is_Not(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Not;
		}

		// Token: 0x06004127 RID: 16679 RVA: 0x000CBFCF File Offset: 0x000CA1CF
		public bool Is_Not(GrammarBuilders g, out Not value)
		{
			if (this.Node.GrammarRule == g.Rule.Not)
			{
				value = Not.CreateUnsafe(this.Node);
				return true;
			}
			value = default(Not);
			return false;
		}

		// Token: 0x06004128 RID: 16680 RVA: 0x000CC004 File Offset: 0x000CA204
		public Not? As_Not(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Not)
			{
				return null;
			}
			return new Not?(Not.CreateUnsafe(this.Node));
		}

		// Token: 0x06004129 RID: 16681 RVA: 0x000CC044 File Offset: 0x000CA244
		public Not Cast_Not(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Not)
			{
				return Not.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Not is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600412A RID: 16682 RVA: 0x000CC09C File Offset: 0x000CA29C
		public T Switch<T>(GrammarBuilders g, Func<pred_match, T> func0, Func<Not, T> func1)
		{
			pred_match pred_match;
			if (this.Is_pred_match(g, out pred_match))
			{
				return func0(pred_match);
			}
			Not not;
			if (this.Is_Not(g, out not))
			{
				return func1(not);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol pred");
		}

		// Token: 0x0600412B RID: 16683 RVA: 0x000CC0F4 File Offset: 0x000CA2F4
		public void Switch(GrammarBuilders g, Action<pred_match> func0, Action<Not> func1)
		{
			pred_match pred_match;
			if (this.Is_pred_match(g, out pred_match))
			{
				func0(pred_match);
				return;
			}
			Not not;
			if (this.Is_Not(g, out not))
			{
				func1(not);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol pred");
		}

		// Token: 0x0600412C RID: 16684 RVA: 0x000CC14B File Offset: 0x000CA34B
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600412D RID: 16685 RVA: 0x000CC160 File Offset: 0x000CA360
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600412E RID: 16686 RVA: 0x000CC18A File Offset: 0x000CA38A
		public bool Equals(pred other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001D8A RID: 7562
		private ProgramNode _node;
	}
}
