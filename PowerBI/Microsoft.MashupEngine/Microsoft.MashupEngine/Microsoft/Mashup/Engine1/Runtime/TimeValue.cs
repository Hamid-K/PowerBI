using System;
using System.Globalization;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.OleDb;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001672 RID: 5746
	public class TimeValue : PrimitiveValue, ITimeValue, IValue
	{
		// Token: 0x17002608 RID: 9736
		// (get) Token: 0x0600914F RID: 37199 RVA: 0x001E2EB8 File Offset: 0x001E10B8
		public int Hour
		{
			get
			{
				return this.time.Hours;
			}
		}

		// Token: 0x17002609 RID: 9737
		// (get) Token: 0x06009150 RID: 37200 RVA: 0x001E2ED4 File Offset: 0x001E10D4
		public int Minute
		{
			get
			{
				return this.time.Minutes;
			}
		}

		// Token: 0x1700260A RID: 9738
		// (get) Token: 0x06009151 RID: 37201 RVA: 0x001E2EEF File Offset: 0x001E10EF
		public double Second
		{
			get
			{
				return DurationValue.CalculateSeconds(this.time);
			}
		}

		// Token: 0x1700260B RID: 9739
		// (get) Token: 0x06009152 RID: 37202 RVA: 0x00002139 File Offset: 0x00000339
		public sealed override ValueKind Kind
		{
			get
			{
				return ValueKind.Time;
			}
		}

		// Token: 0x1700260C RID: 9740
		// (get) Token: 0x06009153 RID: 37203 RVA: 0x00002139 File Offset: 0x00000339
		public override bool IsTime
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700260D RID: 9741
		// (get) Token: 0x06009154 RID: 37204 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public override TimeValue AsTime
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700260E RID: 9742
		// (get) Token: 0x06009155 RID: 37205 RVA: 0x001E2EFC File Offset: 0x001E10FC
		public override TypeValue Type
		{
			get
			{
				return TypeValue.Time;
			}
		}

		// Token: 0x06009156 RID: 37206 RVA: 0x001E2F03 File Offset: 0x001E1103
		protected TimeValue(TimeSpan time)
			: this(time.Ticks)
		{
		}

		// Token: 0x06009157 RID: 37207 RVA: 0x001E2F12 File Offset: 0x001E1112
		private TimeValue(long ticks)
		{
			this.time = new TimeSpan(ticks);
		}

		// Token: 0x06009158 RID: 37208 RVA: 0x001E2F26 File Offset: 0x001E1126
		public static bool HasFractionalSeconds(TimeSpan timeSpan)
		{
			double num = DurationValue.CalculateSeconds(timeSpan);
			return num != (double)((int)num);
		}

		// Token: 0x06009159 RID: 37209 RVA: 0x001E2F36 File Offset: 0x001E1136
		public static long ConvertFractionalPartOfSecondToTicks(double seconds)
		{
			return TimeValue.ConvertSecondToTicks(seconds - Math.Truncate(seconds));
		}

		// Token: 0x0600915A RID: 37210 RVA: 0x001E2F45 File Offset: 0x001E1145
		public static long ConvertSecondToTicks(double seconds)
		{
			return (long)TimeValue.ConvertSecondToDoubleTicks(seconds);
		}

		// Token: 0x0600915B RID: 37211 RVA: 0x001E2F4E File Offset: 0x001E114E
		public static double ConvertSecondToDoubleTicks(double seconds)
		{
			return Math.Round(seconds * 10000000.0);
		}

		// Token: 0x0600915C RID: 37212 RVA: 0x001E2F60 File Offset: 0x001E1160
		public static TimeValue New(TimeSpan time)
		{
			return new TimeValue(time);
		}

		// Token: 0x0600915D RID: 37213 RVA: 0x001E2F68 File Offset: 0x001E1168
		public static TimeValue New(long timeTicks)
		{
			return new TimeValue(timeTicks);
		}

		// Token: 0x0600915E RID: 37214 RVA: 0x001E2F70 File Offset: 0x001E1170
		public static TimeValue New(DateTime dateTime)
		{
			return new TimeValue(dateTime.Ticks % TimeSpan.FromDays(1.0).Ticks);
		}

		// Token: 0x0600915F RID: 37215 RVA: 0x001E2FA0 File Offset: 0x001E11A0
		public static TimeValue New(int hour, int minute, double second)
		{
			if ((hour < 0 || (double)hour >= 24.0 || minute < 0 || (double)minute >= 60.0 || second < 0.0 || second >= 60.0) && ((double)hour != 24.0 || minute != 0 || second != 0.0))
			{
				throw ValueException.NewExpressionError<Message0>(Strings.Time_OutOfRangeError, null, null);
			}
			long num = TimeValue.ConvertSecondToTicks(second);
			if (num >= 600000000L)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.Time_OutOfRangeError, null, null);
			}
			return new TimeValue(new TimeSpan((long)hour * 36000000000L + (long)minute * 600000000L + num));
		}

		// Token: 0x06009160 RID: 37216 RVA: 0x001E304F File Offset: 0x001E124F
		public static bool TryParseFromText(string text, CultureInfo formatProvider, out TimeValue instant)
		{
			return InstantParser.TryParseTime(text, formatProvider, out instant);
		}

		// Token: 0x06009161 RID: 37217 RVA: 0x001E3059 File Offset: 0x001E1259
		public RecordValue ToRecord()
		{
			return RecordValue.New(TimeValue.RecordFormatKeys, new Value[]
			{
				NumberValue.New(this.Hour),
				NumberValue.New(this.Minute),
				NumberValue.New(this.Second)
			});
		}

		// Token: 0x06009162 RID: 37218 RVA: 0x001E3095 File Offset: 0x001E1295
		public override string ToSource()
		{
			return string.Format(CultureInfo.InvariantCulture, "#time({0}, {1}, {2})", this.Hour, this.Minute, this.Second);
		}

		// Token: 0x06009163 RID: 37219 RVA: 0x001E30C8 File Offset: 0x001E12C8
		public override object ToOleDb(Type type)
		{
			if (type == typeof(Time) || type == typeof(object))
			{
				return new Time(this.AsTime.AsClrTimeSpan);
			}
			throw ValueMarshaller.CreateTypeError(this, type);
		}

		// Token: 0x06009164 RID: 37220 RVA: 0x001E3118 File Offset: 0x001E1318
		public override string ToString()
		{
			return this.time.ToString();
		}

		// Token: 0x06009165 RID: 37221 RVA: 0x001E313C File Offset: 0x001E133C
		public string ToString(ICulture culture)
		{
			CultureInfo cultureInfo = culture.GetCultureInfo();
			string text;
			try
			{
				text = this.ToString(cultureInfo.DateTimeFormat.ShortTimePattern, cultureInfo);
			}
			catch (NotSupportedException)
			{
				throw ValueException.NewExpressionError<Message1>(Strings.Culture_NotSupported_Parsing(cultureInfo.Name), null, null);
			}
			return text;
		}

		// Token: 0x06009166 RID: 37222 RVA: 0x001E318C File Offset: 0x001E138C
		public string ToString(string format, IFormatProvider formatProvider)
		{
			string text;
			try
			{
				string text2;
				text = new DateTime(this.time.Ticks).ToString(IsoFormats.TimeIsoToClrFormatMap.TryGetValue(format, out text2) ? text2 : format, formatProvider);
			}
			catch (FormatException ex)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.Time_InvalidOutputFormatError, TextValue.New(format), ex);
			}
			catch (NotSupportedException)
			{
				throw ValueException.NewExpressionError<Message1>(Strings.Culture_NotSupported_Parsing(((CultureInfo)formatProvider).Name), null, null);
			}
			return text;
		}

		// Token: 0x1700260F RID: 9743
		// (get) Token: 0x06009167 RID: 37223 RVA: 0x001E3218 File Offset: 0x001E1418
		public TimeSpan AsClrTimeSpan
		{
			get
			{
				return this.time;
			}
		}

		// Token: 0x06009168 RID: 37224 RVA: 0x001E3220 File Offset: 0x001E1420
		public override bool Equals(Value value, _ValueComparer comparer)
		{
			return value.IsTime && this.time == value.AsTime.time;
		}

		// Token: 0x06009169 RID: 37225 RVA: 0x001E3244 File Offset: 0x001E1444
		public override int GetHashCode(_ValueComparer comparer)
		{
			return this.time.GetHashCode();
		}

		// Token: 0x0600916A RID: 37226 RVA: 0x001E3268 File Offset: 0x001E1468
		public override int CompareTo(Value value, _ValueComparer comparer)
		{
			if (value.IsTime)
			{
				return this.time.CompareTo(value.AsTime.time);
			}
			return base.CompareTo(value, comparer);
		}

		// Token: 0x0600916B RID: 37227 RVA: 0x001E32A0 File Offset: 0x001E14A0
		public override Value Concatenate(Value value)
		{
			if (value.IsNull)
			{
				return Value.Null;
			}
			if (value.IsDate)
			{
				return DateTimeValue.New(this.time.Ticks + value.AsDate.AsClrDateTime.Ticks);
			}
			return base.Concatenate(value);
		}

		// Token: 0x0600916C RID: 37228 RVA: 0x001E32F2 File Offset: 0x001E14F2
		public TimeValue StartOfHour()
		{
			return new TimeValue(new TimeSpan(this.Hour, 0, 0));
		}

		// Token: 0x0600916D RID: 37229 RVA: 0x001E3306 File Offset: 0x001E1506
		public TimeValue EndOfHour()
		{
			return TimeValue.New(new TimeSpan((long)this.Hour * 36000000000L + 35400000000L + 590000000L + TimeValue.MaxSecondFractionInTicks));
		}

		// Token: 0x0600916E RID: 37230 RVA: 0x001E333C File Offset: 0x001E153C
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

		// Token: 0x0600916F RID: 37231 RVA: 0x001B2521 File Offset: 0x001B0721
		public override Value Add(Value value, Precision precision)
		{
			return this.Add(value);
		}

		// Token: 0x06009170 RID: 37232 RVA: 0x001E3374 File Offset: 0x001E1574
		public override Value Subtract(Value value)
		{
			ValueKind kind = value.Kind;
			if (kind == ValueKind.Null)
			{
				return Value.Null;
			}
			if (kind == ValueKind.Time)
			{
				return this.Subtract(value.AsTime);
			}
			if (kind != ValueKind.Duration)
			{
				return base.Subtract(value);
			}
			return this.Subtract(value.AsDuration);
		}

		// Token: 0x06009171 RID: 37233 RVA: 0x001E33BC File Offset: 0x001E15BC
		public DurationValue Subtract(TimeValue instant)
		{
			long ticks = instant.AsClrTimeSpan.Ticks;
			long num = (this.AsClrTimeSpan.Ticks - ticks) % 864000000000L;
			DurationValue durationValue;
			try
			{
				durationValue = DurationValue.New(num);
			}
			catch (ArgumentOutOfRangeException ex)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.Time_OutOfRangeError, Value.Null, ex);
			}
			return durationValue;
		}

		// Token: 0x06009172 RID: 37234 RVA: 0x001B2574 File Offset: 0x001B0774
		public override Value Subtract(Value value, Precision precision)
		{
			return this.Subtract(value);
		}

		// Token: 0x06009173 RID: 37235 RVA: 0x001B2618 File Offset: 0x001B0818
		public Value Subtract(DurationValue interval)
		{
			return this.Add(interval.Negate());
		}

		// Token: 0x06009174 RID: 37236 RVA: 0x001E3424 File Offset: 0x001E1624
		public TimeValue Add(DurationValue interval)
		{
			TimeValue timeValue;
			try
			{
				long num = (this.time.Ticks + interval.AsTimeSpan.Ticks) % 864000000000L;
				if (num < 0L)
				{
					num += 864000000000L;
				}
				timeValue = TimeValue.New(num);
			}
			catch (ArgumentOutOfRangeException ex)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.Time_OutOfRangeError, Value.Null, ex);
			}
			return timeValue;
		}

		// Token: 0x06009175 RID: 37237 RVA: 0x001E3498 File Offset: 0x001E1698
		private static TimeValue New(TimeValue value, RecordValue metaValue, TypeValue type)
		{
			if (value.MetaValue == metaValue && value.Type == type)
			{
				return value;
			}
			return new TimeValue.MetaTypeTimeValue(value, metaValue, type);
		}

		// Token: 0x06009176 RID: 37238 RVA: 0x001E34B6 File Offset: 0x001E16B6
		public sealed override Value NewType(TypeValue type)
		{
			return TimeValue.New(this, this.MetaValue, type);
		}

		// Token: 0x06009177 RID: 37239 RVA: 0x001E34C5 File Offset: 0x001E16C5
		public sealed override Value NewMeta(RecordValue metaValue)
		{
			return TimeValue.New(this, metaValue, this.Type);
		}

		// Token: 0x17002610 RID: 9744
		// (get) Token: 0x06009178 RID: 37240 RVA: 0x00019E61 File Offset: 0x00018061
		public override RecordValue MetaValue
		{
			get
			{
				return RecordValue.Empty;
			}
		}

		// Token: 0x04004E0C RID: 19980
		private const string SourcePattern = "#time({0}, {1}, {2})";

		// Token: 0x04004E0D RID: 19981
		public const string HourKey = "Hour";

		// Token: 0x04004E0E RID: 19982
		public const string MinuteKey = "Minute";

		// Token: 0x04004E0F RID: 19983
		public const string SecondKey = "Second";

		// Token: 0x04004E10 RID: 19984
		public const double MinutesPerHour = 60.0;

		// Token: 0x04004E11 RID: 19985
		public const double SecondsPerMinute = 60.0;

		// Token: 0x04004E12 RID: 19986
		public const double HoursPerDay = 24.0;

		// Token: 0x04004E13 RID: 19987
		public const double MillisecondsPerSecond = 1000.0;

		// Token: 0x04004E14 RID: 19988
		public const double SecondsPerMillsecond = 0.001;

		// Token: 0x04004E15 RID: 19989
		public const double MaxSecondFraction = 0.9999999;

		// Token: 0x04004E16 RID: 19990
		public static readonly long MaxSecondFractionInTicks = TimeValue.ConvertSecondToTicks(0.9999999);

		// Token: 0x04004E17 RID: 19991
		private static readonly Keys RecordFormatKeys = Keys.New("Hour", "Minute", "Second");

		// Token: 0x04004E18 RID: 19992
		private readonly TimeSpan time;

		// Token: 0x02001673 RID: 5747
		private class MetaTypeTimeValue : TimeValue
		{
			// Token: 0x0600917A RID: 37242 RVA: 0x001E3502 File Offset: 0x001E1702
			public MetaTypeTimeValue(TimeValue value, RecordValue meta, TypeValue type)
				: base(value.time)
			{
				this.meta = meta;
				this.type = type;
			}

			// Token: 0x17002611 RID: 9745
			// (get) Token: 0x0600917B RID: 37243 RVA: 0x001E351E File Offset: 0x001E171E
			public override TypeValue Type
			{
				get
				{
					return this.type;
				}
			}

			// Token: 0x17002612 RID: 9746
			// (get) Token: 0x0600917C RID: 37244 RVA: 0x001E3526 File Offset: 0x001E1726
			public override RecordValue MetaValue
			{
				get
				{
					return this.meta;
				}
			}

			// Token: 0x04004E19 RID: 19993
			private RecordValue meta;

			// Token: 0x04004E1A RID: 19994
			private TypeValue type;
		}
	}
}
