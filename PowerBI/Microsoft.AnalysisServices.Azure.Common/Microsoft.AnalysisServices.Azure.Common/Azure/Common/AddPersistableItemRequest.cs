using System;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000046 RID: 70
	public class AddPersistableItemRequest
	{
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600039B RID: 923 RVA: 0x0000F020 File Offset: 0x0000D220
		// (set) Token: 0x0600039C RID: 924 RVA: 0x0000F028 File Offset: 0x0000D228
		public IPersistable Item { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600039D RID: 925 RVA: 0x0000F031 File Offset: 0x0000D231
		// (set) Token: 0x0600039E RID: 926 RVA: 0x0000F039 File Offset: 0x0000D239
		public AddKeyAlreadyExistsBehavior AddKeyAlreadyExistsBehavior { get; set; }

		// Token: 0x0600039F RID: 927 RVA: 0x0000F042 File Offset: 0x0000D242
		public AddPersistableItemRequest(IPersistable item, AddKeyAlreadyExistsBehavior addKeyAlreadyExistsBehavior)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IPersistable>(item, "item");
			ExtendedDiagnostics.EnsureArgumentNotNull<AddKeyAlreadyExistsBehavior>(addKeyAlreadyExistsBehavior, "addKeyAlreadyExistsBehavior");
			this.Item = item;
			this.AddKeyAlreadyExistsBehavior = addKeyAlreadyExistsBehavior;
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000F06E File Offset: 0x0000D26E
		public override string ToString()
		{
			return "Item Key: {0}, AddKeyAlreadyExistsBehavior: {1}".FormatWithInvariantCulture(new object[]
			{
				this.Item.Key,
				this.AddKeyAlreadyExistsBehavior
			});
		}
	}
}
