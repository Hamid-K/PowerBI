using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000172 RID: 370
	public static class DateTimeFactory
	{
		// Token: 0x0600099B RID: 2459 RVA: 0x0001379C File Offset: 0x0001199C
		public static IDateTimeProvider CreateDateTimeProvider(DateTime? baseTime = null)
		{
			return new DateTimeFactory.DateTimeProvider(new DateTime?(baseTime ?? DateTime.UtcNow));
		}

		// Token: 0x0200031F RID: 799
		[ImmutableObject(true)]
		public sealed class DateTimeProvider : IDateTimeProvider
		{
			// Token: 0x060019AA RID: 6570 RVA: 0x0002E254 File Offset: 0x0002C454
			public DateTimeProvider(DateTime? baseTime = null)
			{
				DateTime dateTime = baseTime ?? DateTime.UtcNow;
				this._baseTime = dateTime;
				this._kind = dateTime.Kind;
				this._minValue = new DateTime(DateTime.MinValue.Ticks, this._kind);
				this._maxValue = new DateTime(DateTime.MaxValue.Ticks, this._kind);
			}

			// Token: 0x060019AB RID: 6571 RVA: 0x0002E2D4 File Offset: 0x0002C4D4
			public static IDateTimeProvider CreateDateTimeProvider(DateTime? baseTime = null)
			{
				return new DateTimeFactory.DateTimeProvider(new DateTime?(baseTime ?? DateTime.UtcNow));
			}

			// Token: 0x1700054C RID: 1356
			// (get) Token: 0x060019AC RID: 6572 RVA: 0x0002E304 File Offset: 0x0002C504
			public DateTime MinValue
			{
				get
				{
					return this._minValue;
				}
			}

			// Token: 0x1700054D RID: 1357
			// (get) Token: 0x060019AD RID: 6573 RVA: 0x0002E30C File Offset: 0x0002C50C
			public DateTime MaxValue
			{
				get
				{
					return this._maxValue;
				}
			}

			// Token: 0x1700054E RID: 1358
			// (get) Token: 0x060019AE RID: 6574 RVA: 0x0002E314 File Offset: 0x0002C514
			public DateTime BaseTime
			{
				get
				{
					return this._baseTime;
				}
			}

			// Token: 0x060019AF RID: 6575 RVA: 0x0002E31C File Offset: 0x0002C51C
			public DateTime CreateDateTime(int year, int month, int day)
			{
				return new DateTime(year, month, day, 0, 0, 0, this._kind);
			}

			// Token: 0x060019B0 RID: 6576 RVA: 0x0002E32F File Offset: 0x0002C52F
			public DateTime CreateDateTime(int year, int month, int day, int hour, int minute, int second)
			{
				return new DateTime(year, month, day, hour, minute, second, this._kind);
			}

			// Token: 0x060019B1 RID: 6577 RVA: 0x0002E345 File Offset: 0x0002C545
			public DateTime CreateDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond)
			{
				return new DateTime(year, month, day, hour, minute, second, millisecond, this._kind);
			}

			// Token: 0x060019B2 RID: 6578 RVA: 0x0002E360 File Offset: 0x0002C560
			public bool TryCreateDateTime(int year, int month, int day, out DateTime date)
			{
				bool flag;
				try
				{
					date = this.CreateDateTime(year, month, day);
					flag = true;
				}
				catch (ArgumentOutOfRangeException)
				{
					date = this._minValue;
					flag = false;
				}
				return flag;
			}

			// Token: 0x060019B3 RID: 6579 RVA: 0x0002E3A4 File Offset: 0x0002C5A4
			public bool TryCreateDateTime(int year, int month, int day, int hour, int minute, int second, out DateTime date)
			{
				bool flag;
				try
				{
					date = this.CreateDateTime(year, month, day, hour, minute, second);
					flag = true;
				}
				catch (ArgumentOutOfRangeException)
				{
					date = this._minValue;
					flag = false;
				}
				return flag;
			}

			// Token: 0x060019B4 RID: 6580 RVA: 0x0002E3F0 File Offset: 0x0002C5F0
			public bool TryCreateDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, out DateTime date)
			{
				bool flag;
				try
				{
					date = this.CreateDateTime(year, month, day, hour, minute, second, millisecond);
					flag = true;
				}
				catch (ArgumentOutOfRangeException)
				{
					date = this._minValue;
					flag = false;
				}
				return flag;
			}

			// Token: 0x060019B5 RID: 6581 RVA: 0x0002E43C File Offset: 0x0002C63C
			public bool TryGetFirstDayOfYear(int year, out DateTime date)
			{
				return this.TryCreateDateTime(year, 1, 1, out date);
			}

			// Token: 0x060019B6 RID: 6582 RVA: 0x0002E448 File Offset: 0x0002C648
			public bool TryGetLastDayOfYear(int year, out DateTime date)
			{
				return this.TryCreateDateTime(year, 12, DateTime.DaysInMonth(year, 12), out date);
			}

			// Token: 0x060019B7 RID: 6583 RVA: 0x0002E45C File Offset: 0x0002C65C
			public bool TryAddTimeSpan(DateTime dateTime, TimeSpan timeSpan, out DateTime dateValue)
			{
				bool flag;
				try
				{
					dateValue = dateTime.Add(timeSpan);
					flag = true;
				}
				catch (ArgumentOutOfRangeException)
				{
					dateValue = this._minValue;
					flag = false;
				}
				return flag;
			}

			// Token: 0x060019B8 RID: 6584 RVA: 0x0002E4A0 File Offset: 0x0002C6A0
			public bool TryAddDays(DateTime dateTime, int days, out DateTime dateValue)
			{
				bool flag;
				try
				{
					dateValue = dateTime.AddDays((double)days);
					flag = true;
				}
				catch (ArgumentOutOfRangeException)
				{
					dateValue = this._minValue;
					flag = false;
				}
				return flag;
			}

			// Token: 0x060019B9 RID: 6585 RVA: 0x0002E4E4 File Offset: 0x0002C6E4
			public bool TryAddMonths(DateTime dateTime, int months, out DateTime dateValue)
			{
				bool flag;
				try
				{
					dateValue = dateTime.AddMonths(months);
					flag = true;
				}
				catch (ArgumentOutOfRangeException)
				{
					dateValue = this._minValue;
					flag = false;
				}
				return flag;
			}

			// Token: 0x060019BA RID: 6586 RVA: 0x0002E528 File Offset: 0x0002C728
			public bool TryAddYears(DateTime dateTime, int years, out DateTime dateValue)
			{
				bool flag;
				try
				{
					dateValue = dateTime.AddYears(years);
					flag = true;
				}
				catch (ArgumentOutOfRangeException)
				{
					dateValue = this._minValue;
					flag = false;
				}
				return flag;
			}

			// Token: 0x060019BB RID: 6587 RVA: 0x0002E56C File Offset: 0x0002C76C
			public bool TryAddSeconds(DateTime dateTime, int seconds, out DateTime dateValue)
			{
				bool flag;
				try
				{
					dateValue = dateTime.AddSeconds((double)seconds);
					flag = true;
				}
				catch (ArgumentOutOfRangeException)
				{
					dateValue = this._minValue;
					flag = false;
				}
				return flag;
			}

			// Token: 0x060019BC RID: 6588 RVA: 0x0002E5B0 File Offset: 0x0002C7B0
			public bool TryAddMinutes(DateTime dateTime, int minutes, out DateTime dateValue)
			{
				bool flag;
				try
				{
					dateValue = dateTime.AddMinutes((double)minutes);
					flag = true;
				}
				catch (ArgumentOutOfRangeException)
				{
					dateValue = this._minValue;
					flag = false;
				}
				return flag;
			}

			// Token: 0x060019BD RID: 6589 RVA: 0x0002E5F4 File Offset: 0x0002C7F4
			public bool TryAddHours(DateTime dateTime, int hours, out DateTime dateValue)
			{
				bool flag;
				try
				{
					dateValue = dateTime.AddHours((double)hours);
					flag = true;
				}
				catch (ArgumentOutOfRangeException)
				{
					dateValue = this._minValue;
					flag = false;
				}
				return flag;
			}

			// Token: 0x0400098D RID: 2445
			private readonly DateTime _minValue;

			// Token: 0x0400098E RID: 2446
			private readonly DateTime _maxValue;

			// Token: 0x0400098F RID: 2447
			private readonly DateTime _baseTime;

			// Token: 0x04000990 RID: 2448
			private readonly DateTimeKind _kind;
		}
	}
}
