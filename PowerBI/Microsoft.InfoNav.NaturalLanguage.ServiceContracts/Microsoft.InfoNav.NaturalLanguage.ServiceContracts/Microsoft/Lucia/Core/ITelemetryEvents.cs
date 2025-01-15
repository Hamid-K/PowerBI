using System;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000AA RID: 170
	public interface ITelemetryEvents
	{
		// Token: 0x06000370 RID: 880
		void UpdateNaturalLanguageServicesMemoryStats(int loadedModelsCount, int virtualTablesCount, int spellCorrectorsCount, int wordBreakersCount, int estimatedUtilizedMemory);

		// Token: 0x06000371 RID: 881
		void TraceConceptualSchemaStats(ContractConceptualSchemaStatistics stats);

		// Token: 0x06000372 RID: 882
		void TraceLinguisticSchemaStats(ContractLinguisticSchemaStatistics stats);

		// Token: 0x06000373 RID: 883
		void TraceModelLoadRetryCount(int retryCount);
	}
}
