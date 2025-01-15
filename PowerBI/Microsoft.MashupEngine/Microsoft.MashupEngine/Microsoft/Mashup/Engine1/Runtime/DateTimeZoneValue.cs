using System;
using System.Globalization;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020012D1 RID: 4817
	public class DateTimeZoneValue : PrimitiveValue, IDateTimeZone, IValue
	{
		// Token: 0x17002257 RID: 8791
		// (get) Token: 0x06007EC9 RID: 32457 RVA: 0x001B2864 File Offset: 0x001B0A64
		public int Year
		{
			get
			{
				return this.dateTimeOffset.Year;
			}
		}

		// Token: 0x17002258 RID: 8792
		// (get) Token: 0x06007ECA RID: 32458 RVA: 0x001B2880 File Offset: 0x001B0A80
		public int Month
		{
			get
			{
				return this.dateTimeOffset.Month;
			}
		}

		// Token: 0x17002259 RID: 8793
		// (get) Token: 0x06007ECB RID: 32459 RVA: 0x001B289C File Offset: 0x001B0A9C
		public int Day
		{
			get
			{
				return this.dateTimeOffset.Day;
			}
		}

		// Token: 0x1700225A RID: 8794
		// (get) Token: 0x06007ECC RID: 32460 RVA: 0x001B28B8 File Offset: 0x001B0AB8
		public int Hour
		{
			get
			{
				return this.dateTimeOffset.Hour;
			}
		}

		// Token: 0x1700225B RID: 8795
		// (get) Token: 0x06007ECD RID: 32461 RVA: 0x001B28D4 File Offset: 0x001B0AD4
		public int Minute
		{
			get
			{
				return this.dateTimeOffset.Minute;
			}
		}

		// Token: 0x1700225C RID: 8796
		// (get) Token: 0x06007ECE RID: 32462 RVA: 0x001B28F0 File Offset: 0x001B0AF0
		public double Second
		{
			get
			{
				return DurationValue.CalculateSeconds(this.dateTimeOffset.TimeOfDay);
			}
		}

		// Token: 0x1700225D RID: 8797
		// (get) Token: 0x06007ECF RID: 32463 RVA: 0x001B2910 File Offset: 0x001B0B10
		public int OffsetHours
		{
			get
			{
				return this.dateTimeOffset.Offset.Hours;
			}
		}

		// Token: 0x1700225E RID: 8798
		// (get) Token: 0x06007ED0 RID: 32464 RVA: 0x001B2934 File Offset: 0x001B0B34
		public int OffsetMinutes
		{
			get
			{
				return this.dateTimeOffset.Offset.Minutes;
			}
		}

		// Token: 0x06007ED1 RID: 32465 RVA: 0x001B2958 File Offset: 0x001B0B58
		protected DateTimeZoneValue(DateTimeOffset dateTimeOffset)
			: this(dateTimeOffset.Ticks, (int)dateTimeOffset.Offset.TotalMinutes)
		{
		}

		// Token: 0x06007ED2 RID: 32466 RVA: 0x001B2984 File Offset: 0x001B0B84
		private DateTimeZoneValue(long ticks, int offsetInMinutes)
		{
			try
			{
				this.dateTimeOffset = new DateTimeOffset(ticks, new TimeSpan(0, offsetInMinutes, 0));
			}
			catch (ArgumentOutOfRangeException ex)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.DateTimeZone_OutOfRangeError, Value.Null, ex);
			}
		}

		// Token: 0x06007ED3 RID: 32467 RVA: 0x001B29D0 File Offset: 0x001B0BD0
		public RecordValue ToRecord()
		{
			return RecordValue.New(DateTimeZoneValue.RecordFormatKeys, new Value[]
			{
				NumberValue.New(this.Year),
				NumberValue.New(this.Month),
				NumberValue.New(this.Day),
				NumberValue.New(this.Hour),
				NumberValue.New(this.Minute),
				NumberValue.New(this.Second),
				NumberValue.New(this.OffsetHours),
				NumberValue.New(this.OffsetMinutes)
			});
		}

		// Token: 0x06007ED4 RID: 32468 RVA: 0x001B2A5D File Offset: 0x001B0C5D
		public static DateTimeZoneValue New(DateTimeOffset instant)
		{
			return new DateTimeZoneValue(instant);
		}

		// Token: 0x06007ED5 RID: 32469 RVA: 0x001B2A65 File Offset: 0x001B0C65
		public static DateTimeZoneValue New(long ticks, int offsetInMinutes)
		{
			return new DateTimeZoneValue(ticks, offsetInMinutes);
		}

		// Token: 0x06007ED6 RID: 32470 RVA: 0x001B2A70 File Offset: 0x001B0C70
		public static DateTimeZoneValue New(int year, int month, int day, int hour, int minute, double second, double offsetInHours, double offsetInMinutes)
		{
			DateTime dateTime = DateTimeValue.ToDateTime(year, month, day, hour, minute, second);
			int num = (int)(offsetInHours * 60.0 + offsetInMinutes);
			DateTimeZoneValue dateTimeZoneValue;
			try
			{
				dateTimeZoneValue = new DateTimeZoneValue(new DateTimeOffset(dateTime, new TimeSpan(0, num, 0)));
			}
			catch (ArgumentOutOfRangeException ex)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.DateTimeZone_OutOfRangeError, Value.Null, ex);
			}
			return dateTimeZoneValue;
		}

		// Token: 0x06007ED7 RID: 32471 RVA: 0x001B2AD8 File Offset: 0x001B0CD8
		public static bool TryParseFromText(string text, CultureInfo formatProvider, out DateTimeZoneValue instant)
		{
			return InstantParser.TryParseDateTimeOffset(text, formatProvider, out instant);
		}

		// Token: 0x1700225F RID: 8799
		// (get) Token: 0x06007ED8 RID: 32472 RVA: 0x0000244F File Offset: 0x0000064F
		public sealed override ValueKind Kind
		{
			get
			{
				return ValueKind.DateTimeZone;
			}
		}

		// Token: 0x17002260 RID: 8800
		// (get) Token: 0x06007ED9 RID: 32473 RVA: 0x00002139 File Offset: 0x00000339
		public override bool IsDateTimeZone
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002261 RID: 8801
		// (get) Token: 0x06007EDA RID: 32474 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public override DateTimeZoneValue AsDateTimeZone
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17002262 RID: 8802
		// (get) Token: 0x06007EDB RID: 32475 RVA: 0x001B2AE2 File Offset: 0x001B0CE2
		public override TypeValue Type
		{
			get
			{
				return TypeValue.DateTimeZone;
			}
		}

		// Token: 0x06007EDC RID: 32476 RVA: 0x001B2AEC File Offset: 0x001B0CEC
		public override string ToSource()
		{
			return string.Format(CultureInfo.InvariantCulture, "#datetimezone({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7})", new object[] { this.Year, this.Month, this.Day, this.Hour, this.Minute, this.Second, this.OffsetHours, this.OffsetMinutes });
		}

		// Token: 0x06007EDD RID: 32477 RVA: 0x001B2B7E File Offset: 0x001B0D7E
		public override object ToOleDb(Type type)
		{
			if (type == typeof(DateTimeOffset) || type == typeof(object))
			{
				return this.AsDateTimeZone.AsClrDateTimeOffset;
			}
			throw ValueMarshaller.CreateTypeError(this, type);
		}

		// Token: 0x06007EDE RID: 32478 RVA: 0x001B2BBC File Offset: 0x001B0DBC
		public override string ToString()
		{
			string text;
			try
			{
				text = this.dateTimeOffset.ToString();
			}
			catch (ArgumentOutOfRangeException)
			{
				throw ValueException.NewDataFormatError<Message0>(Strings.Date_NotConvertibleToDate, this, null);
			}
			return text;
		}

		// Token: 0x06007EDF RID: 32479 RVA: 0x001B2C00 File Offset: 0x001B0E00
		public string ToString(IFormatProvider formatProvider)
		{
			string text;
			try
			{
				text = this.dateTimeOffset.ToString(formatProvider);
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

		// Token: 0x06007EE0 RID: 32480 RVA: 0x001B2C64 File Offset: 0x001B0E64
		public string ToString(string format, IFormatProvider formatProvider)
		{
			string text;
			try
			{
				string text2;
				text = this.dateTimeOffset.ToString(IsoFormats.DateTimeZoneIsoToClrFormatMap.TryGetValue(format, out text2) ? text2 : format, formatProvider);
			}
			catch (FormatException ex)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.DateTimeZone_InvalidOutputFormatError, TextValue.New(format), ex);
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

		// Token: 0x17002263 RID: 8803
		// (get) Token: 0x06007EE1 RID: 32481 RVA: 0x001B2CFC File Offset: 0x001B0EFC
		public DateTimeOffset AsClrDateTimeOffset
		{
			get
			{
				return this.dateTimeOffset;
			}
		}

		// Token: 0x06007EE2 RID: 32482 RVA: 0x001B2D04 File Offset: 0x001B0F04
		public override bool Equals(Value value, _ValueComparer comparer)
		{
			return value.IsDateTimeZone && this.dateTimeOffset == value.AsDateTimeZone.dateTimeOffset;
		}

		// Token: 0x06007EE3 RID: 32483 RVA: 0x001B2D28 File Offset: 0x001B0F28
		public override int GetHashCode(_ValueComparer comparer)
		{
			return this.dateTimeOffset.GetHashCode();
		}

		// Token: 0x06007EE4 RID: 32484 RVA: 0x001B2D4C File Offset: 0x001B0F4C
		public override int CompareTo(Value value, _ValueComparer comparer)
		{
			if (value.IsDateTimeZone)
			{
				return this.dateTimeOffset.CompareTo(value.AsDateTimeZone.dateTimeOffset);
			}
			return base.CompareTo(value, comparer);
		}

		// Token: 0x06007EE5 RID: 32485 RVA: 0x001B2D84 File Offset: 0x001B0F84
		public Value StartOfYear()
		{
			return new DateTimeZoneValue(new DateTimeOffset(this.Year, 1, 1, 0, 0, 0, 0, this.dateTimeOffset.Offset));
		}

		// Token: 0x06007EE6 RID: 32486 RVA: 0x001B2DB8 File Offset: 0x001B0FB8
		public Value StartOfQuarter()
		{
			DateTime dateTime = this.dateTimeOffset.DateTime.StartOfQuarter();
			TimeSpan offset = this.dateTimeOffset.Offset;
			return new DateTimeZoneValue(new DateTimeOffset(dateTime, offset));
		}

		// Token: 0x06007EE7 RID: 32487 RVA: 0x001B2DF4 File Offset: 0x001B0FF4
		public Value StartOfMonth()
		{
			return new DateTimeZoneValue(new DateTimeOffset(this.Year, this.Month, 1, 0, 0, 0, 0, this.dateTimeOffset.Offset));
		}

		// Token: 0x06007EE8 RID: 32488 RVA: 0x001B2E2C File Offset: 0x001B102C
		public Value StartOfDay()
		{
			return new DateTimeZoneValue(new DateTimeOffset(this.Year, this.Month, this.Day, 0, 0, 0, 0, this.dateTimeOffset.Offset));
		}

		// Token: 0x06007EE9 RID: 32489 RVA: 0x001B2E68 File Offset: 0x001B1068
		public Value StartOfWeek(DayOfWeek firstDayOfWeek)
		{
			int num = -1 * (this.dateTimeOffset.DayOfWeek - firstDayOfWeek + 7) % 7;
			DateTimeOffset dateTimeOffset = new DateTimeOffset(this.Year, this.Month, this.Day, 0, 0, 0, 0, this.dateTimeOffset.Offset);
			try
			{
				dateTimeOffset = dateTimeOffset.AddDays((double)num);
			}
			catch (ArgumentException ex)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.DateTimeZone_OutOfRangeError, Value.Null, ex);
			}
			return new DateTimeZoneValue(dateTimeOffset);
		}

		// Token: 0x06007EEA RID: 32490 RVA: 0x001B2EEC File Offset: 0x001B10EC
		public DateTimeZoneValue StartOfHour()
		{
			return new DateTimeZoneValue(new DateTimeOffset(this.Year, this.Month, this.Day, this.Hour, 0, 0, 0, this.dateTimeOffset.Offset));
		}

		// Token: 0x06007EEB RID: 32491 RVA: 0x001B2F2C File Offset: 0x001B112C
		public DateTimeZoneValue EndOfYear()
		{
			return new DateTimeZoneValue(new DateTimeOffset(this.Year, 12, 31, 23, 59, 59, this.dateTimeOffset.Offset).AddTicks(TimeValue.MaxSecondFractionInTicks));
		}

		// Token: 0x06007EEC RID: 32492 RVA: 0x001B2F70 File Offset: 0x001B1170
		public Value EndOfQuarter()
		{
			DateTime dateTime = this.dateTimeOffset.DateTime.EndOfQuarter();
			TimeSpan offset = this.dateTimeOffset.Offset;
			return new DateTimeZoneValue(new DateTimeOffset(dateTime, offset));
		}

		// Token: 0x06007EED RID: 32493 RVA: 0x001B2FAC File Offset: 0x001B11AC
		public DateTimeZoneValue EndOfMonth()
		{
			return new DateTimeZoneValue(new DateTimeOffset(this.Year, this.Month, DateValue.Calendar.GetDaysInMonth(this.Year, this.Month), 23, 59, 59, this.dateTimeOffset.Offset).AddTicks(TimeValue.MaxSecondFractionInTicks));
		}

		// Token: 0x06007EEE RID: 32494 RVA: 0x001B3008 File Offset: 0x001B1208
		public DateTimeZoneValue EndOfDay()
		{
			return new DateTimeZoneValue(new DateTimeOffset(this.Year, this.Month, this.Day, 23, 59, 59, this.dateTimeOffset.Offset).AddTicks(TimeValue.MaxSecondFractionInTicks));
		}

		// Token: 0x06007EEF RID: 32495 RVA: 0x001B3054 File Offset: 0x001B1254
		public DateTimeZoneValue EndOfWeek(DayOfWeek lastDayOfWeek)
		{
			int num = (lastDayOfWeek - this.dateTimeOffset.DayOfWeek + 7) % 7;
			DateTimeOffset dateTimeOffset = new DateTimeOffset(this.dateTimeOffset.Year, this.dateTimeOffset.Month, this.dateTimeOffset.Day, 23, 59, 59, this.dateTimeOffset.Offset).AddTicks(TimeValue.MaxSecondFractionInTicks);
			try
			{
				dateTimeOffset = dateTimeOffset.AddDays((double)num);
			}
			catch (ArgumentException ex)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.DateTimeZone_OutOfRangeError, Value.Null, ex);
			}
			return DateTimeZoneValue.New(dateTimeOffset);
		}

		// Token: 0x06007EF0 RID: 32496 RVA: 0x001B30FC File Offset: 0x001B12FC
		public DateTimeZoneValue EndOfHour()
		{
			return DateTimeZoneValue.New(new DateTimeOffset(this.Year, this.Month, this.Day, this.Hour, 59, 59, this.dateTimeOffset.Offset).AddTicks(TimeValue.MaxSecondFractionInTicks));
		}

		// Token: 0x06007EF1 RID: 32497 RVA: 0x001B314C File Offset: 0x001B134C
		public DateTimeZoneValue SwitchOffset(double hours, int minutes)
		{
			int num = (int)(hours * 60.0) + minutes;
			DateTimeZoneValue dateTimeZoneValue;
			try
			{
				dateTimeZoneValue = DateTimeZoneValue.New(this.AsClrDateTimeOffset.ToOffset(new TimeSpan(0, num, 0)));
			}
			catch (ArgumentOutOfRangeException ex)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.DateTimeZone_OutOfRangeError, Value.Null, ex);
			}
			return dateTimeZoneValue;
		}

		// Token: 0x06007EF2 RID: 32498 RVA: 0x001B31AC File Offset: 0x001B13AC
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

		// Token: 0x06007EF3 RID: 32499 RVA: 0x001B2521 File Offset: 0x001B0721
		public override Value Add(Value value, Precision precision)
		{
			return this.Add(value);
		}

		// Token: 0x06007EF4 RID: 32500 RVA: 0x001B31E4 File Offset: 0x001B13E4
		public override Value Subtract(Value value)
		{
			ValueKind kind = value.Kind;
			if (kind == ValueKind.Null)
			{
				return Value.Null;
			}
			if (kind == ValueKind.DateTimeZone)
			{
				return this.Subtract(value.AsDateTimeZone);
			}
			if (kind != ValueKind.Duration)
			{
				return base.Subtract(value);
			}
			return this.Subtract(value.AsDuration);
		}

		// Token: 0x06007EF5 RID: 32501 RVA: 0x001B2574 File Offset: 0x001B0774
		public override Value Subtract(Value value, Precision precision)
		{
			return this.Subtract(value);
		}

		// Token: 0x06007EF6 RID: 32502 RVA: 0x001B322C File Offset: 0x001B142C
		public DateTimeZoneValue Add(DurationValue interval)
		{
			DateTimeZoneValue dateTimeZoneValue;
			try
			{
				dateTimeZoneValue = DateTimeZoneValue.New(this.dateTimeOffset.Add(interval.AsTimeSpan));
			}
			catch (ArgumentOutOfRangeException ex)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.DateTimeZone_OutOfRangeError, Value.Null, ex);
			}
			return dateTimeZoneValue;
		}

		// Token: 0x06007EF7 RID: 32503 RVA: 0x001B3278 File Offset: 0x001B1478
		public DurationValue Subtract(DateTimeZoneValue instant)
		{
			DurationValue durationValue;
			try
			{
				durationValue = new DurationValue(this.dateTimeOffset - instant.AsClrDateTimeOffset);
			}
			catch (ArgumentOutOfRangeException ex)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.DateTimeZone_OutOfRangeError, Value.Null, ex);
			}
			return durationValue;
		}

		// Token: 0x06007EF8 RID: 32504 RVA: 0x001B2618 File Offset: 0x001B0818
		public Value Subtract(DurationValue interval)
		{
			return this.Add(interval.Negate());
		}

		// Token: 0x06007EF9 RID: 32505 RVA: 0x001B32C4 File Offset: 0x001B14C4
		public DateTimeZoneValue AddDays(int days)
		{
			TimeSpan offset = this.dateTimeOffset.Offset;
			DateTime dateTime;
			try
			{
				dateTime = this.dateTimeOffset.DateTime.AddDays((double)days);
			}
			catch (ArgumentException ex)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.Date_OutOfRangeError, Value.Null, ex);
			}
			return new DateTimeZoneValue(new DateTimeOffset(dateTime, offset));
		}

		// Token: 0x06007EFA RID: 32506 RVA: 0x001B332C File Offset: 0x001B152C
		public DateTimeZoneValue AddWeeks(int weeks)
		{
			TimeSpan offset = this.dateTimeOffset.Offset;
			DateTime dateTime;
			try
			{
				dateTime = this.dateTimeOffset.DateTime.AddWeeks(weeks);
			}
			catch (ArgumentException ex)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.Date_OutOfRangeError, Value.Null, ex);
			}
			return new DateTimeZoneValue(new DateTimeOffset(dateTime, offset));
		}

		// Token: 0x06007EFB RID: 32507 RVA: 0x001B3390 File Offset: 0x001B1590
		public DateTimeZoneValue AddMonths(int months)
		{
			DateTimeOffset dateTimeOffset;
			try
			{
				dateTimeOffset = this.dateTimeOffset.AddMonths(months);
			}
			catch (ArgumentException ex)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.DateTimeZone_OutOfRangeError, Value.Null, ex);
			}
			return new DateTimeZoneValue(dateTimeOffset);
		}

		// Token: 0x06007EFC RID: 32508 RVA: 0x001B33D8 File Offset: 0x001B15D8
		public DateTimeZoneValue AddQuarters(int quarters)
		{
			TimeSpan offset = this.dateTimeOffset.Offset;
			DateTime dateTime;
			try
			{
				dateTime = this.dateTimeOffset.DateTime.AddQuarters(quarters);
			}
			catch (ArgumentException ex)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.Date_OutOfRangeError, Value.Null, ex);
			}
			return new DateTimeZoneValue(new DateTimeOffset(dateTime, offset));
		}

		// Token: 0x06007EFD RID: 32509 RVA: 0x001B343C File Offset: 0x001B163C
		public DateTimeZoneValue AddYears(int years)
		{
			DateTimeOffset dateTimeOffset;
			try
			{
				dateTimeOffset = this.dateTimeOffset.AddYears(years);
			}
			catch (ArgumentException ex)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.DateTimeZone_OutOfRangeError, Value.Null, ex);
			}
			return new DateTimeZoneValue(dateTimeOffset);
		}

		// Token: 0x06007EFE RID: 32510 RVA: 0x001B3484 File Offset: 0x001B1684
		public DayOfWeek DayOfWeek(DayOfWeek firstDayOfWeek)
		{
			return (this.AsClrDateTimeOffset.DayOfWeek - (int)firstDayOfWeek + 7) % (DayOfWeek)7;
		}

		// Token: 0x06007EFF RID: 32511 RVA: 0x001B34A5 File Offset: 0x001B16A5
		private static DateTimeZoneValue New(DateTimeZoneValue value, RecordValue metaValue, TypeValue type)
		{
			if (value.MetaValue == metaValue && value.Type == type)
			{
				return value;
			}
			return new DateTimeZoneValue.MetaTypeDateTimeWithTimezoneValue(value, metaValue, type);
		}

		// Token: 0x06007F00 RID: 32512 RVA: 0x001B34C3 File Offset: 0x001B16C3
		public sealed override Value NewType(TypeValue type)
		{
			return DateTimeZoneValue.New(this, this.MetaValue, type);
		}

		// Token: 0x06007F01 RID: 32513 RVA: 0x001B34D2 File Offset: 0x001B16D2
		public sealed override Value NewMeta(RecordValue metaValue)
		{
			return DateTimeZoneValue.New(this, metaValue, this.Type);
		}

		// Token: 0x17002264 RID: 8804
		// (get) Token: 0x06007F02 RID: 32514 RVA: 0x00019E61 File Offset: 0x00018061
		public override RecordValue MetaValue
		{
			get
			{
				return RecordValue.Empty;
			}
		}

		// Token: 0x04004593 RID: 17811
		private const string SourcePattern = "#datetimezone({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7})";

		// Token: 0x04004594 RID: 17812
		public const string ZoneHoursKey = "ZoneHours";

		// Token: 0x04004595 RID: 17813
		public const string ZoneMinutesKey = "ZoneMinutes";

		// Token: 0x04004596 RID: 17814
		private static readonly Keys RecordFormatKeys = Keys.New(new string[] { "Year", "Month", "Day", "Hour", "Minute", "Second", "ZoneHours", "ZoneMinutes" });

		// Token: 0x04004597 RID: 17815
		private readonly DateTimeOffset dateTimeOffset;

		// Token: 0x020012D2 RID: 4818
		private class MetaTypeDateTimeWithTimezoneValue : DateTimeZoneValue
		{
			// Token: 0x06007F04 RID: 32516 RVA: 0x001B3541 File Offset: 0x001B1741
			public MetaTypeDateTimeWithTimezoneValue(DateTimeZoneValue value, RecordValue meta, TypeValue type)
				: base(value.dateTimeOffset)
			{
				this.meta = meta;
				this.type = type;
			}

			// Token: 0x17002265 RID: 8805
			// (get) Token: 0x06007F05 RID: 32517 RVA: 0x001B355D File Offset: 0x001B175D
			public override TypeValue Type
			{
				get
				{
					return this.type;
				}
			}

			// Token: 0x17002266 RID: 8806
			// (get) Token: 0x06007F06 RID: 32518 RVA: 0x001B3565 File Offset: 0x001B1765
			public override RecordValue MetaValue
			{
				get
				{
					return this.meta;
				}
			}

			// Token: 0x04004598 RID: 17816
			private RecordValue meta;

			// Token: 0x04004599 RID: 17817
			private TypeValue type;
		}
	}
}
