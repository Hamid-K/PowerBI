using System;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x02000046 RID: 70
	public enum DataType : byte
	{
		// Token: 0x04000172 RID: 370
		String,
		// Token: 0x04000173 RID: 371
		Integer,
		// Token: 0x04000174 RID: 372
		Decimal,
		// Token: 0x04000175 RID: 373
		Float,
		// Token: 0x04000176 RID: 374
		Boolean,
		// Token: 0x04000177 RID: 375
		DateTime,
		// Token: 0x04000178 RID: 376
		Time = 9,
		// Token: 0x04000179 RID: 377
		Binary = 6,
		// Token: 0x0400017A RID: 378
		EntityKey,
		// Token: 0x0400017B RID: 379
		Null
	}
}
