using System;
using System.Globalization;
using System.Text;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.OleDb;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001598 RID: 5528
	public abstract class NumberValue : PrimitiveValue, INumberValue, IValue
	{
		// Token: 0x06008A10 RID: 35344 RVA: 0x001D1B00 File Offset: 0x001CFD00
		public static NumberValue New(int value)
		{
			return new Int32NumberValue(value);
		}

		// Token: 0x06008A11 RID: 35345 RVA: 0x001D1B08 File Offset: 0x001CFD08
		public static NumberValue New(long value)
		{
			if (value >= -2147483648L && value <= 2147483647L)
			{
				return new Int32NumberValue((int)value);
			}
			if (value >= -4503599627370496L && value <= 4503599627370496L)
			{
				return new DoubleNumberValue((double)value);
			}
			return new DecimalNumberValue(value);
		}

		// Token: 0x06008A12 RID: 35346 RVA: 0x001D1B5A File Offset: 0x001CFD5A
		public static NumberValue New(ulong value)
		{
			if (value <= 2147483647UL)
			{
				return new Int32NumberValue((int)value);
			}
			if (value <= 4503599627370496UL)
			{
				return new DoubleNumberValue(value);
			}
			return new DecimalNumberValue(value);
		}

		// Token: 0x06008A13 RID: 35347 RVA: 0x001D1B8D File Offset: 0x001CFD8D
		public static NumberValue New(double value)
		{
			return new DoubleNumberValue(value);
		}

		// Token: 0x06008A14 RID: 35348 RVA: 0x001D1B95 File Offset: 0x001CFD95
		public static NumberValue New(decimal value)
		{
			return new DecimalNumberValue(value);
		}

		// Token: 0x06008A15 RID: 35349 RVA: 0x001D1B9D File Offset: 0x001CFD9D
		public static NumberValue New(decimal value, double doubleValue)
		{
			return new DoubleDecimalNumberValue(value, doubleValue);
		}

		// Token: 0x06008A16 RID: 35350 RVA: 0x001D1BA6 File Offset: 0x001CFDA6
		private static NumberValue New(NumberValue value, RecordValue meta, TypeValue type)
		{
			if (meta.IsEmpty && value.Type == type)
			{
				return value;
			}
			return new NumberValue.MetaTypeNumberValue(value, meta, type);
		}

		// Token: 0x1700247D RID: 9341
		// (get) Token: 0x06008A17 RID: 35351 RVA: 0x00002461 File Offset: 0x00000661
		public sealed override ValueKind Kind
		{
			get
			{
				return ValueKind.Number;
			}
		}

		// Token: 0x1700247E RID: 9342
		// (get) Token: 0x06008A18 RID: 35352 RVA: 0x00002139 File Offset: 0x00000339
		public sealed override bool IsNumber
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700247F RID: 9343
		// (get) Token: 0x06008A19 RID: 35353 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public sealed override NumberValue AsNumber
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17002480 RID: 9344
		// (get) Token: 0x06008A1A RID: 35354
		public abstract NumberKind NumberKind { get; }

		// Token: 0x06008A1B RID: 35355
		public abstract double ToDouble();

		// Token: 0x06008A1C RID: 35356
		public abstract decimal ToDecimal();

		// Token: 0x06008A1D RID: 35357
		public abstract object ToObject();

		// Token: 0x06008A1E RID: 35358 RVA: 0x001D1BC4 File Offset: 0x001CFDC4
		public override object ToOleDb(Type type)
		{
			switch (global::System.Type.GetTypeCode(type))
			{
			case TypeCode.Object:
				if (type == typeof(Currency))
				{
					return new Currency(this.ToDecimal());
				}
				if (type == typeof(Number))
				{
					return (this.NumberKind == NumberKind.Decimal) ? new Number(this.ToDecimal()) : new Number(this.ToDouble());
				}
				if (type == typeof(object))
				{
					return this.ToObject();
				}
				break;
			case TypeCode.SByte:
				return this.ToInt8();
			case TypeCode.Byte:
				return this.ToByte();
			case TypeCode.Int16:
				return this.ToInt16();
			case TypeCode.Int32:
				return this.ToInt32();
			case TypeCode.Int64:
				return this.ToInt64();
			case TypeCode.Single:
				return (float)this.ToDouble();
			case TypeCode.Double:
				return this.ToDouble();
			case TypeCode.Decimal:
				return this.ToDecimal();
			}
			throw ValueMarshaller.CreateTypeError(this, type);
		}

		// Token: 0x06008A1F RID: 35359 RVA: 0x001D1D00 File Offset: 0x001CFF00
		public byte ToByte()
		{
			long num = this.ToInt64();
			if (num < 0L || num > 255L)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.NumberOutOfRangeByte, this, null);
			}
			return (byte)num;
		}

		// Token: 0x06008A20 RID: 35360 RVA: 0x001D1D34 File Offset: 0x001CFF34
		public sbyte ToInt8()
		{
			long num = this.ToInt64();
			if (num < -128L || num > 127L)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.NumberOutOfRangeInt8, this, null);
			}
			return (sbyte)num;
		}

		// Token: 0x06008A21 RID: 35361 RVA: 0x001D1D64 File Offset: 0x001CFF64
		public short ToInt16()
		{
			long num = this.ToInt64();
			if (num < -32768L || num > 32767L)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.NumberOutOfRangeInt16, this, null);
			}
			return (short)num;
		}

		// Token: 0x06008A22 RID: 35362 RVA: 0x001D1D9C File Offset: 0x001CFF9C
		public int ToInt32()
		{
			int num;
			if (!this.TryGetInt32(out num))
			{
				throw ValueException.NumberOutOfRange<Message0>(Strings.NumberOutOfRangeInt32, this, null);
			}
			return num;
		}

		// Token: 0x06008A23 RID: 35363 RVA: 0x001D1DC4 File Offset: 0x001CFFC4
		public long ToInt64()
		{
			long num;
			if (!this.TryGetInt64(out num))
			{
				throw ValueException.NumberOutOfRange<Message0>(Strings.NumberOutOfRangeInt64, this, null);
			}
			return num;
		}

		// Token: 0x17002481 RID: 9345
		// (get) Token: 0x06008A24 RID: 35364 RVA: 0x001D1DEC File Offset: 0x001CFFEC
		public bool IsInteger32
		{
			get
			{
				int num;
				return this.TryGetInt32(out num);
			}
		}

		// Token: 0x06008A25 RID: 35365 RVA: 0x001D1E04 File Offset: 0x001D0004
		public bool TryGetInt32(out int value)
		{
			long num;
			if (this.TryGetInt64(out num) && num >= -2147483648L && num <= 2147483647L)
			{
				value = (int)num;
				return true;
			}
			value = 0;
			return false;
		}

		// Token: 0x17002482 RID: 9346
		// (get) Token: 0x06008A26 RID: 35366 RVA: 0x001D1E38 File Offset: 0x001D0038
		public bool IsInteger64
		{
			get
			{
				long num;
				return this.TryGetInt64(out num);
			}
		}

		// Token: 0x06008A27 RID: 35367
		public abstract bool TryGetInt64(out long value);

		// Token: 0x06008A28 RID: 35368
		public abstract bool TryGetDecimal(out decimal value);

		// Token: 0x17002483 RID: 9347
		// (get) Token: 0x06008A29 RID: 35369 RVA: 0x001D1E4D File Offset: 0x001D004D
		public double AsDouble
		{
			get
			{
				return this.ToDouble();
			}
		}

		// Token: 0x17002484 RID: 9348
		// (get) Token: 0x06008A2A RID: 35370 RVA: 0x001D1E55 File Offset: 0x001D0055
		public decimal AsDecimal
		{
			get
			{
				return this.ToDecimal();
			}
		}

		// Token: 0x17002485 RID: 9349
		// (get) Token: 0x06008A2B RID: 35371 RVA: 0x001D1E5D File Offset: 0x001D005D
		public new int AsInteger32
		{
			get
			{
				return this.ToInt32();
			}
		}

		// Token: 0x17002486 RID: 9350
		// (get) Token: 0x06008A2C RID: 35372 RVA: 0x001D1E65 File Offset: 0x001D0065
		public long AsInteger64
		{
			get
			{
				return this.ToInt64();
			}
		}

		// Token: 0x17002487 RID: 9351
		// (get) Token: 0x06008A2D RID: 35373 RVA: 0x001D1E6D File Offset: 0x001D006D
		public bool IsNaN
		{
			get
			{
				return double.IsNaN(this.AsDouble);
			}
		}

		// Token: 0x06008A2E RID: 35374
		public abstract string ToString(string format, IFormatProvider provider);

		// Token: 0x06008A2F RID: 35375 RVA: 0x001D1E7A File Offset: 0x001D007A
		public NumberValue Increment()
		{
			return Precision.Decimal.Add(this, NumberValue.One);
		}

		// Token: 0x06008A30 RID: 35376 RVA: 0x001D1E8C File Offset: 0x001D008C
		public NumberValue Decrement()
		{
			return Precision.Decimal.Add(this, NumberValue.NegativeOne);
		}

		// Token: 0x17002488 RID: 9352
		// (get) Token: 0x06008A31 RID: 35377 RVA: 0x001D1E9E File Offset: 0x001D009E
		bool INumberValue.IsDouble
		{
			get
			{
				return this.NumberKind == NumberKind.Double;
			}
		}

		// Token: 0x17002489 RID: 9353
		// (get) Token: 0x06008A32 RID: 35378 RVA: 0x001D1EA9 File Offset: 0x001D00A9
		bool INumberValue.IsDecimal
		{
			get
			{
				return this.NumberKind == NumberKind.Decimal;
			}
		}

		// Token: 0x1700248A RID: 9354
		// (get) Token: 0x06008A33 RID: 35379 RVA: 0x001D1E5D File Offset: 0x001D005D
		int INumberValue.AsInteger32
		{
			get
			{
				return this.ToInt32();
			}
		}

		// Token: 0x1700248B RID: 9355
		// (get) Token: 0x06008A34 RID: 35380 RVA: 0x001D1E4D File Offset: 0x001D004D
		double INumberValue.AsDouble
		{
			get
			{
				return this.ToDouble();
			}
		}

		// Token: 0x1700248C RID: 9356
		// (get) Token: 0x06008A35 RID: 35381 RVA: 0x001D1E55 File Offset: 0x001D0055
		decimal INumberValue.AsDecimal
		{
			get
			{
				return this.ToDecimal();
			}
		}

		// Token: 0x1700248D RID: 9357
		// (get) Token: 0x06008A36 RID: 35382 RVA: 0x001D1E65 File Offset: 0x001D0065
		long INumberValue.AsInteger64
		{
			get
			{
				return this.ToInt64();
			}
		}

		// Token: 0x06008A37 RID: 35383 RVA: 0x001D1EB4 File Offset: 0x001D00B4
		IValue INumberValue.RoundToEven(IValue digits)
		{
			return this.Round(((Value)digits).AsNumber, NumberValue.RoundingMode.ToEven);
		}

		// Token: 0x06008A38 RID: 35384 RVA: 0x001D1EC8 File Offset: 0x001D00C8
		IValue INumberValue.Negate()
		{
			return this.Negate();
		}

		// Token: 0x1700248E RID: 9358
		// (get) Token: 0x06008A39 RID: 35385 RVA: 0x001D1ED0 File Offset: 0x001D00D0
		bool INumberValue.IsNaN
		{
			get
			{
				return this.IsNaN;
			}
		}

		// Token: 0x06008A3A RID: 35386 RVA: 0x001D1ED8 File Offset: 0x001D00D8
		string INumberValue.ToString(string format, IFormatProvider provider)
		{
			return this.ToString(format, provider);
		}

		// Token: 0x06008A3B RID: 35387 RVA: 0x001D1EE4 File Offset: 0x001D00E4
		public NumberValue Round(NumberValue digits, NumberValue.RoundingMode roundingMode)
		{
			NumberValue numberValue;
			try
			{
				numberValue = NumberValue.New(NumberValue.Round(this.AsDecimal, digits.AsInteger32, roundingMode));
			}
			catch (ValueException)
			{
				numberValue = this;
			}
			return numberValue;
		}

		// Token: 0x06008A3C RID: 35388 RVA: 0x001D1F24 File Offset: 0x001D0124
		private static decimal Round(decimal number, int digits, NumberValue.RoundingMode tiesBreakingRule)
		{
			return NumberValue.RoundCore(number, digits, tiesBreakingRule);
		}

		// Token: 0x06008A3D RID: 35389 RVA: 0x001D1F30 File Offset: 0x001D0130
		protected NumberValue ScaledOperation(int digits, Func<decimal, decimal> operation, bool ceiling = false)
		{
			NumberValue numberValue;
			try
			{
				decimal asDecimal = this.AsDecimal;
				decimal num2;
				decimal num = NumberValue.ScaleNumber(asDecimal, digits, out num2);
				decimal num3;
				if (num == asDecimal && digits != 0)
				{
					num3 = (((digits > 0) ^ ceiling) ? asDecimal : 0m);
				}
				else
				{
					num3 = operation(num) / num2;
				}
				numberValue = NumberValue.New(num3);
			}
			catch (ValueException)
			{
				numberValue = this;
			}
			return numberValue;
		}

		// Token: 0x06008A3E RID: 35390 RVA: 0x001D1FA0 File Offset: 0x001D01A0
		private static decimal ScaleNumber(decimal number, int power, out decimal scaleFactor)
		{
			if (power >= -28 && power <= 28)
			{
				scaleFactor = NumberValue.tenPowers[power + 28];
				try
				{
					return number * scaleFactor;
				}
				catch (OverflowException)
				{
				}
			}
			scaleFactor = 1m;
			return number;
		}

		// Token: 0x06008A3F RID: 35391 RVA: 0x001D1FFC File Offset: 0x001D01FC
		private static decimal RoundCore(decimal number, int digits, NumberValue.RoundingMode roundingMode)
		{
			decimal num2;
			decimal num = NumberValue.ScaleNumber(number, digits, out num2);
			if (num == number && digits != 0)
			{
				if (digits <= 0)
				{
					return 0m;
				}
				return number;
			}
			else
			{
				decimal num3 = num + ((number > 0m) ? 0.5m : (-0.5m));
				decimal num4 = Math.Truncate(num3);
				if (!(num3 == num4))
				{
					return num4 / num2;
				}
				switch (roundingMode)
				{
				case NumberValue.RoundingMode.ToEven:
					if (Math.Abs(num3 % 2m) == 1.0m)
					{
						num3 += ((number > 0m) ? (-1.0m) : 1.0m);
					}
					return num3 / num2;
				case NumberValue.RoundingMode.TowardZero:
					return Math.Truncate(num) / num2;
				case NumberValue.RoundingMode.AwayFromZero:
					return num3 / num2;
				case NumberValue.RoundingMode.Up:
					return ((number > 0m) ? num3 : (num3 + 1.0m)) / num2;
				case NumberValue.RoundingMode.Down:
					return ((number > 0m) ? (num3 - 1.0m) : num3) / num2;
				default:
					throw new InvalidOperationException();
				}
			}
		}

		// Token: 0x06008A40 RID: 35392 RVA: 0x001D2150 File Offset: 0x001D0350
		private static bool ComparePartialString(string text, int index, string targetString, bool isReverse)
		{
			int num = 0;
			int num2 = 1;
			if (isReverse)
			{
				num = targetString.Length - 1;
				num2 = -1;
			}
			for (int i = 0; i < targetString.Length; i++)
			{
				if (text[index] != targetString[num])
				{
					return false;
				}
				index += num2;
				num += num2;
				if (index >= text.Length || index < 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06008A41 RID: 35393 RVA: 0x001D21AC File Offset: 0x001D03AC
		private static int SearchNumberText(string text, string percentSymbol, string negativeSign, int startOffset, int endOffset, bool isReverse, ref int percentCount, ref int negativeCount)
		{
			int num = startOffset;
			while (isReverse ? (num >= endOffset) : (num <= endOffset))
			{
				if (NumberValue.ComparePartialString(text, num, negativeSign, isReverse))
				{
					negativeCount++;
					num += (isReverse ? (-negativeSign.Length) : negativeSign.Length);
				}
				else if (NumberValue.ComparePartialString(text, num, percentSymbol, isReverse))
				{
					percentCount++;
					num += (isReverse ? (-percentSymbol.Length) : percentSymbol.Length);
				}
				else
				{
					if (!char.IsWhiteSpace(text[num]))
					{
						break;
					}
					num += (isReverse ? (-1) : 1);
				}
			}
			return num;
		}

		// Token: 0x06008A42 RID: 35394 RVA: 0x001D224C File Offset: 0x001D044C
		private static bool TryParsePercentValue(string text, int offset, int length, NumberStyles styles, CultureInfo cultureInfo, out NumberValue value, out TypeValue typeValue)
		{
			string percentSymbol;
			string negativeSign;
			try
			{
				percentSymbol = cultureInfo.NumberFormat.PercentSymbol;
				negativeSign = cultureInfo.NumberFormat.NegativeSign;
			}
			catch (NotSupportedException)
			{
				throw ValueException.NewExpressionError<Message1>(Strings.Culture_NotSupported_Parsing(cultureInfo.Name), null, null);
			}
			int num = 0;
			int num2 = 0;
			int num3 = NumberValue.SearchNumberText(text, percentSymbol, negativeSign, 0, text.Length - 1, false, ref num, ref num2);
			int num4 = NumberValue.SearchNumberText(text, percentSymbol, negativeSign, text.Length - 1, num3, true, ref num, ref num2);
			if (num4 >= num3 && num == 1 && num2 <= 1)
			{
				int num5 = num4 - num3 + 1;
				NumberValue numberValue;
				if (NumberValue.TryParseSmallDecimal(text, num3, num5, styles, cultureInfo, out numberValue, out typeValue) || NumberValue.TryParseHard(text.Substring(num3, num5), styles, cultureInfo, out numberValue, out typeValue))
				{
					try
					{
						decimal num6 = numberValue.AsDecimal / 100m;
						if (num2 == 1)
						{
							num6 = -num6;
						}
						value = NumberValue.New(num6);
					}
					catch (OverflowException)
					{
						double num7 = numberValue.AsDouble / 100.0;
						if (num2 == 1)
						{
							num7 = -num7;
						}
						value = NumberValue.New(num7);
					}
					typeValue = TypeValue.Percentage;
					return true;
				}
			}
			value = NumberValue.Zero;
			typeValue = TypeValue.Any;
			return false;
		}

		// Token: 0x06008A43 RID: 35395 RVA: 0x001D239C File Offset: 0x001D059C
		public static bool TryParse(string text, NumberStyles styles, CultureInfo culture, out NumberValue value)
		{
			TypeValue typeValue;
			return NumberValue.TryParse(text, 0, text.Length, styles, culture, out value, out typeValue);
		}

		// Token: 0x06008A44 RID: 35396 RVA: 0x001D23BB File Offset: 0x001D05BB
		public static bool TryParse(string text, int offset, int length, NumberStyles styles, CultureInfo culture, out NumberValue value, out TypeValue typeValue)
		{
			return NumberValue.TryParseSmallDecimal(text, offset, length, styles, culture, out value, out typeValue) || NumberValue.TryParsePercentValue(text, offset, length, styles, culture, out value, out typeValue) || NumberValue.TryParseHard(text.Substring(offset, length), styles, culture, out value, out typeValue);
		}

		// Token: 0x06008A45 RID: 35397 RVA: 0x001D23F8 File Offset: 0x001D05F8
		private static bool TryParseSmallDecimal(string text, int offset, int length, NumberStyles styles, CultureInfo culture, out NumberValue value, out TypeValue typeValue)
		{
			SmallDecimal smallDecimal;
			if ((styles & (NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint)) == (NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint) && SmallDecimalParser.TryParse(text, offset, length, culture, out smallDecimal))
			{
				if (smallDecimal.Denominator == 1)
				{
					typeValue = TypeValue.Int32;
					value = NumberValue.New(smallDecimal.Numerator);
				}
				else
				{
					typeValue = TypeValue.Double;
					value = NumberValue.New((double)smallDecimal.Numerator / (double)smallDecimal.Denominator);
				}
				return true;
			}
			typeValue = TypeValue.Int32;
			value = NumberValue.Zero;
			return false;
		}

		// Token: 0x06008A46 RID: 35398 RVA: 0x001D2474 File Offset: 0x001D0674
		private static bool TryParseHard(string number, NumberStyles styles, IFormatProvider provider, out NumberValue value, out TypeValue typeValue)
		{
			double num = 0.0;
			NumberStyles numberStyles = styles & ~NumberStyles.AllowCurrencySymbol;
			bool flag = false;
			bool flag2;
			decimal num2;
			bool flag3;
			try
			{
				flag2 = double.TryParse(number, numberStyles, provider, out num);
				if (!flag2 && styles != numberStyles)
				{
					flag2 = double.TryParse(number, styles, provider, out num);
					if (flag2)
					{
						flag = true;
					}
				}
				flag3 = decimal.TryParse(number, styles, provider, out num2);
				if (!flag2 && flag3 && styles != numberStyles && !decimal.TryParse(number, numberStyles, provider, out num2))
				{
					flag = true;
				}
			}
			catch (NotSupportedException)
			{
				throw ValueException.NewExpressionError<Message1>(Strings.Culture_NotSupported_Parsing((provider as CultureInfo).Name), null, null);
			}
			if (flag3 && flag2)
			{
				if (!NumberValue.TryAsDouble(num2, num, out value))
				{
					value = NumberValue.New(num2, num);
				}
				typeValue = (flag ? TypeValue.Currency : TypeValue.Double);
				return true;
			}
			if (flag2)
			{
				typeValue = (flag ? TypeValue.Currency : TypeValue.Double);
				value = NumberValue.New(num);
				return true;
			}
			if (flag3)
			{
				typeValue = (flag ? TypeValue.Currency : TypeValue.Decimal);
				value = NumberValue.New(num2);
				return true;
			}
			if (NumberValue.TryParseInfinity(number, styles, provider, out num))
			{
				typeValue = TypeValue.Double;
				value = NumberValue.New(num);
				return true;
			}
			typeValue = TypeValue.Int32;
			value = NumberValue.Zero;
			return false;
		}

		// Token: 0x06008A47 RID: 35399 RVA: 0x001D25AC File Offset: 0x001D07AC
		private static bool TryAsDouble(decimal decimalValue, double doubleValue, out NumberValue value)
		{
			string text = decimalValue.ToString(CultureInfo.InvariantCulture);
			if (text.Length > 15)
			{
				int num = ((text[0] == '-') ? 1 : 0);
				while (num < text.Length && text[num] == '0')
				{
					num++;
				}
				int num2 = text.IndexOf('.', num);
				int num3 = text.Length - 1;
				if (num2 >= 0)
				{
					while (num3 > num && num3 > num2 && text[num3] == '0')
					{
						num3--;
					}
				}
				int num4 = ((num2 >= 0) ? num2 : (num3 + 1)) - num;
				int num5 = ((num2 >= 0) ? (num3 - num2) : 0);
				if (num4 + num5 > 15)
				{
					value = null;
					return false;
				}
			}
			value = NumberValue.New(doubleValue);
			return true;
		}

		// Token: 0x06008A48 RID: 35400 RVA: 0x001D2654 File Offset: 0x001D0854
		private static bool TryParseInfinity(string text, NumberStyles styles, IFormatProvider provider, out double value)
		{
			StringBuilder stringBuilder = new StringBuilder(text.Length);
			foreach (char c in text)
			{
				if (c >= '0' && c <= '9')
				{
					c = '0';
				}
				stringBuilder.Append(c);
			}
			if (!double.TryParse(stringBuilder.ToString(), styles, provider, out value))
			{
				return false;
			}
			foreach (char c2 in text)
			{
				if (c2 == '-')
				{
					value = double.NegativeInfinity;
					return true;
				}
				if (c2 == 'e' || c2 == 'E' || (c2 >= '0' && c2 <= '9'))
				{
					break;
				}
			}
			value = double.PositiveInfinity;
			return true;
		}

		// Token: 0x1700248F RID: 9359
		// (get) Token: 0x06008A49 RID: 35401 RVA: 0x001D26FE File Offset: 0x001D08FE
		public override TypeValue Type
		{
			get
			{
				return TypeValue.Number;
			}
		}

		// Token: 0x06008A4A RID: 35402 RVA: 0x001D2705 File Offset: 0x001D0905
		public override Value NewType(TypeValue type)
		{
			return NumberValue.New(this, this.MetaValue, type);
		}

		// Token: 0x06008A4B RID: 35403 RVA: 0x001D2714 File Offset: 0x001D0914
		public override string ToSource()
		{
			return this.ToString();
		}

		// Token: 0x06008A4C RID: 35404
		public abstract NumberValue Abs();

		// Token: 0x06008A4D RID: 35405 RVA: 0x001D271C File Offset: 0x001D091C
		public override Value Identity()
		{
			return base.SubtractMetaValue.AsNumber;
		}

		// Token: 0x06008A4E RID: 35406
		public abstract override Value Negate();

		// Token: 0x06008A4F RID: 35407
		public abstract NumberValue Ceiling(int digits);

		// Token: 0x06008A50 RID: 35408
		public abstract NumberValue Floor(int digits);

		// Token: 0x06008A51 RID: 35409
		public abstract NumberValue Truncate(int digits);

		// Token: 0x06008A52 RID: 35410 RVA: 0x001D2729 File Offset: 0x001D0929
		public override Value Add(Value value)
		{
			return this.Add(value, Precision.Double);
		}

		// Token: 0x06008A53 RID: 35411 RVA: 0x001D2738 File Offset: 0x001D0938
		public override Value Add(Value value, Precision precision)
		{
			ValueKind kind = value.Kind;
			if (kind == ValueKind.Null)
			{
				return Value.Null;
			}
			if (kind != ValueKind.Number)
			{
				return base.Add(value, precision);
			}
			return precision.Add(this, value.AsNumber);
		}

		// Token: 0x06008A54 RID: 35412 RVA: 0x001D2771 File Offset: 0x001D0971
		public override Value Subtract(Value value)
		{
			return this.Subtract(value, Precision.Double);
		}

		// Token: 0x06008A55 RID: 35413 RVA: 0x001D2780 File Offset: 0x001D0980
		public override Value Subtract(Value value, Precision precision)
		{
			ValueKind kind = value.Kind;
			if (kind == ValueKind.Null)
			{
				return Value.Null;
			}
			if (kind != ValueKind.Number)
			{
				return base.Subtract(value, precision);
			}
			return precision.Subtract(this, value.AsNumber);
		}

		// Token: 0x06008A56 RID: 35414 RVA: 0x001D27B9 File Offset: 0x001D09B9
		public override Value Multiply(Value value)
		{
			return this.Multiply(value, Precision.Double);
		}

		// Token: 0x06008A57 RID: 35415 RVA: 0x001D27C8 File Offset: 0x001D09C8
		public override Value Multiply(Value value, Precision precision)
		{
			ValueKind kind = value.Kind;
			if (kind == ValueKind.Null)
			{
				return Value.Null;
			}
			if (kind == ValueKind.Duration)
			{
				return value.AsDuration.Multiply(this);
			}
			if (kind != ValueKind.Number)
			{
				return base.Multiply(value, precision);
			}
			return precision.Multiply(this, value.AsNumber);
		}

		// Token: 0x06008A58 RID: 35416 RVA: 0x001D2812 File Offset: 0x001D0A12
		public override Value Divide(Value value)
		{
			return this.Divide(value, Precision.Double);
		}

		// Token: 0x06008A59 RID: 35417 RVA: 0x001D2820 File Offset: 0x001D0A20
		public override Value Divide(Value value, Precision precision)
		{
			ValueKind kind = value.Kind;
			if (kind == ValueKind.Null)
			{
				return Value.Null;
			}
			if (kind != ValueKind.Number)
			{
				return base.Divide(value, precision);
			}
			return precision.Divide(this, value.AsNumber);
		}

		// Token: 0x06008A5A RID: 35418 RVA: 0x001D2859 File Offset: 0x001D0A59
		public override Value Mod(Value value)
		{
			return this.Mod(value, Precision.Double);
		}

		// Token: 0x06008A5B RID: 35419 RVA: 0x001D2868 File Offset: 0x001D0A68
		public override Value Mod(Value value, Precision precision)
		{
			ValueKind kind = value.Kind;
			if (kind == ValueKind.Null)
			{
				return Value.Null;
			}
			if (kind != ValueKind.Number)
			{
				return base.Mod(value, precision);
			}
			return precision.Mod(this, value.AsNumber);
		}

		// Token: 0x06008A5C RID: 35420 RVA: 0x001D28A1 File Offset: 0x001D0AA1
		public override Value IntegerDivide(Value value)
		{
			return this.IntegerDivide(value, Precision.Double);
		}

		// Token: 0x06008A5D RID: 35421 RVA: 0x001D28B0 File Offset: 0x001D0AB0
		public override Value IntegerDivide(Value value, Precision precision)
		{
			ValueKind kind = value.Kind;
			if (kind == ValueKind.Null)
			{
				return Value.Null;
			}
			if (kind != ValueKind.Number)
			{
				return base.IntegerDivide(value, precision);
			}
			return precision.IntegerDivide(this, value.AsNumber);
		}

		// Token: 0x06008A5E RID: 35422 RVA: 0x001D28E9 File Offset: 0x001D0AE9
		public override bool Equals(Value value, _ValueComparer comparer)
		{
			return value.IsNumber && comparer.Equals(this, value.AsNumber);
		}

		// Token: 0x06008A5F RID: 35423 RVA: 0x001D2902 File Offset: 0x001D0B02
		public override int GetHashCode(_ValueComparer comparer)
		{
			return comparer.GetHashCode(this);
		}

		// Token: 0x06008A60 RID: 35424 RVA: 0x001D290B File Offset: 0x001D0B0B
		public override int CompareTo(Value value, _ValueComparer comparer)
		{
			if (value.IsNumber)
			{
				return comparer.Compare(this, value.AsNumber);
			}
			return base.CompareTo(value, comparer);
		}

		// Token: 0x06008A61 RID: 35425 RVA: 0x001D292B File Offset: 0x001D0B2B
		public override Accumulator GetAccumulator(Precision precision)
		{
			return precision.GetNumberAccumulator();
		}

		// Token: 0x06008A62 RID: 35426 RVA: 0x001D2933 File Offset: 0x001D0B33
		public override Value BitwiseAnd(Value value)
		{
			if (value.IsNull)
			{
				return Value.Null;
			}
			return NumberValue.New(this.AsInteger64 & value.AsNumber.AsInteger64);
		}

		// Token: 0x06008A63 RID: 35427 RVA: 0x001D295A File Offset: 0x001D0B5A
		public override Value BitwiseOr(Value value)
		{
			if (value.IsNull)
			{
				return Value.Null;
			}
			return NumberValue.New(this.AsInteger64 | value.AsNumber.AsInteger64);
		}

		// Token: 0x06008A64 RID: 35428 RVA: 0x001D2981 File Offset: 0x001D0B81
		public override Value BitwiseXor(Value value)
		{
			if (value.IsNull)
			{
				return Value.Null;
			}
			return NumberValue.New(this.AsInteger64 ^ value.AsNumber.AsInteger64);
		}

		// Token: 0x06008A65 RID: 35429 RVA: 0x001D29A8 File Offset: 0x001D0BA8
		public override Value BitwiseNot()
		{
			return NumberValue.New(~this.AsInteger64);
		}

		// Token: 0x06008A66 RID: 35430 RVA: 0x001D29B6 File Offset: 0x001D0BB6
		public override Value ShiftLeft(Value value)
		{
			if (value.IsNull)
			{
				return Value.Null;
			}
			return NumberValue.New(this.AsInteger64 << value.AsNumber.AsInteger32);
		}

		// Token: 0x06008A67 RID: 35431 RVA: 0x001D29E0 File Offset: 0x001D0BE0
		public override Value ShiftRight(Value value)
		{
			if (value.IsNull)
			{
				return Value.Null;
			}
			return NumberValue.New(this.AsInteger64 >> value.AsNumber.AsInteger32);
		}

		// Token: 0x17002490 RID: 9360
		// (get) Token: 0x06008A68 RID: 35432 RVA: 0x00019E61 File Offset: 0x00018061
		public override RecordValue MetaValue
		{
			get
			{
				return RecordValue.Empty;
			}
		}

		// Token: 0x06008A69 RID: 35433 RVA: 0x001D2A0A File Offset: 0x001D0C0A
		public override Value NewMeta(RecordValue metaValue)
		{
			return NumberValue.New(this, metaValue, this.Type);
		}

		// Token: 0x04004C07 RID: 19463
		public const long MaxDoubleIntValue = 4503599627370496L;

		// Token: 0x04004C08 RID: 19464
		public const long MaxGeneralDoubleIntValue = 999999999999999L;

		// Token: 0x04004C09 RID: 19465
		public static readonly NumberValue Zero = new Int32NumberValue(0);

		// Token: 0x04004C0A RID: 19466
		public static readonly NumberValue One = new Int32NumberValue(1);

		// Token: 0x04004C0B RID: 19467
		public static readonly NumberValue NegativeOne = new Int32NumberValue(-1);

		// Token: 0x04004C0C RID: 19468
		public static readonly NumberValue Ten = new Int32NumberValue(10);

		// Token: 0x04004C0D RID: 19469
		public static readonly NumberValue E = new DoubleNumberValue(2.718281828459045);

		// Token: 0x04004C0E RID: 19470
		public static readonly NumberValue NaN = new DoubleNumberValue(double.NaN);

		// Token: 0x04004C0F RID: 19471
		public static readonly NumberValue Infinity = new DoubleNumberValue(double.PositiveInfinity);

		// Token: 0x04004C10 RID: 19472
		public static readonly NumberValue NegativeInfinity = new DoubleNumberValue(double.NegativeInfinity);

		// Token: 0x04004C11 RID: 19473
		private const int precision = 28;

		// Token: 0x04004C12 RID: 19474
		private static readonly decimal[] tenPowers = new decimal[]
		{
			0.0000000000000000000000000001m, 0.000000000000000000000000001m, 0.00000000000000000000000001m, 0.0000000000000000000000001m, 0.000000000000000000000001m, 0.00000000000000000000001m, 0.0000000000000000000001m, 0.000000000000000000001m, 0.00000000000000000001m, 0.0000000000000000001m,
			0.000000000000000001m, 0.00000000000000001m, 0.0000000000000001m, 0.000000000000001m, 0.00000000000001m, 0.0000000000001m, 0.000000000001m, 0.00000000001m, 0.0000000001m, 0.000000001m,
			0.00000001m, 0.0000001m, 0.000001m, 0.00001m, 0.0001m, 0.001m, 0.01m, 0.1m, 1m, 10m,
			100m, 1000m, 10000m, 100000m, 1000000m, 10000000m, 100000000m, 1000000000m, 10000000000m, 100000000000m,
			1000000000000m, 10000000000000m, 100000000000000m, 1000000000000000m, 10000000000000000m, 100000000000000000m, 1000000000000000000m, 10000000000000000000m, 100000000000000000000m, 1000000000000000000000m,
			10000000000000000000000m, 100000000000000000000000m, 1000000000000000000000000m, 10000000000000000000000000m, 100000000000000000000000000m, 1000000000000000000000000000m, 10000000000000000000000000000m
		};

		// Token: 0x04004C13 RID: 19475
		private const NumberStyles SmallDecimalStyle = NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint;

		// Token: 0x02001599 RID: 5529
		public enum RoundingMode
		{
			// Token: 0x04004C15 RID: 19477
			ToEven,
			// Token: 0x04004C16 RID: 19478
			TowardZero,
			// Token: 0x04004C17 RID: 19479
			AwayFromZero,
			// Token: 0x04004C18 RID: 19480
			Up,
			// Token: 0x04004C19 RID: 19481
			Down
		}

		// Token: 0x0200159A RID: 5530
		private class MetaTypeNumberValue : NumberValue
		{
			// Token: 0x06008A6C RID: 35436 RVA: 0x001D2F3D File Offset: 0x001D113D
			public MetaTypeNumberValue(NumberValue value, RecordValue meta, TypeValue type)
			{
				this.value = value;
				this.meta = meta;
				this.type = type;
			}

			// Token: 0x17002491 RID: 9361
			// (get) Token: 0x06008A6D RID: 35437 RVA: 0x001D2F5A File Offset: 0x001D115A
			public override NumberKind NumberKind
			{
				get
				{
					return this.value.NumberKind;
				}
			}

			// Token: 0x06008A6E RID: 35438 RVA: 0x001D2F67 File Offset: 0x001D1167
			public override double ToDouble()
			{
				return this.value.ToDouble();
			}

			// Token: 0x06008A6F RID: 35439 RVA: 0x001D2F74 File Offset: 0x001D1174
			public override decimal ToDecimal()
			{
				return this.value.ToDecimal();
			}

			// Token: 0x06008A70 RID: 35440 RVA: 0x001D2F81 File Offset: 0x001D1181
			public override object ToObject()
			{
				return this.value.ToObject();
			}

			// Token: 0x06008A71 RID: 35441 RVA: 0x001D2F8E File Offset: 0x001D118E
			public override bool TryGetInt64(out long value)
			{
				return this.value.TryGetInt64(out value);
			}

			// Token: 0x06008A72 RID: 35442 RVA: 0x001D2F9C File Offset: 0x001D119C
			public override bool TryGetDecimal(out decimal value)
			{
				return this.value.TryGetDecimal(out value);
			}

			// Token: 0x17002492 RID: 9362
			// (get) Token: 0x06008A73 RID: 35443 RVA: 0x001D2FAA File Offset: 0x001D11AA
			public sealed override RecordValue MetaValue
			{
				get
				{
					return this.meta;
				}
			}

			// Token: 0x06008A74 RID: 35444 RVA: 0x001D2FB2 File Offset: 0x001D11B2
			public sealed override Value NewMeta(RecordValue metaValue)
			{
				return NumberValue.New(this.value, metaValue, this.Type);
			}

			// Token: 0x17002493 RID: 9363
			// (get) Token: 0x06008A75 RID: 35445 RVA: 0x001D2FC6 File Offset: 0x001D11C6
			public sealed override TypeValue Type
			{
				get
				{
					return this.type;
				}
			}

			// Token: 0x06008A76 RID: 35446 RVA: 0x001D2FCE File Offset: 0x001D11CE
			public sealed override Value NewType(TypeValue type)
			{
				return NumberValue.New(this.value, this.meta, type);
			}

			// Token: 0x06008A77 RID: 35447 RVA: 0x001D2FE2 File Offset: 0x001D11E2
			public override NumberValue Abs()
			{
				return this.value.Abs();
			}

			// Token: 0x06008A78 RID: 35448 RVA: 0x001D2FEF File Offset: 0x001D11EF
			public override Value Negate()
			{
				return this.value.Negate();
			}

			// Token: 0x06008A79 RID: 35449 RVA: 0x001D2FFC File Offset: 0x001D11FC
			public override string ToSource()
			{
				return this.value.ToSource();
			}

			// Token: 0x06008A7A RID: 35450 RVA: 0x001D3009 File Offset: 0x001D1209
			public override string ToString()
			{
				return this.value.ToString();
			}

			// Token: 0x06008A7B RID: 35451 RVA: 0x001D3016 File Offset: 0x001D1216
			public override NumberValue Ceiling(int digits)
			{
				return this.value.Ceiling(digits);
			}

			// Token: 0x06008A7C RID: 35452 RVA: 0x001D3024 File Offset: 0x001D1224
			public override NumberValue Floor(int digits)
			{
				return this.value.Floor(digits);
			}

			// Token: 0x06008A7D RID: 35453 RVA: 0x001D3032 File Offset: 0x001D1232
			public override NumberValue Truncate(int digits)
			{
				return this.value.Truncate(digits);
			}

			// Token: 0x06008A7E RID: 35454 RVA: 0x001D3040 File Offset: 0x001D1240
			public override Value Add(Value value)
			{
				return this.value.Add(value);
			}

			// Token: 0x06008A7F RID: 35455 RVA: 0x001D304E File Offset: 0x001D124E
			public override Value Add(Value value, Precision precision)
			{
				return this.value.Add(value, precision);
			}

			// Token: 0x06008A80 RID: 35456 RVA: 0x001D305D File Offset: 0x001D125D
			public override Value Subtract(Value value)
			{
				return this.value.Subtract(value);
			}

			// Token: 0x06008A81 RID: 35457 RVA: 0x001D306B File Offset: 0x001D126B
			public override Value Subtract(Value value, Precision precision)
			{
				return this.value.Subtract(value, precision);
			}

			// Token: 0x06008A82 RID: 35458 RVA: 0x001D307A File Offset: 0x001D127A
			public override Value Multiply(Value value)
			{
				return this.value.Multiply(value);
			}

			// Token: 0x06008A83 RID: 35459 RVA: 0x001D3088 File Offset: 0x001D1288
			public override Value Multiply(Value value, Precision precision)
			{
				return this.value.Multiply(value, precision);
			}

			// Token: 0x06008A84 RID: 35460 RVA: 0x001D3097 File Offset: 0x001D1297
			public override Value Divide(Value value)
			{
				return this.value.Divide(value);
			}

			// Token: 0x06008A85 RID: 35461 RVA: 0x001D30A5 File Offset: 0x001D12A5
			public override Value Divide(Value value, Precision precision)
			{
				return this.value.Divide(value, precision);
			}

			// Token: 0x06008A86 RID: 35462 RVA: 0x001D30B4 File Offset: 0x001D12B4
			public override Value Mod(Value value)
			{
				return this.value.Mod(value);
			}

			// Token: 0x06008A87 RID: 35463 RVA: 0x001D30C2 File Offset: 0x001D12C2
			public override Value Mod(Value value, Precision precision)
			{
				return this.value.Mod(value, precision);
			}

			// Token: 0x06008A88 RID: 35464 RVA: 0x001D30D1 File Offset: 0x001D12D1
			public override Value AddR(Int32NumberValue value, Precision precision)
			{
				return this.value.AddR(value, precision);
			}

			// Token: 0x06008A89 RID: 35465 RVA: 0x001D30E0 File Offset: 0x001D12E0
			public override Value AddR(DoubleNumberValue value, Precision precision)
			{
				return this.value.AddR(value, precision);
			}

			// Token: 0x06008A8A RID: 35466 RVA: 0x001D30EF File Offset: 0x001D12EF
			public override Value SubtractR(Int32NumberValue value, Precision precision)
			{
				return this.value.SubtractR(value, precision);
			}

			// Token: 0x06008A8B RID: 35467 RVA: 0x001D30FE File Offset: 0x001D12FE
			public override Value SubtractR(DoubleNumberValue value, Precision precision)
			{
				return this.value.SubtractR(value, precision);
			}

			// Token: 0x06008A8C RID: 35468 RVA: 0x001D310D File Offset: 0x001D130D
			public override Value MultiplyR(Int32NumberValue value, Precision precision)
			{
				return this.value.MultiplyR(value, precision);
			}

			// Token: 0x06008A8D RID: 35469 RVA: 0x001D311C File Offset: 0x001D131C
			public override Value MultiplyR(DoubleNumberValue value, Precision precision)
			{
				return this.value.MultiplyR(value, precision);
			}

			// Token: 0x06008A8E RID: 35470 RVA: 0x001D312B File Offset: 0x001D132B
			public override Value IntegerDivide(Value value)
			{
				return this.value.IntegerDivide(value);
			}

			// Token: 0x06008A8F RID: 35471 RVA: 0x001D3139 File Offset: 0x001D1339
			public override Value IntegerDivide(Value value, Precision precision)
			{
				return this.value.IntegerDivide(value, precision);
			}

			// Token: 0x06008A90 RID: 35472 RVA: 0x001D3148 File Offset: 0x001D1348
			public override Value BitwiseAnd(Value value)
			{
				return this.value.BitwiseAnd(value);
			}

			// Token: 0x06008A91 RID: 35473 RVA: 0x001D3156 File Offset: 0x001D1356
			public override Value BitwiseOr(Value value)
			{
				return this.value.BitwiseOr(value);
			}

			// Token: 0x06008A92 RID: 35474 RVA: 0x001D3164 File Offset: 0x001D1364
			public override Value BitwiseXor(Value value)
			{
				return this.value.BitwiseXor(value);
			}

			// Token: 0x06008A93 RID: 35475 RVA: 0x001D3172 File Offset: 0x001D1372
			public override Value BitwiseNot()
			{
				return this.value.BitwiseNot();
			}

			// Token: 0x06008A94 RID: 35476 RVA: 0x001D317F File Offset: 0x001D137F
			public override Value ShiftLeft(Value value)
			{
				return this.value.ShiftLeft(value);
			}

			// Token: 0x06008A95 RID: 35477 RVA: 0x001D318D File Offset: 0x001D138D
			public override Value ShiftRight(Value value)
			{
				return this.value.ShiftRight(value);
			}

			// Token: 0x06008A96 RID: 35478 RVA: 0x001D319B File Offset: 0x001D139B
			public override string ToString(string format, IFormatProvider provider)
			{
				return this.value.ToString(format, provider);
			}

			// Token: 0x04004C1A RID: 19482
			private readonly NumberValue value;

			// Token: 0x04004C1B RID: 19483
			private readonly RecordValue meta;

			// Token: 0x04004C1C RID: 19484
			private readonly TypeValue type;
		}
	}
}
