using System;
using System.Globalization;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils
{
	// Token: 0x020001AF RID: 431
	public static class UnicodeCategoryUtils
	{
		// Token: 0x0600098E RID: 2446 RVA: 0x0001C12F File Offset: 0x0001A32F
		public static UnicodeCategory GetUnicodeCategory(this char c)
		{
			return char.GetUnicodeCategory(c);
		}
	}
}
