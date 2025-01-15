using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Web.Http.Routing;

namespace System.Net.Http.Formatting
{
	// Token: 0x0200000A RID: 10
	public class UriPathExtensionMapping : MediaTypeMapping
	{
		// Token: 0x0600004B RID: 75 RVA: 0x00003047 File Offset: 0x00001247
		public UriPathExtensionMapping(string uriPathExtension, string mediaType)
			: base(mediaType)
		{
			this.Initialize(uriPathExtension);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003057 File Offset: 0x00001257
		public UriPathExtensionMapping(string uriPathExtension, MediaTypeHeaderValue mediaType)
			: base(mediaType)
		{
			this.Initialize(uriPathExtension);
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00003067 File Offset: 0x00001267
		// (set) Token: 0x0600004E RID: 78 RVA: 0x0000306F File Offset: 0x0000126F
		public string UriPathExtension { get; private set; }

		// Token: 0x0600004F RID: 79 RVA: 0x00003078 File Offset: 0x00001278
		public override double TryMatchMediaType(HttpRequestMessage request)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			if (!string.Equals(UriPathExtensionMapping.GetUriPathExtensionOrNull(request), this.UriPathExtension, StringComparison.OrdinalIgnoreCase))
			{
				return 0.0;
			}
			return 1.0;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000030B0 File Offset: 0x000012B0
		private static string GetUriPathExtensionOrNull(HttpRequestMessage request)
		{
			IHttpRouteData routeData = request.GetRouteData();
			string text;
			if (routeData != null && routeData.Values.TryGetValue(UriPathExtensionMapping.UriPathExtensionKey, out text))
			{
				return text;
			}
			return null;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000030DE File Offset: 0x000012DE
		private void Initialize(string uriPathExtension)
		{
			if (string.IsNullOrWhiteSpace(uriPathExtension))
			{
				throw new ArgumentNullException("uriPathExtension");
			}
			this.UriPathExtension = uriPathExtension.Trim().TrimStart(new char[] { '.' });
		}

		// Token: 0x04000009 RID: 9
		public static readonly string UriPathExtensionKey = "ext";
	}
}
