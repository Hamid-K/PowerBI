using System;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.DslLibrary.Dates;

namespace Microsoft.ProgramSynthesis.FormatParsing.Dates
{
	// Token: 0x02000791 RID: 1937
	internal static class EmptyParseFactory
	{
		// Token: 0x06002997 RID: 10647 RVA: 0x00075CFC File Offset: 0x00073EFC
		public static DateTimeFormatMatch CreateEmptyPartialParse(StringRegion region)
		{
			return DateTimeFormatMatch.Empty(region);
		}
	}
}
