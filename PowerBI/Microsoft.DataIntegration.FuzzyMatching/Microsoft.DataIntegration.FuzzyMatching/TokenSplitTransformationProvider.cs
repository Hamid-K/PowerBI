using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.DataIntegration.FuzzyMatchingCommon;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000109 RID: 265
	[Serializable]
	public class TokenSplitTransformationProvider : ITransformationProvider, IProviderInitialize, IRowsetConsumer, ISessionable
	{
		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000AF8 RID: 2808 RVA: 0x00030DD9 File Offset: 0x0002EFD9
		// (set) Token: 0x06000AF9 RID: 2809 RVA: 0x00030DE1 File Offset: 0x0002EFE1
		public int MaxSegments { get; set; }

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000AFA RID: 2810 RVA: 0x00030DEA File Offset: 0x0002EFEA
		// (set) Token: 0x06000AFB RID: 2811 RVA: 0x00030DF2 File Offset: 0x0002EFF2
		public string DomainName { get; set; }

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000AFC RID: 2812 RVA: 0x00030DFB File Offset: 0x0002EFFB
		// (set) Token: 0x06000AFD RID: 2813 RVA: 0x00030E03 File Offset: 0x0002F003
		public string ReferenceRowsetName { get; set; }

		// Token: 0x06000AFE RID: 2814 RVA: 0x00030E0C File Offset: 0x0002F00C
		public TokenSplitTransformationProvider()
		{
			this.ReferenceRowsetName = "default";
			this.m_referenceRowsetSink = new TokenSplitTransformationProvider.ReferenceRowsetSink
			{
				Name = "ReferenceRowset",
				Provider = this
			};
			this.MaxSegments = 2;
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x00030E59 File Offset: 0x0002F059
		public void Initialize(IDomainManager domainManager, string domainName)
		{
			this.DomainName = domainName;
			this.m_domainId = domainManager.GetDomainId(domainName);
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000B00 RID: 2816 RVA: 0x00030E6F File Offset: 0x0002F06F
		public IList<IRowsetSink> RowsetSinks
		{
			get
			{
				return new IRowsetSink[] { this.m_referenceRowsetSink };
			}
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x00030E80 File Offset: 0x0002F080
		void IRowsetConsumer.RequestRowsets(IRowsetDistributor rowsetDistributor)
		{
			if (!string.IsNullOrEmpty(this.ReferenceRowsetName))
			{
				rowsetDistributor.RequestRowset(this.ReferenceRowsetName, this.m_referenceRowsetSink);
			}
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x00030EA1 File Offset: 0x0002F0A1
		public void Clear()
		{
			this.m_ahoCorasick = new AhoCorasick();
			this.m_tokenPairBloomFilter.Clear();
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x00030EB9 File Offset: 0x0002F0B9
		public static int GetHashCode1(int key)
		{
			return Utilities.GetHashCode(179, key);
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x00030EC6 File Offset: 0x0002F0C6
		public static int GetHashCode2(int key)
		{
			return Utilities.GetHashCode(1001, key);
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x00030ED3 File Offset: 0x0002F0D3
		public ISession CreateSession()
		{
			return new TokenSplitTransformationProvider.Session
			{
				m_ahoCorasickSession = this.m_ahoCorasick.CreateSession()
			};
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x00030EEC File Offset: 0x0002F0EC
		public void Match(ISession session, ITokenIdProvider tokenIdProvider, TokenSequence tokenSeq, out ArraySegment<TransformationMatch> transformationMatchList)
		{
			TokenSplitTransformationProvider.Session session2 = (TokenSplitTransformationProvider.Session)session;
			session.Reset();
			lock (this)
			{
				for (int i = 0; i < tokenSeq.Count; i++)
				{
					if (tokenIdProvider.GetDomainId(tokenSeq[i]) == this.m_domainId)
					{
						StringExtent token = tokenIdProvider.GetToken(tokenSeq[i]);
						int length = token.Length;
						this.m_ahoCorasick.ResetLookupForSegmentation<StringExtent>(session2.m_ahoCorasickSession, token);
						session2.m_tokenIdSegmentBuilder.Reset();
						session2.m_tokenIdSegmentBuilder.Add(tokenSeq[i]);
						TokenSequence tokenSequence = default(TokenSequence);
						while (this.m_ahoCorasick.GetNextSegmentation(session2.m_ahoCorasickSession, this.m_tokenPairBloomFilter))
						{
							int numSegments = this.m_ahoCorasick.GetNumSegments(session2.m_ahoCorasickSession);
							if (numSegments <= this.MaxSegments && numSegments != length)
							{
								if (tokenSequence.IsDefault<TokenSequence>())
								{
									tokenSequence = new TokenSequence(session2.m_tokenIdSegmentBuilder.ToSegment(session2.m_intAllocator));
								}
								session2.m_tokenIdSegmentBuilder2.Reset();
								while (this.m_ahoCorasick.GetNextSegment(session2.m_ahoCorasickSession))
								{
									session2.m_tokenIdSegmentBuilder2.Add(this.m_ahoCorasick.GetCurSegment(session2.m_ahoCorasickSession));
								}
								if (!tokenSequence.Equals(session2.m_tokenIdSegmentBuilder2))
								{
									session2.m_tranMatchListBuilder.Add(new TransformationMatch
									{
										Position = i,
										Transformation = new Transformation
										{
											From = tokenSequence,
											To = session2.m_tokenIdSegmentBuilder2.ToSegment(session2.m_intAllocator),
											Type = TransformationType.TokenSplit
										}
									});
								}
							}
						}
					}
				}
				transformationMatchList = session2.m_tranMatchListBuilder;
			}
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x000310E0 File Offset: 0x0002F2E0
		public IEnumerable<Transformation> Transformations(ITokenIdProvider tokenIdProvider)
		{
			yield break;
		}

		// Token: 0x04000427 RID: 1063
		private AhoCorasick m_ahoCorasick = new AhoCorasick();

		// Token: 0x04000428 RID: 1064
		private BloomFilter<int> m_tokenPairBloomFilter;

		// Token: 0x0400042C RID: 1068
		private TokenSplitTransformationProvider.ReferenceRowsetSink m_referenceRowsetSink;

		// Token: 0x0400042D RID: 1069
		private int m_domainId;

		// Token: 0x020001AA RID: 426
		[Serializable]
		private class TokenIdPair
		{
			// Token: 0x06000DF7 RID: 3575 RVA: 0x0003B878 File Offset: 0x00039A78
			public TokenIdPair(int i, StringExtent s)
			{
				this.Id = i;
				this.Token = s;
			}

			// Token: 0x04000707 RID: 1799
			public int Id;

			// Token: 0x04000708 RID: 1800
			public StringExtent Token;
		}

		// Token: 0x020001AB RID: 427
		private class Session : ISession
		{
			// Token: 0x06000DF8 RID: 3576 RVA: 0x0003B88E File Offset: 0x00039A8E
			public void Reset()
			{
				this.m_tokenIdSegmentBuilder.Reset();
				this.m_tokenIdSegmentBuilder2.Reset();
				this.m_intAllocator.Reset();
				this.m_tranMatchListBuilder.Reset();
				this.m_ahoCorasickSession.Reset();
			}

			// Token: 0x04000709 RID: 1801
			public ArraySegmentBuilder<int> m_tokenIdSegmentBuilder = new ArraySegmentBuilder<int>();

			// Token: 0x0400070A RID: 1802
			public ArraySegmentBuilder<int> m_tokenIdSegmentBuilder2 = new ArraySegmentBuilder<int>();

			// Token: 0x0400070B RID: 1803
			public BlockedSegmentArray<int> m_intAllocator = new BlockedSegmentArray<int>();

			// Token: 0x0400070C RID: 1804
			public ArraySegmentBuilder<TransformationMatch> m_tranMatchListBuilder = new ArraySegmentBuilder<TransformationMatch>();

			// Token: 0x0400070D RID: 1805
			public ISession m_ahoCorasickSession;
		}

		// Token: 0x020001AC RID: 428
		[Serializable]
		private class ReferenceRowsetSink : RowsetSinkBase
		{
			// Token: 0x1700028B RID: 651
			// (get) Token: 0x06000DFA RID: 3578 RVA: 0x0003B8FB File Offset: 0x00039AFB
			// (set) Token: 0x06000DFB RID: 3579 RVA: 0x0003B903 File Offset: 0x00039B03
			public TokenSplitTransformationProvider Provider { get; set; }

			// Token: 0x06000DFC RID: 3580 RVA: 0x0003B90C File Offset: 0x00039B0C
			public override IUpdateContext BeginUpdate(DataTable schemaTable)
			{
				return new TokenSplitTransformationProvider.ReferenceRowsetSink.UpdateContext
				{
					DomainName = this.Provider.DomainName
				};
			}

			// Token: 0x06000DFD RID: 3581 RVA: 0x0003B924 File Offset: 0x00039B24
			public override void Add(IUpdateContext _context, TokenSequence tokenSeq)
			{
				TokenSplitTransformationProvider.ReferenceRowsetSink.UpdateContext updateContext = (TokenSplitTransformationProvider.ReferenceRowsetSink.UpdateContext)_context;
				for (int i = 0; i < tokenSeq.Count; i++)
				{
					if (!updateContext.m_dictionaryTokenIds.Contains(tokenSeq[i]))
					{
						updateContext.m_dictionaryTokenIds.Add(tokenSeq[i]);
						updateContext.m_dictionaryTokens.Add(new TokenSplitTransformationProvider.TokenIdPair(tokenSeq[i], updateContext.TokenIdProvider.GetToken(tokenSeq[i]).AllocClone(updateContext.m_charSegmentAllocator)));
					}
					if (i > 0)
					{
						int hashCode = Utilities.GetHashCode(tokenSeq[i - 1], tokenSeq[i]);
						if (!updateContext.m_dictionaryTokenPairHashes.Contains(hashCode))
						{
							updateContext.m_dictionaryTokenPairHashes.Add(hashCode);
							updateContext.m_numDistTokenPairHashes++;
						}
					}
				}
			}

			// Token: 0x06000DFE RID: 3582 RVA: 0x0003B9F8 File Offset: 0x00039BF8
			public override void EndUpdate(IUpdateContext _context)
			{
				TokenSplitTransformationProvider.ReferenceRowsetSink.UpdateContext updateContext = (TokenSplitTransformationProvider.ReferenceRowsetSink.UpdateContext)_context;
				updateContext.m_dictionaryTokens.Sort(new Comparison<TokenSplitTransformationProvider.TokenIdPair>(this.Compare));
				AhoCorasick ahoCorasick = new AhoCorasick();
				ahoCorasick.BeginUpdate();
				for (int i = updateContext.m_dictionaryTokens.Count - 1; i >= 0; i--)
				{
					ahoCorasick.Add<StringExtent>(updateContext.m_dictionaryTokens[i].Token, updateContext.m_dictionaryTokens[i].Id);
				}
				ahoCorasick.EndUpdate();
				BloomFilter<int> bloomFilter = new BloomFilter<int>(updateContext.m_numDistTokenPairHashes, 0.01, new BloomFilter<int>.GetHashCodeDelegate(TokenSplitTransformationProvider.GetHashCode1), new BloomFilter<int>.GetHashCodeDelegate(TokenSplitTransformationProvider.GetHashCode2));
				foreach (int num in updateContext.m_dictionaryTokenPairHashes)
				{
					bloomFilter.Insert(num);
				}
				lock (this)
				{
					this.Provider.m_ahoCorasick = ahoCorasick;
					this.Provider.m_tokenPairBloomFilter = bloomFilter;
				}
				updateContext.Dispose();
			}

			// Token: 0x06000DFF RID: 3583 RVA: 0x0003BB28 File Offset: 0x00039D28
			private int Compare(TokenSplitTransformationProvider.TokenIdPair tip1, TokenSplitTransformationProvider.TokenIdPair tip2)
			{
				return tip1.Token.CompareTo(tip2.Token);
			}

			// Token: 0x020001C6 RID: 454
			private class UpdateContext : RowsetSinkBase.UpdateContextBase
			{
				// Token: 0x06000E68 RID: 3688 RVA: 0x0003D309 File Offset: 0x0003B509
				public override void Dispose()
				{
					this.m_dictionaryTokens = null;
					this.m_dictionaryTokenPairHashes = null;
					this.m_dictionaryTokenIds = null;
					base.Dispose();
				}

				// Token: 0x06000E69 RID: 3689 RVA: 0x0003D326 File Offset: 0x0003B526
				public override void Initialize(IRowsetManager rowsetManager, IDomainManager domainManager, ITokenIdProvider tokenIdProvider, RecordBinding recordBinding)
				{
					base.Initialize(rowsetManager, domainManager, tokenIdProvider, recordBinding);
					this.m_dictionaryTokenIds = new FastIntHashSet();
					this.m_dictionaryTokenPairHashes = new FastIntHashSet();
					this.m_numDistTokenPairHashes = 0;
					this.m_dictionaryTokens = new List<TokenSplitTransformationProvider.TokenIdPair>();
				}

				// Token: 0x04000781 RID: 1921
				public List<TokenSplitTransformationProvider.TokenIdPair> m_dictionaryTokens;

				// Token: 0x04000782 RID: 1922
				public FastIntHashSet m_dictionaryTokenPairHashes;

				// Token: 0x04000783 RID: 1923
				public int m_numDistTokenPairHashes;

				// Token: 0x04000784 RID: 1924
				public FastIntHashSet m_dictionaryTokenIds;
			}
		}
	}
}
