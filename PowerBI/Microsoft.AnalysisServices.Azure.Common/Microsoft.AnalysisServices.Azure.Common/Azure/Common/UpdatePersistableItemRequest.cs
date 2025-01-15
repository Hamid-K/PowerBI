using System;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000066 RID: 102
	public class UpdatePersistableItemRequest
	{
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x00010511 File Offset: 0x0000E711
		// (set) Token: 0x060004B4 RID: 1204 RVA: 0x00010519 File Offset: 0x0000E719
		public IPersistable Item { get; set; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x00010522 File Offset: 0x0000E722
		// (set) Token: 0x060004B6 RID: 1206 RVA: 0x0001052A File Offset: 0x0000E72A
		public UpdateKeyNotExistsBehavior UpdateKeyNotExistsBehavior { get; set; }

		// Token: 0x060004B7 RID: 1207 RVA: 0x00010533 File Offset: 0x0000E733
		public UpdatePersistableItemRequest(IPersistable item, UpdateKeyNotExistsBehavior updateKeyNotExistsBehavior)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IPersistable>(item, "item");
			ExtendedDiagnostics.EnsureArgumentNotNull<UpdateKeyNotExistsBehavior>(updateKeyNotExistsBehavior, "updateKeyNotExistsBehavior");
			this.Item = item;
			this.UpdateKeyNotExistsBehavior = updateKeyNotExistsBehavior;
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0001055F File Offset: 0x0000E75F
		public override string ToString()
		{
			return "Item Key: {0}, UpdateKeyNotExistsBehavior: {1}".FormatWithInvariantCulture(new object[]
			{
				this.Item.Key,
				this.UpdateKeyNotExistsBehavior
			});
		}
	}
}
