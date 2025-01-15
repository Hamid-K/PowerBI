using System;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.DslLibrary.Dates;

namespace Microsoft.ProgramSynthesis.FormatParsing.Dates
{
	// Token: 0x02000792 RID: 1938
	public class DateTimeFormatParserBuilder : FormatParserBuilder<DateTimeFormat, DateTimeFormatMatch, PartialDateTime, StringRegion, DateTimeSpacerFormatParser>
	{
		// Token: 0x06002998 RID: 10648 RVA: 0x00075D04 File Offset: 0x00073F04
		public DateTimeFormatParserBuilder()
		{
			Func<StringRegion, DateTimeFormatMatch> func;
			if ((func = DateTimeFormatParserBuilder.<>O.<0>__CreateEmptyPartialParse) == null)
			{
				func = (DateTimeFormatParserBuilder.<>O.<0>__CreateEmptyPartialParse = new Func<StringRegion, DateTimeFormatMatch>(EmptyParseFactory.CreateEmptyPartialParse));
			}
			base..ctor(func);
		}

		// Token: 0x06002999 RID: 10649 RVA: 0x00075D28 File Offset: 0x00073F28
		public void AppendSpacer(string spacerName)
		{
			base.AppendSpacer(spacerName, new DateTimeSpacerFormatParser(false, null, null, null, 1, 1, null), null);
		}

		// Token: 0x02000793 RID: 1939
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x0400143B RID: 5179
			public static Func<StringRegion, DateTimeFormatMatch> <0>__CreateEmptyPartialParse;
		}
	}
}
