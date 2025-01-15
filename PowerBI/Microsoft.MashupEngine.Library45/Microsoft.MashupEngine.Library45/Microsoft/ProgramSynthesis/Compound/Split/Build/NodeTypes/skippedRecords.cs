using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Compound.Split.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes
{
	// Token: 0x0200096A RID: 2410
	public struct skippedRecords : IProgramNodeBuilder, IEquatable<skippedRecords>
	{
		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x06003924 RID: 14628 RVA: 0x000B13D2 File Offset: 0x000AF5D2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06003925 RID: 14629 RVA: 0x000B13DA File Offset: 0x000AF5DA
		private skippedRecords(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06003926 RID: 14630 RVA: 0x000B13E3 File Offset: 0x000AF5E3
		public static skippedRecords CreateUnsafe(ProgramNode node)
		{
			return new skippedRecords(node);
		}

		// Token: 0x06003927 RID: 14631 RVA: 0x000B13EC File Offset: 0x000AF5EC
		public static skippedRecords? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.skippedRecords)
			{
				return null;
			}
			return new skippedRecords?(skippedRecords.CreateUnsafe(node));
		}

		// Token: 0x06003928 RID: 14632 RVA: 0x000B1426 File Offset: 0x000AF626
		public static skippedRecords CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new skippedRecords(new Hole(g.Symbol.skippedRecords, holeId));
		}

		// Token: 0x06003929 RID: 14633 RVA: 0x000B143E File Offset: 0x000AF63E
		public bool Is_Skip(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Skip;
		}

		// Token: 0x0600392A RID: 14634 RVA: 0x000B1458 File Offset: 0x000AF658
		public bool Is_Skip(GrammarBuilders g, out Skip value)
		{
			if (this.Node.GrammarRule == g.Rule.Skip)
			{
				value = Skip.CreateUnsafe(this.Node);
				return true;
			}
			value = default(Skip);
			return false;
		}

		// Token: 0x0600392B RID: 14635 RVA: 0x000B1490 File Offset: 0x000AF690
		public Skip? As_Skip(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Skip)
			{
				return null;
			}
			return new Skip?(Skip.CreateUnsafe(this.Node));
		}

		// Token: 0x0600392C RID: 14636 RVA: 0x000B14D0 File Offset: 0x000AF6D0
		public Skip Cast_Skip(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Skip)
			{
				return Skip.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Skip is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600392D RID: 14637 RVA: 0x000B1525 File Offset: 0x000AF725
		public bool Is_skippedRecords_skippedFooter(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.skippedRecords_skippedFooter;
		}

		// Token: 0x0600392E RID: 14638 RVA: 0x000B153F File Offset: 0x000AF73F
		public bool Is_skippedRecords_skippedFooter(GrammarBuilders g, out skippedRecords_skippedFooter value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.skippedRecords_skippedFooter)
			{
				value = skippedRecords_skippedFooter.CreateUnsafe(this.Node);
				return true;
			}
			value = default(skippedRecords_skippedFooter);
			return false;
		}

		// Token: 0x0600392F RID: 14639 RVA: 0x000B1574 File Offset: 0x000AF774
		public skippedRecords_skippedFooter? As_skippedRecords_skippedFooter(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.skippedRecords_skippedFooter)
			{
				return null;
			}
			return new skippedRecords_skippedFooter?(skippedRecords_skippedFooter.CreateUnsafe(this.Node));
		}

		// Token: 0x06003930 RID: 14640 RVA: 0x000B15B4 File Offset: 0x000AF7B4
		public skippedRecords_skippedFooter Cast_skippedRecords_skippedFooter(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.skippedRecords_skippedFooter)
			{
				return skippedRecords_skippedFooter.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_skippedRecords_skippedFooter is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06003931 RID: 14641 RVA: 0x000B160C File Offset: 0x000AF80C
		public T Switch<T>(GrammarBuilders g, Func<Skip, T> func0, Func<skippedRecords_skippedFooter, T> func1)
		{
			Skip skip;
			if (this.Is_Skip(g, out skip))
			{
				return func0(skip);
			}
			skippedRecords_skippedFooter skippedRecords_skippedFooter;
			if (this.Is_skippedRecords_skippedFooter(g, out skippedRecords_skippedFooter))
			{
				return func1(skippedRecords_skippedFooter);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol skippedRecords");
		}

		// Token: 0x06003932 RID: 14642 RVA: 0x000B1664 File Offset: 0x000AF864
		public void Switch(GrammarBuilders g, Action<Skip> func0, Action<skippedRecords_skippedFooter> func1)
		{
			Skip skip;
			if (this.Is_Skip(g, out skip))
			{
				func0(skip);
				return;
			}
			skippedRecords_skippedFooter skippedRecords_skippedFooter;
			if (this.Is_skippedRecords_skippedFooter(g, out skippedRecords_skippedFooter))
			{
				func1(skippedRecords_skippedFooter);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol skippedRecords");
		}

		// Token: 0x06003933 RID: 14643 RVA: 0x000B16BB File Offset: 0x000AF8BB
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06003934 RID: 14644 RVA: 0x000B16D0 File Offset: 0x000AF8D0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06003935 RID: 14645 RVA: 0x000B16FA File Offset: 0x000AF8FA
		public bool Equals(skippedRecords other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A8A RID: 6794
		private ProgramNode _node;
	}
}
