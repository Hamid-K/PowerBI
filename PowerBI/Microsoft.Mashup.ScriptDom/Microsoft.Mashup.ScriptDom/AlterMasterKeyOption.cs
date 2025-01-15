using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020000E9 RID: 233
	[Serializable]
	internal enum AlterMasterKeyOption
	{
		// Token: 0x04000A4B RID: 2635
		None,
		// Token: 0x04000A4C RID: 2636
		Regenerate,
		// Token: 0x04000A4D RID: 2637
		ForceRegenerate,
		// Token: 0x04000A4E RID: 2638
		AddEncryptionByServiceMasterKey,
		// Token: 0x04000A4F RID: 2639
		AddEncryptionByPassword,
		// Token: 0x04000A50 RID: 2640
		DropEncryptionByServiceMasterKey,
		// Token: 0x04000A51 RID: 2641
		DropEncryptionByPassword
	}
}
