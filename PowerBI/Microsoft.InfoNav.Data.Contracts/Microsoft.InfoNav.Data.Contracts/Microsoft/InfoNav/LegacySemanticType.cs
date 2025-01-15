using System;

namespace Microsoft.InfoNav
{
	// Token: 0x02000069 RID: 105
	[Flags]
	internal enum LegacySemanticType
	{
		// Token: 0x04000149 RID: 329
		None = 0,
		// Token: 0x0400014A RID: 330
		Number = 1,
		// Token: 0x0400014B RID: 331
		Integer = 3,
		// Token: 0x0400014C RID: 332
		DateTime = 4,
		// Token: 0x0400014D RID: 333
		Time = 8,
		// Token: 0x0400014E RID: 334
		Date = 20,
		// Token: 0x0400014F RID: 335
		Month = 35,
		// Token: 0x04000150 RID: 336
		Year = 67,
		// Token: 0x04000151 RID: 337
		YearAndMonth = 128,
		// Token: 0x04000152 RID: 338
		MonthAndDay = 256,
		// Token: 0x04000153 RID: 339
		Decade = 515,
		// Token: 0x04000154 RID: 340
		YearAndWeek = 1024,
		// Token: 0x04000155 RID: 341
		String = 2048,
		// Token: 0x04000156 RID: 342
		Boolean = 4096,
		// Token: 0x04000157 RID: 343
		Table = 8192,
		// Token: 0x04000158 RID: 344
		Range = 16384,
		// Token: 0x04000159 RID: 345
		YearToHour = 32768,
		// Token: 0x0400015A RID: 346
		YearToMinute = 65536,
		// Token: 0x0400015B RID: 347
		YearToSecond = 131072,
		// Token: 0x0400015C RID: 348
		HourToSecond = 262144,
		// Token: 0x0400015D RID: 349
		HourAndMinute = 524288,
		// Token: 0x0400015E RID: 350
		MonthToMinute = 1048576,
		// Token: 0x0400015F RID: 351
		MonthToSecond = 2097152,
		// Token: 0x04000160 RID: 352
		Null = 4194304,
		// Token: 0x04000161 RID: 353
		Pod = 8388608
	}
}
