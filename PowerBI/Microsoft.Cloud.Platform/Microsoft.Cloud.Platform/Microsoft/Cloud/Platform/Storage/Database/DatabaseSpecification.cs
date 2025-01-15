using System;
using System.Data.SqlClient;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Storage.Database
{
	// Token: 0x0200004B RID: 75
	public sealed class DatabaseSpecification : IDatabaseSpecification
	{
		// Token: 0x060001C5 RID: 453 RVA: 0x000063A7 File Offset: 0x000045A7
		public DatabaseSpecification(string connectionString, StorageOperationMode operationMode, DatabaseRetriesProfile retryProfile, int commandTimeout, IThrottler throttler, int bulkInsertBatchSize)
		{
			this.ConnectionProperties = new SqlConnectionStringBuilder(connectionString);
			this.OperationMode = operationMode;
			this.RetryProfile = retryProfile;
			this.CommandTimeout = commandTimeout;
			this.Throttler = throttler;
			this.BulkInsertBatchSize = bulkInsertBatchSize;
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x000063E1 File Offset: 0x000045E1
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x000063E9 File Offset: 0x000045E9
		public SqlConnectionStringBuilder ConnectionProperties { get; private set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x000063F2 File Offset: 0x000045F2
		public string ConnectionString
		{
			get
			{
				return this.ConnectionProperties.ConnectionString;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x000063FF File Offset: 0x000045FF
		// (set) Token: 0x060001CA RID: 458 RVA: 0x00006407 File Offset: 0x00004607
		public StorageOperationMode OperationMode { get; private set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00006410 File Offset: 0x00004610
		// (set) Token: 0x060001CC RID: 460 RVA: 0x00006418 File Offset: 0x00004618
		public DatabaseRetriesProfile RetryProfile { get; private set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00006421 File Offset: 0x00004621
		// (set) Token: 0x060001CE RID: 462 RVA: 0x00006429 File Offset: 0x00004629
		public IThrottler Throttler { get; private set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00006432 File Offset: 0x00004632
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x0000643A File Offset: 0x0000463A
		public int BulkInsertBatchSize { get; private set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x00006443 File Offset: 0x00004643
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x0000644B File Offset: 0x0000464B
		public int CommandTimeout { get; private set; }

		// Token: 0x060001D3 RID: 467 RVA: 0x00006454 File Offset: 0x00004654
		public override string ToString()
		{
			return "<Server: {0}, Database: {1} Mode: {2}, (Throttled: {3})>".FormatWithInvariantCulture(new object[]
			{
				this.ConnectionProperties.DataSource,
				this.ConnectionProperties.InitialCatalog,
				this.OperationMode,
				(this.Throttler != null) ? "Yes" : "No"
			});
		}
	}
}
