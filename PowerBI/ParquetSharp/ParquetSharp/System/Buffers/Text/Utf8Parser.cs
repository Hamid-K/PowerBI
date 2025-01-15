﻿using System;
using System.Runtime.CompilerServices;

namespace System.Buffers.Text
{
	// Token: 0x020000ED RID: 237
	internal static class Utf8Parser
	{
		// Token: 0x06000872 RID: 2162 RVA: 0x00025D08 File Offset: 0x00023F08
		public unsafe static bool TryParse(ReadOnlySpan<byte> source, out bool value, out int bytesConsumed, char standardFormat = '\0')
		{
			if (standardFormat != '\0' && standardFormat != 'G' && standardFormat != 'l')
			{
				return ThrowHelper.TryParseThrowFormatException<bool>(out value, out bytesConsumed);
			}
			if (source.Length >= 4)
			{
				if ((*source[0] == 84 || *source[0] == 116) && (*source[1] == 82 || *source[1] == 114) && (*source[2] == 85 || *source[2] == 117) && (*source[3] == 69 || *source[3] == 101))
				{
					bytesConsumed = 4;
					value = true;
					return true;
				}
				if (source.Length >= 5 && (*source[0] == 70 || *source[0] == 102) && (*source[1] == 65 || *source[1] == 97) && (*source[2] == 76 || *source[2] == 108) && (*source[3] == 83 || *source[3] == 115) && (*source[4] == 69 || *source[4] == 101))
				{
					bytesConsumed = 5;
					value = false;
					return true;
				}
			}
			bytesConsumed = 0;
			value = false;
			return false;
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00025E88 File Offset: 0x00024088
		public static bool TryParse(ReadOnlySpan<byte> source, out DateTime value, out int bytesConsumed, char standardFormat = '\0')
		{
			if (standardFormat <= 'G')
			{
				if (standardFormat == '\0' || standardFormat == 'G')
				{
					DateTimeOffset dateTimeOffset;
					return Utf8Parser.TryParseDateTimeG(source, out value, out dateTimeOffset, out bytesConsumed);
				}
			}
			else if (standardFormat != 'O')
			{
				if (standardFormat != 'R')
				{
					if (standardFormat == 'l')
					{
						DateTimeOffset dateTimeOffset2;
						if (!Utf8Parser.TryParseDateTimeOffsetR(source, 32U, out dateTimeOffset2, out bytesConsumed))
						{
							value = default(DateTime);
							return false;
						}
						value = dateTimeOffset2.DateTime;
						return true;
					}
				}
				else
				{
					DateTimeOffset dateTimeOffset3;
					if (!Utf8Parser.TryParseDateTimeOffsetR(source, 0U, out dateTimeOffset3, out bytesConsumed))
					{
						value = default(DateTime);
						return false;
					}
					value = dateTimeOffset3.DateTime;
					return true;
				}
			}
			else
			{
				DateTimeOffset dateTimeOffset4;
				DateTimeKind dateTimeKind;
				if (!Utf8Parser.TryParseDateTimeOffsetO(source, out dateTimeOffset4, out bytesConsumed, out dateTimeKind))
				{
					value = default(DateTime);
					bytesConsumed = 0;
					return false;
				}
				if (dateTimeKind != DateTimeKind.Utc)
				{
					if (dateTimeKind == DateTimeKind.Local)
					{
						value = dateTimeOffset4.LocalDateTime;
					}
					else
					{
						value = dateTimeOffset4.DateTime;
					}
				}
				else
				{
					value = dateTimeOffset4.UtcDateTime;
				}
				return true;
			}
			return ThrowHelper.TryParseThrowFormatException<DateTime>(out value, out bytesConsumed);
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x00025F90 File Offset: 0x00024190
		public static bool TryParse(ReadOnlySpan<byte> source, out DateTimeOffset value, out int bytesConsumed, char standardFormat = '\0')
		{
			if (standardFormat <= 'G')
			{
				if (standardFormat == '\0')
				{
					return Utf8Parser.TryParseDateTimeOffsetDefault(source, out value, out bytesConsumed);
				}
				if (standardFormat == 'G')
				{
					DateTime dateTime;
					return Utf8Parser.TryParseDateTimeG(source, out dateTime, out value, out bytesConsumed);
				}
			}
			else
			{
				if (standardFormat == 'O')
				{
					DateTimeKind dateTimeKind;
					return Utf8Parser.TryParseDateTimeOffsetO(source, out value, out bytesConsumed, out dateTimeKind);
				}
				if (standardFormat == 'R')
				{
					return Utf8Parser.TryParseDateTimeOffsetR(source, 0U, out value, out bytesConsumed);
				}
				if (standardFormat == 'l')
				{
					return Utf8Parser.TryParseDateTimeOffsetR(source, 32U, out value, out bytesConsumed);
				}
			}
			return ThrowHelper.TryParseThrowFormatException<DateTimeOffset>(out value, out bytesConsumed);
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x00026014 File Offset: 0x00024214
		private unsafe static bool TryParseDateTimeOffsetDefault(ReadOnlySpan<byte> source, out DateTimeOffset value, out int bytesConsumed)
		{
			if (source.Length < 26)
			{
				bytesConsumed = 0;
				value = default(DateTimeOffset);
				return false;
			}
			DateTime dateTime;
			DateTimeOffset dateTimeOffset;
			int num;
			if (!Utf8Parser.TryParseDateTimeG(source, out dateTime, out dateTimeOffset, out num))
			{
				bytesConsumed = 0;
				value = default(DateTimeOffset);
				return false;
			}
			if (*source[19] != 32)
			{
				bytesConsumed = 0;
				value = default(DateTimeOffset);
				return false;
			}
			byte b = *source[20];
			if (b != 43 && b != 45)
			{
				bytesConsumed = 0;
				value = default(DateTimeOffset);
				return false;
			}
			uint num2 = (uint)(*source[21] - 48);
			uint num3 = (uint)(*source[22] - 48);
			if (num2 > 9U || num3 > 9U)
			{
				bytesConsumed = 0;
				value = default(DateTimeOffset);
				return false;
			}
			int num4 = (int)(num2 * 10U + num3);
			if (*source[23] != 58)
			{
				bytesConsumed = 0;
				value = default(DateTimeOffset);
				return false;
			}
			uint num5 = (uint)(*source[24] - 48);
			uint num6 = (uint)(*source[25] - 48);
			if (num5 > 9U || num6 > 9U)
			{
				bytesConsumed = 0;
				value = default(DateTimeOffset);
				return false;
			}
			int num7 = (int)(num5 * 10U + num6);
			TimeSpan timeSpan = new TimeSpan(num4, num7, 0);
			if (b == 45)
			{
				timeSpan = -timeSpan;
			}
			if (!Utf8Parser.TryCreateDateTimeOffset(dateTime, b == 45, num4, num7, out value))
			{
				bytesConsumed = 0;
				value = default(DateTimeOffset);
				return false;
			}
			bytesConsumed = 26;
			return true;
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x0002618C File Offset: 0x0002438C
		private unsafe static bool TryParseDateTimeG(ReadOnlySpan<byte> source, out DateTime value, out DateTimeOffset valueAsOffset, out int bytesConsumed)
		{
			if (source.Length < 19)
			{
				bytesConsumed = 0;
				value = default(DateTime);
				valueAsOffset = default(DateTimeOffset);
				return false;
			}
			uint num = (uint)(*source[0] - 48);
			uint num2 = (uint)(*source[1] - 48);
			if (num > 9U || num2 > 9U)
			{
				bytesConsumed = 0;
				value = default(DateTime);
				valueAsOffset = default(DateTimeOffset);
				return false;
			}
			int num3 = (int)(num * 10U + num2);
			if (*source[2] != 47)
			{
				bytesConsumed = 0;
				value = default(DateTime);
				valueAsOffset = default(DateTimeOffset);
				return false;
			}
			uint num4 = (uint)(*source[3] - 48);
			uint num5 = (uint)(*source[4] - 48);
			if (num4 > 9U || num5 > 9U)
			{
				bytesConsumed = 0;
				value = default(DateTime);
				valueAsOffset = default(DateTimeOffset);
				return false;
			}
			int num6 = (int)(num4 * 10U + num5);
			if (*source[5] != 47)
			{
				bytesConsumed = 0;
				value = default(DateTime);
				valueAsOffset = default(DateTimeOffset);
				return false;
			}
			uint num7 = (uint)(*source[6] - 48);
			uint num8 = (uint)(*source[7] - 48);
			uint num9 = (uint)(*source[8] - 48);
			uint num10 = (uint)(*source[9] - 48);
			if (num7 > 9U || num8 > 9U || num9 > 9U || num10 > 9U)
			{
				bytesConsumed = 0;
				value = default(DateTime);
				valueAsOffset = default(DateTimeOffset);
				return false;
			}
			int num11 = (int)(num7 * 1000U + num8 * 100U + num9 * 10U + num10);
			if (*source[10] != 32)
			{
				bytesConsumed = 0;
				value = default(DateTime);
				valueAsOffset = default(DateTimeOffset);
				return false;
			}
			uint num12 = (uint)(*source[11] - 48);
			uint num13 = (uint)(*source[12] - 48);
			if (num12 > 9U || num13 > 9U)
			{
				bytesConsumed = 0;
				value = default(DateTime);
				valueAsOffset = default(DateTimeOffset);
				return false;
			}
			int num14 = (int)(num12 * 10U + num13);
			if (*source[13] != 58)
			{
				bytesConsumed = 0;
				value = default(DateTime);
				valueAsOffset = default(DateTimeOffset);
				return false;
			}
			uint num15 = (uint)(*source[14] - 48);
			uint num16 = (uint)(*source[15] - 48);
			if (num15 > 9U || num16 > 9U)
			{
				bytesConsumed = 0;
				value = default(DateTime);
				valueAsOffset = default(DateTimeOffset);
				return false;
			}
			int num17 = (int)(num15 * 10U + num16);
			if (*source[16] != 58)
			{
				bytesConsumed = 0;
				value = default(DateTime);
				valueAsOffset = default(DateTimeOffset);
				return false;
			}
			uint num18 = (uint)(*source[17] - 48);
			uint num19 = (uint)(*source[18] - 48);
			if (num18 > 9U || num19 > 9U)
			{
				bytesConsumed = 0;
				value = default(DateTime);
				valueAsOffset = default(DateTimeOffset);
				return false;
			}
			int num20 = (int)(num18 * 10U + num19);
			if (!Utf8Parser.TryCreateDateTimeOffsetInterpretingDataAsLocalTime(num11, num3, num6, num14, num17, num20, 0, out valueAsOffset))
			{
				bytesConsumed = 0;
				value = default(DateTime);
				valueAsOffset = default(DateTimeOffset);
				return false;
			}
			bytesConsumed = 19;
			value = valueAsOffset.DateTime;
			return true;
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x000264A8 File Offset: 0x000246A8
		private static bool TryCreateDateTimeOffset(DateTime dateTime, bool offsetNegative, int offsetHours, int offsetMinutes, out DateTimeOffset value)
		{
			if (offsetHours > 14)
			{
				value = default(DateTimeOffset);
				return false;
			}
			if (offsetMinutes > 59)
			{
				value = default(DateTimeOffset);
				return false;
			}
			if (offsetHours == 14 && offsetMinutes != 0)
			{
				value = default(DateTimeOffset);
				return false;
			}
			long num = ((long)offsetHours * 3600L + (long)offsetMinutes * 60L) * 10000000L;
			if (offsetNegative)
			{
				num = -num;
			}
			try
			{
				value = new DateTimeOffset(dateTime.Ticks, new TimeSpan(num));
			}
			catch (ArgumentOutOfRangeException)
			{
				value = default(DateTimeOffset);
				return false;
			}
			return true;
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x00026554 File Offset: 0x00024754
		private static bool TryCreateDateTimeOffset(int year, int month, int day, int hour, int minute, int second, int fraction, bool offsetNegative, int offsetHours, int offsetMinutes, out DateTimeOffset value)
		{
			DateTime dateTime;
			if (!Utf8Parser.TryCreateDateTime(year, month, day, hour, minute, second, fraction, DateTimeKind.Unspecified, out dateTime))
			{
				value = default(DateTimeOffset);
				return false;
			}
			if (!Utf8Parser.TryCreateDateTimeOffset(dateTime, offsetNegative, offsetHours, offsetMinutes, out value))
			{
				value = default(DateTimeOffset);
				return false;
			}
			return true;
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x000265A4 File Offset: 0x000247A4
		private static bool TryCreateDateTimeOffsetInterpretingDataAsLocalTime(int year, int month, int day, int hour, int minute, int second, int fraction, out DateTimeOffset value)
		{
			DateTime dateTime;
			if (!Utf8Parser.TryCreateDateTime(year, month, day, hour, minute, second, fraction, DateTimeKind.Local, out dateTime))
			{
				value = default(DateTimeOffset);
				return false;
			}
			try
			{
				value = new DateTimeOffset(dateTime);
			}
			catch (ArgumentOutOfRangeException)
			{
				value = default(DateTimeOffset);
				return false;
			}
			return true;
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x00026608 File Offset: 0x00024808
		private static bool TryCreateDateTime(int year, int month, int day, int hour, int minute, int second, int fraction, DateTimeKind kind, out DateTime value)
		{
			if (year == 0)
			{
				value = default(DateTime);
				return false;
			}
			if (month - 1 >= 12)
			{
				value = default(DateTime);
				return false;
			}
			uint num = (uint)(day - 1);
			if (num >= 28U && (ulong)num >= (ulong)((long)DateTime.DaysInMonth(year, month)))
			{
				value = default(DateTime);
				return false;
			}
			if (hour > 23)
			{
				value = default(DateTime);
				return false;
			}
			if (minute > 59)
			{
				value = default(DateTime);
				return false;
			}
			if (second > 59)
			{
				value = default(DateTime);
				return false;
			}
			int[] array = (DateTime.IsLeapYear(year) ? Utf8Parser.s_daysToMonth366 : Utf8Parser.s_daysToMonth365);
			int num2 = year - 1;
			int num3 = num2 * 365 + num2 / 4 - num2 / 100 + num2 / 400 + array[month - 1] + day - 1;
			long num4 = (long)num3 * 864000000000L;
			int num5 = hour * 3600 + minute * 60 + second;
			num4 += (long)num5 * 10000000L;
			num4 += (long)fraction;
			value = new DateTime(num4, kind);
			return true;
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x00026724 File Offset: 0x00024924
		private unsafe static bool TryParseDateTimeOffsetO(ReadOnlySpan<byte> source, out DateTimeOffset value, out int bytesConsumed, out DateTimeKind kind)
		{
			if (source.Length < 27)
			{
				value = default(DateTimeOffset);
				bytesConsumed = 0;
				kind = DateTimeKind.Unspecified;
				return false;
			}
			uint num = (uint)(*source[0] - 48);
			uint num2 = (uint)(*source[1] - 48);
			uint num3 = (uint)(*source[2] - 48);
			uint num4 = (uint)(*source[3] - 48);
			if (num > 9U || num2 > 9U || num3 > 9U || num4 > 9U)
			{
				value = default(DateTimeOffset);
				bytesConsumed = 0;
				kind = DateTimeKind.Unspecified;
				return false;
			}
			int num5 = (int)(num * 1000U + num2 * 100U + num3 * 10U + num4);
			if (*source[4] != 45)
			{
				value = default(DateTimeOffset);
				bytesConsumed = 0;
				kind = DateTimeKind.Unspecified;
				return false;
			}
			uint num6 = (uint)(*source[5] - 48);
			uint num7 = (uint)(*source[6] - 48);
			if (num6 > 9U || num7 > 9U)
			{
				value = default(DateTimeOffset);
				bytesConsumed = 0;
				kind = DateTimeKind.Unspecified;
				return false;
			}
			int num8 = (int)(num6 * 10U + num7);
			if (*source[7] != 45)
			{
				value = default(DateTimeOffset);
				bytesConsumed = 0;
				kind = DateTimeKind.Unspecified;
				return false;
			}
			uint num9 = (uint)(*source[8] - 48);
			uint num10 = (uint)(*source[9] - 48);
			if (num9 > 9U || num10 > 9U)
			{
				value = default(DateTimeOffset);
				bytesConsumed = 0;
				kind = DateTimeKind.Unspecified;
				return false;
			}
			int num11 = (int)(num9 * 10U + num10);
			if (*source[10] != 84)
			{
				value = default(DateTimeOffset);
				bytesConsumed = 0;
				kind = DateTimeKind.Unspecified;
				return false;
			}
			uint num12 = (uint)(*source[11] - 48);
			uint num13 = (uint)(*source[12] - 48);
			if (num12 > 9U || num13 > 9U)
			{
				value = default(DateTimeOffset);
				bytesConsumed = 0;
				kind = DateTimeKind.Unspecified;
				return false;
			}
			int num14 = (int)(num12 * 10U + num13);
			if (*source[13] != 58)
			{
				value = default(DateTimeOffset);
				bytesConsumed = 0;
				kind = DateTimeKind.Unspecified;
				return false;
			}
			uint num15 = (uint)(*source[14] - 48);
			uint num16 = (uint)(*source[15] - 48);
			if (num15 > 9U || num16 > 9U)
			{
				value = default(DateTimeOffset);
				bytesConsumed = 0;
				kind = DateTimeKind.Unspecified;
				return false;
			}
			int num17 = (int)(num15 * 10U + num16);
			if (*source[16] != 58)
			{
				value = default(DateTimeOffset);
				bytesConsumed = 0;
				kind = DateTimeKind.Unspecified;
				return false;
			}
			uint num18 = (uint)(*source[17] - 48);
			uint num19 = (uint)(*source[18] - 48);
			if (num18 > 9U || num19 > 9U)
			{
				value = default(DateTimeOffset);
				bytesConsumed = 0;
				kind = DateTimeKind.Unspecified;
				return false;
			}
			int num20 = (int)(num18 * 10U + num19);
			if (*source[19] != 46)
			{
				value = default(DateTimeOffset);
				bytesConsumed = 0;
				kind = DateTimeKind.Unspecified;
				return false;
			}
			uint num21 = (uint)(*source[20] - 48);
			uint num22 = (uint)(*source[21] - 48);
			uint num23 = (uint)(*source[22] - 48);
			uint num24 = (uint)(*source[23] - 48);
			uint num25 = (uint)(*source[24] - 48);
			uint num26 = (uint)(*source[25] - 48);
			uint num27 = (uint)(*source[26] - 48);
			if (num21 > 9U || num22 > 9U || num23 > 9U || num24 > 9U || num25 > 9U || num26 > 9U || num27 > 9U)
			{
				value = default(DateTimeOffset);
				bytesConsumed = 0;
				kind = DateTimeKind.Unspecified;
				return false;
			}
			int num28 = (int)(num21 * 1000000U + num22 * 100000U + num23 * 10000U + num24 * 1000U + num25 * 100U + num26 * 10U + num27);
			byte b = ((source.Length <= 27) ? 0 : (*source[27]));
			if (b != 90 && b != 43 && b != 45)
			{
				if (!Utf8Parser.TryCreateDateTimeOffsetInterpretingDataAsLocalTime(num5, num8, num11, num14, num17, num20, num28, out value))
				{
					value = default(DateTimeOffset);
					bytesConsumed = 0;
					kind = DateTimeKind.Unspecified;
					return false;
				}
				bytesConsumed = 27;
				kind = DateTimeKind.Unspecified;
				return true;
			}
			else if (b == 90)
			{
				if (!Utf8Parser.TryCreateDateTimeOffset(num5, num8, num11, num14, num17, num20, num28, false, 0, 0, out value))
				{
					value = default(DateTimeOffset);
					bytesConsumed = 0;
					kind = DateTimeKind.Unspecified;
					return false;
				}
				bytesConsumed = 28;
				kind = DateTimeKind.Utc;
				return true;
			}
			else
			{
				if (source.Length < 33)
				{
					value = default(DateTimeOffset);
					bytesConsumed = 0;
					kind = DateTimeKind.Unspecified;
					return false;
				}
				uint num29 = (uint)(*source[28] - 48);
				uint num30 = (uint)(*source[29] - 48);
				if (num29 > 9U || num30 > 9U)
				{
					value = default(DateTimeOffset);
					bytesConsumed = 0;
					kind = DateTimeKind.Unspecified;
					return false;
				}
				int num31 = (int)(num29 * 10U + num30);
				if (*source[30] != 58)
				{
					value = default(DateTimeOffset);
					bytesConsumed = 0;
					kind = DateTimeKind.Unspecified;
					return false;
				}
				uint num32 = (uint)(*source[31] - 48);
				uint num33 = (uint)(*source[32] - 48);
				if (num32 > 9U || num33 > 9U)
				{
					value = default(DateTimeOffset);
					bytesConsumed = 0;
					kind = DateTimeKind.Unspecified;
					return false;
				}
				int num34 = (int)(num32 * 10U + num33);
				if (!Utf8Parser.TryCreateDateTimeOffset(num5, num8, num11, num14, num17, num20, num28, b == 45, num31, num34, out value))
				{
					value = default(DateTimeOffset);
					bytesConsumed = 0;
					kind = DateTimeKind.Unspecified;
					return false;
				}
				bytesConsumed = 33;
				kind = DateTimeKind.Local;
				return true;
			}
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x00026C90 File Offset: 0x00024E90
		private unsafe static bool TryParseDateTimeOffsetR(ReadOnlySpan<byte> source, uint caseFlipXorMask, out DateTimeOffset dateTimeOffset, out int bytesConsumed)
		{
			if (source.Length < 29)
			{
				bytesConsumed = 0;
				dateTimeOffset = default(DateTimeOffset);
				return false;
			}
			uint num = (uint)(*source[0]) ^ caseFlipXorMask;
			uint num2 = (uint)(*source[1]);
			uint num3 = (uint)(*source[2]);
			uint num4 = (uint)(*source[3]);
			uint num5 = (num << 24) | (num2 << 16) | (num3 << 8) | num4;
			DayOfWeek dayOfWeek;
			if (num5 <= 1398895660U)
			{
				if (num5 == 1181903148U)
				{
					dayOfWeek = DayOfWeek.Friday;
					goto IL_0114;
				}
				if (num5 == 1299148332U)
				{
					dayOfWeek = DayOfWeek.Monday;
					goto IL_0114;
				}
				if (num5 == 1398895660U)
				{
					dayOfWeek = DayOfWeek.Saturday;
					goto IL_0114;
				}
			}
			else if (num5 <= 1416131884U)
			{
				if (num5 == 1400204844U)
				{
					dayOfWeek = DayOfWeek.Sunday;
					goto IL_0114;
				}
				if (num5 == 1416131884U)
				{
					dayOfWeek = DayOfWeek.Thursday;
					goto IL_0114;
				}
			}
			else
			{
				if (num5 == 1416979756U)
				{
					dayOfWeek = DayOfWeek.Tuesday;
					goto IL_0114;
				}
				if (num5 == 1466262572U)
				{
					dayOfWeek = DayOfWeek.Wednesday;
					goto IL_0114;
				}
			}
			bytesConsumed = 0;
			dateTimeOffset = default(DateTimeOffset);
			return false;
			IL_0114:
			if (*source[4] != 32)
			{
				bytesConsumed = 0;
				dateTimeOffset = default(DateTimeOffset);
				return false;
			}
			uint num6 = (uint)(*source[5] - 48);
			uint num7 = (uint)(*source[6] - 48);
			if (num6 > 9U || num7 > 9U)
			{
				bytesConsumed = 0;
				dateTimeOffset = default(DateTimeOffset);
				return false;
			}
			int num8 = (int)(num6 * 10U + num7);
			if (*source[7] != 32)
			{
				bytesConsumed = 0;
				dateTimeOffset = default(DateTimeOffset);
				return false;
			}
			uint num9 = (uint)(*source[8]) ^ caseFlipXorMask;
			uint num10 = (uint)(*source[9]);
			uint num11 = (uint)(*source[10]);
			uint num12 = (uint)(*source[11]);
			uint num13 = (num9 << 24) | (num10 << 16) | (num11 << 8) | num12;
			int num14;
			if (num13 <= 1249209376U)
			{
				if (num13 <= 1147495200U)
				{
					if (num13 == 1097888288U)
					{
						num14 = 4;
						goto IL_030E;
					}
					if (num13 == 1098213152U)
					{
						num14 = 8;
						goto IL_030E;
					}
					if (num13 == 1147495200U)
					{
						num14 = 12;
						goto IL_030E;
					}
				}
				else
				{
					if (num13 == 1181049376U)
					{
						num14 = 2;
						goto IL_030E;
					}
					if (num13 == 1247899168U)
					{
						num14 = 1;
						goto IL_030E;
					}
					if (num13 == 1249209376U)
					{
						num14 = 7;
						goto IL_030E;
					}
				}
			}
			else if (num13 <= 1298233632U)
			{
				if (num13 == 1249209888U)
				{
					num14 = 6;
					goto IL_030E;
				}
				if (num13 == 1298231840U)
				{
					num14 = 3;
					goto IL_030E;
				}
				if (num13 == 1298233632U)
				{
					num14 = 5;
					goto IL_030E;
				}
			}
			else
			{
				if (num13 == 1315927584U)
				{
					num14 = 11;
					goto IL_030E;
				}
				if (num13 == 1331917856U)
				{
					num14 = 10;
					goto IL_030E;
				}
				if (num13 == 1399156768U)
				{
					num14 = 9;
					goto IL_030E;
				}
			}
			bytesConsumed = 0;
			dateTimeOffset = default(DateTimeOffset);
			return false;
			IL_030E:
			uint num15 = (uint)(*source[12] - 48);
			uint num16 = (uint)(*source[13] - 48);
			uint num17 = (uint)(*source[14] - 48);
			uint num18 = (uint)(*source[15] - 48);
			if (num15 > 9U || num16 > 9U || num17 > 9U || num18 > 9U)
			{
				bytesConsumed = 0;
				dateTimeOffset = default(DateTimeOffset);
				return false;
			}
			int num19 = (int)(num15 * 1000U + num16 * 100U + num17 * 10U + num18);
			if (*source[16] != 32)
			{
				bytesConsumed = 0;
				dateTimeOffset = default(DateTimeOffset);
				return false;
			}
			uint num20 = (uint)(*source[17] - 48);
			uint num21 = (uint)(*source[18] - 48);
			if (num20 > 9U || num21 > 9U)
			{
				bytesConsumed = 0;
				dateTimeOffset = default(DateTimeOffset);
				return false;
			}
			int num22 = (int)(num20 * 10U + num21);
			if (*source[19] != 58)
			{
				bytesConsumed = 0;
				dateTimeOffset = default(DateTimeOffset);
				return false;
			}
			uint num23 = (uint)(*source[20] - 48);
			uint num24 = (uint)(*source[21] - 48);
			if (num23 > 9U || num24 > 9U)
			{
				bytesConsumed = 0;
				dateTimeOffset = default(DateTimeOffset);
				return false;
			}
			int num25 = (int)(num23 * 10U + num24);
			if (*source[22] != 58)
			{
				bytesConsumed = 0;
				dateTimeOffset = default(DateTimeOffset);
				return false;
			}
			uint num26 = (uint)(*source[23] - 48);
			uint num27 = (uint)(*source[24] - 48);
			if (num26 > 9U || num27 > 9U)
			{
				bytesConsumed = 0;
				dateTimeOffset = default(DateTimeOffset);
				return false;
			}
			int num28 = (int)(num26 * 10U + num27);
			uint num29 = (uint)(*source[25]);
			uint num30 = (uint)(*source[26]) ^ caseFlipXorMask;
			uint num31 = (uint)(*source[27]) ^ caseFlipXorMask;
			uint num32 = (uint)(*source[28]) ^ caseFlipXorMask;
			uint num33 = (num29 << 24) | (num30 << 16) | (num31 << 8) | num32;
			if (num33 != 541543764U)
			{
				bytesConsumed = 0;
				dateTimeOffset = default(DateTimeOffset);
				return false;
			}
			if (!Utf8Parser.TryCreateDateTimeOffset(num19, num14, num8, num22, num25, num28, 0, false, 0, 0, out dateTimeOffset))
			{
				bytesConsumed = 0;
				dateTimeOffset = default(DateTimeOffset);
				return false;
			}
			if (dayOfWeek != dateTimeOffset.DayOfWeek)
			{
				bytesConsumed = 0;
				dateTimeOffset = default(DateTimeOffset);
				return false;
			}
			bytesConsumed = 29;
			return true;
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x00027208 File Offset: 0x00025408
		public unsafe static bool TryParse(ReadOnlySpan<byte> source, out decimal value, out int bytesConsumed, char standardFormat = '\0')
		{
			Utf8Parser.ParseNumberOptions parseNumberOptions;
			if (standardFormat != '\0')
			{
				switch (standardFormat)
				{
				case 'E':
				case 'G':
					goto IL_0035;
				case 'F':
					break;
				default:
					switch (standardFormat)
					{
					case 'e':
					case 'g':
						goto IL_0035;
					case 'f':
						break;
					default:
						return ThrowHelper.TryParseThrowFormatException<decimal>(out value, out bytesConsumed);
					}
					break;
				}
				parseNumberOptions = (Utf8Parser.ParseNumberOptions)0;
				goto IL_004B;
			}
			IL_0035:
			parseNumberOptions = Utf8Parser.ParseNumberOptions.AllowExponent;
			IL_004B:
			NumberBuffer numberBuffer = default(NumberBuffer);
			bool flag;
			if (!Utf8Parser.TryParseNumber(source, ref numberBuffer, out bytesConsumed, parseNumberOptions, out flag))
			{
				value = 0m;
				return false;
			}
			if (!flag && (standardFormat == 'E' || standardFormat == 'e'))
			{
				value = 0m;
				bytesConsumed = 0;
				return false;
			}
			if (*numberBuffer.Digits[0] == 0 && numberBuffer.Scale == 0)
			{
				numberBuffer.IsNegative = false;
			}
			value = 0m;
			if (!Number.NumberBufferToDecimal(ref numberBuffer, ref value))
			{
				value = 0m;
				bytesConsumed = 0;
				return false;
			}
			return true;
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x000272F4 File Offset: 0x000254F4
		public static bool TryParse(ReadOnlySpan<byte> source, out float value, out int bytesConsumed, char standardFormat = '\0')
		{
			double num;
			if (!Utf8Parser.TryParseNormalAsFloatingPoint(source, out num, out bytesConsumed, standardFormat))
			{
				return Utf8Parser.TryParseAsSpecialFloatingPoint<float>(source, float.PositiveInfinity, float.NegativeInfinity, float.NaN, out value, out bytesConsumed);
			}
			value = (float)num;
			if (float.IsInfinity(value))
			{
				value = 0f;
				bytesConsumed = 0;
				return false;
			}
			return true;
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x0002734C File Offset: 0x0002554C
		public static bool TryParse(ReadOnlySpan<byte> source, out double value, out int bytesConsumed, char standardFormat = '\0')
		{
			return Utf8Parser.TryParseNormalAsFloatingPoint(source, out value, out bytesConsumed, standardFormat) || Utf8Parser.TryParseAsSpecialFloatingPoint<double>(source, double.PositiveInfinity, double.NegativeInfinity, double.NaN, out value, out bytesConsumed);
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x00027384 File Offset: 0x00025584
		private unsafe static bool TryParseNormalAsFloatingPoint(ReadOnlySpan<byte> source, out double value, out int bytesConsumed, char standardFormat)
		{
			Utf8Parser.ParseNumberOptions parseNumberOptions;
			if (standardFormat != '\0')
			{
				switch (standardFormat)
				{
				case 'E':
				case 'G':
					goto IL_0035;
				case 'F':
					break;
				default:
					switch (standardFormat)
					{
					case 'e':
					case 'g':
						goto IL_0035;
					case 'f':
						break;
					default:
						return ThrowHelper.TryParseThrowFormatException<double>(out value, out bytesConsumed);
					}
					break;
				}
				parseNumberOptions = (Utf8Parser.ParseNumberOptions)0;
				goto IL_004B;
			}
			IL_0035:
			parseNumberOptions = Utf8Parser.ParseNumberOptions.AllowExponent;
			IL_004B:
			NumberBuffer numberBuffer = default(NumberBuffer);
			bool flag;
			if (!Utf8Parser.TryParseNumber(source, ref numberBuffer, out bytesConsumed, parseNumberOptions, out flag))
			{
				value = 0.0;
				return false;
			}
			if (!flag && (standardFormat == 'E' || standardFormat == 'e'))
			{
				value = 0.0;
				bytesConsumed = 0;
				return false;
			}
			if (*numberBuffer.Digits[0] == 0)
			{
				numberBuffer.IsNegative = false;
			}
			if (!Number.NumberBufferToDouble(ref numberBuffer, out value))
			{
				value = 0.0;
				bytesConsumed = 0;
				return false;
			}
			return true;
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x00027468 File Offset: 0x00025668
		private unsafe static bool TryParseAsSpecialFloatingPoint<T>(ReadOnlySpan<byte> source, T positiveInfinity, T negativeInfinity, T nan, out T value, out int bytesConsumed)
		{
			if (source.Length >= 8 && *source[0] == 73 && *source[1] == 110 && *source[2] == 102 && *source[3] == 105 && *source[4] == 110 && *source[5] == 105 && *source[6] == 116 && *source[7] == 121)
			{
				value = positiveInfinity;
				bytesConsumed = 8;
				return true;
			}
			if (source.Length >= 9 && *source[0] == 45 && *source[1] == 73 && *source[2] == 110 && *source[3] == 102 && *source[4] == 105 && *source[5] == 110 && *source[6] == 105 && *source[7] == 116 && *source[8] == 121)
			{
				value = negativeInfinity;
				bytesConsumed = 9;
				return true;
			}
			if (source.Length >= 3 && *source[0] == 78 && *source[1] == 97 && *source[2] == 78)
			{
				value = nan;
				bytesConsumed = 3;
				return true;
			}
			value = default(T);
			bytesConsumed = 0;
			return false;
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x0002761C File Offset: 0x0002581C
		public static bool TryParse(ReadOnlySpan<byte> source, out Guid value, out int bytesConsumed, char standardFormat = '\0')
		{
			if (standardFormat <= 'B')
			{
				if (standardFormat != '\0')
				{
					if (standardFormat != 'B')
					{
						goto IL_006B;
					}
					return Utf8Parser.TryParseGuidCore(source, true, '{', '}', out value, out bytesConsumed);
				}
			}
			else if (standardFormat != 'D')
			{
				if (standardFormat == 'N')
				{
					return Utf8Parser.TryParseGuidN(source, out value, out bytesConsumed);
				}
				if (standardFormat != 'P')
				{
					goto IL_006B;
				}
				return Utf8Parser.TryParseGuidCore(source, true, '(', ')', out value, out bytesConsumed);
			}
			return Utf8Parser.TryParseGuidCore(source, false, ' ', ' ', out value, out bytesConsumed);
			IL_006B:
			return ThrowHelper.TryParseThrowFormatException<Guid>(out value, out bytesConsumed);
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x000276A0 File Offset: 0x000258A0
		private static bool TryParseGuidN(ReadOnlySpan<byte> text, out Guid value, out int bytesConsumed)
		{
			if (text.Length < 32)
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			uint num;
			int num2;
			if (!Utf8Parser.TryParseUInt32X(text.Slice(0, 8), out num, out num2) || num2 != 8)
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			ushort num3;
			if (!Utf8Parser.TryParseUInt16X(text.Slice(8, 4), out num3, out num2) || num2 != 4)
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			ushort num4;
			if (!Utf8Parser.TryParseUInt16X(text.Slice(12, 4), out num4, out num2) || num2 != 4)
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			ushort num5;
			if (!Utf8Parser.TryParseUInt16X(text.Slice(16, 4), out num5, out num2) || num2 != 4)
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			ulong num6;
			if (!Utf8Parser.TryParseUInt64X(text.Slice(20), out num6, out num2) || num2 != 12)
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			bytesConsumed = 32;
			value = new Guid((int)num, (short)num3, (short)num4, (byte)(num5 >> 8), (byte)num5, (byte)(num6 >> 40), (byte)(num6 >> 32), (byte)(num6 >> 24), (byte)(num6 >> 16), (byte)(num6 >> 8), (byte)num6);
			return true;
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x000277E0 File Offset: 0x000259E0
		private unsafe static bool TryParseGuidCore(ReadOnlySpan<byte> source, bool ends, char begin, char end, out Guid value, out int bytesConsumed)
		{
			int num = 36 + (ends ? 2 : 0);
			if (source.Length < num)
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			if (ends)
			{
				if ((char)(*source[0]) != begin)
				{
					value = default(Guid);
					bytesConsumed = 0;
					return false;
				}
				source = source.Slice(1);
			}
			uint num2;
			int num3;
			if (!Utf8Parser.TryParseUInt32X(source, out num2, out num3))
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			if (num3 != 8)
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			if (*source[num3] != 45)
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			source = source.Slice(9);
			ushort num4;
			if (!Utf8Parser.TryParseUInt16X(source, out num4, out num3))
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			if (num3 != 4)
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			if (*source[num3] != 45)
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			source = source.Slice(5);
			ushort num5;
			if (!Utf8Parser.TryParseUInt16X(source, out num5, out num3))
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			if (num3 != 4)
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			if (*source[num3] != 45)
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			source = source.Slice(5);
			ushort num6;
			if (!Utf8Parser.TryParseUInt16X(source, out num6, out num3))
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			if (num3 != 4)
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			if (*source[num3] != 45)
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			source = source.Slice(5);
			ulong num7;
			if (!Utf8Parser.TryParseUInt64X(source, out num7, out num3))
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			if (num3 != 12)
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			if (ends && (char)(*source[num3]) != end)
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			bytesConsumed = num;
			value = new Guid((int)num2, (short)num4, (short)num5, (byte)(num6 >> 8), (byte)num6, (byte)(num7 >> 40), (byte)(num7 >> 32), (byte)(num7 >> 24), (byte)(num7 >> 16), (byte)(num7 >> 8), (byte)num7);
			return true;
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x00027A48 File Offset: 0x00025C48
		[CLSCompliant(false)]
		public static bool TryParse(ReadOnlySpan<byte> source, out sbyte value, out int bytesConsumed, char standardFormat = '\0')
		{
			if (standardFormat > 'N')
			{
				if (standardFormat <= 'd')
				{
					if (standardFormat != 'X')
					{
						if (standardFormat != 'd')
						{
							goto IL_0095;
						}
						goto IL_0072;
					}
				}
				else
				{
					if (standardFormat == 'g')
					{
						goto IL_0072;
					}
					if (standardFormat == 'n')
					{
						goto IL_007B;
					}
					if (standardFormat != 'x')
					{
						goto IL_0095;
					}
				}
				value = 0;
				return Utf8Parser.TryParseByteX(source, Unsafe.As<sbyte, byte>(ref value), out bytesConsumed);
			}
			if (standardFormat <= 'D')
			{
				if (standardFormat != '\0' && standardFormat != 'D')
				{
					goto IL_0095;
				}
			}
			else if (standardFormat != 'G')
			{
				if (standardFormat != 'N')
				{
					goto IL_0095;
				}
				goto IL_007B;
			}
			IL_0072:
			return Utf8Parser.TryParseSByteD(source, out value, out bytesConsumed);
			IL_007B:
			return Utf8Parser.TryParseSByteN(source, out value, out bytesConsumed);
			IL_0095:
			return ThrowHelper.TryParseThrowFormatException<sbyte>(out value, out bytesConsumed);
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00027AF8 File Offset: 0x00025CF8
		public static bool TryParse(ReadOnlySpan<byte> source, out short value, out int bytesConsumed, char standardFormat = '\0')
		{
			if (standardFormat > 'N')
			{
				if (standardFormat <= 'd')
				{
					if (standardFormat != 'X')
					{
						if (standardFormat != 'd')
						{
							goto IL_0095;
						}
						goto IL_0072;
					}
				}
				else
				{
					if (standardFormat == 'g')
					{
						goto IL_0072;
					}
					if (standardFormat == 'n')
					{
						goto IL_007B;
					}
					if (standardFormat != 'x')
					{
						goto IL_0095;
					}
				}
				value = 0;
				return Utf8Parser.TryParseUInt16X(source, Unsafe.As<short, ushort>(ref value), out bytesConsumed);
			}
			if (standardFormat <= 'D')
			{
				if (standardFormat != '\0' && standardFormat != 'D')
				{
					goto IL_0095;
				}
			}
			else if (standardFormat != 'G')
			{
				if (standardFormat != 'N')
				{
					goto IL_0095;
				}
				goto IL_007B;
			}
			IL_0072:
			return Utf8Parser.TryParseInt16D(source, out value, out bytesConsumed);
			IL_007B:
			return Utf8Parser.TryParseInt16N(source, out value, out bytesConsumed);
			IL_0095:
			return ThrowHelper.TryParseThrowFormatException<short>(out value, out bytesConsumed);
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00027BA8 File Offset: 0x00025DA8
		public static bool TryParse(ReadOnlySpan<byte> source, out int value, out int bytesConsumed, char standardFormat = '\0')
		{
			if (standardFormat > 'N')
			{
				if (standardFormat <= 'd')
				{
					if (standardFormat != 'X')
					{
						if (standardFormat != 'd')
						{
							goto IL_0095;
						}
						goto IL_0072;
					}
				}
				else
				{
					if (standardFormat == 'g')
					{
						goto IL_0072;
					}
					if (standardFormat == 'n')
					{
						goto IL_007B;
					}
					if (standardFormat != 'x')
					{
						goto IL_0095;
					}
				}
				value = 0;
				return Utf8Parser.TryParseUInt32X(source, Unsafe.As<int, uint>(ref value), out bytesConsumed);
			}
			if (standardFormat <= 'D')
			{
				if (standardFormat != '\0' && standardFormat != 'D')
				{
					goto IL_0095;
				}
			}
			else if (standardFormat != 'G')
			{
				if (standardFormat != 'N')
				{
					goto IL_0095;
				}
				goto IL_007B;
			}
			IL_0072:
			return Utf8Parser.TryParseInt32D(source, out value, out bytesConsumed);
			IL_007B:
			return Utf8Parser.TryParseInt32N(source, out value, out bytesConsumed);
			IL_0095:
			return ThrowHelper.TryParseThrowFormatException<int>(out value, out bytesConsumed);
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x00027C58 File Offset: 0x00025E58
		public static bool TryParse(ReadOnlySpan<byte> source, out long value, out int bytesConsumed, char standardFormat = '\0')
		{
			if (standardFormat > 'N')
			{
				if (standardFormat <= 'd')
				{
					if (standardFormat != 'X')
					{
						if (standardFormat != 'd')
						{
							goto IL_0096;
						}
						goto IL_0072;
					}
				}
				else
				{
					if (standardFormat == 'g')
					{
						goto IL_0072;
					}
					if (standardFormat == 'n')
					{
						goto IL_007B;
					}
					if (standardFormat != 'x')
					{
						goto IL_0096;
					}
				}
				value = 0L;
				return Utf8Parser.TryParseUInt64X(source, Unsafe.As<long, ulong>(ref value), out bytesConsumed);
			}
			if (standardFormat <= 'D')
			{
				if (standardFormat != '\0' && standardFormat != 'D')
				{
					goto IL_0096;
				}
			}
			else if (standardFormat != 'G')
			{
				if (standardFormat != 'N')
				{
					goto IL_0096;
				}
				goto IL_007B;
			}
			IL_0072:
			return Utf8Parser.TryParseInt64D(source, out value, out bytesConsumed);
			IL_007B:
			return Utf8Parser.TryParseInt64N(source, out value, out bytesConsumed);
			IL_0096:
			return ThrowHelper.TryParseThrowFormatException<long>(out value, out bytesConsumed);
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x00027D08 File Offset: 0x00025F08
		private unsafe static bool TryParseSByteD(ReadOnlySpan<byte> source, out sbyte value, out int bytesConsumed)
		{
			if (source.Length >= 1)
			{
				int num = 1;
				int num2 = 0;
				int num3 = (int)(*source[num2]);
				if (num3 == 45)
				{
					num = -1;
					num2++;
					if (num2 >= source.Length)
					{
						goto IL_0144;
					}
					num3 = (int)(*source[num2]);
				}
				else if (num3 == 43)
				{
					num2++;
					if (num2 >= source.Length)
					{
						goto IL_0144;
					}
					num3 = (int)(*source[num2]);
				}
				int num4 = 0;
				if (ParserHelpers.IsDigit(num3))
				{
					if (num3 == 48)
					{
						do
						{
							num2++;
							if (num2 >= source.Length)
							{
								goto IL_014C;
							}
							num3 = (int)(*source[num2]);
						}
						while (num3 == 48);
						if (!ParserHelpers.IsDigit(num3))
						{
							goto IL_014C;
						}
					}
					num4 = num3 - 48;
					num2++;
					if (num2 < source.Length)
					{
						num3 = (int)(*source[num2]);
						if (ParserHelpers.IsDigit(num3))
						{
							num2++;
							num4 = 10 * num4 + num3 - 48;
							if (num2 < source.Length)
							{
								num3 = (int)(*source[num2]);
								if (ParserHelpers.IsDigit(num3))
								{
									num2++;
									num4 = num4 * 10 + num3 - 48;
									if ((ulong)num4 > (ulong)(127L + (long)((-1 * num + 1) / 2)) || (num2 < source.Length && ParserHelpers.IsDigit((int)(*source[num2]))))
									{
										goto IL_0144;
									}
								}
							}
						}
					}
					IL_014C:
					bytesConsumed = num2;
					value = (sbyte)(num4 * num);
					return true;
				}
			}
			IL_0144:
			bytesConsumed = 0;
			value = 0;
			return false;
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x00027E70 File Offset: 0x00026070
		private unsafe static bool TryParseInt16D(ReadOnlySpan<byte> source, out short value, out int bytesConsumed)
		{
			if (source.Length >= 1)
			{
				int num = 1;
				int num2 = 0;
				int num3 = (int)(*source[num2]);
				if (num3 == 45)
				{
					num = -1;
					num2++;
					if (num2 >= source.Length)
					{
						goto IL_01A7;
					}
					num3 = (int)(*source[num2]);
				}
				else if (num3 == 43)
				{
					num2++;
					if (num2 >= source.Length)
					{
						goto IL_01A7;
					}
					num3 = (int)(*source[num2]);
				}
				int num4 = 0;
				if (ParserHelpers.IsDigit(num3))
				{
					if (num3 == 48)
					{
						do
						{
							num2++;
							if (num2 >= source.Length)
							{
								goto IL_01AF;
							}
							num3 = (int)(*source[num2]);
						}
						while (num3 == 48);
						if (!ParserHelpers.IsDigit(num3))
						{
							goto IL_01AF;
						}
					}
					num4 = num3 - 48;
					num2++;
					if (num2 < source.Length)
					{
						num3 = (int)(*source[num2]);
						if (ParserHelpers.IsDigit(num3))
						{
							num2++;
							num4 = 10 * num4 + num3 - 48;
							if (num2 < source.Length)
							{
								num3 = (int)(*source[num2]);
								if (ParserHelpers.IsDigit(num3))
								{
									num2++;
									num4 = 10 * num4 + num3 - 48;
									if (num2 < source.Length)
									{
										num3 = (int)(*source[num2]);
										if (ParserHelpers.IsDigit(num3))
										{
											num2++;
											num4 = 10 * num4 + num3 - 48;
											if (num2 < source.Length)
											{
												num3 = (int)(*source[num2]);
												if (ParserHelpers.IsDigit(num3))
												{
													num2++;
													num4 = num4 * 10 + num3 - 48;
													if ((ulong)num4 > (ulong)(32767L + (long)((-1 * num + 1) / 2)) || (num2 < source.Length && ParserHelpers.IsDigit((int)(*source[num2]))))
													{
														goto IL_01A7;
													}
												}
											}
										}
									}
								}
							}
						}
					}
					IL_01AF:
					bytesConsumed = num2;
					value = (short)(num4 * num);
					return true;
				}
			}
			IL_01A7:
			bytesConsumed = 0;
			value = 0;
			return false;
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x0002803C File Offset: 0x0002623C
		private unsafe static bool TryParseInt32D(ReadOnlySpan<byte> source, out int value, out int bytesConsumed)
		{
			if (source.Length >= 1)
			{
				int num = 1;
				int num2 = 0;
				int num3 = (int)(*source[num2]);
				if (num3 == 45)
				{
					num = -1;
					num2++;
					if (num2 >= source.Length)
					{
						goto IL_02A2;
					}
					num3 = (int)(*source[num2]);
				}
				else if (num3 == 43)
				{
					num2++;
					if (num2 >= source.Length)
					{
						goto IL_02A2;
					}
					num3 = (int)(*source[num2]);
				}
				int num4 = 0;
				if (ParserHelpers.IsDigit(num3))
				{
					if (num3 == 48)
					{
						do
						{
							num2++;
							if (num2 >= source.Length)
							{
								goto IL_02AA;
							}
							num3 = (int)(*source[num2]);
						}
						while (num3 == 48);
						if (!ParserHelpers.IsDigit(num3))
						{
							goto IL_02AA;
						}
					}
					num4 = num3 - 48;
					num2++;
					if (num2 < source.Length)
					{
						num3 = (int)(*source[num2]);
						if (ParserHelpers.IsDigit(num3))
						{
							num2++;
							num4 = 10 * num4 + num3 - 48;
							if (num2 < source.Length)
							{
								num3 = (int)(*source[num2]);
								if (ParserHelpers.IsDigit(num3))
								{
									num2++;
									num4 = 10 * num4 + num3 - 48;
									if (num2 < source.Length)
									{
										num3 = (int)(*source[num2]);
										if (ParserHelpers.IsDigit(num3))
										{
											num2++;
											num4 = 10 * num4 + num3 - 48;
											if (num2 < source.Length)
											{
												num3 = (int)(*source[num2]);
												if (ParserHelpers.IsDigit(num3))
												{
													num2++;
													num4 = 10 * num4 + num3 - 48;
													if (num2 < source.Length)
													{
														num3 = (int)(*source[num2]);
														if (ParserHelpers.IsDigit(num3))
														{
															num2++;
															num4 = 10 * num4 + num3 - 48;
															if (num2 < source.Length)
															{
																num3 = (int)(*source[num2]);
																if (ParserHelpers.IsDigit(num3))
																{
																	num2++;
																	num4 = 10 * num4 + num3 - 48;
																	if (num2 < source.Length)
																	{
																		num3 = (int)(*source[num2]);
																		if (ParserHelpers.IsDigit(num3))
																		{
																			num2++;
																			num4 = 10 * num4 + num3 - 48;
																			if (num2 < source.Length)
																			{
																				num3 = (int)(*source[num2]);
																				if (ParserHelpers.IsDigit(num3))
																				{
																					num2++;
																					num4 = 10 * num4 + num3 - 48;
																					if (num2 < source.Length)
																					{
																						num3 = (int)(*source[num2]);
																						if (ParserHelpers.IsDigit(num3))
																						{
																							num2++;
																							if (num4 > 214748364)
																							{
																								goto IL_02A2;
																							}
																							num4 = num4 * 10 + num3 - 48;
																							if ((ulong)num4 > (ulong)(2147483647L + (long)((-1 * num + 1) / 2)) || (num2 < source.Length && ParserHelpers.IsDigit((int)(*source[num2]))))
																							{
																								goto IL_02A2;
																							}
																						}
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
					IL_02AA:
					bytesConsumed = num2;
					value = num4 * num;
					return true;
				}
			}
			IL_02A2:
			bytesConsumed = 0;
			value = 0;
			return false;
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x00028300 File Offset: 0x00026500
		private unsafe static bool TryParseInt64D(ReadOnlySpan<byte> source, out long value, out int bytesConsumed)
		{
			if (source.Length < 1)
			{
				bytesConsumed = 0;
				value = 0L;
				return false;
			}
			int num = 0;
			int num2 = 1;
			if (*source[0] == 45)
			{
				num = 1;
				num2 = -1;
				if (source.Length <= num)
				{
					bytesConsumed = 0;
					value = 0L;
					return false;
				}
			}
			else if (*source[0] == 43)
			{
				num = 1;
				if (source.Length <= num)
				{
					bytesConsumed = 0;
					value = 0L;
					return false;
				}
			}
			int num3 = 19 + num;
			long num4 = (long)(*source[num] - 48);
			if (num4 < 0L || num4 > 9L)
			{
				bytesConsumed = 0;
				value = 0L;
				return false;
			}
			ulong num5 = (ulong)num4;
			if (source.Length < num3)
			{
				for (int i = num + 1; i < source.Length; i++)
				{
					long num6 = (long)(*source[i] - 48);
					if (num6 < 0L || num6 > 9L)
					{
						bytesConsumed = i;
						value = (long)(num5 * (ulong)((long)num2));
						return true;
					}
					num5 = num5 * 10UL + (ulong)num6;
				}
			}
			else
			{
				for (int j = num + 1; j < num3 - 1; j++)
				{
					long num7 = (long)(*source[j] - 48);
					if (num7 < 0L || num7 > 9L)
					{
						bytesConsumed = j;
						value = (long)(num5 * (ulong)((long)num2));
						return true;
					}
					num5 = num5 * 10UL + (ulong)num7;
				}
				for (int k = num3 - 1; k < source.Length; k++)
				{
					long num8 = (long)(*source[k] - 48);
					if (num8 < 0L || num8 > 9L)
					{
						bytesConsumed = k;
						value = (long)(num5 * (ulong)((long)num2));
						return true;
					}
					bool flag = num2 > 0;
					bool flag2 = num8 > 8L || (flag && num8 > 7L);
					if (num5 > 922337203685477580UL || (num5 == 922337203685477580UL && flag2))
					{
						bytesConsumed = 0;
						value = 0L;
						return false;
					}
					num5 = num5 * 10UL + (ulong)num8;
				}
			}
			bytesConsumed = source.Length;
			value = (long)(num5 * (ulong)((long)num2));
			return true;
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x0002852C File Offset: 0x0002672C
		private unsafe static bool TryParseSByteN(ReadOnlySpan<byte> source, out sbyte value, out int bytesConsumed)
		{
			if (source.Length >= 1)
			{
				int num = 1;
				int num2 = 0;
				int num3 = (int)(*source[num2]);
				if (num3 == 45)
				{
					num = -1;
					num2++;
					if (num2 >= source.Length)
					{
						goto IL_011D;
					}
					num3 = (int)(*source[num2]);
				}
				else if (num3 == 43)
				{
					num2++;
					if (num2 >= source.Length)
					{
						goto IL_011D;
					}
					num3 = (int)(*source[num2]);
				}
				int num4;
				if (num3 != 46)
				{
					if (ParserHelpers.IsDigit(num3))
					{
						num4 = num3 - 48;
						for (;;)
						{
							num2++;
							if (num2 >= source.Length)
							{
								goto IL_0125;
							}
							num3 = (int)(*source[num2]);
							if (num3 != 44)
							{
								if (num3 == 46)
								{
									goto IL_00F2;
								}
								if (!ParserHelpers.IsDigit(num3))
								{
									goto IL_0125;
								}
								num4 = num4 * 10 + num3 - 48;
								if (num4 > 127 + (-1 * num + 1) / 2)
								{
									break;
								}
							}
						}
						goto IL_011D;
					}
					goto IL_011D;
				}
				else
				{
					num4 = 0;
					num2++;
					if (num2 >= source.Length || *source[num2] != 48)
					{
						goto IL_011D;
					}
				}
				do
				{
					IL_00F2:
					num2++;
					if (num2 >= source.Length)
					{
						goto IL_0125;
					}
					num3 = (int)(*source[num2]);
				}
				while (num3 == 48);
				if (ParserHelpers.IsDigit(num3))
				{
					goto IL_011D;
				}
				IL_0125:
				bytesConsumed = num2;
				value = (sbyte)(num4 * num);
				return true;
			}
			IL_011D:
			bytesConsumed = 0;
			value = 0;
			return false;
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x0002866C File Offset: 0x0002686C
		private unsafe static bool TryParseInt16N(ReadOnlySpan<byte> source, out short value, out int bytesConsumed)
		{
			if (source.Length >= 1)
			{
				int num = 1;
				int num2 = 0;
				int num3 = (int)(*source[num2]);
				if (num3 == 45)
				{
					num = -1;
					num2++;
					if (num2 >= source.Length)
					{
						goto IL_0120;
					}
					num3 = (int)(*source[num2]);
				}
				else if (num3 == 43)
				{
					num2++;
					if (num2 >= source.Length)
					{
						goto IL_0120;
					}
					num3 = (int)(*source[num2]);
				}
				int num4;
				if (num3 != 46)
				{
					if (ParserHelpers.IsDigit(num3))
					{
						num4 = num3 - 48;
						for (;;)
						{
							num2++;
							if (num2 >= source.Length)
							{
								goto IL_0128;
							}
							num3 = (int)(*source[num2]);
							if (num3 != 44)
							{
								if (num3 == 46)
								{
									goto IL_00F5;
								}
								if (!ParserHelpers.IsDigit(num3))
								{
									goto IL_0128;
								}
								num4 = num4 * 10 + num3 - 48;
								if (num4 > 32767 + (-1 * num + 1) / 2)
								{
									break;
								}
							}
						}
						goto IL_0120;
					}
					goto IL_0120;
				}
				else
				{
					num4 = 0;
					num2++;
					if (num2 >= source.Length || *source[num2] != 48)
					{
						goto IL_0120;
					}
				}
				do
				{
					IL_00F5:
					num2++;
					if (num2 >= source.Length)
					{
						goto IL_0128;
					}
					num3 = (int)(*source[num2]);
				}
				while (num3 == 48);
				if (ParserHelpers.IsDigit(num3))
				{
					goto IL_0120;
				}
				IL_0128:
				bytesConsumed = num2;
				value = (short)(num4 * num);
				return true;
			}
			IL_0120:
			bytesConsumed = 0;
			value = 0;
			return false;
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x000287B0 File Offset: 0x000269B0
		private unsafe static bool TryParseInt32N(ReadOnlySpan<byte> source, out int value, out int bytesConsumed)
		{
			if (source.Length >= 1)
			{
				int num = 1;
				int num2 = 0;
				int num3 = (int)(*source[num2]);
				if (num3 == 45)
				{
					num = -1;
					num2++;
					if (num2 >= source.Length)
					{
						goto IL_012E;
					}
					num3 = (int)(*source[num2]);
				}
				else if (num3 == 43)
				{
					num2++;
					if (num2 >= source.Length)
					{
						goto IL_012E;
					}
					num3 = (int)(*source[num2]);
				}
				int num4;
				if (num3 != 46)
				{
					if (ParserHelpers.IsDigit(num3))
					{
						num4 = num3 - 48;
						for (;;)
						{
							num2++;
							if (num2 >= source.Length)
							{
								goto IL_0136;
							}
							num3 = (int)(*source[num2]);
							if (num3 != 44)
							{
								if (num3 == 46)
								{
									goto IL_0103;
								}
								if (!ParserHelpers.IsDigit(num3))
								{
									goto IL_0136;
								}
								if (num4 > 214748364)
								{
									break;
								}
								num4 = num4 * 10 + num3 - 48;
								if ((ulong)num4 > (ulong)(2147483647L + (long)((-1 * num + 1) / 2)))
								{
									break;
								}
							}
						}
						goto IL_012E;
					}
					goto IL_012E;
				}
				else
				{
					num4 = 0;
					num2++;
					if (num2 >= source.Length || *source[num2] != 48)
					{
						goto IL_012E;
					}
				}
				do
				{
					IL_0103:
					num2++;
					if (num2 >= source.Length)
					{
						goto IL_0136;
					}
					num3 = (int)(*source[num2]);
				}
				while (num3 == 48);
				if (ParserHelpers.IsDigit(num3))
				{
					goto IL_012E;
				}
				IL_0136:
				bytesConsumed = num2;
				value = num4 * num;
				return true;
			}
			IL_012E:
			bytesConsumed = 0;
			value = 0;
			return false;
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x00028900 File Offset: 0x00026B00
		private unsafe static bool TryParseInt64N(ReadOnlySpan<byte> source, out long value, out int bytesConsumed)
		{
			if (source.Length >= 1)
			{
				int num = 1;
				int num2 = 0;
				int num3 = (int)(*source[num2]);
				if (num3 == 45)
				{
					num = -1;
					num2++;
					if (num2 >= source.Length)
					{
						goto IL_0139;
					}
					num3 = (int)(*source[num2]);
				}
				else if (num3 == 43)
				{
					num2++;
					if (num2 >= source.Length)
					{
						goto IL_0139;
					}
					num3 = (int)(*source[num2]);
				}
				long num4;
				if (num3 != 46)
				{
					if (ParserHelpers.IsDigit(num3))
					{
						num4 = (long)(num3 - 48);
						for (;;)
						{
							num2++;
							if (num2 >= source.Length)
							{
								goto IL_0142;
							}
							num3 = (int)(*source[num2]);
							if (num3 != 44)
							{
								if (num3 == 46)
								{
									goto IL_010E;
								}
								if (!ParserHelpers.IsDigit(num3))
								{
									goto IL_0142;
								}
								if (num4 > 922337203685477580L)
								{
									break;
								}
								num4 = num4 * 10L + (long)num3 - 48L;
								if (num4 > 9223372036854775807L + (long)((-1 * num + 1) / 2))
								{
									break;
								}
							}
						}
						goto IL_0139;
					}
					goto IL_0139;
				}
				else
				{
					num4 = 0L;
					num2++;
					if (num2 >= source.Length || *source[num2] != 48)
					{
						goto IL_0139;
					}
				}
				do
				{
					IL_010E:
					num2++;
					if (num2 >= source.Length)
					{
						goto IL_0142;
					}
					num3 = (int)(*source[num2]);
				}
				while (num3 == 48);
				if (ParserHelpers.IsDigit(num3))
				{
					goto IL_0139;
				}
				IL_0142:
				bytesConsumed = num2;
				value = num4 * (long)num;
				return true;
			}
			IL_0139:
			bytesConsumed = 0;
			value = 0L;
			return false;
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x00028A60 File Offset: 0x00026C60
		public static bool TryParse(ReadOnlySpan<byte> source, out byte value, out int bytesConsumed, char standardFormat = '\0')
		{
			if (standardFormat > 'N')
			{
				if (standardFormat <= 'd')
				{
					if (standardFormat != 'X')
					{
						if (standardFormat != 'd')
						{
							goto IL_008D;
						}
						goto IL_0072;
					}
				}
				else
				{
					if (standardFormat == 'g')
					{
						goto IL_0072;
					}
					if (standardFormat == 'n')
					{
						goto IL_007B;
					}
					if (standardFormat != 'x')
					{
						goto IL_008D;
					}
				}
				return Utf8Parser.TryParseByteX(source, out value, out bytesConsumed);
			}
			if (standardFormat <= 'D')
			{
				if (standardFormat != '\0' && standardFormat != 'D')
				{
					goto IL_008D;
				}
			}
			else if (standardFormat != 'G')
			{
				if (standardFormat != 'N')
				{
					goto IL_008D;
				}
				goto IL_007B;
			}
			IL_0072:
			return Utf8Parser.TryParseByteD(source, out value, out bytesConsumed);
			IL_007B:
			return Utf8Parser.TryParseByteN(source, out value, out bytesConsumed);
			IL_008D:
			return ThrowHelper.TryParseThrowFormatException<byte>(out value, out bytesConsumed);
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x00028B08 File Offset: 0x00026D08
		[CLSCompliant(false)]
		public static bool TryParse(ReadOnlySpan<byte> source, out ushort value, out int bytesConsumed, char standardFormat = '\0')
		{
			if (standardFormat > 'N')
			{
				if (standardFormat <= 'd')
				{
					if (standardFormat != 'X')
					{
						if (standardFormat != 'd')
						{
							goto IL_008D;
						}
						goto IL_0072;
					}
				}
				else
				{
					if (standardFormat == 'g')
					{
						goto IL_0072;
					}
					if (standardFormat == 'n')
					{
						goto IL_007B;
					}
					if (standardFormat != 'x')
					{
						goto IL_008D;
					}
				}
				return Utf8Parser.TryParseUInt16X(source, out value, out bytesConsumed);
			}
			if (standardFormat <= 'D')
			{
				if (standardFormat != '\0' && standardFormat != 'D')
				{
					goto IL_008D;
				}
			}
			else if (standardFormat != 'G')
			{
				if (standardFormat != 'N')
				{
					goto IL_008D;
				}
				goto IL_007B;
			}
			IL_0072:
			return Utf8Parser.TryParseUInt16D(source, out value, out bytesConsumed);
			IL_007B:
			return Utf8Parser.TryParseUInt16N(source, out value, out bytesConsumed);
			IL_008D:
			return ThrowHelper.TryParseThrowFormatException<ushort>(out value, out bytesConsumed);
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x00028BB0 File Offset: 0x00026DB0
		[CLSCompliant(false)]
		public static bool TryParse(ReadOnlySpan<byte> source, out uint value, out int bytesConsumed, char standardFormat = '\0')
		{
			if (standardFormat > 'N')
			{
				if (standardFormat <= 'd')
				{
					if (standardFormat != 'X')
					{
						if (standardFormat != 'd')
						{
							goto IL_008D;
						}
						goto IL_0072;
					}
				}
				else
				{
					if (standardFormat == 'g')
					{
						goto IL_0072;
					}
					if (standardFormat == 'n')
					{
						goto IL_007B;
					}
					if (standardFormat != 'x')
					{
						goto IL_008D;
					}
				}
				return Utf8Parser.TryParseUInt32X(source, out value, out bytesConsumed);
			}
			if (standardFormat <= 'D')
			{
				if (standardFormat != '\0' && standardFormat != 'D')
				{
					goto IL_008D;
				}
			}
			else if (standardFormat != 'G')
			{
				if (standardFormat != 'N')
				{
					goto IL_008D;
				}
				goto IL_007B;
			}
			IL_0072:
			return Utf8Parser.TryParseUInt32D(source, out value, out bytesConsumed);
			IL_007B:
			return Utf8Parser.TryParseUInt32N(source, out value, out bytesConsumed);
			IL_008D:
			return ThrowHelper.TryParseThrowFormatException<uint>(out value, out bytesConsumed);
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x00028C58 File Offset: 0x00026E58
		[CLSCompliant(false)]
		public static bool TryParse(ReadOnlySpan<byte> source, out ulong value, out int bytesConsumed, char standardFormat = '\0')
		{
			if (standardFormat > 'N')
			{
				if (standardFormat <= 'd')
				{
					if (standardFormat != 'X')
					{
						if (standardFormat != 'd')
						{
							goto IL_008D;
						}
						goto IL_0072;
					}
				}
				else
				{
					if (standardFormat == 'g')
					{
						goto IL_0072;
					}
					if (standardFormat == 'n')
					{
						goto IL_007B;
					}
					if (standardFormat != 'x')
					{
						goto IL_008D;
					}
				}
				return Utf8Parser.TryParseUInt64X(source, out value, out bytesConsumed);
			}
			if (standardFormat <= 'D')
			{
				if (standardFormat != '\0' && standardFormat != 'D')
				{
					goto IL_008D;
				}
			}
			else if (standardFormat != 'G')
			{
				if (standardFormat != 'N')
				{
					goto IL_008D;
				}
				goto IL_007B;
			}
			IL_0072:
			return Utf8Parser.TryParseUInt64D(source, out value, out bytesConsumed);
			IL_007B:
			return Utf8Parser.TryParseUInt64N(source, out value, out bytesConsumed);
			IL_008D:
			return ThrowHelper.TryParseThrowFormatException<ulong>(out value, out bytesConsumed);
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x00028D00 File Offset: 0x00026F00
		private unsafe static bool TryParseByteD(ReadOnlySpan<byte> source, out byte value, out int bytesConsumed)
		{
			if (source.Length >= 1)
			{
				int num = 0;
				int num2 = (int)(*source[num]);
				int num3 = 0;
				if (ParserHelpers.IsDigit(num2))
				{
					if (num2 == 48)
					{
						do
						{
							num++;
							if (num >= source.Length)
							{
								goto IL_00F5;
							}
							num2 = (int)(*source[num]);
						}
						while (num2 == 48);
						if (!ParserHelpers.IsDigit(num2))
						{
							goto IL_00F5;
						}
					}
					num3 = num2 - 48;
					num++;
					if (num < source.Length)
					{
						num2 = (int)(*source[num]);
						if (ParserHelpers.IsDigit(num2))
						{
							num++;
							num3 = 10 * num3 + num2 - 48;
							if (num < source.Length)
							{
								num2 = (int)(*source[num]);
								if (ParserHelpers.IsDigit(num2))
								{
									num++;
									num3 = num3 * 10 + num2 - 48;
									if (num3 > 255 || (num < source.Length && ParserHelpers.IsDigit((int)(*source[num]))))
									{
										goto IL_00ED;
									}
								}
							}
						}
					}
					IL_00F5:
					bytesConsumed = num;
					value = (byte)num3;
					return true;
				}
			}
			IL_00ED:
			bytesConsumed = 0;
			value = 0;
			return false;
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x00028E10 File Offset: 0x00027010
		private unsafe static bool TryParseUInt16D(ReadOnlySpan<byte> source, out ushort value, out int bytesConsumed)
		{
			if (source.Length >= 1)
			{
				int num = 0;
				int num2 = (int)(*source[num]);
				int num3 = 0;
				if (ParserHelpers.IsDigit(num2))
				{
					if (num2 == 48)
					{
						do
						{
							num++;
							if (num >= source.Length)
							{
								goto IL_0155;
							}
							num2 = (int)(*source[num]);
						}
						while (num2 == 48);
						if (!ParserHelpers.IsDigit(num2))
						{
							goto IL_0155;
						}
					}
					num3 = num2 - 48;
					num++;
					if (num < source.Length)
					{
						num2 = (int)(*source[num]);
						if (ParserHelpers.IsDigit(num2))
						{
							num++;
							num3 = 10 * num3 + num2 - 48;
							if (num < source.Length)
							{
								num2 = (int)(*source[num]);
								if (ParserHelpers.IsDigit(num2))
								{
									num++;
									num3 = 10 * num3 + num2 - 48;
									if (num < source.Length)
									{
										num2 = (int)(*source[num]);
										if (ParserHelpers.IsDigit(num2))
										{
											num++;
											num3 = 10 * num3 + num2 - 48;
											if (num < source.Length)
											{
												num2 = (int)(*source[num]);
												if (ParserHelpers.IsDigit(num2))
												{
													num++;
													num3 = num3 * 10 + num2 - 48;
													if (num3 > 65535 || (num < source.Length && ParserHelpers.IsDigit((int)(*source[num]))))
													{
														goto IL_014D;
													}
												}
											}
										}
									}
								}
							}
						}
					}
					IL_0155:
					bytesConsumed = num;
					value = (ushort)num3;
					return true;
				}
			}
			IL_014D:
			bytesConsumed = 0;
			value = 0;
			return false;
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x00028F80 File Offset: 0x00027180
		private unsafe static bool TryParseUInt32D(ReadOnlySpan<byte> source, out uint value, out int bytesConsumed)
		{
			if (source.Length >= 1)
			{
				int num = 0;
				int num2 = (int)(*source[num]);
				int num3 = 0;
				if (ParserHelpers.IsDigit(num2))
				{
					if (num2 == 48)
					{
						do
						{
							num++;
							if (num >= source.Length)
							{
								goto IL_0258;
							}
							num2 = (int)(*source[num]);
						}
						while (num2 == 48);
						if (!ParserHelpers.IsDigit(num2))
						{
							goto IL_0258;
						}
					}
					num3 = num2 - 48;
					num++;
					if (num < source.Length)
					{
						num2 = (int)(*source[num]);
						if (ParserHelpers.IsDigit(num2))
						{
							num++;
							num3 = 10 * num3 + num2 - 48;
							if (num < source.Length)
							{
								num2 = (int)(*source[num]);
								if (ParserHelpers.IsDigit(num2))
								{
									num++;
									num3 = 10 * num3 + num2 - 48;
									if (num < source.Length)
									{
										num2 = (int)(*source[num]);
										if (ParserHelpers.IsDigit(num2))
										{
											num++;
											num3 = 10 * num3 + num2 - 48;
											if (num < source.Length)
											{
												num2 = (int)(*source[num]);
												if (ParserHelpers.IsDigit(num2))
												{
													num++;
													num3 = 10 * num3 + num2 - 48;
													if (num < source.Length)
													{
														num2 = (int)(*source[num]);
														if (ParserHelpers.IsDigit(num2))
														{
															num++;
															num3 = 10 * num3 + num2 - 48;
															if (num < source.Length)
															{
																num2 = (int)(*source[num]);
																if (ParserHelpers.IsDigit(num2))
																{
																	num++;
																	num3 = 10 * num3 + num2 - 48;
																	if (num < source.Length)
																	{
																		num2 = (int)(*source[num]);
																		if (ParserHelpers.IsDigit(num2))
																		{
																			num++;
																			num3 = 10 * num3 + num2 - 48;
																			if (num < source.Length)
																			{
																				num2 = (int)(*source[num]);
																				if (ParserHelpers.IsDigit(num2))
																				{
																					num++;
																					num3 = 10 * num3 + num2 - 48;
																					if (num < source.Length)
																					{
																						num2 = (int)(*source[num]);
																						if (ParserHelpers.IsDigit(num2))
																						{
																							num++;
																							if (num3 > 429496729 || (num3 == 429496729 && num2 > 53))
																							{
																								goto IL_0250;
																							}
																							num3 = num3 * 10 + num2 - 48;
																							if (num < source.Length && ParserHelpers.IsDigit((int)(*source[num])))
																							{
																								goto IL_0250;
																							}
																						}
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
					IL_0258:
					bytesConsumed = num;
					value = (uint)num3;
					return true;
				}
			}
			IL_0250:
			bytesConsumed = 0;
			value = 0U;
			return false;
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x000291F0 File Offset: 0x000273F0
		private unsafe static bool TryParseUInt64D(ReadOnlySpan<byte> source, out ulong value, out int bytesConsumed)
		{
			if (source.Length < 1)
			{
				bytesConsumed = 0;
				value = 0UL;
				return false;
			}
			ulong num = (ulong)(*source[0] - 48);
			if (num > 9UL)
			{
				bytesConsumed = 0;
				value = 0UL;
				return false;
			}
			ulong num2 = num;
			if (source.Length < 19)
			{
				for (int i = 1; i < source.Length; i++)
				{
					ulong num3 = (ulong)(*source[i] - 48);
					if (num3 > 9UL)
					{
						bytesConsumed = i;
						value = num2;
						return true;
					}
					num2 = num2 * 10UL + num3;
				}
			}
			else
			{
				for (int j = 1; j < 18; j++)
				{
					ulong num4 = (ulong)(*source[j] - 48);
					if (num4 > 9UL)
					{
						bytesConsumed = j;
						value = num2;
						return true;
					}
					num2 = num2 * 10UL + num4;
				}
				for (int k = 18; k < source.Length; k++)
				{
					ulong num5 = (ulong)(*source[k] - 48);
					if (num5 > 9UL)
					{
						bytesConsumed = k;
						value = num2;
						return true;
					}
					if (num2 > 1844674407370955161UL || (num2 == 1844674407370955161UL && num5 > 5UL))
					{
						bytesConsumed = 0;
						value = 0UL;
						return false;
					}
					num2 = num2 * 10UL + num5;
				}
			}
			bytesConsumed = source.Length;
			value = num2;
			return true;
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x0002934C File Offset: 0x0002754C
		private unsafe static bool TryParseByteN(ReadOnlySpan<byte> source, out byte value, out int bytesConsumed)
		{
			if (source.Length >= 1)
			{
				int num = 0;
				int num2 = (int)(*source[num]);
				if (num2 == 43)
				{
					num++;
					if (num >= source.Length)
					{
						goto IL_00EC;
					}
					num2 = (int)(*source[num]);
				}
				int num3;
				if (num2 != 46)
				{
					if (ParserHelpers.IsDigit(num2))
					{
						num3 = num2 - 48;
						for (;;)
						{
							num++;
							if (num >= source.Length)
							{
								goto IL_00F4;
							}
							num2 = (int)(*source[num]);
							if (num2 != 44)
							{
								if (num2 == 46)
								{
									goto IL_00C1;
								}
								if (!ParserHelpers.IsDigit(num2))
								{
									goto IL_00F4;
								}
								num3 = num3 * 10 + num2 - 48;
								if (num3 > 255)
								{
									break;
								}
							}
						}
						goto IL_00EC;
					}
					goto IL_00EC;
				}
				else
				{
					num3 = 0;
					num++;
					if (num >= source.Length || *source[num] != 48)
					{
						goto IL_00EC;
					}
				}
				do
				{
					IL_00C1:
					num++;
					if (num >= source.Length)
					{
						goto IL_00F4;
					}
					num2 = (int)(*source[num]);
				}
				while (num2 == 48);
				if (ParserHelpers.IsDigit(num2))
				{
					goto IL_00EC;
				}
				IL_00F4:
				bytesConsumed = num;
				value = (byte)num3;
				return true;
			}
			IL_00EC:
			bytesConsumed = 0;
			value = 0;
			return false;
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x0002945C File Offset: 0x0002765C
		private unsafe static bool TryParseUInt16N(ReadOnlySpan<byte> source, out ushort value, out int bytesConsumed)
		{
			if (source.Length >= 1)
			{
				int num = 0;
				int num2 = (int)(*source[num]);
				if (num2 == 43)
				{
					num++;
					if (num >= source.Length)
					{
						goto IL_00EC;
					}
					num2 = (int)(*source[num]);
				}
				int num3;
				if (num2 != 46)
				{
					if (ParserHelpers.IsDigit(num2))
					{
						num3 = num2 - 48;
						for (;;)
						{
							num++;
							if (num >= source.Length)
							{
								goto IL_00F4;
							}
							num2 = (int)(*source[num]);
							if (num2 != 44)
							{
								if (num2 == 46)
								{
									goto IL_00C1;
								}
								if (!ParserHelpers.IsDigit(num2))
								{
									goto IL_00F4;
								}
								num3 = num3 * 10 + num2 - 48;
								if (num3 > 65535)
								{
									break;
								}
							}
						}
						goto IL_00EC;
					}
					goto IL_00EC;
				}
				else
				{
					num3 = 0;
					num++;
					if (num >= source.Length || *source[num] != 48)
					{
						goto IL_00EC;
					}
				}
				do
				{
					IL_00C1:
					num++;
					if (num >= source.Length)
					{
						goto IL_00F4;
					}
					num2 = (int)(*source[num]);
				}
				while (num2 == 48);
				if (ParserHelpers.IsDigit(num2))
				{
					goto IL_00EC;
				}
				IL_00F4:
				bytesConsumed = num;
				value = (ushort)num3;
				return true;
			}
			IL_00EC:
			bytesConsumed = 0;
			value = 0;
			return false;
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x0002956C File Offset: 0x0002776C
		private unsafe static bool TryParseUInt32N(ReadOnlySpan<byte> source, out uint value, out int bytesConsumed)
		{
			if (source.Length >= 1)
			{
				int num = 0;
				int num2 = (int)(*source[num]);
				if (num2 == 43)
				{
					num++;
					if (num >= source.Length)
					{
						goto IL_00FF;
					}
					num2 = (int)(*source[num]);
				}
				int num3;
				if (num2 != 46)
				{
					if (!ParserHelpers.IsDigit(num2))
					{
						goto IL_00FF;
					}
					num3 = num2 - 48;
					for (;;)
					{
						num++;
						if (num >= source.Length)
						{
							goto IL_0107;
						}
						num2 = (int)(*source[num]);
						if (num2 != 44)
						{
							if (num2 == 46)
							{
								break;
							}
							if (!ParserHelpers.IsDigit(num2))
							{
								goto IL_0107;
							}
							if (num3 > 429496729 || (num3 == 429496729 && num2 > 53))
							{
								goto IL_00FF;
							}
							num3 = num3 * 10 + num2 - 48;
						}
					}
				}
				else
				{
					num3 = 0;
					num++;
					if (num >= source.Length || *source[num] != 48)
					{
						goto IL_00FF;
					}
				}
				do
				{
					num++;
					if (num >= source.Length)
					{
						goto IL_0107;
					}
					num2 = (int)(*source[num]);
				}
				while (num2 == 48);
				if (ParserHelpers.IsDigit(num2))
				{
					goto IL_00FF;
				}
				IL_0107:
				bytesConsumed = num;
				value = (uint)num3;
				return true;
			}
			IL_00FF:
			bytesConsumed = 0;
			value = 0U;
			return false;
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x0002968C File Offset: 0x0002788C
		private unsafe static bool TryParseUInt64N(ReadOnlySpan<byte> source, out ulong value, out int bytesConsumed)
		{
			if (source.Length >= 1)
			{
				int num = 0;
				int num2 = (int)(*source[num]);
				if (num2 == 43)
				{
					num++;
					if (num >= source.Length)
					{
						goto IL_010C;
					}
					num2 = (int)(*source[num]);
				}
				long num3;
				if (num2 != 46)
				{
					if (!ParserHelpers.IsDigit(num2))
					{
						goto IL_010C;
					}
					num3 = (long)(num2 - 48);
					for (;;)
					{
						num++;
						if (num >= source.Length)
						{
							goto IL_0115;
						}
						num2 = (int)(*source[num]);
						if (num2 != 44)
						{
							if (num2 == 46)
							{
								break;
							}
							if (!ParserHelpers.IsDigit(num2))
							{
								goto IL_0115;
							}
							if (num3 > 1844674407370955161L || (num3 == 1844674407370955161L && num2 > 53))
							{
								goto IL_010C;
							}
							num3 = num3 * 10L + (long)num2 - 48L;
						}
					}
				}
				else
				{
					num3 = 0L;
					num++;
					if (num >= source.Length || *source[num] != 48)
					{
						goto IL_010C;
					}
				}
				do
				{
					num++;
					if (num >= source.Length)
					{
						goto IL_0115;
					}
					num2 = (int)(*source[num]);
				}
				while (num2 == 48);
				if (ParserHelpers.IsDigit(num2))
				{
					goto IL_010C;
				}
				IL_0115:
				bytesConsumed = num;
				value = (ulong)num3;
				return true;
			}
			IL_010C:
			bytesConsumed = 0;
			value = 0UL;
			return false;
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x000297BC File Offset: 0x000279BC
		private unsafe static bool TryParseByteX(ReadOnlySpan<byte> source, out byte value, out int bytesConsumed)
		{
			if (source.Length < 1)
			{
				bytesConsumed = 0;
				value = 0;
				return false;
			}
			byte[] s_hexLookup = ParserHelpers.s_hexLookup;
			byte b = *source[0];
			byte b2 = s_hexLookup[(int)b];
			if (b2 == 255)
			{
				bytesConsumed = 0;
				value = 0;
				return false;
			}
			uint num = (uint)b2;
			if (source.Length <= 2)
			{
				for (int i = 1; i < source.Length; i++)
				{
					b = *source[i];
					b2 = s_hexLookup[(int)b];
					if (b2 == 255)
					{
						bytesConsumed = i;
						value = (byte)num;
						return true;
					}
					num = (num << 4) + (uint)b2;
				}
			}
			else
			{
				for (int j = 1; j < 2; j++)
				{
					b = *source[j];
					b2 = s_hexLookup[(int)b];
					if (b2 == 255)
					{
						bytesConsumed = j;
						value = (byte)num;
						return true;
					}
					num = (num << 4) + (uint)b2;
				}
				for (int k = 2; k < source.Length; k++)
				{
					b = *source[k];
					b2 = s_hexLookup[(int)b];
					if (b2 == 255)
					{
						bytesConsumed = k;
						value = (byte)num;
						return true;
					}
					if (num > 15U)
					{
						bytesConsumed = 0;
						value = 0;
						return false;
					}
					num = (num << 4) + (uint)b2;
				}
			}
			bytesConsumed = source.Length;
			value = (byte)num;
			return true;
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x00029900 File Offset: 0x00027B00
		private unsafe static bool TryParseUInt16X(ReadOnlySpan<byte> source, out ushort value, out int bytesConsumed)
		{
			if (source.Length < 1)
			{
				bytesConsumed = 0;
				value = 0;
				return false;
			}
			byte[] s_hexLookup = ParserHelpers.s_hexLookup;
			byte b = *source[0];
			byte b2 = s_hexLookup[(int)b];
			if (b2 == 255)
			{
				bytesConsumed = 0;
				value = 0;
				return false;
			}
			uint num = (uint)b2;
			if (source.Length <= 4)
			{
				for (int i = 1; i < source.Length; i++)
				{
					b = *source[i];
					b2 = s_hexLookup[(int)b];
					if (b2 == 255)
					{
						bytesConsumed = i;
						value = (ushort)num;
						return true;
					}
					num = (num << 4) + (uint)b2;
				}
			}
			else
			{
				for (int j = 1; j < 4; j++)
				{
					b = *source[j];
					b2 = s_hexLookup[(int)b];
					if (b2 == 255)
					{
						bytesConsumed = j;
						value = (ushort)num;
						return true;
					}
					num = (num << 4) + (uint)b2;
				}
				for (int k = 4; k < source.Length; k++)
				{
					b = *source[k];
					b2 = s_hexLookup[(int)b];
					if (b2 == 255)
					{
						bytesConsumed = k;
						value = (ushort)num;
						return true;
					}
					if (num > 4095U)
					{
						bytesConsumed = 0;
						value = 0;
						return false;
					}
					num = (num << 4) + (uint)b2;
				}
			}
			bytesConsumed = source.Length;
			value = (ushort)num;
			return true;
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x00029A48 File Offset: 0x00027C48
		private unsafe static bool TryParseUInt32X(ReadOnlySpan<byte> source, out uint value, out int bytesConsumed)
		{
			if (source.Length < 1)
			{
				bytesConsumed = 0;
				value = 0U;
				return false;
			}
			byte[] s_hexLookup = ParserHelpers.s_hexLookup;
			byte b = *source[0];
			byte b2 = s_hexLookup[(int)b];
			if (b2 == 255)
			{
				bytesConsumed = 0;
				value = 0U;
				return false;
			}
			uint num = (uint)b2;
			if (source.Length <= 8)
			{
				for (int i = 1; i < source.Length; i++)
				{
					b = *source[i];
					b2 = s_hexLookup[(int)b];
					if (b2 == 255)
					{
						bytesConsumed = i;
						value = num;
						return true;
					}
					num = (num << 4) + (uint)b2;
				}
			}
			else
			{
				for (int j = 1; j < 8; j++)
				{
					b = *source[j];
					b2 = s_hexLookup[(int)b];
					if (b2 == 255)
					{
						bytesConsumed = j;
						value = num;
						return true;
					}
					num = (num << 4) + (uint)b2;
				}
				for (int k = 8; k < source.Length; k++)
				{
					b = *source[k];
					b2 = s_hexLookup[(int)b];
					if (b2 == 255)
					{
						bytesConsumed = k;
						value = num;
						return true;
					}
					if (num > 268435455U)
					{
						bytesConsumed = 0;
						value = 0U;
						return false;
					}
					num = (num << 4) + (uint)b2;
				}
			}
			bytesConsumed = source.Length;
			value = num;
			return true;
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x00029B8C File Offset: 0x00027D8C
		private unsafe static bool TryParseUInt64X(ReadOnlySpan<byte> source, out ulong value, out int bytesConsumed)
		{
			if (source.Length < 1)
			{
				bytesConsumed = 0;
				value = 0UL;
				return false;
			}
			byte[] s_hexLookup = ParserHelpers.s_hexLookup;
			byte b = *source[0];
			byte b2 = s_hexLookup[(int)b];
			if (b2 == 255)
			{
				bytesConsumed = 0;
				value = 0UL;
				return false;
			}
			ulong num = (ulong)b2;
			if (source.Length <= 16)
			{
				for (int i = 1; i < source.Length; i++)
				{
					b = *source[i];
					b2 = s_hexLookup[(int)b];
					if (b2 == 255)
					{
						bytesConsumed = i;
						value = num;
						return true;
					}
					num = (num << 4) + (ulong)b2;
				}
			}
			else
			{
				for (int j = 1; j < 16; j++)
				{
					b = *source[j];
					b2 = s_hexLookup[(int)b];
					if (b2 == 255)
					{
						bytesConsumed = j;
						value = num;
						return true;
					}
					num = (num << 4) + (ulong)b2;
				}
				for (int k = 16; k < source.Length; k++)
				{
					b = *source[k];
					b2 = s_hexLookup[(int)b];
					if (b2 == 255)
					{
						bytesConsumed = k;
						value = num;
						return true;
					}
					if (num > 1152921504606846975UL)
					{
						bytesConsumed = 0;
						value = 0UL;
						return false;
					}
					num = (num << 4) + (ulong)b2;
				}
			}
			bytesConsumed = source.Length;
			value = num;
			return true;
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x00029CDC File Offset: 0x00027EDC
		private unsafe static bool TryParseNumber(ReadOnlySpan<byte> source, ref NumberBuffer number, out int bytesConsumed, Utf8Parser.ParseNumberOptions options, out bool textUsedExponentNotation)
		{
			textUsedExponentNotation = false;
			if (source.Length == 0)
			{
				bytesConsumed = 0;
				return false;
			}
			Span<byte> digits = number.Digits;
			int num = 0;
			byte b = *source[num];
			if (b != 43)
			{
				if (b != 45)
				{
					goto IL_0061;
				}
				number.IsNegative = true;
			}
			num++;
			if (num == source.Length)
			{
				bytesConsumed = 0;
				return false;
			}
			b = *source[num];
			IL_0061:
			int num2 = num;
			while (num != source.Length)
			{
				b = *source[num];
				if (b != 48)
				{
					break;
				}
				num++;
			}
			if (num == source.Length)
			{
				*digits[0] = 0;
				number.Scale = 0;
				bytesConsumed = num;
				return true;
			}
			int num3 = num;
			while (num != source.Length)
			{
				b = *source[num];
				if (b - 48 > 9)
				{
					break;
				}
				num++;
			}
			int num4 = num - num2;
			int num5 = num - num3;
			int num6 = Math.Min(num5, 50);
			source.Slice(num3, num6).CopyTo(digits);
			int num7 = num6;
			number.Scale = num5;
			if (num == source.Length)
			{
				bytesConsumed = num;
				return true;
			}
			int num8 = 0;
			if (b == 46)
			{
				num++;
				int num9 = num;
				while (num != source.Length)
				{
					b = *source[num];
					if (b - 48 > 9)
					{
						break;
					}
					num++;
				}
				num8 = num - num9;
				int num10 = num9;
				if (num7 == 0)
				{
					while (num10 < num && *source[num10] == 48)
					{
						number.Scale--;
						num10++;
					}
				}
				int num11 = Math.Min(num - num10, 51 - num7 - 1);
				source.Slice(num10, num11).CopyTo(digits.Slice(num7));
				num7 += num11;
				if (num == source.Length)
				{
					if (num4 == 0 && num8 == 0)
					{
						bytesConsumed = 0;
						return false;
					}
					bytesConsumed = num;
					return true;
				}
			}
			if (num4 == 0 && num8 == 0)
			{
				bytesConsumed = 0;
				return false;
			}
			if (((int)b & -33) != 69)
			{
				bytesConsumed = num;
				return true;
			}
			textUsedExponentNotation = true;
			num++;
			if ((options & Utf8Parser.ParseNumberOptions.AllowExponent) == (Utf8Parser.ParseNumberOptions)0)
			{
				bytesConsumed = 0;
				return false;
			}
			if (num == source.Length)
			{
				bytesConsumed = 0;
				return false;
			}
			bool flag = false;
			b = *source[num];
			if (b != 43)
			{
				if (b != 45)
				{
					goto IL_0277;
				}
				flag = true;
			}
			num++;
			if (num == source.Length)
			{
				bytesConsumed = 0;
				return false;
			}
			b = *source[num];
			IL_0277:
			uint num12;
			int num13;
			if (!Utf8Parser.TryParseUInt32D(source.Slice(num), out num12, out num13))
			{
				bytesConsumed = 0;
				return false;
			}
			num += num13;
			if (flag)
			{
				if ((long)number.Scale < (long)(18446744071562067968UL + (ulong)num12))
				{
					number.Scale = int.MinValue;
				}
				else
				{
					number.Scale -= (int)num12;
				}
			}
			else
			{
				if ((long)number.Scale > (long)(2147483647UL - (ulong)num12))
				{
					bytesConsumed = 0;
					return false;
				}
				number.Scale += (int)num12;
			}
			bytesConsumed = num;
			return true;
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x00029FF0 File Offset: 0x000281F0
		private unsafe static bool TryParseTimeSpanBigG(ReadOnlySpan<byte> source, out TimeSpan value, out int bytesConsumed)
		{
			int num = 0;
			byte b = 0;
			while (num != source.Length)
			{
				b = *source[num];
				if (b != 32 && b != 9)
				{
					break;
				}
				num++;
			}
			if (num == source.Length)
			{
				value = default(TimeSpan);
				bytesConsumed = 0;
				return false;
			}
			bool flag = false;
			if (b == 45)
			{
				flag = true;
				num++;
				if (num == source.Length)
				{
					value = default(TimeSpan);
					bytesConsumed = 0;
					return false;
				}
			}
			uint num2;
			int num3;
			if (!Utf8Parser.TryParseUInt32D(source.Slice(num), out num2, out num3))
			{
				value = default(TimeSpan);
				bytesConsumed = 0;
				return false;
			}
			num += num3;
			if (num == source.Length || *source[num++] != 58)
			{
				value = default(TimeSpan);
				bytesConsumed = 0;
				return false;
			}
			uint num4;
			if (!Utf8Parser.TryParseUInt32D(source.Slice(num), out num4, out num3))
			{
				value = default(TimeSpan);
				bytesConsumed = 0;
				return false;
			}
			num += num3;
			if (num == source.Length || *source[num++] != 58)
			{
				value = default(TimeSpan);
				bytesConsumed = 0;
				return false;
			}
			uint num5;
			if (!Utf8Parser.TryParseUInt32D(source.Slice(num), out num5, out num3))
			{
				value = default(TimeSpan);
				bytesConsumed = 0;
				return false;
			}
			num += num3;
			if (num == source.Length || *source[num++] != 58)
			{
				value = default(TimeSpan);
				bytesConsumed = 0;
				return false;
			}
			uint num6;
			if (!Utf8Parser.TryParseUInt32D(source.Slice(num), out num6, out num3))
			{
				value = default(TimeSpan);
				bytesConsumed = 0;
				return false;
			}
			num += num3;
			if (num == source.Length || *source[num++] != 46)
			{
				value = default(TimeSpan);
				bytesConsumed = 0;
				return false;
			}
			uint num7;
			if (!Utf8Parser.TryParseTimeSpanFraction(source.Slice(num), out num7, out num3))
			{
				value = default(TimeSpan);
				bytesConsumed = 0;
				return false;
			}
			num += num3;
			if (!Utf8Parser.TryCreateTimeSpan(flag, num2, num4, num5, num6, num7, out value))
			{
				value = default(TimeSpan);
				bytesConsumed = 0;
				return false;
			}
			if (num != source.Length && (*source[num] == 46 || *source[num] == 58))
			{
				value = default(TimeSpan);
				bytesConsumed = 0;
				return false;
			}
			bytesConsumed = num;
			return true;
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0002A24C File Offset: 0x0002844C
		private static bool TryParseTimeSpanC(ReadOnlySpan<byte> source, out TimeSpan value, out int bytesConsumed)
		{
			Utf8Parser.TimeSpanSplitter timeSpanSplitter = default(Utf8Parser.TimeSpanSplitter);
			if (!timeSpanSplitter.TrySplitTimeSpan(source, true, out bytesConsumed))
			{
				value = default(TimeSpan);
				return false;
			}
			bool isNegative = timeSpanSplitter.IsNegative;
			uint separators = timeSpanSplitter.Separators;
			bool flag;
			if (separators <= 16842752U)
			{
				if (separators == 0U)
				{
					flag = Utf8Parser.TryCreateTimeSpan(isNegative, timeSpanSplitter.V1, 0U, 0U, 0U, 0U, out value);
					goto IL_0190;
				}
				if (separators == 16777216U)
				{
					flag = Utf8Parser.TryCreateTimeSpan(isNegative, 0U, timeSpanSplitter.V1, timeSpanSplitter.V2, 0U, 0U, out value);
					goto IL_0190;
				}
				if (separators == 16842752U)
				{
					flag = Utf8Parser.TryCreateTimeSpan(isNegative, 0U, timeSpanSplitter.V1, timeSpanSplitter.V2, timeSpanSplitter.V3, 0U, out value);
					goto IL_0190;
				}
			}
			else if (separators <= 33619968U)
			{
				if (separators == 16843264U)
				{
					flag = Utf8Parser.TryCreateTimeSpan(isNegative, 0U, timeSpanSplitter.V1, timeSpanSplitter.V2, timeSpanSplitter.V3, timeSpanSplitter.V4, out value);
					goto IL_0190;
				}
				if (separators == 33619968U)
				{
					flag = Utf8Parser.TryCreateTimeSpan(isNegative, timeSpanSplitter.V1, timeSpanSplitter.V2, timeSpanSplitter.V3, 0U, 0U, out value);
					goto IL_0190;
				}
			}
			else
			{
				if (separators == 33620224U)
				{
					flag = Utf8Parser.TryCreateTimeSpan(isNegative, timeSpanSplitter.V1, timeSpanSplitter.V2, timeSpanSplitter.V3, timeSpanSplitter.V4, 0U, out value);
					goto IL_0190;
				}
				if (separators == 33620226U)
				{
					flag = Utf8Parser.TryCreateTimeSpan(isNegative, timeSpanSplitter.V1, timeSpanSplitter.V2, timeSpanSplitter.V3, timeSpanSplitter.V4, timeSpanSplitter.V5, out value);
					goto IL_0190;
				}
			}
			value = default(TimeSpan);
			flag = false;
			IL_0190:
			if (!flag)
			{
				bytesConsumed = 0;
				return false;
			}
			return true;
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x0002A3FC File Offset: 0x000285FC
		public static bool TryParse(ReadOnlySpan<byte> source, out TimeSpan value, out int bytesConsumed, char standardFormat = '\0')
		{
			if (standardFormat <= 'T')
			{
				if (standardFormat != '\0')
				{
					if (standardFormat == 'G')
					{
						return Utf8Parser.TryParseTimeSpanBigG(source, out value, out bytesConsumed);
					}
					if (standardFormat != 'T')
					{
						goto IL_0056;
					}
				}
			}
			else if (standardFormat != 'c')
			{
				if (standardFormat == 'g')
				{
					return Utf8Parser.TryParseTimeSpanLittleG(source, out value, out bytesConsumed);
				}
				if (standardFormat != 't')
				{
					goto IL_0056;
				}
			}
			return Utf8Parser.TryParseTimeSpanC(source, out value, out bytesConsumed);
			IL_0056:
			return ThrowHelper.TryParseThrowFormatException<TimeSpan>(out value, out bytesConsumed);
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x0002A46C File Offset: 0x0002866C
		private unsafe static bool TryParseTimeSpanFraction(ReadOnlySpan<byte> source, out uint value, out int bytesConsumed)
		{
			int num = 0;
			if (num == source.Length)
			{
				value = 0U;
				bytesConsumed = 0;
				return false;
			}
			uint num2 = (uint)(*source[num] - 48);
			if (num2 > 9U)
			{
				value = 0U;
				bytesConsumed = 0;
				return false;
			}
			num++;
			uint num3 = num2;
			int num4 = 1;
			while (num != source.Length)
			{
				num2 = (uint)(*source[num] - 48);
				if (num2 > 9U)
				{
					break;
				}
				num++;
				num4++;
				if (num4 > 7)
				{
					value = 0U;
					bytesConsumed = 0;
					return false;
				}
				num3 = 10U * num3 + num2;
			}
			switch (num4)
			{
			case 2:
				num3 *= 100000U;
				break;
			case 3:
				num3 *= 10000U;
				break;
			case 4:
				num3 *= 1000U;
				break;
			case 5:
				num3 *= 100U;
				break;
			case 6:
				num3 *= 10U;
				break;
			case 7:
				break;
			default:
				num3 *= 1000000U;
				break;
			}
			value = num3;
			bytesConsumed = num;
			return true;
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x0002A56C File Offset: 0x0002876C
		private static bool TryCreateTimeSpan(bool isNegative, uint days, uint hours, uint minutes, uint seconds, uint fraction, out TimeSpan timeSpan)
		{
			if (hours > 23U || minutes > 59U || seconds > 59U)
			{
				timeSpan = default(TimeSpan);
				return false;
			}
			long num = (long)(((ulong)days * 3600UL * 24UL + (ulong)hours * 3600UL + (ulong)minutes * 60UL + (ulong)seconds) * 1000UL);
			long num3;
			if (isNegative)
			{
				num = -num;
				if (num < -922337203685477L)
				{
					timeSpan = default(TimeSpan);
					return false;
				}
				long num2 = num * 10000L;
				if (num2 < (long)(9223372036854775808UL + (ulong)fraction))
				{
					timeSpan = default(TimeSpan);
					return false;
				}
				num3 = num2 - (long)((ulong)fraction);
			}
			else
			{
				if (num > 922337203685477L)
				{
					timeSpan = default(TimeSpan);
					return false;
				}
				long num4 = num * 10000L;
				if (num4 > (long)(9223372036854775807UL - (ulong)fraction))
				{
					timeSpan = default(TimeSpan);
					return false;
				}
				num3 = num4 + (long)((ulong)fraction);
			}
			timeSpan = new TimeSpan(num3);
			return true;
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x0002A670 File Offset: 0x00028870
		private static bool TryParseTimeSpanLittleG(ReadOnlySpan<byte> source, out TimeSpan value, out int bytesConsumed)
		{
			Utf8Parser.TimeSpanSplitter timeSpanSplitter = default(Utf8Parser.TimeSpanSplitter);
			if (!timeSpanSplitter.TrySplitTimeSpan(source, false, out bytesConsumed))
			{
				value = default(TimeSpan);
				return false;
			}
			bool isNegative = timeSpanSplitter.IsNegative;
			uint separators = timeSpanSplitter.Separators;
			bool flag;
			if (separators <= 16842752U)
			{
				if (separators == 0U)
				{
					flag = Utf8Parser.TryCreateTimeSpan(isNegative, timeSpanSplitter.V1, 0U, 0U, 0U, 0U, out value);
					goto IL_0154;
				}
				if (separators == 16777216U)
				{
					flag = Utf8Parser.TryCreateTimeSpan(isNegative, 0U, timeSpanSplitter.V1, timeSpanSplitter.V2, 0U, 0U, out value);
					goto IL_0154;
				}
				if (separators == 16842752U)
				{
					flag = Utf8Parser.TryCreateTimeSpan(isNegative, 0U, timeSpanSplitter.V1, timeSpanSplitter.V2, timeSpanSplitter.V3, 0U, out value);
					goto IL_0154;
				}
			}
			else
			{
				if (separators == 16843008U)
				{
					flag = Utf8Parser.TryCreateTimeSpan(isNegative, timeSpanSplitter.V1, timeSpanSplitter.V2, timeSpanSplitter.V3, timeSpanSplitter.V4, 0U, out value);
					goto IL_0154;
				}
				if (separators == 16843010U)
				{
					flag = Utf8Parser.TryCreateTimeSpan(isNegative, timeSpanSplitter.V1, timeSpanSplitter.V2, timeSpanSplitter.V3, timeSpanSplitter.V4, timeSpanSplitter.V5, out value);
					goto IL_0154;
				}
				if (separators == 16843264U)
				{
					flag = Utf8Parser.TryCreateTimeSpan(isNegative, 0U, timeSpanSplitter.V1, timeSpanSplitter.V2, timeSpanSplitter.V3, timeSpanSplitter.V4, out value);
					goto IL_0154;
				}
			}
			value = default(TimeSpan);
			flag = false;
			IL_0154:
			if (!flag)
			{
				bytesConsumed = 0;
				return false;
			}
			return true;
		}

		// Token: 0x0400029D RID: 669
		private const uint FlipCase = 32U;

		// Token: 0x0400029E RID: 670
		private const uint NoFlipCase = 0U;

		// Token: 0x0400029F RID: 671
		private static readonly int[] s_daysToMonth365 = new int[]
		{
			0, 31, 59, 90, 120, 151, 181, 212, 243, 273,
			304, 334, 365
		};

		// Token: 0x040002A0 RID: 672
		private static readonly int[] s_daysToMonth366 = new int[]
		{
			0, 31, 60, 91, 121, 152, 182, 213, 244, 274,
			305, 335, 366
		};

		// Token: 0x02000152 RID: 338
		[Flags]
		private enum ParseNumberOptions
		{
			// Token: 0x04000350 RID: 848
			AllowExponent = 1
		}

		// Token: 0x02000153 RID: 339
		private enum ComponentParseResult : byte
		{
			// Token: 0x04000352 RID: 850
			NoMoreData,
			// Token: 0x04000353 RID: 851
			Colon,
			// Token: 0x04000354 RID: 852
			Period,
			// Token: 0x04000355 RID: 853
			ParseFailure
		}

		// Token: 0x02000154 RID: 340
		private struct TimeSpanSplitter
		{
			// Token: 0x06000A2B RID: 2603 RVA: 0x0002C740 File Offset: 0x0002A940
			public unsafe bool TrySplitTimeSpan(ReadOnlySpan<byte> source, bool periodUsedToSeparateDay, out int bytesConsumed)
			{
				int num = 0;
				byte b = 0;
				while (num != source.Length)
				{
					b = *source[num];
					if (b != 32 && b != 9)
					{
						break;
					}
					num++;
				}
				if (num == source.Length)
				{
					bytesConsumed = 0;
					return false;
				}
				if (b == 45)
				{
					this.IsNegative = true;
					num++;
					if (num == source.Length)
					{
						bytesConsumed = 0;
						return false;
					}
				}
				int num2;
				if (!Utf8Parser.TryParseUInt32D(source.Slice(num), out this.V1, out num2))
				{
					bytesConsumed = 0;
					return false;
				}
				num += num2;
				Utf8Parser.ComponentParseResult componentParseResult = Utf8Parser.TimeSpanSplitter.ParseComponent(source, periodUsedToSeparateDay, ref num, out this.V2);
				if (componentParseResult == Utf8Parser.ComponentParseResult.ParseFailure)
				{
					bytesConsumed = 0;
					return false;
				}
				if (componentParseResult == Utf8Parser.ComponentParseResult.NoMoreData)
				{
					bytesConsumed = num;
					return true;
				}
				this.Separators |= (uint)((uint)componentParseResult << 24);
				componentParseResult = Utf8Parser.TimeSpanSplitter.ParseComponent(source, false, ref num, out this.V3);
				if (componentParseResult == Utf8Parser.ComponentParseResult.ParseFailure)
				{
					bytesConsumed = 0;
					return false;
				}
				if (componentParseResult == Utf8Parser.ComponentParseResult.NoMoreData)
				{
					bytesConsumed = num;
					return true;
				}
				this.Separators |= (uint)((uint)componentParseResult << 16);
				componentParseResult = Utf8Parser.TimeSpanSplitter.ParseComponent(source, false, ref num, out this.V4);
				if (componentParseResult == Utf8Parser.ComponentParseResult.ParseFailure)
				{
					bytesConsumed = 0;
					return false;
				}
				if (componentParseResult == Utf8Parser.ComponentParseResult.NoMoreData)
				{
					bytesConsumed = num;
					return true;
				}
				this.Separators |= (uint)((uint)componentParseResult << 8);
				componentParseResult = Utf8Parser.TimeSpanSplitter.ParseComponent(source, false, ref num, out this.V5);
				if (componentParseResult == Utf8Parser.ComponentParseResult.ParseFailure)
				{
					bytesConsumed = 0;
					return false;
				}
				if (componentParseResult == Utf8Parser.ComponentParseResult.NoMoreData)
				{
					bytesConsumed = num;
					return true;
				}
				this.Separators |= (uint)componentParseResult;
				if (num != source.Length && (*source[num] == 46 || *source[num] == 58))
				{
					bytesConsumed = 0;
					return false;
				}
				bytesConsumed = num;
				return true;
			}

			// Token: 0x06000A2C RID: 2604 RVA: 0x0002C8F0 File Offset: 0x0002AAF0
			private unsafe static Utf8Parser.ComponentParseResult ParseComponent(ReadOnlySpan<byte> source, bool neverParseAsFraction, ref int srcIndex, out uint value)
			{
				if (srcIndex == source.Length)
				{
					value = 0U;
					return Utf8Parser.ComponentParseResult.NoMoreData;
				}
				byte b = *source[srcIndex];
				if (b == 58 || (b == 46 && neverParseAsFraction))
				{
					srcIndex++;
					int num;
					if (!Utf8Parser.TryParseUInt32D(source.Slice(srcIndex), out value, out num))
					{
						value = 0U;
						return Utf8Parser.ComponentParseResult.ParseFailure;
					}
					srcIndex += num;
					if (b != 58)
					{
						return Utf8Parser.ComponentParseResult.Period;
					}
					return Utf8Parser.ComponentParseResult.Colon;
				}
				else
				{
					if (b != 46)
					{
						value = 0U;
						return Utf8Parser.ComponentParseResult.NoMoreData;
					}
					srcIndex++;
					int num2;
					if (!Utf8Parser.TryParseTimeSpanFraction(source.Slice(srcIndex), out value, out num2))
					{
						value = 0U;
						return Utf8Parser.ComponentParseResult.ParseFailure;
					}
					srcIndex += num2;
					return Utf8Parser.ComponentParseResult.Period;
				}
			}

			// Token: 0x04000356 RID: 854
			public uint V1;

			// Token: 0x04000357 RID: 855
			public uint V2;

			// Token: 0x04000358 RID: 856
			public uint V3;

			// Token: 0x04000359 RID: 857
			public uint V4;

			// Token: 0x0400035A RID: 858
			public uint V5;

			// Token: 0x0400035B RID: 859
			public bool IsNegative;

			// Token: 0x0400035C RID: 860
			public uint Separators;
		}
	}
}
