using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.OData.Interfaces;

namespace Microsoft.AspNet.OData
{
	// Token: 0x02000034 RID: 52
	internal static class RequestPreferenceHelpers
	{
		// Token: 0x0600013E RID: 318 RVA: 0x000062E8 File Offset: 0x000044E8
		internal static bool RequestPrefersReturnContent(IWebApiHeaders headers)
		{
			IEnumerable<string> enumerable = null;
			if (headers.TryGetValues("Prefer", out enumerable))
			{
				return enumerable.FirstOrDefault((string s) => s.IndexOf("return=representation", StringComparison.OrdinalIgnoreCase) >= 0) != null;
			}
			return false;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00006330 File Offset: 0x00004530
		internal static bool RequestPrefersReturnNoContent(IWebApiHeaders headers)
		{
			IEnumerable<string> enumerable = null;
			if (headers.TryGetValues("Prefer", out enumerable))
			{
				return enumerable.FirstOrDefault((string s) => s.IndexOf("return=minimal", StringComparison.OrdinalIgnoreCase) >= 0) != null;
			}
			return false;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00006378 File Offset: 0x00004578
		internal static bool RequestPrefersMaxPageSize(IWebApiHeaders headers, out int pageSize)
		{
			pageSize = -1;
			IEnumerable<string> enumerable = null;
			if (headers.TryGetValues("Prefer", out enumerable))
			{
				pageSize = RequestPreferenceHelpers.GetMaxPageSize(enumerable, "maxpagesize");
				if (pageSize >= 0)
				{
					return true;
				}
				pageSize = RequestPreferenceHelpers.GetMaxPageSize(enumerable, "odata.maxpagesize");
				if (pageSize >= 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x000063C4 File Offset: 0x000045C4
		private static int GetMaxPageSize(IEnumerable<string> preferences, string preferenceHeaderName)
		{
			string text = preferences.FirstOrDefault((string s) => s.IndexOf(preferenceHeaderName, StringComparison.OrdinalIgnoreCase) >= 0);
			if (string.IsNullOrEmpty(text))
			{
				return -1;
			}
			int num = text.IndexOf(preferenceHeaderName, StringComparison.OrdinalIgnoreCase) + preferenceHeaderName.Length;
			string text2 = string.Empty;
			if (text[num++] == '=')
			{
				while (num < text.Length && char.IsDigit(text[num]))
				{
					text2 += text[num++].ToString();
				}
			}
			int num2 = -1;
			if (int.TryParse(text2, out num2))
			{
				return num2;
			}
			return -1;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00006470 File Offset: 0x00004670
		internal static string GetRequestPreferHeader(IWebApiHeaders headers)
		{
			IEnumerable<string> enumerable;
			if (headers.TryGetValues("Prefer", out enumerable))
			{
				return enumerable.FirstOrDefault<string>();
			}
			return null;
		}

		// Token: 0x04000054 RID: 84
		public const string PreferHeaderName = "Prefer";

		// Token: 0x04000055 RID: 85
		public const string ReturnContentHeaderValue = "return=representation";

		// Token: 0x04000056 RID: 86
		public const string ReturnNoContentHeaderValue = "return=minimal";

		// Token: 0x04000057 RID: 87
		public const string ODataMaxPageSize = "odata.maxpagesize";

		// Token: 0x04000058 RID: 88
		public const string MaxPageSize = "maxpagesize";
	}
}
