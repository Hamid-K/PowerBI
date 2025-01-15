using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Compound.Split.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes
{
	// Token: 0x0200096C RID: 2412
	public struct allRecords : IProgramNodeBuilder, IEquatable<allRecords>
	{
		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x06003948 RID: 14664 RVA: 0x000B1A4A File Offset: 0x000AFC4A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06003949 RID: 14665 RVA: 0x000B1A52 File Offset: 0x000AFC52
		private allRecords(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600394A RID: 14666 RVA: 0x000B1A5B File Offset: 0x000AFC5B
		public static allRecords CreateUnsafe(ProgramNode node)
		{
			return new allRecords(node);
		}

		// Token: 0x0600394B RID: 14667 RVA: 0x000B1A64 File Offset: 0x000AFC64
		public static allRecords? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.allRecords)
			{
				return null;
			}
			return new allRecords?(allRecords.CreateUnsafe(node));
		}

		// Token: 0x0600394C RID: 14668 RVA: 0x000B1A9E File Offset: 0x000AFC9E
		public static allRecords CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new allRecords(new Hole(g.Symbol.allRecords, holeId));
		}

		// Token: 0x0600394D RID: 14669 RVA: 0x000B1AB6 File Offset: 0x000AFCB6
		public bool Is_allRecords_allLines(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.allRecords_allLines;
		}

		// Token: 0x0600394E RID: 14670 RVA: 0x000B1AD0 File Offset: 0x000AFCD0
		public bool Is_allRecords_allLines(GrammarBuilders g, out allRecords_allLines value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.allRecords_allLines)
			{
				value = allRecords_allLines.CreateUnsafe(this.Node);
				return true;
			}
			value = default(allRecords_allLines);
			return false;
		}

		// Token: 0x0600394F RID: 14671 RVA: 0x000B1B08 File Offset: 0x000AFD08
		public allRecords_allLines? As_allRecords_allLines(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.allRecords_allLines)
			{
				return null;
			}
			return new allRecords_allLines?(allRecords_allLines.CreateUnsafe(this.Node));
		}

		// Token: 0x06003950 RID: 14672 RVA: 0x000B1B48 File Offset: 0x000AFD48
		public allRecords_allLines Cast_allRecords_allLines(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.allRecords_allLines)
			{
				return allRecords_allLines.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_allRecords_allLines is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06003951 RID: 14673 RVA: 0x000B1B9D File Offset: 0x000AFD9D
		public bool Is_QuoteRecords(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.QuoteRecords;
		}

		// Token: 0x06003952 RID: 14674 RVA: 0x000B1BB7 File Offset: 0x000AFDB7
		public bool Is_QuoteRecords(GrammarBuilders g, out QuoteRecords value)
		{
			if (this.Node.GrammarRule == g.Rule.QuoteRecords)
			{
				value = QuoteRecords.CreateUnsafe(this.Node);
				return true;
			}
			value = default(QuoteRecords);
			return false;
		}

		// Token: 0x06003953 RID: 14675 RVA: 0x000B1BEC File Offset: 0x000AFDEC
		public QuoteRecords? As_QuoteRecords(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.QuoteRecords)
			{
				return null;
			}
			return new QuoteRecords?(QuoteRecords.CreateUnsafe(this.Node));
		}

		// Token: 0x06003954 RID: 14676 RVA: 0x000B1C2C File Offset: 0x000AFE2C
		public QuoteRecords Cast_QuoteRecords(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.QuoteRecords)
			{
				return QuoteRecords.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_QuoteRecords is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06003955 RID: 14677 RVA: 0x000B1C84 File Offset: 0x000AFE84
		public T Switch<T>(GrammarBuilders g, Func<allRecords_allLines, T> func0, Func<QuoteRecords, T> func1)
		{
			allRecords_allLines allRecords_allLines;
			if (this.Is_allRecords_allLines(g, out allRecords_allLines))
			{
				return func0(allRecords_allLines);
			}
			QuoteRecords quoteRecords;
			if (this.Is_QuoteRecords(g, out quoteRecords))
			{
				return func1(quoteRecords);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol allRecords");
		}

		// Token: 0x06003956 RID: 14678 RVA: 0x000B1CDC File Offset: 0x000AFEDC
		public void Switch(GrammarBuilders g, Action<allRecords_allLines> func0, Action<QuoteRecords> func1)
		{
			allRecords_allLines allRecords_allLines;
			if (this.Is_allRecords_allLines(g, out allRecords_allLines))
			{
				func0(allRecords_allLines);
				return;
			}
			QuoteRecords quoteRecords;
			if (this.Is_QuoteRecords(g, out quoteRecords))
			{
				func1(quoteRecords);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol allRecords");
		}

		// Token: 0x06003957 RID: 14679 RVA: 0x000B1D33 File Offset: 0x000AFF33
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06003958 RID: 14680 RVA: 0x000B1D48 File Offset: 0x000AFF48
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06003959 RID: 14681 RVA: 0x000B1D72 File Offset: 0x000AFF72
		public bool Equals(allRecords other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A8C RID: 6796
		private ProgramNode _node;
	}
}
