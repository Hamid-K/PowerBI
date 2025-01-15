using System;
using System.Globalization;
using System.Reflection;
using System.Xml;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020012DF RID: 4831
	public class DurationValue : PrimitiveValue, IDurationValue, IValue
	{
		// Token: 0x06007FE3 RID: 32739 RVA: 0x001B49B0 File Offset: 0x001B2BB0
		public DurationValue(int days, int hours, int minutes, double seconds)
		{
			checked
			{
				long num;
				try
				{
					num = unchecked((long)days) * 864000000000L + unchecked((long)hours) * 36000000000L + unchecked((long)minutes) * 600000000L + (long)TimeValue.ConvertSecondToDoubleTicks(seconds);
				}
				catch (OverflowException ex)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.Duration_OutOfRangeError, Value.Null, ex);
				}
				this.timeSpan = new TimeSpan(num);
			}
		}

		// Token: 0x06007FE4 RID: 32740 RVA: 0x001B4A20 File Offset: 0x001B2C20
		public DurationValue(TimeSpan timeSpan)
		{
			this.timeSpan = timeSpan;
		}

		// Token: 0x170022AE RID: 8878
		// (get) Token: 0x06007FE5 RID: 32741 RVA: 0x001B4A2F File Offset: 0x001B2C2F
		public int Days
		{
			get
			{
				return this.timeSpan.Days;
			}
		}

		// Token: 0x170022AF RID: 8879
		// (get) Token: 0x06007FE6 RID: 32742 RVA: 0x001B4A3C File Offset: 0x001B2C3C
		public int Hours
		{
			get
			{
				return this.timeSpan.Hours;
			}
		}

		// Token: 0x170022B0 RID: 8880
		// (get) Token: 0x06007FE7 RID: 32743 RVA: 0x001B4A49 File Offset: 0x001B2C49
		public int Minutes
		{
			get
			{
				return this.timeSpan.Minutes;
			}
		}

		// Token: 0x170022B1 RID: 8881
		// (get) Token: 0x06007FE8 RID: 32744 RVA: 0x001B4A56 File Offset: 0x001B2C56
		public double Seconds
		{
			get
			{
				return DurationValue.CalculateSeconds(this.timeSpan);
			}
		}

		// Token: 0x06007FE9 RID: 32745 RVA: 0x001B4A63 File Offset: 0x001B2C63
		internal static DurationValue New(int days, int hours, int minutes, double seconds)
		{
			return new DurationValue(days, hours, minutes, seconds);
		}

		// Token: 0x06007FEA RID: 32746 RVA: 0x001B4A6E File Offset: 0x001B2C6E
		private static DurationValue New(DurationValue value, RecordValue metaValue, TypeValue type)
		{
			if (metaValue.IsEmpty && value.Type == type)
			{
				return value;
			}
			return new DurationValue.MetaDurationTypeValue(value, metaValue, type);
		}

		// Token: 0x06007FEB RID: 32747 RVA: 0x001B4A8C File Offset: 0x001B2C8C
		public static double CalculateSeconds(TimeSpan timeSpan)
		{
			return Math.Round(timeSpan.Subtract(new TimeSpan(timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, 0)).TotalSeconds, 7);
		}

		// Token: 0x06007FEC RID: 32748 RVA: 0x001B4ACC File Offset: 0x001B2CCC
		public static DurationValue New(long ticks)
		{
			TimeSpan timeSpan;
			try
			{
				timeSpan = new TimeSpan(ticks);
			}
			catch (ArgumentOutOfRangeException ex)
			{
				throw ValueException.NewExpressionError<Message1>(Strings.Duration_TicksExceedsMaximumValueError(TimeSpan.MaxValue.Ticks), NumberValue.New(ticks), ex);
			}
			return DurationValue.New(timeSpan);
		}

		// Token: 0x06007FED RID: 32749 RVA: 0x001B4B20 File Offset: 0x001B2D20
		public static DurationValue New(TimeSpan value)
		{
			return new DurationValue(value);
		}

		// Token: 0x06007FEE RID: 32750 RVA: 0x001B4B28 File Offset: 0x001B2D28
		public static bool TryParse(string value, out TimeSpan timeSpan)
		{
			if (value.StartsWith("P", StringComparison.Ordinal) || value.StartsWith("-P", StringComparison.Ordinal))
			{
				try
				{
					timeSpan = XmlConvert.ToTimeSpan(value);
					return true;
				}
				catch (FormatException)
				{
				}
				catch (OverflowException)
				{
				}
			}
			if (FxVersionDetector.FxVersion == ClrVersion.Net35)
			{
				return TimeSpan.TryParse(value, out timeSpan);
			}
			int num = 0;
			value = value.Trim();
			if (value.StartsWith("-", StringComparison.OrdinalIgnoreCase))
			{
				num = 1;
				value = value.Substring(1);
			}
			bool flag = DurationValue.TimeSpanExtensions.TryParseExact(value, DurationValue.TimeSpanExtensions.formats, null, num, out timeSpan);
			return (num != 1 || timeSpan.Ticks <= 0L) && flag;
		}

		// Token: 0x06007FEF RID: 32751 RVA: 0x001B4BE0 File Offset: 0x001B2DE0
		public static DurationValue ParseLiteral(Value timeSpanLiteral)
		{
			TimeSpan timeSpan;
			if (DurationValue.TryParse(timeSpanLiteral.AsString, out timeSpan))
			{
				return DurationValue.New(timeSpan.Ticks);
			}
			throw ValueException.NewExpressionError<Message0>(Strings.Duration_CannotParseDurationLiteralError, timeSpanLiteral, null);
		}

		// Token: 0x06007FF0 RID: 32752 RVA: 0x001B4C18 File Offset: 0x001B2E18
		public static RecordValue ToRecord(Value value)
		{
			TimeSpan asTimeSpan = value.AsDuration.AsTimeSpan;
			double num = DurationValue.CalculateSeconds(asTimeSpan);
			return RecordValue.New(DurationValue.RecordKeys, new Value[]
			{
				NumberValue.New(asTimeSpan.Days),
				NumberValue.New(asTimeSpan.Hours),
				NumberValue.New(asTimeSpan.Minutes),
				NumberValue.New(num)
			});
		}

		// Token: 0x170022B2 RID: 8882
		// (get) Token: 0x06007FF1 RID: 32753 RVA: 0x001B4C7E File Offset: 0x001B2E7E
		public TimeSpan AsTimeSpan
		{
			get
			{
				return this.timeSpan;
			}
		}

		// Token: 0x170022B3 RID: 8883
		// (get) Token: 0x06007FF2 RID: 32754 RVA: 0x001B4C86 File Offset: 0x001B2E86
		public override TypeValue Type
		{
			get
			{
				return TypeValue.Duration;
			}
		}

		// Token: 0x06007FF3 RID: 32755 RVA: 0x001B4C8D File Offset: 0x001B2E8D
		public sealed override Value NewType(TypeValue type)
		{
			return DurationValue.New(this, this.MetaValue, type);
		}

		// Token: 0x170022B4 RID: 8884
		// (get) Token: 0x06007FF4 RID: 32756 RVA: 0x00075E2C File Offset: 0x0007402C
		public sealed override ValueKind Kind
		{
			get
			{
				return ValueKind.Duration;
			}
		}

		// Token: 0x170022B5 RID: 8885
		// (get) Token: 0x06007FF5 RID: 32757 RVA: 0x00002139 File Offset: 0x00000339
		public override bool IsDuration
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170022B6 RID: 8886
		// (get) Token: 0x06007FF6 RID: 32758 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public override DurationValue AsDuration
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06007FF7 RID: 32759 RVA: 0x001B4C9C File Offset: 0x001B2E9C
		public override Value Identity()
		{
			return base.SubtractMetaValue;
		}

		// Token: 0x06007FF8 RID: 32760 RVA: 0x001B4CA4 File Offset: 0x001B2EA4
		public override Value Negate()
		{
			Value value;
			try
			{
				value = DurationValue.New(this.AsTimeSpan.Negate());
			}
			catch (OverflowException ex)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.Duration_OutOfRangeError, this, ex);
			}
			return value;
		}

		// Token: 0x06007FF9 RID: 32761 RVA: 0x001B4CE8 File Offset: 0x001B2EE8
		public override Value Add(Value value)
		{
			switch (value.Kind)
			{
			case ValueKind.Null:
				return Value.Null;
			case ValueKind.Time:
				return value.AsTime.Add(this);
			case ValueKind.Date:
				return value.AsDate.Add(this);
			case ValueKind.DateTime:
				return value.AsDateTime.Add(this);
			case ValueKind.DateTimeZone:
				return value.AsDateTimeZone.Add(this);
			case ValueKind.Duration:
				return this.Add(value.AsDuration);
			default:
				return base.Add(value);
			}
		}

		// Token: 0x06007FFA RID: 32762 RVA: 0x001B2521 File Offset: 0x001B0721
		public override Value Add(Value value, Precision precision)
		{
			return this.Add(value);
		}

		// Token: 0x06007FFB RID: 32763 RVA: 0x001B4D6C File Offset: 0x001B2F6C
		public override Value Subtract(Value value)
		{
			ValueKind kind = value.Kind;
			if (kind == ValueKind.Null)
			{
				return Value.Null;
			}
			if (kind == ValueKind.Duration)
			{
				return this.Subtract(value.AsDuration);
			}
			return base.Subtract(value);
		}

		// Token: 0x06007FFC RID: 32764 RVA: 0x001B2574 File Offset: 0x001B0774
		public override Value Subtract(Value value, Precision precision)
		{
			return this.Subtract(value);
		}

		// Token: 0x06007FFD RID: 32765 RVA: 0x001B4DA4 File Offset: 0x001B2FA4
		public override Value Divide(Value value)
		{
			ValueKind kind = value.Kind;
			if (kind == ValueKind.Null)
			{
				return Value.Null;
			}
			if (kind == ValueKind.Duration)
			{
				return this.Divide(value.AsDuration);
			}
			if (kind == ValueKind.Number)
			{
				return this.Divide(value.AsNumber);
			}
			return base.Divide(value);
		}

		// Token: 0x06007FFE RID: 32766 RVA: 0x001B4DEA File Offset: 0x001B2FEA
		public override Value Divide(Value value, Precision precision)
		{
			return this.Divide(value);
		}

		// Token: 0x06007FFF RID: 32767 RVA: 0x001B4DF4 File Offset: 0x001B2FF4
		public override Value Multiply(Value value)
		{
			ValueKind kind = value.Kind;
			if (kind == ValueKind.Null)
			{
				return Value.Null;
			}
			if (kind == ValueKind.Number)
			{
				return this.Multiply(value.AsNumber);
			}
			return base.Multiply(value);
		}

		// Token: 0x06008000 RID: 32768 RVA: 0x001B4E29 File Offset: 0x001B3029
		public override Value Multiply(Value value, Precision precision)
		{
			return this.Multiply(value);
		}

		// Token: 0x06008001 RID: 32769 RVA: 0x001B4E34 File Offset: 0x001B3034
		public Value Add(DurationValue interval)
		{
			Value value;
			try
			{
				value = DurationValue.New(this.AsTimeSpan + interval.AsTimeSpan);
			}
			catch (OverflowException ex)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.Duration_OutOfRangeError, Value.Null, ex);
			}
			return value;
		}

		// Token: 0x06008002 RID: 32770 RVA: 0x001B4E80 File Offset: 0x001B3080
		public Value Subtract(DurationValue interval)
		{
			Value value;
			try
			{
				value = DurationValue.New(this.AsTimeSpan - interval.AsTimeSpan);
			}
			catch (OverflowException ex)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.Duration_OutOfRangeError, Value.Null, ex);
			}
			return value;
		}

		// Token: 0x06008003 RID: 32771 RVA: 0x001B4ECC File Offset: 0x001B30CC
		public Value Divide(NumberValue number)
		{
			Value value;
			try
			{
				value = DurationValue.New(checked((long)((double)this.AsTimeSpan.Ticks / number.AsScientific64)));
			}
			catch (OverflowException ex)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.Duration_OutOfRangeError, Value.Null, ex);
			}
			return value;
		}

		// Token: 0x06008004 RID: 32772 RVA: 0x001B4F1C File Offset: 0x001B311C
		public Value Divide(DurationValue interval)
		{
			return NumberValue.New((double)this.AsTimeSpan.Ticks / (double)interval.AsTimeSpan.Ticks);
		}

		// Token: 0x06008005 RID: 32773 RVA: 0x001B4F50 File Offset: 0x001B3150
		public Value Multiply(NumberValue number)
		{
			checked
			{
				Value value;
				try
				{
					value = DurationValue.New((long)(unchecked((double)this.AsTimeSpan.Ticks * number.AsScientific64)));
				}
				catch (OverflowException ex)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.Duration_OutOfRangeError, Value.Null, ex);
				}
				return value;
			}
		}

		// Token: 0x06008006 RID: 32774 RVA: 0x001B4FA0 File Offset: 0x001B31A0
		public sealed override string ToSource()
		{
			TimeSpan asTimeSpan = this.AsTimeSpan;
			return string.Format(CultureInfo.InvariantCulture, "#duration({0}, {1}, {2}, {3})", new object[]
			{
				asTimeSpan.Days,
				asTimeSpan.Hours,
				asTimeSpan.Minutes,
				DurationValue.CalculateSeconds(asTimeSpan)
			});
		}

		// Token: 0x06008007 RID: 32775 RVA: 0x001B5004 File Offset: 0x001B3204
		public override object ToOleDb(Type type)
		{
			if (type == typeof(TimeSpan) || type == typeof(object))
			{
				return this.AsDuration.AsTimeSpan;
			}
			throw ValueMarshaller.CreateTypeError(this, type);
		}

		// Token: 0x06008008 RID: 32776 RVA: 0x001B5042 File Offset: 0x001B3242
		public sealed override string ToString()
		{
			return this.timeSpan.ToString();
		}

		// Token: 0x170022B7 RID: 8887
		// (get) Token: 0x06008009 RID: 32777 RVA: 0x001B4C7E File Offset: 0x001B2E7E
		public TimeSpan AsClrTimeSpan
		{
			get
			{
				return this.timeSpan;
			}
		}

		// Token: 0x0600800A RID: 32778 RVA: 0x001B5055 File Offset: 0x001B3255
		public override bool Equals(Value value, _ValueComparer comparer)
		{
			return value.IsDuration && value.AsDuration.AsTimeSpan == this.timeSpan;
		}

		// Token: 0x0600800B RID: 32779 RVA: 0x001B5077 File Offset: 0x001B3277
		public override int GetHashCode(_ValueComparer comparer)
		{
			return this.timeSpan.GetHashCode();
		}

		// Token: 0x0600800C RID: 32780 RVA: 0x001B508A File Offset: 0x001B328A
		public override int CompareTo(Value value, _ValueComparer comparer)
		{
			if (value.IsDuration)
			{
				return this.timeSpan.CompareTo(value.AsDuration.timeSpan);
			}
			return base.CompareTo(value, comparer);
		}

		// Token: 0x170022B8 RID: 8888
		// (get) Token: 0x0600800D RID: 32781 RVA: 0x00019E61 File Offset: 0x00018061
		public override RecordValue MetaValue
		{
			get
			{
				return RecordValue.Empty;
			}
		}

		// Token: 0x0600800E RID: 32782 RVA: 0x001B50B3 File Offset: 0x001B32B3
		public override Value NewMeta(RecordValue metaValue)
		{
			return DurationValue.New(this, metaValue, this.Type);
		}

		// Token: 0x170022B9 RID: 8889
		// (get) Token: 0x0600800F RID: 32783 RVA: 0x001B50C2 File Offset: 0x001B32C2
		TimeSpan IDurationValue.AsTimeSpan
		{
			get
			{
				return this.AsTimeSpan;
			}
		}

		// Token: 0x040045BC RID: 17852
		public static readonly DurationValue OneDay = DurationValue.New(1, 0, 0, 0.0);

		// Token: 0x040045BD RID: 17853
		private const string PatternName = "Interval";

		// Token: 0x040045BE RID: 17854
		private const string DaysKey = "Days";

		// Token: 0x040045BF RID: 17855
		private const string HoursKey = "Hours";

		// Token: 0x040045C0 RID: 17856
		private const string MinutesKey = "Minutes";

		// Token: 0x040045C1 RID: 17857
		private const string SecondsKey = "Seconds";

		// Token: 0x040045C2 RID: 17858
		private static readonly Keys RecordKeys = Keys.New("Days", "Hours", "Minutes", "Seconds");

		// Token: 0x040045C3 RID: 17859
		private TimeSpan timeSpan;

		// Token: 0x020012E0 RID: 4832
		private class TimeSpanExtensions
		{
			// Token: 0x170022BA RID: 8890
			// (get) Token: 0x06008011 RID: 32785 RVA: 0x001B5100 File Offset: 0x001B3300
			public static DurationValue.TimeSpanExtensions.TryParseExactDelegate TryParseExact
			{
				get
				{
					if (DurationValue.TimeSpanExtensions.tryParseExact == null)
					{
						MethodInfo method = typeof(TimeSpan).GetMethod("TryParseExact", new Type[]
						{
							typeof(string),
							typeof(string[]),
							typeof(IFormatProvider),
							Assembly.GetAssembly(typeof(TimeSpan)).GetType("System.Globalization.TimeSpanStyles"),
							typeof(TimeSpan).MakeByRefType()
						});
						DurationValue.TimeSpanExtensions.tryParseExact = (DurationValue.TimeSpanExtensions.TryParseExactDelegate)Delegate.CreateDelegate(typeof(DurationValue.TimeSpanExtensions.TryParseExactDelegate), method);
					}
					return DurationValue.TimeSpanExtensions.tryParseExact;
				}
			}

			// Token: 0x040045C4 RID: 17860
			public static readonly string[] formats = new string[] { "%d", "d\\.h\\:m", "d\\.h\\:m\\:s", "d\\.h\\:m\\:s\\.FFFFFFF", "h\\:m", "h\\:m\\:s", "h\\:m\\:s\\.FFFFFFF" };

			// Token: 0x040045C5 RID: 17861
			private static DurationValue.TimeSpanExtensions.TryParseExactDelegate tryParseExact;

			// Token: 0x020012E1 RID: 4833
			// (Invoke) Token: 0x06008015 RID: 32789
			public delegate bool TryParseExactDelegate(string input, string[] formats, IFormatProvider formatProvider, int isNegative, out TimeSpan result);
		}

		// Token: 0x020012E2 RID: 4834
		private class MetaDurationTypeValue : DurationValue
		{
			// Token: 0x06008018 RID: 32792 RVA: 0x001B51F8 File Offset: 0x001B33F8
			public MetaDurationTypeValue(DurationValue value, RecordValue metaValue, TypeValue type)
				: base(value.AsTimeSpan)
			{
				this.metaValue = metaValue;
				this.type = type;
			}

			// Token: 0x170022BB RID: 8891
			// (get) Token: 0x06008019 RID: 32793 RVA: 0x001B5214 File Offset: 0x001B3414
			public override RecordValue MetaValue
			{
				get
				{
					return this.metaValue;
				}
			}

			// Token: 0x170022BC RID: 8892
			// (get) Token: 0x0600801A RID: 32794 RVA: 0x001B521C File Offset: 0x001B341C
			public sealed override TypeValue Type
			{
				get
				{
					return this.type;
				}
			}

			// Token: 0x040045C6 RID: 17862
			private RecordValue metaValue;

			// Token: 0x040045C7 RID: 17863
			private TypeValue type;
		}
	}
}
