using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x02000207 RID: 519
	public abstract class RegexBasedTokenizer : EntityBasedTokenizer
	{
		// Token: 0x06000B20 RID: 2848 RVA: 0x00021FF1 File Offset: 0x000201F1
		protected RegexBasedTokenizer(OverlapStrategy overlapStrategy)
		{
			this.OverlapStrategy = overlapStrategy;
			this._patterns = new List<TokenPattern>();
			this.TokenFactory = null;
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x00022012 File Offset: 0x00020212
		protected RegexBasedTokenizer(OverlapStrategy overlapStrategy, params TokenPattern[] patterns)
			: this(overlapStrategy, null, patterns)
		{
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x0002201D File Offset: 0x0002021D
		protected RegexBasedTokenizer(OverlapStrategy overlapStrategy, RegexBasedTokenizer.TokenFactoryDelegate tokenFactory, params TokenPattern[] patterns)
		{
			this.TokenFactory = tokenFactory;
			this._patterns = patterns.ToList<TokenPattern>();
			this.OverlapStrategy = overlapStrategy;
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000B23 RID: 2851 RVA: 0x0002203F File Offset: 0x0002023F
		[JsonIgnore]
		private OverlapStrategy OverlapStrategy { get; }

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000B24 RID: 2852 RVA: 0x00022047 File Offset: 0x00020247
		// (set) Token: 0x06000B25 RID: 2853 RVA: 0x0002204F File Offset: 0x0002024F
		[JsonIgnore]
		protected RegexBasedTokenizer.TokenFactoryDelegate TokenFactory { get; set; }

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000B26 RID: 2854 RVA: 0x00022058 File Offset: 0x00020258
		[JsonIgnore]
		protected IReadOnlyList<TokenPattern> Patterns
		{
			get
			{
				return this._patterns;
			}
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x00022060 File Offset: 0x00020260
		protected void Initialize(params TokenPattern[] patterns)
		{
			this._patterns.AddRange(patterns);
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x00022060 File Offset: 0x00020260
		protected void Initialize(IEnumerable<TokenPattern> patterns)
		{
			this._patterns.AddRange(patterns);
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x0002206E File Offset: 0x0002026E
		protected void Initialize(RegexBasedTokenizer.TokenFactoryDelegate tokenFactory, params TokenPattern[] patterns)
		{
			this.TokenFactory = tokenFactory;
			this.Initialize(patterns);
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x0002207E File Offset: 0x0002027E
		protected void Initialize(RegexBasedTokenizer.TokenFactoryDelegate tokenFactory, IEnumerable<TokenPattern> patterns)
		{
			this.TokenFactory = tokenFactory;
			this.Initialize(patterns);
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x0002208E File Offset: 0x0002028E
		protected virtual IEnumerable<EntityToken> MakeTokensFromMatches(IEnumerable<TokenPatternMatch> matches)
		{
			if (this.TokenFactory != null)
			{
				return matches.SelectMany((TokenPatternMatch m) => this.TokenFactory(m));
			}
			return null;
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x000220AC File Offset: 0x000202AC
		private IEnumerable<TokenPatternMatch> GetTokenPatternMatches(string sequence)
		{
			foreach (TokenPattern tokenPattern in this.Patterns)
			{
				List<TokenPatternMatch> matches = tokenPattern.Matches(sequence).ToList<TokenPatternMatch>();
				foreach (TokenPatternMatch tokenPatternMatch in matches)
				{
					yield return tokenPatternMatch;
				}
				List<TokenPatternMatch>.Enumerator enumerator2 = default(List<TokenPatternMatch>.Enumerator);
				if (this.OverlapStrategy == OverlapStrategy.StopAtFirstSuccess && matches.Any<TokenPatternMatch>())
				{
					yield break;
				}
				matches = null;
			}
			IEnumerator<TokenPattern> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x000220C3 File Offset: 0x000202C3
		public sealed override IEnumerable<EntityToken> Tokenize(string sequence)
		{
			return this.HandleOverlaps(this.MakeTokensFromMatches(this.GetTokenPatternMatches(sequence)));
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x000220D8 File Offset: 0x000202D8
		private IEnumerable<EntityToken> HandleOverlaps(IEnumerable<EntityToken> tokens)
		{
			if (this.OverlapStrategy != OverlapStrategy.Subsumption)
			{
				foreach (EntityToken entityToken in tokens)
				{
					yield return entityToken;
				}
				IEnumerator<EntityToken> enumerator = null;
				yield break;
			}
			List<IGrouping<Type, EntityToken>> list = (from t in tokens
				group t by t.GetType()).ToList<IGrouping<Type, EntityToken>>();
			using (List<IGrouping<Type, EntityToken>>.Enumerator enumerator2 = list.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					IOrderedEnumerable<EntityToken> orderedEnumerable = from t in enumerator2.Current
						orderby t.Start, t.Length descending
						select t;
					int num = 0;
					int num2 = 0;
					HashSet<EntityToken> bestTokens = new HashSet<EntityToken>(ValueAndPositionBasedEntityEqualityComparer.Instance);
					foreach (EntityToken token in orderedEnumerable)
					{
						if (token.End > num2 || (token.Start == num && token.End == num2 && !bestTokens.Contains(token)))
						{
							yield return token;
							num = token.Start;
							num2 = token.End;
							bestTokens.Add(token);
						}
						token = null;
					}
					IEnumerator<EntityToken> enumerator = null;
					bestTokens = null;
				}
			}
			List<IGrouping<Type, EntityToken>>.Enumerator enumerator2 = default(List<IGrouping<Type, EntityToken>>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x040005BD RID: 1469
		[JsonIgnore]
		private readonly List<TokenPattern> _patterns;

		// Token: 0x02000208 RID: 520
		// (Invoke) Token: 0x06000B31 RID: 2865
		protected delegate IEnumerable<EntityToken> TokenFactoryDelegate(TokenPatternMatch match);
	}
}
