using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using Microsoft.DataIntegration.FuzzyMatchingCommon;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Data;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000107 RID: 263
	[Serializable]
	public class StaticTransformationProvider : ITransformationProvider, IProviderInitialize, IRowsetConsumer, IMemoryUsage, ISessionable
	{
		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000AD1 RID: 2769 RVA: 0x000307FA File Offset: 0x0002E9FA
		// (set) Token: 0x06000AD2 RID: 2770 RVA: 0x00030802 File Offset: 0x0002EA02
		public string DomainName { get; set; }

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000AD3 RID: 2771 RVA: 0x0003080B File Offset: 0x0002EA0B
		// (set) Token: 0x06000AD4 RID: 2772 RVA: 0x00030813 File Offset: 0x0002EA13
		public string ContextDomainName { get; set; }

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000AD5 RID: 2773 RVA: 0x0003081C File Offset: 0x0002EA1C
		// (set) Token: 0x06000AD6 RID: 2774 RVA: 0x00030824 File Offset: 0x0002EA24
		public string TransformationsRowsetName { get; set; }

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000AD7 RID: 2775 RVA: 0x0003082D File Offset: 0x0002EA2D
		// (set) Token: 0x06000AD8 RID: 2776 RVA: 0x00030835 File Offset: 0x0002EA35
		public string ContextColumnName { get; set; }

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000AD9 RID: 2777 RVA: 0x0003083E File Offset: 0x0002EA3E
		// (set) Token: 0x06000ADA RID: 2778 RVA: 0x00030846 File Offset: 0x0002EA46
		public string FromColumnName { get; set; }

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000ADB RID: 2779 RVA: 0x0003084F File Offset: 0x0002EA4F
		// (set) Token: 0x06000ADC RID: 2780 RVA: 0x00030857 File Offset: 0x0002EA57
		public string ToColumnName { get; set; }

		// Token: 0x06000ADD RID: 2781 RVA: 0x00030860 File Offset: 0x0002EA60
		public StaticTransformationProvider()
		{
			this.TransformationsRowsetName = "";
			this.m_transformationsRowsetSink = new StaticTransformationProvider.TransformationsRowsetSink
			{
				Name = "TransformationsRowset",
				Provider = this
			};
			this.m_prefixTree = new TranPrefixTree();
			this.m_tokenIdAllocator = new BlockedSegmentArray<int>();
			this.m_transformationMetadataAllocator = new BlockedSegmentArray<byte>();
			this.ContextColumnName = string.Empty;
			this.FromColumnName = "From";
			this.ToColumnName = "To";
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x000308DD File Offset: 0x0002EADD
		public void Initialize(IDomainManager domainManager, string domainName)
		{
			this.DomainName = domainName;
			if (string.IsNullOrEmpty(this.ContextDomainName))
			{
				this.ContextDomainName = domainName;
			}
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x000308FA File Offset: 0x0002EAFA
		public void Clear()
		{
			this.m_prefixTree = new TranPrefixTree();
			this.m_tokenIdAllocator = new BlockedSegmentArray<int>();
			this.m_transformationMetadataAllocator = new BlockedSegmentArray<byte>();
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x0003091D File Offset: 0x0002EB1D
		public ISession CreateSession()
		{
			return new StaticTransformationProvider.Session();
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000AE1 RID: 2785 RVA: 0x00030924 File Offset: 0x0002EB24
		public long MemoryUsage
		{
			get
			{
				return this.m_prefixTree.MemoryUsage + this.m_tokenIdAllocator.MemoryUsage + this.m_transformationMetadataAllocator.MemoryUsage;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000AE2 RID: 2786 RVA: 0x00030949 File Offset: 0x0002EB49
		public IList<IRowsetSink> RowsetSinks
		{
			get
			{
				return new IRowsetSink[] { this.m_transformationsRowsetSink };
			}
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x0003095A File Offset: 0x0002EB5A
		public void RequestRowsets(IRowsetDistributor rowsetDistributor)
		{
			if (!string.IsNullOrEmpty(this.TransformationsRowsetName))
			{
				rowsetDistributor.RequestRowset(this.TransformationsRowsetName, this.m_transformationsRowsetSink);
			}
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x0003097C File Offset: 0x0002EB7C
		public void Match(ISession session, ITokenIdProvider tokenIdProvider, TokenSequence tokenSeq, out ArraySegment<TransformationMatch> transformationMatchList)
		{
			StaticTransformationProvider.Session session2 = (StaticTransformationProvider.Session)session;
			session.Reset();
			this.m_prefixTree.Lookup(tokenSeq, session2.m_tokenIdAllocator, session2.m_tranMatchListBuilder, out transformationMatchList);
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x000309B0 File Offset: 0x0002EBB0
		public IEnumerable<Transformation> Transformations(ITokenIdProvider tokenIdProvider)
		{
			return this.m_prefixTree.Transformations(tokenIdProvider);
		}

		// Token: 0x04000417 RID: 1047
		private TranPrefixTree m_prefixTree;

		// Token: 0x04000418 RID: 1048
		private BlockedSegmentArray<int> m_tokenIdAllocator;

		// Token: 0x04000419 RID: 1049
		private BlockedSegmentArray<byte> m_transformationMetadataAllocator;

		// Token: 0x04000420 RID: 1056
		private StaticTransformationProvider.TransformationsRowsetSink m_transformationsRowsetSink;

		// Token: 0x020001A3 RID: 419
		private class Session : ISession
		{
			// Token: 0x06000DD3 RID: 3539 RVA: 0x0003B1B8 File Offset: 0x000393B8
			public void Reset()
			{
				this.m_tokenIdAllocator.Reset();
				this.m_tranMatchListBuilder.Reset();
			}

			// Token: 0x040006EA RID: 1770
			public BlockedSegmentArray<int> m_tokenIdAllocator = new BlockedSegmentArray<int>();

			// Token: 0x040006EB RID: 1771
			public ArraySegmentBuilder<TransformationMatch> m_tranMatchListBuilder = new ArraySegmentBuilder<TransformationMatch>();
		}

		// Token: 0x020001A4 RID: 420
		[Serializable]
		internal class TransformationsRowsetSink : IRowsetSink, IRecordUpdate
		{
			// Token: 0x17000286 RID: 646
			// (get) Token: 0x06000DD5 RID: 3541 RVA: 0x0003B1EE File Offset: 0x000393EE
			// (set) Token: 0x06000DD6 RID: 3542 RVA: 0x0003B1F6 File Offset: 0x000393F6
			public string Name { get; set; }

			// Token: 0x17000287 RID: 647
			// (get) Token: 0x06000DD7 RID: 3543 RVA: 0x0003B1FF File Offset: 0x000393FF
			// (set) Token: 0x06000DD8 RID: 3544 RVA: 0x0003B207 File Offset: 0x00039407
			public StaticTransformationProvider Provider { get; set; }

			// Token: 0x06000DD9 RID: 3545 RVA: 0x0003B210 File Offset: 0x00039410
			public IUpdateContext BeginUpdate(DataTable schemaTable)
			{
				this.Provider.m_prefixTree.BeginUpdate();
				return new StaticTransformationProvider.TransformationsRowsetSink.UpdateContext
				{
					Provider = this.Provider,
					DomainName = this.Provider.DomainName,
					ContextDomainName = this.Provider.ContextDomainName,
					RecordSchema = schemaTable
				};
			}

			// Token: 0x06000DDA RID: 3546 RVA: 0x0003B268 File Offset: 0x00039468
			public void EndUpdate(IUpdateContext _updateContext)
			{
				StaticTransformationProvider.TransformationsRowsetSink.UpdateContext updateContext = (StaticTransformationProvider.TransformationsRowsetSink.UpdateContext)_updateContext;
				StaticTransformationProvider provider = this.Provider;
				lock (provider)
				{
					this.Provider.m_prefixTree.EndUpdate();
				}
				updateContext.Dispose();
			}

			// Token: 0x06000DDB RID: 3547 RVA: 0x0003B2B8 File Offset: 0x000394B8
			public void RemoveRecord(IUpdateContext _updateContext, IDataRecord record)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000DDC RID: 3548 RVA: 0x0003B2C0 File Offset: 0x000394C0
			public void AddRecord(IUpdateContext _updateContext, IDataRecord transformationRecord)
			{
				StaticTransformationProvider.TransformationsRowsetSink.UpdateContext updateContext = (StaticTransformationProvider.TransformationsRowsetSink.UpdateContext)_updateContext;
				this.AddTransformation(updateContext, updateContext.CreateTransformation(transformationRecord));
			}

			// Token: 0x06000DDD RID: 3549 RVA: 0x0003B2E4 File Offset: 0x000394E4
			internal void AddTransformation(IUpdateContext _updateContext, Transformation t)
			{
				StaticTransformationProvider.TransformationsRowsetSink.UpdateContext updateContext = (StaticTransformationProvider.TransformationsRowsetSink.UpdateContext)_updateContext;
				StaticTransformationProvider provider = this.Provider;
				lock (provider)
				{
					if (t.From.Count == 0)
					{
						throw new InvalidDataException(StringResources.EpsilonRulesNotAllowed);
					}
					if (!t.From.Equals(t.To))
					{
						this.Provider.m_prefixTree.Insert(t);
					}
				}
			}

			// Token: 0x020001C4 RID: 452
			private class UpdateContext : IUpdateContext, IRecordUpdateContextInitialize, IDisposable
			{
				// Token: 0x06000E5F RID: 3679 RVA: 0x0003CC9C File Offset: 0x0003AE9C
				private DataTable CreateTransformationRecordSchema(string contextColumnName, string fromColumnName, string toColumnName)
				{
					DataTable dataTable = SchemaUtils.CreateSchemaTable("Transformation Schema");
					int num = 0;
					DataRow dataRow = dataTable.NewRow();
					dataRow[SchemaTableColumn.ColumnName] = contextColumnName;
					dataRow[SchemaTableColumn.ColumnOrdinal] = num++;
					dataRow[SchemaTableColumn.DataType] = typeof(string);
					dataTable.Rows.Add(dataRow);
					dataRow = dataTable.NewRow();
					dataRow[SchemaTableColumn.ColumnName] = fromColumnName;
					dataRow[SchemaTableColumn.ColumnOrdinal] = num++;
					dataRow[SchemaTableColumn.DataType] = typeof(string);
					dataTable.Rows.Add(dataRow);
					dataRow = dataTable.NewRow();
					dataRow[SchemaTableColumn.ColumnName] = toColumnName;
					dataRow[SchemaTableColumn.ColumnOrdinal] = num++;
					dataRow[SchemaTableColumn.DataType] = typeof(string);
					dataTable.Rows.Add(dataRow);
					return dataTable;
				}

				// Token: 0x06000E60 RID: 3680 RVA: 0x0003CD90 File Offset: 0x0003AF90
				public void Initialize(IRowsetManager rowsetManager, IDomainManager domainManager, ITokenIdProvider tokenIdProvider, RecordBinding recordBinding)
				{
					this.TokenIdProvider = tokenIdProvider;
					this.m_domainManager = domainManager;
					this.DomainId = domainManager.GetDomainId(this.DomainName);
					if (this.RecordSchema == null)
					{
						this.RecordSchema = this.CreateTransformationRecordSchema(this.Provider.ContextColumnName, this.Provider.FromColumnName, this.Provider.ToColumnName);
					}
					this.SetTransformationRecordBinding(this.RecordSchema, this.Provider.ContextColumnName, this.Provider.FromColumnName, this.Provider.ToColumnName);
				}

				// Token: 0x06000E61 RID: 3681 RVA: 0x0003CE20 File Offset: 0x0003B020
				public void SetTransformationRecordBinding(DataTable transformationSchemaTable, string contextColumnName, string fromColumnName, string toColumnName)
				{
					this.m_fromColumnIndex = (int)SchemaUtils.FindColumnSchemaRow(transformationSchemaTable, fromColumnName, true)[SchemaTableColumn.ColumnOrdinal];
					this.m_toColumnIndex = (int)SchemaUtils.FindColumnSchemaRow(transformationSchemaTable, toColumnName, true)[SchemaTableColumn.ColumnOrdinal];
					this.m_domainId = this.m_domainManager.GetDomainId(this.DomainName);
					this.m_tokenizer = this.m_domainManager.GetTokenizer(this.DomainName);
					IRecordTokenizer tokenizer = this.m_tokenizer;
					DomainBinding domainBinding = new DomainBinding();
					List<Column> list = new List<Column>();
					list.Add(new Column
					{
						Name = fromColumnName
					});
					domainBinding.Columns = list;
					tokenizer.Prepare(transformationSchemaTable, domainBinding, out this.m_fromTokenizerContext);
					IRecordTokenizer tokenizer2 = this.m_tokenizer;
					DomainBinding domainBinding2 = new DomainBinding();
					List<Column> list2 = new List<Column>();
					list2.Add(new Column
					{
						Name = toColumnName
					});
					domainBinding2.Columns = list2;
					tokenizer2.Prepare(transformationSchemaTable, domainBinding2, out this.m_toTokenizerContext);
					this.m_contextColumnIndex = -1;
					this.m_contextDomainId = -1;
					if (!string.IsNullOrEmpty(this.ContextDomainName))
					{
						this.m_contextDomainId = this.m_domainManager.GetDomainId(this.ContextDomainName);
					}
					if (!string.IsNullOrEmpty(this.Provider.ContextColumnName))
					{
						this.m_contextDomainTokenizer = this.m_domainManager.GetTokenizer(this.ContextDomainName);
						IRecordTokenizer contextDomainTokenizer = this.m_contextDomainTokenizer;
						DomainBinding domainBinding3 = new DomainBinding();
						List<Column> list3 = new List<Column>();
						list3.Add(new Column
						{
							Name = contextColumnName
						});
						domainBinding3.Columns = list3;
						contextDomainTokenizer.Prepare(transformationSchemaTable, domainBinding3, out this.m_contextTokenizerContext);
						this.m_contextColumnIndex = (int)SchemaUtils.FindColumnSchemaRow(transformationSchemaTable, contextColumnName, true)[SchemaTableColumn.ColumnOrdinal];
					}
				}

				// Token: 0x06000E62 RID: 3682 RVA: 0x0003CFAB File Offset: 0x0003B1AB
				public void Dispose()
				{
				}

				// Token: 0x06000E63 RID: 3683 RVA: 0x0003CFB0 File Offset: 0x0003B1B0
				public Transformation CreateTransformation(IDataRecord transformationRecord)
				{
					TokenSequence tokenSequence = default(TokenSequence);
					TokenSequence tokenSequence2 = default(TokenSequence);
					TokenSequence tokenSequence3 = default(TokenSequence);
					string text = ((this.m_contextColumnIndex >= 0) ? transformationRecord[this.m_contextColumnIndex].ToString() : string.Empty);
					string text2 = transformationRecord[this.m_fromColumnIndex].ToString();
					string text3 = transformationRecord[this.m_toColumnIndex].ToString();
					if (!string.IsNullOrEmpty(text))
					{
						this.m_tokenIdSegmentBuilder.Reset();
						this.m_contextTokenizerContext.Reset();
						foreach (StringExtent stringExtent in this.m_contextDomainTokenizer.Tokenize(this.m_contextTokenizerContext, transformationRecord))
						{
							this.m_tokenIdSegmentBuilder.Add(this.TokenIdProvider.GetOrCreateTokenId(stringExtent, this.m_contextDomainId));
						}
						tokenSequence = new TokenSequence(this.m_tokenIdSegmentBuilder.ToSegment(this.Provider.m_tokenIdAllocator));
					}
					if (!string.IsNullOrEmpty(text2))
					{
						this.m_tokenIdSegmentBuilder.Reset();
						this.m_fromTokenizerContext.Reset();
						foreach (StringExtent stringExtent2 in this.m_tokenizer.Tokenize(this.m_fromTokenizerContext, transformationRecord))
						{
							this.m_tokenIdSegmentBuilder.Add(this.TokenIdProvider.GetOrCreateTokenId(stringExtent2, this.m_domainId));
						}
						tokenSequence2 = new TokenSequence(this.m_tokenIdSegmentBuilder.ToSegment(this.Provider.m_tokenIdAllocator));
					}
					if (!string.IsNullOrEmpty(text3))
					{
						this.m_tokenIdSegmentBuilder.Reset();
						this.m_toTokenizerContext.Reset();
						foreach (StringExtent stringExtent3 in this.m_tokenizer.Tokenize(this.m_toTokenizerContext, transformationRecord))
						{
							this.m_tokenIdSegmentBuilder.Add(this.TokenIdProvider.GetOrCreateTokenId(stringExtent3, this.m_domainId));
						}
						tokenSequence3 = new TokenSequence(this.m_tokenIdSegmentBuilder.ToSegment(this.Provider.m_tokenIdAllocator));
					}
					Transformation transformation = new Transformation(tokenSequence2, tokenSequence3, TransformationType.StaticTransformation);
					if (tokenSequence.Count > 0)
					{
						MemoryStream memoryStream = new MemoryStream();
						BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
						tokenSequence.Write(binaryWriter);
						binaryWriter.Flush();
						memoryStream.SetLength(memoryStream.Position);
						transformation.Metadata = this.Provider.m_transformationMetadataAllocator.New((int)memoryStream.Length);
						Array.ConstrainedCopy(memoryStream.GetBuffer(), 0, transformation.Metadata.Array, transformation.Metadata.Offset, (int)memoryStream.Length);
					}
					return transformation;
				}

				// Token: 0x0400076A RID: 1898
				public StaticTransformationProvider Provider;

				// Token: 0x0400076B RID: 1899
				public string DomainName;

				// Token: 0x0400076C RID: 1900
				public string ContextDomainName;

				// Token: 0x0400076D RID: 1901
				public DataTable RecordSchema;

				// Token: 0x0400076E RID: 1902
				public int DomainId;

				// Token: 0x0400076F RID: 1903
				public ITokenIdProvider TokenIdProvider;

				// Token: 0x04000770 RID: 1904
				public IDomainManager m_domainManager;

				// Token: 0x04000771 RID: 1905
				public IRecordTokenizer m_tokenizer;

				// Token: 0x04000772 RID: 1906
				public IRecordTokenizer m_contextDomainTokenizer;

				// Token: 0x04000773 RID: 1907
				public int m_domainId;

				// Token: 0x04000774 RID: 1908
				public int m_contextDomainId;

				// Token: 0x04000775 RID: 1909
				public TokenizerContext m_contextTokenizerContext;

				// Token: 0x04000776 RID: 1910
				public TokenizerContext m_fromTokenizerContext;

				// Token: 0x04000777 RID: 1911
				public TokenizerContext m_toTokenizerContext;

				// Token: 0x04000778 RID: 1912
				public int m_contextColumnIndex;

				// Token: 0x04000779 RID: 1913
				public int m_toColumnIndex;

				// Token: 0x0400077A RID: 1914
				public int m_fromColumnIndex;

				// Token: 0x0400077B RID: 1915
				public TokenSequence m_tokenSequence;

				// Token: 0x0400077C RID: 1916
				private ArraySegmentBuilder<int> m_tokenIdSegmentBuilder = new ArraySegmentBuilder<int>();
			}
		}
	}
}
