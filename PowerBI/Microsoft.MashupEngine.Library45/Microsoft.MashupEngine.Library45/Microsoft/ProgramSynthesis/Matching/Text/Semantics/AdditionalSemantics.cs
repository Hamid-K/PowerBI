using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Matching.Text.Semantics
{
	// Token: 0x0200121F RID: 4639
	public static class AdditionalSemantics
	{
		// Token: 0x06008BB7 RID: 35767 RVA: 0x001D459D File Offset: 0x001D279D
		public static MatchingLabel IfThenElse(bool @if, MatchingLabel then, MatchingLabel @else)
		{
			if (!@if)
			{
				return @else;
			}
			return then;
		}

		// Token: 0x06008BB8 RID: 35768 RVA: 0x001D45A5 File Offset: 0x001D27A5
		public static ImmutableList<MatchingLabel> LabelledMatchColumns(MatchingLabel labelledDisjunction, ImmutableList<MatchingLabel> labelledMultiResult)
		{
			if (labelledMultiResult == null)
			{
				return null;
			}
			return labelledMultiResult.Insert(0, labelledDisjunction);
		}

		// Token: 0x06008BB9 RID: 35769 RVA: 0x001D45B4 File Offset: 0x001D27B4
		public static ImmutableList<bool> Nil(IEnumerable<SuffixRegion> inputSRegions)
		{
			if (inputSRegions != null && !inputSRegions.Any<SuffixRegion>())
			{
				return ImmutableList<bool>.Empty;
			}
			return null;
		}

		// Token: 0x06008BBA RID: 35770 RVA: 0x001D45CB File Offset: 0x001D27CB
		public static SuffixRegion Head(IEnumerable<SuffixRegion> inputSRegions)
		{
			if (inputSRegions == null)
			{
				return null;
			}
			return inputSRegions.FirstOrDefault<SuffixRegion>();
		}

		// Token: 0x06008BBB RID: 35771 RVA: 0x001D45D8 File Offset: 0x001D27D8
		public static IEnumerable<SuffixRegion> Tail(IEnumerable<SuffixRegion> inputSRegions)
		{
			if (inputSRegions == null)
			{
				return null;
			}
			return inputSRegions.Skip(1);
		}

		// Token: 0x06008BBC RID: 35772 RVA: 0x001D45E6 File Offset: 0x001D27E6
		public static IEnumerable<bool> MatchColumns(bool disjunctiveMatch, IEnumerable<bool> multiResult)
		{
			if (multiResult == null)
			{
				return null;
			}
			return multiResult.PrependItem(disjunctiveMatch);
		}
	}
}
