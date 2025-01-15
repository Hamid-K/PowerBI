using System;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000065 RID: 101
	public class UpdateKeyValueItemRequest
	{
		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x00010493 File Offset: 0x0000E693
		// (set) Token: 0x060004AE RID: 1198 RVA: 0x0001049B File Offset: 0x0000E69B
		public KeyValueItem Item { get; set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x000104A4 File Offset: 0x0000E6A4
		// (set) Token: 0x060004B0 RID: 1200 RVA: 0x000104AC File Offset: 0x0000E6AC
		public UpdateKeyNotExistsBehavior UpdateKeyNotExistsBehavior { get; set; }

		// Token: 0x060004B1 RID: 1201 RVA: 0x000104B5 File Offset: 0x0000E6B5
		public UpdateKeyValueItemRequest(KeyValueItem item, UpdateKeyNotExistsBehavior updateKeyNotExistsBehavior)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<KeyValueItem>(item, "item");
			ExtendedDiagnostics.EnsureArgumentNotNull<UpdateKeyNotExistsBehavior>(updateKeyNotExistsBehavior, "updateKeyNotExistsBehavior");
			this.Item = item;
			this.UpdateKeyNotExistsBehavior = updateKeyNotExistsBehavior;
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x000104E1 File Offset: 0x0000E6E1
		public UpdateKeyValueItemRequest(UpdatePersistableItemRequest persistableItemRequest)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<UpdatePersistableItemRequest>(persistableItemRequest, "persistableItemRequest");
			this.Item = new KeyValueItem(persistableItemRequest.Item);
			this.UpdateKeyNotExistsBehavior = persistableItemRequest.UpdateKeyNotExistsBehavior;
		}
	}
}
