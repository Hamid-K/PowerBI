using System;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x0200007D RID: 125
	public interface IDatabaseEntityLookup
	{
		// Token: 0x060004E0 RID: 1248
		bool TryGetDatabaseEntityByDatabaseId(string databaseId, out DatabaseEntity databaseEntity);

		// Token: 0x060004E1 RID: 1249
		bool TryGetDatabaseEntityByEngineFriendlyName(string databaseExternalName, out DatabaseEntity databaseEntity);
	}
}
