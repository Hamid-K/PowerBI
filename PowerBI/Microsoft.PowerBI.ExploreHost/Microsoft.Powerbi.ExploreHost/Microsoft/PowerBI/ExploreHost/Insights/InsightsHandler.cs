using System;
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
	// Token: 0x02000083 RID: 131
	internal sealed class InsightsHandler : IInsightsHandler
	{
		// Token: 0x06000376 RID: 886 RVA: 0x0000AFA4 File Offset: 0x000091A4
		internal InsightsHandler(IPowerViewHandler powerViewHandler)
		{
			this.m_powerViewHandler = powerViewHandler;
			this.m_createInsightsServiceFactory = delegate(string databaseId, FeatureSwitches fs, IBaseTimeProvider baseTimeProvider, bool isQuerySuggestions)
			{
				Microsoft.InfoNav.ITracer instance = ExploreTracer.Instance;
				return new InsightProviderServiceFactory(DesktopConfigurationOverrides.GetConfigurationOverrides(isQuerySuggestions, InsightsSessionDataSourceType.Other), instance, null, null, new TelemetryEvents(this.m_powerViewHandler.CreateTelemetryServiceForRequest(databaseId)), null, baseTimeProvider, new FeatureSwitchProvider(fs.InsightsMParameterEnabled, true, true));
			};
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000AFC8 File Offset: 0x000091C8
		internal InsightsHandler(IPowerViewHandler powerViewHandler, IInsightProviderServiceFactory insightProviderServiceFactoryMock)
		{
			this.m_powerViewHandler = powerViewHandler;
			this.m_createInsightsServiceFactory = (string databaseId, FeatureSwitches fs, IBaseTimeProvider dateTimeProvider, bool isQuerySuggestions) => insightProviderServiceFactoryMock;
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000B004 File Offset: 0x00009204
		public async Task<string> DeriveInsightsAsync(string request, string databaseID, CancellationToken cancellationToken, IMeasureExpressionProvider expressionProvider = null, IModel model = null)
		{
			DeriveInsightsHostRequest hostRequest;
			string text;
			if (!InsightsHandler.TryReadDeriveInsightsRequest(request, out hostRequest))
			{
				text = InsightsHandler.m_invalidRequestResult;
			}
			else
			{
				IExecutionMetricsService executionMetricsService = ExecutionMetricsServiceFactory.CreateService(hostRequest.ExecutionMetricsKind);
				text = JsonConvert.SerializeObject(ExecutionMetricsExtensions.WithExecutionMetrics(await ExecutionMetricsExtensions.StartNew<DeriveInsightsResult>(executionMetricsService, "Run Analysis Flow", "Analysis Host", (IEventTracker _) => this.DeriveInsightsCoreAsync(hostRequest, databaseID, cancellationToken, expressionProvider, model, executionMetricsService), delegate(DeriveInsightsResult analysisResult, IEventTracker deriveInsightsEvent)
				{
					ExecutionMetricsExtensions.SetErrorStatusMetric(deriveInsightsEvent, analysisResult);
				}), executionMetricsService.ToExecutionMetrics()));
			}
			return text;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000B071 File Offset: 0x00009271
		public Task<string> ExecuteAnalysisAsync(string request, string databaseID)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000B078 File Offset: 0x00009278
		public void CancelAnalysis(string request, string databaseID)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0000B080 File Offset: 0x00009280
		private async Task<DeriveInsightsResult> DeriveInsightsCoreAsync(DeriveInsightsHostRequest hostRequest, string databaseID, CancellationToken cancellationToken, IMeasureExpressionProvider expressionProvider, IModel model, IExecutionMetricsService executionMetricsService)
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
						return InsightsHandler.Traced(analysis, DeriveInsightResultErrors.InvalidAnchorTime, model);
					}
					baseTimeProvider = new FixedBaseTimeProvider(dateTime);
				}
				InsightsConnectionFactory insightsConnectionFactory;
				IConceptualSchema conceptualSchema;
				if (!this.TryGetConceptualSchema(databaseID, executionMetricsService, out insightsConnectionFactory, out conceptualSchema))
				{
					deriveInsightsResult = InsightsHandler.Traced(analysis, DeriveInsightResultErrors.FailedToConnectToModel(null), model);
				}
				else
				{
					IMeasureMetadataProvider measureMetadataProvider = null;
					if (expressionProvider != null)
					{
						measureMetadataProvider = MeasureMetadataProvider.Create(expressionProvider, conceptualSchema, ExploreTracer.Instance);
					}
					DataSource dataSource = new DataSource(insightsConnectionFactory, conceptualSchema, measureMetadataProvider, false);
					bool flag = analysis.SuggestQueries != null;
					IInsightProviderService insightProviderService = this.CreateInsightProviderService(databaseID, baseTimeProvider, flag);
					DeriveInsightsResult deriveInsightsResult2 = await this.DeriveInsightsAsync(insightProviderService, analysis, dataSource, executionMetricsService, hostRequest.ExecutionMetricsKind, cancellationToken);
					deriveInsightsResult = InsightsHandler.Traced(analysis, deriveInsightsResult2, model);
				}
			}
			catch (Exception ex) when (!AsynchronousExceptionDetection.IsStoppingException(ex))
			{
				ExploreHostUtils.TraceDeriveInsightsException(ex);
				deriveInsightsResult = InsightsHandler.Traced(analysis, DeriveInsightResultErrors.InsightHostException, model);
			}
			return deriveInsightsResult;
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000B0F8 File Offset: 0x000092F8
		private bool TryGetConceptualSchema(string databaseID, IExecutionMetricsService executionMetricsService, out InsightsConnectionFactory connectionFactory, out IConceptualSchema schema)
		{
			bool flag;
			using (ITimedEventTracker timedEventTracker = executionMetricsService.BeginEvent("Get Conceptual Schema", "Analysis Host"))
			{
				try
				{
					IDataShapingDataSourceInfo dataSourceInfo = this.m_powerViewHandler.GetDataSourceInfo(databaseID);
					connectionFactory = new InsightsConnectionFactory(this.m_powerViewHandler.GetConnectionFactory(databaseID), this.m_powerViewHandler.GetConnectionPool(databaseID), new DataSourceInfo(dataSourceInfo.Name, dataSourceInfo.Extension, dataSourceInfo.ConnectionString), DataShapingTracer.Instance);
					string text = "2.5";
					schema = ExploreHostUtils.GetConceptualSchema(this.m_powerViewHandler, databaseID, text, null);
					flag = true;
				}
				catch (Exception ex) when (!AsynchronousExceptionDetection.IsStoppingException(ex))
				{
					ExploreHostUtils.TraceDeriveInsightsException(ex);
					ExecutionMetricsExtensions.MarkAsError(timedEventTracker);
					connectionFactory = null;
					schema = null;
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000B1E4 File Offset: 0x000093E4
		private async Task<DeriveInsightsResult> DeriveInsightsAsync(IInsightProviderService runtime, AnalysisDefinitionContainer analysis, IDataSource dataSource, IExecutionMetricsService executionMetricsService, ExecutionMetricsKind executionMetricsKind, CancellationToken cancellationToken)
		{
			DeriveInsightsResult deriveInsightsResult;
			using (ITimedEventTracker analysisEvent = executionMetricsService.BeginEvent("Run Analysis", "Analysis Host"))
			{
				try
				{
					deriveInsightsResult = DeriveInsightsResultExtensions.ToDeriveInsightsResult(await runtime.DeriveInsightsAsync(analysis, dataSource, null, executionMetricsService, executionMetricsKind, cancellationToken));
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

		// Token: 0x0600037E RID: 894 RVA: 0x0000B252 File Offset: 0x00009452
		internal IInsightProviderService CreateInsightProviderService(string databaseID, IBaseTimeProvider baseTimeProvider, bool isQuerySuggestions)
		{
			return this.m_createInsightsServiceFactory(databaseID, this.m_powerViewHandler.FeatureSwitches, baseTimeProvider, isQuerySuggestions).CreateInsightProviderService();
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000B274 File Offset: 0x00009474
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

		// Token: 0x06000380 RID: 896 RVA: 0x0000B2C8 File Offset: 0x000094C8
		private static DeriveInsightsResult Traced(AnalysisDefinitionContainer analysis, DeriveInsightsResult result, IModel model)
		{
			ExploreHostUtils.TraceDeriveInsightsCompleted(((analysis != null) ? analysis.GetName() : null) ?? "(null)", result, model != null && model.HasDirectQueryContent);
			return result;
		}

		// Token: 0x0400019E RID: 414
		private static readonly string m_invalidRequestResult = JsonConvert.SerializeObject(DeriveInsightResultErrors.InvalidRequest);

		// Token: 0x0400019F RID: 415
		private readonly IPowerViewHandler m_powerViewHandler;

		// Token: 0x040001A0 RID: 416
		private readonly InsightsHandler.CreateInsightsServiceFactory m_createInsightsServiceFactory;

		// Token: 0x020000DD RID: 221
		// (Invoke) Token: 0x06000469 RID: 1129
		private delegate IInsightProviderServiceFactory CreateInsightsServiceFactory(string databaseId, FeatureSwitches featureSwitches, IBaseTimeProvider baseTimeProvider, bool isQuerySuggestions);
	}
}
