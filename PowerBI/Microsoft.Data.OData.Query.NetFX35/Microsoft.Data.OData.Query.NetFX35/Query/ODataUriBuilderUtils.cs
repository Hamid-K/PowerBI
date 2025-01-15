using System;
using System.Globalization;
using Microsoft.Data.OData;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x0200002A RID: 42
	internal static class ODataUriBuilderUtils
	{
		// Token: 0x060000BA RID: 186 RVA: 0x000057B4 File Offset: 0x000039B4
		internal static string Escape(string text)
		{
			return text.Replace("'", "''");
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000057C8 File Offset: 0x000039C8
		internal static string ToText(this InlineCountKind inlineCount)
		{
			switch (inlineCount)
			{
			case InlineCountKind.None:
				return "none";
			case InlineCountKind.AllPages:
				return "allpages";
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataUriBuilderUtils_ToText_InlineCountKind_UnreachableCodePath));
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00005804 File Offset: 0x00003A04
		internal static void NotSupported(QueryTokenKind kind)
		{
			throw new ODataException(Strings.UriBuilder_NotSupportedQueryToken(kind));
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00005816 File Offset: 0x00003A16
		internal static void NotSupported(Type type)
		{
			throw new ODataException(Strings.UriBuilder_NotSupportedClrLiteral(type.FullName));
		}

		// Token: 0x0400013A RID: 314
		internal const string IntegerFormat = "D";

		// Token: 0x0400013B RID: 315
		internal const string FloatFormat = "F";

		// Token: 0x0400013C RID: 316
		internal const string BinaryFormat = "X2";

		// Token: 0x0400013D RID: 317
		internal const string DoubleFormat = "R";

		// Token: 0x0400013E RID: 318
		internal const string DateTimeFormat = "yyyy-MM-ddTHH:mm:ss.FFFFFFF";

		// Token: 0x0400013F RID: 319
		internal const string DateTimeOffsetFormat = "yyyy-MM-ddTHH:mm:ss.FFFFFFFzzzzzzz";

		// Token: 0x04000140 RID: 320
		internal static readonly NumberFormatInfo DecimalFormatInfo = new NumberFormatInfo
		{
			NumberDecimalDigits = 0
		};

		// Token: 0x04000141 RID: 321
		internal static readonly NumberFormatInfo DoubleFormatInfo = new NumberFormatInfo
		{
			NumberDecimalDigits = 0,
			PositiveSign = string.Empty
		};
	}
}
