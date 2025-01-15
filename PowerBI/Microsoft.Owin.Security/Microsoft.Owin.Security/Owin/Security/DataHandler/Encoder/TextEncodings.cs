using System;

namespace Microsoft.Owin.Security.DataHandler.Encoder
{
	// Token: 0x02000034 RID: 52
	public static class TextEncodings
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00003C05 File Offset: 0x00001E05
		public static ITextEncoder Base64
		{
			get
			{
				return TextEncodings.Base64Instance;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00003C0C File Offset: 0x00001E0C
		public static ITextEncoder Base64Url
		{
			get
			{
				return TextEncodings.Base64UrlInstance;
			}
		}

		// Token: 0x04000050 RID: 80
		private static readonly ITextEncoder Base64Instance = new Base64TextEncoder();

		// Token: 0x04000051 RID: 81
		private static readonly ITextEncoder Base64UrlInstance = new Base64UrlTextEncoder();
	}
}
