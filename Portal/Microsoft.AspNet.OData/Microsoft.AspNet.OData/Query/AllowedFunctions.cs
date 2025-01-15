using System;

namespace Microsoft.AspNet.OData.Query
{
	// Token: 0x020000CA RID: 202
	[Flags]
	public enum AllowedFunctions
	{
		// Token: 0x040001AA RID: 426
		None = 0,
		// Token: 0x040001AB RID: 427
		StartsWith = 1,
		// Token: 0x040001AC RID: 428
		EndsWith = 2,
		// Token: 0x040001AD RID: 429
		Contains = 4,
		// Token: 0x040001AE RID: 430
		Length = 8,
		// Token: 0x040001AF RID: 431
		IndexOf = 16,
		// Token: 0x040001B0 RID: 432
		Concat = 32,
		// Token: 0x040001B1 RID: 433
		Substring = 64,
		// Token: 0x040001B2 RID: 434
		ToLower = 128,
		// Token: 0x040001B3 RID: 435
		ToUpper = 256,
		// Token: 0x040001B4 RID: 436
		Trim = 512,
		// Token: 0x040001B5 RID: 437
		Cast = 1024,
		// Token: 0x040001B6 RID: 438
		Year = 2048,
		// Token: 0x040001B7 RID: 439
		Date = 4096,
		// Token: 0x040001B8 RID: 440
		Month = 8192,
		// Token: 0x040001B9 RID: 441
		Time = 16384,
		// Token: 0x040001BA RID: 442
		Day = 32768,
		// Token: 0x040001BB RID: 443
		Hour = 131072,
		// Token: 0x040001BC RID: 444
		Minute = 524288,
		// Token: 0x040001BD RID: 445
		Second = 2097152,
		// Token: 0x040001BE RID: 446
		FractionalSeconds = 4194304,
		// Token: 0x040001BF RID: 447
		Round = 8388608,
		// Token: 0x040001C0 RID: 448
		Floor = 16777216,
		// Token: 0x040001C1 RID: 449
		Ceiling = 33554432,
		// Token: 0x040001C2 RID: 450
		IsOf = 67108864,
		// Token: 0x040001C3 RID: 451
		Any = 134217728,
		// Token: 0x040001C4 RID: 452
		All = 268435456,
		// Token: 0x040001C5 RID: 453
		AllStringFunctions = 1023,
		// Token: 0x040001C6 RID: 454
		AllDateTimeFunctions = 7010304,
		// Token: 0x040001C7 RID: 455
		AllMathFunctions = 58720256,
		// Token: 0x040001C8 RID: 456
		AllFunctions = 535494655
	}
}
