using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.InfoNav.Explore.ServiceContracts.Internal;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;
using Microsoft.PowerBI.ExploreHost;
using Microsoft.PowerBI.ExploreHost.SemanticQuery;
using Microsoft.PowerBI.Query.Contracts;
using Microsoft.PowerBI.ReportServer.ExploreHost.Error;
using Microsoft.PowerBI.ReportServer.ExploreHost.Logging;
using Microsoft.PowerBI.ReportServer.ExploreHost.Telemetry;
using Microsoft.PowerBI.Telemetry;

namespace Microsoft.PowerBI.ReportServer.ExploreHost
{
	// Token: 0x02000018 RID: 24
	public sealed class RSExploreHost : IRSExploreHost, IDisposable
	{
		// Token: 0x0600008A RID: 138 RVA: 0x000031BB File Offset: 0x000013BB
		public RSExploreHost(IReportServerLogger reportServerLogger, IRSDataSourceProvider rsDataSourceProvider, IReadOnlyDictionary<string, bool> rsFeatureSwitches, string requestId, string clientSessionId)
			: this(reportServerLogger, rsDataSourceProvider, rsFeatureSwitches, requestId, clientSessionId, RSExploreClientFactory.Instance)
		{
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000031D0 File Offset: 0x000013D0
		internal RSExploreHost(IReportServerLogger reportServerLogger, IRSDataSourceProvider rsDataSourceProvider, IReadOnlyDictionary<string, bool> rsFeatureSwitches, string requestId, string clientSessionId, IExploreClientFactory exploreClientFactory)
		{
			RSTelemetryConfiguration rstelemetryConfiguration;
			RSExploreHost.InitializeRsConfiguration(reportServerLogger, requestId, clientSessionId, out rstelemetryConfiguration);
			RSFeatureSwitchesAdapter rsfeatureSwitchesAdapter = new RSFeatureSwitchesAdapter(rsFeatureSwitches);
			RSPowerViewHandler rspowerViewHandler = new RSPowerViewHandler(new RSPowerViewDataSourceProvider(rsDataSourceProvider), rstelemetryConfiguration, rsfeatureSwitchesAdapter);
			this._client = exploreClientFactory.Create(rspowerViewHandler, rsfeatureSwitchesAdapter, false);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003214 File Offset: 0x00001414
		private static void InitializeRsConfiguration(IReportServerLogger reportServerLogger, string requestId, string clientSessionId, out RSTelemetryConfiguration telemetryConfiguration)
		{
			telemetryConfiguration = new RSTelemetryConfiguration(new RSLoggerService(reportServerLogger), requestId, clientSessionId);
			TelemetryService.EnableLogging(telemetryConfiguration);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000322C File Offset: 0x0000142C
		public async Task<string> GetConceptualSchemaAsync(string modelId, string clientSessionId, string request)
		{
			return await this._client.GetClientConceptualSchemaAsync(modelId, request, null, false, TranslationsBehavior.Default);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000327F File Offset: 0x0000147F
		public static string CreateConceptualSchemaErrorObject(Exception ex, IServiceErrorExtractor serviceErrorExtractor)
		{
			return GetConceptualSchemaFlow.CreateConceptualSchemaResponseFromException(ex, ServiceErrorStatusCode.GeneralError, null, new RSExploreHostServiceErrorFactory(serviceErrorExtractor));
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003290 File Offset: 0x00001490
		public async Task<string> ExecuteSemanticQueryAsync(string modelId, string clientSessionId, string request, ASQueryLimits asQueryLimits)
		{
			return await this._client.ExecuteSemanticQueryAsync(request, modelId, asQueryLimits);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000032EC File Offset: 0x000014EC
		public async Task<ExportDataMetadata> ExecuteExportDataQueryAsync(string databaseId, string clientSessionId, string jsonCommands, Stream output, ASQueryLimits asQueryLimits)
		{
			return await this._client.ExecuteExportDataQueryAsync(jsonCommands, databaseId, output, asQueryLimits);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003351 File Offset: 0x00001551
		public void Dispose()
		{
		}

		// Token: 0x04000057 RID: 87
		private readonly IExploreClient _client;
	}
}
