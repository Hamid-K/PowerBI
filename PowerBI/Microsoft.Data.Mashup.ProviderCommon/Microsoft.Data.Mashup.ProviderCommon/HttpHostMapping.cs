using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.EngineHost.Services;

namespace Microsoft.Data.Mashup.ProviderCommon
{
	// Token: 0x0200000D RID: 13
	internal static class HttpHostMapping
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002FD7 File Offset: 0x000011D7
		// (set) Token: 0x06000040 RID: 64 RVA: 0x00002FDE File Offset: 0x000011DE
		internal static IHttpUriRewritingService TestHook { get; set; }

		// Token: 0x06000041 RID: 65 RVA: 0x00002FE8 File Offset: 0x000011E8
		public static void SetHttpHostMapping(Uri originalUri, Uri replacedUri)
		{
			if (originalUri == null)
			{
				throw new ArgumentNullException("originalUri");
			}
			HttpHostMapping.ValidateUri(originalUri, "originalUri");
			HttpHostMapping.ValidateUri(replacedUri, "replacedUri");
			Dictionary<string, KeyValuePair<Uri, Uri>> dictionary = HttpHostMapping.map;
			lock (dictionary)
			{
				string absoluteUri = originalUri.AbsoluteUri;
				if (replacedUri == null)
				{
					HttpHostMapping.map.Remove(absoluteUri);
				}
				else
				{
					HttpHostMapping.map[absoluteUri] = new KeyValuePair<Uri, Uri>(originalUri, replacedUri);
				}
				HttpHostMapping.service = null;
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003084 File Offset: 0x00001284
		public static bool TryCreateService(out IHttpUriRewritingService uriRewritingService)
		{
			Dictionary<string, KeyValuePair<Uri, Uri>> dictionary = HttpHostMapping.map;
			bool flag2;
			lock (dictionary)
			{
				if (HttpHostMapping.service == null && HttpHostMapping.map.Count > 0)
				{
					Uri[,] array = new Uri[HttpHostMapping.map.Count, 2];
					int num = 0;
					foreach (KeyValuePair<Uri, Uri> keyValuePair in HttpHostMapping.map.Values)
					{
						array[num, 0] = keyValuePair.Key;
						array[num, 1] = keyValuePair.Value;
						num++;
					}
					HttpHostMapping.service = new HttpHostRewritingService(array);
				}
				IHttpUriRewritingService testHook = HttpHostMapping.TestHook;
				if (testHook != null && HttpHostMapping.service != null)
				{
					throw new InvalidOperationException("Test hook set when also using public remapping");
				}
				uriRewritingService = HttpHostMapping.service ?? testHook;
				flag2 = uriRewritingService != null;
			}
			return flag2;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000318C File Offset: 0x0000138C
		private static void ValidateUri(Uri uri, string argumentName)
		{
			if (uri != null && (uri.PathAndQuery.Length > 1 || uri.Fragment.Length > 0))
			{
				throw new ArgumentException(ProviderErrorStrings.UriCannotContainPath, argumentName);
			}
		}

		// Token: 0x04000020 RID: 32
		private static Dictionary<string, KeyValuePair<Uri, Uri>> map = new Dictionary<string, KeyValuePair<Uri, Uri>>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000021 RID: 33
		private static IHttpUriRewritingService service;
	}
}
