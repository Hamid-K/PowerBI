using System;
using System.Data.SqlClient;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Storage.Database
{
	// Token: 0x0200004A RID: 74
	public interface IDatabaseSpecification
	{
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001BE RID: 446
		SqlConnectionStringBuilder ConnectionProperties { get; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001BF RID: 447
		string ConnectionString { get; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001C0 RID: 448
		StorageOperationMode OperationMode { get; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001C1 RID: 449
		DatabaseRetriesProfile RetryProfile { get; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001C2 RID: 450
		IThrottler Throttler { get; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001C3 RID: 451
		int BulkInsertBatchSize { get; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001C4 RID: 452
		int CommandTimeout { get; }
	}
}
