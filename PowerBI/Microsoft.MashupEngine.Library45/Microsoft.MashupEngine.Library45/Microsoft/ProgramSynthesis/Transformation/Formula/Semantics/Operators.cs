using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.DslLibrary.Numbers;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics
{
	// Token: 0x020015F8 RID: 5624
	public static class Operators
	{
		// Token: 0x0600BAD4 RID: 47828 RVA: 0x00056398 File Offset: 0x00054598
		public static string Concat(string subject1, string subject2)
		{
			return subject1 + subject2;
		}

		// Token: 0x0600BAD5 RID: 47829 RVA: 0x00283356 File Offset: 0x00281556
		public static string LowerCase(string subject)
		{
			return subject.ToLowerInvariant();
		}

		// Token: 0x0600BAD6 RID: 47830 RVA: 0x0028335E File Offset: 0x0028155E
		public static string ProperCase(string subject)
		{
			return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(subject.ToLowerInvariant());
		}

		// Token: 0x0600BAD7 RID: 47831 RVA: 0x00283375 File Offset: 0x00281575
		public static string UpperCase(string subject)
		{
			return subject.ToUpperInvariant();
		}

		// Token: 0x0600BAD8 RID: 47832 RVA: 0x00283380 File Offset: 0x00281580
		public static int? Abs(string subject, int position)
		{
			if (position != 0)
			{
				return new int?((position >= 0) ? position : (subject.Length + position + 1));
			}
			return null;
		}

		// Token: 0x0600BAD9 RID: 47833 RVA: 0x002833B0 File Offset: 0x002815B0
		public static int? Find(string subject, string delimiter, int instance, int findOffsetK)
		{
			if (string.IsNullOrEmpty(subject) || instance == 0 || delimiter.Length != 1)
			{
				return null;
			}
			IEnumerable<int> enumerable = subject.AllIndexesOf(delimiter, StringComparison.Ordinal);
			if (instance < 0)
			{
				enumerable = enumerable.Reverse<int>();
				instance = -instance;
			}
			Optional<int> optional = enumerable.MaybeElementAt(instance - 1);
			if (!optional.HasValue)
			{
				return null;
			}
			return new int?(optional.Value + 1 + findOffsetK);
		}

		// Token: 0x0600BADA RID: 47834 RVA: 0x00283420 File Offset: 0x00281620
		public static decimal Length(string subject)
		{
			return Convert.ToDecimal(subject.Length);
		}

		// Token: 0x0600BADB RID: 47835 RVA: 0x0028342D File Offset: 0x0028162D
		public static string Slice(string subject, int startPosition, int endPosition)
		{
			if (startPosition <= 0 || endPosition <= 0 || startPosition >= endPosition || startPosition > subject.Length)
			{
				return null;
			}
			return subject.Slice(new int?(startPosition - 1), new int?(endPosition - 1), 1);
		}

		// Token: 0x0600BADC RID: 47836 RVA: 0x00283460 File Offset: 0x00281660
		public static string SliceBetween(string subject, string startText, string endText)
		{
			if (subject == null)
			{
				return null;
			}
			int num = subject.IndexOf(startText, StringComparison.Ordinal);
			if (num < 0)
			{
				return null;
			}
			num += startText.Length;
			int num2 = subject.IndexOf(endText, num, StringComparison.Ordinal);
			return subject.Slice(new int?(num), new int?(num2), 1);
		}

		// Token: 0x0600BADD RID: 47837 RVA: 0x002834A7 File Offset: 0x002816A7
		public static string SlicePrefix(string subject, int position)
		{
			return Operators.Slice(subject, 1, position);
		}

		// Token: 0x0600BADE RID: 47838 RVA: 0x002834B1 File Offset: 0x002816B1
		public static string SlicePrefixAbs(string subject, int position)
		{
			return Operators.SlicePrefix(subject, position);
		}

		// Token: 0x0600BADF RID: 47839 RVA: 0x002834BA File Offset: 0x002816BA
		public static string SliceSuffix(string subject, int position)
		{
			return Operators.Slice(subject, position, subject.Length + 1);
		}

		// Token: 0x0600BAE0 RID: 47840 RVA: 0x002834CC File Offset: 0x002816CC
		public static string Split(string subject, string delimiter, int instance)
		{
			if (instance == 0 || delimiter.Length != 1)
			{
				return null;
			}
			string[] array = subject.Split(new char[] { delimiter[0] });
			int num = ((instance > 0) ? (instance - 1) : (array.Length + instance));
			if (0 > num || num >= array.Length)
			{
				return null;
			}
			return array[num];
		}

		// Token: 0x0600BAE1 RID: 47841 RVA: 0x0028351C File Offset: 0x0028171C
		public static string Trim(string subject)
		{
			return subject.Trim();
		}

		// Token: 0x0600BAE2 RID: 47842 RVA: 0x00283524 File Offset: 0x00281724
		public static string TrimFull(string subject)
		{
			return Operators._trimFullRegex.Replace(subject.Trim(), (Match m) => m.Captures[0].Value[0].ToString());
		}

		// Token: 0x0600BAE3 RID: 47843 RVA: 0x00283555 File Offset: 0x00281755
		public static string Replace(string subject, string findText, string replaceText)
		{
			if (string.IsNullOrEmpty(findText))
			{
				return null;
			}
			if (subject == null)
			{
				return null;
			}
			return subject.Replace(findText, replaceText);
		}

		// Token: 0x0600BAE4 RID: 47844 RVA: 0x00283570 File Offset: 0x00281770
		public static int? Match(string subject, MatchDescriptor regexDesc, int instance)
		{
			Match match = Operators.ResolveMatch(subject, regexDesc, instance);
			if (match == null)
			{
				return null;
			}
			return new int?(match.Index + 1);
		}

		// Token: 0x0600BAE5 RID: 47845 RVA: 0x002835A0 File Offset: 0x002817A0
		public static int? MatchEnd(string subject, MatchDescriptor regexDesc, int instance)
		{
			Match match = Operators.ResolveMatch(subject, regexDesc, instance);
			if (match != null)
			{
				return new int?(match.Index + match.Value.Length + 1);
			}
			return null;
		}

		// Token: 0x0600BAE6 RID: 47846 RVA: 0x002835DC File Offset: 0x002817DC
		public static string MatchFull(string subject, MatchDescriptor regexDesc, int instance)
		{
			Match match = Operators.ResolveMatch(subject, regexDesc, instance);
			if (match == null)
			{
				return null;
			}
			return match.Value;
		}

		// Token: 0x0600BAE7 RID: 47847 RVA: 0x002835F4 File Offset: 0x002817F4
		private static Match ResolveMatch(string subject, MatchDescriptor regexDesc, int instance)
		{
			if (instance == 0)
			{
				return null;
			}
			IEnumerable<Match> enumerable = regexDesc.Regex.NonCachingMatches(subject);
			if (instance < 0)
			{
				enumerable = enumerable.Reverse<Match>();
				instance = -instance;
			}
			return enumerable.MaybeElementAt(instance - 1).OrElseDefault<Match>();
		}

		// Token: 0x0600BAE8 RID: 47848 RVA: 0x00283630 File Offset: 0x00281830
		public static decimal? DateTimePart(DateTime subject, DateTimePartKind kind)
		{
			DateRecognition dateRecognition = new DateRecognition();
			int? num;
			switch (kind)
			{
			case DateTimePartKind.Second:
				num = new int?(dateRecognition.Second(subject));
				break;
			case DateTimePartKind.Minute:
				num = new int?(dateRecognition.Minute(subject));
				break;
			case DateTimePartKind.Hour:
				num = new int?(dateRecognition.Hour(subject));
				break;
			case DateTimePartKind.WeekDay:
				num = new int?(dateRecognition.WeekDay(subject) + 1);
				break;
			case DateTimePartKind.MonthDay:
				num = new int?(dateRecognition.MonthDay(subject));
				break;
			case DateTimePartKind.MonthWeek:
				num = new int?(dateRecognition.MonthWeek(subject));
				break;
			case DateTimePartKind.MonthDays:
				num = new int?(dateRecognition.MonthDays(subject));
				break;
			case DateTimePartKind.Month:
				num = new int?(dateRecognition.Month(subject));
				break;
			case DateTimePartKind.QuarterDay:
				num = new int?(dateRecognition.QuarterDay(subject));
				break;
			case DateTimePartKind.QuarterWeek:
				num = new int?(dateRecognition.QuarterWeek(subject));
				break;
			case DateTimePartKind.QuarterDays:
				num = dateRecognition.QuarterDays(subject);
				break;
			case DateTimePartKind.Quarter:
				num = new int?(dateRecognition.Quarter(subject));
				break;
			case DateTimePartKind.YearDay:
				num = new int?(dateRecognition.YearDay(subject));
				break;
			case DateTimePartKind.YearWeek:
				num = new int?(dateRecognition.YearWeek(subject));
				break;
			case DateTimePartKind.YearDays:
				num = new int?(dateRecognition.YearDays(subject));
				break;
			case DateTimePartKind.Year:
				num = new int?(dateRecognition.Year(subject));
				break;
			default:
				num = null;
				break;
			}
			int? num2 = num;
			if (num2 == null)
			{
				return null;
			}
			return new decimal?(num2.GetValueOrDefault());
		}

		// Token: 0x0600BAE9 RID: 47849 RVA: 0x002837C4 File Offset: 0x002819C4
		public static string FormatDateTime(DateTime subject, DateTimeDescriptor descriptor)
		{
			if (!(subject < descriptor.Culture.Calendar.MinSupportedDateTime) && !(subject > descriptor.Culture.Calendar.MaxSupportedDateTime))
			{
				return subject.ToString(descriptor.Mask, descriptor.Culture.DateTimeFormat);
			}
			return null;
		}

		// Token: 0x0600BAEA RID: 47850 RVA: 0x0028381B File Offset: 0x00281A1B
		public static DateTime? ParseDateTime(string subject, DateTimeDescriptor descriptor)
		{
			return Operators.ParseDateTime(subject, descriptor, false);
		}

		// Token: 0x0600BAEB RID: 47851 RVA: 0x00283828 File Offset: 0x00281A28
		public static DateTime? ParseDateTime(string subject, DateTimeDescriptor descriptor, bool ignoreCase)
		{
			DateTime dateTime;
			if (!DateTime.TryParseExact(subject, descriptor.Mask, descriptor.Culture.DateTimeFormat, DateTimeStyles.None, out dateTime))
			{
				return null;
			}
			return new DateTime?(dateTime);
		}

		// Token: 0x0600BAEC RID: 47852 RVA: 0x00283864 File Offset: 0x00281A64
		public static DateTime? RoundDateTime(DateTime subject, RoundDateTimeDescriptor descriptor)
		{
			DateRecognition dateRecognition = new DateRecognition();
			RoundDateTimePeriod period = descriptor.Period;
			RoundingMode mode = descriptor.Mode;
			DateTime? dateTime;
			switch (period)
			{
			case RoundDateTimePeriod.Second:
				dateTime = new DateTime?(dateRecognition.SecondStart(subject));
				break;
			case RoundDateTimePeriod.Minute:
				dateTime = new DateTime?(dateRecognition.MinuteStart(subject));
				break;
			case RoundDateTimePeriod.Hour:
				dateTime = new DateTime?(dateRecognition.HourStart(subject));
				break;
			case RoundDateTimePeriod.Day:
				dateTime = new DateTime?(dateRecognition.DayStart(subject));
				break;
			case RoundDateTimePeriod.Week:
				dateTime = dateRecognition.WeekStart(subject);
				break;
			case RoundDateTimePeriod.Month:
				dateTime = new DateTime?(dateRecognition.MonthStart(subject));
				break;
			case RoundDateTimePeriod.Quarter:
				dateTime = new DateTime?(dateRecognition.QuarterStart(subject));
				break;
			case RoundDateTimePeriod.Year:
				dateTime = new DateTime?(dateRecognition.YearStart(subject));
				break;
			default:
				throw new Exception(string.Format("Invalid RoundDatePeriod ({0})", period));
			}
			DateTime? dateTime2 = dateTime;
			if (dateTime2 == null)
			{
				return null;
			}
			DateTime value = dateTime2.Value;
			switch (period)
			{
			case RoundDateTimePeriod.Second:
				dateTime = dateRecognition.SecondEnd(subject);
				break;
			case RoundDateTimePeriod.Minute:
				dateTime = dateRecognition.MinuteEnd(subject);
				break;
			case RoundDateTimePeriod.Hour:
				dateTime = dateRecognition.HourEnd(subject);
				break;
			case RoundDateTimePeriod.Day:
				dateTime = dateRecognition.DayEnd(subject);
				break;
			case RoundDateTimePeriod.Week:
				dateTime = dateRecognition.WeekEnd(subject);
				break;
			case RoundDateTimePeriod.Month:
				dateTime = dateRecognition.MonthEnd(subject);
				break;
			case RoundDateTimePeriod.Quarter:
				dateTime = dateRecognition.QuarterEnd(subject);
				break;
			case RoundDateTimePeriod.Year:
				dateTime = dateRecognition.YearEnd(subject);
				break;
			default:
				throw new Exception(string.Format("Invalid RoundDatePeriod ({0})", period));
			}
			DateTime? dateTime3 = dateTime;
			if (dateTime3 == null)
			{
				return null;
			}
			DateTime value2 = dateTime3.Value;
			if (mode == RoundingMode.Down)
			{
				if (subject == value2)
				{
					return new DateTime?(value2);
				}
				if (!(value <= subject) || !(subject < value2))
				{
					return null;
				}
				return new DateTime?(value);
			}
			else
			{
				bool flag = descriptor.Ceiling == RoundDatePeriodCeiling.LastDay;
				if (mode == RoundingMode.Up && flag)
				{
					switch (period)
					{
					case RoundDateTimePeriod.Week:
						dateTime = dateRecognition.WeekEndDay(subject);
						break;
					case RoundDateTimePeriod.Month:
						dateTime = dateRecognition.MonthEndDay(subject);
						break;
					case RoundDateTimePeriod.Quarter:
						dateTime = dateRecognition.QuarterEndDay(subject);
						break;
					case RoundDateTimePeriod.Year:
						dateTime = dateRecognition.YearEndDay(subject);
						break;
					default:
						dateTime = null;
						break;
					}
					DateTime? dateTime4 = dateTime;
					if (!(value <= subject) || !(subject <= value2))
					{
						return null;
					}
					return dateTime4;
				}
				else if (mode == RoundingMode.Up)
				{
					if (subject == value)
					{
						return new DateTime?(value);
					}
					if (!(value < subject) || !(subject <= value2))
					{
						return null;
					}
					return new DateTime?(value2);
				}
				else
				{
					if (mode == RoundingMode.Nearest)
					{
						long ticks = (value2 - value).Ticks;
						DateTime dateTime5 = value + TimeSpan.FromTicks(ticks / 2L);
						return new DateTime?((subject >= dateTime5) ? value2 : value);
					}
					return null;
				}
			}
		}

		// Token: 0x0600BAED RID: 47853 RVA: 0x00283B68 File Offset: 0x00281D68
		public static decimal? TimePart(Time subject, TimePartKind kind)
		{
			decimal? num;
			switch (kind)
			{
			case TimePartKind.Second:
				num = new decimal?(subject.Seconds);
				break;
			case TimePartKind.Minute:
				num = new decimal?(subject.Minutes);
				break;
			case TimePartKind.Hour:
				num = new decimal?(subject.Hours);
				break;
			case TimePartKind.Hour12:
				num = new decimal?(subject.Hours % 12);
				break;
			case TimePartKind.TotalSeconds:
				num = new decimal?(Convert.ToDecimal(subject.TotalSeconds));
				break;
			case TimePartKind.TotalMinutes:
				num = new decimal?(Convert.ToDecimal(subject.TotalMinutes));
				break;
			case TimePartKind.TotalHours:
				num = new decimal?(Convert.ToDecimal(subject.TotalHours));
				break;
			default:
				num = null;
				break;
			}
			return num;
		}

		// Token: 0x0600BAEE RID: 47854 RVA: 0x00283C39 File Offset: 0x00281E39
		public static string FormatNumber(decimal subject, FormatNumberDescriptor descriptor)
		{
			return subject.ToString(descriptor.ToFormatString(), descriptor.Culture);
		}

		// Token: 0x0600BAEF RID: 47855 RVA: 0x00283C50 File Offset: 0x00281E50
		public static decimal? ParseNumber(string subject, string locale)
		{
			if (string.IsNullOrEmpty(subject))
			{
				return null;
			}
			decimal num;
			if (!decimal.TryParse(subject, NumberStyles.Any, new CultureInfo(locale).NumberFormat, out num))
			{
				return null;
			}
			return new decimal?(num.Normalize());
		}

		// Token: 0x0600BAF0 RID: 47856 RVA: 0x00283CA0 File Offset: 0x00281EA0
		public static decimal? RoundNumber(decimal subject, RoundNumberDescriptor descriptor)
		{
			decimal? num2;
			try
			{
				decimal num = Convert.ToDecimal(descriptor.Delta);
				switch (descriptor.Mode)
				{
				case RoundingMode.Nearest:
					num2 = new decimal?(Math.Round(subject / num, MidpointRounding.AwayFromZero) * num);
					break;
				case RoundingMode.Down:
					num2 = new decimal?(Math.Floor(subject / num) * num);
					break;
				case RoundingMode.Up:
					num2 = new decimal?(Math.Ceiling(subject / num) * num);
					break;
				default:
					num2 = null;
					break;
				}
				decimal? num3 = num2;
				decimal? num4;
				if (num3 == null)
				{
					num2 = null;
					num4 = num2;
				}
				else
				{
					num4 = new decimal?(num3.GetValueOrDefault().Normalize());
				}
				num2 = num4;
			}
			catch (OverflowException)
			{
				num2 = null;
			}
			return num2;
		}

		// Token: 0x0600BAF1 RID: 47857 RVA: 0x00004FAE File Offset: 0x000031AE
		public static DateTime Date(DateTime subject)
		{
			return subject;
		}

		// Token: 0x0600BAF2 RID: 47858 RVA: 0x00004FAE File Offset: 0x000031AE
		public static decimal Number(decimal subject)
		{
			return subject;
		}

		// Token: 0x0600BAF3 RID: 47859 RVA: 0x00004FAE File Offset: 0x000031AE
		public static string Str(string subject)
		{
			return subject;
		}

		// Token: 0x0600BAF4 RID: 47860 RVA: 0x00283D78 File Offset: 0x00281F78
		public static decimal? Add(decimal left, decimal right)
		{
			decimal? num;
			try
			{
				num = new decimal?(left + right);
			}
			catch (OverflowException)
			{
				num = null;
			}
			return num;
		}

		// Token: 0x0600BAF5 RID: 47861 RVA: 0x00283DB4 File Offset: 0x00281FB4
		public static decimal? Average(decimal[] subject)
		{
			decimal? num;
			try
			{
				decimal? num2;
				if (subject == null)
				{
					num = null;
					num2 = num;
				}
				else
				{
					num2 = new decimal?(subject.Average());
				}
				num = num2;
			}
			catch (OverflowException)
			{
				num = null;
			}
			return num;
		}

		// Token: 0x0600BAF6 RID: 47862 RVA: 0x00283DFC File Offset: 0x00281FFC
		public static decimal? Divide(decimal left, decimal right)
		{
			decimal? num2;
			try
			{
				decimal? num;
				if (!(right == 0m))
				{
					num = new decimal?(left / right);
				}
				else
				{
					num2 = null;
					num = num2;
				}
				num2 = num;
			}
			catch (OverflowException)
			{
				num2 = null;
			}
			return num2;
		}

		// Token: 0x0600BAF7 RID: 47863 RVA: 0x00283E50 File Offset: 0x00282050
		public static decimal? Multiply(decimal left, decimal right)
		{
			decimal? num;
			try
			{
				num = new decimal?(left * right);
			}
			catch (OverflowException)
			{
				num = null;
			}
			return num;
		}

		// Token: 0x0600BAF8 RID: 47864 RVA: 0x00283E8C File Offset: 0x0028208C
		public static decimal? Product(decimal[] subject)
		{
			decimal? num;
			try
			{
				decimal? num2;
				if (subject == null)
				{
					num = null;
					num2 = num;
				}
				else
				{
					num2 = new decimal?(subject.Aggregate((decimal total, decimal next) => total * next));
				}
				num = num2;
			}
			catch (OverflowException)
			{
				num = null;
			}
			return num;
		}

		// Token: 0x0600BAF9 RID: 47865 RVA: 0x00283EF4 File Offset: 0x002820F4
		public static decimal? Subtract(decimal left, decimal right)
		{
			decimal? num;
			try
			{
				num = new decimal?(left - right);
			}
			catch (OverflowException)
			{
				num = null;
			}
			return num;
		}

		// Token: 0x0600BAFA RID: 47866 RVA: 0x00283F30 File Offset: 0x00282130
		public static decimal? Sum(decimal[] subject)
		{
			decimal? num;
			try
			{
				decimal? num2;
				if (subject == null)
				{
					num = null;
					num2 = num;
				}
				else
				{
					num2 = new decimal?(subject.Sum());
				}
				num = num2;
			}
			catch (OverflowException)
			{
				num = null;
			}
			return num;
		}

		// Token: 0x0600BAFB RID: 47867 RVA: 0x00283F78 File Offset: 0x00282178
		public static DateTime? FromDateTime(IRow inputRow, string columnName)
		{
			object obj;
			if (!inputRow.TryGetValue(columnName, out obj))
			{
				return null;
			}
			return obj as DateTime?;
		}

		// Token: 0x0600BAFC RID: 47868 RVA: 0x00283FA8 File Offset: 0x002821A8
		public static DateTime? FromDateTimePart(IRow inputRow, string columnName, DateTimePartKind kind)
		{
			decimal? num = Operators.FromNumber(inputRow, columnName);
			if (num == null)
			{
				return null;
			}
			decimal value = num.Value;
			if (kind != DateTimePartKind.Month)
			{
				if (kind == DateTimePartKind.Year && DateTime.MinValue.Year <= value && value <= DateTime.MaxValue.Year)
				{
					return new DateTime?(new DateTime(Convert.ToInt32(num.Value), 1, 1));
				}
			}
			else if (DateTime.MinValue.Month <= value && value <= DateTime.MaxValue.Month)
			{
				return new DateTime?(new DateTime(2000, Convert.ToInt32(num.Value), 1));
			}
			return null;
		}

		// Token: 0x0600BAFD RID: 47869 RVA: 0x0028409C File Offset: 0x0028229C
		public static decimal? FromNumber(IRow inputRow, string columnName)
		{
			object obj;
			if (columnName == null || !inputRow.TryGetValue(columnName, out obj))
			{
				return null;
			}
			decimal? num2;
			if (obj is decimal)
			{
				decimal num = (decimal)obj;
				num2 = new decimal?(num);
			}
			else if (obj is double)
			{
				double num3 = (double)obj;
				num2 = new decimal?(Convert.ToDecimal(num3));
			}
			else if (obj is int)
			{
				int num4 = (int)obj;
				num2 = new decimal?(Convert.ToDecimal(num4));
			}
			else if (obj is uint)
			{
				uint num5 = (uint)obj;
				num2 = new decimal?(Convert.ToDecimal(num5));
			}
			else if (obj is long)
			{
				long num6 = (long)obj;
				num2 = new decimal?(Convert.ToDecimal(num6));
			}
			else if (obj is ulong)
			{
				ulong num7 = (ulong)obj;
				num2 = new decimal?(Convert.ToDecimal(num7));
			}
			else if (obj is short)
			{
				short num8 = (short)obj;
				num2 = new decimal?(Convert.ToDecimal(num8));
			}
			else if (obj is ushort)
			{
				ushort num9 = (ushort)obj;
				num2 = new decimal?(Convert.ToDecimal(num9));
			}
			else if (obj is byte)
			{
				byte b = (byte)obj;
				num2 = new decimal?(Convert.ToDecimal(b));
			}
			else if (obj is float)
			{
				float num10 = (float)obj;
				num2 = new decimal?(Convert.ToDecimal(num10));
			}
			else
			{
				num2 = null;
			}
			return num2;
		}

		// Token: 0x0600BAFE RID: 47870 RVA: 0x00284235 File Offset: 0x00282435
		public static decimal? FromNumber(IRow inputRow, string columnName, bool coalesceZero)
		{
			if (!coalesceZero)
			{
				return Operators.FromNumber(inputRow, columnName);
			}
			return Operators.FromNumberCoalesced(inputRow, columnName);
		}

		// Token: 0x0600BAFF RID: 47871 RVA: 0x0028424C File Offset: 0x0028244C
		public static decimal? FromNumberCoalesced(IRow inputRow, string columnName)
		{
			object obj;
			if (columnName == null || !inputRow.TryGetValue(columnName, out obj))
			{
				return null;
			}
			bool flag;
			if (obj != null)
			{
				string text = obj as string;
				if (text == null || !(text == ""))
				{
					flag = false;
					goto IL_0038;
				}
			}
			flag = true;
			IL_0038:
			if (!flag)
			{
				return Operators.FromNumber(inputRow, columnName);
			}
			return new decimal?(0m);
		}

		// Token: 0x0600BB00 RID: 47872 RVA: 0x002842A8 File Offset: 0x002824A8
		public static decimal[] FromNumbers(IRow inputRow, string[] columnNames)
		{
			bool flag = !(((columnNames != null) ? new bool?(columnNames.Any<string>()) : null) ?? false);
			if (flag)
			{
				return null;
			}
			decimal[] array = (from columnName in columnNames
				let value = Operators.FromNumberCoalesced(inputRow, columnName)
				where value != null
				select value.Value).ToArray<decimal>();
			if (columnNames.Length == array.Length)
			{
				return array;
			}
			return null;
		}

		// Token: 0x0600BB01 RID: 47873 RVA: 0x00284368 File Offset: 0x00282568
		public static string FromNumberStr(IRow inputRow, string columnName)
		{
			object obj;
			if (!inputRow.TryGetValue(columnName, out obj))
			{
				return null;
			}
			string text;
			if (obj is int)
			{
				text = ((int)obj).ToString();
			}
			else if (obj is double)
			{
				text = ((double)obj).ToString(CultureInfo.InvariantCulture);
			}
			else if (obj is decimal)
			{
				text = ((decimal)obj).ToString(CultureInfo.InvariantCulture);
			}
			else
			{
				text = null;
			}
			return text;
		}

		// Token: 0x0600BB02 RID: 47874 RVA: 0x002843E8 File Offset: 0x002825E8
		public static string FromStr(IRow inputRow, string columnName)
		{
			object obj;
			if (inputRow.TryGetValue(columnName, out obj))
			{
				string text = obj as string;
				if (text != null)
				{
					return text;
				}
			}
			return null;
		}

		// Token: 0x0600BB03 RID: 47875 RVA: 0x00284410 File Offset: 0x00282610
		public static Time? FromTime(IRow inputRow, string columnName)
		{
			object obj;
			if (inputRow.TryGetValue(columnName, out obj) && obj is Time)
			{
				Time time = (Time)obj;
				return new Time?(time);
			}
			return null;
		}

		// Token: 0x0600BB04 RID: 47876 RVA: 0x00002188 File Offset: 0x00000388
		public static object Null()
		{
			return null;
		}

		// Token: 0x0600BB05 RID: 47877 RVA: 0x00004FAE File Offset: 0x000031AE
		public static DateTime ToDateTime(DateTime subject)
		{
			return subject;
		}

		// Token: 0x0600BB06 RID: 47878 RVA: 0x00004FAE File Offset: 0x000031AE
		public static decimal ToDecimal(decimal subject)
		{
			return subject;
		}

		// Token: 0x0600BB07 RID: 47879 RVA: 0x00284449 File Offset: 0x00282649
		public static double ToDouble(decimal subject)
		{
			return Convert.ToDouble(subject);
		}

		// Token: 0x0600BB08 RID: 47880 RVA: 0x00284451 File Offset: 0x00282651
		public static int ToInt(decimal subject)
		{
			return Convert.ToInt32(subject);
		}

		// Token: 0x0600BB09 RID: 47881 RVA: 0x00004FAE File Offset: 0x000031AE
		public static string ToStr(string subject)
		{
			return subject;
		}

		// Token: 0x0600BB0A RID: 47882 RVA: 0x0028445C File Offset: 0x0028265C
		public static int? FromRowNumber(IRow inputRow)
		{
			INumberedRow numberedRow = inputRow as INumberedRow;
			if (numberedRow == null)
			{
				return null;
			}
			return new int?(numberedRow.RowNumber);
		}

		// Token: 0x0600BB0B RID: 47883 RVA: 0x00284488 File Offset: 0x00282688
		public static decimal RowNumberLinearTransform(int rowNumber, RowNumberLinearTransformDescriptor descriptor)
		{
			return rowNumber * descriptor.Gradient + descriptor.Intercept;
		}

		// Token: 0x0600BB0C RID: 47884 RVA: 0x002844A8 File Offset: 0x002826A8
		public static bool Contains(IRow row, string columnName, string findText, int count)
		{
			object obj;
			if (row != null && row.TryGetValue(columnName, out obj))
			{
				string text = obj as string;
				if (text != null)
				{
					if (count <= 0 || findText.IsNullOrEmpty())
					{
						return false;
					}
					if (findText.Length == 1)
					{
						return text.Count((char c) => c == findText[0]) == count;
					}
					return text.AllIndexesOf(findText, StringComparison.Ordinal).Count<int>() == count;
				}
			}
			return false;
		}

		// Token: 0x0600BB0D RID: 47885 RVA: 0x00284528 File Offset: 0x00282728
		public static bool EndsWith(string subject, string findText)
		{
			return !findText.IsNullOrEmpty() && subject.EndsWith(findText);
		}

		// Token: 0x0600BB0E RID: 47886 RVA: 0x0028453C File Offset: 0x0028273C
		public static bool StringEquals(IRow row, string columnName, string text)
		{
			object obj;
			if (row != null && row.TryGetValue(columnName, out obj))
			{
				string text2 = obj as string;
				if (text2 != null)
				{
					return !text.IsNullOrEmpty() && text2.Equals(text);
				}
			}
			return false;
		}

		// Token: 0x0600BB0F RID: 47887 RVA: 0x00284574 File Offset: 0x00282774
		[LazySemantics]
		public static object If(bool? predicate, object trueBranch, object falseBranch)
		{
			if (predicate == null)
			{
				return null;
			}
			if (!predicate.Value)
			{
				return falseBranch;
			}
			return trueBranch;
		}

		// Token: 0x0600BB10 RID: 47888 RVA: 0x00284590 File Offset: 0x00282790
		public static bool IsBlank(IRow row, string columnName)
		{
			object obj;
			if (row == null || !row.TryGetValue(columnName, out obj))
			{
				return true;
			}
			if (obj != null)
			{
				string text = obj as string;
				return text != null && string.IsNullOrEmpty(text);
			}
			return true;
		}

		// Token: 0x0600BB11 RID: 47889 RVA: 0x002845C4 File Offset: 0x002827C4
		public static bool IsMatch(IRow row, string columnName, Regex regex)
		{
			object obj;
			if (row != null && row.TryGetValue(columnName, out obj))
			{
				string text = obj as string;
				if (text != null)
				{
					return regex != null && regex.IsMatch(text);
				}
			}
			return false;
		}

		// Token: 0x0600BB12 RID: 47890 RVA: 0x002845F8 File Offset: 0x002827F8
		public static bool ContainsMatch(IRow row, string columnName, Regex regex, int count)
		{
			object obj;
			if (row != null && row.TryGetValue(columnName, out obj))
			{
				string text = obj as string;
				if (text != null)
				{
					return regex != null && regex.NonCachingMatches(text).Count<Match>() == count;
				}
			}
			return false;
		}

		// Token: 0x0600BB13 RID: 47891 RVA: 0x00284633 File Offset: 0x00282833
		public static bool IsNotBlank(IRow row, string columnName)
		{
			return !Operators.IsBlank(row, columnName);
		}

		// Token: 0x0600BB14 RID: 47892 RVA: 0x00284640 File Offset: 0x00282840
		public static bool IsNumber(IRow row, string columnName)
		{
			object obj;
			return row != null && row.TryGetValue(columnName, out obj) && obj.IsNumeric();
		}

		// Token: 0x0600BB15 RID: 47893 RVA: 0x00284664 File Offset: 0x00282864
		public static bool IsString(IRow row, string columnName)
		{
			object obj;
			return row != null && row.TryGetValue(columnName, out obj) && obj is string;
		}

		// Token: 0x0600BB16 RID: 47894 RVA: 0x0028468C File Offset: 0x0028288C
		public static bool NumberEquals(IRow row, string columnName, decimal value)
		{
			object obj;
			if (row == null || !row.TryGetValue(columnName, out obj))
			{
				return false;
			}
			decimal? num = obj.AsDecimal();
			return (num.GetValueOrDefault() == value) & (num != null);
		}

		// Token: 0x0600BB17 RID: 47895 RVA: 0x002846C8 File Offset: 0x002828C8
		public static bool NumberGreaterThan(IRow row, string columnName, decimal value)
		{
			object obj;
			if (row == null || !row.TryGetValue(columnName, out obj))
			{
				return false;
			}
			decimal? num = obj.AsDecimal();
			return (num.GetValueOrDefault() > value) & (num != null);
		}

		// Token: 0x0600BB18 RID: 47896 RVA: 0x00284704 File Offset: 0x00282904
		public static bool NumberLessThan(IRow row, string columnName, decimal value)
		{
			object obj;
			if (row == null || !row.TryGetValue(columnName, out obj))
			{
				return false;
			}
			decimal? num = obj.AsDecimal();
			return (num.GetValueOrDefault() < value) & (num != null);
		}

		// Token: 0x0600BB19 RID: 47897 RVA: 0x0028473F File Offset: 0x0028293F
		public static bool Or(bool? left, bool? right)
		{
			return left != null && right != null && (left.Value || right.Value);
		}

		// Token: 0x0600BB1A RID: 47898 RVA: 0x00284768 File Offset: 0x00282968
		public static bool StartsWith(IRow row, string columnName, string findText)
		{
			object obj;
			if (row != null && row.TryGetValue(columnName, out obj))
			{
				string text = obj as string;
				if (text != null)
				{
					return !text.IsNullOrEmpty() && text.StartsWith(findText);
				}
			}
			return false;
		}

		// Token: 0x0600BB1B RID: 47899 RVA: 0x002847A0 File Offset: 0x002829A0
		public static bool StartsWithDigit(IRow row, string columnName)
		{
			object obj;
			if (row != null && row.TryGetValue(columnName, out obj))
			{
				string text = obj as string;
				if (text != null)
				{
					return !text.IsNullOrEmpty() && char.IsDigit(text[0]);
				}
			}
			return false;
		}

		// Token: 0x0600BB1C RID: 47900 RVA: 0x002847E0 File Offset: 0x002829E0
		public static bool EndsWithDigit(IRow row, string columnName)
		{
			object obj;
			if (row != null && row.TryGetValue(columnName, out obj))
			{
				string text = obj as string;
				if (text != null)
				{
					return !text.IsNullOrEmpty() && char.IsDigit(text[text.Length - 1]);
				}
			}
			return false;
		}

		// Token: 0x040046A6 RID: 18086
		private static readonly Regex _trimFullRegex = "(\\p{Z}{2,})".ToRegex(RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant);
	}
}
