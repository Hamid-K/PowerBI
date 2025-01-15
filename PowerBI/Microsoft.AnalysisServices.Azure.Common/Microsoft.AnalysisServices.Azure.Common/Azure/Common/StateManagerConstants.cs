using System;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x0200003D RID: 61
	public static class StateManagerConstants
	{
		// Token: 0x040000DA RID: 218
		public const string KEY_DELIMITER = ".";

		// Token: 0x040000DB RID: 219
		public const string DEFAULT_SUBSCRIPTION_NAME = "CA4468AA-562B-4B75-B2BB-1D82608E42BB";

		// Token: 0x040000DC RID: 220
		public const string DEFAULT_AUTHORITY_ID = "powerbi";

		// Token: 0x040000DD RID: 221
		public const string SUBSCRIPTIONS_TABLE_NAME = "Subscriptions";

		// Token: 0x040000DE RID: 222
		public const string VIRTUAL_SERVERS_TABLE_NAME = "VirtualServers";

		// Token: 0x040000DF RID: 223
		public const string DATABASES_TABLE_NAME = "Databases";

		// Token: 0x040000E0 RID: 224
		public const string MIGRATION_INFO_TABLE_NAME = "MigrationInfo";

		// Token: 0x040000E1 RID: 225
		public const string CONTAINER_CLEANUP_INFO_TABLE_NAME = "ContainerCleanupInfo";

		// Token: 0x040000E2 RID: 226
		public const string CONTAINER_CLEANUP_RECORDS_TABLE_NAME = "ContainerCleanupRecords";

		// Token: 0x040000E3 RID: 227
		public const string BLOB_MIGRATION_INFO_TABLE_NAME = "BlobMigrationInfo";

		// Token: 0x040000E4 RID: 228
		public const string FABRIC_WRITE_SERVICE_URI_TEMPLATE = "fabric:/saas/service/analyticswrite/{0}";
	}
}
