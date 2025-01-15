using System;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000019 RID: 25
	internal sealed class DateTimeProvider : IDateTimeProvider
	{
		// Token: 0x060000DF RID: 223 RVA: 0x000055E0 File Offset: 0x000037E0
		internal DateTimeProvider(DateTime? specificBaseTime)
		{
			if (specificBaseTime != null)
			{
				this._baseTime = specificBaseTime.Value;
				return;
			}
			this._baseTime = DateTime.UtcNow;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x0000560A File Offset: 0x0000380A
		public DateTime MinValue
		{
			get
			{
				return DateTimeProvider.MinValueInstance;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00005611 File Offset: 0x00003811
		public DateTime MaxValue
		{
			get
			{
				return DateTimeProvider.MaxValueInstance;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00005618 File Offset: 0x00003818
		public DateTime BaseTime
		{
			get
			{
				return this._baseTime;
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00005620 File Offset: 0x00003820
		public DateTime CreateDateTime(int year, int month, int day)
		{
			return new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Utc);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000562E File Offset: 0x0000382E
		public DateTime CreateDateTime(int year, int month, int day, int hour, int minute, int second)
		{
			return new DateTime(year, month, day, hour, minute, second, DateTimeKind.Utc);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000563F File Offset: 0x0000383F
		public DateTime CreateDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond)
		{
			return new DateTime(year, month, day, hour, minute, second, millisecond, DateTimeKind.Utc);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00005654 File Offset: 0x00003854
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
				date = DateTimeProvider.MinValueInstance;
				flag = false;
			}
			return flag;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00005698 File Offset: 0x00003898
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
				date = DateTimeProvider.MinValueInstance;
				flag = false;
			}
			return flag;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000056E4 File Offset: 0x000038E4
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
				date = DateTimeProvider.MinValueInstance;
				flag = false;
			}
			return flag;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00005730 File Offset: 0x00003930
		public bool TryGetFirstDayOfYear(int year, out DateTime date)
		{
			return this.TryCreateDateTime(year, 1, 1, out date);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000573C File Offset: 0x0000393C
		public bool TryGetLastDayOfYear(int year, out DateTime date)
		{
			return this.TryCreateDateTime(year, 12, DateTime.DaysInMonth(year, 12), out date);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00005750 File Offset: 0x00003950
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
				dateValue = DateTimeProvider.MinValueInstance;
				flag = false;
			}
			return flag;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00005790 File Offset: 0x00003990
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
				dateValue = DateTimeProvider.MinValueInstance;
				flag = false;
			}
			return flag;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000057D4 File Offset: 0x000039D4
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
				dateValue = DateTimeProvider.MinValueInstance;
				flag = false;
			}
			return flag;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00005814 File Offset: 0x00003A14
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
				dateValue = DateTimeProvider.MinValueInstance;
				flag = false;
			}
			return flag;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00005854 File Offset: 0x00003A54
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
				dateValue = DateTimeProvider.MinValueInstance;
				flag = false;
			}
			return flag;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00005898 File Offset: 0x00003A98
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
				dateValue = DateTimeProvider.MinValueInstance;
				flag = false;
			}
			return flag;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000058DC File Offset: 0x00003ADC
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
				dateValue = DateTimeProvider.MinValueInstance;
				flag = false;
			}
			return flag;
		}

		// Token: 0x04000095 RID: 149
		private static readonly DateTime MinValueInstance = new DateTime(DateTime.MinValue.Ticks, DateTimeKind.Utc);

		// Token: 0x04000096 RID: 150
		private static readonly DateTime MaxValueInstance = new DateTime(DateTime.MaxValue.Ticks, DateTimeKind.Utc);

		// Token: 0x04000097 RID: 151
		private readonly DateTime _baseTime;
	}
}
