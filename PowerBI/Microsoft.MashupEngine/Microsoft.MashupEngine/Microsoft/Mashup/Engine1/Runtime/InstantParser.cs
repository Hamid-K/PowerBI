using System;
using System.Globalization;
using System.Linq;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200134C RID: 4940
	public static class InstantParser
	{
		// Token: 0x060081FA RID: 33274 RVA: 0x001B97F0 File Offset: 0x001B79F0
		private static bool TryParse<TClr, TValue>(InstantParser.ParseFunc<TClr> parser, Func<TClr, TValue> constructor, string text, string format, IFormatProvider formatProvider, out TValue instant) where TValue : Value
		{
			bool flag;
			try
			{
				TClr tclr;
				if (parser(text, format, formatProvider, DateTimeStyles.AssumeLocal, out tclr))
				{
					instant = constructor(tclr);
					flag = true;
				}
				else
				{
					instant = default(TValue);
					flag = false;
				}
			}
			catch (ArgumentException)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.FormatValueUnknownFailure, null, null);
			}
			return flag;
		}

		// Token: 0x060081FB RID: 33275 RVA: 0x001B984C File Offset: 0x001B7A4C
		public static bool TryParseDate(string text, string format, CultureInfo formatProvider, out DateValue date)
		{
			return InstantParser.TryParse<DateTime, DateValue>(new InstantParser.ParseFunc<DateTime>(DateTime.TryParseExact), new Func<DateTime, DateValue>(DateValue.New), text, format, formatProvider, out date);
		}

		// Token: 0x060081FC RID: 33276 RVA: 0x001B986F File Offset: 0x001B7A6F
		public static bool TryParseDateTime(string text, string format, IFormatProvider formatProvider, out DateTimeValue dateTime)
		{
			return InstantParser.TryParse<DateTime, DateTimeValue>(new InstantParser.ParseFunc<DateTime>(DateTime.TryParseExact), new Func<DateTime, DateTimeValue>(DateTimeValue.New), text, format, formatProvider, out dateTime);
		}

		// Token: 0x060081FD RID: 33277 RVA: 0x001B9892 File Offset: 0x001B7A92
		public static bool TryParseDateTimeZone(string text, string format, CultureInfo formatProvider, out DateTimeZoneValue dateTimeZone)
		{
			return InstantParser.TryParse<DateTimeOffset, DateTimeZoneValue>(new InstantParser.ParseFunc<DateTimeOffset>(DateTimeOffset.TryParseExact), new Func<DateTimeOffset, DateTimeZoneValue>(DateTimeZoneValue.New), text, format, formatProvider, out dateTimeZone);
		}

		// Token: 0x060081FE RID: 33278 RVA: 0x001B98B5 File Offset: 0x001B7AB5
		public static bool TryParseTime(string text, string format, CultureInfo formatProvider, out TimeValue dateTimeZone)
		{
			return InstantParser.TryParse<DateTime, TimeValue>(new InstantParser.ParseFunc<DateTime>(DateTime.TryParseExact), new Func<DateTime, TimeValue>(TimeValue.New), text, format, formatProvider, out dateTimeZone);
		}

		// Token: 0x060081FF RID: 33279 RVA: 0x001B98D8 File Offset: 0x001B7AD8
		public static bool TryParseTime(string text, CultureInfo formatProvider, out TimeValue time)
		{
			bool flag;
			try
			{
				DateTime dateTime;
				if (IsoDateTimeParser.TryParseTime(text, out dateTime))
				{
					time = TimeValue.New(dateTime.TimeOfDay);
					flag = true;
				}
				else
				{
					string[] array = new string[]
					{
						formatProvider.DateTimeFormat.LongTimePattern,
						formatProvider.DateTimeFormat.ShortTimePattern
					};
					if (DateTime.TryParseExact(text, array, formatProvider, DateTimeStyles.NoCurrentDateDefault | DateTimeStyles.AssumeLocal, out dateTime))
					{
						time = TimeValue.New(dateTime.TimeOfDay);
						flag = true;
					}
					else
					{
						if (DateTime.TryParse(text, formatProvider, DateTimeStyles.NoCurrentDateDefault, out dateTime) && dateTime.Year == 1 && dateTime.Month == 1 && dateTime.Day == 1)
						{
							string text2 = text.ToLower(formatProvider);
							DateTimeFormatInfo dateTimeFormat = formatProvider.DateTimeFormat;
							NumberFormatInfo numberFormat = formatProvider.NumberFormat;
							if (!text2.Contains(dateTimeFormat.DateSeparator) && !text2.Contains('t') && !text2.StartsWith(numberFormat.NegativeSign, StringComparison.OrdinalIgnoreCase) && !text2.StartsWith(numberFormat.PositiveSign, StringComparison.OrdinalIgnoreCase))
							{
								time = TimeValue.New(dateTime.TimeOfDay);
								return true;
							}
						}
						time = null;
						flag = false;
					}
				}
			}
			catch (NotSupportedException)
			{
				throw ValueException.NewExpressionError<Message1>(Strings.Culture_NotSupported_Parsing(formatProvider.Name), null, null);
			}
			return flag;
		}

		// Token: 0x06008200 RID: 33280 RVA: 0x001B9A08 File Offset: 0x001B7C08
		public static bool TryParseDate(string text, CultureInfo formatProvider, out DateValue date)
		{
			bool flag;
			try
			{
				DateTime dateTime;
				if (IsoDateTimeParser.TryParseDate(text, out dateTime))
				{
					date = DateValue.New(dateTime.Ticks);
					flag = true;
				}
				else
				{
					string[] array = new string[]
					{
						formatProvider.DateTimeFormat.LongDatePattern,
						formatProvider.DateTimeFormat.ShortDatePattern
					};
					if (DateTime.TryParseExact(text, array, formatProvider, DateTimeStyles.NoCurrentDateDefault | DateTimeStyles.AssumeLocal, out dateTime))
					{
						date = DateValue.New(dateTime.Ticks);
						flag = true;
					}
					else
					{
						if (DateTime.TryParse(text, formatProvider, DateTimeStyles.None, out dateTime) && dateTime.Hour == 0 && dateTime.Minute == 0)
						{
							string text2 = text.ToLower(formatProvider);
							DateTimeFormatInfo dateTimeFormat = formatProvider.DateTimeFormat;
							string text3 = dateTimeFormat.AMDesignator.ToLower(formatProvider);
							string text4 = dateTimeFormat.PMDesignator.ToLower(formatProvider);
							if (!text2.Contains(dateTimeFormat.TimeSeparator) && (text3.Length == 0 || !text2.Contains(text3)) && (text4.Length == 0 || !text2.Contains(text4)))
							{
								date = DateValue.New(dateTime.Ticks);
								return true;
							}
						}
						date = null;
						flag = false;
					}
				}
			}
			catch (NotSupportedException)
			{
				throw ValueException.NewExpressionError<Message1>(Strings.Culture_NotSupported_Parsing(formatProvider.Name), null, null);
			}
			return flag;
		}

		// Token: 0x06008201 RID: 33281 RVA: 0x001B9B48 File Offset: 0x001B7D48
		public static bool TryParseDateTime(string text, CultureInfo formatProvider, out DateTimeValue dateTime)
		{
			bool flag;
			try
			{
				DateTime dateTime2;
				if (IsoDateTimeParser.TryParseDateTime(text, out dateTime2))
				{
					dateTime = DateTimeValue.New(dateTime2);
					flag = true;
				}
				else
				{
					string[] array = new string[]
					{
						formatProvider.DateTimeFormat.FullDateTimePattern,
						formatProvider.DateTimeFormat.SortableDateTimePattern
					};
					DateTimeOffset dateTimeOffset;
					if (DateTime.TryParseExact(text, array, formatProvider, DateTimeStyles.NoCurrentDateDefault | DateTimeStyles.AssumeLocal, out dateTime2))
					{
						dateTime = DateTimeValue.New(dateTime2);
						flag = true;
					}
					else if (DateTime.TryParse(text, formatProvider, DateTimeStyles.None, out dateTime2) && (dateTime2.Kind == DateTimeKind.Unspecified || (DateTimeOffset.TryParse(text, formatProvider, DateTimeStyles.AssumeUniversal, out dateTimeOffset) && dateTimeOffset.Offset == new TimeSpan(0, 0, 0))))
					{
						dateTime = DateTimeValue.New(dateTime2);
						flag = true;
					}
					else
					{
						dateTime = null;
						flag = false;
					}
				}
			}
			catch (NotSupportedException)
			{
				throw ValueException.NewExpressionError<Message1>(Strings.Culture_NotSupported_Parsing(formatProvider.Name), null, null);
			}
			return flag;
		}

		// Token: 0x06008202 RID: 33282 RVA: 0x001B9C1C File Offset: 0x001B7E1C
		public static bool TryParseDateTimeOffset(string text, CultureInfo formatProvider, out DateTimeZoneValue dateTimeOffsetValue)
		{
			bool flag;
			try
			{
				DateTimeOffset dateTimeOffset;
				if (IsoDateTimeParser.TryParseDateTimeOffset(text, out dateTimeOffset))
				{
					dateTimeOffsetValue = DateTimeZoneValue.New(dateTimeOffset);
					flag = true;
				}
				else
				{
					string[] array = new string[]
					{
						formatProvider.DateTimeFormat.UniversalSortableDateTimePattern,
						formatProvider.DateTimeFormat.RFC1123Pattern
					};
					if (DateTimeOffset.TryParseExact(text, array, formatProvider, DateTimeStyles.None, out dateTimeOffset))
					{
						dateTimeOffsetValue = DateTimeZoneValue.New(dateTimeOffset);
						flag = true;
					}
					else if (DateTimeOffset.TryParse(text, formatProvider, DateTimeStyles.None, out dateTimeOffset))
					{
						dateTimeOffsetValue = DateTimeZoneValue.New(dateTimeOffset);
						flag = true;
					}
					else
					{
						dateTimeOffsetValue = null;
						flag = false;
					}
				}
			}
			catch (NotSupportedException)
			{
				throw ValueException.NewExpressionError<Message1>(Strings.Culture_NotSupported_Parsing(formatProvider.Name), null, null);
			}
			return flag;
		}

		// Token: 0x040046D0 RID: 18128
		private const DateTimeStyles DefaultStyles = DateTimeStyles.NoCurrentDateDefault | DateTimeStyles.AssumeLocal;

		// Token: 0x0200134D RID: 4941
		// (Invoke) Token: 0x06008204 RID: 33284
		private delegate bool ParseFunc<T>(string input, string format, IFormatProvider formatProvider, DateTimeStyles styles, out T result);
	}
}
