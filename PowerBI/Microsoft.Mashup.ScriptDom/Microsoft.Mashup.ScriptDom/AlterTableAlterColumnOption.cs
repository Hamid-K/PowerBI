using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020000FE RID: 254
	internal enum AlterTableAlterColumnOption
	{
		// Token: 0x04000B1D RID: 2845
		NoOptionDefined,
		// Token: 0x04000B1E RID: 2846
		AddRowGuidCol,
		// Token: 0x04000B1F RID: 2847
		DropRowGuidCol,
		// Token: 0x04000B20 RID: 2848
		Null,
		// Token: 0x04000B21 RID: 2849
		NotNull,
		// Token: 0x04000B22 RID: 2850
		AddPersisted,
		// Token: 0x04000B23 RID: 2851
		DropPersisted,
		// Token: 0x04000B24 RID: 2852
		AddNotForReplication,
		// Token: 0x04000B25 RID: 2853
		DropNotForReplication,
		// Token: 0x04000B26 RID: 2854
		AddSparse,
		// Token: 0x04000B27 RID: 2855
		DropSparse
	}
}
