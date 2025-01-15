using System;
using System.Globalization;
using System.Linq;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000163 RID: 355
	internal static class UriParserHelper
	{
		// Token: 0x0600120C RID: 4620 RVA: 0x00035BB1 File Offset: 0x00033DB1
		internal static bool IsCharHexDigit(char c)
		{
			return (c >= '0' && c <= '9') || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F');
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x00035BD8 File Offset: 0x00033DD8
		internal static bool TryRemovePrefix(string prefix, ref string text)
		{
			return UriParserHelper.TryRemoveLiteralPrefix(prefix, ref text);
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x00035BE4 File Offset: 0x00033DE4
		internal static bool TryRemoveQuotes(ref string text)
		{
			if (text.Length < 2)
			{
				return false;
			}
			char c = text[0];
			if (c != '\'' || text[text.Length - 1] != c)
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
				if (text2.Length < num2 + 1 || text2[num2] != c)
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

		// Token: 0x0600120F RID: 4623 RVA: 0x00035C6C File Offset: 0x00033E6C
		internal static string RemoveQuotes(string text)
		{
			char c = text[0];
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

		// Token: 0x06001210 RID: 4624 RVA: 0x00035CB0 File Offset: 0x00033EB0
		internal static bool TryRemoveLiteralSuffix(string suffix, ref string text)
		{
			text = text.Trim();
			if (text.Length <= suffix.Length || UriParserHelper.IsValidNumericConstant(text) || !text.EndsWith(suffix, StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			text = text.Substring(0, text.Length - suffix.Length);
			return true;
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x00035D04 File Offset: 0x00033F04
		internal static bool TryRemoveLiteralPrefix(string prefix, ref string text)
		{
			if (text.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
			{
				text = text.Remove(0, prefix.Length);
				return true;
			}
			return false;
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x00035D24 File Offset: 0x00033F24
		internal static void ValidatePrefixLiteral(string typePrefixLiteralName)
		{
			if (!typePrefixLiteralName.ToCharArray().All((char x) => char.IsLetter(x) || x == '.'))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, Strings.UriParserHelper_InvalidPrefixLiteral(typePrefixLiteralName), new object[0]));
			}
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x00035D7C File Offset: 0x00033F7C
		internal static bool IsUriValueQuoted(string text)
		{
			if (text.Length < 2 || text[0] != '\'' || text[text.Length - 1] != '\'')
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
				if (num == text.Length - 2 || text[num + 1] != '\'')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x00035DF8 File Offset: 0x00033FF8
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

		// Token: 0x06001215 RID: 4629 RVA: 0x00035F55 File Offset: 0x00034155
		internal static bool IsAnnotation(string identifier)
		{
			return !string.IsNullOrEmpty(identifier) && identifier[0] == '@' && identifier.Contains(".");
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x00035F77 File Offset: 0x00034177
		private static bool IsValidNumericConstant(string text)
		{
			return string.Equals(text, "INF", StringComparison.OrdinalIgnoreCase) || string.Equals(text, "-INF", StringComparison.OrdinalIgnoreCase) || string.Equals(text, "NaN", StringComparison.OrdinalIgnoreCase);
		}
	}
}
