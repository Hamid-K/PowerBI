using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using Microsoft.BusinessIntelligence;
using Microsoft.DataShaping.Engine;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;
using Microsoft.PowerBI.ExploreHost.Contracts;
using Microsoft.PowerBI.ReportingServicesHost.Insights;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x02000041 RID: 65
	internal interface IPowerViewHandler : IReportingSessionProvider, IDataSourceStateProvider
	{
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000163 RID: 355
		FeatureSwitches FeatureSwitches { get; }

		// Token: 0x06000164 RID: 356
		void Shutdown();

		// Token: 0x06000165 RID: 357
		string GetModelString(string databaseID, string maxModelMetadataVersion, TranslationsBehavior? translationsBehavior);

		// Token: 0x06000166 RID: 358
		string GetModelString(ASConnectionInfo asConnectionInfo, string maxModelMetadataVersion, TranslationsBehavior translationsBehavior);

		// Token: 0x06000167 RID: 359
		EngineDataModel GetEngineDataModel(string databaseID, string maxModelMetadataVersion, TranslationsBehavior? translationsBehavior, Func<Stream, IFeatureSwitchProvider, EngineDataModel> parser);

		// Token: 0x06000168 RID: 360
		ConceptualSchemaAndCapabilities GetConceptualSchema(string databaseID, string maxModelMetadataVersion, TranslationsBehavior? translationsBehavior, ParseConceptualSchema parser);

		// Token: 0x06000169 RID: 361
		ConceptualSchemaAndCapabilities GetConceptualSchema(ASConnectionInfo asConnectionInfo, string maxModelMetadataVersion, TranslationsBehavior translationsBehavior, ParseConceptualSchema parser);

		// Token: 0x0600016A RID: 362
		DataSet GetSchemaDataSet(string databaseID, string schemaName, IReadOnlyDictionary<string, object> restrictions);

		// Token: 0x0600016B RID: 363
		void EnsureSession(string databaseID);

		// Token: 0x0600016C RID: 364
		Task EnsureSessionAsync(string databaseID);

		// Token: 0x0600016D RID: 365
		ModelLocation GetModelLocation(string databaseID);

		// Token: 0x0600016E RID: 366
		void CreateNewReportingSession(ASConnectionInfo connectionInfo, LuciaSessionParameters luciaSessionParameters, QueryExecutionOptionsBase queryExecutionOptions = null, SchemaOptions schemaOptions = null, InsightsSessionParameters insightsSessionParameters = null);

		// Token: 0x0600016F RID: 367
		void DisconnectReportingSession(string databaseID);

		// Token: 0x06000170 RID: 368
		void ClearAllModelCaches(DateTime? modelLastModifiedTime = null);

		// Token: 0x06000171 RID: 369
		void ClearModelCache(string databaseID, DateTime? modelLastModifiedTime = null);

		// Token: 0x06000172 RID: 370
		void ClearCachesForDataSource(string connectionString);

		// Token: 0x06000173 RID: 371
		QueryExecutionOptions GetDSEQueryExecutionOptions(string databaseId);
	}
}
