using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000102 RID: 258
	[Flags]
	internal enum AuthenticationTypes
	{
		// Token: 0x04000B36 RID: 2870
		None = 0,
		// Token: 0x04000B37 RID: 2871
		Basic = 1,
		// Token: 0x04000B38 RID: 2872
		Digest = 2,
		// Token: 0x04000B39 RID: 2873
		Integrated = 4,
		// Token: 0x04000B3A RID: 2874
		Ntlm = 8,
		// Token: 0x04000B3B RID: 2875
		Kerberos = 16
	}
}
