using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001355 RID: 4949
	internal class FastIsoDateTimeParser
	{
		// Token: 0x06008226 RID: 33318 RVA: 0x001BA1DC File Offset: 0x001B83DC
		public static bool TryParseDate(string s, out DateTime dateTime)
		{
			FastIsoDateTimeParser.Date date;
			if (FastIsoDateTimeParser.TryParseDate(s, 0, s.Length, out date))
			{
				try
				{
					dateTime = new DateTime(date.year, date.month, date.day);
					return true;
				}
				catch (ArgumentException)
				{
				}
			}
			dateTime = default(DateTime);
			return false;
		}

		// Token: 0x06008227 RID: 33319 RVA: 0x001BA238 File Offset: 0x001B8438
		public static bool TryParseTime(string s, out DateTime dateTime)
		{
			FastIsoDateTimeParser.Time time;
			if (FastIsoDateTimeParser.TryParseTime(s, 0, s.Length, out time))
			{
				try
				{
					dateTime = new DateTime(1, 1, 1, time.hour, time.minute, time.second, 0) + new TimeSpan((long)time.ticks);
					return true;
				}
				catch (ArgumentException)
				{
				}
				catch (OverflowException)
				{
				}
			}
			dateTime = default(DateTime);
			return false;
		}

		// Token: 0x06008228 RID: 33320 RVA: 0x001BA2B8 File Offset: 0x001B84B8
		public static bool TryParseDateTime(string s, out DateTime dateTime)
		{
			FastIsoDateTimeParser.Date date;
			FastIsoDateTimeParser.Time time;
			if (FastIsoDateTimeParser.TryParseDateTime(s, 0, s.Length, out date, out time))
			{
				try
				{
					dateTime = new DateTime(date.year, date.month, date.day, time.hour, time.minute, time.second, 0) + new TimeSpan((long)time.ticks);
					return true;
				}
				catch (ArgumentException)
				{
				}
				catch (OverflowException)
				{
				}
			}
			dateTime = default(DateTime);
			return false;
		}

		// Token: 0x06008229 RID: 33321 RVA: 0x001BA34C File Offset: 0x001B854C
		public static bool TryParseDateTimeOffset(string s, out DateTimeOffset dateTimeOffset)
		{
			FastIsoDateTimeParser.Date date;
			FastIsoDateTimeParser.Time time;
			FastIsoDateTimeParser.TimeOffset timeOffset;
			if (FastIsoDateTimeParser.TryParseDateTimeOffset(s, 0, s.Length, out date, out time, out timeOffset))
			{
				try
				{
					dateTimeOffset = new DateTimeOffset(date.year, date.month, date.day, time.hour, time.minute, time.second, 0, new TimeSpan(timeOffset.hour, timeOffset.minute, 0)) + new TimeSpan((long)time.ticks);
					return true;
				}
				catch (ArgumentException)
				{
				}
				catch (OverflowException)
				{
				}
			}
			dateTimeOffset = default(DateTimeOffset);
			return false;
		}

		// Token: 0x0600822A RID: 33322 RVA: 0x001BA3F4 File Offset: 0x001B85F4
		private static bool TryParseDate(string s, int offset, int length, out FastIsoDateTimeParser.Date date)
		{
			if (length == 10 && s[offset + 4] == '-' && s[offset + 7] == '-')
			{
				date.year = SmallDecimalParser.ParseDigits(s, offset, 4);
				date.month = SmallDecimalParser.ParseDigits(s, offset + 5, 2);
				date.day = SmallDecimalParser.ParseDigits(s, offset + 8, 2);
			}
			else if (length == 8)
			{
				date.year = SmallDecimalParser.ParseDigits(s, offset, 4);
				date.month = SmallDecimalParser.ParseDigits(s, offset + 4, 2);
				date.day = SmallDecimalParser.ParseDigits(s, offset + 6, 2);
			}
			else if (length == 7 && s[offset + 4] == '-')
			{
				date.year = SmallDecimalParser.ParseDigits(s, offset, 4);
				date.month = SmallDecimalParser.ParseDigits(s, offset + 5, 2);
				date.day = 1;
			}
			else if (length == 4)
			{
				date.year = SmallDecimalParser.ParseDigits(s, offset, 4);
				date.month = 1;
				date.day = 1;
			}
			else
			{
				date.year = -1;
				date.month = -1;
				date.day = -1;
			}
			return (date.year | date.month | date.day) >= 0;
		}

		// Token: 0x0600822B RID: 33323 RVA: 0x001BA510 File Offset: 0x001B8710
		private static int ParseTicks(string s, int offset, int length)
		{
			int num = SmallDecimalParser.ParseDigits(s, offset, length);
			if (num == -1)
			{
				return -1;
			}
			return (int)((double)num * 10000000.0 / (double)SmallDecimalParser.Power10(length));
		}

		// Token: 0x0600822C RID: 33324 RVA: 0x001BA544 File Offset: 0x001B8744
		private static bool TryParseTime(string s, int offset, int length, out FastIsoDateTimeParser.Time time)
		{
			time.hour = 0;
			time.minute = 0;
			time.second = 0;
			time.ticks = 0;
			if (length >= 2)
			{
				time.hour = SmallDecimalParser.ParseDigits(s, offset, 2);
				offset += 2;
				length -= 2;
				if (length >= 3 && s[offset] == ':')
				{
					time.minute = SmallDecimalParser.ParseDigits(s, offset + 1, 2);
					offset += 3;
					length -= 3;
					if (length >= 3 && s[offset] == ':')
					{
						time.second = SmallDecimalParser.ParseDigits(s, offset + 1, 2);
						offset += 3;
						length -= 3;
						if (length >= 2 && length <= 8 && FastIsoDateTimeParser.IsDecimalPoint(s[offset]))
						{
							time.ticks = FastIsoDateTimeParser.ParseTicks(s, offset + 1, length - 1);
							length = 0;
						}
					}
				}
				else if (length >= 2)
				{
					time.minute = SmallDecimalParser.ParseDigits(s, offset, 2);
					offset += 2;
					length -= 2;
					if (length >= 2)
					{
						time.second = SmallDecimalParser.ParseDigits(s, offset, 2);
						offset += 2;
						length -= 2;
						if (length >= 2 && length <= 8 && FastIsoDateTimeParser.IsDecimalPoint(s[offset]))
						{
							time.ticks = FastIsoDateTimeParser.ParseTicks(s, offset + 1, length - 1);
							length = 0;
						}
					}
				}
				if (length == 0 && (time.hour | time.minute | time.second | time.ticks) >= 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600822D RID: 33325 RVA: 0x001BA6A4 File Offset: 0x001B88A4
		private static bool TryParseTimeOffset(string s, int offset, int length, out FastIsoDateTimeParser.TimeOffset timeOffset)
		{
			timeOffset.hour = 0;
			timeOffset.minute = 0;
			if (length == 1 && s[offset] == 'Z')
			{
				return true;
			}
			bool flag = false;
			if (length >= 3)
			{
				char c = s[offset];
				if (FastIsoDateTimeParser.IsSign(c))
				{
					flag = c == '-';
					timeOffset.hour = SmallDecimalParser.ParseDigits(s, offset + 1, 2);
					offset += 3;
					length -= 3;
					if (length >= 3 && s[offset] == ':')
					{
						timeOffset.minute = SmallDecimalParser.ParseDigits(s, offset + 1, 2);
						offset += 3;
						length -= 3;
					}
					else if (length >= 2)
					{
						timeOffset.minute = SmallDecimalParser.ParseDigits(s, offset, 2);
						offset += 2;
						length -= 2;
					}
				}
			}
			if (length == 0 && (timeOffset.hour | timeOffset.minute) >= 0)
			{
				if (flag)
				{
					timeOffset.hour = -timeOffset.hour;
					timeOffset.minute = -timeOffset.minute;
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600822E RID: 33326 RVA: 0x001BA780 File Offset: 0x001B8980
		private static bool TryParseDateTime(string s, int offset, int length, out FastIsoDateTimeParser.Date date, out FastIsoDateTimeParser.Time time)
		{
			if (length >= 11 && s[offset + 10] == 'T')
			{
				if (FastIsoDateTimeParser.TryParseDate(s, offset, 10, out date) && FastIsoDateTimeParser.TryParseTime(s, offset + 11, length - 11, out time))
				{
					return true;
				}
			}
			else if (length >= 9 && s[offset + 8] == 'T' && FastIsoDateTimeParser.TryParseDate(s, offset, 8, out date) && FastIsoDateTimeParser.TryParseTime(s, offset + 9, length - 9, out time))
			{
				return true;
			}
			date = default(FastIsoDateTimeParser.Date);
			time = default(FastIsoDateTimeParser.Time);
			return false;
		}

		// Token: 0x0600822F RID: 33327 RVA: 0x001BA804 File Offset: 0x001B8A04
		private static bool TryParseDateTimeOffset(string s, int offset, int length, out FastIsoDateTimeParser.Date date, out FastIsoDateTimeParser.Time time, out FastIsoDateTimeParser.TimeOffset timeOffset)
		{
			int num = 1;
			if (length >= 3 && FastIsoDateTimeParser.IsSign(s[length - 3]))
			{
				num = 3;
			}
			else if (length >= 5 && FastIsoDateTimeParser.IsSign(s[length - 5]))
			{
				num = 5;
			}
			else if (length >= 6 && FastIsoDateTimeParser.IsSign(s[length - 6]))
			{
				num = 6;
			}
			if (FastIsoDateTimeParser.TryParseDateTime(s, offset, length - num, out date, out time) && FastIsoDateTimeParser.TryParseTimeOffset(s, offset + length - num, num, out timeOffset))
			{
				return true;
			}
			timeOffset = default(FastIsoDateTimeParser.TimeOffset);
			return false;
		}

		// Token: 0x06008230 RID: 33328 RVA: 0x001BA883 File Offset: 0x001B8A83
		private static bool IsSign(char ch)
		{
			return ch == '+' || ch == '-';
		}

		// Token: 0x06008231 RID: 33329 RVA: 0x001BA891 File Offset: 0x001B8A91
		private static bool IsDecimalPoint(char ch)
		{
			return ch == '.' || ch == ',';
		}

		// Token: 0x02001356 RID: 4950
		private struct Date
		{
			// Token: 0x040046DB RID: 18139
			public int year;

			// Token: 0x040046DC RID: 18140
			public int month;

			// Token: 0x040046DD RID: 18141
			public int day;
		}

		// Token: 0x02001357 RID: 4951
		private struct Time
		{
			// Token: 0x040046DE RID: 18142
			public int hour;

			// Token: 0x040046DF RID: 18143
			public int minute;

			// Token: 0x040046E0 RID: 18144
			public int second;

			// Token: 0x040046E1 RID: 18145
			public int ticks;
		}

		// Token: 0x02001358 RID: 4952
		private struct TimeOffset
		{
			// Token: 0x040046E2 RID: 18146
			public int hour;

			// Token: 0x040046E3 RID: 18147
			public int minute;
		}
	}
}
