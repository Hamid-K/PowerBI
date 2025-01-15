using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace System.Data.Entity.Infrastructure.Pluralization
{
	// Token: 0x0200026D RID: 621
	internal static class PluralizationServiceUtil
	{
		// Token: 0x06001F78 RID: 8056 RVA: 0x00059D68 File Offset: 0x00057F68
		internal static bool DoesWordContainSuffix(string word, IEnumerable<string> suffixes, CultureInfo culture)
		{
			return suffixes.Any((string s) => word.EndsWith(s, true, culture));
		}

		// Token: 0x06001F79 RID: 8057 RVA: 0x00059D9C File Offset: 0x00057F9C
		internal static bool TryGetMatchedSuffixForWord(string word, IEnumerable<string> suffixes, CultureInfo culture, out string matchedSuffix)
		{
			matchedSuffix = null;
			if (PluralizationServiceUtil.DoesWordContainSuffix(word, suffixes, culture))
			{
				matchedSuffix = suffixes.First((string s) => word.EndsWith(s, true, culture));
				return true;
			}
			return false;
		}

		// Token: 0x06001F7A RID: 8058 RVA: 0x00059DEC File Offset: 0x00057FEC
		internal static bool TryInflectOnSuffixInWord(string word, IEnumerable<string> suffixes, Func<string, string> operationOnWord, CultureInfo culture, out string newWord)
		{
			newWord = null;
			string text;
			if (PluralizationServiceUtil.TryGetMatchedSuffixForWord(word, suffixes, culture, out text))
			{
				newWord = operationOnWord(word);
				return true;
			}
			return false;
		}
	}
}
