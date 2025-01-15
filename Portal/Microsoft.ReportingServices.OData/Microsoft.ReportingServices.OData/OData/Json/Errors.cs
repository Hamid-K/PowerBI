using System;
using System.Globalization;
using Microsoft.ReportingServices.OData.Query;

namespace Microsoft.ReportingServices.OData.Json
{
	// Token: 0x0200000F RID: 15
	internal static class Errors
	{
		// Token: 0x06000063 RID: 99 RVA: 0x000031B1 File Offset: 0x000013B1
		internal static string UnsupportedType(string type)
		{
			return Errors.FormatInvariant("The data type '{0}' is not supported.", new object[] { type });
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000031C7 File Offset: 0x000013C7
		internal static string UnsupportedPrimitiveType(PrimitiveTypeKind kind)
		{
			return Errors.FormatInvariant("The primitive type kind '{0}' is not supported.", new object[] { kind.ToString() });
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000031E9 File Offset: 0x000013E9
		internal static string InvalidHexChar(string invalidChar)
		{
			return Errors.FormatInvariant("The character '{0}' is not a valid hex character.", new object[] { invalidChar });
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000031FF File Offset: 0x000013FF
		internal static string UriLiteralUnsupportedType(string type)
		{
			return Errors.FormatInvariant("The type {0} is not a supported URI literal.", new object[] { type });
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003215 File Offset: 0x00001415
		private static string FormatInvariant(string format, params object[] args)
		{
			return string.Format(CultureInfo.InvariantCulture, format, args);
		}
	}
}
