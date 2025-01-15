using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes
{
	// Token: 0x02001368 RID: 4968
	public struct constantDelimiterMatches : IProgramNodeBuilder, IEquatable<constantDelimiterMatches>
	{
		// Token: 0x17001A71 RID: 6769
		// (get) Token: 0x060099CB RID: 39371 RVA: 0x00209A56 File Offset: 0x00207C56
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060099CC RID: 39372 RVA: 0x00209A5E File Offset: 0x00207C5E
		private constantDelimiterMatches(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060099CD RID: 39373 RVA: 0x00209A67 File Offset: 0x00207C67
		public static constantDelimiterMatches CreateUnsafe(ProgramNode node)
		{
			return new constantDelimiterMatches(node);
		}

		// Token: 0x060099CE RID: 39374 RVA: 0x00209A70 File Offset: 0x00207C70
		public static constantDelimiterMatches? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.constantDelimiterMatches)
			{
				return null;
			}
			return new constantDelimiterMatches?(constantDelimiterMatches.CreateUnsafe(node));
		}

		// Token: 0x060099CF RID: 39375 RVA: 0x00209AAA File Offset: 0x00207CAA
		public static constantDelimiterMatches CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new constantDelimiterMatches(new Hole(g.Symbol.constantDelimiterMatches, holeId));
		}

		// Token: 0x060099D0 RID: 39376 RVA: 0x00209AC2 File Offset: 0x00207CC2
		public bool Is_ConstantDelimiterWithQuoting(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.ConstantDelimiterWithQuoting;
		}

		// Token: 0x060099D1 RID: 39377 RVA: 0x00209ADC File Offset: 0x00207CDC
		public bool Is_ConstantDelimiterWithQuoting(GrammarBuilders g, out ConstantDelimiterWithQuoting value)
		{
			if (this.Node.GrammarRule == g.Rule.ConstantDelimiterWithQuoting)
			{
				value = ConstantDelimiterWithQuoting.CreateUnsafe(this.Node);
				return true;
			}
			value = default(ConstantDelimiterWithQuoting);
			return false;
		}

		// Token: 0x060099D2 RID: 39378 RVA: 0x00209B14 File Offset: 0x00207D14
		public ConstantDelimiterWithQuoting? As_ConstantDelimiterWithQuoting(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.ConstantDelimiterWithQuoting)
			{
				return null;
			}
			return new ConstantDelimiterWithQuoting?(ConstantDelimiterWithQuoting.CreateUnsafe(this.Node));
		}

		// Token: 0x060099D3 RID: 39379 RVA: 0x00209B54 File Offset: 0x00207D54
		public ConstantDelimiterWithQuoting Cast_ConstantDelimiterWithQuoting(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.ConstantDelimiterWithQuoting)
			{
				return ConstantDelimiterWithQuoting.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_ConstantDelimiterWithQuoting is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060099D4 RID: 39380 RVA: 0x00209BA9 File Offset: 0x00207DA9
		public bool Is_ConstantDelimiter(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.ConstantDelimiter;
		}

		// Token: 0x060099D5 RID: 39381 RVA: 0x00209BC3 File Offset: 0x00207DC3
		public bool Is_ConstantDelimiter(GrammarBuilders g, out ConstantDelimiter value)
		{
			if (this.Node.GrammarRule == g.Rule.ConstantDelimiter)
			{
				value = ConstantDelimiter.CreateUnsafe(this.Node);
				return true;
			}
			value = default(ConstantDelimiter);
			return false;
		}

		// Token: 0x060099D6 RID: 39382 RVA: 0x00209BF8 File Offset: 0x00207DF8
		public ConstantDelimiter? As_ConstantDelimiter(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.ConstantDelimiter)
			{
				return null;
			}
			return new ConstantDelimiter?(ConstantDelimiter.CreateUnsafe(this.Node));
		}

		// Token: 0x060099D7 RID: 39383 RVA: 0x00209C38 File Offset: 0x00207E38
		public ConstantDelimiter Cast_ConstantDelimiter(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.ConstantDelimiter)
			{
				return ConstantDelimiter.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_ConstantDelimiter is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060099D8 RID: 39384 RVA: 0x00209C90 File Offset: 0x00207E90
		public T Switch<T>(GrammarBuilders g, Func<ConstantDelimiterWithQuoting, T> func0, Func<ConstantDelimiter, T> func1)
		{
			ConstantDelimiterWithQuoting constantDelimiterWithQuoting;
			if (this.Is_ConstantDelimiterWithQuoting(g, out constantDelimiterWithQuoting))
			{
				return func0(constantDelimiterWithQuoting);
			}
			ConstantDelimiter constantDelimiter;
			if (this.Is_ConstantDelimiter(g, out constantDelimiter))
			{
				return func1(constantDelimiter);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol constantDelimiterMatches");
		}

		// Token: 0x060099D9 RID: 39385 RVA: 0x00209CE8 File Offset: 0x00207EE8
		public void Switch(GrammarBuilders g, Action<ConstantDelimiterWithQuoting> func0, Action<ConstantDelimiter> func1)
		{
			ConstantDelimiterWithQuoting constantDelimiterWithQuoting;
			if (this.Is_ConstantDelimiterWithQuoting(g, out constantDelimiterWithQuoting))
			{
				func0(constantDelimiterWithQuoting);
				return;
			}
			ConstantDelimiter constantDelimiter;
			if (this.Is_ConstantDelimiter(g, out constantDelimiter))
			{
				func1(constantDelimiter);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol constantDelimiterMatches");
		}

		// Token: 0x060099DA RID: 39386 RVA: 0x00209D3F File Offset: 0x00207F3F
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060099DB RID: 39387 RVA: 0x00209D54 File Offset: 0x00207F54
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060099DC RID: 39388 RVA: 0x00209D7E File Offset: 0x00207F7E
		public bool Equals(constantDelimiterMatches other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DDF RID: 15839
		private ProgramNode _node;
	}
}
