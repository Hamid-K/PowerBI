using System;

namespace Microsoft.Cloud.Platform.Storage.Database
{
	// Token: 0x02000036 RID: 54
	public sealed class DatabaseSpecificationEnabledStateChangedEventArgs : EventArgs
	{
		// Token: 0x06000149 RID: 329 RVA: 0x0000514C File Offset: 0x0000334C
		public DatabaseSpecificationEnabledStateChangedEventArgs(StorageOperationMode storageOperationMode, string databaseKey)
		{
			this.OperationMode = storageOperationMode;
			this.DatabaseKey = databaseKey;
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00005162 File Offset: 0x00003362
		// (set) Token: 0x0600014B RID: 331 RVA: 0x0000516A File Offset: 0x0000336A
		public StorageOperationMode OperationMode { get; private set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00005173 File Offset: 0x00003373
		// (set) Token: 0x0600014D RID: 333 RVA: 0x0000517B File Offset: 0x0000337B
		public string DatabaseKey { get; private set; }
	}
}
