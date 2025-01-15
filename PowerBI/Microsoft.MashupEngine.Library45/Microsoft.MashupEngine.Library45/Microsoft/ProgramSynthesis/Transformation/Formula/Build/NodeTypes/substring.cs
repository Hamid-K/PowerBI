using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015BC RID: 5564
	public struct substring : IProgramNodeBuilder, IEquatable<substring>
	{
		// Token: 0x17001FE2 RID: 8162
		// (get) Token: 0x0600B80C RID: 47116 RVA: 0x0027E3E2 File Offset: 0x0027C5E2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B80D RID: 47117 RVA: 0x0027E3EA File Offset: 0x0027C5EA
		private substring(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B80E RID: 47118 RVA: 0x0027E3F3 File Offset: 0x0027C5F3
		public static substring CreateUnsafe(ProgramNode node)
		{
			return new substring(node);
		}

		// Token: 0x0600B80F RID: 47119 RVA: 0x0027E3FC File Offset: 0x0027C5FC
		public static substring? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.substring)
			{
				return null;
			}
			return new substring?(substring.CreateUnsafe(node));
		}

		// Token: 0x0600B810 RID: 47120 RVA: 0x0027E436 File Offset: 0x0027C636
		public static substring CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new substring(new Hole(g.Symbol.substring, holeId));
		}

		// Token: 0x0600B811 RID: 47121 RVA: 0x0027E44E File Offset: 0x0027C64E
		public bool Is_substring_splitTrim(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.substring_splitTrim;
		}

		// Token: 0x0600B812 RID: 47122 RVA: 0x0027E468 File Offset: 0x0027C668
		public bool Is_substring_splitTrim(GrammarBuilders g, out substring_splitTrim value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.substring_splitTrim)
			{
				value = substring_splitTrim.CreateUnsafe(this.Node);
				return true;
			}
			value = default(substring_splitTrim);
			return false;
		}

		// Token: 0x0600B813 RID: 47123 RVA: 0x0027E4A0 File Offset: 0x0027C6A0
		public substring_splitTrim? As_substring_splitTrim(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.substring_splitTrim)
			{
				return null;
			}
			return new substring_splitTrim?(substring_splitTrim.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B814 RID: 47124 RVA: 0x0027E4E0 File Offset: 0x0027C6E0
		public substring_splitTrim Cast_substring_splitTrim(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.substring_splitTrim)
			{
				return substring_splitTrim.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_substring_splitTrim is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B815 RID: 47125 RVA: 0x0027E535 File Offset: 0x0027C735
		public bool Is_SlicePrefixAbs(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.SlicePrefixAbs;
		}

		// Token: 0x0600B816 RID: 47126 RVA: 0x0027E54F File Offset: 0x0027C74F
		public bool Is_SlicePrefixAbs(GrammarBuilders g, out SlicePrefixAbs value)
		{
			if (this.Node.GrammarRule == g.Rule.SlicePrefixAbs)
			{
				value = SlicePrefixAbs.CreateUnsafe(this.Node);
				return true;
			}
			value = default(SlicePrefixAbs);
			return false;
		}

		// Token: 0x0600B817 RID: 47127 RVA: 0x0027E584 File Offset: 0x0027C784
		public SlicePrefixAbs? As_SlicePrefixAbs(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.SlicePrefixAbs)
			{
				return null;
			}
			return new SlicePrefixAbs?(SlicePrefixAbs.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B818 RID: 47128 RVA: 0x0027E5C4 File Offset: 0x0027C7C4
		public SlicePrefixAbs Cast_SlicePrefixAbs(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.SlicePrefixAbs)
			{
				return SlicePrefixAbs.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_SlicePrefixAbs is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B819 RID: 47129 RVA: 0x0027E619 File Offset: 0x0027C819
		public bool Is_SlicePrefix(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.SlicePrefix;
		}

		// Token: 0x0600B81A RID: 47130 RVA: 0x0027E633 File Offset: 0x0027C833
		public bool Is_SlicePrefix(GrammarBuilders g, out SlicePrefix value)
		{
			if (this.Node.GrammarRule == g.Rule.SlicePrefix)
			{
				value = SlicePrefix.CreateUnsafe(this.Node);
				return true;
			}
			value = default(SlicePrefix);
			return false;
		}

		// Token: 0x0600B81B RID: 47131 RVA: 0x0027E668 File Offset: 0x0027C868
		public SlicePrefix? As_SlicePrefix(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.SlicePrefix)
			{
				return null;
			}
			return new SlicePrefix?(SlicePrefix.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B81C RID: 47132 RVA: 0x0027E6A8 File Offset: 0x0027C8A8
		public SlicePrefix Cast_SlicePrefix(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.SlicePrefix)
			{
				return SlicePrefix.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_SlicePrefix is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B81D RID: 47133 RVA: 0x0027E6FD File Offset: 0x0027C8FD
		public bool Is_SliceSuffix(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.SliceSuffix;
		}

		// Token: 0x0600B81E RID: 47134 RVA: 0x0027E717 File Offset: 0x0027C917
		public bool Is_SliceSuffix(GrammarBuilders g, out SliceSuffix value)
		{
			if (this.Node.GrammarRule == g.Rule.SliceSuffix)
			{
				value = SliceSuffix.CreateUnsafe(this.Node);
				return true;
			}
			value = default(SliceSuffix);
			return false;
		}

		// Token: 0x0600B81F RID: 47135 RVA: 0x0027E74C File Offset: 0x0027C94C
		public SliceSuffix? As_SliceSuffix(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.SliceSuffix)
			{
				return null;
			}
			return new SliceSuffix?(SliceSuffix.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B820 RID: 47136 RVA: 0x0027E78C File Offset: 0x0027C98C
		public SliceSuffix Cast_SliceSuffix(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.SliceSuffix)
			{
				return SliceSuffix.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_SliceSuffix is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B821 RID: 47137 RVA: 0x0027E7E1 File Offset: 0x0027C9E1
		public bool Is_substring_sliceTrim(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.substring_sliceTrim;
		}

		// Token: 0x0600B822 RID: 47138 RVA: 0x0027E7FB File Offset: 0x0027C9FB
		public bool Is_substring_sliceTrim(GrammarBuilders g, out substring_sliceTrim value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.substring_sliceTrim)
			{
				value = substring_sliceTrim.CreateUnsafe(this.Node);
				return true;
			}
			value = default(substring_sliceTrim);
			return false;
		}

		// Token: 0x0600B823 RID: 47139 RVA: 0x0027E830 File Offset: 0x0027CA30
		public substring_sliceTrim? As_substring_sliceTrim(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.substring_sliceTrim)
			{
				return null;
			}
			return new substring_sliceTrim?(substring_sliceTrim.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B824 RID: 47140 RVA: 0x0027E870 File Offset: 0x0027CA70
		public substring_sliceTrim Cast_substring_sliceTrim(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.substring_sliceTrim)
			{
				return substring_sliceTrim.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_substring_sliceTrim is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B825 RID: 47141 RVA: 0x0027E8C5 File Offset: 0x0027CAC5
		public bool Is_MatchFull(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.MatchFull;
		}

		// Token: 0x0600B826 RID: 47142 RVA: 0x0027E8DF File Offset: 0x0027CADF
		public bool Is_MatchFull(GrammarBuilders g, out MatchFull value)
		{
			if (this.Node.GrammarRule == g.Rule.MatchFull)
			{
				value = MatchFull.CreateUnsafe(this.Node);
				return true;
			}
			value = default(MatchFull);
			return false;
		}

		// Token: 0x0600B827 RID: 47143 RVA: 0x0027E914 File Offset: 0x0027CB14
		public MatchFull? As_MatchFull(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.MatchFull)
			{
				return null;
			}
			return new MatchFull?(MatchFull.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B828 RID: 47144 RVA: 0x0027E954 File Offset: 0x0027CB54
		public MatchFull Cast_MatchFull(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.MatchFull)
			{
				return MatchFull.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_MatchFull is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B829 RID: 47145 RVA: 0x0027E9A9 File Offset: 0x0027CBA9
		public bool Is_SliceBetween(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.SliceBetween;
		}

		// Token: 0x0600B82A RID: 47146 RVA: 0x0027E9C3 File Offset: 0x0027CBC3
		public bool Is_SliceBetween(GrammarBuilders g, out SliceBetween value)
		{
			if (this.Node.GrammarRule == g.Rule.SliceBetween)
			{
				value = SliceBetween.CreateUnsafe(this.Node);
				return true;
			}
			value = default(SliceBetween);
			return false;
		}

		// Token: 0x0600B82B RID: 47147 RVA: 0x0027E9F8 File Offset: 0x0027CBF8
		public SliceBetween? As_SliceBetween(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.SliceBetween)
			{
				return null;
			}
			return new SliceBetween?(SliceBetween.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B82C RID: 47148 RVA: 0x0027EA38 File Offset: 0x0027CC38
		public SliceBetween Cast_SliceBetween(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.SliceBetween)
			{
				return SliceBetween.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_SliceBetween is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B82D RID: 47149 RVA: 0x0027EA90 File Offset: 0x0027CC90
		public T Switch<T>(GrammarBuilders g, Func<substring_splitTrim, T> func0, Func<SlicePrefixAbs, T> func1, Func<SlicePrefix, T> func2, Func<SliceSuffix, T> func3, Func<substring_sliceTrim, T> func4, Func<MatchFull, T> func5, Func<SliceBetween, T> func6)
		{
			substring_splitTrim substring_splitTrim;
			if (this.Is_substring_splitTrim(g, out substring_splitTrim))
			{
				return func0(substring_splitTrim);
			}
			SlicePrefixAbs slicePrefixAbs;
			if (this.Is_SlicePrefixAbs(g, out slicePrefixAbs))
			{
				return func1(slicePrefixAbs);
			}
			SlicePrefix slicePrefix;
			if (this.Is_SlicePrefix(g, out slicePrefix))
			{
				return func2(slicePrefix);
			}
			SliceSuffix sliceSuffix;
			if (this.Is_SliceSuffix(g, out sliceSuffix))
			{
				return func3(sliceSuffix);
			}
			substring_sliceTrim substring_sliceTrim;
			if (this.Is_substring_sliceTrim(g, out substring_sliceTrim))
			{
				return func4(substring_sliceTrim);
			}
			MatchFull matchFull;
			if (this.Is_MatchFull(g, out matchFull))
			{
				return func5(matchFull);
			}
			SliceBetween sliceBetween;
			if (this.Is_SliceBetween(g, out sliceBetween))
			{
				return func6(sliceBetween);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol substring");
		}

		// Token: 0x0600B82E RID: 47150 RVA: 0x0027EB50 File Offset: 0x0027CD50
		public void Switch(GrammarBuilders g, Action<substring_splitTrim> func0, Action<SlicePrefixAbs> func1, Action<SlicePrefix> func2, Action<SliceSuffix> func3, Action<substring_sliceTrim> func4, Action<MatchFull> func5, Action<SliceBetween> func6)
		{
			substring_splitTrim substring_splitTrim;
			if (this.Is_substring_splitTrim(g, out substring_splitTrim))
			{
				func0(substring_splitTrim);
				return;
			}
			SlicePrefixAbs slicePrefixAbs;
			if (this.Is_SlicePrefixAbs(g, out slicePrefixAbs))
			{
				func1(slicePrefixAbs);
				return;
			}
			SlicePrefix slicePrefix;
			if (this.Is_SlicePrefix(g, out slicePrefix))
			{
				func2(slicePrefix);
				return;
			}
			SliceSuffix sliceSuffix;
			if (this.Is_SliceSuffix(g, out sliceSuffix))
			{
				func3(sliceSuffix);
				return;
			}
			substring_sliceTrim substring_sliceTrim;
			if (this.Is_substring_sliceTrim(g, out substring_sliceTrim))
			{
				func4(substring_sliceTrim);
				return;
			}
			MatchFull matchFull;
			if (this.Is_MatchFull(g, out matchFull))
			{
				func5(matchFull);
				return;
			}
			SliceBetween sliceBetween;
			if (this.Is_SliceBetween(g, out sliceBetween))
			{
				func6(sliceBetween);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol substring");
		}

		// Token: 0x0600B82F RID: 47151 RVA: 0x0027EC0E File Offset: 0x0027CE0E
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B830 RID: 47152 RVA: 0x0027EC24 File Offset: 0x0027CE24
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B831 RID: 47153 RVA: 0x0027EC4E File Offset: 0x0027CE4E
		public bool Equals(substring other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400466A RID: 18026
		private ProgramNode _node;
	}
}
