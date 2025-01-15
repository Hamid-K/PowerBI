using System;
using AngleSharp.Network;

namespace AngleSharp.Extensions
{
	// Token: 0x020000F4 RID: 244
	public static class ResponseExtensions
	{
		// Token: 0x060007A0 RID: 1952 RVA: 0x00035C5C File Offset: 0x00033E5C
		public static MimeType GetContentType(this IResponse response)
		{
			string path = response.Address.Path;
			int num = path.LastIndexOf('.');
			string text = MimeTypeNames.FromExtension((num >= 0) ? path.Substring(num) : ".a");
			return new MimeType(response.Headers.GetOrDefault(HeaderNames.ContentType, text));
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x00035CAC File Offset: 0x00033EAC
		public static MimeType GetContentType(this IResponse response, string defaultType)
		{
			string path = response.Address.Path;
			int num = path.LastIndexOf('.');
			if (num >= 0)
			{
				defaultType = MimeTypeNames.FromExtension(path.Substring(num));
			}
			return new MimeType(response.Headers.GetOrDefault(HeaderNames.ContentType, defaultType));
		}
	}
}
