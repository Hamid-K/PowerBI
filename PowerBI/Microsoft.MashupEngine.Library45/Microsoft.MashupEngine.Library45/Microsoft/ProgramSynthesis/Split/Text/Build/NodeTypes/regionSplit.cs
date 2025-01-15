using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes
{
	// Token: 0x0200135F RID: 4959
	public struct regionSplit : IProgramNodeBuilder, IEquatable<regionSplit>
	{
		// Token: 0x17001A68 RID: 6760
		// (get) Token: 0x06009923 RID: 39203 RVA: 0x00207C62 File Offset: 0x00205E62
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06009924 RID: 39204 RVA: 0x00207C6A File Offset: 0x00205E6A
		private regionSplit(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06009925 RID: 39205 RVA: 0x00207C73 File Offset: 0x00205E73
		public static regionSplit CreateUnsafe(ProgramNode node)
		{
			return new regionSplit(node);
		}

		// Token: 0x06009926 RID: 39206 RVA: 0x00207C7C File Offset: 0x00205E7C
		public static regionSplit? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.regionSplit)
			{
				return null;
			}
			return new regionSplit?(regionSplit.CreateUnsafe(node));
		}

		// Token: 0x06009927 RID: 39207 RVA: 0x00207CB6 File Offset: 0x00205EB6
		public static regionSplit CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new regionSplit(new Hole(g.Symbol.regionSplit, holeId));
		}

		// Token: 0x06009928 RID: 39208 RVA: 0x00207CCE File Offset: 0x00205ECE
		public bool Is_ExtractionSplit(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.ExtractionSplit;
		}

		// Token: 0x06009929 RID: 39209 RVA: 0x00207CE8 File Offset: 0x00205EE8
		public bool Is_ExtractionSplit(GrammarBuilders g, out ExtractionSplit value)
		{
			if (this.Node.GrammarRule == g.Rule.ExtractionSplit)
			{
				value = ExtractionSplit.CreateUnsafe(this.Node);
				return true;
			}
			value = default(ExtractionSplit);
			return false;
		}

		// Token: 0x0600992A RID: 39210 RVA: 0x00207D20 File Offset: 0x00205F20
		public ExtractionSplit? As_ExtractionSplit(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.ExtractionSplit)
			{
				return null;
			}
			return new ExtractionSplit?(ExtractionSplit.CreateUnsafe(this.Node));
		}

		// Token: 0x0600992B RID: 39211 RVA: 0x00207D60 File Offset: 0x00205F60
		public ExtractionSplit Cast_ExtractionSplit(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.ExtractionSplit)
			{
				return ExtractionSplit.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_ExtractionSplit is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600992C RID: 39212 RVA: 0x00207DB5 File Offset: 0x00205FB5
		public bool Is_SplitRegion(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.SplitRegion;
		}

		// Token: 0x0600992D RID: 39213 RVA: 0x00207DCF File Offset: 0x00205FCF
		public bool Is_SplitRegion(GrammarBuilders g, out SplitRegion value)
		{
			if (this.Node.GrammarRule == g.Rule.SplitRegion)
			{
				value = SplitRegion.CreateUnsafe(this.Node);
				return true;
			}
			value = default(SplitRegion);
			return false;
		}

		// Token: 0x0600992E RID: 39214 RVA: 0x00207E04 File Offset: 0x00206004
		public SplitRegion? As_SplitRegion(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.SplitRegion)
			{
				return null;
			}
			return new SplitRegion?(SplitRegion.CreateUnsafe(this.Node));
		}

		// Token: 0x0600992F RID: 39215 RVA: 0x00207E44 File Offset: 0x00206044
		public SplitRegion Cast_SplitRegion(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.SplitRegion)
			{
				return SplitRegion.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_SplitRegion is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06009930 RID: 39216 RVA: 0x00207E9C File Offset: 0x0020609C
		public T Switch<T>(GrammarBuilders g, Func<ExtractionSplit, T> func0, Func<SplitRegion, T> func1)
		{
			ExtractionSplit extractionSplit;
			if (this.Is_ExtractionSplit(g, out extractionSplit))
			{
				return func0(extractionSplit);
			}
			SplitRegion splitRegion;
			if (this.Is_SplitRegion(g, out splitRegion))
			{
				return func1(splitRegion);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol regionSplit");
		}

		// Token: 0x06009931 RID: 39217 RVA: 0x00207EF4 File Offset: 0x002060F4
		public void Switch(GrammarBuilders g, Action<ExtractionSplit> func0, Action<SplitRegion> func1)
		{
			ExtractionSplit extractionSplit;
			if (this.Is_ExtractionSplit(g, out extractionSplit))
			{
				func0(extractionSplit);
				return;
			}
			SplitRegion splitRegion;
			if (this.Is_SplitRegion(g, out splitRegion))
			{
				func1(splitRegion);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol regionSplit");
		}

		// Token: 0x06009932 RID: 39218 RVA: 0x00207F4B File Offset: 0x0020614B
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06009933 RID: 39219 RVA: 0x00207F60 File Offset: 0x00206160
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06009934 RID: 39220 RVA: 0x00207F8A File Offset: 0x0020618A
		public bool Equals(regionSplit other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DD6 RID: 15830
		private ProgramNode _node;
	}
}
