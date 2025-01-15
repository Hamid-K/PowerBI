using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Uris
{
	// Token: 0x020002B2 RID: 690
	internal static class UriErrors
	{
		// Token: 0x06001B45 RID: 6981 RVA: 0x00038EF2 File Offset: 0x000370F2
		public static ValueException InputInvalid(string uriText)
		{
			return ValueException.NewDataFormatError<Message1>(Strings.UriInputInvalid(uriText), TextValue.New(uriText), null);
		}

		// Token: 0x06001B46 RID: 6982 RVA: 0x00038F06 File Offset: 0x00037106
		public static ValueException InputInvalid(string uriText, string errorMessage)
		{
			return ValueException.NewDataFormatError(errorMessage, TextValue.New(uriText), null);
		}

		// Token: 0x06001B47 RID: 6983 RVA: 0x00038F15 File Offset: 0x00037115
		public static ValueException InputInvalid(Value uriComponent, string errorMessage)
		{
			return ValueException.NewDataFormatError(errorMessage, uriComponent, null);
		}

		// Token: 0x06001B48 RID: 6984 RVA: 0x00038F1F File Offset: 0x0003711F
		public static ValueException InvalidHttpsScheme(TextValue url)
		{
			return ValueException.NewExpressionError<Message0>(Strings.UriInvalidHttps, url, null);
		}
	}
}
