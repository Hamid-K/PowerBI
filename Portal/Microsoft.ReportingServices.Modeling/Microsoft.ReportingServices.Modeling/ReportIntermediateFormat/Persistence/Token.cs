using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x0200001E RID: 30
	internal enum Token : byte
	{
		// Token: 0x040000F8 RID: 248
		Null,
		// Token: 0x040000F9 RID: 249
		Object,
		// Token: 0x040000FA RID: 250
		Reference,
		// Token: 0x040000FB RID: 251
		Enum,
		// Token: 0x040000FC RID: 252
		GlobalReference,
		// Token: 0x040000FD RID: 253
		Hashtable = 232,
		// Token: 0x040000FE RID: 254
		Serializable,
		// Token: 0x040000FF RID: 255
		SqlGeometry,
		// Token: 0x04000100 RID: 256
		SqlGeography,
		// Token: 0x04000101 RID: 257
		DateTimeWithKind,
		// Token: 0x04000102 RID: 258
		DateTimeOffset,
		// Token: 0x04000103 RID: 259
		ByteArray,
		// Token: 0x04000104 RID: 260
		Guid,
		// Token: 0x04000105 RID: 261
		String,
		// Token: 0x04000106 RID: 262
		DateTime,
		// Token: 0x04000107 RID: 263
		TimeSpan,
		// Token: 0x04000108 RID: 264
		Char,
		// Token: 0x04000109 RID: 265
		Boolean,
		// Token: 0x0400010A RID: 266
		Int16,
		// Token: 0x0400010B RID: 267
		Int32,
		// Token: 0x0400010C RID: 268
		Int64,
		// Token: 0x0400010D RID: 269
		UInt16,
		// Token: 0x0400010E RID: 270
		UInt32,
		// Token: 0x0400010F RID: 271
		UInt64,
		// Token: 0x04000110 RID: 272
		Byte,
		// Token: 0x04000111 RID: 273
		SByte,
		// Token: 0x04000112 RID: 274
		Single,
		// Token: 0x04000113 RID: 275
		Double,
		// Token: 0x04000114 RID: 276
		Decimal
	}
}
