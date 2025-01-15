using System;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000045 RID: 69
	public class AddKeyValueItemRequest
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000395 RID: 917 RVA: 0x0000EFA2 File Offset: 0x0000D1A2
		// (set) Token: 0x06000396 RID: 918 RVA: 0x0000EFAA File Offset: 0x0000D1AA
		public KeyValueItem Item { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000397 RID: 919 RVA: 0x0000EFB3 File Offset: 0x0000D1B3
		// (set) Token: 0x06000398 RID: 920 RVA: 0x0000EFBB File Offset: 0x0000D1BB
		public AddKeyAlreadyExistsBehavior AddKeyAlreadyExistsBehavior { get; set; }

		// Token: 0x06000399 RID: 921 RVA: 0x0000EFC4 File Offset: 0x0000D1C4
		public AddKeyValueItemRequest(KeyValueItem item, AddKeyAlreadyExistsBehavior addKeyAlreadyExistsBehavior)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<KeyValueItem>(item, "item");
			ExtendedDiagnostics.EnsureArgumentNotNull<AddKeyAlreadyExistsBehavior>(addKeyAlreadyExistsBehavior, "addKeyAlreadyExistsBehavior");
			this.Item = item;
			this.AddKeyAlreadyExistsBehavior = addKeyAlreadyExistsBehavior;
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000EFF0 File Offset: 0x0000D1F0
		public AddKeyValueItemRequest(AddPersistableItemRequest persistableItemRequest)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<AddPersistableItemRequest>(persistableItemRequest, "persistableItemRequest");
			this.Item = new KeyValueItem(persistableItemRequest.Item);
			this.AddKeyAlreadyExistsBehavior = persistableItemRequest.AddKeyAlreadyExistsBehavior;
		}
	}
}
