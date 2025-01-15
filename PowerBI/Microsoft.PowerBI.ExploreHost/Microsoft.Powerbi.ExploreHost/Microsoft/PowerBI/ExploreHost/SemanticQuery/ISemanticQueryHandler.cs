using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Explore.ServiceContracts.Internal;
using Microsoft.PowerBI.Query.Contracts;

namespace Microsoft.PowerBI.ExploreHost.SemanticQuery
{
	// Token: 0x02000043 RID: 67
	public interface ISemanticQueryHandler : IConceptualSchemaRetriever
	{
		// Token: 0x06000228 RID: 552
		Task<string> ExecuteSemanticQueryAsync(string jsonCommands, string databaseID, ASQueryLimits asQueryLimits);

		// Token: 0x06000229 RID: 553
		Task<ExportDataMetadata> ExecuteExportDataQueryAsync(string jsonCommands, string databaseID, Stream output, ASQueryLimits asQueryLimits);

		// Token: 0x0600022A RID: 554
		void Shutdown();

		// Token: 0x0600022B RID: 555
		Task<string> GetPerspectivesInfoAsync(string databaseID);

		// Token: 0x0600022C RID: 556
		SemanticQueryDiagnosticsInfo GetSemanticQueryDiagnosticsInfo(string databaseID, string maxModelMetadataVersion = null);

		// Token: 0x0600022D RID: 557
		Task<object> EditScriptVisualCommandAsync(string jsonCommands, string databaseID, ASQueryLimits asQueryLimits);

		// Token: 0x0600022E RID: 558
		TranslatedGroupingDefinition TranslateGroupingDefinition(GroupingDefinition definition, string databaseID);

		// Token: 0x0600022F RID: 559
		Task<TranslatedGroupingDefinition> TranslateGroupingDefinitionAsync(GroupingDefinition definition, string databaseID);

		// Token: 0x06000230 RID: 560
		TranslatedPartitionColumn TranslatePartitionColumn(GroupingDefinition definition, string databaseID);

		// Token: 0x06000231 RID: 561
		Task<TranslatedPartitionColumn> TranslatePartitionColumnAsync(GroupingDefinition definition, string databaseID);

		// Token: 0x06000232 RID: 562
		DataViewQueryTranslationResult TranslateDataViewQueryDefinition(DataViewQueryDefinition definition, string databaseID);

		// Token: 0x06000233 RID: 563
		Task<DataViewQueryTranslationResult> TranslateDataViewQueryDefinitionAsync(DataViewQueryDefinition definition, string databaseID);
	}
}
