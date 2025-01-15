using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x020000E0 RID: 224
	internal sealed class UriPathParser
	{
		// Token: 0x06000577 RID: 1399 RVA: 0x0001349E File Offset: 0x0001169E
		internal UriPathParser(int maxSegments)
		{
			this.maxSegments = maxSegments;
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x000134B0 File Offset: 0x000116B0
		internal string[] ParsePath(string escapedRelativePathUri)
		{
			if (escapedRelativePathUri == null || string.IsNullOrEmpty(escapedRelativePathUri.Trim()))
			{
				return new string[0];
			}
			string[] array = escapedRelativePathUri.Split(new char[] { '/' }, 1);
			if (array.Length >= this.maxSegments)
			{
				throw new ODataException(Strings.UriQueryPathParser_TooManySegments);
			}
			return array;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00013500 File Offset: 0x00011700
		internal ICollection<string> ParsePathIntoSegments(Uri absoluteUri, Uri serviceBaseUri)
		{
			if (!UriUtils.UriInvariantInsensitiveIsBaseOf(serviceBaseUri, absoluteUri))
			{
				throw new ODataException(Strings.UriQueryPathParser_RequestUriDoesNotHaveTheCorrectBaseUri(absoluteUri, serviceBaseUri));
			}
			ICollection<string> collection;
			try
			{
				int num = serviceBaseUri.Segments.Length;
				string[] segments = absoluteUri.Segments;
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

		// Token: 0x04000255 RID: 597
		private readonly int maxSegments;
	}
}
