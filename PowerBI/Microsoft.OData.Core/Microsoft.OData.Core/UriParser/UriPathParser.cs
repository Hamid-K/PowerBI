using System;
using System.Collections.Generic;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000164 RID: 356
	public class UriPathParser
	{
		// Token: 0x06001217 RID: 4631 RVA: 0x00035FA3 File Offset: 0x000341A3
		public UriPathParser(ODataUriParserSettings settings)
		{
			this.maxSegments = settings.PathLimit;
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x00035FB8 File Offset: 0x000341B8
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
				int num = serviceBaseUri.AbsolutePath.Split(new char[] { '/' }).Length - 1;
				string[] array = uri.AbsolutePath.Split(new char[] { '/' });
				int num2 = -1;
				List<string> list = new List<string>();
				for (int i = num; i < array.Length; i++)
				{
					string text = array[i];
					if (text.Length != 0 && !(text == "/"))
					{
						if (text[text.Length - 1] == '/')
						{
							text = text.Substring(0, text.Length - 1);
						}
						if (list.Count == this.maxSegments)
						{
							throw new ODataException(Strings.UriQueryPathParser_TooManySegments);
						}
						if (text.Length >= 2 && text.EndsWith("::", StringComparison.Ordinal))
						{
							if (num2 == -1)
							{
								throw new ODataException(Strings.UriQueryPathParser_InvalidEscapeUri(text));
							}
							string text2 = string.Join("/", array, num2, i - num2 + 1);
							list.Add(":" + text2.Substring(0, text2.Length - 1));
							num2 = i + 1;
						}
						else if (text.Length >= 1 && text[text.Length - 1] == ':')
						{
							if (num2 == -1)
							{
								if (text != ":")
								{
									list.Add(text.Substring(0, text.Length - 1));
								}
								num2 = i + 1;
							}
							else
							{
								string text3 = ":" + string.Join("/", array, num2, i - num2 + 1);
								list.Add(text3);
								num2 = -1;
							}
						}
						else if (num2 == -1)
						{
							list.Add(Uri.UnescapeDataString(text));
						}
					}
				}
				if (num2 != -1 && num2 < array.Length)
				{
					string text4 = ":" + string.Join("/", array, num2, array.Length - num2);
					list.Add(text4);
				}
				collection = list.ToArray();
			}
			catch (FormatException ex)
			{
				throw new ODataException(Strings.UriQueryPathParser_SyntaxError, ex);
			}
			return collection;
		}

		// Token: 0x0400083D RID: 2109
		private readonly int maxSegments;
	}
}
