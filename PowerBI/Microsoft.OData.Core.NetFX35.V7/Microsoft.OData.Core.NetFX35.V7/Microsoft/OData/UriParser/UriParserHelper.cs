using System;
using System.Globalization;
using System.Linq;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001BF RID: 447
	internal static class UriParserHelper
	{
		// Token: 0x060011A9 RID: 4521 RVA: 0x00031183 File Offset: 0x0002F383
		internal static bool IsCharHexDigit(char c)
		{
			return (c >= '0' && c <= '9') || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F');
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x000311AA File Offset: 0x0002F3AA
		internal static bool TryRemovePrefix(string prefix, ref string text)
		{
			return UriParserHelper.TryRemoveLiteralPrefix(prefix, ref text);
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x000311B4 File Offset: 0x0002F3B4
		internal static bool TryRemoveQuotes(ref string text)
		{
			if (text.Length < 2)
			{
				return false;
			}
			char c = text.get_Chars(0);
			if (c != '\'' || text.get_Chars(text.Length - 1) != c)
			{
				return false;
			}
			string text2 = text.Substring(1, text.Length - 2);
			int num = 0;
			for (;;)
			{
				int num2 = text2.IndexOf(c, num);
				if (num2 < 0)
				{
					goto IL_0076;
				}
				text2 = text2.Remove(num2, 1);
				if (text2.Length < num2 + 1 || text2.get_Chars(num2) != c)
				{
					break;
				}
				num = num2 + 1;
			}
			return false;
			IL_0076:
			text = text2;
			return true;
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x0003123C File Offset: 0x0002F43C
		internal static string RemoveQuotes(string text)
		{
			char c = text.get_Chars(0);
			string text2 = text.Substring(1, text.Length - 2);
			int num = 0;
			for (;;)
			{
				int num2 = text2.IndexOf(c, num);
				if (num2 < 0)
				{
					break;
				}
				text2 = text2.Remove(num2, 1);
				num = num2 + 1;
			}
			return text2;
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x00031280 File Offset: 0x0002F480
		internal static bool TryRemoveLiteralSuffix(string suffix, ref string text)
		{
			text = text.Trim();
			if (text.Length <= suffix.Length || UriParserHelper.IsValidNumericConstant(text) || !text.EndsWith(suffix, 5))
			{
				return false;
			}
			text = text.Substring(0, text.Length - suffix.Length);
			return true;
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x000312D4 File Offset: 0x0002F4D4
		internal static bool TryRemoveLiteralPrefix(string prefix, ref string text)
		{
			if (text.StartsWith(prefix, 5))
			{
				text = text.Remove(0, prefix.Length);
				return true;
			}
			return false;
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x000312F4 File Offset: 0x0002F4F4
		internal static void ValidatePrefixLiteral(string typePrefixLiteralName)
		{
			if (!Enumerable.All<char>(typePrefixLiteralName.ToCharArray(), (char x) => char.IsLetter(x) || x == '.'))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, Strings.UriParserHelper_InvalidPrefixLiteral(typePrefixLiteralName), new object[0]));
			}
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x0003134C File Offset: 0x0002F54C
		internal static bool IsUriValueQuoted(string text)
		{
			if (text.Length < 2 || text.get_Chars(0) != '\'' || text.get_Chars(text.Length - 1) != '\'')
			{
				return false;
			}
			int num;
			for (int i = 1; i < text.Length - 1; i = num + 2)
			{
				num = text.IndexOf('\'', i, text.Length - i - 1);
				if (num == -1)
				{
					break;
				}
				if (num == text.Length - 2 || text.get_Chars(num + 1) != '\'')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x000313C8 File Offset: 0x0002F5C8
		internal static IEdmTypeReference GetLiteralEdmTypeReference(ExpressionTokenKind tokenKind)
		{
			switch (tokenKind)
			{
			case ExpressionTokenKind.BooleanLiteral:
				return EdmCoreModel.Instance.GetBoolean(false);
			case ExpressionTokenKind.StringLiteral:
				return EdmCoreModel.Instance.GetString(true);
			case ExpressionTokenKind.IntegerLiteral:
				return EdmCoreModel.Instance.GetInt32(false);
			case ExpressionTokenKind.Int64Literal:
				return EdmCoreModel.Instance.GetInt64(false);
			case ExpressionTokenKind.SingleLiteral:
				return EdmCoreModel.Instance.GetSingle(false);
			case ExpressionTokenKind.DateTimeOffsetLiteral:
				return EdmCoreModel.Instance.GetDateTimeOffset(false);
			case ExpressionTokenKind.DurationLiteral:
				return EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.Duration, false);
			case ExpressionTokenKind.DecimalLiteral:
				return EdmCoreModel.Instance.GetDecimal(false);
			case ExpressionTokenKind.DoubleLiteral:
				return EdmCoreModel.Instance.GetDouble(false);
			case ExpressionTokenKind.GuidLiteral:
				return EdmCoreModel.Instance.GetGuid(false);
			case ExpressionTokenKind.BinaryLiteral:
				return EdmCoreModel.Instance.GetBinary(true);
			case ExpressionTokenKind.GeographyLiteral:
				return EdmCoreModel.Instance.GetSpatial(EdmPrimitiveTypeKind.Geography, false);
			case ExpressionTokenKind.GeometryLiteral:
				return EdmCoreModel.Instance.GetSpatial(EdmPrimitiveTypeKind.Geometry, false);
			case ExpressionTokenKind.QuotedLiteral:
				return EdmCoreModel.Instance.GetString(true);
			case ExpressionTokenKind.DateLiteral:
				return EdmCoreModel.Instance.GetDate(false);
			case ExpressionTokenKind.TimeOfDayLiteral:
				return EdmCoreModel.Instance.GetTimeOfDay(false);
			}
			return null;
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x00031525 File Offset: 0x0002F725
		private static bool IsValidNumericConstant(string text)
		{
			return string.Equals(text, "INF", 5) || string.Equals(text, "-INF", 5) || string.Equals(text, "NaN", 5);
		}
	}
}
