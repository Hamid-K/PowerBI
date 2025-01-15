using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000111 RID: 273
	internal enum ColumnType
	{
		// Token: 0x040010EB RID: 4331
		Regular,
		// Token: 0x040010EC RID: 4332
		IdentityCol,
		// Token: 0x040010ED RID: 4333
		RowGuidCol,
		// Token: 0x040010EE RID: 4334
		Wildcard,
		// Token: 0x040010EF RID: 4335
		PseudoColumnIdentity,
		// Token: 0x040010F0 RID: 4336
		PseudoColumnRowGuid,
		// Token: 0x040010F1 RID: 4337
		PseudoColumnAction,
		// Token: 0x040010F2 RID: 4338
		PseudoColumnCuid
	}
}
