using System;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x020000A4 RID: 164
	public interface IPersistable
	{
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060005E9 RID: 1513
		string Key { get; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060005EA RID: 1514
		bool IsBackupEnabled { get; }

		// Token: 0x060005EB RID: 1515
		byte[] Serialize();

		// Token: 0x060005EC RID: 1516
		T Deserialize<T>(byte[] data);

		// Token: 0x060005ED RID: 1517
		IPersistable Clone();

		// Token: 0x060005EE RID: 1518
		PersistableItemTypes EnumType();
	}
}
