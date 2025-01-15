using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Microsoft.DataShaping.Engine;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x02000043 RID: 67
	internal interface IReportingSession
	{
		// Token: 0x06000178 RID: 376
		void Dispose();

		// Token: 0x06000179 RID: 377
		string GetModelString(string maxModelMetadataVersion, TranslationsBehavior? translationsBehavior);

		// Token: 0x0600017A RID: 378
		EngineDataModel GetEngineDataModel(string maxModelMetadataVersion, TranslationsBehavior? translationsBehavior, Func<Stream, IFeatureSwitchProvider, EngineDataModel> parse);

		// Token: 0x0600017B RID: 379
		ConceptualSchemaAndCapabilities GetConceptualSchema(string maxModelMetadataVersion, TranslationsBehavior? translationsBehavior, ParseConceptualSchema parse);

		// Token: 0x0600017C RID: 380
		DataSet GetSchemaDataSet(string schemaName, IReadOnlyDictionary<string, object> restrictions);

		// Token: 0x0600017D RID: 381
		ExploreHostDataSourceInfo GetDataSource();

		// Token: 0x0600017E RID: 382
		void ClearModelCache(DateTime? lastModifiedTime);

		// Token: 0x0600017F RID: 383
		IConnectionFactory GetConnectionFactory();

		// Token: 0x06000180 RID: 384
		IConnectionPool GetConnectionPool();

		// Token: 0x06000181 RID: 385
		ITelemetryService CreateTelemetryServiceForRequest();

		// Token: 0x06000182 RID: 386
		ILuciaSession GetLuciaSession();

		// Token: 0x06000183 RID: 387
		IInsightsSession GetInsightsSession();

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000184 RID: 388
		QueryExecutionOptions DSEQueryExecutionOptions { get; }

		// Token: 0x06000185 RID: 389
		ModelMetadataRequest GetModelMetadataRequest(string modelMetadataVersion, TranslationsBehavior? translationsBehavior);

		// Token: 0x06000186 RID: 390
		SchemaCommandRequest GetSchemaCommandRequest();
	}
}
