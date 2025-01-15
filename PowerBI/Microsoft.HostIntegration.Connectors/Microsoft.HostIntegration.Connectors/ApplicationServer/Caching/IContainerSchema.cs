using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000215 RID: 533
	internal interface IContainerSchema
	{
		// Token: 0x060011B4 RID: 4532
		void AddIndex(IIndexSchema iIndexSchema);

		// Token: 0x060011B5 RID: 4533
		IIndexSchema GetIndexSchema();

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x060011B6 RID: 4534
		// (set) Token: 0x060011B7 RID: 4535
		IStoreSchema BaseStoreSchema { get; set; }

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x060011B8 RID: 4536
		// (set) Token: 0x060011B9 RID: 4537
		CommitType CommitType { get; set; }

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x060011BA RID: 4538
		// (set) Token: 0x060011BB RID: 4539
		ExpirationType ExpirationType { get; set; }

		// Token: 0x060011BC RID: 4540
		void AddCallBack(DMCallBackType callBackType, DMOperationCallBack callBack);
	}
}
