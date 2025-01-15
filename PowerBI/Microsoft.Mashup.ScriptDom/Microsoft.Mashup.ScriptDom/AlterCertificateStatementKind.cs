using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020000E8 RID: 232
	[Serializable]
	internal enum AlterCertificateStatementKind
	{
		// Token: 0x04000A44 RID: 2628
		None,
		// Token: 0x04000A45 RID: 2629
		RemovePrivateKey,
		// Token: 0x04000A46 RID: 2630
		RemoveAttestedOption,
		// Token: 0x04000A47 RID: 2631
		WithPrivateKey,
		// Token: 0x04000A48 RID: 2632
		WithActiveForBeginDialog,
		// Token: 0x04000A49 RID: 2633
		AttestedBy
	}
}
