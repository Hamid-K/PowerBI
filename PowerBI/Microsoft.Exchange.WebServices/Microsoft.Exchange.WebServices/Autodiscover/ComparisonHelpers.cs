using System;
using System.Collections;

namespace Microsoft.Exchange.WebServices.Autodiscover
{
	// Token: 0x0200000D RID: 13
	internal static class ComparisonHelpers
	{
		// Token: 0x0600003B RID: 59 RVA: 0x000028C8 File Offset: 0x000018C8
		internal static bool CaseInsensitiveContains(this ICollection collection, string match)
		{
			foreach (object obj in collection)
			{
				string text = obj as string;
				if (text != null && string.Compare(text, match, 5) == 0)
				{
					return true;
				}
			}
			return false;
		}
	}
}
