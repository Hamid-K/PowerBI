using System;
using System.Collections.Generic;

namespace Microsoft.AspNet.OData
{
	// Token: 0x02000038 RID: 56
	internal static class ContentIdHelpers
	{
		// Token: 0x06000151 RID: 337 RVA: 0x000066CC File Offset: 0x000048CC
		public static string ResolveContentId(string url, IDictionary<string, string> contentIdToLocationMapping)
		{
			int num = 0;
			int num2;
			string text2;
			for (;;)
			{
				num = url.IndexOf('$', num);
				if (num == -1)
				{
					return url;
				}
				num2 = 0;
				while (num + num2 < url.Length - 1 && ContentIdHelpers.IsContentIdCharacter(url[num + num2 + 1]))
				{
					num2++;
				}
				if (num2 > 0)
				{
					string text = url.Substring(num + 1, num2);
					if (contentIdToLocationMapping.TryGetValue(text, out text2))
					{
						break;
					}
				}
				num++;
			}
			return text2 + url.Substring(num + 1 + num2);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00006743 File Offset: 0x00004943
		public static void AddLocationHeaderToMapping(Uri location, IDictionary<string, string> contentIdToLocationMapping, string contentId)
		{
			if (location != null)
			{
				contentIdToLocationMapping.Add(contentId, location.AbsoluteUri);
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000675B File Offset: 0x0000495B
		private static bool IsContentIdCharacter(char c)
		{
			if (c <= '.')
			{
				if (c != '-' && c != '.')
				{
					goto IL_001D;
				}
			}
			else if (c != '_' && c != '~')
			{
				goto IL_001D;
			}
			return true;
			IL_001D:
			return char.IsLetterOrDigit(c);
		}
	}
}
