using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x0200006F RID: 111
	internal enum TraceLoggingDataType
	{
		// Token: 0x04000112 RID: 274
		Nil,
		// Token: 0x04000113 RID: 275
		Utf16String,
		// Token: 0x04000114 RID: 276
		MbcsString,
		// Token: 0x04000115 RID: 277
		Int8,
		// Token: 0x04000116 RID: 278
		UInt8,
		// Token: 0x04000117 RID: 279
		Int16,
		// Token: 0x04000118 RID: 280
		UInt16,
		// Token: 0x04000119 RID: 281
		Int32,
		// Token: 0x0400011A RID: 282
		UInt32,
		// Token: 0x0400011B RID: 283
		Int64,
		// Token: 0x0400011C RID: 284
		UInt64,
		// Token: 0x0400011D RID: 285
		Float,
		// Token: 0x0400011E RID: 286
		Double,
		// Token: 0x0400011F RID: 287
		Boolean32,
		// Token: 0x04000120 RID: 288
		Binary,
		// Token: 0x04000121 RID: 289
		Guid,
		// Token: 0x04000122 RID: 290
		FileTime = 17,
		// Token: 0x04000123 RID: 291
		SystemTime,
		// Token: 0x04000124 RID: 292
		HexInt32 = 20,
		// Token: 0x04000125 RID: 293
		HexInt64,
		// Token: 0x04000126 RID: 294
		CountedUtf16String,
		// Token: 0x04000127 RID: 295
		CountedMbcsString,
		// Token: 0x04000128 RID: 296
		Struct,
		// Token: 0x04000129 RID: 297
		Char16 = 518,
		// Token: 0x0400012A RID: 298
		Char8 = 516,
		// Token: 0x0400012B RID: 299
		Boolean8 = 772,
		// Token: 0x0400012C RID: 300
		HexInt8 = 1028,
		// Token: 0x0400012D RID: 301
		HexInt16 = 1030,
		// Token: 0x0400012E RID: 302
		Utf16Xml = 2817,
		// Token: 0x0400012F RID: 303
		MbcsXml,
		// Token: 0x04000130 RID: 304
		CountedUtf16Xml = 2838,
		// Token: 0x04000131 RID: 305
		CountedMbcsXml,
		// Token: 0x04000132 RID: 306
		Utf16Json = 3073,
		// Token: 0x04000133 RID: 307
		MbcsJson,
		// Token: 0x04000134 RID: 308
		CountedUtf16Json = 3094,
		// Token: 0x04000135 RID: 309
		CountedMbcsJson,
		// Token: 0x04000136 RID: 310
		HResult = 3847
	}
}
