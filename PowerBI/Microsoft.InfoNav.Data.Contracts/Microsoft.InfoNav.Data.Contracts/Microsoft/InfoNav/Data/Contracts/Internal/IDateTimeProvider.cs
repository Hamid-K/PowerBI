using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001F1 RID: 497
	public interface IDateTimeProvider
	{
		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000D7A RID: 3450
		DateTime MinValue { get; }

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000D7B RID: 3451
		DateTime MaxValue { get; }

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000D7C RID: 3452
		DateTime BaseTime { get; }

		// Token: 0x06000D7D RID: 3453
		DateTime CreateDateTime(int year, int month, int day);

		// Token: 0x06000D7E RID: 3454
		DateTime CreateDateTime(int year, int month, int day, int hour, int minute, int second);

		// Token: 0x06000D7F RID: 3455
		DateTime CreateDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond);

		// Token: 0x06000D80 RID: 3456
		bool TryCreateDateTime(int year, int month, int day, out DateTime date);

		// Token: 0x06000D81 RID: 3457
		bool TryCreateDateTime(int year, int month, int day, int hour, int minute, int second, out DateTime date);

		// Token: 0x06000D82 RID: 3458
		bool TryCreateDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, out DateTime date);

		// Token: 0x06000D83 RID: 3459
		bool TryGetFirstDayOfYear(int yearValue, out DateTime date);

		// Token: 0x06000D84 RID: 3460
		bool TryGetLastDayOfYear(int yearValue, out DateTime date);

		// Token: 0x06000D85 RID: 3461
		bool TryAddTimeSpan(DateTime dateTime, TimeSpan timeSpan, out DateTime dateValue);

		// Token: 0x06000D86 RID: 3462
		bool TryAddDays(DateTime dateTime, int days, out DateTime dateValue);

		// Token: 0x06000D87 RID: 3463
		bool TryAddMonths(DateTime dateTime, int months, out DateTime dateValue);

		// Token: 0x06000D88 RID: 3464
		bool TryAddYears(DateTime dateTime, int years, out DateTime dateValue);

		// Token: 0x06000D89 RID: 3465
		bool TryAddSeconds(DateTime dateTime, int seconds, out DateTime dateValue);

		// Token: 0x06000D8A RID: 3466
		bool TryAddMinutes(DateTime dateTime, int minutes, out DateTime dateValue);

		// Token: 0x06000D8B RID: 3467
		bool TryAddHours(DateTime dateTime, int hours, out DateTime dateValue);
	}
}
