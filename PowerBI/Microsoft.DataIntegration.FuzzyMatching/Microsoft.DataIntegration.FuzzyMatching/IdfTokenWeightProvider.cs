using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.DataIntegration.FuzzyMatchingCommon;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000118 RID: 280
	[Serializable]
	public sealed class IdfTokenWeightProvider : ITokenWeightProvider, IProviderInitialize, IRowsetConsumer
	{
		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000BA5 RID: 2981 RVA: 0x00033405 File Offset: 0x00031605
		// (set) Token: 0x06000BA6 RID: 2982 RVA: 0x0003340D File Offset: 0x0003160D
		public double Scale { get; set; }

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000BA7 RID: 2983 RVA: 0x00033416 File Offset: 0x00031616
		// (set) Token: 0x06000BA8 RID: 2984 RVA: 0x0003341E File Offset: 0x0003161E
		public int DefaultWeight { get; set; }

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000BA9 RID: 2985 RVA: 0x00033427 File Offset: 0x00031627
		// (set) Token: 0x06000BAA RID: 2986 RVA: 0x0003342F File Offset: 0x0003162F
		public string DomainName { get; set; }

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000BAB RID: 2987 RVA: 0x00033438 File Offset: 0x00031638
		// (set) Token: 0x06000BAC RID: 2988 RVA: 0x00033440 File Offset: 0x00031640
		public string ReferenceRowsetName { get; set; }

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000BAD RID: 2989 RVA: 0x00033449 File Offset: 0x00031649
		// (set) Token: 0x06000BAE RID: 2990 RVA: 0x00033451 File Offset: 0x00031651
		public string CustomTokenWeightsRowsetName { get; set; }

		// Token: 0x06000BAF RID: 2991 RVA: 0x0003345C File Offset: 0x0003165C
		public IdfTokenWeightProvider()
		{
			this.Scale = 1.0;
			this.DefaultWeight = -1;
			this.ReferenceRowsetName = "default";
			this.m_referenceRowsetSink = new IdfTokenWeightProvider.ReferenceRowsetSink
			{
				Name = "ReferenceRowset",
				Provider = this
			};
			this.CustomTokenWeightsRowsetName = string.Empty;
			this.m_customTokenWeightsRowsetSink = new IdfTokenWeightProvider.CustomTokenWeightsRowsetSink
			{
				Name = "CustomTokenWeightsRowset",
				Provider = this
			};
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x000334EB File Offset: 0x000316EB
		public void Initialize(IDomainManager domainManager, string domainName)
		{
			this.DomainName = domainName;
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x000334F4 File Offset: 0x000316F4
		private void AssertMode(FLMode mode)
		{
			if (this.m_mode != mode)
			{
				throw new InvalidOperationException(string.Format(StringResources.InvalidMode, Enum.GetName(typeof(FLMode), mode)));
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000BB2 RID: 2994 RVA: 0x00033524 File Offset: 0x00031724
		public IList<IRowsetSink> RowsetSinks
		{
			get
			{
				return new IRowsetSink[] { this.m_referenceRowsetSink, this.m_customTokenWeightsRowsetSink };
			}
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0003353E File Offset: 0x0003173E
		public void RequestRowsets(IRowsetDistributor rowsetDistributor)
		{
			if (!string.IsNullOrEmpty(this.ReferenceRowsetName))
			{
				rowsetDistributor.RequestRowset(this.ReferenceRowsetName, this.m_referenceRowsetSink);
			}
			if (!string.IsNullOrEmpty(this.CustomTokenWeightsRowsetName))
			{
				rowsetDistributor.RequestRowset(this.CustomTokenWeightsRowsetName, this.m_customTokenWeightsRowsetSink);
			}
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x00033580 File Offset: 0x00031780
		public double GetPreciseWeight(ITokenIdProvider tokenIdProvider, int tokenId)
		{
			int num;
			double num3;
			if (this.m_customWeights.Count == 0 || !this.m_customWeights.TryGetValue(tokenId, out num))
			{
				int num2;
				if (!this.m_tokenFreqs.TryGetValue(tokenId, out num2))
				{
					num3 = this.GetDefaultWeight();
				}
				else
				{
					num3 = Math.Max(1.0, this.Scale * Math.Log((double)this.TotalCardinality / (double)num2));
				}
			}
			else
			{
				num3 = (double)num;
			}
			return num3;
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x000335EE File Offset: 0x000317EE
		private double GetDefaultWeight()
		{
			if (this.DefaultWeight > 0)
			{
				return (double)this.DefaultWeight;
			}
			return Math.Max(1.0, this.Scale * Math.Log((double)this.TotalCardinality / 2.0));
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0003362C File Offset: 0x0003182C
		public int GetWeight(ITokenIdProvider tokenIdProvider, int tokenId)
		{
			return Math.Max(1, (int)Math.Round(this.GetPreciseWeight(tokenIdProvider, tokenId), 0));
		}

		// Token: 0x04000468 RID: 1128
		private FLMode m_mode;

		// Token: 0x04000469 RID: 1129
		private int TotalCardinality;

		// Token: 0x0400046A RID: 1130
		private FastIntToIntHash m_tokenFreqs = new FastIntToIntHash();

		// Token: 0x0400046B RID: 1131
		private FastIntToIntHash m_customWeights = new FastIntToIntHash();

		// Token: 0x04000470 RID: 1136
		private IdfTokenWeightProvider.ReferenceRowsetSink m_referenceRowsetSink;

		// Token: 0x04000472 RID: 1138
		private IdfTokenWeightProvider.CustomTokenWeightsRowsetSink m_customTokenWeightsRowsetSink;

		// Token: 0x020001B7 RID: 439
		[Serializable]
		private class CustomTokenWeightsRowsetSink : IRowsetSink, IRecordUpdate
		{
			// Token: 0x17000296 RID: 662
			// (get) Token: 0x06000E31 RID: 3633 RVA: 0x0003C32C File Offset: 0x0003A52C
			// (set) Token: 0x06000E32 RID: 3634 RVA: 0x0003C334 File Offset: 0x0003A534
			public string Name { get; set; }

			// Token: 0x06000E33 RID: 3635 RVA: 0x0003C33D File Offset: 0x0003A53D
			public IUpdateContext BeginUpdate(DataTable schemaTable)
			{
				return new IdfTokenWeightProvider.CustomTokenWeightsRowsetSink.UpdateContext
				{
					DomainName = this.Provider.DomainName
				};
			}

			// Token: 0x06000E34 RID: 3636 RVA: 0x0003C355 File Offset: 0x0003A555
			public void EndUpdate(IUpdateContext context)
			{
			}

			// Token: 0x06000E35 RID: 3637 RVA: 0x0003C358 File Offset: 0x0003A558
			public void AddRecord(IUpdateContext context, IDataRecord record)
			{
				IdfTokenWeightProvider.CustomTokenWeightsRowsetSink.UpdateContext updateContext = (IdfTokenWeightProvider.CustomTokenWeightsRowsetSink.UpdateContext)context;
				int num;
				int num2;
				if (record.FieldCount == 2)
				{
					num = -1;
					num2 = 1;
				}
				else
				{
					if (record.FieldCount != 3)
					{
						throw new ArgumentException("Record must have schema {Token string, Weight int} or {DomainId int, Token string, Weight int}");
					}
					num = 0;
					num2 = 2;
				}
				int num3 = updateContext.DomainId;
				if (num >= 0)
				{
					num3 = this.ReadInt(record, num);
				}
				updateContext.m_tokenizerContext.Reset();
				foreach (StringExtent stringExtent in updateContext.m_tokenizer.Tokenize(updateContext.m_tokenizerContext, record))
				{
					int orCreateTokenId = updateContext.TokenIdProvider.GetOrCreateTokenId(stringExtent, num3);
					int num4 = this.ReadInt(record, num2);
					IdfTokenWeightProvider provider = this.Provider;
					lock (provider)
					{
						this.Provider.m_customWeights[orCreateTokenId] = num4;
						break;
					}
				}
			}

			// Token: 0x06000E36 RID: 3638 RVA: 0x0003C454 File Offset: 0x0003A654
			private int ReadInt(IDataRecord record, int columnOrdinal)
			{
				object obj = record[columnOrdinal];
				if (!(obj is int))
				{
					return int.Parse(obj.ToString());
				}
				return (int)obj;
			}

			// Token: 0x06000E37 RID: 3639 RVA: 0x0003C483 File Offset: 0x0003A683
			public void RemoveRecord(IUpdateContext context, IDataRecord r)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0400073B RID: 1851
			public IdfTokenWeightProvider Provider;

			// Token: 0x020001C7 RID: 455
			private class UpdateContext : IUpdateContext, IRecordUpdateContextInitialize
			{
				// Token: 0x06000E6B RID: 3691 RVA: 0x0003D364 File Offset: 0x0003B564
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

				// Token: 0x04000785 RID: 1925
				public string DomainName;

				// Token: 0x04000786 RID: 1926
				public ITokenIdProvider TokenIdProvider;

				// Token: 0x04000787 RID: 1927
				public int DomainId;

				// Token: 0x04000788 RID: 1928
				public IRecordTokenizer m_tokenizer;

				// Token: 0x04000789 RID: 1929
				public TokenizerContext m_tokenizerContext;
			}
		}

		// Token: 0x020001B8 RID: 440
		[Serializable]
		private class ReferenceRowsetSink : IRowsetSink, IRecordUpdate, IRecordContextUpdate
		{
			// Token: 0x17000297 RID: 663
			// (get) Token: 0x06000E39 RID: 3641 RVA: 0x0003C492 File Offset: 0x0003A692
			// (set) Token: 0x06000E3A RID: 3642 RVA: 0x0003C49A File Offset: 0x0003A69A
			public string Name { get; set; }

			// Token: 0x06000E3B RID: 3643 RVA: 0x0003C4A3 File Offset: 0x0003A6A3
			public IUpdateContext BeginUpdate()
			{
				return this.BeginUpdate(null);
			}

			// Token: 0x06000E3C RID: 3644 RVA: 0x0003C4AC File Offset: 0x0003A6AC
			public IUpdateContext BeginUpdate(DataTable schemaTable)
			{
				return new IdfTokenWeightProvider.ReferenceRowsetSink.UpdateContext
				{
					DomainName = this.Provider.DomainName
				};
			}

			// Token: 0x06000E3D RID: 3645 RVA: 0x0003C4C4 File Offset: 0x0003A6C4
			public void EndUpdate(IUpdateContext context)
			{
			}

			// Token: 0x06000E3E RID: 3646 RVA: 0x0003C4C8 File Offset: 0x0003A6C8
			public void AddRecordContext(IUpdateContext _updateContext, RecordContext recordContext)
			{
				IdfTokenWeightProvider.ReferenceRowsetSink.UpdateContext updateContext = (IdfTokenWeightProvider.ReferenceRowsetSink.UpdateContext)_updateContext;
				updateContext.distinctIds.Clear();
				for (int i = 0; i < recordContext.TokenSequence.Tokens.Count; i++)
				{
					int num = recordContext.TokenSequence.Tokens[i];
					if (!updateContext.distinctIds.Contains(num))
					{
						updateContext.distinctIds.Add(num);
					}
				}
				IdfTokenWeightProvider provider = this.Provider;
				lock (provider)
				{
					this.Provider.TotalCardinality++;
					foreach (int num2 in updateContext.distinctIds)
					{
						int num3;
						if (!this.Provider.m_tokenFreqs.TryGetValue(num2, out num3))
						{
							num3 = 0;
						}
						this.Provider.m_tokenFreqs[num2] = num3 + 1;
					}
				}
			}

			// Token: 0x06000E3F RID: 3647 RVA: 0x0003C5D4 File Offset: 0x0003A7D4
			public void RemoveRecordContext(IUpdateContext _updateContext, RecordContext recordContext)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000E40 RID: 3648 RVA: 0x0003C5DC File Offset: 0x0003A7DC
			public void AddRecord(IUpdateContext _updateContext, IDataRecord record)
			{
				IdfTokenWeightProvider.ReferenceRowsetSink.UpdateContext updateContext = (IdfTokenWeightProvider.ReferenceRowsetSink.UpdateContext)_updateContext;
				IdfTokenWeightProvider provider = this.Provider;
				lock (provider)
				{
					this.Provider.TotalCardinality++;
					foreach (int num in updateContext.DistinctTokenProvider.GetDistinctTokens(updateContext.TokenIdProvider, updateContext.DomainId, record))
					{
						int num2;
						if (!this.Provider.m_tokenFreqs.TryGetValue(num, out num2))
						{
							num2 = 0;
						}
						this.Provider.m_tokenFreqs[num] = num2 + 1;
					}
				}
			}

			// Token: 0x06000E41 RID: 3649 RVA: 0x0003C6A4 File Offset: 0x0003A8A4
			public void RemoveRecord(IUpdateContext _updateContext, IDataRecord record)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0400073D RID: 1853
			public IdfTokenWeightProvider Provider;

			// Token: 0x020001C8 RID: 456
			private class UpdateContext : IUpdateContext, IRecordUpdateContextInitialize
			{
				// Token: 0x06000E6D RID: 3693 RVA: 0x0003D40C File Offset: 0x0003B60C
				public void Initialize(IRowsetManager rowsetManager, IDomainManager domainManager, ITokenIdProvider tokenIdProvider, RecordBinding recordBinding)
				{
					this.TokenIdProvider = tokenIdProvider;
					this.DomainId = domainManager.GetDomainId(this.DomainName);
					this.DistinctTokenProvider = new DistinctTokenProvider(recordBinding, this.DomainName, domainManager.GetTokenizer(this.DomainName), domainManager.GetRightTransformationProvider(this.DomainName));
				}

				// Token: 0x0400078A RID: 1930
				public string DomainName;

				// Token: 0x0400078B RID: 1931
				public ITokenIdProvider TokenIdProvider;

				// Token: 0x0400078C RID: 1932
				public DistinctTokenProvider DistinctTokenProvider;

				// Token: 0x0400078D RID: 1933
				public int DomainId;

				// Token: 0x0400078E RID: 1934
				public List<int> distinctIds = new List<int>();
			}
		}
	}
}
