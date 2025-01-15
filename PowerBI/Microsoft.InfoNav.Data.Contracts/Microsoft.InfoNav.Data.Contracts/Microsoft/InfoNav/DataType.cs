using System;

namespace Microsoft.InfoNav
{
	// Token: 0x02000060 RID: 96
	[Flags]
	public enum DataType
	{
		// Token: 0x04000116 RID: 278
		None = 0,
		// Token: 0x04000117 RID: 279
		Text = 1,
		// Token: 0x04000118 RID: 280
		Boolean = 2,
		// Token: 0x04000119 RID: 281
		Scalar = 4,
		// Token: 0x0400011A RID: 282
		Number = 12,
		// Token: 0x0400011B RID: 283
		Integer = 28,
		// Token: 0x0400011C RID: 284
		Time = 36,
		// Token: 0x0400011D RID: 285
		DateSpan = 100,
		// Token: 0x0400011E RID: 286
		Decade = 228,
		// Token: 0x0400011F RID: 287
		Year = 356,
		// Token: 0x04000120 RID: 288
		YearAndMonth = 612,
		// Token: 0x04000121 RID: 289
		YearAndWeek = 1124,
		// Token: 0x04000122 RID: 290
		SingleDate = 2148,
		// Token: 0x04000123 RID: 291
		Date = 6244,
		// Token: 0x04000124 RID: 292
		YearToHour = 10340,
		// Token: 0x04000125 RID: 293
		YearToMinute = 18532,
		// Token: 0x04000126 RID: 294
		YearToSecond = 34916,
		// Token: 0x04000127 RID: 295
		DateTime = 67684,
		// Token: 0x04000128 RID: 296
		PartialDate = 131108,
		// Token: 0x04000129 RID: 297
		Month = 393252,
		// Token: 0x0400012A RID: 298
		DayOfMonth = 131127,
		// Token: 0x0400012B RID: 299
		MonthAndDay = 1179684,
		// Token: 0x0400012C RID: 300
		TimeOfDay = 2228260,
		// Token: 0x0400012D RID: 301
		HourAndMinute = 6422564,
		// Token: 0x0400012E RID: 302
		HourToSecond = 10616868,
		// Token: 0x0400012F RID: 303
		MonthToMinute = 16908324,
		// Token: 0x04000130 RID: 304
		MonthToSecond = 33685540,
		// Token: 0x04000131 RID: 305
		Null = 67108864,
		// Token: 0x04000132 RID: 306
		Table = 134217728,
		// Token: 0x04000133 RID: 307
		Range = 268435456,
		// Token: 0x04000134 RID: 308
		Pod = 536870912
	}
}
