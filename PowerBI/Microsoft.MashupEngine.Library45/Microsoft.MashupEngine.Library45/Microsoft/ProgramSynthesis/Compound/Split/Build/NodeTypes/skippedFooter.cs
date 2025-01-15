using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Compound.Split.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes
{
	// Token: 0x0200096B RID: 2411
	public struct skippedFooter : IProgramNodeBuilder, IEquatable<skippedFooter>
	{
		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x06003936 RID: 14646 RVA: 0x000B170E File Offset: 0x000AF90E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06003937 RID: 14647 RVA: 0x000B1716 File Offset: 0x000AF916
		private skippedFooter(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06003938 RID: 14648 RVA: 0x000B171F File Offset: 0x000AF91F
		public static skippedFooter CreateUnsafe(ProgramNode node)
		{
			return new skippedFooter(node);
		}

		// Token: 0x06003939 RID: 14649 RVA: 0x000B1728 File Offset: 0x000AF928
		public static skippedFooter? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.skippedFooter)
			{
				return null;
			}
			return new skippedFooter?(skippedFooter.CreateUnsafe(node));
		}

		// Token: 0x0600393A RID: 14650 RVA: 0x000B1762 File Offset: 0x000AF962
		public static skippedFooter CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new skippedFooter(new Hole(g.Symbol.skippedFooter, holeId));
		}

		// Token: 0x0600393B RID: 14651 RVA: 0x000B177A File Offset: 0x000AF97A
		public bool Is_SkipFooter(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.SkipFooter;
		}

		// Token: 0x0600393C RID: 14652 RVA: 0x000B1794 File Offset: 0x000AF994
		public bool Is_SkipFooter(GrammarBuilders g, out SkipFooter value)
		{
			if (this.Node.GrammarRule == g.Rule.SkipFooter)
			{
				value = SkipFooter.CreateUnsafe(this.Node);
				return true;
			}
			value = default(SkipFooter);
			return false;
		}

		// Token: 0x0600393D RID: 14653 RVA: 0x000B17CC File Offset: 0x000AF9CC
		public SkipFooter? As_SkipFooter(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.SkipFooter)
			{
				return null;
			}
			return new SkipFooter?(SkipFooter.CreateUnsafe(this.Node));
		}

		// Token: 0x0600393E RID: 14654 RVA: 0x000B180C File Offset: 0x000AFA0C
		public SkipFooter Cast_SkipFooter(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.SkipFooter)
			{
				return SkipFooter.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_SkipFooter is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600393F RID: 14655 RVA: 0x000B1861 File Offset: 0x000AFA61
		public bool Is_skippedFooter_allRecords(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.skippedFooter_allRecords;
		}

		// Token: 0x06003940 RID: 14656 RVA: 0x000B187B File Offset: 0x000AFA7B
		public bool Is_skippedFooter_allRecords(GrammarBuilders g, out skippedFooter_allRecords value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.skippedFooter_allRecords)
			{
				value = skippedFooter_allRecords.CreateUnsafe(this.Node);
				return true;
			}
			value = default(skippedFooter_allRecords);
			return false;
		}

		// Token: 0x06003941 RID: 14657 RVA: 0x000B18B0 File Offset: 0x000AFAB0
		public skippedFooter_allRecords? As_skippedFooter_allRecords(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.skippedFooter_allRecords)
			{
				return null;
			}
			return new skippedFooter_allRecords?(skippedFooter_allRecords.CreateUnsafe(this.Node));
		}

		// Token: 0x06003942 RID: 14658 RVA: 0x000B18F0 File Offset: 0x000AFAF0
		public skippedFooter_allRecords Cast_skippedFooter_allRecords(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.skippedFooter_allRecords)
			{
				return skippedFooter_allRecords.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_skippedFooter_allRecords is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06003943 RID: 14659 RVA: 0x000B1948 File Offset: 0x000AFB48
		public T Switch<T>(GrammarBuilders g, Func<SkipFooter, T> func0, Func<skippedFooter_allRecords, T> func1)
		{
			SkipFooter skipFooter;
			if (this.Is_SkipFooter(g, out skipFooter))
			{
				return func0(skipFooter);
			}
			skippedFooter_allRecords skippedFooter_allRecords;
			if (this.Is_skippedFooter_allRecords(g, out skippedFooter_allRecords))
			{
				return func1(skippedFooter_allRecords);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol skippedFooter");
		}

		// Token: 0x06003944 RID: 14660 RVA: 0x000B19A0 File Offset: 0x000AFBA0
		public void Switch(GrammarBuilders g, Action<SkipFooter> func0, Action<skippedFooter_allRecords> func1)
		{
			SkipFooter skipFooter;
			if (this.Is_SkipFooter(g, out skipFooter))
			{
				func0(skipFooter);
				return;
			}
			skippedFooter_allRecords skippedFooter_allRecords;
			if (this.Is_skippedFooter_allRecords(g, out skippedFooter_allRecords))
			{
				func1(skippedFooter_allRecords);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol skippedFooter");
		}

		// Token: 0x06003945 RID: 14661 RVA: 0x000B19F7 File Offset: 0x000AFBF7
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06003946 RID: 14662 RVA: 0x000B1A0C File Offset: 0x000AFC0C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06003947 RID: 14663 RVA: 0x000B1A36 File Offset: 0x000AFC36
		public bool Equals(skippedFooter other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A8B RID: 6795
		private ProgramNode _node;
	}
}
