using System;
using System.ComponentModel;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000025 RID: 37
	[EditorBrowsable(EditorBrowsableState.Never)]
	[CompatibilityRequirement("Preview")]
	public enum TimeUnit
	{
		// Token: 0x040000A8 RID: 168
		Unknown,
		// Token: 0x040000A9 RID: 169
		Year,
		// Token: 0x040000AA RID: 170
		Semester,
		// Token: 0x040000AB RID: 171
		SemesterOfYear,
		// Token: 0x040000AC RID: 172
		Quarter,
		// Token: 0x040000AD RID: 173
		QuarterOfYear,
		// Token: 0x040000AE RID: 174
		QuarterOfSemester,
		// Token: 0x040000AF RID: 175
		Month,
		// Token: 0x040000B0 RID: 176
		MonthOfYear,
		// Token: 0x040000B1 RID: 177
		MonthOfSemester,
		// Token: 0x040000B2 RID: 178
		MonthOfQuarter,
		// Token: 0x040000B3 RID: 179
		Week,
		// Token: 0x040000B4 RID: 180
		WeekOfYear,
		// Token: 0x040000B5 RID: 181
		WeekOfSemester,
		// Token: 0x040000B6 RID: 182
		WeekOfQuarter,
		// Token: 0x040000B7 RID: 183
		WeekOfMonth,
		// Token: 0x040000B8 RID: 184
		Date,
		// Token: 0x040000B9 RID: 185
		DayOfYear,
		// Token: 0x040000BA RID: 186
		DayOfSemester,
		// Token: 0x040000BB RID: 187
		DayOfQuarter,
		// Token: 0x040000BC RID: 188
		DayOfMonth,
		// Token: 0x040000BD RID: 189
		DayOfWeek
	}
}
