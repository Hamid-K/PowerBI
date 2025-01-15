using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.BusinessIntelligence;
using Microsoft.InfoNav;
using Microsoft.Lucia.Core;
using Microsoft.Lucia.Core.DomainModel;
using Microsoft.Lucia.Core.DomainModel.Serialization;
using Microsoft.Lucia.Core.Packaging;
using Microsoft.Lucia.Core.TermIndex;
using Microsoft.PowerBI.Data.ModelSchemaAnalysis;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;
using Microsoft.PowerBI.ExploreHost.DataExtension;
using Microsoft.PowerBI.ExploreHost.Lucia.NLToDax;
using Microsoft.PowerBI.ExploreHost.Utils;
using Microsoft.PowerBI.Lucia.Hosting;
using Microsoft.PowerBI.Lucia.Hosting.SchemaAnnotations;
using Microsoft.PowerBI.Lucia.Hosting.SchemaAnnotations.Serialization;
using Microsoft.PowerBI.Lucia.Interpret;
using Microsoft.PowerBI.NaturalLanguage.NLToDax;
using Microsoft.PowerBI.ReportingServicesHost;
using Microsoft.ReportingServices.Common;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.ExploreHost.Lucia
{
	// Token: 0x0200006A RID: 106
	internal sealed class LuciaSession : ILuciaSession, IDisposable
	{
		// Token: 0x060002DB RID: 731 RVA: 0x0000933C File Offset: 0x0000753C
		internal LuciaSession(Lazy<INaturalLanguageServicesFactory> nlServiceFactory, IConnectionFactory connectionFactory, IConnectionPool connectionPool, IDataSourceInfo dataSourceInfo, Func<string> getConceptualSchemaXml, Func<string, IReadOnlyDictionary<string, object>, DataSet> getSchemaDataSet, Func<string> getLinguisticSchemaJson, Func<string, Task<string>> getDaxTemplate, IBulkMeasureExpressionProvider measureExpressionProvider, string workingDirectory, IDataIndexDisposer dataIndexDisposer, IStreamBasedStorage indexStreamStorage, LuciaSessionOptions options, FeatureSwitches featureSwitches)
		{
			LuciaSession <>4__this = this;
			this.m_nlServiceFactory = nlServiceFactory;
			this.m_measureExpressionProvider = measureExpressionProvider;
			this.m_modelLastModifiedTime = DateTime.MinValue;
			this.m_activationStatus = LuciaSession.ActivationStatus.Inactive;
			this.m_usageStatus = LuciaSession.UsageStatus.Idle;
			this.m_luciaSessionOptions = options;
			this.m_featureSwitches = LuciaUtils.GetFeatureSwitches(featureSwitches).AsReadOnlyList<FeatureSwitch>();
			this.m_featureSwitchProvider = FeatureSwitchProvider.Create(this.m_featureSwitches);
			ConnectionProvider connectionProvider = new ConnectionProvider(connectionFactory, connectionPool, DataShapingTracer.Instance);
			this.m_connectionManager = new DbConnectionManager(dataSourceInfo, connectionProvider);
			if (options.IsEmulation())
			{
				DataInstanceComparer dataInstanceComparer = new DataInstanceComparer(StringComparer.Ordinal);
				DataInstanceFilterCache dataInstanceFilterCache = new DataInstanceFilterCache(dataInstanceComparer);
				this.m_dataInstanceFilter = new DataInstanceFilter(dataInstanceFilterCache, new DataInstanceFilterLookup(this.m_connectionManager, dataInstanceComparer, ExploreTracer.Instance), dataInstanceComparer, ExploreTracer.Instance, null, false, null);
			}
			Version version = (this.m_featureSwitchProvider.IsFeatureSwitchEnabled(FeatureSwitch.DataIndexV1_2) ? DataIndexVersion.V1_2 : DataIndexVersion.Default);
			DataIndexStore dataIndexStore = new DataIndexStore(indexStreamStorage, this.m_nlServiceFactory, workingDirectory, this.m_luciaSessionOptions, version, false);
			this.m_utteranceFeedStore = new UtteranceFeedStore(indexStreamStorage);
			DataIndexBuilder dataIndexBuilder = new DataIndexBuilder(this.m_nlServiceFactory, workingDirectory, dataSourceInfo, connectionProvider, version, this.m_luciaSessionOptions);
			IColumnStatisticsDiscovererWrapper columnStatisticsDiscovererWrapper = LuciaSession.CreateStatisticsDiscoverer(this.m_connectionManager, indexStreamStorage);
			this.m_domainModelManager = new DomainModelManager(this.m_nlServiceFactory, getConceptualSchemaXml, () => <>4__this.GetLinguisticSchemaJsonWithOverride(getLinguisticSchemaJson), measureExpressionProvider, dataIndexBuilder, dataIndexDisposer, this.m_luciaSessionOptions, dataIndexStore, columnStatisticsDiscovererWrapper, false, new TimeSpan?(LuciaSession.UpdateDatabaseContextDelay), new TimeSpan?(LuciaSession.BuildNewIndexDelay), options.IsEmulation(), this.m_featureSwitchProvider);
			this.m_linguisticSchemaServices = new LinguisticSchemaServices(nlServiceFactory, getConceptualSchemaXml, () => <>4__this.GetLinguisticSchemaJsonWithOverride(getLinguisticSchemaJson), measureExpressionProvider, this.m_luciaSessionOptions, this.m_featureSwitchProvider);
			this.m_schemaMetadataProvider = new SchemaMetadataProvider(ExploreTracer.Instance, null, null);
			this.m_interpretHandler = new InterpretHandler(this.m_nlServiceFactory, new NLToDaxRuntimeFactory<DesktopRequestContext, DesktopResultContext>(new SchemaDataSetProvider(getSchemaDataSet), new DaxServices(getDaxTemplate, ExploreTracer.Instance), ExploreTracer.Instance, NLToDaxTelemetryService.Instance), this.m_schemaMetadataProvider, this.m_schemaMetadataProvider, this.m_dataInstanceFilter, this.m_featureSwitches, null);
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00009560 File Offset: 0x00007760
		internal LuciaSession(Lazy<INaturalLanguageServicesFactory> nlServiceFactory, IDomainModelManager domainModelManager, Func<string> getConceptualSchemaXml, Func<string> getLinguisticSchemaJson, IBulkMeasureExpressionProvider measureExpressionProvider, IEnumerable<FeatureSwitch> featureSwitches, INLToDaxRuntimeFactory<DesktopRequestContext, DesktopResultContext> nlToDaxRuntimeFactory)
		{
			LuciaSession <>4__this = this;
			this.m_nlServiceFactory = nlServiceFactory;
			this.m_measureExpressionProvider = measureExpressionProvider;
			this.m_modelLastModifiedTime = DateTime.MinValue;
			this.m_activationStatus = LuciaSession.ActivationStatus.Inactive;
			this.m_usageStatus = LuciaSession.UsageStatus.Idle;
			this.m_domainModelManager = domainModelManager;
			this.m_featureSwitches = featureSwitches.AsReadOnlyList<FeatureSwitch>();
			this.m_featureSwitchProvider = FeatureSwitchProvider.Create(this.m_featureSwitches);
			this.m_linguisticSchemaServices = new LinguisticSchemaServices(this.m_nlServiceFactory, getConceptualSchemaXml, () => <>4__this.GetLinguisticSchemaJsonWithOverride(getLinguisticSchemaJson), measureExpressionProvider, LuciaSessionOptions.Default, this.m_featureSwitchProvider);
			this.m_schemaMetadataProvider = new SchemaMetadataProvider(ExploreTracer.Instance, null, null);
			this.m_interpretHandler = new InterpretHandler(this.m_nlServiceFactory, nlToDaxRuntimeFactory, this.m_schemaMetadataProvider, this.m_schemaMetadataProvider, this.m_dataInstanceFilter, this.m_featureSwitches, null);
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060002DD RID: 733 RVA: 0x0000963C File Offset: 0x0000783C
		// (remove) Token: 0x060002DE RID: 734 RVA: 0x00009674 File Offset: 0x00007874
		public event EventHandler DomainModelUpdated;

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060002DF RID: 735 RVA: 0x000096A9 File Offset: 0x000078A9
		public bool IsActive
		{
			get
			{
				return this.m_activationStatus == LuciaSession.ActivationStatus.Active;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x000096B4 File Offset: 0x000078B4
		public bool IsDomainModelReady
		{
			get
			{
				return this.m_domainModelManager.Status == DomainModelManagerStatus.Ready || this.m_domainModelManager.Status == DomainModelManagerStatus.StaleDataIndex;
			}
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x000096D8 File Offset: 0x000078D8
		public void Activate()
		{
			if (this.IsActive)
			{
				return;
			}
			try
			{
				this.m_domainModelManager.NotifyModelChanged(this.m_filePath, this.m_modelLastModifiedTime, null, new Action(this.OnDomainModelUpdated), this.m_usageStatus == LuciaSession.UsageStatus.InUse);
				this.m_activationStatus = LuciaSession.ActivationStatus.Active;
			}
			catch (Exception ex) when (!AsynchronousExceptionDetection.IsStoppingException(ex))
			{
				ExploreHostUtils.TraceActivateLuciaSessionException(ex, this.m_luciaSessionOptions);
				this.m_activationStatus = LuciaSession.ActivationStatus.Error;
			}
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00009768 File Offset: 0x00007968
		public void Deactivate()
		{
			this.m_activationStatus = LuciaSession.ActivationStatus.Inactive;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00009774 File Offset: 0x00007974
		public async Task<string> InterpretAsync(string interpretJsonRequest)
		{
			InterpretRequest<DesktopRequestContext> pbiLuciaRequest;
			string text;
			if (!InterpretRequest<DesktopRequestContext>.TryReadJson(interpretJsonRequest, ref pbiLuciaRequest))
			{
				text = LuciaUtils.SerializeInterpretResponse(InterpretVersion.Base, InterpretDiagnosticMessageFactory.GenericRequestFailure(null, null));
			}
			else
			{
				if (this.m_featureSwitchProvider.IsFeatureSwitchEnabled(FeatureSwitch.QnaReindexPausing) && !pbiLuciaRequest.HasNullOrEmptyUtterance<DesktopRequestContext>())
				{
					this.SetInUse();
				}
				LuciaSession.VerifyRuntimeKnownError verifyRuntimeKnownError;
				Exception ex;
				if (this.IsFaultedOnVerifyRuntime(out verifyRuntimeKnownError, out ex))
				{
					text = LuciaUtils.SerializeInterpretResponse(pbiLuciaRequest.Version, (verifyRuntimeKnownError == LuciaSession.VerifyRuntimeKnownError.MissingRuntimeModule) ? InterpretDiagnosticMessageFactory.MissingRuntimeModule((ex != null) ? ex.Message : null, (ex != null) ? ex.StackTrace : null) : InterpretDiagnosticMessageFactory.VerifyRuntimeFailure((ex != null) ? ex.Message : null, (ex != null) ? ex.StackTrace : null));
				}
				else
				{
					DomainModelManagerStatus status = this.m_domainModelManager.Status;
					if (status == DomainModelManagerStatus.FaultedDueToUnsupportedLanguage)
					{
						text = LuciaUtils.SerializeInterpretResponse(pbiLuciaRequest.Version, InterpretDiagnosticMessageFactory.SchemaLanguageNotSupported());
					}
					else if (this.m_activationStatus == LuciaSession.ActivationStatus.Inactive)
					{
						text = LuciaUtils.SerializeInterpretResponse(pbiLuciaRequest.Version, InterpretDiagnosticMessageFactory.DomainModelNotReady(1));
					}
					else if (this.m_activationStatus == LuciaSession.ActivationStatus.Error)
					{
						text = LuciaUtils.SerializeInterpretResponse(pbiLuciaRequest.Version, InterpretDiagnosticMessageFactory.ActivationFailure());
					}
					else
					{
						try
						{
							IReference<IDatabaseContext> reference = await this.m_domainModelManager.GetDatabaseContextAsync();
							using (IReference<IDatabaseContext> databaseContext = reference)
							{
								if (databaseContext.Value.GetConceptualSchemaReaders() == null)
								{
									if (status == DomainModelManagerStatus.FailedToUpdateDatabaseContext)
									{
										text = LuciaUtils.SerializeInterpretResponse(pbiLuciaRequest.Version, InterpretDiagnosticMessageFactory.DomainModelLoadFailure());
									}
									else
									{
										text = LuciaUtils.SerializeInterpretResponse(pbiLuciaRequest.Version, InterpretDiagnosticMessageFactory.DomainModelNotReady(1));
									}
								}
								else
								{
									using (IReference<IDataIndexContainer> dataIndex = await this.m_domainModelManager.GetDataIndexAsync())
									{
										List<InterpretDiagnosticMessage> list = InterpretContractUtils.ExtractDiagnosticMessages(dataIndex.Value.Index.Metadata);
										InterpretResponse<DesktopResultContext> interpretResponse;
										if (InterpretContractUtils.HasDiagnosticMessage(list, 39, 0))
										{
											InterpretResult<DesktopResultContext> interpretResult = new InterpretResult<DesktopResultContext>();
											interpretResult.RegisterDiagnosticMessages(list);
											interpretResponse = InterpretResponseFactory<DesktopResultContext>.Create(pbiLuciaRequest.Version, interpretResult);
										}
										else
										{
											SchemaMetadataProvider schemaMetadataProvider = this.m_schemaMetadataProvider;
											DesktopRequestContext context = pbiLuciaRequest.Context;
											schemaMetadataProvider.UpdateReportMetadata(SchemaMetadataProvider.ConvertReportMetadata((context != null) ? context.ReportMetadata : null));
											if (pbiLuciaRequest.Context == null)
											{
												pbiLuciaRequest.Context = new DesktopRequestContext();
											}
											interpretResponse = await this.m_interpretHandler.InterpretAsync(pbiLuciaRequest, databaseContext.Value, dataIndex.Value);
											InterpretResponseExtensions.RegisterDiagnosticMessage<DesktopResultContext>(interpretResponse, LuciaSession.GetDiagnosticMessageFrom(status, dataIndex.Value.Index));
											InterpretResponseExtensions.RegisterDiagnosticMessages<DesktopResultContext>(interpretResponse, InterpretContractUtils.ExtractDiagnosticMessages(dataIndex.Value.Index.Metadata));
										}
										text = InterpretContractsSerializer.ToJsonString<DesktopResultContext>(interpretResponse);
									}
								}
							}
						}
						catch (Exception ex2) when (!AsynchronousExceptionDetection.IsStoppingException(ex2))
						{
							ExploreHostUtils.TraceInterpretUtteranceException(ex2, this.m_luciaSessionOptions);
							text = LuciaUtils.SerializeInterpretResponse(pbiLuciaRequest.Version, InterpretDiagnosticMessageFactory.GenericRequestFailure(ex2.Message, ex2.StackTrace));
						}
					}
				}
			}
			return text;
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x000097BF File Offset: 0x000079BF
		public void NotifyModelChanging()
		{
			if (this.IsActive)
			{
				this.m_domainModelManager.NotifyModelChanging();
			}
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x000097D8 File Offset: 0x000079D8
		public void NotifyModelChanged(LuciaSessionModelChangedArgs args)
		{
			this.m_filePath = args.FilePath;
			this.m_modelLastModifiedTime = args.LastModifiedTime;
			if (this.m_dataInstanceFilter != null)
			{
				this.m_dataInstanceFilter.ClearCache();
			}
			if (this.IsActive)
			{
				this.m_domainModelManager.NotifyModelChanged(this.m_filePath, this.m_modelLastModifiedTime, args.SchemaItemsToInvalidate, new Action(this.OnDomainModelUpdated), this.m_usageStatus == LuciaSession.UsageStatus.InUse);
			}
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000984C File Offset: 0x00007A4C
		public void NotifyFileSaved(LuciaSessionFileSavedArgs args)
		{
			if (!this.m_featureSwitchProvider.IsFeatureSwitchEnabled(FeatureSwitch.QnaReindexPausing))
			{
				return;
			}
			string oldFilePath = args.OldFilePath;
			string newFilePath = args.NewFilePath;
			this.m_filePath = newFilePath;
			if (string.IsNullOrEmpty(oldFilePath) && string.IsNullOrEmpty(newFilePath))
			{
				this.m_domainModelManager.NotifyFileDiscarded();
				return;
			}
			if (!string.Equals(oldFilePath, newFilePath, StringComparison.OrdinalIgnoreCase))
			{
				this.m_domainModelManager.NotifyFilePathChanged(args.OldFilePath, args.NewFilePath);
			}
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x000098C0 File Offset: 0x00007AC0
		public Task<ValidateLinguisticSchemaResult> ValidateLinguisticSchemaYamlForImportAsync(TextReader reader)
		{
			string text;
			if (this.IsFaultedOnVerifyRuntimeWithKnownError(out text))
			{
				return Task.FromResult<ValidateLinguisticSchemaResult>(ValidateLinguisticSchemaResult.Failure(text));
			}
			return this.m_linguisticSchemaServices.ValidateLinguisticSchemaYamlForImportAsync(reader);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x000098F0 File Offset: 0x00007AF0
		public Task<ExportLinguisticSchemaResult> ExportLinguisticSchemaYamlAsync(string defaultSchemaSource = null)
		{
			string text;
			if (this.IsFaultedOnVerifyRuntimeWithKnownError(out text))
			{
				return Task.FromResult<ExportLinguisticSchemaResult>(ExportLinguisticSchemaResult.Failure(text));
			}
			return this.m_linguisticSchemaServices.ExportLinguisticSchemaYamlAsync(defaultSchemaSource);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x00009920 File Offset: 0x00007B20
		public async Task<bool> TryWriteLinguisticSchemaJsonAsync(JsonWriter writer, string requestedVersionString)
		{
			Version requestedVersion = Version.Parse(requestedVersionString);
			DomainModelManagerStatus status = this.m_domainModelManager.Status;
			if (this.IsActive && status != DomainModelManagerStatus.FaultedOnVerifyRuntime && status != DomainModelManagerStatus.FaultedDueToUnsupportedLanguage)
			{
				using (IReference<IDatabaseContext> reference = await this.m_domainModelManager.GetDatabaseContextAsync())
				{
					if (reference.Value.GetConceptualSchemaReaders() != null)
					{
						LsdlDocument lsdlDocument;
						if (!this.m_nlServiceFactory.Value.CreateManagementService(this.m_featureSwitchProvider, LinguisticSchemaServicesBuilderOptions.None).TryGetLinguisticSchema(reference.Value, out lsdlDocument, null))
						{
							return false;
						}
						Version version;
						lsdlDocument.WriteJson(writer, requestedVersion, out version);
						return requestedVersion == version;
					}
				}
			}
			return this.m_linguisticSchemaServices.TryWriteRawLinguisticSchemaJson(writer, requestedVersion);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00009973 File Offset: 0x00007B73
		public void OverrideLinguisticSchemaJson(string schemaJson)
		{
			this.m_overrideLinguisticSchemaJson = schemaJson;
			this.NotifyModelChanged(new LuciaSessionModelChangedArgs(this.m_modelLastModifiedTime, null, null));
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000998F File Offset: 0x00007B8F
		public void ResetLinguisticSchemaJson()
		{
			this.OverrideLinguisticSchemaJson(null);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00009998 File Offset: 0x00007B98
		public Task StoreUtteranceFeedAsync(string datasetId, Stream stream, CancellationToken cancellationToken)
		{
			return this.m_utteranceFeedStore.StoreUtteranceFeedAsync(datasetId, stream, cancellationToken);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x000099A8 File Offset: 0x00007BA8
		public Task<string> GetUtteranceHistoryAsync(string datasetId, CancellationToken cancellationToken)
		{
			return Task.Run<string>(delegate
			{
				cancellationToken.ThrowIfCancellationRequested();
				return this.m_utteranceFeedStore.GetUtteranceHistory(datasetId, cancellationToken);
			});
		}

		// Token: 0x060002EE RID: 750 RVA: 0x000099D4 File Offset: 0x00007BD4
		public async Task WriteDataIndexAsync(Stream stream, CancellationToken token)
		{
			using (IReference<IDataIndexContainer> reference = await this.m_domainModelManager.GetDataIndexAsync())
			{
				token.ThrowIfCancellationRequested();
				using (DataIndexPackageWriter dataIndexPackageWriter = DataIndexPackage.CreateWriter(stream))
				{
					reference.Value.Index.WriteTo(dataIndexPackageWriter, token);
				}
			}
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00009A28 File Offset: 0x00007C28
		public async Task WriteSchemaAnnotationAsync(Stream stream, CancellationToken token)
		{
			if (this.m_measureExpressionProvider != null)
			{
				using (IReference<IDatabaseContext> reference = await this.m_domainModelManager.GetDatabaseContextAsync())
				{
					if (reference.Value.GetConceptualSchemaReaders() != null)
					{
						IConceptualSchema conceptualSchema;
						this.m_nlServiceFactory.Value.CreateManagementService(this.m_featureSwitchProvider, LinguisticSchemaServicesBuilderOptions.None).TryGetConceptualSchema(reference.Value, out conceptualSchema);
						IAnnotationProvider<IMeasureLogicalIdentityAnnotation, IConceptualMeasure> annotationProvider;
						if (conceptualSchema.TryGetAnnotationProvider<IMeasureLogicalIdentityAnnotation, IConceptualMeasure>(out annotationProvider))
						{
							MeasureMetadataAnnotationProvider measureMetadataAnnotationProvider = annotationProvider as MeasureMetadataAnnotationProvider;
							if (measureMetadataAnnotationProvider != null)
							{
								SchemaAnnotationContainer.Create(measureMetadataAnnotationProvider).Save(stream);
							}
						}
					}
				}
			}
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00009A73 File Offset: 0x00007C73
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00009A7C File Offset: 0x00007C7C
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				try
				{
					this.m_domainModelManager.Dispose();
				}
				catch (Exception ex)
				{
					if (AsynchronousExceptionDetection.IsStoppingException(ex))
					{
						throw;
					}
					ExploreHostUtils.TraceLuciaSessionDisposalTelemetry(ex);
				}
			}
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00009ABC File Offset: 0x00007CBC
		private void OnDomainModelUpdated()
		{
			EventHandler domainModelUpdated = this.DomainModelUpdated;
			if (domainModelUpdated == null)
			{
				return;
			}
			domainModelUpdated(this, EventArgs.Empty);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00009AD4 File Offset: 0x00007CD4
		private bool IsFaultedOnVerifyRuntime(out LuciaSession.VerifyRuntimeKnownError error, out Exception exception)
		{
			if (this.m_domainModelManager.Status == DomainModelManagerStatus.FaultedOnVerifyRuntime)
			{
				exception = this.m_domainModelManager.VerifyRuntimeException;
				error = ((exception is MissingModuleException) ? LuciaSession.VerifyRuntimeKnownError.MissingRuntimeModule : LuciaSession.VerifyRuntimeKnownError.Unknown);
				return true;
			}
			error = LuciaSession.VerifyRuntimeKnownError.Unknown;
			exception = null;
			return false;
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00009B0C File Offset: 0x00007D0C
		private bool IsFaultedOnVerifyRuntimeWithKnownError(out string message)
		{
			LuciaSession.VerifyRuntimeKnownError verifyRuntimeKnownError;
			Exception ex;
			if (this.IsFaultedOnVerifyRuntime(out verifyRuntimeKnownError, out ex) && verifyRuntimeKnownError == LuciaSession.VerifyRuntimeKnownError.MissingRuntimeModule)
			{
				message = "A system component required by Q&A is missing. You can try installing important updates from Windows Update or manually install the required component from Microsoft (KB2999226).";
				return true;
			}
			message = null;
			return false;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00009B38 File Offset: 0x00007D38
		private void SetInUse()
		{
			if (this.m_usageStatus != LuciaSession.UsageStatus.InUse)
			{
				this.m_usageStatus = LuciaSession.UsageStatus.InUse;
				if (this.m_domainModelManager.Status == DomainModelManagerStatus.StaleDataIndex)
				{
					this.m_domainModelManager.NotifyModelChanged(this.m_filePath, this.m_modelLastModifiedTime, null, new Action(this.OnDomainModelUpdated), true);
				}
			}
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00009B89 File Offset: 0x00007D89
		private static IColumnStatisticsDiscovererWrapper CreateStatisticsDiscoverer(IDbConnectionManager connectionManager, IStreamBasedStorage indexStreamStorage)
		{
			return new ColumnStatisticsDiscovererWrapper((IConceptualSchema schema, string filePath, bool luciaSessionInUse) => new ColumnStatisticsDiscoverer(new ColumnStatisticsProvider(connectionManager, DbCommandCustomizer.Default, ExploreTracer.Instance), ColumnStatisticsStore.CreateFor(schema, indexStreamStorage, (filePath != null) ? filePath.ArrayWrap<string>() : null, !luciaSessionInUse), ExploreTracer.Instance));
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00009BB0 File Offset: 0x00007DB0
		private static InterpretDiagnosticMessage GetDiagnosticMessageFrom(DomainModelManagerStatus status, DataIndex index)
		{
			long num = status - DomainModelManagerStatus.UpdatingDatabaseContext;
			if (num <= 4L)
			{
				switch ((uint)num)
				{
				case 0U:
					return InterpretDiagnosticMessageFactory.StaleDomainModel(1);
				case 1U:
					if (index != DataIndex.Empty)
					{
						return InterpretDiagnosticMessageFactory.StaleDataIndex(1);
					}
					return InterpretDiagnosticMessageFactory.DataIndexNotReady(1);
				case 2U:
					return InterpretDiagnosticMessageFactory.DomainModelUpdateFailure();
				case 3U:
					if (index != DataIndex.Empty)
					{
						return InterpretDiagnosticMessageFactory.DataIndexUpdateFailure();
					}
					return InterpretDiagnosticMessageFactory.DataIndexBuildFailure();
				case 4U:
					return InterpretDiagnosticMessageFactory.StaleDataIndex(1);
				}
			}
			return null;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00009C24 File Offset: 0x00007E24
		private string GetLinguisticSchemaJsonWithOverride(Func<string> getLinguisticSchemaJson)
		{
			if (getLinguisticSchemaJson == null)
			{
				return null;
			}
			if (string.IsNullOrEmpty(this.m_overrideLinguisticSchemaJson))
			{
				return getLinguisticSchemaJson();
			}
			return this.m_overrideLinguisticSchemaJson;
		}

		// Token: 0x0400014A RID: 330
		private static readonly TimeSpan UpdateDatabaseContextDelay = TimeSpan.FromSeconds(1.0);

		// Token: 0x0400014B RID: 331
		private static readonly TimeSpan BuildNewIndexDelay = TimeSpan.FromSeconds(14.0);

		// Token: 0x0400014C RID: 332
		private readonly Lazy<INaturalLanguageServicesFactory> m_nlServiceFactory;

		// Token: 0x0400014D RID: 333
		private readonly IBulkMeasureExpressionProvider m_measureExpressionProvider;

		// Token: 0x0400014E RID: 334
		private readonly IDomainModelManager m_domainModelManager;

		// Token: 0x0400014F RID: 335
		private readonly LinguisticSchemaServices m_linguisticSchemaServices;

		// Token: 0x04000150 RID: 336
		private readonly SchemaMetadataProvider m_schemaMetadataProvider;

		// Token: 0x04000151 RID: 337
		private readonly DataInstanceFilter m_dataInstanceFilter;

		// Token: 0x04000152 RID: 338
		private readonly UtteranceFeedStore m_utteranceFeedStore;

		// Token: 0x04000153 RID: 339
		private readonly LuciaSessionOptions m_luciaSessionOptions;

		// Token: 0x04000154 RID: 340
		private readonly IReadOnlyList<FeatureSwitch> m_featureSwitches;

		// Token: 0x04000155 RID: 341
		private readonly IFeatureSwitchProvider m_featureSwitchProvider;

		// Token: 0x04000156 RID: 342
		private readonly IDbConnectionManager m_connectionManager;

		// Token: 0x04000157 RID: 343
		private readonly IInterpretHandler m_interpretHandler;

		// Token: 0x04000158 RID: 344
		private string m_overrideLinguisticSchemaJson;

		// Token: 0x04000159 RID: 345
		private string m_filePath;

		// Token: 0x0400015A RID: 346
		private DateTime m_modelLastModifiedTime;

		// Token: 0x0400015B RID: 347
		private LuciaSession.ActivationStatus m_activationStatus;

		// Token: 0x0400015C RID: 348
		private LuciaSession.UsageStatus m_usageStatus;

		// Token: 0x020000C2 RID: 194
		private enum ActivationStatus
		{
			// Token: 0x04000291 RID: 657
			Active,
			// Token: 0x04000292 RID: 658
			Inactive,
			// Token: 0x04000293 RID: 659
			Error
		}

		// Token: 0x020000C3 RID: 195
		private enum UsageStatus
		{
			// Token: 0x04000295 RID: 661
			InUse,
			// Token: 0x04000296 RID: 662
			Idle
		}

		// Token: 0x020000C4 RID: 196
		private enum VerifyRuntimeKnownError
		{
			// Token: 0x04000298 RID: 664
			Unknown,
			// Token: 0x04000299 RID: 665
			MissingRuntimeModule
		}
	}
}
