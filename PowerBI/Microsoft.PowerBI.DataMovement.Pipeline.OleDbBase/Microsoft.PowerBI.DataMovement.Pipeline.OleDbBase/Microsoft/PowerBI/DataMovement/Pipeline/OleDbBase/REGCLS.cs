using System;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000053 RID: 83
	[Flags]
	[CLSCompliant(false)]
	public enum REGCLS : uint
	{
		// Token: 0x040000F2 RID: 242
		SINGLEUSE = 0U,
		// Token: 0x040000F3 RID: 243
		MULTIPLEUSE = 1U,
		// Token: 0x040000F4 RID: 244
		MULTI_SEPARATE = 2U,
		// Token: 0x040000F5 RID: 245
		SUSPENDED = 4U,
		// Token: 0x040000F6 RID: 246
		SURROGATE = 8U
	}
}
