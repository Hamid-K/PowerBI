using System;
using System.Collections.Generic;

namespace Microsoft.OData.Core.UriParser.Parsers
{
	// Token: 0x02000215 RID: 533
	internal sealed class UriPathParser
	{
		// Token: 0x06001366 RID: 4966 RVA: 0x00046EF1 File Offset: 0x000450F1
		internal UriPathParser(int maxSegments)
		{
			this.maxSegments = maxSegments;
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x00046F00 File Offset: 0x00045100
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

		// Token: 0x06001368 RID: 4968 RVA: 0x00046F50 File Offset: 0x00045150
		internal ICollection<string> ParsePathIntoSegments(Uri fullUri, Uri serviceBaseUri)
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

		// Token: 0x04000847 RID: 2119
		private readonly int maxSegments;
	}
}
