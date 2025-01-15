using System;
using System.ComponentModel;
using System.Net.Http.Headers;

namespace System.Net.Http.Formatting
{
	// Token: 0x02000009 RID: 9
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class MediaTypeFormatterExtensions
	{
		// Token: 0x06000049 RID: 73 RVA: 0x00002FE8 File Offset: 0x000011E8
		public static void AddUriPathExtensionMapping(this MediaTypeFormatter formatter, string uriPathExtension, MediaTypeHeaderValue mediaType)
		{
			if (formatter == null)
			{
				throw new ArgumentNullException("formatter");
			}
			UriPathExtensionMapping uriPathExtensionMapping = new UriPathExtensionMapping(uriPathExtension, mediaType);
			formatter.MediaTypeMappings.Add(uriPathExtensionMapping);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003018 File Offset: 0x00001218
		public static void AddUriPathExtensionMapping(this MediaTypeFormatter formatter, string uriPathExtension, string mediaType)
		{
			if (formatter == null)
			{
				throw new ArgumentNullException("formatter");
			}
			UriPathExtensionMapping uriPathExtensionMapping = new UriPathExtensionMapping(uriPathExtension, mediaType);
			formatter.MediaTypeMappings.Add(uriPathExtensionMapping);
		}
	}
}
