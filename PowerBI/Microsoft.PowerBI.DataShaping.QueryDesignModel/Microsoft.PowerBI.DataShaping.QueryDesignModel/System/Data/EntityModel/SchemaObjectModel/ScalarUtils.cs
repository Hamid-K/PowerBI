using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000046 RID: 70
	internal static class ScalarUtils
	{
		// Token: 0x060007A6 RID: 1958 RVA: 0x0000F500 File Offset: 0x0000D700
		public static bool TryParse(PrimitiveType primitiveType, string text, out object value)
		{
			EntityUtil.CheckArgumentNull<PrimitiveType>(primitiveType, "primitiveType");
			switch (primitiveType.PrimitiveTypeKind)
			{
			case PrimitiveTypeKind.Binary:
				return ScalarUtils.TryParseBinary(text, out value);
			case PrimitiveTypeKind.Boolean:
				return ScalarUtils.TryParseBoolean(text, out value);
			case PrimitiveTypeKind.Byte:
				return ScalarUtils.TryParseByte(text, out value);
			case PrimitiveTypeKind.DateTime:
				return ScalarUtils.TryParseDateTime(text, out value);
			case PrimitiveTypeKind.Decimal:
				return ScalarUtils.TryParseDecimal(text, out value);
			case PrimitiveTypeKind.Double:
				return ScalarUtils.TryParseDouble(text, out value);
			case PrimitiveTypeKind.Guid:
				return ScalarUtils.TryParseGuid(text, out value);
			case PrimitiveTypeKind.Single:
				return ScalarUtils.TryParseSingle(text, out value);
			case PrimitiveTypeKind.SByte:
				return ScalarUtils.TryParseSByte(text, out value);
			case PrimitiveTypeKind.Int16:
				return ScalarUtils.TryParseInt16(text, out value);
			case PrimitiveTypeKind.Int32:
				return ScalarUtils.TryParseInt32(text, out value);
			case PrimitiveTypeKind.Int64:
				return ScalarUtils.TryParseInt64(text, out value);
			case PrimitiveTypeKind.String:
				return ScalarUtils.TryParseString(text, out value);
			case PrimitiveTypeKind.Time:
				return ScalarUtils.TryParseTime(text, out value);
			case PrimitiveTypeKind.DateTimeOffset:
				return ScalarUtils.TryParseDateTimeOffset(text, out value);
			default:
				throw EntityUtil.NotSupported(primitiveType.FullName);
			}
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x0000F5E8 File Offset: 0x0000D7E8
		private static bool TryParseBoolean(string text, out object value)
		{
			bool flag;
			if (!bool.TryParse(text, out flag))
			{
				value = null;
				return false;
			}
			value = flag;
			return true;
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x0000F610 File Offset: 0x0000D810
		private static bool TryParseByte(string text, out object value)
		{
			byte b;
			if (!byte.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out b))
			{
				value = null;
				return false;
			}
			value = b;
			return true;
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x0000F63C File Offset: 0x0000D83C
		private static bool TryParseSByte(string text, out object value)
		{
			sbyte b;
			if (!sbyte.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out b))
			{
				value = null;
				return false;
			}
			value = b;
			return true;
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x0000F668 File Offset: 0x0000D868
		private static bool TryParseInt16(string text, out object value)
		{
			short num;
			if (!short.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
			{
				value = null;
				return false;
			}
			value = num;
			return true;
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x0000F694 File Offset: 0x0000D894
		private static bool TryParseInt32(string text, out object value)
		{
			int num;
			if (!int.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
			{
				value = null;
				return false;
			}
			value = num;
			return true;
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0000F6C0 File Offset: 0x0000D8C0
		private static bool TryParseInt64(string text, out object value)
		{
			long num;
			if (!long.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
			{
				value = null;
				return false;
			}
			value = num;
			return true;
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0000F6EC File Offset: 0x0000D8EC
		private static bool TryParseDouble(string text, out object value)
		{
			double num;
			if (!double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out num))
			{
				value = null;
				return false;
			}
			value = num;
			return true;
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x0000F71C File Offset: 0x0000D91C
		private static bool TryParseDecimal(string text, out object value)
		{
			decimal num;
			if (!decimal.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out num))
			{
				value = null;
				return false;
			}
			value = num;
			return true;
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x0000F74C File Offset: 0x0000D94C
		private static bool TryParseDateTime(string text, out object value)
		{
			DateTime dateTime;
			if (!DateTime.TryParseExact(text, "yyyy-MM-dd HH\\:mm\\:ss.fffZ", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out dateTime))
			{
				value = null;
				return false;
			}
			value = dateTime;
			return true;
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x0000F780 File Offset: 0x0000D980
		private static bool TryParseTime(string text, out object value)
		{
			DateTime dateTime;
			if (!DateTime.TryParseExact(text, "HH\\:mm\\:ss.fffffffZ", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out dateTime))
			{
				value = null;
				return false;
			}
			value = new TimeSpan(dateTime.Ticks);
			return true;
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x0000F7BC File Offset: 0x0000D9BC
		private static bool TryParseDateTimeOffset(string text, out object value)
		{
			DateTimeOffset dateTimeOffset;
			if (!DateTimeOffset.TryParse(text, out dateTimeOffset))
			{
				value = null;
				return false;
			}
			value = dateTimeOffset;
			return true;
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0000F7E1 File Offset: 0x0000D9E1
		private static bool TryParseGuid(string text, out object value)
		{
			if (!ScalarUtils._GuidValueValidator.IsMatch(text))
			{
				value = null;
				return false;
			}
			value = new Guid(text);
			return true;
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x0000F803 File Offset: 0x0000DA03
		private static bool TryParseString(string text, out object value)
		{
			value = text;
			return true;
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x0000F80C File Offset: 0x0000DA0C
		private static bool TryParseBinary(string text, out object value)
		{
			if (!ScalarUtils._BinaryValueValidator.IsMatch(text))
			{
				value = null;
				return false;
			}
			string text2 = text.Substring(2);
			value = ScalarUtils.ConvertToByteArray(text2);
			return true;
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x0000F83C File Offset: 0x0000DA3C
		internal static byte[] ConvertToByteArray(string text)
		{
			int num = 2;
			int num2 = text.Length / 2;
			if (text.Length % 2 == 1)
			{
				num = 1;
				num2++;
			}
			byte[] array = new byte[num2];
			int i = 0;
			int num3 = 0;
			while (i < text.Length)
			{
				array[num3] = byte.Parse(text.Substring(i, num), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
				i += num;
				num = 2;
				num3++;
			}
			return array;
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x0000F8A4 File Offset: 0x0000DAA4
		private static bool TryParseSingle(string text, out object value)
		{
			float num;
			if (!float.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out num))
			{
				value = null;
				return false;
			}
			value = num;
			return true;
		}

		// Token: 0x04000691 RID: 1681
		internal const string DateTimeFormat = "yyyy-MM-dd HH\\:mm\\:ss.fffZ";

		// Token: 0x04000692 RID: 1682
		internal const string TimeFormat = "HH\\:mm\\:ss.fffffffZ";

		// Token: 0x04000693 RID: 1683
		internal const string DateTimeOffsetFormat = "yyyy-MM-dd HH\\:mm\\:ss.fffffffz";

		// Token: 0x04000694 RID: 1684
		private static readonly Regex _BinaryValueValidator = new Regex("^0[xX][0-9a-fA-F]+$");

		// Token: 0x04000695 RID: 1685
		private static readonly Regex _GuidValueValidator = new Regex("[0-9a-fA-F]{8,8}(-[0-9a-fA-F]{4,4}){3,3}-[0-9a-fA-F]{12,12}");
	}
}
