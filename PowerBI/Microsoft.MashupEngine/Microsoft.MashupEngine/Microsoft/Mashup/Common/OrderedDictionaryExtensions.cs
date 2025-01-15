using System;
using System.Collections.Specialized;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001C07 RID: 7175
	public static class OrderedDictionaryExtensions
	{
		// Token: 0x0600B31A RID: 45850 RVA: 0x002470D8 File Offset: 0x002452D8
		public static object Pop(this OrderedDictionary dictionary)
		{
			int num = dictionary.Count - 1;
			object obj = dictionary[num];
			dictionary.RemoveAt(num);
			return obj;
		}
	}
}
