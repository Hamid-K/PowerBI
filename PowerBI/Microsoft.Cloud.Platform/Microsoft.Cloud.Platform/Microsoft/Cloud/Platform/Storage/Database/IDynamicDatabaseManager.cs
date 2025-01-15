using System;

namespace Microsoft.Cloud.Platform.Storage.Database
{
	// Token: 0x02000039 RID: 57
	public interface IDynamicDatabaseManager : IDatabaseManager
	{
		// Token: 0x06000157 RID: 343
		void AddDatabaseSpecification(DatabaseSpecificationConfiguration specification);

		// Token: 0x06000158 RID: 344
		void RemoveDatabaseSpecification(string key);

		// Token: 0x06000159 RID: 345
		void ClearDatabaseSpecifications();
	}
}
