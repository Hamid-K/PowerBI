using System;
using System.Data.Entity.Resources;
using System.Globalization;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x0200068B RID: 1675
	internal sealed class Literal : Node
	{
		// Token: 0x06004F3B RID: 20283 RVA: 0x0011FB31 File Offset: 0x0011DD31
		internal Literal(string originalValue, LiteralKind kind, string query, int inputPos)
			: base(query, inputPos)
		{
			this._originalValue = originalValue;
			this._literalKind = kind;
		}

		// Token: 0x06004F3C RID: 20284 RVA: 0x0011FB4A File Offset: 0x0011DD4A
		internal static Literal NewBooleanLiteral(bool value)
		{
			return new Literal(value);
		}

		// Token: 0x06004F3D RID: 20285 RVA: 0x0011FB52 File Offset: 0x0011DD52
		private Literal(bool boolLiteral)
			: base(null, 0)
		{
			this._wasValueComputed = true;
			this._originalValue = string.Empty;
			this._computedValue = boolLiteral;
			this._type = typeof(bool);
		}

		// Token: 0x17000F58 RID: 3928
		// (get) Token: 0x06004F3E RID: 20286 RVA: 0x0011FB8A File Offset: 0x0011DD8A
		internal bool IsNumber
		{
			get
			{
				return this._literalKind == LiteralKind.Number;
			}
		}

		// Token: 0x17000F59 RID: 3929
		// (get) Token: 0x06004F3F RID: 20287 RVA: 0x0011FB95 File Offset: 0x0011DD95
		internal bool IsSignedNumber
		{
			get
			{
				return this.IsNumber && (this._originalValue[0] == '-' || this._originalValue[0] == '+');
			}
		}

		// Token: 0x17000F5A RID: 3930
		// (get) Token: 0x06004F40 RID: 20288 RVA: 0x0011FBC3 File Offset: 0x0011DDC3
		internal bool IsString
		{
			get
			{
				return this._literalKind == LiteralKind.String || this._literalKind == LiteralKind.UnicodeString;
			}
		}

		// Token: 0x17000F5B RID: 3931
		// (get) Token: 0x06004F41 RID: 20289 RVA: 0x0011FBD9 File Offset: 0x0011DDD9
		internal bool IsUnicodeString
		{
			get
			{
				return this._literalKind == LiteralKind.UnicodeString;
			}
		}

		// Token: 0x17000F5C RID: 3932
		// (get) Token: 0x06004F42 RID: 20290 RVA: 0x0011FBE4 File Offset: 0x0011DDE4
		internal bool IsNullLiteral
		{
			get
			{
				return this._literalKind == LiteralKind.Null;
			}
		}

		// Token: 0x17000F5D RID: 3933
		// (get) Token: 0x06004F43 RID: 20291 RVA: 0x0011FBF0 File Offset: 0x0011DDF0
		internal string OriginalValue
		{
			get
			{
				return this._originalValue;
			}
		}

		// Token: 0x06004F44 RID: 20292 RVA: 0x0011FBF8 File Offset: 0x0011DDF8
		internal void PrefixSign(string sign)
		{
			this._originalValue = sign + this._originalValue;
		}

		// Token: 0x17000F5E RID: 3934
		// (get) Token: 0x06004F45 RID: 20293 RVA: 0x0011FC0C File Offset: 0x0011DE0C
		internal object Value
		{
			get
			{
				this.ComputeValue();
				return this._computedValue;
			}
		}

		// Token: 0x17000F5F RID: 3935
		// (get) Token: 0x06004F46 RID: 20294 RVA: 0x0011FC1A File Offset: 0x0011DE1A
		internal Type Type
		{
			get
			{
				this.ComputeValue();
				return this._type;
			}
		}

		// Token: 0x06004F47 RID: 20295 RVA: 0x0011FC28 File Offset: 0x0011DE28
		private void ComputeValue()
		{
			if (!this._wasValueComputed)
			{
				this._wasValueComputed = true;
				switch (this._literalKind)
				{
				case LiteralKind.Number:
					this._computedValue = Literal.ConvertNumericLiteral(base.ErrCtx, this._originalValue);
					break;
				case LiteralKind.String:
					this._computedValue = Literal.GetStringLiteralValue(this._originalValue, false);
					break;
				case LiteralKind.UnicodeString:
					this._computedValue = Literal.GetStringLiteralValue(this._originalValue, true);
					break;
				case LiteralKind.Boolean:
					this._computedValue = Literal.ConvertBooleanLiteralValue(base.ErrCtx, this._originalValue);
					break;
				case LiteralKind.Binary:
					this._computedValue = Literal.ConvertBinaryLiteralValue(this._originalValue);
					break;
				case LiteralKind.DateTime:
					this._computedValue = Literal.ConvertDateTimeLiteralValue(this._originalValue);
					break;
				case LiteralKind.Time:
					this._computedValue = Literal.ConvertTimeLiteralValue(this._originalValue);
					break;
				case LiteralKind.DateTimeOffset:
					this._computedValue = Literal.ConvertDateTimeOffsetLiteralValue(base.ErrCtx, this._originalValue);
					break;
				case LiteralKind.Guid:
					this._computedValue = Literal.ConvertGuidLiteralValue(this._originalValue);
					break;
				case LiteralKind.Null:
					this._computedValue = null;
					break;
				default:
					throw new NotSupportedException(Strings.LiteralTypeNotSupported(this._literalKind.ToString()));
				}
				this._type = (this.IsNullLiteral ? null : this._computedValue.GetType());
			}
		}

		// Token: 0x06004F48 RID: 20296 RVA: 0x0011FDAC File Offset: 0x0011DFAC
		private static object ConvertNumericLiteral(ErrorContext errCtx, string numericString)
		{
			int num = numericString.IndexOfAny(Literal._numberSuffixes);
			if (-1 != num)
			{
				string text = numericString.Substring(num).ToUpperInvariant();
				string text2 = numericString.Substring(0, numericString.Length - text.Length);
				if (text != null)
				{
					uint num2 = <PrivateImplementationDetails>.ComputeStringHash(text);
					if (num2 <= 3238785555U)
					{
						if (num2 != 2078435802U)
						{
							if (num2 != 2129901492U)
							{
								if (num2 != 3238785555U)
								{
									goto IL_0256;
								}
								if (!(text == "D"))
								{
									goto IL_0256;
								}
								double num3;
								if (!double.TryParse(text2, NumberStyles.Float, CultureInfo.InvariantCulture, out num3))
								{
									string text3 = Strings.CannotConvertNumericLiteral(numericString, "double");
									throw EntitySqlException.Create(errCtx, text3, null);
								}
								return num3;
							}
							else if (!(text == "UL"))
							{
								goto IL_0256;
							}
						}
						else if (!(text == "LU"))
						{
							goto IL_0256;
						}
						ulong num4;
						if (!ulong.TryParse(text2, NumberStyles.Integer, CultureInfo.InvariantCulture, out num4))
						{
							string text4 = Strings.CannotConvertNumericLiteral(numericString, "unsigned long");
							throw EntitySqlException.Create(errCtx, text4, null);
						}
						return num4;
					}
					else if (num2 <= 3356228888U)
					{
						if (num2 != 3272340793U)
						{
							if (num2 == 3356228888U)
							{
								if (text == "M")
								{
									decimal num5;
									if (!decimal.TryParse(text2, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out num5))
									{
										string text5 = Strings.CannotConvertNumericLiteral(numericString, "decimal");
										throw EntitySqlException.Create(errCtx, text5, null);
									}
									return num5;
								}
							}
						}
						else if (text == "F")
						{
							float num6;
							if (!float.TryParse(text2, NumberStyles.Float, CultureInfo.InvariantCulture, out num6))
							{
								string text6 = Strings.CannotConvertNumericLiteral(numericString, "float");
								throw EntitySqlException.Create(errCtx, text6, null);
							}
							return num6;
						}
					}
					else if (num2 != 3373006507U)
					{
						if (num2 == 3490449840U)
						{
							if (text == "U")
							{
								uint num7;
								if (!uint.TryParse(text2, NumberStyles.Integer, CultureInfo.InvariantCulture, out num7))
								{
									string text7 = Strings.CannotConvertNumericLiteral(numericString, "unsigned int");
									throw EntitySqlException.Create(errCtx, text7, null);
								}
								return num7;
							}
						}
					}
					else if (text == "L")
					{
						long num8;
						if (!long.TryParse(text2, NumberStyles.Integer, CultureInfo.InvariantCulture, out num8))
						{
							string text8 = Strings.CannotConvertNumericLiteral(numericString, "long");
							throw EntitySqlException.Create(errCtx, text8, null);
						}
						return num8;
					}
				}
			}
			IL_0256:
			return Literal.DefaultNumericConversion(numericString, errCtx);
		}

		// Token: 0x06004F49 RID: 20297 RVA: 0x00120018 File Offset: 0x0011E218
		private static object DefaultNumericConversion(string numericString, ErrorContext errCtx)
		{
			if (-1 != numericString.IndexOfAny(Literal._floatTokens))
			{
				double num;
				if (!double.TryParse(numericString, NumberStyles.Float, CultureInfo.InvariantCulture, out num))
				{
					string text = Strings.CannotConvertNumericLiteral(numericString, "double");
					throw EntitySqlException.Create(errCtx, text, null);
				}
				return num;
			}
			else
			{
				int num2;
				if (int.TryParse(numericString, NumberStyles.Integer, CultureInfo.InvariantCulture, out num2))
				{
					return num2;
				}
				long num3;
				if (!long.TryParse(numericString, NumberStyles.Integer, CultureInfo.InvariantCulture, out num3))
				{
					string text2 = Strings.CannotConvertNumericLiteral(numericString, "long");
					throw EntitySqlException.Create(errCtx, text2, null);
				}
				return num3;
			}
		}

		// Token: 0x06004F4A RID: 20298 RVA: 0x001200A8 File Offset: 0x0011E2A8
		private static bool ConvertBooleanLiteralValue(ErrorContext errCtx, string booleanLiteralValue)
		{
			bool flag = false;
			if (!bool.TryParse(booleanLiteralValue, out flag))
			{
				string text = Strings.InvalidLiteralFormat("Boolean", booleanLiteralValue);
				throw EntitySqlException.Create(errCtx, text, null);
			}
			return flag;
		}

		// Token: 0x06004F4B RID: 20299 RVA: 0x001200D8 File Offset: 0x0011E2D8
		private static string GetStringLiteralValue(string stringLiteralValue, bool isUnicode)
		{
			int num = (isUnicode ? 2 : 1);
			char c = stringLiteralValue[num - 1];
			if (c != '\'' && c != '"')
			{
				throw new EntitySqlException(Strings.MalformedStringLiteralPayload);
			}
			int num2 = stringLiteralValue.Split(new char[] { c }).Length - 1;
			if (num2 % 2 != 0)
			{
				throw new EntitySqlException(Strings.MalformedStringLiteralPayload);
			}
			string text = stringLiteralValue.Substring(num, stringLiteralValue.Length - (1 + num));
			text = text.Replace(new string(c, 2), new string(c, 1));
			if (text.Split(new char[] { c }).Length - 1 != (num2 - 2) / 2)
			{
				throw new EntitySqlException(Strings.MalformedStringLiteralPayload);
			}
			return text;
		}

		// Token: 0x06004F4C RID: 20300 RVA: 0x00120188 File Offset: 0x0011E388
		private static byte[] ConvertBinaryLiteralValue(string binaryLiteralValue)
		{
			if (string.IsNullOrEmpty(binaryLiteralValue))
			{
				return Literal._emptyByteArray;
			}
			int i = 0;
			int num = binaryLiteralValue.Length - 1;
			int num2 = num - i + 1;
			int num3 = num2 / 2;
			bool flag = num2 % 2 != 0;
			if (flag)
			{
				num3++;
			}
			byte[] array = new byte[num3];
			int num4 = 0;
			if (flag)
			{
				array[num4++] = (byte)Literal.HexDigitToBinaryValue(binaryLiteralValue[i++]);
			}
			while (i < num)
			{
				array[num4++] = (byte)((Literal.HexDigitToBinaryValue(binaryLiteralValue[i++]) << 4) | Literal.HexDigitToBinaryValue(binaryLiteralValue[i++]));
			}
			return array;
		}

		// Token: 0x06004F4D RID: 20301 RVA: 0x00120223 File Offset: 0x0011E423
		private static int HexDigitToBinaryValue(char hexChar)
		{
			if (hexChar >= '0' && hexChar <= '9')
			{
				return (int)(hexChar - '0');
			}
			if (hexChar >= 'A' && hexChar <= 'F')
			{
				return (int)(hexChar - 'A' + '\n');
			}
			if (hexChar >= 'a' && hexChar <= 'f')
			{
				return (int)(hexChar - 'a' + '\n');
			}
			throw new ArgumentOutOfRangeException("hexChar");
		}

		// Token: 0x06004F4E RID: 20302 RVA: 0x00120264 File Offset: 0x0011E464
		private static DateTime ConvertDateTimeLiteralValue(string datetimeLiteralValue)
		{
			string[] array = datetimeLiteralValue.Split(Literal._datetimeSeparators, StringSplitOptions.RemoveEmptyEntries);
			int num;
			int num2;
			int num3;
			Literal.GetDateParts(datetimeLiteralValue, array, out num, out num2, out num3);
			int num4;
			int num5;
			int num6;
			int num7;
			Literal.GetTimeParts(datetimeLiteralValue, array, 3, out num4, out num5, out num6, out num7);
			DateTime dateTime = new DateTime(num, num2, num3, num4, num5, num6, 0);
			dateTime = dateTime.AddTicks((long)num7);
			return dateTime;
		}

		// Token: 0x06004F4F RID: 20303 RVA: 0x001202BC File Offset: 0x0011E4BC
		private static DateTimeOffset ConvertDateTimeOffsetLiteralValue(ErrorContext errCtx, string datetimeLiteralValue)
		{
			string[] array = datetimeLiteralValue.Split(Literal._datetimeOffsetSeparators, StringSplitOptions.RemoveEmptyEntries);
			int num;
			int num2;
			int num3;
			Literal.GetDateParts(datetimeLiteralValue, array, out num, out num2, out num3);
			string[] array2 = new string[array.Length - 2];
			Array.Copy(array, array2, array.Length - 2);
			int num4;
			int num5;
			int num6;
			int num7;
			Literal.GetTimeParts(datetimeLiteralValue, array2, 3, out num4, out num5, out num6, out num7);
			int num8 = int.Parse(array[array.Length - 2], NumberStyles.Integer, CultureInfo.InvariantCulture);
			int num9 = int.Parse(array[array.Length - 1], NumberStyles.Integer, CultureInfo.InvariantCulture);
			TimeSpan timeSpan = new TimeSpan(num8, num9, 0);
			if (datetimeLiteralValue.IndexOf('+') == -1)
			{
				timeSpan = timeSpan.Negate();
			}
			DateTime dateTime = new DateTime(num, num2, num3, num4, num5, num6, 0);
			dateTime = dateTime.AddTicks((long)num7);
			DateTimeOffset dateTimeOffset;
			try
			{
				dateTimeOffset = new DateTimeOffset(dateTime, timeSpan);
			}
			catch (ArgumentOutOfRangeException ex)
			{
				string text = Strings.InvalidDateTimeOffsetLiteral(datetimeLiteralValue);
				throw EntitySqlException.Create(errCtx, text, ex);
			}
			return dateTimeOffset;
		}

		// Token: 0x06004F50 RID: 20304 RVA: 0x001203A8 File Offset: 0x0011E5A8
		private static TimeSpan ConvertTimeLiteralValue(string datetimeLiteralValue)
		{
			string[] array = datetimeLiteralValue.Split(Literal._datetimeSeparators, StringSplitOptions.RemoveEmptyEntries);
			int num;
			int num2;
			int num3;
			int num4;
			Literal.GetTimeParts(datetimeLiteralValue, array, 0, out num, out num2, out num3, out num4);
			TimeSpan timeSpan = new TimeSpan(num, num2, num3);
			timeSpan = timeSpan.Add(new TimeSpan((long)num4));
			return timeSpan;
		}

		// Token: 0x06004F51 RID: 20305 RVA: 0x001203F0 File Offset: 0x0011E5F0
		private static void GetTimeParts(string datetimeLiteralValue, string[] datetimeParts, int timePartStartIndex, out int hour, out int minute, out int second, out int ticks)
		{
			hour = int.Parse(datetimeParts[timePartStartIndex], NumberStyles.Integer, CultureInfo.InvariantCulture);
			if (hour > 23)
			{
				throw new EntitySqlException(Strings.InvalidHour(datetimeParts[timePartStartIndex], datetimeLiteralValue));
			}
			minute = int.Parse(datetimeParts[++timePartStartIndex], NumberStyles.Integer, CultureInfo.InvariantCulture);
			if (minute > 59)
			{
				throw new EntitySqlException(Strings.InvalidMinute(datetimeParts[timePartStartIndex], datetimeLiteralValue));
			}
			second = 0;
			ticks = 0;
			timePartStartIndex++;
			if (datetimeParts.Length > timePartStartIndex)
			{
				second = int.Parse(datetimeParts[timePartStartIndex], NumberStyles.Integer, CultureInfo.InvariantCulture);
				if (second > 59)
				{
					throw new EntitySqlException(Strings.InvalidSecond(datetimeParts[timePartStartIndex], datetimeLiteralValue));
				}
				timePartStartIndex++;
				if (datetimeParts.Length > timePartStartIndex)
				{
					string text = datetimeParts[timePartStartIndex].PadRight(7, '0');
					ticks = int.Parse(text, NumberStyles.Integer, CultureInfo.InvariantCulture);
				}
			}
		}

		// Token: 0x06004F52 RID: 20306 RVA: 0x001204B0 File Offset: 0x0011E6B0
		private static void GetDateParts(string datetimeLiteralValue, string[] datetimeParts, out int year, out int month, out int day)
		{
			year = int.Parse(datetimeParts[0], NumberStyles.Integer, CultureInfo.InvariantCulture);
			if (year < 1 || year > 9999)
			{
				throw new EntitySqlException(Strings.InvalidYear(datetimeParts[0], datetimeLiteralValue));
			}
			month = int.Parse(datetimeParts[1], NumberStyles.Integer, CultureInfo.InvariantCulture);
			if (month < 1 || month > 12)
			{
				throw new EntitySqlException(Strings.InvalidMonth(datetimeParts[1], datetimeLiteralValue));
			}
			day = int.Parse(datetimeParts[2], NumberStyles.Integer, CultureInfo.InvariantCulture);
			if (day < 1)
			{
				throw new EntitySqlException(Strings.InvalidDay(datetimeParts[2], datetimeLiteralValue));
			}
			if (day > DateTime.DaysInMonth(year, month))
			{
				throw new EntitySqlException(Strings.InvalidDayInMonth(datetimeParts[2], datetimeParts[1], datetimeLiteralValue));
			}
		}

		// Token: 0x06004F53 RID: 20307 RVA: 0x0012055A File Offset: 0x0011E75A
		private static Guid ConvertGuidLiteralValue(string guidLiteralValue)
		{
			return new Guid(guidLiteralValue);
		}

		// Token: 0x04001CEE RID: 7406
		private readonly LiteralKind _literalKind;

		// Token: 0x04001CEF RID: 7407
		private string _originalValue;

		// Token: 0x04001CF0 RID: 7408
		private bool _wasValueComputed;

		// Token: 0x04001CF1 RID: 7409
		private object _computedValue;

		// Token: 0x04001CF2 RID: 7410
		private Type _type;

		// Token: 0x04001CF3 RID: 7411
		private static readonly byte[] _emptyByteArray = new byte[0];

		// Token: 0x04001CF4 RID: 7412
		private static readonly char[] _numberSuffixes = new char[] { 'U', 'u', 'L', 'l', 'F', 'f', 'M', 'm', 'D', 'd' };

		// Token: 0x04001CF5 RID: 7413
		private static readonly char[] _floatTokens = new char[] { '.', 'E', 'e' };

		// Token: 0x04001CF6 RID: 7414
		private static readonly char[] _datetimeSeparators = new char[] { ' ', ':', '-', '.' };

		// Token: 0x04001CF7 RID: 7415
		private static readonly char[] _datetimeOffsetSeparators = new char[] { ' ', ':', '-', '.', '+', '-' };
	}
}
