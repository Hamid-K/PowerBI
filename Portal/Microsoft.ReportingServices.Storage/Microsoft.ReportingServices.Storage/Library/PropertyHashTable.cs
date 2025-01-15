using System;
using System.Collections;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000018 RID: 24
	internal static class PropertyHashTable
	{
		// Token: 0x060000EC RID: 236 RVA: 0x00006740 File Offset: 0x00004940
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
