using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils
{
	// Token: 0x020001A6 RID: 422
	public class CompletionInfo
	{
		// Token: 0x06000939 RID: 2361 RVA: 0x0001B59C File Offset: 0x0001979C
		public CompletionInfo(string key, string value, EntityToken token, double relativeScore, IReadOnlyDictionary<string, object> metadata = null)
		{
			this.Token = token;
			this.RelativeScore = relativeScore;
			this.Key = key;
			this.Value = value;
			this.Metadata = metadata;
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x0001B5C9 File Offset: 0x000197C9
		public CompletionInfo(string key, EntityToken token, double relativeScore, IReadOnlyDictionary<string, object> metadata = null)
			: this(key, key, token, relativeScore, metadata)
		{
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x0600093B RID: 2363 RVA: 0x0001B5D7 File Offset: 0x000197D7
		public EntityToken Token { get; }

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x0600093C RID: 2364 RVA: 0x0001B5DF File Offset: 0x000197DF
		public double RelativeScore { get; }

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x0600093D RID: 2365 RVA: 0x0001B5E7 File Offset: 0x000197E7
		public string Value { get; }

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x0600093E RID: 2366 RVA: 0x0001B5EF File Offset: 0x000197EF
		public string Key { get; }

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x0600093F RID: 2367 RVA: 0x0001B5F7 File Offset: 0x000197F7
		public IReadOnlyDictionary<string, object> Metadata { get; }

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x0001B5FF File Offset: 0x000197FF
		public double EntityScoreMultiplier
		{
			get
			{
				return this.Token.ScoreMultiplier;
			}
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x0001B60C File Offset: 0x0001980C
		public override string ToString()
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("{0}({1}, {2})", new object[]
			{
				this.Token.EntityName,
				this.RelativeScore,
				this.EntityScoreMultiplier
			}));
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x0001B658 File Offset: 0x00019858
		public CompletionInfo CloneWithValue(string value, IReadOnlyDictionary<string, object> metadata = null)
		{
			return new CompletionInfo(this.Key, value, this.Token, this.RelativeScore, metadata ?? this.Metadata);
		}
	}
}
