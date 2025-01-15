using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000055 RID: 85
	[Flags]
	public enum SqlBulkCopyOptions
	{
		// Token: 0x0400012F RID: 303
		Default = 0,
		// Token: 0x04000130 RID: 304
		KeepIdentity = 1,
		// Token: 0x04000131 RID: 305
		CheckConstraints = 2,
		// Token: 0x04000132 RID: 306
		TableLock = 4,
		// Token: 0x04000133 RID: 307
		KeepNulls = 8,
		// Token: 0x04000134 RID: 308
		FireTriggers = 16,
		// Token: 0x04000135 RID: 309
		UseInternalTransaction = 32,
		// Token: 0x04000136 RID: 310
		AllowEncryptedValueModifications = 64
	}
}
