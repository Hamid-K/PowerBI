using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x0200053B RID: 1339
	internal enum Token : byte
	{
		// Token: 0x04002078 RID: 8312
		Null,
		// Token: 0x04002079 RID: 8313
		Object,
		// Token: 0x0400207A RID: 8314
		Reference,
		// Token: 0x0400207B RID: 8315
		Enum,
		// Token: 0x0400207C RID: 8316
		GlobalReference,
		// Token: 0x0400207D RID: 8317
		Hashtable = 232,
		// Token: 0x0400207E RID: 8318
		Serializable,
		// Token: 0x0400207F RID: 8319
		SqlGeometry,
		// Token: 0x04002080 RID: 8320
		SqlGeography,
		// Token: 0x04002081 RID: 8321
		DateTimeWithKind,
		// Token: 0x04002082 RID: 8322
		DateTimeOffset,
		// Token: 0x04002083 RID: 8323
		ByteArray,
		// Token: 0x04002084 RID: 8324
		Guid,
		// Token: 0x04002085 RID: 8325
		String,
		// Token: 0x04002086 RID: 8326
		DateTime,
		// Token: 0x04002087 RID: 8327
		TimeSpan,
		// Token: 0x04002088 RID: 8328
		Char,
		// Token: 0x04002089 RID: 8329
		Boolean,
		// Token: 0x0400208A RID: 8330
		Int16,
		// Token: 0x0400208B RID: 8331
		Int32,
		// Token: 0x0400208C RID: 8332
		Int64,
		// Token: 0x0400208D RID: 8333
		UInt16,
		// Token: 0x0400208E RID: 8334
		UInt32,
		// Token: 0x0400208F RID: 8335
		UInt64,
		// Token: 0x04002090 RID: 8336
		Byte,
		// Token: 0x04002091 RID: 8337
		SByte,
		// Token: 0x04002092 RID: 8338
		Single,
		// Token: 0x04002093 RID: 8339
		Double,
		// Token: 0x04002094 RID: 8340
		Decimal
	}
}
