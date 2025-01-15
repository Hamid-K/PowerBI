using System;

namespace Microsoft.ProgramSynthesis.Compound.Split.Semantics
{
	// Token: 0x0200099C RID: 2460
	internal enum RecordStatus
	{
		// Token: 0x04001B29 RID: 6953
		FieldStart,
		// Token: 0x04001B2A RID: 6954
		FieldEnd,
		// Token: 0x04001B2B RID: 6955
		InField,
		// Token: 0x04001B2C RID: 6956
		InQuotedField,
		// Token: 0x04001B2D RID: 6957
		Escape,
		// Token: 0x04001B2E RID: 6958
		EscapeInQuotedField,
		// Token: 0x04001B2F RID: 6959
		EndRecord,
		// Token: 0x04001B30 RID: 6960
		Error
	}
}
