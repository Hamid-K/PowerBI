using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.ProgramSynthesis.Matching.Text.Semantics;

namespace Microsoft.ProgramSynthesis.Matching.Text
{
	// Token: 0x020011BE RID: 4542
	public class PatternInfo
	{
		// Token: 0x06008729 RID: 34601 RVA: 0x001C5D1C File Offset: 0x001C3F1C
		private PatternInfo(string description, string regex, IReadOnlyList<string> regexesToExclude, double matchingFraction, IReadOnlyList<string> examples, IReadOnlyList<IToken> descriptionTokens, bool isNull)
		{
			this.Description = description;
			this.Regex = regex;
			this.RegexesToExclude = regexesToExclude;
			this.MatchingFraction = matchingFraction;
			this.Examples = examples;
			this.DescriptionTokens = ((descriptionTokens != null) ? descriptionTokens.ToImmutableList<IToken>() : null);
			this.IsNull = isNull;
		}

		// Token: 0x0600872A RID: 34602 RVA: 0x001C5D70 File Offset: 0x001C3F70
		public static PatternInfo TokenSequencePattern(string description, string regex, IReadOnlyList<string> regexesToExclude, double matchingFraction, IReadOnlyList<string> examples, IReadOnlyList<IToken> descriptionTokens)
		{
			return new PatternInfo(description, regex, regexesToExclude, matchingFraction, examples, descriptionTokens, false);
		}

		// Token: 0x0600872B RID: 34603 RVA: 0x001C5D80 File Offset: 0x001C3F80
		public static PatternInfo IsNullPatternInfo(double matchingFraction)
		{
			return new PatternInfo(MatchingLabel.NullMatch.Description(), null, null, matchingFraction, new List<string> { null }, null, true);
		}

		// Token: 0x17001725 RID: 5925
		// (get) Token: 0x0600872C RID: 34604 RVA: 0x001C5DA2 File Offset: 0x001C3FA2
		public string Description { get; }

		// Token: 0x17001726 RID: 5926
		// (get) Token: 0x0600872D RID: 34605 RVA: 0x001C5DAA File Offset: 0x001C3FAA
		public string Regex { get; }

		// Token: 0x17001727 RID: 5927
		// (get) Token: 0x0600872E RID: 34606 RVA: 0x001C5DB2 File Offset: 0x001C3FB2
		public IReadOnlyList<string> RegexesToExclude { get; }

		// Token: 0x17001728 RID: 5928
		// (get) Token: 0x0600872F RID: 34607 RVA: 0x001C5DBA File Offset: 0x001C3FBA
		public double MatchingFraction { get; }

		// Token: 0x17001729 RID: 5929
		// (get) Token: 0x06008730 RID: 34608 RVA: 0x001C5DC2 File Offset: 0x001C3FC2
		public IReadOnlyList<string> Examples { get; }

		// Token: 0x1700172A RID: 5930
		// (get) Token: 0x06008731 RID: 34609 RVA: 0x001C5DCA File Offset: 0x001C3FCA
		public IImmutableList<IToken> DescriptionTokens { get; }

		// Token: 0x040037DE RID: 14302
		public readonly bool IsNull;
	}
}
