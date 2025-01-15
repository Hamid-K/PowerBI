using System;
using System.Globalization;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.OleDb;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020012D3 RID: 4819
	public class DateValue : PrimitiveValue, IDateValue, IValue
	{
		// Token: 0x17002267 RID: 8807
		// (get) Token: 0x06007F07 RID: 32519 RVA: 0x001B3570 File Offset: 0x001B1770
		public int Year
		{
			get
			{
				return this.date.Year;
			}
		}

		// Token: 0x17002268 RID: 8808
		// (get) Token: 0x06007F08 RID: 32520 RVA: 0x001B358C File Offset: 0x001B178C
		public int Month
		{
			get
			{
				return this.date.Month;
			}
		}

		// Token: 0x17002269 RID: 8809
		// (get) Token: 0x06007F09 RID: 32521 RVA: 0x001B35A8 File Offset: 0x001B17A8
		public int Day
		{
			get
			{
				return this.date.Day;
			}
		}

		// Token: 0x06007F0A RID: 32522 RVA: 0x001B35C3 File Offset: 0x001B17C3
		protected DateValue(DateTime dateTime)
		{
			this.date = dateTime.Date;
		}

		// Token: 0x06007F0B RID: 32523 RVA: 0x001B35D8 File Offset: 0x001B17D8
		public static DateValue New(DateTime date)
		{
			return new DateValue(date);
		}

		// Token: 0x06007F0C RID: 32524 RVA: 0x001B35E0 File Offset: 0x001B17E0
		public static DateValue New(long ticks)
		{
			return DateValue.New(new DateTime(ticks));
		}

		// Token: 0x06007F0D RID: 32525 RVA: 0x001B35F0 File Offset: 0x001B17F0
		public static DateValue New(int year, int month, int day)
		{
			DateValue dateValue;
			try
			{
				dateValue = new DateValue(new DateTime(year, month, day));
			}
			catch (ArgumentOutOfRangeException ex)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.Date_OutOfRangeError, Value.Null, ex);
			}
			return dateValue;
		}

		// Token: 0x06007F0E RID: 32526 RVA: 0x001B3630 File Offset: 0x001B1830
		public static bool TryParseFromText(string text, CultureInfo formatProvider, out DateValue instant)
		{
			return InstantParser.TryParseDate(text, formatProvider, out instant);
		}

		// Token: 0x06007F0F RID: 32527 RVA: 0x001B363A File Offset: 0x001B183A
		public static bool TryParseFromText(string text, string format, CultureInfo formatProvider, out DateValue instant)
		{
			return InstantParser.TryParseDate(text, format, formatProvider, out instant);
		}

		// Token: 0x06007F10 RID: 32528 RVA: 0x001B3645 File Offset: 0x001B1845
		public RecordValue ToRecord()
		{
			return RecordValue.New(DateValue.RecordFormatKeys, new Value[]
			{
				NumberValue.New(this.Year),
				NumberValue.New(this.Month),
				NumberValue.New(this.Day)
			});
		}

		// Token: 0x1700226A RID: 8810
		// (get) Token: 0x06007F11 RID: 32529 RVA: 0x000023C4 File Offset: 0x000005C4
		public sealed override ValueKind Kind
		{
			get
			{
				return ValueKind.Date;
			}
		}

		// Token: 0x1700226B RID: 8811
		// (get) Token: 0x06007F12 RID: 32530 RVA: 0x00002139 File Offset: 0x00000339
		public override bool IsDate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700226C RID: 8812
		// (get) Token: 0x06007F13 RID: 32531 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public override DateValue AsDate
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700226D RID: 8813
		// (get) Token: 0x06007F14 RID: 32532 RVA: 0x001B3681 File Offset: 0x001B1881
		public override TypeValue Type
		{
			get
			{
				return TypeValue.Date;
			}
		}

		// Token: 0x06007F15 RID: 32533 RVA: 0x001B3688 File Offset: 0x001B1888
		public override string ToSource()
		{
			return string.Format(CultureInfo.InvariantCulture, "#date({0}, {1}, {2})", this.Year, this.Month, this.Day);
		}

		// Token: 0x06007F16 RID: 32534 RVA: 0x001B36BC File Offset: 0x001B18BC
		public override string ToString()
		{
			string text;
			try
			{
				text = this.date.ToString(CultureInfo.CurrentCulture);
			}
			catch (ArgumentOutOfRangeException)
			{
				throw ValueException.NewDataFormatError<Message0>(Strings.Date_NotConvertibleToDate, this, null);
			}
			return text;
		}

		// Token: 0x06007F17 RID: 32535 RVA: 0x001B3700 File Offset: 0x001B1900
		public sealed override object ToOleDb(Type type)
		{
			if (type == typeof(Date) || type == typeof(object))
			{
				return new Date(this.AsDate.AsClrDateTime);
			}
			throw ValueMarshaller.CreateTypeError(this, type);
		}

		// Token: 0x06007F18 RID: 32536 RVA: 0x001B3750 File Offset: 0x001B1950
		public string ToString(IFormatProvider formatProvider)
		{
			string text;
			try
			{
				text = this.date.ToString(formatProvider);
			}
			catch (ArgumentOutOfRangeException)
			{
				throw ValueException.NewDataFormatError<Message0>(Strings.Date_NotConvertibleToDate, this, null);
			}
			catch (NotSupportedException)
			{
				throw ValueException.NewExpressionError<Message1>(Strings.Culture_NotSupported_Parsing((formatProvider as CultureInfo).Name), null, null);
			}
			return text;
		}

		// Token: 0x06007F19 RID: 32537 RVA: 0x001B37B4 File Offset: 0x001B19B4
		public string ToString(string format, IFormatProvider formatProvider)
		{
			string text;
			try
			{
				string text2;
				text = this.date.ToString(IsoFormats.DateIsoToClrFormatMap.TryGetValue(format, out text2) ? text2 : format, formatProvider);
			}
			catch (FormatException ex)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.DateTime_InvalidOutputFormatError, TextValue.New(format), ex);
			}
			catch (ArgumentOutOfRangeException)
			{
				throw ValueException.NewDataFormatError<Message0>(Strings.Date_NotConvertibleToDate, this, null);
			}
			catch (NotSupportedException)
			{
				throw ValueException.NewExpressionError<Message1>(Strings.Culture_NotSupported_Parsing((formatProvider as CultureInfo).Name), null, null);
			}
			return text;
		}

		// Token: 0x1700226E RID: 8814
		// (get) Token: 0x06007F1A RID: 32538 RVA: 0x001B384C File Offset: 0x001B1A4C
		public DateTime AsClrDateTime
		{
			get
			{
				return this.date;
			}
		}

		// Token: 0x06007F1B RID: 32539 RVA: 0x001B3854 File Offset: 0x001B1A54
		public override bool Equals(Value value, _ValueComparer comparer)
		{
			return value.IsDate && this.date == value.AsDate.date;
		}

		// Token: 0x06007F1C RID: 32540 RVA: 0x001B3878 File Offset: 0x001B1A78
		public override int GetHashCode(_ValueComparer comparer)
		{
			return this.date.GetHashCode();
		}

		// Token: 0x06007F1D RID: 32541 RVA: 0x001B3894 File Offset: 0x001B1A94
		public override int CompareTo(Value value, _ValueComparer comparer)
		{
			if (value.IsDate)
			{
				return this.date.CompareTo(value.AsDate.date);
			}
			return base.CompareTo(value, comparer);
		}

		// Token: 0x06007F1E RID: 32542 RVA: 0x001B38CC File Offset: 0x001B1ACC
		public override Value Concatenate(Value value)
		{
			if (value.IsNull)
			{
				return Value.Null;
			}
			if (value.IsTime)
			{
				return DateTimeValue.New(this.date.Ticks + value.AsTime.AsClrTimeSpan.Ticks);
			}
			return base.Concatenate(value);
		}

		// Token: 0x06007F1F RID: 32543 RVA: 0x001B391E File Offset: 0x001B1B1E
		public Value StartOfYear()
		{
			return new DateValue(new DateTime(this.Year, 1, 1));
		}

		// Token: 0x06007F20 RID: 32544 RVA: 0x001B3932 File Offset: 0x001B1B32
		public Value StartOfQuarter()
		{
			return new DateValue(this.date.StartOfQuarter());
		}

		// Token: 0x06007F21 RID: 32545 RVA: 0x001B3944 File Offset: 0x001B1B44
		public Value StartOfMonth()
		{
			return new DateValue(new DateTime(this.Year, this.Month, 1));
		}

		// Token: 0x06007F22 RID: 32546 RVA: 0x001B395D File Offset: 0x001B1B5D
		public Value StartOfDay()
		{
			return new DateValue(new DateTime(this.Year, this.Month, this.Day));
		}

		// Token: 0x06007F23 RID: 32547 RVA: 0x001B397C File Offset: 0x001B1B7C
		public Value StartOfWeek(DayOfWeek firstDayOfWeek)
		{
			int num = -1 * (this.date.DayOfWeek - firstDayOfWeek + 7) % 7;
			DateTime dateTime = new DateTime(this.Year, this.Month, this.Day);
			try
			{
				dateTime = dateTime.AddDays((double)num);
			}
			catch (ArgumentException ex)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.Date_OutOfRangeError, Value.Null, ex);
			}
			return new DateValue(dateTime);
		}

		// Token: 0x06007F24 RID: 32548 RVA: 0x001B39F0 File Offset: 0x001B1BF0
		public DateValue EndOfYear()
		{
			return DateValue.New(new DateTime(this.Year, 12, 31));
		}

		// Token: 0x06007F25 RID: 32549 RVA: 0x001B3A06 File Offset: 0x001B1C06
		public Value EndOfQuarter()
		{
			return new DateValue(this.date.EndOfQuarter());
		}

		// Token: 0x06007F26 RID: 32550 RVA: 0x001B3A18 File Offset: 0x001B1C18
		public DateValue EndOfMonth()
		{
			return DateValue.New(new DateTime(this.Year, this.Month, DateValue.Calendar.GetDaysInMonth(this.Year, this.Month)));
		}

		// Token: 0x06007F27 RID: 32551 RVA: 0x001B3A46 File Offset: 0x001B1C46
		public DateValue EndOfDay()
		{
			return DateValue.New(new DateTime(this.Year, this.Month, this.Day));
		}

		// Token: 0x06007F28 RID: 32552 RVA: 0x001B3A64 File Offset: 0x001B1C64
		public DateValue EndOfWeek(DayOfWeek lastDayOfWeek)
		{
			int num = (lastDayOfWeek - this.date.DayOfWeek + 7) % 7;
			DateTime dateTime = new DateTime(this.Year, this.Month, this.Day);
			try
			{
				dateTime = dateTime.AddDays((double)num);
			}
			catch (ArgumentException ex)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.Date_OutOfRangeError, Value.Null, ex);
			}
			return DateValue.New(dateTime);
		}

		// Token: 0x06007F29 RID: 32553 RVA: 0x001B3AD4 File Offset: 0x001B1CD4
		public override Value Add(Value value)
		{
			ValueKind kind = value.Kind;
			if (kind == ValueKind.Null)
			{
				return Value.Null;
			}
			if (kind == ValueKind.Duration)
			{
				return this.Add(value.AsDuration);
			}
			return base.Add(value);
		}

		// Token: 0x06007F2A RID: 32554 RVA: 0x001B2521 File Offset: 0x001B0721
		public override Value Add(Value value, Precision precision)
		{
			return this.Add(value);
		}

		// Token: 0x06007F2B RID: 32555 RVA: 0x001B3B0C File Offset: 0x001B1D0C
		public override Value Subtract(Value value)
		{
			ValueKind kind = value.Kind;
			if (kind == ValueKind.Null)
			{
				return Value.Null;
			}
			if (kind == ValueKind.Date)
			{
				return this.Subtract(value.AsDate);
			}
			if (kind != ValueKind.Duration)
			{
				return base.Subtract(value);
			}
			return this.Subtract(value.AsDuration);
		}

		// Token: 0x06007F2C RID: 32556 RVA: 0x001B2574 File Offset: 0x001B0774
		public override Value Subtract(Value value, Precision precision)
		{
			return this.Subtract(value);
		}

		// Token: 0x06007F2D RID: 32557 RVA: 0x001B3B54 File Offset: 0x001B1D54
		public DateValue Add(DurationValue interval)
		{
			DateValue dateValue;
			try
			{
				dateValue = DateValue.New(this.date.Add(interval.AsTimeSpan));
			}
			catch (ArgumentOutOfRangeException ex)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.Date_OutOfRangeError, Value.Null, ex);
			}
			return dateValue;
		}

		// Token: 0x06007F2E RID: 32558 RVA: 0x001B3BA0 File Offset: 0x001B1DA0
		public DurationValue Subtract(DateValue instant)
		{
			DurationValue durationValue;
			try
			{
				durationValue = new DurationValue(this.date - instant.AsClrDateTime);
			}
			catch (ArgumentOutOfRangeException ex)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.Date_OutOfRangeError, Value.Null, ex);
			}
			return durationValue;
		}

		// Token: 0x06007F2F RID: 32559 RVA: 0x001B2618 File Offset: 0x001B0818
		public Value Subtract(DurationValue interval)
		{
			return this.Add(interval.Negate());
		}

		// Token: 0x06007F30 RID: 32560 RVA: 0x001B3BEC File Offset: 0x001B1DEC
		public DateValue AddDays(int days)
		{
			DateTime dateTime;
			try
			{
				dateTime = this.date.AddDays((double)days);
			}
			catch (ArgumentException ex)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.Date_OutOfRangeError, Value.Null, ex);
			}
			return new DateValue(dateTime);
		}

		// Token: 0x06007F31 RID: 32561 RVA: 0x001B3C34 File Offset: 0x001B1E34
		public DateValue AddWeeks(int weeks)
		{
			DateTime dateTime;
			try
			{
				dateTime = this.date.AddWeeks(weeks);
			}
			catch (ArgumentException ex)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.Date_OutOfRangeError, Value.Null, ex);
			}
			return new DateValue(dateTime);
		}

		// Token: 0x06007F32 RID: 32562 RVA: 0x001B3C78 File Offset: 0x001B1E78
		public DateValue AddMonths(int months)
		{
			DateTime dateTime;
			try
			{
				dateTime = this.date.AddMonths(months);
			}
			catch (ArgumentException ex)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.Date_OutOfRangeError, Value.Null, ex);
			}
			return new DateValue(dateTime);
		}

		// Token: 0x06007F33 RID: 32563 RVA: 0x001B3CC0 File Offset: 0x001B1EC0
		public DateValue AddQuarters(int quarters)
		{
			DateTime dateTime;
			try
			{
				dateTime = this.date.AddQuarters(quarters);
			}
			catch (ArgumentException ex)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.Date_OutOfRangeError, Value.Null, ex);
			}
			return new DateValue(dateTime);
		}

		// Token: 0x06007F34 RID: 32564 RVA: 0x001B3D04 File Offset: 0x001B1F04
		public DateValue AddYears(int years)
		{
			DateTime dateTime;
			try
			{
				dateTime = this.date.AddYears(years);
			}
			catch (ArgumentException ex)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.Date_OutOfRangeError, Value.Null, ex);
			}
			return new DateValue(dateTime);
		}

		// Token: 0x06007F35 RID: 32565 RVA: 0x001B3D4C File Offset: 0x001B1F4C
		public DayOfWeek DayOfWeek(DayOfWeek firstDayOfWeek)
		{
			return (this.AsClrDateTime.DayOfWeek - (int)firstDayOfWeek + 7) % (DayOfWeek)7;
		}

		// Token: 0x06007F36 RID: 32566 RVA: 0x001B3D6D File Offset: 0x001B1F6D
		private static DateValue New(DateValue value, RecordValue metaValue, TypeValue type)
		{
			if (value.MetaValue == metaValue && value.Type == type)
			{
				return value;
			}
			return new DateValue.MetaTypeDateValue(value, metaValue, type);
		}

		// Token: 0x06007F37 RID: 32567 RVA: 0x001B3D8B File Offset: 0x001B1F8B
		public sealed override Value NewType(TypeValue type)
		{
			return DateValue.New(this, this.MetaValue, type);
		}

		// Token: 0x06007F38 RID: 32568 RVA: 0x001B3D9A File Offset: 0x001B1F9A
		public sealed override Value NewMeta(RecordValue metaValue)
		{
			return DateValue.New(this, metaValue, this.Type);
		}

		// Token: 0x1700226F RID: 8815
		// (get) Token: 0x06007F39 RID: 32569 RVA: 0x00019E61 File Offset: 0x00018061
		public override RecordValue MetaValue
		{
			get
			{
				return RecordValue.Empty;
			}
		}

		// Token: 0x0400459A RID: 17818
		public static readonly GregorianCalendar Calendar = new GregorianCalendar();

		// Token: 0x0400459B RID: 17819
		private const string TypeName = "Date";

		// Token: 0x0400459C RID: 17820
		private const string SourcePattern = "#date({0}, {1}, {2})";

		// Token: 0x0400459D RID: 17821
		public const string YearKey = "Year";

		// Token: 0x0400459E RID: 17822
		public const string MonthKey = "Month";

		// Token: 0x0400459F RID: 17823
		public const string DayKey = "Day";

		// Token: 0x040045A0 RID: 17824
		public const int DaysPerWeek = 7;

		// Token: 0x040045A1 RID: 17825
		private static readonly Keys RecordFormatKeys = Keys.New("Year", "Month", "Day");

		// Token: 0x040045A2 RID: 17826
		private readonly DateTime date;

		// Token: 0x020012D4 RID: 4820
		private class MetaTypeDateValue : DateValue
		{
			// Token: 0x06007F3B RID: 32571 RVA: 0x001B3DCE File Offset: 0x001B1FCE
			public MetaTypeDateValue(DateValue value, RecordValue meta, TypeValue type)
				: base(value.date)
			{
				this.meta = meta;
				this.type = type;
			}

			// Token: 0x17002270 RID: 8816
			// (get) Token: 0x06007F3C RID: 32572 RVA: 0x001B3DEA File Offset: 0x001B1FEA
			public override TypeValue Type
			{
				get
				{
					return this.type;
				}
			}

			// Token: 0x17002271 RID: 8817
			// (get) Token: 0x06007F3D RID: 32573 RVA: 0x001B3DF2 File Offset: 0x001B1FF2
			public override RecordValue MetaValue
			{
				get
				{
					return this.meta;
				}
			}

			// Token: 0x040045A3 RID: 17827
			private RecordValue meta;

			// Token: 0x040045A4 RID: 17828
			private TypeValue type;
		}
	}
}
