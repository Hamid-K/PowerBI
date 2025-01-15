using System;
using System.Globalization;
using Microsoft.ReportingServices.OData.Query;

namespace Microsoft.ReportingServices.OData.Json
{
	// Token: 0x02000012 RID: 18
	internal static class Errors
	{
		// Token: 0x06000062 RID: 98 RVA: 0x000030F1 File Offset: 0x000012F1
		internal static string UnsupportedType(string type)
		{
			return Errors.FormatInvariant("The data type '{0}' is not supported.", new object[] { type });
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003107 File Offset: 0x00001307
		internal static string UnsupportedPrimitiveType(PrimitiveTypeKind kind)
		{
			return Errors.FormatInvariant("The primitive type kind '{0}' is not supported.", new object[] { kind.ToString() });
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003129 File Offset: 0x00001329
		internal static string InvalidHexChar(string invalidChar)
		{
			return Errors.FormatInvariant("The character '{0}' is not a valid hex character.", new object[] { invalidChar });
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000313F File Offset: 0x0000133F
		internal static string UriLiteralUnsupportedType(string type)
		{
			return Errors.FormatInvariant("The type {0} is not a supported URI literal.", new object[] { type });
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003155 File Offset: 0x00001355
		private static string FormatInvariant(string format, params object[] args)
		{
			return string.Format(CultureInfo.InvariantCulture, format, args);
		}
	}
}
