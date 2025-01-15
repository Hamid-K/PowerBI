using System;
using System.Globalization;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020012CF RID: 4815
	public class DateTimeValue : PrimitiveValue, IDateTime, IValue
	{
		// Token: 0x17002249 RID: 8777
		// (get) Token: 0x06007E8C RID: 32396 RVA: 0x001B1CB8 File Offset: 0x001AFEB8
		public int Year
		{
			get
			{
				return this.dateTime.Year;
			}
		}

		// Token: 0x1700224A RID: 8778
		// (get) Token: 0x06007E8D RID: 32397 RVA: 0x001B1CD4 File Offset: 0x001AFED4
		public int Month
		{
			get
			{
				return this.dateTime.Month;
			}
		}

		// Token: 0x1700224B RID: 8779
		// (get) Token: 0x06007E8E RID: 32398 RVA: 0x001B1CF0 File Offset: 0x001AFEF0
		public int Day
		{
			get
			{
				return this.dateTime.Day;
			}
		}

		// Token: 0x1700224C RID: 8780
		// (get) Token: 0x06007E8F RID: 32399 RVA: 0x001B1D0C File Offset: 0x001AFF0C
		public int Hour
		{
			get
			{
				return this.dateTime.Hour;
			}
		}

		// Token: 0x1700224D RID: 8781
		// (get) Token: 0x06007E90 RID: 32400 RVA: 0x001B1D28 File Offset: 0x001AFF28
		public int Minute
		{
			get
			{
				return this.dateTime.Minute;
			}
		}

		// Token: 0x1700224E RID: 8782
		// (get) Token: 0x06007E91 RID: 32401 RVA: 0x001B1D44 File Offset: 0x001AFF44
		public double Second
		{
			get
			{
				return DurationValue.CalculateSeconds(this.dateTime.TimeOfDay);
			}
		}

		// Token: 0x1700224F RID: 8783
		// (get) Token: 0x06007E92 RID: 32402 RVA: 0x0000240C File Offset: 0x0000060C
		public sealed override ValueKind Kind
		{
			get
			{
				return ValueKind.DateTime;
			}
		}

		// Token: 0x17002250 RID: 8784
		// (get) Token: 0x06007E93 RID: 32403 RVA: 0x00002139 File Offset: 0x00000339
		public override bool IsDateTime
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002251 RID: 8785
		// (get) Token: 0x06007E94 RID: 32404 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public override DateTimeValue AsDateTime
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17002252 RID: 8786
		// (get) Token: 0x06007E95 RID: 32405 RVA: 0x001B1D64 File Offset: 0x001AFF64
		public override TypeValue Type
		{
			get
			{
				return TypeValue.DateTime;
			}
		}

		// Token: 0x06007E96 RID: 32406 RVA: 0x001B1D6B File Offset: 0x001AFF6B
		protected DateTimeValue(DateTime dateTime)
		{
			this.dateTime = new DateTime(dateTime.Ticks);
		}

		// Token: 0x06007E97 RID: 32407 RVA: 0x001B1D88 File Offset: 0x001AFF88
		private DateTimeValue(long ticks)
		{
			try
			{
				this.dateTime = new DateTime(ticks);
			}
			catch (ArgumentOutOfRangeException ex)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.DateTime_OutOfRangeError, Value.Null, ex);
			}
		}

		// Token: 0x06007E98 RID: 32408 RVA: 0x001B1DCC File Offset: 0x001AFFCC
		public static DateTimeValue New(DateTime dateTime)
		{
			return new DateTimeValue(dateTime);
		}

		// Token: 0x06007E99 RID: 32409 RVA: 0x001B1DD4 File Offset: 0x001AFFD4
		public static DateTimeValue New(long ticks)
		{
			return new DateTimeValue(ticks);
		}

		// Token: 0x06007E9A RID: 32410 RVA: 0x001B1DDC File Offset: 0x001AFFDC
		public static DateTimeValue New(int year, int month, int day, int hour, int minute, double second)
		{
			return new DateTimeValue(DateTimeValue.ToDateTime(year, month, day, hour, minute, second));
		}

		// Token: 0x06007E9B RID: 32411 RVA: 0x001B1DF0 File Offset: 0x001AFFF0
		public static DateTime ToDateTime(int year, int month, int day, int hour, int minute, double second)
		{
			if (second < 0.0)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.DateTime_OutOfRangeError, null, null);
			}
			long num = TimeValue.ConvertFractionalPartOfSecondToTicks(second);
			if (num >= 10000000L)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.DateTime_OutOfRangeError, null, null);
			}
			DateTime dateTime;
			try
			{
				dateTime = new DateTime(year, month, day, hour, minute, (int)second);
				dateTime = dateTime.AddTicks(num);
			}
			catch (ArgumentOutOfRangeException ex)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.DateTime_OutOfRangeError, Value.Null, ex);
			}
			return dateTime;
		}

		// Token: 0x06007E9C RID: 32412 RVA: 0x001B1E74 File Offset: 0x001B0074
		public static bool TryParseFromText(string text, CultureInfo formatProvider, out DateTimeValue instant)
		{
			return InstantParser.TryParseDateTime(text, formatProvider, out instant);
		}

		// Token: 0x06007E9D RID: 32413 RVA: 0x001B1E80 File Offset: 0x001B0080
		public RecordValue ToRecord()
		{
			return RecordValue.New(DateTimeValue.RecordFormatKeys, new Value[]
			{
				NumberValue.New(this.Year),
				NumberValue.New(this.Month),
				NumberValue.New(this.Day),
				NumberValue.New(this.Hour),
				NumberValue.New(this.Minute),
				NumberValue.New(this.Second)
			});
		}

		// Token: 0x06007E9E RID: 32414 RVA: 0x001B1EF4 File Offset: 0x001B00F4
		public override string ToSource()
		{
			return string.Format(CultureInfo.InvariantCulture, "#datetime({0}, {1}, {2}, {3}, {4}, {5})", new object[] { this.Year, this.Month, this.Day, this.Hour, this.Minute, this.Second });
		}

		// Token: 0x06007E9F RID: 32415 RVA: 0x001B1F6A File Offset: 0x001B016A
		public override object ToOleDb(Type type)
		{
			if (type == typeof(DateTime) || type == typeof(object))
			{
				return this.AsDateTime.AsClrDateTime;
			}
			throw ValueMarshaller.CreateTypeError(this, type);
		}

		// Token: 0x06007EA0 RID: 32416 RVA: 0x001B1FA8 File Offset: 0x001B01A8
		public override string ToString()
		{
			string text;
			try
			{
				text = this.dateTime.ToString(CultureInfo.CurrentCulture);
			}
			catch (ArgumentOutOfRangeException)
			{
				throw ValueException.NewDataFormatError<Message0>(Strings.Date_NotConvertibleToDate, this, null);
			}
			return text;
		}

		// Token: 0x06007EA1 RID: 32417 RVA: 0x001B1FEC File Offset: 0x001B01EC
		public string ToString(IFormatProvider formatProvider)
		{
			string text;
			try
			{
				text = this.dateTime.ToString(formatProvider);
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

		// Token: 0x06007EA2 RID: 32418 RVA: 0x001B2050 File Offset: 0x001B0250
		public string ToString(string format, IFormatProvider formatProvider)
		{
			string text;
			try
			{
				string text2;
				text = this.dateTime.ToString(IsoFormats.DateTimeIsoToClrFormatMap.TryGetValue(format, out text2) ? text2 : format, formatProvider);
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

		// Token: 0x17002253 RID: 8787
		// (get) Token: 0x06007EA3 RID: 32419 RVA: 0x001B20E8 File Offset: 0x001B02E8
		public DateTime AsClrDateTime
		{
			get
			{
				return this.dateTime;
			}
		}

		// Token: 0x06007EA4 RID: 32420 RVA: 0x001B20F0 File Offset: 0x001B02F0
		public override bool Equals(Value value, _ValueComparer comparer)
		{
			return value.IsDateTime && this.dateTime == value.AsDateTime.dateTime;
		}

		// Token: 0x06007EA5 RID: 32421 RVA: 0x001B2114 File Offset: 0x001B0314
		public override int GetHashCode(_ValueComparer comparer)
		{
			return this.dateTime.GetHashCode();
		}

		// Token: 0x06007EA6 RID: 32422 RVA: 0x001B2130 File Offset: 0x001B0330
		public override int CompareTo(Value value, _ValueComparer comparer)
		{
			if (value.IsDateTime)
			{
				return this.dateTime.CompareTo(value.AsDateTime.dateTime);
			}
			return base.CompareTo(value, comparer);
		}

		// Token: 0x06007EA7 RID: 32423 RVA: 0x001B2167 File Offset: 0x001B0367
		public Value StartOfYear()
		{
			return new DateTimeValue(new DateTime(this.Year, 1, 1));
		}

		// Token: 0x06007EA8 RID: 32424 RVA: 0x001B217B File Offset: 0x001B037B
		public Value StartOfQuarter()
		{
			return new DateTimeValue(this.dateTime.StartOfQuarter());
		}

		// Token: 0x06007EA9 RID: 32425 RVA: 0x001B218D File Offset: 0x001B038D
		public Value StartOfMonth()
		{
			return new DateTimeValue(new DateTime(this.Year, this.Month, 1));
		}

		// Token: 0x06007EAA RID: 32426 RVA: 0x001B21A6 File Offset: 0x001B03A6
		public Value StartOfDay()
		{
			return new DateTimeValue(new DateTime(this.Year, this.Month, this.Day));
		}

		// Token: 0x06007EAB RID: 32427 RVA: 0x001B21C4 File Offset: 0x001B03C4
		public Value StartOfWeek(DayOfWeek firstDayOfWeek)
		{
			int num = -1 * (this.dateTime.DayOfWeek - firstDayOfWeek + 7) % 7;
			DateTime dateTime = new DateTime(this.Year, this.Month, this.Day, 0, 0, 0, 0);
			try
			{
				dateTime = dateTime.AddDays((double)num);
			}
			catch (ArgumentException ex)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.DateTime_OutOfRangeError, Value.Null, ex);
			}
			return new DateTimeValue(dateTime);
		}

		// Token: 0x06007EAC RID: 32428 RVA: 0x001B223C File Offset: 0x001B043C
		public DateTimeValue StartOfHour()
		{
			return new DateTimeValue(new DateTime(this.Year, this.Month, this.Day, this.Hour, 0, 0, 0));
		}

		// Token: 0x06007EAD RID: 32429 RVA: 0x001B2264 File Offset: 0x001B0464
		public DateTimeValue EndOfYear()
		{
			return new DateTimeValue(new DateTime(this.Year, 12, 31, 23, 59, 59).AddTicks(TimeValue.MaxSecondFractionInTicks));
		}

		// Token: 0x06007EAE RID: 32430 RVA: 0x001B2298 File Offset: 0x001B0498
		public Value EndOfQuarter()
		{
			return new DateTimeValue(this.dateTime.EndOfQuarter());
		}

		// Token: 0x06007EAF RID: 32431 RVA: 0x001B22AC File Offset: 0x001B04AC
		public DateTimeValue EndOfMonth()
		{
			return new DateTimeValue(new DateTime(this.Year, this.Month, DateValue.Calendar.GetDaysInMonth(this.Year, this.Month), 23, 59, 59).AddTicks(TimeValue.MaxSecondFractionInTicks));
		}

		// Token: 0x06007EB0 RID: 32432 RVA: 0x001B22F8 File Offset: 0x001B04F8
		public DateTimeValue EndOfDay()
		{
			return new DateTimeValue(new DateTime(this.Year, this.Month, this.Day, 23, 59, 59).AddTicks(TimeValue.MaxSecondFractionInTicks));
		}

		// Token: 0x06007EB1 RID: 32433 RVA: 0x001B2334 File Offset: 0x001B0534
		public DateTimeValue EndOfWeek(DayOfWeek lastDayOfWeek)
		{
			int num = (lastDayOfWeek - this.dateTime.DayOfWeek + 7) % 7;
			DateTime dateTime = new DateTime(this.Year, this.Month, this.Day, 23, 59, 59).AddTicks(TimeValue.MaxSecondFractionInTicks);
			try
			{
				dateTime = dateTime.AddDays((double)num);
			}
			catch (ArgumentException ex)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.DateTime_OutOfRangeError, Value.Null, ex);
			}
			return DateTimeValue.New(dateTime);
		}

		// Token: 0x06007EB2 RID: 32434 RVA: 0x001B23B8 File Offset: 0x001B05B8
		public DateTimeValue EndOfHour()
		{
			return new DateTimeValue(new DateTime(this.Year, this.Month, this.Day, this.Hour, 59, 59).AddTicks(TimeValue.MaxSecondFractionInTicks));
		}

		// Token: 0x06007EB3 RID: 32435 RVA: 0x001B23F8 File Offset: 0x001B05F8
		public DateTimeZoneValue SetOffset(double hours, int minutes)
		{
			int num = (int)(hours * 60.0) + minutes;
			DateTimeOffset dateTimeOffset;
			try
			{
				int year = this.AsClrDateTime.Year;
				int month = this.AsClrDateTime.Month;
				int day = this.AsClrDateTime.Day;
				int hour = this.AsClrDateTime.Hour;
				int minute = this.AsClrDateTime.Minute;
				int second = this.AsClrDateTime.Second;
				DateTime dateTime = new DateTime(year, month, day, hour, minute, second);
				long num2 = this.AsClrDateTime.Ticks - dateTime.Ticks;
				dateTimeOffset = new DateTimeOffset(dateTime, new TimeSpan(0, num, 0)).AddTicks(num2);
			}
			catch (ArgumentOutOfRangeException ex)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.DateTimeZone_OutOfRangeError, Value.Null, ex);
			}
			return DateTimeZoneValue.New(dateTimeOffset);
		}

		// Token: 0x06007EB4 RID: 32436 RVA: 0x001B24EC File Offset: 0x001B06EC
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

		// Token: 0x06007EB5 RID: 32437 RVA: 0x001B2521 File Offset: 0x001B0721
		public override Value Add(Value value, Precision precision)
		{
			return this.Add(value);
		}

		// Token: 0x06007EB6 RID: 32438 RVA: 0x001B252C File Offset: 0x001B072C
		public override Value Subtract(Value value)
		{
			ValueKind kind = value.Kind;
			if (kind == ValueKind.Null)
			{
				return Value.Null;
			}
			if (kind == ValueKind.DateTime)
			{
				return this.Subtract(value.AsDateTime);
			}
			if (kind != ValueKind.Duration)
			{
				return base.Subtract(value);
			}
			return this.Subtract(value.AsDuration);
		}

		// Token: 0x06007EB7 RID: 32439 RVA: 0x001B2574 File Offset: 0x001B0774
		public override Value Subtract(Value value, Precision precision)
		{
			return this.Subtract(value);
		}

		// Token: 0x06007EB8 RID: 32440 RVA: 0x001B2580 File Offset: 0x001B0780
		public DateTimeValue Add(DurationValue interval)
		{
			DateTimeValue dateTimeValue;
			try
			{
				dateTimeValue = DateTimeValue.New(this.dateTime.Add(interval.AsTimeSpan));
			}
			catch (ArgumentOutOfRangeException ex)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.DateTime_OutOfRangeError, Value.Null, ex);
			}
			return dateTimeValue;
		}

		// Token: 0x06007EB9 RID: 32441 RVA: 0x001B25CC File Offset: 0x001B07CC
		public DurationValue Subtract(DateTimeValue instant)
		{
			DurationValue durationValue;
			try
			{
				durationValue = new DurationValue(this.dateTime - instant.AsClrDateTime);
			}
			catch (ArgumentOutOfRangeException ex)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.DateTime_OutOfRangeError, Value.Null, ex);
			}
			return durationValue;
		}

		// Token: 0x06007EBA RID: 32442 RVA: 0x001B2618 File Offset: 0x001B0818
		public Value Subtract(DurationValue interval)
		{
			return this.Add(interval.Negate());
		}

		// Token: 0x06007EBB RID: 32443 RVA: 0x001B2628 File Offset: 0x001B0828
		public DateTimeValue AddDays(int days)
		{
			DateTime dateTime;
			try
			{
				dateTime = this.dateTime.AddDays((double)days);
			}
			catch (ArgumentException ex)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.DateTime_OutOfRangeError, Value.Null, ex);
			}
			return new DateTimeValue(dateTime);
		}

		// Token: 0x06007EBC RID: 32444 RVA: 0x001B2670 File Offset: 0x001B0870
		public DateTimeValue AddWeeks(int weeks)
		{
			DateTime dateTime;
			try
			{
				dateTime = this.dateTime.AddWeeks(weeks);
			}
			catch (ArgumentException ex)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.Date_OutOfRangeError, Value.Null, ex);
			}
			return new DateTimeValue(dateTime);
		}

		// Token: 0x06007EBD RID: 32445 RVA: 0x001B26B4 File Offset: 0x001B08B4
		public DateTimeValue AddMonths(int months)
		{
			DateTime dateTime;
			try
			{
				dateTime = this.dateTime.AddMonths(months);
			}
			catch (ArgumentException ex)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.DateTime_OutOfRangeError, Value.Null, ex);
			}
			return new DateTimeValue(dateTime);
		}

		// Token: 0x06007EBE RID: 32446 RVA: 0x001B26FC File Offset: 0x001B08FC
		public DateTimeValue AddQuarters(int quarters)
		{
			DateTime dateTime;
			try
			{
				dateTime = this.dateTime.AddQuarters(quarters);
			}
			catch (ArgumentException ex)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.DateTime_OutOfRangeError, Value.Null, ex);
			}
			return new DateTimeValue(dateTime);
		}

		// Token: 0x06007EBF RID: 32447 RVA: 0x001B2740 File Offset: 0x001B0940
		public DateTimeValue AddYears(int years)
		{
			DateTime dateTime;
			try
			{
				dateTime = this.dateTime.AddYears(years);
			}
			catch (ArgumentException ex)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.DateTime_OutOfRangeError, Value.Null, ex);
			}
			return new DateTimeValue(dateTime);
		}

		// Token: 0x06007EC0 RID: 32448 RVA: 0x001B2788 File Offset: 0x001B0988
		public DayOfWeek DayOfWeek(DayOfWeek firstDayOfWeek)
		{
			return (this.AsClrDateTime.DayOfWeek - (int)firstDayOfWeek + 7) % (DayOfWeek)7;
		}

		// Token: 0x06007EC1 RID: 32449 RVA: 0x001B27A9 File Offset: 0x001B09A9
		private static DateTimeValue New(DateTimeValue value, RecordValue metaValue, TypeValue type)
		{
			if (value.MetaValue == metaValue && value.Type == type)
			{
				return value;
			}
			return new DateTimeValue.MetaTypeDateTimeWithoutTimezoneValue(value, metaValue, type);
		}

		// Token: 0x06007EC2 RID: 32450 RVA: 0x001B27C7 File Offset: 0x001B09C7
		public sealed override Value NewType(TypeValue type)
		{
			return DateTimeValue.New(this, this.MetaValue, type);
		}

		// Token: 0x06007EC3 RID: 32451 RVA: 0x001B27D6 File Offset: 0x001B09D6
		public sealed override Value NewMeta(RecordValue metaValue)
		{
			return DateTimeValue.New(this, metaValue, this.Type);
		}

		// Token: 0x17002254 RID: 8788
		// (get) Token: 0x06007EC4 RID: 32452 RVA: 0x00019E61 File Offset: 0x00018061
		public override RecordValue MetaValue
		{
			get
			{
				return RecordValue.Empty;
			}
		}

		// Token: 0x0400458E RID: 17806
		private const string SourcePattern = "#datetime({0}, {1}, {2}, {3}, {4}, {5})";

		// Token: 0x0400458F RID: 17807
		private static readonly Keys RecordFormatKeys = Keys.New(new string[] { "Year", "Month", "Day", "Hour", "Minute", "Second" });

		// Token: 0x04004590 RID: 17808
		private readonly DateTime dateTime;

		// Token: 0x020012D0 RID: 4816
		private class MetaTypeDateTimeWithoutTimezoneValue : DateTimeValue
		{
			// Token: 0x06007EC6 RID: 32454 RVA: 0x001B2835 File Offset: 0x001B0A35
			public MetaTypeDateTimeWithoutTimezoneValue(DateTimeValue value, RecordValue meta, TypeValue type)
				: base(value.dateTime)
			{
				this.meta = meta;
				this.type = type;
			}

			// Token: 0x17002255 RID: 8789
			// (get) Token: 0x06007EC7 RID: 32455 RVA: 0x001B2851 File Offset: 0x001B0A51
			public override TypeValue Type
			{
				get
				{
					return this.type;
				}
			}

			// Token: 0x17002256 RID: 8790
			// (get) Token: 0x06007EC8 RID: 32456 RVA: 0x001B2859 File Offset: 0x001B0A59
			public override RecordValue MetaValue
			{
				get
				{
					return this.meta;
				}
			}

			// Token: 0x04004591 RID: 17809
			private RecordValue meta;

			// Token: 0x04004592 RID: 17810
			private TypeValue type;
		}
	}
}
