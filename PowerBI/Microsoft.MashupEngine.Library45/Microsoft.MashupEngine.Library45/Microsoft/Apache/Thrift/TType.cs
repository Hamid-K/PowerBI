using System;

namespace Microsoft.Apache.Thrift
{
	// Token: 0x02001FF1 RID: 8177
	internal enum TType : byte
	{
		// Token: 0x0400673B RID: 26427
		Stop,
		// Token: 0x0400673C RID: 26428
		Void,
		// Token: 0x0400673D RID: 26429
		Bool,
		// Token: 0x0400673E RID: 26430
		Byte,
		// Token: 0x0400673F RID: 26431
		Double,
		// Token: 0x04006740 RID: 26432
		I8,
		// Token: 0x04006741 RID: 26433
		I16,
		// Token: 0x04006742 RID: 26434
		I32 = 8,
		// Token: 0x04006743 RID: 26435
		I64 = 10,
		// Token: 0x04006744 RID: 26436
		String,
		// Token: 0x04006745 RID: 26437
		Struct,
		// Token: 0x04006746 RID: 26438
		Map,
		// Token: 0x04006747 RID: 26439
		Set,
		// Token: 0x04006748 RID: 26440
		List
	}
}
