using System;
using System.Collections;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200007E RID: 126
	internal static class PropertyHashTable
	{
		// Token: 0x0600055A RID: 1370 RVA: 0x00015C88 File Offset: 0x00013E88
		internal static Hashtable Create(string[] strings)
		{
			Hashtable hashtable = new Hashtable(strings.Length, StringComparer.InvariantCultureIgnoreCase);
			foreach (string text in strings)
			{
				hashtable.Add(text, text);
			}
			return hashtable;
		}
	}
}
