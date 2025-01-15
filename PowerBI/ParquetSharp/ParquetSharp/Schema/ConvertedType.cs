using System;

namespace ParquetSharp.Schema
{
	// Token: 0x02000095 RID: 149
	public enum ConvertedType
	{
		// Token: 0x0400013E RID: 318
		None,
		// Token: 0x0400013F RID: 319
		UTF8,
		// Token: 0x04000140 RID: 320
		Map,
		// Token: 0x04000141 RID: 321
		MapKeyValue,
		// Token: 0x04000142 RID: 322
		List,
		// Token: 0x04000143 RID: 323
		Enum,
		// Token: 0x04000144 RID: 324
		Decimal,
		// Token: 0x04000145 RID: 325
		Date,
		// Token: 0x04000146 RID: 326
		TimeMillis,
		// Token: 0x04000147 RID: 327
		TimeMicros,
		// Token: 0x04000148 RID: 328
		TimestampMillis,
		// Token: 0x04000149 RID: 329
		TimestampMicros,
		// Token: 0x0400014A RID: 330
		UInt8,
		// Token: 0x0400014B RID: 331
		UInt16,
		// Token: 0x0400014C RID: 332
		UInt32,
		// Token: 0x0400014D RID: 333
		UInt64,
		// Token: 0x0400014E RID: 334
		Int8,
		// Token: 0x0400014F RID: 335
		Int16,
		// Token: 0x04000150 RID: 336
		Int32,
		// Token: 0x04000151 RID: 337
		Int64,
		// Token: 0x04000152 RID: 338
		Json,
		// Token: 0x04000153 RID: 339
		Bson,
		// Token: 0x04000154 RID: 340
		Interval,
		// Token: 0x04000155 RID: 341
		NA = 25,
		// Token: 0x04000156 RID: 342
		Undefined
	}
}
