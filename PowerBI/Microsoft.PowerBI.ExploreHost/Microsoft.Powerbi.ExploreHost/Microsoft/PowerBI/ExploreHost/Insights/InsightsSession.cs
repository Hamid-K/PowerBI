using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.BusinessIntelligence;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.ExecutionMetadata;
using Microsoft.InfoNav.Experimental.Insights;
using Microsoft.InfoNav.Experimental.Insights.ServiceContracts.Internal;
using Microsoft.InfoNav.Experimental.Insights.ServiceRuntime.Internal;
using Microsoft.PowerBI.Data.ModelSchemaAnalysis;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;
using Microsoft.PowerBI.ExploreHost.Utils;
using Microsoft.PowerBI.Insights.Hosting;
using Microsoft.PowerBI.Insights.Hosting.AnalysisResult;
using Microsoft.PowerBI.Insights.Hosting.ExecutionMetadata;
using Microsoft.PowerBI.Insights.Hosting.Utils;
using Microsoft.PowerBI.ReportingServicesHost;
using Microsoft.PowerBI.ReportingServicesHost.Insights;
using Microsoft.ReportingServices.Common;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.ExploreHost.Insights
{
	// Token: 0x02000084 RID: 132
	internal sealed class InsightsSession : IInsightsSession, IDisposable
	{
		// Token: 0x06000383 RID: 899 RVA: 0x0000B348 File Offset: 0x00009548
		internal InsightsSession(InsightsSessionParameters insightsSessionParameters, IConnectionFactory connectionFactory, IConnectionPool connectionPool, IDataSourceInfo dataSourceInfo, FeatureSwitches featureSwitches, ITelemetryService telemetryService, Func<string, ParseConceptualSchema, IConceptualSchema> getConceptualSchema, IInsightsSessionStorage insightsSessionStorage, ICancellationTokenManager insightsCancellationTokenManager)
		{
			InsightsSession <>4__this = this;
			this.m_expressionProvider = insightsSessionParameters.ExpressionProvider;
			this.m_dataSourceType = insightsSessionParameters.DataSourceType;
			this.m_connectionFactory = connectionFactory;
			this.m_featureSwitches = featureSwitches;
			this.m_dataSourceInfo = dataSourceInfo;
			this.m_connectionPool = connectionPool;
			this.m_getConceptualSchema = getConceptualSchema;
			this.m_sessionStorage = insightsSessionStorage;
			this.m_cancellationTokenManager = insightsCancellationTokenManager;
			this.m_createInsightsServiceFactory = delegate(FeatureSwitches fs, IBaseTimeProvider baseTimeProvider, bool isQuerySuggestions)
			{
				Microsoft.InfoNav.ITracer instance = ExploreTracer.Instance;
				return new InsightProviderServiceFactory(DesktopConfigurationOverrides.GetConfigurationOverrides(isQuerySuggestions, <>4__this.m_dataSourceType), instance, null, null, new TelemetryEvents(telemetryService), null, baseTimeProvider, new FeatureSwitchProvider(fs.InsightsMParameterEnabled, true, true));
			};
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000B3D0 File Offset: 0x000095D0
		internal InsightsSession(IInsightProviderServiceFactory insightProviderServiceFactoryMock, Func<string, ParseConceptualSchema, IConceptualSchema> getConceptualSchema = null, InsightsSessionParameters insightsSessionParameters = null, IConnectionFactory connectionFactory = null, IConnectionPool connectionPool = null, IDataSourceInfo dataSourceInfo = null, IInsightsSessionStorage insightsSessionStorage = null, ICancellationTokenManager cancellationTokenManager = null)
		{
			this.m_createInsightsServiceFactory = (FeatureSwitches fs, IBaseTimeProvider dateTimeProvider, bool isQuerySuggestions) => insightProviderServiceFactoryMock;
			this.m_getConceptualSchema = getConceptualSchema;
			this.m_dataSourceType = ((insightsSessionParameters != null) ? insightsSessionParameters.DataSourceType : InsightsSessionDataSourceType.Local);
			this.m_expressionProvider = ((insightsSessionParameters != null) ? insightsSessionParameters.ExpressionProvider : null);
			this.m_connectionFactory = connectionFactory;
			this.m_connectionPool = connectionPool;
			this.m_dataSourceInfo = dataSourceInfo;
			this.m_sessionStorage = insightsSessionStorage;
			this.m_cancellationTokenManager = cancellationTokenManager;
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000385 RID: 901 RVA: 0x0000B455 File Offset: 0x00009655
		internal IStorageService StorageService
		{
			get
			{
				return this.m_sessionStorage;
			}
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000B460 File Offset: 0x00009660
		public async Task<string> ExecuteAnalysisAsync(string request)
		{
			DeriveInsightsHostRequest hostRequest;
			string text;
			if (!InsightsSession.TryReadDeriveInsightsRequest(request, out hostRequest))
			{
				text = InsightsSession.m_invalidRequestResult;
			}
			else
			{
				IExecutionMetricsService executionMetricsService = ExecutionMetricsServiceFactory.CreateService(hostRequest.ExecutionMetricsKind);
				DeriveInsightsResult deriveInsightsResult;
				try
				{
					CancellationToken cancellationToken = this.m_cancellationTokenManager.RegisterRequest(hostRequest.JobId);
					deriveInsightsResult = await ExecutionMetricsExtensions.StartNew<DeriveInsightsResult>(executionMetricsService, "Run Analysis Flow", "Analysis Host", (IEventTracker _) => this.DeriveInsightsCoreAsync(hostRequest, cancellationToken, executionMetricsService), delegate(DeriveInsightsResult analysisResult, IEventTracker deriveInsightsEvent)
					{
						ExecutionMetricsExtensions.SetErrorStatusMetric(deriveInsightsEvent, analysisResult);
					});
				}
				finally
				{
					this.m_cancellationTokenManager.UnregisterRequest(hostRequest.JobId);
				}
				text = JsonConvert.SerializeObject(ExecutionMetricsExtensions.WithExecutionMetrics(deriveInsightsResult, executionMetricsService.ToExecutionMetrics()));
			}
			return text;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000B4AB File Offset: 0x000096AB
		public void NotifyModelChanged(InsightsSessionModelChangedArgs _)
		{
			IInsightsSessionStorage sessionStorage = this.m_sessionStorage;
			if (sessionStorage == null)
			{
				return;
			}
			sessionStorage.Clear();
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000B4C0 File Offset: 0x000096C0
		public void CancelAnalysis(string request)
		{
			CancelAnalysisHostRequest cancelAnalysisHostRequest = JsonConvert.DeserializeObject<CancelAnalysisHostRequest>(request);
			this.m_cancellationTokenManager.CancelRequest(cancelAnalysisHostRequest.JobId);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000B4E5 File Offset: 0x000096E5
		public void Dispose()
		{
			this.m_cancellationTokenManager.Dispose();
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000B4F4 File Offset: 0x000096F4
		private async Task<DeriveInsightsResult> DeriveInsightsCoreAsync(DeriveInsightsHostRequest hostRequest, CancellationToken cancellationToken, IExecutionMetricsService executionMetricsService)
		{
			AnalysisDefinitionContainer analysis = null;
			DeriveInsightsResult deriveInsightsResult;
			try
			{
				analysis = hostRequest.Analysis;
				IBaseTimeProvider baseTimeProvider = null;
				if (!string.IsNullOrEmpty(hostRequest.AnchorTime))
				{
					DateTime dateTime;
					if (!AnchorTimeUtils.TryParse(hostRequest.AnchorTime, ref dateTime))
					{
						return InsightsSession.Traced(analysis, DeriveInsightResultErrors.InvalidAnchorTime, this.m_dataSourceType);
					}
					baseTimeProvider = new FixedBaseTimeProvider(dateTime);
				}
				IConceptualSchema conceptualSchema;
				if (!this.TryGetConceptualSchema(executionMetricsService, out conceptualSchema))
				{
					deriveInsightsResult = InsightsSession.Traced(analysis, DeriveInsightResultErrors.FailedToConnectToModel(null), this.m_dataSourceType);
				}
				else
				{
					IMeasureMetadataProvider measureMetadataProvider = null;
					if (this.m_expressionProvider != null)
					{
						measureMetadataProvider = MeasureMetadataProvider.Create(this.m_expressionProvider, conceptualSchema, ExploreTracer.Instance);
					}
					DataSource dataSource = new DataSource(new InsightsConnectionFactory(this.m_connectionFactory, this.m_connectionPool, this.m_dataSourceInfo, DataShapingTracer.Instance), conceptualSchema, measureMetadataProvider, false);
					bool flag = analysis.SuggestQueries != null;
					IInsightProviderService insightProviderService = this.CreateInsightProviderService(baseTimeProvider, flag);
					DeriveInsightsResult deriveInsightsResult2 = await this.DeriveInsightsAsync(insightProviderService, analysis, dataSource, executionMetricsService, hostRequest.ExecutionMetricsKind, cancellationToken);
					deriveInsightsResult = InsightsSession.Traced(analysis, deriveInsightsResult2, this.m_dataSourceType);
				}
			}
			catch (Exception ex) when (!AsynchronousExceptionDetection.IsStoppingException(ex))
			{
				ExploreHostUtils.TraceDeriveInsightsException(ex);
				deriveInsightsResult = InsightsSession.Traced(analysis, DeriveInsightResultErrors.InsightHostException, this.m_dataSourceType);
			}
			return deriveInsightsResult;
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000B550 File Offset: 0x00009750
		private async Task<DeriveInsightsResult> DeriveInsightsAsync(IInsightProviderService runtime, AnalysisDefinitionContainer analysis, IDataSource dataSource, IExecutionMetricsService executionMetricsService, ExecutionMetricsKind executionMetricsKind, CancellationToken cancellationToken)
		{
			DeriveInsightsResult deriveInsightsResult;
			using (ITimedEventTracker analysisEvent = executionMetricsService.BeginEvent("Run Analysis", "Analysis Host"))
			{
				try
				{
					deriveInsightsResult = DeriveInsightsResultExtensions.ToDeriveInsightsResult(await runtime.DeriveInsightsAsync(analysis, dataSource, this.m_sessionStorage, executionMetricsService, executionMetricsKind, cancellationToken));
				}
				catch (OperationCanceledException)
				{
					ExecutionMetricsExtensions.MarkAsCancelled(analysisEvent);
					deriveInsightsResult = DeriveInsightResultErrors.AnalysisCancelled;
				}
				catch (Exception ex) when (!AsynchronousExceptionDetection.IsStoppingException(ex))
				{
					ExploreHostUtils.TraceDeriveInsightsException(ex);
					ExecutionMetricsExtensions.MarkAsError(analysisEvent);
					deriveInsightsResult = DeriveInsightResultErrors.InsightRuntimeException;
				}
			}
			return deriveInsightsResult;
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000B5C8 File Offset: 0x000097C8
		private bool TryGetConceptualSchema(IExecutionMetricsService executionMetricsService, out IConceptualSchema conceptualSchema)
		{
			string text = "2.5";
			bool flag;
			using (ITimedEventTracker timedEventTracker = executionMetricsService.BeginEvent("Get Conceptual Schema", "Analysis Host"))
			{
				try
				{
					Func<string, ParseConceptualSchema, IConceptualSchema> getConceptualSchema = this.m_getConceptualSchema;
					string text2 = text;
					ParseConceptualSchema parseConceptualSchema;
					if ((parseConceptualSchema = InsightsSession.<>O.<0>__ParseConceptualSchema) == null)
					{
						parseConceptualSchema = (InsightsSession.<>O.<0>__ParseConceptualSchema = new ParseConceptualSchema(ExploreHostUtils.ParseConceptualSchema));
					}
					conceptualSchema = getConceptualSchema(text2, parseConceptualSchema);
					flag = true;
				}
				catch (Exception ex) when (!AsynchronousExceptionDetection.IsStoppingException(ex))
				{
					ExploreHostUtils.TraceDeriveInsightsException(ex);
					ExecutionMetricsExtensions.MarkAsError(timedEventTracker);
					conceptualSchema = null;
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000B670 File Offset: 0x00009870
		internal IInsightProviderService CreateInsightProviderService(IBaseTimeProvider baseTimeProvider, bool isQuerySuggestions)
		{
			return this.m_createInsightsServiceFactory(this.m_featureSwitches, baseTimeProvider, isQuerySuggestions).CreateInsightProviderService();
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000B68C File Offset: 0x0000988C
		private static bool TryReadDeriveInsightsRequest(string jsonHostRequest, out DeriveInsightsHostRequest hostRequest)
		{
			bool flag;
			try
			{
				hostRequest = JsonConvert.DeserializeObject<DeriveInsightsHostRequest>(jsonHostRequest);
				flag = true;
			}
			catch (Exception ex) when (!AsynchronousExceptionDetection.IsStoppingException(ex))
			{
				ExploreHostUtils.TraceDeriveInsightsException(ex);
				hostRequest = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000B6E0 File Offset: 0x000098E0
		private static DeriveInsightsResult Traced(AnalysisDefinitionContainer analysis, DeriveInsightsResult result, InsightsSessionDataSourceType dataSourceType)
		{
			ExploreHostUtils.TraceDeriveInsightsCompleted(((analysis != null) ? analysis.GetName() : null) ?? "(null)", result, dataSourceType == InsightsSessionDataSourceType.LocalWithDirectQuery);
			return result;
		}

		// Token: 0x040001A1 RID: 417
		private readonly IConnectionFactory m_connectionFactory;

		// Token: 0x040001A2 RID: 418
		private readonly FeatureSwitches m_featureSwitches;

		// Token: 0x040001A3 RID: 419
		private readonly IDataSourceInfo m_dataSourceInfo;

		// Token: 0x040001A4 RID: 420
		private readonly IConnectionPool m_connectionPool;

		// Token: 0x040001A5 RID: 421
		private readonly IMeasureExpressionProvider m_expressionProvider;

		// Token: 0x040001A6 RID: 422
		private readonly InsightsSessionDataSourceType m_dataSourceType;

		// Token: 0x040001A7 RID: 423
		private readonly IInsightsSessionStorage m_sessionStorage;

		// Token: 0x040001A8 RID: 424
		private readonly ICancellationTokenManager m_cancellationTokenManager;

		// Token: 0x040001A9 RID: 425
		private readonly Func<string, ParseConceptualSchema, IConceptualSchema> m_getConceptualSchema;

		// Token: 0x040001AA RID: 426
		private static readonly string m_invalidRequestResult = JsonConvert.SerializeObject(DeriveInsightResultErrors.InvalidRequest);

		// Token: 0x040001AB RID: 427
		private readonly InsightsSession.CreateInsightsServiceFactory m_createInsightsServiceFactory;

		// Token: 0x020000E4 RID: 228
		// (Invoke) Token: 0x0600047A RID: 1146
		private delegate IInsightProviderServiceFactory CreateInsightsServiceFactory(FeatureSwitches featureSwitches, IBaseTimeProvider baseTimeProvider, bool isQuerySuggestions);

		// Token: 0x020000E5 RID: 229
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000325 RID: 805
			public static ParseConceptualSchema <0>__ParseConceptualSchema;
		}
	}
}
