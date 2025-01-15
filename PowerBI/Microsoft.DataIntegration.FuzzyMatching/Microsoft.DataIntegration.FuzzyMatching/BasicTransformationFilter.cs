using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.DataIntegration.FuzzyMatchingCommon;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000F4 RID: 244
	[Serializable]
	public class BasicTransformationFilter : ITransformationFilter, ISessionable, IProviderInitialize, IRowsetConsumer
	{
		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060009D7 RID: 2519 RVA: 0x0002CC92 File Offset: 0x0002AE92
		// (set) Token: 0x060009D8 RID: 2520 RVA: 0x0002CC9A File Offset: 0x0002AE9A
		public int MaxTransformationsPerToken { get; set; }

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060009D9 RID: 2521 RVA: 0x0002CCA3 File Offset: 0x0002AEA3
		// (set) Token: 0x060009DA RID: 2522 RVA: 0x0002CCAB File Offset: 0x0002AEAB
		public int MaxTransformations { get; set; }

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060009DB RID: 2523 RVA: 0x0002CCB4 File Offset: 0x0002AEB4
		// (set) Token: 0x060009DC RID: 2524 RVA: 0x0002CCBC File Offset: 0x0002AEBC
		public Comparison<TransformationMatch> Comparison { get; set; }

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060009DD RID: 2525 RVA: 0x0002CCC5 File Offset: 0x0002AEC5
		// (set) Token: 0x060009DE RID: 2526 RVA: 0x0002CCCD File Offset: 0x0002AECD
		public string DomainName { get; set; }

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060009DF RID: 2527 RVA: 0x0002CCD6 File Offset: 0x0002AED6
		// (set) Token: 0x060009E0 RID: 2528 RVA: 0x0002CCDE File Offset: 0x0002AEDE
		public string FilteredTransformationsRowsetName { get; set; }

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060009E1 RID: 2529 RVA: 0x0002CCE7 File Offset: 0x0002AEE7
		// (set) Token: 0x060009E2 RID: 2530 RVA: 0x0002CCEF File Offset: 0x0002AEEF
		public int MinFromTokenLength { get; set; }

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060009E3 RID: 2531 RVA: 0x0002CCF8 File Offset: 0x0002AEF8
		// (set) Token: 0x060009E4 RID: 2532 RVA: 0x0002CD00 File Offset: 0x0002AF00
		public int MinToTokenLength { get; set; }

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060009E5 RID: 2533 RVA: 0x0002CD09 File Offset: 0x0002AF09
		// (set) Token: 0x060009E6 RID: 2534 RVA: 0x0002CD11 File Offset: 0x0002AF11
		public int MinPrefixLength { get; set; }

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060009E7 RID: 2535 RVA: 0x0002CD1A File Offset: 0x0002AF1A
		// (set) Token: 0x060009E8 RID: 2536 RVA: 0x0002CD22 File Offset: 0x0002AF22
		public bool FilterNumericTransformations { get; set; }

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060009E9 RID: 2537 RVA: 0x0002CD2B File Offset: 0x0002AF2B
		// (set) Token: 0x060009EA RID: 2538 RVA: 0x0002CD33 File Offset: 0x0002AF33
		public bool FilterTransformationsContainingDigits { get; set; }

		// Token: 0x060009EB RID: 2539 RVA: 0x0002CD3C File Offset: 0x0002AF3C
		public BasicTransformationFilter()
		{
			this.FilteredTransformationsRowsetName = string.Empty;
			this.m_filteredTransformationsRowsetSink = new BasicTransformationFilter.FilteredTransformationsRowsetSink
			{
				Name = "ExcludedTransformationsRowset",
				Filter = this
			};
			this.MinFromTokenLength = 0;
			this.MinToTokenLength = 0;
			this.MinPrefixLength = 0;
			this.MaxTransformationsPerToken = int.MaxValue;
			this.MaxTransformations = int.MaxValue;
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x0002CDD8 File Offset: 0x0002AFD8
		public void Initialize(IDomainManager domainManager, string domainName)
		{
			this.DomainName = domainName;
			this.m_domainId = domainManager.GetDomainId(domainName);
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x0002CDEE File Offset: 0x0002AFEE
		public void Clear()
		{
			this.m_excludedFromSequences.Clear();
			this.m_excludedTransformations.Clear();
			this.m_tokenIdSegmentBuilder.Reset();
			this.m_intAllocator.Reset();
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x0002CE1C File Offset: 0x0002B01C
		public ISession CreateSession()
		{
			return new BasicTransformationFilter.Session();
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060009EF RID: 2543 RVA: 0x0002CE23 File Offset: 0x0002B023
		public IList<IRowsetSink> RowsetSinks
		{
			get
			{
				return new IRowsetSink[] { this.m_filteredTransformationsRowsetSink };
			}
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x0002CE34 File Offset: 0x0002B034
		void IRowsetConsumer.RequestRowsets(IRowsetDistributor rowsetDistributor)
		{
			if (!string.IsNullOrEmpty(this.FilteredTransformationsRowsetName))
			{
				rowsetDistributor.RequestRowset(this.FilteredTransformationsRowsetName, this.m_filteredTransformationsRowsetSink);
			}
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x0002CE55 File Offset: 0x0002B055
		public void Prepare(ISession session, ITokenIdProvider tokenIdProvider, TokenSequence tokenSequence)
		{
			BasicTransformationFilter.Session session2 = (BasicTransformationFilter.Session)session;
			session2.m_tokenIdProvider = tokenIdProvider;
			session2.m_tokenSequence = tokenSequence;
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x0002CE6C File Offset: 0x0002B06C
		public bool AllowTransformations(ISession session, int fromTokenIndex)
		{
			BasicTransformationFilter.Session session2 = (BasicTransformationFilter.Session)session;
			session2.m_tempFromTokenSequence.Array[session2.m_tempFromTokenSequence.Offset] = session2.m_tokenSequence[fromTokenIndex];
			if (this.m_excludedFromSequences.Count > 0 && this.m_excludedFromSequences.Contains(session2.m_tempFromTokenSequence))
			{
				return false;
			}
			if (this.MinFromTokenLength > 0 || this.FilterNumericTransformations || this.FilterTransformationsContainingDigits)
			{
				if (!session2.m_tokenIdProvider.SupportsGetToken)
				{
					throw new InvalidOperationException("The BasicTransformationFilter does not work if the token id provider does not support GetToken().");
				}
				StringExtent token = session2.m_tokenIdProvider.GetToken(session2.m_tokenSequence[fromTokenIndex]);
				if (token.Length < this.MinFromTokenLength)
				{
					return false;
				}
				if (this.FilterTransformationsContainingDigits && BasicTransformationFilter.ContainsDigit(token))
				{
					return false;
				}
				if (this.FilterNumericTransformations && BasicTransformationFilter.ContainsOnlyDigits(token))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x0002CF48 File Offset: 0x0002B148
		protected static bool ContainsDigit(StringExtent token)
		{
			for (int i = 0; i < token.Length; i++)
			{
				if (char.IsDigit(token[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x0002CF7C File Offset: 0x0002B17C
		protected static bool ContainsOnlyDigits(StringExtent token)
		{
			for (int i = 0; i < token.Length; i++)
			{
				if (!char.IsDigit(token[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x0002CFB0 File Offset: 0x0002B1B0
		public bool AllowTransformation(ISession session, int fromTokenIndex, Transformation transformation)
		{
			BasicTransformationFilter.Session session2 = (BasicTransformationFilter.Session)session;
			if (this.m_excludedTransformations.Count > 0 && this.m_excludedTransformations.Contains(transformation))
			{
				return false;
			}
			if (this.MinFromTokenLength > 0 || this.MinToTokenLength > 0 || this.MinPrefixLength > 0)
			{
				if (!session2.m_tokenIdProvider.SupportsGetToken)
				{
					throw new InvalidOperationException("The BasicTransformationFilter does not work if the token id provider does not support GetToken().");
				}
				if (this.MinFromTokenLength > 0)
				{
					for (int i = 0; i < transformation.From.Count; i++)
					{
						if (session2.m_tokenIdProvider.GetToken(transformation.From[i]).Length < this.MinFromTokenLength)
						{
							return false;
						}
					}
				}
				if (this.MinToTokenLength > 0)
				{
					if (transformation.To.Count == 0)
					{
						return false;
					}
					for (int j = 0; j < transformation.To.Count; j++)
					{
						if (session2.m_tokenIdProvider.GetToken(transformation.To[j]).Length < this.MinToTokenLength)
						{
							return false;
						}
					}
				}
				if (this.MinPrefixLength > 0)
				{
					if (transformation.From.Count != transformation.To.Count)
					{
						return false;
					}
					for (int k = 0; k < transformation.From.Count; k++)
					{
						StringExtent token = session2.m_tokenIdProvider.GetToken(transformation.From[k]);
						StringExtent token2 = session2.m_tokenIdProvider.GetToken(transformation.To[k]);
						int num;
						int num2;
						this.IsPrefix(token, token2, out num, out num2);
						if (num < this.MinPrefixLength)
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x0002D152 File Offset: 0x0002B352
		public bool IsPrefix(StringExtent token1, StringExtent token2, out int prefixMatchLength, out int maxLength)
		{
			return Utilities.IsPrefix(token1, token2, false, out prefixMatchLength, out maxLength);
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x0002D16C File Offset: 0x0002B36C
		public void FilterTransformations(ISession session, int fromTokenIndex, ArraySegment<TransformationMatch> transformationMatchList, out ArraySegment<TransformationMatch> filteredTransformationMatchList)
		{
			BasicTransformationFilter.Session session2 = (BasicTransformationFilter.Session)session;
			this.Filter(session2, transformationMatchList, this.MaxTransformationsPerToken, out filteredTransformationMatchList);
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x0002D190 File Offset: 0x0002B390
		public void FilterTransformations(ISession session, ArraySegment<TransformationMatch> transformationMatchList, out ArraySegment<TransformationMatch> filteredTransformationMatchList)
		{
			BasicTransformationFilter.Session session2 = (BasicTransformationFilter.Session)session;
			this.Filter(session2, transformationMatchList, this.MaxTransformations, out filteredTransformationMatchList);
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x0002D1B4 File Offset: 0x0002B3B4
		private void Filter(BasicTransformationFilter.Session s, ArraySegment<TransformationMatch> transformationMatchList, int k, out ArraySegment<TransformationMatch> filteredTransformationMatchList)
		{
			if (transformationMatchList.Count > k)
			{
				s.m_tempTransformationMatchesSort.Clear();
				for (int i = 0; i < transformationMatchList.Count; i++)
				{
					s.m_tempTransformationMatchesSort.Add(transformationMatchList.Array[transformationMatchList.Offset + i]);
				}
				if (this.Comparison != null)
				{
					s.m_tempTransformationMatchesSort.Sort(this.Comparison);
				}
				filteredTransformationMatchList = s.m_transformationMatchAllocator.New(k);
				for (int j = 0; j < k; j++)
				{
					filteredTransformationMatchList.Array[filteredTransformationMatchList.Offset + j] = new TransformationMatch
					{
						Position = s.m_tempTransformationMatchesSort[j].Position,
						Transformation = s.m_tempTransformationMatchesSort[j].Transformation
					};
				}
				return;
			}
			filteredTransformationMatchList = transformationMatchList;
		}

		// Token: 0x040003BF RID: 959
		private int m_domainId;

		// Token: 0x040003C0 RID: 960
		private BasicTransformationFilter.FilteredTransformationsRowsetSink m_filteredTransformationsRowsetSink;

		// Token: 0x040003C1 RID: 961
		private ArraySegmentBuilder<int> m_tokenIdSegmentBuilder = new ArraySegmentBuilder<int>();

		// Token: 0x040003C2 RID: 962
		private BlockedSegmentArray<int> m_intAllocator = new BlockedSegmentArray<int>();

		// Token: 0x040003C3 RID: 963
		private HashSet<TokenSequence> m_excludedFromSequences = new HashSet<TokenSequence>(TokenSequenceComparer.Instance);

		// Token: 0x040003C4 RID: 964
		private HashSet<Transformation> m_excludedTransformations = new HashSet<Transformation>(TransformationComparer.Instance);

		// Token: 0x0200018E RID: 398
		private class Session : ISession
		{
			// Token: 0x06000D47 RID: 3399 RVA: 0x00038DD3 File Offset: 0x00036FD3
			public void Reset()
			{
				this.m_tokenIdProvider = null;
				this.m_tokenSequence = default(TokenSequence);
				this.m_tempTransformationMatchesSort.Clear();
				this.m_transformationMatchAllocator.Reset();
			}

			// Token: 0x04000673 RID: 1651
			public ITokenIdProvider m_tokenIdProvider;

			// Token: 0x04000674 RID: 1652
			public TokenSequence m_tokenSequence;

			// Token: 0x04000675 RID: 1653
			public TokenSequence m_tempFromTokenSequence = new TokenSequence(new int[1]);

			// Token: 0x04000676 RID: 1654
			public List<TransformationMatch> m_tempTransformationMatchesSort = new List<TransformationMatch>();

			// Token: 0x04000677 RID: 1655
			public BlockedSegmentArray<TransformationMatch> m_transformationMatchAllocator = new BlockedSegmentArray<TransformationMatch>();
		}

		// Token: 0x0200018F RID: 399
		[Serializable]
		private class FilteredTransformationsRowsetSink : IRowsetSink, IRecordUpdate
		{
			// Token: 0x17000272 RID: 626
			// (get) Token: 0x06000D49 RID: 3401 RVA: 0x00038E2D File Offset: 0x0003702D
			// (set) Token: 0x06000D4A RID: 3402 RVA: 0x00038E35 File Offset: 0x00037035
			public string Name { get; set; }

			// Token: 0x06000D4B RID: 3403 RVA: 0x00038E3E File Offset: 0x0003703E
			public IUpdateContext BeginUpdate(DataTable schemaTable)
			{
				return new BasicTransformationFilter.FilteredTransformationsRowsetSink.UpdateContext
				{
					DomainName = this.Filter.DomainName
				};
			}

			// Token: 0x06000D4C RID: 3404 RVA: 0x00038E56 File Offset: 0x00037056
			public void EndUpdate(IUpdateContext context)
			{
			}

			// Token: 0x06000D4D RID: 3405 RVA: 0x00038E58 File Offset: 0x00037058
			public void AddRecord(IUpdateContext context, IDataRecord record)
			{
				BasicTransformationFilter.FilteredTransformationsRowsetSink.UpdateContext updateContext = (BasicTransformationFilter.FilteredTransformationsRowsetSink.UpdateContext)context;
				int num;
				int num2;
				int num3;
				if (record.FieldCount == 2)
				{
					num = -1;
					num2 = 0;
					num3 = 1;
				}
				else
				{
					if (record.FieldCount != 3)
					{
						throw new ArgumentException("Record must have schema {FromString string, ToString string} or {DomainId int, FromString string, ToString string}");
					}
					num = 0;
					num2 = 1;
					num3 = 2;
				}
				int domainId = updateContext.DomainId;
				if (num >= 0)
				{
					this.ReadInt(record, num);
				}
				updateContext.m_tokenizerContext.Reset();
				Record record2 = new Record
				{
					Values = new object[1]
				};
				BasicTransformationFilter filter = this.Filter;
				lock (filter)
				{
					record2.Values[0] = record[num2];
					TokenSequence tokenSequence = TokenSequence.Create(updateContext.m_tokenizer.Tokenize(updateContext.m_tokenizerContext, record2), updateContext.DomainId, updateContext.TokenIdProvider, this.Filter.m_tokenIdSegmentBuilder, this.Filter.m_intAllocator);
					if (record.IsDBNull(num3))
					{
						this.Filter.m_excludedFromSequences.Add(tokenSequence);
					}
					else
					{
						record2.Values[0] = record[num3];
						TokenSequence tokenSequence2 = TokenSequence.Create(updateContext.m_tokenizer.Tokenize(updateContext.m_tokenizerContext, record2), updateContext.DomainId, updateContext.TokenIdProvider, this.Filter.m_tokenIdSegmentBuilder, this.Filter.m_intAllocator);
						this.Filter.m_excludedTransformations.Add(new Transformation
						{
							From = tokenSequence,
							To = tokenSequence2
						});
					}
				}
			}

			// Token: 0x06000D4E RID: 3406 RVA: 0x00038FDC File Offset: 0x000371DC
			private int ReadInt(IDataRecord record, int columnOrdinal)
			{
				object obj = record[columnOrdinal];
				if (!(obj is int))
				{
					return int.Parse(obj.ToString());
				}
				return (int)obj;
			}

			// Token: 0x06000D4F RID: 3407 RVA: 0x0003900B File Offset: 0x0003720B
			public void RemoveRecord(IUpdateContext context, IDataRecord r)
			{
				throw new NotImplementedException();
			}

			// Token: 0x04000679 RID: 1657
			public BasicTransformationFilter Filter;

			// Token: 0x020001BF RID: 447
			private class UpdateContext : IUpdateContext, IRecordUpdateContextInitialize
			{
				// Token: 0x06000E55 RID: 3669 RVA: 0x0003CA24 File Offset: 0x0003AC24
				public void Initialize(IRowsetManager rowsetManager, IDomainManager domainManager, ITokenIdProvider tokenIdProvider, RecordBinding recordBinding)
				{
					this.TokenIdProvider = tokenIdProvider;
					this.DomainId = domainManager.GetDomainId(this.DomainName);
					DomainBinding domainBinding = new DomainBinding
					{
						DomainName = this.DomainName
					};
					domainBinding.Columns.Add(new Column
					{
						Ordinal = 0
					});
					DataTable dataTable = new DataTable();
					dataTable.Columns.Add("Token1", typeof(string));
					this.m_tokenizer = domainManager.GetTokenizer(this.DomainName);
					this.m_tokenizer.Prepare(dataTable.CreateDataReader().GetSchemaTable(), domainBinding, out this.m_tokenizerContext);
				}

				// Token: 0x0400074B RID: 1867
				public string DomainName;

				// Token: 0x0400074C RID: 1868
				public ITokenIdProvider TokenIdProvider;

				// Token: 0x0400074D RID: 1869
				public int DomainId;

				// Token: 0x0400074E RID: 1870
				public IRecordTokenizer m_tokenizer;

				// Token: 0x0400074F RID: 1871
				public TokenizerContext m_tokenizerContext;
			}
		}
	}
}
