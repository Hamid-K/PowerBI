using System;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B39 RID: 2873
	public class FieldTypeAndIndex
	{
		// Token: 0x170015EC RID: 5612
		// (get) Token: 0x06005AD3 RID: 23251 RVA: 0x00176494 File Offset: 0x00174694
		// (set) Token: 0x06005AD4 RID: 23252 RVA: 0x0017649C File Offset: 0x0017469C
		public FolderFieldType FolderFieldType { get; private set; }

		// Token: 0x170015ED RID: 5613
		// (get) Token: 0x06005AD5 RID: 23253 RVA: 0x001764A5 File Offset: 0x001746A5
		// (set) Token: 0x06005AD6 RID: 23254 RVA: 0x001764AD File Offset: 0x001746AD
		public int FolderFieldIndex { get; private set; }

		// Token: 0x06005AD7 RID: 23255 RVA: 0x001764B6 File Offset: 0x001746B6
		public FieldTypeAndIndex(FolderFieldType folderFieldType, int folderFieldIndex)
		{
			this.FolderFieldType = folderFieldType;
			this.FolderFieldIndex = folderFieldIndex;
		}
	}
}
