using System;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000088 RID: 136
	public static class Extensions
	{
		// Token: 0x06000272 RID: 626 RVA: 0x00005CE6 File Offset: 0x00003EE6
		public static IPosTagSet GetPosTagSet(this LanguageIdentifier id)
		{
			if (id == LanguageIdentifier.de_DE)
			{
				return GermanPosTagSet.Instance;
			}
			if (id == LanguageIdentifier.en_US)
			{
				return EnglishPosTagSet.Instance;
			}
			if (id != LanguageIdentifier.es_ES)
			{
				throw new NotImplementedException();
			}
			return SpanishPosTagSet.Instance;
		}
	}
}
