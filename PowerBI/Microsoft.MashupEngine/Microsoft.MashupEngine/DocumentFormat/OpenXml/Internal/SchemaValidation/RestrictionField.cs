using System;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x02003127 RID: 12583
	[Flags]
	internal enum RestrictionField : byte
	{
		// Token: 0x0400B511 RID: 46353
		None = 0,
		// Token: 0x0400B512 RID: 46354
		Length = 1,
		// Token: 0x0400B513 RID: 46355
		MinLength = 2,
		// Token: 0x0400B514 RID: 46356
		MaxLength = 4,
		// Token: 0x0400B515 RID: 46357
		MinInclusive = 8,
		// Token: 0x0400B516 RID: 46358
		MaxInclusive = 16,
		// Token: 0x0400B517 RID: 46359
		MinExclusive = 32,
		// Token: 0x0400B518 RID: 46360
		MaxExclusive = 64,
		// Token: 0x0400B519 RID: 46361
		Pattern = 128,
		// Token: 0x0400B51A RID: 46362
		MinMaxInclusive = 24,
		// Token: 0x0400B51B RID: 46363
		MinMaxExclusive = 96,
		// Token: 0x0400B51C RID: 46364
		MinMaxRestriction = 120,
		// Token: 0x0400B51D RID: 46365
		LengthRestriction = 7
	}
}
