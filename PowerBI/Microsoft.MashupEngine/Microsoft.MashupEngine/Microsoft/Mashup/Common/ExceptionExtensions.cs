using System;
using System.Collections;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001BF1 RID: 7153
	public static class ExceptionExtensions
	{
		// Token: 0x0600B29C RID: 45724 RVA: 0x00245C0C File Offset: 0x00243E0C
		public static T CopyExceptionDataFrom<T>(this T target, Exception source) where T : Exception
		{
			foreach (object obj in source.Data)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				string text = dictionaryEntry.Key as string;
				if (text != null && dictionaryEntry.Value is string)
				{
					target.Data[text] = (string)dictionaryEntry.Value;
				}
			}
			return target;
		}
	}
}
