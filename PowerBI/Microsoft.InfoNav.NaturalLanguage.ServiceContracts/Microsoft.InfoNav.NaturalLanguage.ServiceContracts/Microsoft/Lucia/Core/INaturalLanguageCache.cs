using System;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000051 RID: 81
	public interface INaturalLanguageCache
	{
		// Token: 0x0600016B RID: 363
		ModelLinguisticSchemasCacheEntry GetLinguisticSchemas(string databaseName);

		// Token: 0x0600016C RID: 364
		void UpdateLinguisticSchemas(string databaseName, ModelLinguisticSchemasCacheEntry linguisticSchemas);

		// Token: 0x0600016D RID: 365
		void InvalidateLinguisticSchemas(string databaseName);
	}
}
