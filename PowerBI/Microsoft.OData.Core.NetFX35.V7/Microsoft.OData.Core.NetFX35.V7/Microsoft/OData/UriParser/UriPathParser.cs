using System;
using System.Collections.Generic;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200011F RID: 287
	public class UriPathParser
	{
		// Token: 0x06000D43 RID: 3395 RVA: 0x00026C11 File Offset: 0x00024E11
		public UriPathParser(ODataUriParserSettings settings)
		{
			this.maxSegments = settings.PathLimit;
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x00026C28 File Offset: 0x00024E28
		public virtual ICollection<string> ParsePathIntoSegments(Uri fullUri, Uri serviceBaseUri)
		{
			if (serviceBaseUri == null)
			{
				serviceBaseUri = UriUtils.CreateMockAbsoluteUri(null);
				fullUri = UriUtils.CreateMockAbsoluteUri(fullUri);
			}
			if (!UriUtils.UriInvariantInsensitiveIsBaseOf(serviceBaseUri, fullUri))
			{
				throw new ODataException(Strings.UriQueryPathParser_RequestUriDoesNotHaveTheCorrectBaseUri(fullUri, serviceBaseUri));
			}
			ICollection<string> collection;
			try
			{
				Uri uri = fullUri;
				int num = serviceBaseUri.Segments.Length;
				string[] segments = uri.Segments;
				List<string> list = new List<string>();
				for (int i = num; i < segments.Length; i++)
				{
					string text = segments[i];
					if (text.Length != 0 && text != "/")
					{
						if (text.get_Chars(text.Length - 1) == '/')
						{
							text = text.Substring(0, text.Length - 1);
						}
						if (list.Count == this.maxSegments)
						{
							throw new ODataException(Strings.UriQueryPathParser_TooManySegments);
						}
						list.Add(Uri.UnescapeDataString(text));
					}
				}
				collection = list.ToArray();
			}
			catch (UriFormatException ex)
			{
				throw new ODataException(Strings.UriQueryPathParser_SyntaxError, ex);
			}
			return collection;
		}

		// Token: 0x0400071E RID: 1822
		private readonly int maxSegments;
	}
}
