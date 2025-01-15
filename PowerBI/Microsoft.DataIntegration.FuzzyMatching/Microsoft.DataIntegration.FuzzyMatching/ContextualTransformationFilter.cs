using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.DataIntegration.FuzzyMatchingCommon;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000F6 RID: 246
	[Serializable]
	public class ContextualTransformationFilter : IRowsetConsumer, ITransformationFilter, IProviderInitialize, ISessionable
	{
		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x0002D52B File Offset: 0x0002B72B
		// (set) Token: 0x06000A04 RID: 2564 RVA: 0x0002D533 File Offset: 0x0002B733
		public int MaxTransformations { get; set; }

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000A05 RID: 2565 RVA: 0x0002D53C File Offset: 0x0002B73C
		// (set) Token: 0x06000A06 RID: 2566 RVA: 0x0002D544 File Offset: 0x0002B744
		public int MaxTransformationsPerToken { get; set; }

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000A07 RID: 2567 RVA: 0x0002D54D File Offset: 0x0002B74D
		// (set) Token: 0x06000A08 RID: 2568 RVA: 0x0002D555 File Offset: 0x0002B755
		public int MaxContextFreeTransformationsPerToken { get; set; }

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x0002D55E File Offset: 0x0002B75E
		// (set) Token: 0x06000A0A RID: 2570 RVA: 0x0002D566 File Offset: 0x0002B766
		public IComparer<TransformationMatch> Comparison { get; set; }

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x0002D56F File Offset: 0x0002B76F
		// (set) Token: 0x06000A0C RID: 2572 RVA: 0x0002D577 File Offset: 0x0002B777
		public string DomainName { get; set; }

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x0002D580 File Offset: 0x0002B780
		// (set) Token: 0x06000A0E RID: 2574 RVA: 0x0002D588 File Offset: 0x0002B788
		public string ReferenceRowsetName { get; set; }

		// Token: 0x06000A0F RID: 2575 RVA: 0x0002D594 File Offset: 0x0002B794
		public ContextualTransformationFilter()
		{
			this.ReferenceRowsetName = "default";
			this.m_referenceRowsetSink = new ContextualTransformationFilter.ReferenceRowsetSink
			{
				Name = "ReferenceRowset",
				Filter = this
			};
			this.MaxTransformations = int.MaxValue;
			this.MaxTransformationsPerToken = int.MaxValue;
			this.MaxContextFreeTransformationsPerToken = 0;
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x0002D5F7 File Offset: 0x0002B7F7
		public void Initialize(IDomainManager domainManager, string domainName)
		{
			this.DomainName = domainName;
			this.m_domainId = domainManager.GetDomainId(domainName);
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x0002D60D File Offset: 0x0002B80D
		public ISession CreateSession()
		{
			return new ContextualTransformationFilter.Session();
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000A12 RID: 2578 RVA: 0x0002D614 File Offset: 0x0002B814
		public IList<IRowsetSink> RowsetSinks
		{
			get
			{
				return new IRowsetSink[] { this.m_referenceRowsetSink };
			}
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x0002D625 File Offset: 0x0002B825
		void IRowsetConsumer.RequestRowsets(IRowsetDistributor rowsetDistributor)
		{
			if (!string.IsNullOrEmpty(this.ReferenceRowsetName))
			{
				rowsetDistributor.RequestRowset(this.ReferenceRowsetName, this.m_referenceRowsetSink);
			}
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x0002D646 File Offset: 0x0002B846
		public void Clear()
		{
			this.m_contextFilter.Clear();
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x0002D653 File Offset: 0x0002B853
		public void Prepare(ISession session, ITokenIdProvider tokenIdProvider, TokenSequence tokenSequence)
		{
			ContextualTransformationFilter.Session session2 = (ContextualTransformationFilter.Session)session;
			session2.m_tokenIdProvider = tokenIdProvider;
			session2.m_filterTokenSequence = tokenSequence;
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x0002D668 File Offset: 0x0002B868
		public bool AllowTransformations(ISession session, int fromTokenIndex)
		{
			return true;
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x0002D66B File Offset: 0x0002B86B
		public void FilterTransformations(ISession session, int fromTokenIndex, ArraySegment<TransformationMatch> transformationMatchList, out ArraySegment<TransformationMatch> filteredTransformationMatchList)
		{
			filteredTransformationMatchList = transformationMatchList;
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x0002D678 File Offset: 0x0002B878
		public void FilterTransformations(ISession session, ArraySegment<TransformationMatch> transformationMatchList, out ArraySegment<TransformationMatch> filteredTransformationMatchList)
		{
			ContextualTransformationFilter.Session session2 = (ContextualTransformationFilter.Session)session;
			int num = -1;
			for (int i = 0; i <= transformationMatchList.Count; i++)
			{
				if (transformationMatchList.Count == i || transformationMatchList.Array[transformationMatchList.Offset + i].Position != num)
				{
					if (num >= 0)
					{
						if (session2.m_tempTransformationMatchesPassing.Count > this.MaxTransformationsPerToken)
						{
							if (this.Comparison != null)
							{
								session2.m_tempTransformationMatchesPassing.Sort(this.Comparison);
							}
							session2.m_tempTransformationMatchesPassing.Count = this.MaxTransformationsPerToken;
						}
						for (int j = 0; j < session2.m_tempTransformationMatchesPassing.Count; j++)
						{
							session2.m_tempTransformationMatches.Add(session2.m_tempTransformationMatchesPassing[j]);
						}
						int num2 = Math.Min(this.MaxContextFreeTransformationsPerToken, this.MaxTransformationsPerToken - session2.m_tempTransformationMatchesPassing.Count);
						if (num2 > 0)
						{
							if (session2.m_tempTransformationMatchesFailing.Count > num2)
							{
								if (this.Comparison != null)
								{
									session2.m_tempTransformationMatchesFailing.Sort(this.Comparison);
								}
								session2.m_tempTransformationMatchesFailing.Count = num2;
							}
							for (int k = 0; k < session2.m_tempTransformationMatchesFailing.Count; k++)
							{
								session2.m_tempTransformationMatches.Add(session2.m_tempTransformationMatchesFailing[k]);
							}
						}
					}
					if (transformationMatchList.Count == i)
					{
						break;
					}
					num = transformationMatchList.Array[transformationMatchList.Offset + i].Position;
					session2.m_tempTransformationMatchesPassing.Reset();
					session2.m_tempTransformationMatchesFailing.Reset();
				}
				if (this.SatisfiesContext(session2, transformationMatchList, i))
				{
					session2.m_tempTransformationMatchesPassing.Add(transformationMatchList.Array[transformationMatchList.Offset + i]);
				}
				else
				{
					session2.m_tempTransformationMatchesFailing.Add(transformationMatchList.Array[transformationMatchList.Offset + i]);
				}
			}
			if (session2.m_tempTransformationMatches.Count > this.MaxTransformations)
			{
				if (this.Comparison != null)
				{
					session2.m_tempTransformationMatches.Sort(this.Comparison);
				}
				session2.m_tempTransformationMatches.Count = this.MaxTransformationsPerToken;
			}
			filteredTransformationMatchList = session2.m_transformationMatchAllocator.New(session2.m_tempTransformationMatches.Count);
			for (int l = 0; l < session2.m_tempTransformationMatches.Count; l++)
			{
				filteredTransformationMatchList.Array[filteredTransformationMatchList.Offset + l] = new TransformationMatch
				{
					Position = session2.m_tempTransformationMatches[l].Position,
					Transformation = session2.m_tempTransformationMatches[l].Transformation
				};
			}
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x0002D91B File Offset: 0x0002BB1B
		public bool AllowTransformation(ISession session, int fromTokenIndex, Transformation transformation)
		{
			return true;
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x0002D91E File Offset: 0x0002BB1E
		public bool SatisfiesContext(int tokenId1, int tokenId2)
		{
			return this.m_contextFilter.Contains(Utilities.Int32ToInt64(tokenId1, tokenId2));
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x0002D934 File Offset: 0x0002BB34
		public bool SatisfiesContext(ISession session, ArraySegment<TransformationMatch> tranMatchList, int tranMatchListPos)
		{
			ContextualTransformationFilter.Session session2 = (ContextualTransformationFilter.Session)session;
			int position = tranMatchList.Array[tranMatchList.Offset + tranMatchListPos].Position;
			int num = tranMatchList.Array[tranMatchList.Offset + tranMatchListPos].Transformation.To[0];
			if (position - 1 >= 0 && session2.m_tokenIdProvider.GetDomainId(session2.m_filterTokenSequence[position - 1]) == this.m_domainId)
			{
				if (this.SatisfiesContext(session2.m_filterTokenSequence[position - 1], num))
				{
					return true;
				}
				int num2 = tranMatchListPos - 1;
				while (num2 >= 0 && tranMatchList.Array[tranMatchList.Offset + num2].Position == tranMatchList.Array[tranMatchList.Offset + tranMatchListPos].Position - 1)
				{
					if (this.SatisfiesContext(tranMatchList.Array[tranMatchList.Offset + num2].Transformation.To[0], num))
					{
						return true;
					}
					num2--;
				}
			}
			if (position + 1 < session2.m_filterTokenSequence.Count && session2.m_tokenIdProvider.GetDomainId(session2.m_filterTokenSequence[position + 1]) == this.m_domainId)
			{
				if (this.SatisfiesContext(num, session2.m_filterTokenSequence[position + 1]))
				{
					return true;
				}
				int num3 = tranMatchListPos + 1;
				while (num3 < tranMatchList.Count && tranMatchList.Array[tranMatchList.Offset + num3].Position == tranMatchList.Array[tranMatchList.Offset + tranMatchListPos].Position + 1)
				{
					if (this.SatisfiesContext(num, tranMatchList.Array[tranMatchList.Offset + num3].Transformation.To[0]))
					{
						return true;
					}
					num3++;
				}
			}
			return false;
		}

		// Token: 0x040003D2 RID: 978
		private int m_domainId;

		// Token: 0x040003D3 RID: 979
		private FastInt64HashSet m_contextFilter = new FastInt64HashSet();

		// Token: 0x040003D4 RID: 980
		private ContextualTransformationFilter.ReferenceRowsetSink m_referenceRowsetSink;

		// Token: 0x02000192 RID: 402
		private class Session : ISession
		{
			// Token: 0x06000D5B RID: 3419 RVA: 0x00039118 File Offset: 0x00037318
			public Session()
			{
				this.m_filterTokenSequence = default(TokenSequence);
				this.m_tempTransformationMatchesPassing = new ArraySegmentBuilder<TransformationMatch>();
				this.m_tempTransformationMatchesFailing = new ArraySegmentBuilder<TransformationMatch>();
				this.m_tempTransformationMatches = new ArraySegmentBuilder<TransformationMatch>();
			}

			// Token: 0x06000D5C RID: 3420 RVA: 0x00039158 File Offset: 0x00037358
			public void Reset()
			{
				this.m_filterTokenSequence.Clear();
				this.m_tempTransformationMatchesPassing.Reset();
				this.m_tempTransformationMatchesFailing.Reset();
				this.m_tempTransformationMatches.Reset();
				this.m_transformationMatchAllocator.Reset();
			}

			// Token: 0x04000681 RID: 1665
			public ITokenIdProvider m_tokenIdProvider;

			// Token: 0x04000682 RID: 1666
			public TokenSequence m_filterTokenSequence;

			// Token: 0x04000683 RID: 1667
			public ArraySegmentBuilder<TransformationMatch> m_tempTransformationMatchesPassing;

			// Token: 0x04000684 RID: 1668
			public ArraySegmentBuilder<TransformationMatch> m_tempTransformationMatchesFailing;

			// Token: 0x04000685 RID: 1669
			public ArraySegmentBuilder<TransformationMatch> m_tempTransformationMatches;

			// Token: 0x04000686 RID: 1670
			public BlockedSegmentArray<TransformationMatch> m_transformationMatchAllocator = new BlockedSegmentArray<TransformationMatch>();
		}

		// Token: 0x02000193 RID: 403
		[Serializable]
		private class ReferenceRowsetSink : IRowsetSink, IRecordUpdate, IRecordContextUpdate
		{
			// Token: 0x17000275 RID: 629
			// (get) Token: 0x06000D5D RID: 3421 RVA: 0x00039191 File Offset: 0x00037391
			// (set) Token: 0x06000D5E RID: 3422 RVA: 0x00039199 File Offset: 0x00037399
			public string Name { get; set; }

			// Token: 0x17000276 RID: 630
			// (get) Token: 0x06000D5F RID: 3423 RVA: 0x000391A2 File Offset: 0x000373A2
			// (set) Token: 0x06000D60 RID: 3424 RVA: 0x000391AA File Offset: 0x000373AA
			public ContextualTransformationFilter Filter { get; set; }

			// Token: 0x06000D61 RID: 3425 RVA: 0x000391B3 File Offset: 0x000373B3
			public IUpdateContext BeginUpdate()
			{
				return this.BeginUpdate(null);
			}

			// Token: 0x06000D62 RID: 3426 RVA: 0x000391BC File Offset: 0x000373BC
			public IUpdateContext BeginUpdate(DataTable schemaTable)
			{
				return new ContextualTransformationFilter.ReferenceRowsetSink.UpdateContext
				{
					DomainName = this.Filter.DomainName
				};
			}

			// Token: 0x06000D63 RID: 3427 RVA: 0x000391D4 File Offset: 0x000373D4
			public void EndUpdate(IUpdateContext _updateContext)
			{
				ContextualTransformationFilter.ReferenceRowsetSink.UpdateContext updateContext = (ContextualTransformationFilter.ReferenceRowsetSink.UpdateContext)_updateContext;
				if (updateContext.m_transformationProviderSessionForAddRecord is IDisposable)
				{
					(updateContext.m_transformationProviderSessionForAddRecord as IDisposable).Dispose();
				}
				updateContext.Dispose();
			}

			// Token: 0x06000D64 RID: 3428 RVA: 0x0003920B File Offset: 0x0003740B
			void IRecordContextUpdate.RemoveRecordContext(IUpdateContext _updateContext, RecordContext recordContext)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000D65 RID: 3429 RVA: 0x00039212 File Offset: 0x00037412
			void IRecordUpdate.RemoveRecord(IUpdateContext _updateContext, IDataRecord record)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000D66 RID: 3430 RVA: 0x0003921C File Offset: 0x0003741C
			void IRecordContextUpdate.AddRecordContext(IUpdateContext _updateContext, RecordContext recordContext)
			{
				ContextualTransformationFilter.ReferenceRowsetSink.UpdateContext updateContext = (ContextualTransformationFilter.ReferenceRowsetSink.UpdateContext)_updateContext;
				this.AddRecord(updateContext, recordContext.TokenSequence.Tokens, recordContext.TransformationMatchList.ToTransformationMatchArraySegment(updateContext.m_tranMatchAllocator));
				updateContext.m_tranMatchAllocator.Reset();
				updateContext.m_transformationProviderSessionForAddRecord.Reset();
			}

			// Token: 0x06000D67 RID: 3431 RVA: 0x0003926C File Offset: 0x0003746C
			void IRecordUpdate.AddRecord(IUpdateContext _updateContext, IDataRecord record)
			{
				ContextualTransformationFilter.ReferenceRowsetSink.UpdateContext updateContext = (ContextualTransformationFilter.ReferenceRowsetSink.UpdateContext)_updateContext;
				updateContext.m_tempTokenizationTokenSeq.Reset();
				updateContext.m_tokenizerContext.Reset();
				updateContext.m_tokenizer.Tokenize(updateContext.m_tokenizerContext, updateContext.TokenIdProvider, updateContext.m_domainId, record, updateContext.m_tempTokenizationTokenSeq);
				ArraySegmentBuilder<int> tempTokenizationTokenSeq = updateContext.m_tempTokenizationTokenSeq;
				ArraySegment<TransformationMatch> arraySegment;
				updateContext.m_rightTransformationProvider.Match(updateContext.m_transformationProviderSessionForAddRecord, updateContext.TokenIdProvider, tempTokenizationTokenSeq, out arraySegment);
				this.AddRecord(updateContext, tempTokenizationTokenSeq, arraySegment);
				updateContext.m_transformationProviderSessionForAddRecord.Reset();
			}

			// Token: 0x06000D68 RID: 3432 RVA: 0x000392FC File Offset: 0x000374FC
			public void AddRecord(IUpdateContext _updateContext, TokenSequence tokenSeq, ArraySegment<TransformationMatch> transformations)
			{
				ContextualTransformationFilter.ReferenceRowsetSink.UpdateContext updateContext = (ContextualTransformationFilter.ReferenceRowsetSink.UpdateContext)_updateContext;
				updateContext.m_ruleApplier.Reset(tokenSeq, transformations);
				ContextualTransformationFilter filter = this.Filter;
				lock (filter)
				{
					while (updateContext.m_ruleApplier.GetNextDerivedTokenSequence(out updateContext.m_tempDerivedTokenSeq))
					{
						int num = 0;
						for (int i = 0; i < updateContext.m_tempDerivedTokenSeq.Count; i++)
						{
							if (updateContext.TokenIdProvider.GetDomainId(updateContext.m_tempDerivedTokenSeq[i]) == updateContext.m_domainId)
							{
								num++;
								if (i < updateContext.m_tempDerivedTokenSeq.Count - 1 && updateContext.TokenIdProvider.GetDomainId(updateContext.m_tempDerivedTokenSeq[i + 1]) == updateContext.m_domainId)
								{
									this.Filter.m_contextFilter.Add(Utilities.Int32ToInt64(updateContext.m_tempDerivedTokenSeq[i], updateContext.m_tempDerivedTokenSeq[i + 1]));
								}
							}
						}
					}
				}
			}

			// Token: 0x020001C0 RID: 448
			private class UpdateContext : IUpdateContext, IRecordUpdateContextInitialize, IDisposable
			{
				// Token: 0x06000E57 RID: 3671 RVA: 0x0003CACB File Offset: 0x0003ACCB
				public UpdateContext()
				{
					this.m_ruleApplier = new RuleApplier<TransformationMatch>(new RuleApplEnumerator<TransformationMatch>());
					this.m_tempDerivedTokenSeq = default(TokenSequence);
					this.m_tempTokenizationTokenSeq = new ArraySegmentBuilder<int>();
				}

				// Token: 0x06000E58 RID: 3672 RVA: 0x0003CB08 File Offset: 0x0003AD08
				public void Initialize(IRowsetManager rowsetManager, IDomainManager domainManager, ITokenIdProvider tokenIdProvider, RecordBinding recordBinding)
				{
					this.TokenIdProvider = tokenIdProvider;
					this.m_domainId = domainManager.GetDomainId(this.DomainName);
					this.m_rowsetBinding = recordBinding;
					this.m_tokenizer = domainManager.GetTokenizer(this.DomainName);
					this.m_tokenizer.Prepare(this.m_rowsetBinding.Schema, this.m_rowsetBinding.GetDomainBinding(this.DomainName), out this.m_tokenizerContext);
					this.m_rightTransformationProvider = domainManager.GetRightTransformationProvider(this.DomainName);
					if (this.m_transformationProviderSessionForAddRecord == null && this.m_rightTransformationProvider != null && this.m_rightTransformationProvider is ISessionable)
					{
						this.m_transformationProviderSessionForAddRecord = (this.m_rightTransformationProvider as ISessionable).CreateSession();
					}
				}

				// Token: 0x06000E59 RID: 3673 RVA: 0x0003CBBA File Offset: 0x0003ADBA
				public void Dispose()
				{
					this.m_transformationProviderSessionForAddRecord = null;
				}

				// Token: 0x04000750 RID: 1872
				public ITokenIdProvider TokenIdProvider;

				// Token: 0x04000751 RID: 1873
				public string DomainName;

				// Token: 0x04000752 RID: 1874
				public int m_domainId;

				// Token: 0x04000753 RID: 1875
				public RecordBinding m_rowsetBinding;

				// Token: 0x04000754 RID: 1876
				public ArraySegmentBuilder<int> m_tempTokenizationTokenSeq;

				// Token: 0x04000755 RID: 1877
				public ISession m_transformationProviderSessionForAddRecord;

				// Token: 0x04000756 RID: 1878
				public RuleApplier<TransformationMatch> m_ruleApplier;

				// Token: 0x04000757 RID: 1879
				public TokenSequence m_tempDerivedTokenSeq;

				// Token: 0x04000758 RID: 1880
				public IRecordTokenizer m_tokenizer;

				// Token: 0x04000759 RID: 1881
				public TokenizerContext m_tokenizerContext;

				// Token: 0x0400075A RID: 1882
				public ITransformationProvider m_rightTransformationProvider;

				// Token: 0x0400075B RID: 1883
				public BlockedSegmentArray<TransformationMatch> m_tranMatchAllocator = new BlockedSegmentArray<TransformationMatch>();
			}
		}
	}
}
