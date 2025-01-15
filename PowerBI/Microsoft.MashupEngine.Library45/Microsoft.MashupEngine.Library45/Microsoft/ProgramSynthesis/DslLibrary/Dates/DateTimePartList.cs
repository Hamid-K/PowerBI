using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.DslLibrary.Dates
{
	// Token: 0x0200084A RID: 2122
	public static class DateTimePartList
	{
		// Token: 0x0400164C RID: 5708
		public static readonly IReadOnlyList<DateTimePart> StandardDateTimeDescending = new DateTimePart[]
		{
			DateTimePart.Year,
			DateTimePart.Month,
			DateTimePart.Day,
			DateTimePart.Hour,
			DateTimePart.Minute,
			DateTimePart.Second,
			DateTimePart.Millisecond
		};

		// Token: 0x0400164D RID: 5709
		public static readonly IReadOnlyList<DateTimePart> StandardDateTimeAscending = DateTimePartList.StandardDateTimeDescending.Reverse<DateTimePart>().ToArray<DateTimePart>();

		// Token: 0x0400164E RID: 5710
		public static readonly IReadOnlyList<DateTimePart> StandardTimeDescending = new DateTimePart[]
		{
			DateTimePart.Hour,
			DateTimePart.Minute,
			DateTimePart.Second,
			DateTimePart.Millisecond
		};

		// Token: 0x0400164F RID: 5711
		public static readonly IReadOnlyList<DateTimePart> AllDateTime = (DateTimePart[])Enum.GetValues(typeof(DateTimePart));
	}
}
