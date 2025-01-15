using System;
using System.ComponentModel;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.Lucia.Core.Text
{
	// Token: 0x0200014E RID: 334
	[ImmutableObject(false)]
	public sealed class TextAnalysisContext
	{
		// Token: 0x0600069D RID: 1693 RVA: 0x0000B6CC File Offset: 0x000098CC
		private TextAnalysisContext(IDateTimeProvider dateTimeProvider, TextAnalyzerBehavior behavior, TokenizerRuleCategories tokenizerRuleCategories, TokenizerRuleOptions tokenizerRuleOptions, StemmingOptions options, IPosTaggerCache posTaggerCache, ILemmatizerCache lemmatizerCache, int? maxTokenCount, int? maxTokenLength)
		{
			this.DateTimeProvider = dateTimeProvider;
			this.Behavior = behavior;
			this.TokenizerRuleCategories = tokenizerRuleCategories;
			this.TokenizerRuleOptions = tokenizerRuleOptions;
			this.StemmingOptions = options;
			this.PosTaggerCache = posTaggerCache;
			this.LemmatizerCache = lemmatizerCache;
			this.MaxTokenCount = maxTokenCount;
			this.MaxTokenLength = maxTokenLength;
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x0600069E RID: 1694 RVA: 0x0000B724 File Offset: 0x00009924
		public IDateTimeProvider DateTimeProvider { get; }

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x0600069F RID: 1695 RVA: 0x0000B72C File Offset: 0x0000992C
		public TextAnalyzerBehavior Behavior { get; }

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060006A0 RID: 1696 RVA: 0x0000B734 File Offset: 0x00009934
		public TokenizerRuleCategories TokenizerRuleCategories { get; }

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060006A1 RID: 1697 RVA: 0x0000B73C File Offset: 0x0000993C
		public TokenizerRuleOptions TokenizerRuleOptions { get; }

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x0000B744 File Offset: 0x00009944
		public StemmingOptions StemmingOptions { get; }

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060006A3 RID: 1699 RVA: 0x0000B74C File Offset: 0x0000994C
		public IPosTaggerCache PosTaggerCache { get; }

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060006A4 RID: 1700 RVA: 0x0000B754 File Offset: 0x00009954
		public ILemmatizerCache LemmatizerCache { get; }

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x0000B75C File Offset: 0x0000995C
		public int? MaxTokenCount { get; }

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x0000B764 File Offset: 0x00009964
		public int? MaxTokenLength { get; }

		// Token: 0x060006A7 RID: 1703 RVA: 0x0000B76C File Offset: 0x0000996C
		public static TextAnalysisContext Create(IDateTimeProvider dateTimeProvider, TextAnalyzerBehavior behavior = TextAnalyzerBehavior.All, TokenizerRuleCategories tokenizerRuleCategories = TokenizerRuleCategories.All, TokenizerRuleOptions tokenizerRuleOptions = TokenizerRuleOptions.PreserveDateTimeInQuotes, StemmingOptions options = StemmingOptions.All, IPosTaggerCache posTaggerCache = null, ILemmatizerCache lemmatizerCache = null, int? maxTokenCount = 60, int? maxTokenLength = 50)
		{
			return new TextAnalysisContext(dateTimeProvider, behavior, tokenizerRuleCategories, tokenizerRuleOptions, options, posTaggerCache, lemmatizerCache, maxTokenCount, maxTokenLength);
		}

		// Token: 0x04000663 RID: 1635
		private const int DefaultMaxTokenCount = 60;

		// Token: 0x04000664 RID: 1636
		private const int DefaultMaxTokenLength = 50;
	}
}
