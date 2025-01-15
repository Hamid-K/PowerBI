using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Caching;
using Microsoft.ProgramSynthesis.Utils.Interactive;

namespace Microsoft.ProgramSynthesis.DslLibrary
{
	// Token: 0x02000802 RID: 2050
	public class StringLearningCache : ICachefulObject<StringLearningCache>, ICachefulObject
	{
		// Token: 0x06002BBA RID: 11194 RVA: 0x0007AC08 File Offset: 0x00078E08
		private StringLearningCache()
		{
			this._tokens = new HashSet<Token>();
			this._startingTokenMatches = new ConcurrentUnboundedCache<uint, UnboundedCache<Token, TokenMatch>>(null, null, (UnboundedCache<Token, TokenMatch> c) => c.DeepClone());
			this._endingTokenMatches = new ConcurrentUnboundedCache<uint, UnboundedCache<Token, TokenMatch>>(null, null, (UnboundedCache<Token, TokenMatch> c) => c.DeepClone());
			this._matchPositions = new ConcurrentUnboundedCache<Token, CachedList>(null, null, (CachedList lst) => new CachedList(lst));
			this._specializedCaches = new ConcurrentUnboundedCache<Type, ICachefulObject>(null, null, (ICachefulObject c) => c.CloneWithCurrentCacheState());
		}

		// Token: 0x06002BBB RID: 11195 RVA: 0x0007ACD6 File Offset: 0x00078ED6
		public StringLearningCache(string s, IReadOnlyDictionary<string, Token> tokens)
			: this()
		{
			this.StaticTokens = tokens;
			this.Content = s;
		}

		// Token: 0x06002BBC RID: 11196 RVA: 0x0007ACEC File Offset: 0x00078EEC
		private StringLearningCache(StringLearningCache other)
		{
			this._tokens = new HashSet<Token>(other._tokens);
			this._startingTokenMatches = other._startingTokenMatches.DeepClone();
			this._endingTokenMatches = other._endingTokenMatches.DeepClone();
			this._matchPositions = other._matchPositions.DeepClone();
			this._specializedCaches = other._specializedCaches.DeepClone();
			this._populatedTokenCaches = other._populatedTokenCaches;
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06002BBD RID: 11197 RVA: 0x0007AD60 File Offset: 0x00078F60
		public string Content { get; }

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06002BBE RID: 11198 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public uint StartPosition
		{
			get
			{
				return 0U;
			}
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06002BBF RID: 11199 RVA: 0x0007AD68 File Offset: 0x00078F68
		public uint EndPosition
		{
			get
			{
				return (uint)(this.Content.Length + 1);
			}
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06002BC0 RID: 11200 RVA: 0x0007AD77 File Offset: 0x00078F77
		public IEnumerable<Token> MatchedTokens
		{
			get
			{
				this.PopulateTokenCache(null, false);
				return this._matchPositions.Keys;
			}
		}

		// Token: 0x06002BC1 RID: 11201 RVA: 0x0007AD8C File Offset: 0x00078F8C
		public bool TryGetAllMatchesStartingAt(uint pos, out UnboundedCache<Token, TokenMatch> matches)
		{
			this.PopulateTokenCache(null, false);
			return this._startingTokenMatches.Lookup(pos, out matches);
		}

		// Token: 0x06002BC2 RID: 11202 RVA: 0x0007ADA4 File Offset: 0x00078FA4
		public bool TryGetTokenMatchStartingAt(uint pos, Token token, out PositionMatch match)
		{
			this.AddToken(token, true);
			CachedList cachedList;
			if (this._matchPositions.Lookup(token, out cachedList) && cachedList.TryGetMatchStartingAt(pos, out match))
			{
				return true;
			}
			match = default(PositionMatch);
			return false;
		}

		// Token: 0x06002BC3 RID: 11203 RVA: 0x0007ADDD File Offset: 0x00078FDD
		public bool TryGetAllMatchesEndingAt(uint pos, out UnboundedCache<Token, TokenMatch> matches)
		{
			this.PopulateTokenCache(null, false);
			return this._endingTokenMatches.Lookup(pos, out matches);
		}

		// Token: 0x06002BC4 RID: 11204 RVA: 0x0007ADF4 File Offset: 0x00078FF4
		public bool TryGetTokenMatchEndingAt(uint pos, Token token, out PositionMatch match)
		{
			this.AddToken(token, true);
			CachedList cachedList;
			if (this._matchPositions.Lookup(token, out cachedList) && cachedList.TryGetMatchEndingAt(pos, out match))
			{
				return true;
			}
			match = default(PositionMatch);
			return false;
		}

		// Token: 0x06002BC5 RID: 11205 RVA: 0x0007AE2D File Offset: 0x0007902D
		public bool TryGetMatchPositionsFor(Token token, out CachedList matchPositions)
		{
			this.AddToken(token, true);
			return this._matchPositions.Lookup(token, out matchPositions);
		}

		// Token: 0x06002BC6 RID: 11206 RVA: 0x0007AE44 File Offset: 0x00079044
		public MultiValueDictionary<Token, PositionMatch> GetAllTokensMatchPositions(uint start, uint end)
		{
			MultiValueDictionary<Token, PositionMatch> multiValueDictionary = new MultiValueDictionary<Token, PositionMatch>();
			foreach (KeyValuePair<Token, CachedList> keyValuePair in this._matchPositions.Mappings)
			{
				CachedList value = keyValuePair.Value;
				Record<int, int>? values = value.GetValues(start, end);
				if (values != null)
				{
					for (int i = values.Value.Item1; i <= values.Value.Item2; i++)
					{
						multiValueDictionary.Add(keyValuePair.Key, value[i]);
					}
				}
			}
			return multiValueDictionary;
		}

		// Token: 0x06002BC7 RID: 11207 RVA: 0x0007AEEC File Offset: 0x000790EC
		public Token GetStaticTokenByName(string name)
		{
			return this.StaticTokens.MaybeGet(name).OrElseDefault<Token>();
		}

		// Token: 0x06002BC8 RID: 11208 RVA: 0x0007AEFF File Offset: 0x000790FF
		public void InitializeStaticTokens(IEnumerable<Token> supportedTokens = null)
		{
			this.PopulateTokenCache(supportedTokens, true);
		}

		// Token: 0x06002BC9 RID: 11209 RVA: 0x0007AF09 File Offset: 0x00079109
		private void PopulateTokenCache(IEnumerable<Token> supportedTokens = null, bool forceRewrite = false)
		{
			if (this._populatedTokenCaches && !forceRewrite)
			{
				return;
			}
			this.AddTokens(supportedTokens ?? this.StaticTokens.Values);
			this._populatedTokenCaches = true;
		}

		// Token: 0x06002BCA RID: 11210 RVA: 0x0007AF34 File Offset: 0x00079134
		public void AddTokens(IEnumerable<Token> tokens)
		{
			foreach (Token token in tokens)
			{
				this.AddToken(token, false);
			}
		}

		// Token: 0x06002BCB RID: 11211 RVA: 0x0007AF80 File Offset: 0x00079180
		private void AddToken(Token token, bool ignoreIfCachePopulated = false)
		{
			if (ignoreIfCachePopulated && this._populatedTokenCaches)
			{
				return;
			}
			lock (this)
			{
				if (!this._tokens.Contains(token))
				{
					this._tokens.Add(token);
					foreach (PositionMatch positionMatch in token.GetMatches(this.Content))
					{
						this.AddMatch(token, positionMatch);
					}
				}
			}
		}

		// Token: 0x06002BCC RID: 11212 RVA: 0x0007B020 File Offset: 0x00079220
		private void AddMatch(Token token, PositionMatch match)
		{
			if (token.UseForLearning)
			{
				TokenMatch tokenMatch = new TokenMatch(token, match.Length);
				this._startingTokenMatches.GetOrAdd(match.Position, (uint _) => new ConcurrentUnboundedCache<Token, TokenMatch>()).Add(token, tokenMatch);
				this._endingTokenMatches.GetOrAdd(match.Right, (uint _) => new ConcurrentUnboundedCache<Token, TokenMatch>()).Add(token, tokenMatch);
			}
			this._matchPositions.GetOrAdd(token, (Token _) => new CachedList()).Add(match);
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06002BCD RID: 11213 RVA: 0x0007B0E2 File Offset: 0x000792E2
		public IEnumerable<uint> MatchStartPositions
		{
			get
			{
				return this._startingTokenMatches.Keys;
			}
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06002BCE RID: 11214 RVA: 0x0007B0EF File Offset: 0x000792EF
		public IEnumerable<uint> MatchEndPositions
		{
			get
			{
				return this._endingTokenMatches.Keys;
			}
		}

		// Token: 0x06002BCF RID: 11215 RVA: 0x0007B0FC File Offset: 0x000792FC
		public TCache GetSpecializedCache<TCache>(Func<StringLearningCache, TCache> createFunc) where TCache : ICachefulObject<TCache>
		{
			return (TCache)((object)this._specializedCaches.GetOrAdd(typeof(TCache), (Type _) => createFunc(this)));
		}

		// Token: 0x06002BD0 RID: 11216 RVA: 0x0007B143 File Offset: 0x00079343
		public StringLearningCache CloneWithCurrentCacheState()
		{
			return new StringLearningCache(this);
		}

		// Token: 0x06002BD1 RID: 11217 RVA: 0x0007B14C File Offset: 0x0007934C
		public void ClearCaches()
		{
			lock (this)
			{
				this._tokens.Clear();
				this._endingTokenMatches.Clear();
				this._startingTokenMatches.Clear();
				this._matchPositions.Clear();
				this._populatedTokenCaches = false;
				this._specializedCaches.Mappings.ForEach(delegate(KeyValuePair<Type, ICachefulObject> kvp)
				{
					kvp.Value.ClearCaches();
				});
			}
		}

		// Token: 0x06002BD2 RID: 11218 RVA: 0x0007B1E4 File Offset: 0x000793E4
		ICachefulObject ICachefulObject.CloneWithCurrentCacheState()
		{
			return this.CloneWithCurrentCacheState();
		}

		// Token: 0x040014F5 RID: 5365
		private readonly UnboundedCache<uint, UnboundedCache<Token, TokenMatch>> _endingTokenMatches;

		// Token: 0x040014F6 RID: 5366
		private readonly UnboundedCache<Token, CachedList> _matchPositions;

		// Token: 0x040014F7 RID: 5367
		private readonly UnboundedCache<uint, UnboundedCache<Token, TokenMatch>> _startingTokenMatches;

		// Token: 0x040014F8 RID: 5368
		private readonly HashSet<Token> _tokens;

		// Token: 0x040014F9 RID: 5369
		private bool _populatedTokenCaches;

		// Token: 0x040014FA RID: 5370
		private readonly UnboundedCache<Type, ICachefulObject> _specializedCaches;

		// Token: 0x040014FB RID: 5371
		public readonly IReadOnlyDictionary<string, Token> StaticTokens;
	}
}
