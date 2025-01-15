using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Data.Sql;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200003C RID: 60
	[Serializable]
	public class FuzzyLookupBuilder
	{
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000222 RID: 546 RVA: 0x00009F27 File Offset: 0x00008127
		// (set) Token: 0x06000223 RID: 547 RVA: 0x00009F2F File Offset: 0x0000812F
		public DbCommand TransformationsDbCommand
		{
			get
			{
				return this.m_transformationsDbCommand;
			}
			set
			{
				if (this.m_transformationsDataTable != null)
				{
					throw new InvalidOperationException("Only one of TransformationsDbCommand or TransformationsDataTable can be set.");
				}
				this.m_transformationsDbCommand = value;
				this.CreateTransformationsDataReader = () => this.m_transformationsDbCommand.ExecuteReader();
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000224 RID: 548 RVA: 0x00009F5D File Offset: 0x0000815D
		// (set) Token: 0x06000225 RID: 549 RVA: 0x00009F65 File Offset: 0x00008165
		public DataTable TransformationsDataTable
		{
			get
			{
				return this.m_transformationsDataTable;
			}
			set
			{
				if (this.m_transformationsDbCommand != null)
				{
					throw new InvalidOperationException("Only one of TransformationsDbCommand or TransformationsDataTable can be set.");
				}
				this.m_transformationsDataTable = value;
				if (this.m_transformationsDataTable != null)
				{
					this.CreateTransformationsDataReader = () => this.m_transformationsDataTable.CreateDataReader();
					return;
				}
				this.CreateTransformationsDataReader = null;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000226 RID: 550 RVA: 0x00009FA4 File Offset: 0x000081A4
		// (remove) Token: 0x06000227 RID: 551 RVA: 0x00009FDC File Offset: 0x000081DC
		public event FuzzyLookupBuilder.TraceDelegate OnTrace;

		// Token: 0x06000229 RID: 553 RVA: 0x0000A0F8 File Offset: 0x000082F8
		protected DbCommand Clone(DbCommand cmd)
		{
			DbCommand dbCommand = cmd.Connection.CreateCommand();
			dbCommand.CommandText = cmd.CommandText;
			dbCommand.CommandTimeout = cmd.CommandTimeout;
			dbCommand.CommandType = cmd.CommandType;
			dbCommand.Transaction = cmd.Transaction;
			dbCommand.UpdatedRowSource = cmd.UpdatedRowSource;
			foreach (object obj in cmd.Parameters)
			{
				DbParameter dbParameter = (DbParameter)obj;
				dbCommand.Parameters.Add(dbParameter);
			}
			return dbCommand;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000A1A0 File Offset: 0x000083A0
		public virtual FuzzyLookup CreateFuzzyLookup(DbCommand referenceTableCommand, params string[] columnsToMatchOn)
		{
			DataTable schemaTable;
			using (IDataReader dataReader = this.Clone(referenceTableCommand).ExecuteReader(2))
			{
				schemaTable = dataReader.GetSchemaTable();
			}
			RecordBinding recordBinding = this.CreateDomainBinding(schemaTable, this.MatchAcrossColumns, columnsToMatchOn);
			return this.CreateFuzzyLookup(() => referenceTableCommand.ExecuteReader(), recordBinding, recordBinding, null, recordBinding.GetBoundDomainNames());
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000A21C File Offset: 0x0000841C
		public FuzzyLookup CreateFuzzyLookup(DataTable referenceTable, FuzzyLookupEntry.FuzzyLookupParameters parameter, params string[] columnsToMatchOn)
		{
			return this.CreateFuzzyLookup(() => referenceTable.CreateDataReader(), parameter, columnsToMatchOn);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000A24C File Offset: 0x0000844C
		public FuzzyLookup CreateFuzzyLookup(CreateDataReaderDelegate createReferenceDataReader, FuzzyLookupEntry.FuzzyLookupParameters parameter, params string[] columnsToMatchOn)
		{
			DataTable schemaTable;
			using (IDataReader dataReader = createReferenceDataReader())
			{
				schemaTable = dataReader.GetSchemaTable();
			}
			RecordBinding recordBinding = this.CreateDomainBinding(schemaTable, this.MatchAcrossColumns, columnsToMatchOn);
			return this.CreateFuzzyLookup(createReferenceDataReader, recordBinding, recordBinding, parameter, recordBinding.GetBoundDomainNames());
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000A2A4 File Offset: 0x000084A4
		public virtual FuzzyLookup CreateFuzzyLookup(CreateDataReaderDelegate createReferenceDataReader, RecordBinding leftBinding, RecordBinding rightBinding, FuzzyLookupEntry.FuzzyLookupParameters parameter, params string[] domainsToMatchOn)
		{
			DomainManager domainManager = this.CreateDomainManager(createReferenceDataReader, rightBinding, parameter);
			return this.BuildFuzzyLookup(domainManager, leftBinding, rightBinding, createReferenceDataReader);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000A2C8 File Offset: 0x000084C8
		public DomainManager CreateDomainManager(DataTable referenceRowset, RecordBinding recordBinding)
		{
			RowsetManager rowsetManager = new RowsetManager();
			rowsetManager.Rowsets.Add(new InlineRowset
			{
				DataTable = referenceRowset
			});
			rowsetManager.Rowsets.Default = "0";
			rowsetManager.RecordBindings.Add(recordBinding);
			rowsetManager.RecordBindings.Default = "0";
			if (this.CreateTransformationsDataReader != null)
			{
				rowsetManager.Rowsets.Add(new CreateDataReaderRowset
				{
					Name = "Transformations",
					CreateDataReaderDelegate = this.CreateTransformationsDataReader
				});
			}
			DomainManager domainManager = new DomainManager();
			this.CreateDomains(domainManager, rowsetManager, null, recordBinding.GetBoundDomainNames());
			return domainManager;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000A364 File Offset: 0x00008564
		public DomainManager CreateDomainManager(CreateDataReaderDelegate createReferenceDataReader, RecordBinding recordBinding, FuzzyLookupEntry.FuzzyLookupParameters parameter)
		{
			RowsetManager rowsetManager = new RowsetManager();
			rowsetManager.Rowsets.Add(new CreateDataReaderRowset
			{
				CreateDataReaderDelegate = createReferenceDataReader
			});
			rowsetManager.Rowsets.Default = "0";
			rowsetManager.RecordBindings.Add(recordBinding);
			rowsetManager.RecordBindings.Default = "0";
			if (this.CreateTransformationsDataReader != null)
			{
				rowsetManager.Rowsets.Add(new CreateDataReaderRowset
				{
					Name = "Transformations",
					CreateDataReaderDelegate = this.CreateTransformationsDataReader
				});
			}
			DomainManager domainManager = new DomainManager();
			this.CreateDomains(domainManager, rowsetManager, parameter, recordBinding.GetBoundDomainNames());
			return domainManager;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000A400 File Offset: 0x00008600
		protected virtual FuzzyLookupDefinition CreateIndexDefinition(RecordBinding rightBinding)
		{
			FuzzyLookupDefinition fuzzyLookupDefinition = new FuzzyLookupDefinition(rightBinding, new string[0]);
			Lookup lookup = new Lookup
			{
				Domains = new List<string>(rightBinding.GetBoundDomainNames())
			};
			if (this.FuzzyIndexType == FuzzyIndexType.Default)
			{
				if (this.MinQueryThreshold >= 0.8 && this.IndexedComparisonType == FuzzyComparisonType.Jaccard)
				{
					lookup.SignatureGenerator = SignatureGenerator.Create(FuzzyIndexType.LocalitySensitiveHashing);
				}
				else
				{
					lookup.SignatureGenerator = SignatureGenerator.Create(FuzzyIndexType.PrefixFiltering, this.MinQueryThreshold);
				}
			}
			else if (this.FuzzyIndexType == FuzzyIndexType.PrefixFiltering)
			{
				lookup.SignatureGenerator = SignatureGenerator.Create(FuzzyIndexType.PrefixFiltering, this.MinQueryThreshold);
			}
			else
			{
				lookup.SignatureGenerator = SignatureGenerator.Create(this.FuzzyIndexType);
			}
			fuzzyLookupDefinition.Lookups.Add(lookup);
			return fuzzyLookupDefinition;
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000A4B0 File Offset: 0x000086B0
		protected virtual FuzzyLookup BuildFuzzyLookup(DomainManager domainManager, RecordBinding leftBinding, RecordBinding rightBinding, CreateDataReaderDelegate createReferenceDataReader)
		{
			FuzzyLookupDefinition fuzzyLookupDefinition = this.CreateIndexDefinition(rightBinding);
			if (this.EnableFuzzyLookupTuner)
			{
				using (IDataReader dataReader = createReferenceDataReader())
				{
					this.TuneFuzzyLookup(domainManager, dataReader, fuzzyLookupDefinition);
				}
			}
			FuzzyLookup fuzzyLookup2;
			using (IDataReader dataReader2 = createReferenceDataReader())
			{
				FuzzyLookup fuzzyLookup = new FuzzyLookup(domainManager, fuzzyLookupDefinition);
				this.PopulateFuzzyLookup(fuzzyLookup, dataReader2);
				fuzzyLookup2 = fuzzyLookup;
			}
			return fuzzyLookup2;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000A530 File Offset: 0x00008730
		public void PopulateFuzzyLookup(FuzzyLookup fuzzyLookup, IDataReader referenceReader)
		{
			this.Statistics.PopulateFuzzyLookupTime.Start();
			IRecordUpdate recordUpdate = fuzzyLookup.RowsetSinks[0] as IRecordUpdate;
			IUpdateContext updateContext = recordUpdate.BeginUpdate(referenceReader.GetSchemaTable());
			while (referenceReader.Read())
			{
				recordUpdate.AddRecord(updateContext, referenceReader);
			}
			recordUpdate.EndUpdate(updateContext);
			this.Statistics.PopulateFuzzyLookupTime.Stop();
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000A598 File Offset: 0x00008798
		protected virtual void TuneFuzzyLookup(DomainManager domainManager, IDataReader reader, FuzzyLookupDefinition indexDefinition)
		{
			this.Statistics.TuneFuzzyLookupTime.Start();
			FuzzyLookupTuner fuzzyLookupTuner = new FuzzyLookupTuner();
			fuzzyLookupTuner.EnableLocalitySensitiveHashing = true;
			fuzzyLookupTuner.EnablePrefixFiltering = true;
			fuzzyLookupTuner.LSHSuccessProbability = 0.95;
			fuzzyLookupTuner.SampleFraction = 1.0;
			if (fuzzyLookupTuner.Prepare(domainManager, indexDefinition))
			{
				this.Trace("Beginning tuning of FuzzyLookup parameters.");
				IUpdateContext updateContext = fuzzyLookupTuner.BeginUpdate(reader.GetSchemaTable());
				int num = 0;
				while (reader.Read())
				{
					fuzzyLookupTuner.AddRecord(updateContext, reader);
					if (++num % 10000 == 0)
					{
						this.Trace(string.Format("Added {0} records.", num));
					}
				}
				fuzzyLookupTuner.EndUpdate(updateContext);
				this.Trace("Finished tuning of FuzzyLookup parameters.");
			}
			this.Statistics.TuneFuzzyLookupTime.Stop();
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000A662 File Offset: 0x00008862
		public virtual FuzzyQuery CreateFuzzyQuery(FuzzyLookup fuzzyLookup, FuzzyLookupEntry.FuzzyLookupParameters parameter)
		{
			return this.CreateFuzzyQuery(fuzzyLookup, fuzzyLookup.IndexDefinition.RecordBinding, parameter);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000A678 File Offset: 0x00008878
		public virtual FuzzyQuery CreateFuzzyQuery(FuzzyLookup fuzzyLookup, RecordBinding leftRowsetRecordBinding, FuzzyLookupEntry.FuzzyLookupParameters parameter)
		{
			FuzzyQuery fuzzyQuery = new FuzzyQuery(fuzzyLookup, 0, leftRowsetRecordBinding, parameter);
			fuzzyQuery.Threshold = this.MinQueryThreshold;
			if (this.InvokeDefaultComparer)
			{
				CustomComparer customComparer = new CustomComparer
				{
					ContainmentBias = this.ContainmentBias,
					Threshold = this.MinQueryThreshold
				};
				customComparer.Initialize(leftRowsetRecordBinding, fuzzyLookup.IndexDefinition.RecordBinding, fuzzyLookup.IndexDefinition.Lookups[0].Domains, fuzzyLookup.IndexDefinition.Lookups[0].ExactMatchDomains);
				fuzzyQuery.Comparer = customComparer;
				if (this.OrderByCustomSimilarity)
				{
					fuzzyQuery.RankBy(new FuzzyLookupBuilder.CompareMatchResultByCustomSimilarityDescRidAsc(), true);
				}
			}
			return fuzzyQuery;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000A71C File Offset: 0x0000891C
		public virtual RecordBinding CreateDomainBinding(DataTable schema, bool matchAcrossColumns, params string[] columnsToMatchOn)
		{
			RecordBinding recordBinding = new RecordBinding(schema);
			if (matchAcrossColumns)
			{
				recordBinding.Bind(schema.TableName, columnsToMatchOn);
			}
			else
			{
				foreach (string text in columnsToMatchOn)
				{
					recordBinding.Bind(text, new string[] { text });
				}
			}
			return recordBinding;
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000A768 File Offset: 0x00008968
		protected virtual void CreateRightTransformationProviders(DomainManager domainManager, string domainName, IRowsetDistributor rowsetDistributor)
		{
			if (this.CreateTransformationsDataReader != null)
			{
				ITransformationProvider transformationProvider = this.CreateStaticTransfomationRuleProvider(domainManager, domainManager.GetTokenizer(domainName), domainName, rowsetDistributor);
				domainManager[domainName].RightTransformationProvider.Add(transformationProvider);
			}
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000A7A4 File Offset: 0x000089A4
		protected virtual void CreateLeftTransformationProviders(DomainManager domainManager, string domainName, IRowsetDistributor rowsetDistributor)
		{
			if (this.CreateTransformationsDataReader != null)
			{
				ITransformationProvider transformationProvider = this.CreateStaticTransfomationRuleProvider(domainManager, domainManager.GetTokenizer(domainName), domainName, rowsetDistributor);
				domainManager[domainName].LeftTransformationProvider.Add(transformationProvider);
			}
			if (this.EnablePrefixTransformations)
			{
				PrefixTransformationProvider prefixTransformationProvider = new PrefixTransformationProvider();
				prefixTransformationProvider.Initialize(domainManager, domainName);
				TransformationFilterAggregator transformationFilterAggregator = new TransformationFilterAggregator();
				if (this.MaxPrefixLength < 2147483647)
				{
					transformationFilterAggregator.Add(new LhsLengthTransformationFilter(domainManager.TokenIdProvider)
					{
						MaxLength = this.MaxPrefixLength
					});
				}
				if (this.MaxPrefixRulesPerToken < 2147483647 || this.MaxPrefixRulesPerRecord < 2147483647)
				{
					TransformationRhsWeightComparer transformationRhsWeightComparer = new TransformationRhsWeightComparer(domainManager.GetTokenWeightProvider(domainName));
					transformationFilterAggregator.Add(new TopKTransformationFilter
					{
						Comparison = new Comparison<TransformationMatch>(transformationRhsWeightComparer.Compare),
						MaxTransformationsPerToken = this.MaxPrefixRulesPerToken,
						MaxTransformations = this.MaxPrefixRulesPerRecord
					});
				}
				if (transformationFilterAggregator.Count > 0)
				{
					prefixTransformationProvider.TransformationFilter = transformationFilterAggregator;
				}
				domainManager[domainName].LeftTransformationProvider.Add(prefixTransformationProvider);
				if (prefixTransformationProvider != null)
				{
					((IRowsetConsumer)prefixTransformationProvider).RequestRowsets(rowsetDistributor);
				}
			}
			if (this.EnableTokenMergeTransformations)
			{
				TokenMergeTransformationProvider tokenMergeTransformationProvider = new TokenMergeTransformationProvider();
				tokenMergeTransformationProvider.Initialize(domainManager, domainName);
				domainManager[domainName].LeftTransformationProvider.Add(tokenMergeTransformationProvider);
				if (tokenMergeTransformationProvider != null)
				{
					((IRowsetConsumer)tokenMergeTransformationProvider).RequestRowsets(rowsetDistributor);
				}
			}
			if (this.EnableTokenSplitTransformations)
			{
				TokenSplitTransformationProvider tokenSplitTransformationProvider = new TokenSplitTransformationProvider();
				tokenSplitTransformationProvider.Initialize(domainManager, domainName);
				domainManager[domainName].LeftTransformationProvider.Add(tokenSplitTransformationProvider);
				if (tokenSplitTransformationProvider != null)
				{
					((IRowsetConsumer)tokenSplitTransformationProvider).RequestRowsets(rowsetDistributor);
				}
			}
			if (this.EnableEditTransformations)
			{
				EditTransformationProvider editTransformationProvider = new EditTransformationProvider
				{
					Threshold = this.EditThreshold,
					MaxEdits = this.MaxEdits
				};
				editTransformationProvider.Initialize(domainManager, domainName);
				TransformationFilterAggregator transformationFilterAggregator2 = new TransformationFilterAggregator();
				if (!this.AllowEditsOnNumericTokens)
				{
					transformationFilterAggregator2.Add(new NumericTransformationFilter());
				}
				if (this.EnableEditContexts)
				{
					if (2147483647 == this.MaxEditRulesPerToken && 2147483647 == this.MaxContextFreeEditRulesPerToken)
					{
						CustomEditTransformationFilter customEditTransformationFilter = new CustomEditTransformationFilter();
						customEditTransformationFilter.Initialize(domainManager, domainName);
						customEditTransformationFilter.MaxTransformationsPerRecord = this.MaxEditRulesPerRecord;
						if (customEditTransformationFilter != null)
						{
							((IRowsetConsumer)customEditTransformationFilter).RequestRowsets(rowsetDistributor);
						}
						transformationFilterAggregator2.Add(customEditTransformationFilter);
					}
					else
					{
						ContextualTransformationFilter contextualTransformationFilter = new ContextualTransformationFilter();
						contextualTransformationFilter.Initialize(domainManager, domainName);
						contextualTransformationFilter.Comparison = new EditTransformationDistanceComparer();
						contextualTransformationFilter.MaxTransformations = this.MaxEditRulesPerRecord;
						contextualTransformationFilter.MaxTransformationsPerToken = this.MaxEditRulesPerToken;
						contextualTransformationFilter.MaxContextFreeTransformationsPerToken = this.MaxContextFreeEditRulesPerToken;
						if (contextualTransformationFilter != null)
						{
							((IRowsetConsumer)contextualTransformationFilter).RequestRowsets(rowsetDistributor);
						}
						transformationFilterAggregator2.Add(contextualTransformationFilter);
					}
				}
				else if (this.MaxEditRulesPerToken < 2147483647 || this.MaxEditRulesPerRecord < 2147483647)
				{
					EditTransformationDistanceComparer editTransformationDistanceComparer = new EditTransformationDistanceComparer();
					transformationFilterAggregator2.Add(new TopKTransformationFilter
					{
						Comparison = new Comparison<TransformationMatch>(editTransformationDistanceComparer.Compare),
						MaxTransformationsPerToken = this.MaxEditRulesPerToken,
						MaxTransformations = this.MaxEditRulesPerRecord
					});
				}
				if (transformationFilterAggregator2.Count > 1)
				{
					editTransformationProvider.TransformationFilter = transformationFilterAggregator2;
				}
				else if (transformationFilterAggregator2.Count == 1)
				{
					editTransformationProvider.TransformationFilter = transformationFilterAggregator2[0];
				}
				if (editTransformationProvider != null)
				{
					((IRowsetConsumer)editTransformationProvider).RequestRowsets(rowsetDistributor);
				}
				domainManager[domainName].LeftTransformationProvider.Add(editTransformationProvider);
			}
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000AAD8 File Offset: 0x00008CD8
		internal void CreateDomains(DomainManager domainManager, CreateDataReaderDelegate createReferenceDataReader, RecordBinding rightBinding, params string[] domainNames)
		{
			RowsetManager rowsetManager = new RowsetManager();
			rowsetManager.Rowsets.Add(new CreateDataReaderRowset
			{
				CreateDataReaderDelegate = createReferenceDataReader
			});
			rowsetManager.Rowsets.Default = "0";
			rowsetManager.RecordBindings.Add(rightBinding);
			rowsetManager.RecordBindings.Default = "0";
			if (this.CreateTransformationsDataReader != null)
			{
				rowsetManager.Rowsets.Add(new CreateDataReaderRowset
				{
					Name = "Transformations",
					CreateDataReaderDelegate = this.CreateTransformationsDataReader
				});
			}
			this.CreateDomains(domainManager, rowsetManager, null, domainNames);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000AB68 File Offset: 0x00008D68
		public void CreateDomains(DomainManager domainManager, RowsetManager rowsetManager, FuzzyLookupEntry.FuzzyLookupParameters parameter, params string[] domainNames)
		{
			this.Statistics.CreateDomainsTime.Start();
			RowsetDistributor rowsetDistributor = new RowsetDistributor(rowsetManager);
			foreach (string text in domainNames)
			{
				this.CreateDomain(domainManager, text, rowsetDistributor, parameter);
			}
			using (ConnectionManager connectionManager = new ConnectionManager(rowsetManager.Connections))
			{
				rowsetDistributor.DistributeRowsets(connectionManager, domainManager, domainManager.TokenIdProvider);
			}
			this.FinalizeCreateDomains(domainManager, domainNames);
			this.Statistics.CreateDomainsTime.Stop();
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000AC00 File Offset: 0x00008E00
		protected virtual void CreateDomain(DomainManager domainManager, string domainName, IRowsetDistributor rowsetDistributor, FuzzyLookupEntry.FuzzyLookupParameters parameter)
		{
			domainManager.CreateDomain(domainName);
			this.CreateRecordTokenizer(domainManager, domainName, parameter);
			this.CreateRightTransformationProviders(domainManager, domainName, rowsetDistributor);
			this.CreateLeftTransformationProviders(domainManager, domainName, rowsetDistributor);
			this.CreateWeightProvider(domainManager, domainName, rowsetDistributor);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000AC30 File Offset: 0x00008E30
		protected virtual void FinalizeCreateDomains(DomainManager domainManager, params string[] domainNames)
		{
			if (this.EditTransformationPrecomputation != EditTransformationPrecomputationOptions.DoNotUsePrecomputedEditTransformations)
			{
				this.Statistics.EditTransformationPrecomputationTime.Start();
				foreach (string text in domainNames)
				{
					foreach (ITransformationProvider transformationProvider in domainManager.LeftTransformationProviders(text))
					{
						if (transformationProvider is EditTransformationProvider)
						{
							EditTransformationProvider editTransformationProvider = transformationProvider as EditTransformationProvider;
							if (EditTransformationPrecomputationOptions.PrecomputeButDoNotPersistToSql == this.EditTransformationPrecomputation)
							{
								IDataReader dataReader = editTransformationProvider.PrecomputedEditCache.PrecomputeTransformations(editTransformationProvider.Dictionary, domainManager.TokenIdProvider);
								while (dataReader.Read())
								{
								}
							}
							else
							{
								using (SqlConnection sqlConnection = new SqlConnection(this.EditTransformationPrecomputationConnectionString))
								{
									sqlConnection.Open();
									string text2 = this.EditTransformationPrecomputationTableNamePrefix + text;
									bool flag = SqlUtils.TableExists(sqlConnection, text2);
									if (!flag && EditTransformationPrecomputationOptions.LoadFromSqlIfPresentOtherwiseFail == this.EditTransformationPrecomputation)
									{
										throw new Exception("The precomputed edit transformation table does not exist.");
									}
									if (flag && (EditTransformationPrecomputationOptions.LoadFromSqlIfPresentOtherwisePrecomputeAndWriteToSql == this.EditTransformationPrecomputation || EditTransformationPrecomputationOptions.LoadFromSqlIfPresentOtherwiseFail == this.EditTransformationPrecomputation))
									{
										using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
										{
											sqlCommand.CommandText = string.Format("select * from {0} order by [From]", text2);
											sqlCommand.CommandTimeout = 0;
											using (IDataReader dataReader2 = sqlCommand.ExecuteReader())
											{
												editTransformationProvider.PrecomputedEditCache.Add(dataReader2, domainManager.TokenIdProvider);
												continue;
											}
										}
									}
									using (IDataReader dataReader3 = editTransformationProvider.PrecomputedEditCache.PrecomputeTransformations(editTransformationProvider.Dictionary, domainManager.TokenIdProvider))
									{
										SqlUtils.CreateTableFromSchema(sqlConnection, dataReader3.GetSchemaTable(), text2, true);
										using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(sqlConnection))
										{
											sqlBulkCopy.BulkCopyTimeout = 0;
											sqlBulkCopy.BatchSize = 50000;
											sqlBulkCopy.DestinationTableName = text2;
											sqlBulkCopy.WriteToServer(dataReader3);
										}
									}
								}
							}
						}
					}
				}
				this.Statistics.EditTransformationPrecomputationTime.Stop();
			}
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000AED4 File Offset: 0x000090D4
		protected virtual void CreateRecordTokenizer(DomainManager domainManager, string domainName, FuzzyLookupEntry.FuzzyLookupParameters parameter)
		{
			FtsRecordTokenizer ftsRecordTokenizer;
			if (parameter == null)
			{
				ftsRecordTokenizer = new FtsRecordTokenizer();
			}
			else
			{
				ftsRecordTokenizer = new FtsRecordTokenizer(parameter.LocaleId);
			}
			ftsRecordTokenizer.NormalizationOptions.IgnoreCase = this.IgnoreCase;
			ftsRecordTokenizer.NormalizationOptions.IgnoreNonSpacing = this.IgnoreNonSpacing;
			domainManager.SetTokenizer(domainName, ftsRecordTokenizer);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000AF24 File Offset: 0x00009124
		protected virtual void CreateWeightProvider(DomainManager domainManager, string domainName, IRowsetDistributor rowsetDistributor)
		{
			ITokenWeightProvider tokenWeightProvider;
			if (this.UseIDFWeights)
			{
				tokenWeightProvider = new IdfTokenWeightProvider();
				if (tokenWeightProvider is IRowsetConsumer)
				{
					(tokenWeightProvider as IRowsetConsumer).RequestRowsets(rowsetDistributor);
				}
			}
			else
			{
				tokenWeightProvider = new ConstantWeightProvider();
			}
			if (tokenWeightProvider is IProviderInitialize)
			{
				(tokenWeightProvider as IProviderInitialize).Initialize(domainManager, domainName);
			}
			domainManager.SetTokenWeightProvider(domainName, tokenWeightProvider);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000AF78 File Offset: 0x00009178
		protected ITransformationProvider CreateStaticTransfomationRuleProvider(DomainManager domainManager, IRecordTokenizer tokenizer, string domainName, IRowsetDistributor rowsetDistributor)
		{
			StaticTransformationProvider staticTransformationProvider = new StaticTransformationProvider
			{
				TransformationsRowsetName = "Transformations"
			};
			if (staticTransformationProvider != null)
			{
				((IProviderInitialize)staticTransformationProvider).Initialize(domainManager, domainName);
			}
			if (staticTransformationProvider != null)
			{
				((IRowsetConsumer)staticTransformationProvider).RequestRowsets(rowsetDistributor);
			}
			return staticTransformationProvider;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000AFAD File Offset: 0x000091AD
		protected virtual void Trace(string message)
		{
			if (this.OnTrace != null)
			{
				this.OnTrace(message);
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000AFC4 File Offset: 0x000091C4
		public static FuzzyLookupBuilder Jaccard(FuzzyComparisonType IndexedComparisonType = FuzzyComparisonType.Jaccard, FuzzyIndexType FuzzyIndexType = FuzzyIndexType.PrefixFiltering, double MinQueryThreshold = 0.0, double ContainmentBias = 0.0, bool EnableFuzzyLookupTuner = false, bool EnableTokenMergeTransformations = false, bool EnableTokenSplitTransformations = false, bool EnablePrefixTransformations = false, int MaxPrefixRulesPerToken = 20, int MaxPrefixRulesPerRecord = 2147483647, int MaxPrefixLength = 1, bool EnableEditTransformations = false, double EditThreshold = 0.75, int MaxEdits = 2147483647, int MaxEditRulesPerToken = 2147483647, int MaxContextFreeEditRulesPerToken = 2147483647, int MaxEditRulesPerRecord = 100, bool AllowEditsOnNumericTokens = true, bool EnableEditContexts = true, EditTransformationPrecomputationOptions EditTransformationPrecomputation = EditTransformationPrecomputationOptions.DoNotUsePrecomputedEditTransformations, string EditTransformationPrecomputationConnectionString = "server=.,database=tempdb,Integrated Security=SSPI,Connection Timeout=0", string EditTransformationPrecomputationTableNamePrefix = "FuzzyLookup_PrecomputedEditTransformations_", bool UseIDFWeights = false, bool IgnoreCase = false, bool IgnoreNonSpacing = false)
		{
			return new FuzzyLookupBuilder
			{
				IndexedComparisonType = IndexedComparisonType,
				FuzzyIndexType = FuzzyIndexType,
				MinQueryThreshold = MinQueryThreshold,
				ContainmentBias = ContainmentBias,
				EnableFuzzyLookupTuner = EnableFuzzyLookupTuner,
				EnableTokenMergeTransformations = EnableTokenMergeTransformations,
				EnableTokenSplitTransformations = EnableTokenSplitTransformations,
				EnablePrefixTransformations = EnablePrefixTransformations,
				MaxPrefixRulesPerToken = MaxPrefixRulesPerToken,
				MaxPrefixRulesPerRecord = MaxPrefixRulesPerRecord,
				MaxPrefixLength = MaxPrefixLength,
				EnableEditTransformations = EnableEditTransformations,
				EditThreshold = EditThreshold,
				MaxEdits = MaxEdits,
				MaxEditRulesPerToken = MaxEditRulesPerToken,
				MaxContextFreeEditRulesPerToken = MaxContextFreeEditRulesPerToken,
				MaxEditRulesPerRecord = MaxEditRulesPerRecord,
				AllowEditsOnNumericTokens = AllowEditsOnNumericTokens,
				EnableEditContexts = EnableEditContexts,
				EditTransformationPrecomputation = EditTransformationPrecomputation,
				EditTransformationPrecomputationConnectionString = EditTransformationPrecomputationConnectionString,
				EditTransformationPrecomputationTableNamePrefix = EditTransformationPrecomputationTableNamePrefix,
				UseIDFWeights = UseIDFWeights,
				IgnoreCase = IgnoreCase,
				IgnoreNonSpacing = IgnoreNonSpacing
			};
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000B09C File Offset: 0x0000929C
		public static FuzzyLookupBuilder JaccardContainment(FuzzyComparisonType IndexedComparisonType = FuzzyComparisonType.LeftJaccardContainment, FuzzyIndexType FuzzyIndexType = FuzzyIndexType.PrefixFiltering, double MinQueryThreshold = 0.0, double ContainmentBias = 0.0, bool EnableFuzzyLookupTuner = false, bool EnableTokenMergeTransformations = false, bool EnableTokenSplitTransformations = false, bool EnablePrefixTransformations = false, int MaxPrefixRulesPerToken = 20, int MaxPrefixRulesPerRecord = 2147483647, int MaxPrefixLength = 1, bool EnableEditTransformations = false, double EditThreshold = 0.75, int MaxEdits = 2147483647, int MaxEditRulesPerToken = 2147483647, int MaxContextFreeEditRulesPerToken = 2147483647, int MaxEditRulesPerRecord = 100, bool AllowEditsOnNumericTokens = true, bool EnableEditContexts = true, EditTransformationPrecomputationOptions EditTransformationPrecomputation = EditTransformationPrecomputationOptions.DoNotUsePrecomputedEditTransformations, string EditTransformationPrecomputationConnectionString = "server=.,database=tempdb,Integrated Security=SSPI,Connection Timeout=0", string EditTransformationPrecomputationTableNamePrefix = "FuzzyLookup_PrecomputedEditTransformations_", bool UseIDFWeights = false, bool IgnoreCase = false, bool IgnoreNonSpacing = false)
		{
			return new FuzzyLookupBuilder
			{
				IndexedComparisonType = IndexedComparisonType,
				FuzzyIndexType = FuzzyIndexType,
				MinQueryThreshold = MinQueryThreshold,
				ContainmentBias = ContainmentBias,
				EnableFuzzyLookupTuner = EnableFuzzyLookupTuner,
				EnableTokenMergeTransformations = EnableTokenMergeTransformations,
				EnableTokenSplitTransformations = EnableTokenSplitTransformations,
				EnablePrefixTransformations = EnablePrefixTransformations,
				MaxPrefixRulesPerToken = MaxPrefixRulesPerToken,
				MaxPrefixRulesPerRecord = MaxPrefixRulesPerRecord,
				MaxPrefixLength = MaxPrefixLength,
				EnableEditTransformations = EnableEditTransformations,
				EditThreshold = EditThreshold,
				MaxEdits = MaxEdits,
				MaxEditRulesPerToken = MaxEditRulesPerToken,
				MaxContextFreeEditRulesPerToken = MaxContextFreeEditRulesPerToken,
				MaxEditRulesPerRecord = MaxEditRulesPerRecord,
				AllowEditsOnNumericTokens = AllowEditsOnNumericTokens,
				EnableEditContexts = EnableEditContexts,
				EditTransformationPrecomputation = EditTransformationPrecomputation,
				EditTransformationPrecomputationConnectionString = EditTransformationPrecomputationConnectionString,
				EditTransformationPrecomputationTableNamePrefix = EditTransformationPrecomputationTableNamePrefix,
				UseIDFWeights = UseIDFWeights,
				IgnoreCase = IgnoreCase,
				IgnoreNonSpacing = IgnoreNonSpacing
			};
		}

		// Token: 0x0400009D RID: 157
		public FuzzyLookupBuilderStatistics Statistics = new FuzzyLookupBuilderStatistics();

		// Token: 0x0400009E RID: 158
		public bool MatchAcrossColumns = true;

		// Token: 0x0400009F RID: 159
		public double MinQueryThreshold = 0.8;

		// Token: 0x040000A0 RID: 160
		public FuzzyComparisonType IndexedComparisonType;

		// Token: 0x040000A1 RID: 161
		public double ContainmentBias = 0.5;

		// Token: 0x040000A2 RID: 162
		public bool EnableFuzzyLookupTuner;

		// Token: 0x040000A3 RID: 163
		public bool EnableTokenMergeTransformations;

		// Token: 0x040000A4 RID: 164
		public bool EnableTokenSplitTransformations = true;

		// Token: 0x040000A5 RID: 165
		public bool EnablePrefixTransformations;

		// Token: 0x040000A6 RID: 166
		public int MaxPrefixRulesPerToken = 20;

		// Token: 0x040000A7 RID: 167
		public int MaxPrefixRulesPerRecord = int.MaxValue;

		// Token: 0x040000A8 RID: 168
		public int MaxPrefixLength = 1;

		// Token: 0x040000A9 RID: 169
		public bool EnableEditTransformations = true;

		// Token: 0x040000AA RID: 170
		public double EditThreshold = 0.65;

		// Token: 0x040000AB RID: 171
		public int MaxEdits = int.MaxValue;

		// Token: 0x040000AC RID: 172
		public int MaxEditRulesPerToken = int.MaxValue;

		// Token: 0x040000AD RID: 173
		public int MaxContextFreeEditRulesPerToken = int.MaxValue;

		// Token: 0x040000AE RID: 174
		public int MaxEditRulesPerRecord = 100;

		// Token: 0x040000AF RID: 175
		public bool AllowEditsOnNumericTokens = true;

		// Token: 0x040000B0 RID: 176
		public bool EnableEditContexts = true;

		// Token: 0x040000B1 RID: 177
		public EditTransformationPrecomputationOptions EditTransformationPrecomputation;

		// Token: 0x040000B2 RID: 178
		public string EditTransformationPrecomputationConnectionString = "server=.;database=tempdb;Integrated Security=SSPI;Connection Timeout=0";

		// Token: 0x040000B3 RID: 179
		public string EditTransformationPrecomputationTableNamePrefix = "FuzzyLookup_PrecomputedEditTransformations_";

		// Token: 0x040000B4 RID: 180
		public bool UseIDFWeights = true;

		// Token: 0x040000B5 RID: 181
		public bool IgnoreCase = true;

		// Token: 0x040000B6 RID: 182
		public bool IgnoreNonSpacing;

		// Token: 0x040000B7 RID: 183
		public FuzzyIndexType FuzzyIndexType;

		// Token: 0x040000B8 RID: 184
		[NonSerialized]
		private DbCommand m_transformationsDbCommand;

		// Token: 0x040000B9 RID: 185
		[NonSerialized]
		private DataTable m_transformationsDataTable;

		// Token: 0x040000BA RID: 186
		[NonSerialized]
		protected CreateDataReaderDelegate CreateTransformationsDataReader;

		// Token: 0x040000BB RID: 187
		public bool InvokeDefaultComparer = true;

		// Token: 0x040000BC RID: 188
		public bool OrderByCustomSimilarity = true;

		// Token: 0x02000128 RID: 296
		// (Invoke) Token: 0x06000BE3 RID: 3043
		public delegate void TraceDelegate(string message);

		// Token: 0x02000129 RID: 297
		[Serializable]
		public sealed class CompareMatchResultByCustomSimilarityDescRidAsc : IComparer<IMatchResult>
		{
			// Token: 0x06000BE6 RID: 3046 RVA: 0x00033A18 File Offset: 0x00031C18
			public int Compare(IMatchResult x, IMatchResult y)
			{
				if (x.ComparisonResult.Similarity == y.ComparisonResult.Similarity)
				{
					if (x.RightRecordId == y.RightRecordId)
					{
						return 0;
					}
					if (x.RightRecordId <= y.RightRecordId)
					{
						return -1;
					}
					return 1;
				}
				else
				{
					if (x.ComparisonResult.Similarity <= y.ComparisonResult.Similarity)
					{
						return 1;
					}
					return -1;
				}
			}
		}
	}
}
