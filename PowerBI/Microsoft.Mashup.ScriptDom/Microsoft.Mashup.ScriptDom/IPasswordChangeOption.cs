using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001A3 RID: 419
	internal interface IPasswordChangeOption
	{
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060021E0 RID: 8672
		// (set) Token: 0x060021E1 RID: 8673
		Literal EncryptionPassword { get; set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060021E2 RID: 8674
		// (set) Token: 0x060021E3 RID: 8675
		Literal DecryptionPassword { get; set; }
	}
}
