using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200018A RID: 394
	[Flags]
	[Serializable]
	internal enum XmlForClauseOptions
	{
		// Token: 0x04001991 RID: 6545
		None = 0,
		// Token: 0x04001992 RID: 6546
		Raw = 1,
		// Token: 0x04001993 RID: 6547
		Auto = 2,
		// Token: 0x04001994 RID: 6548
		Explicit = 4,
		// Token: 0x04001995 RID: 6549
		Path = 8,
		// Token: 0x04001996 RID: 6550
		XmlData = 16,
		// Token: 0x04001997 RID: 6551
		XmlSchema = 32,
		// Token: 0x04001998 RID: 6552
		Elements = 64,
		// Token: 0x04001999 RID: 6553
		ElementsXsiNil = 128,
		// Token: 0x0400199A RID: 6554
		ElementsAbsent = 256,
		// Token: 0x0400199B RID: 6555
		BinaryBase64 = 512,
		// Token: 0x0400199C RID: 6556
		Type = 1024,
		// Token: 0x0400199D RID: 6557
		Root = 2048,
		// Token: 0x0400199E RID: 6558
		ElementsAll = 448
	}
}
