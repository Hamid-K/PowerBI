using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.DslLibrary.Dates
{
	// Token: 0x02000882 RID: 2178
	internal class DayJsDateTimeFormatTranslator : RewritingDateTimeFormatTranslator
	{
		// Token: 0x06002F9E RID: 12190 RVA: 0x0008BE3B File Offset: 0x0008A03B
		public DayJsDateTimeFormatTranslator()
			: base(DateTimeFormat.Target.DayJSFormatting)
		{
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x06002F9F RID: 12191 RVA: 0x0008BE44 File Offset: 0x0008A044
		public override IReadOnlyList<Record<string[], string>> Rewrites
		{
			get
			{
				return new Record<string[], string>[]
				{
					Record.Create<string[], string>(new string[] { "h", ":", "mm", " ", "A" }, "LT"),
					Record.Create<string[], string>(new string[] { "h", ":", "mm", ":", "ss", " ", "A" }, "LTS"),
					Record.Create<string[], string>(new string[] { "MM", "/", "DD", "/", "YYYY" }, "L"),
					Record.Create<string[], string>(new string[] { "MMMM", " ", "D", ", ", "YYYY" }, "LL"),
					Record.Create<string[], string>(new string[]
					{
						"MMMM", " ", "D", ", ", "YYYY", " ", "h", ":", "mm", " ",
						"A"
					}, "LLL"),
					Record.Create<string[], string>(new string[]
					{
						"dddd", ", ", "MMMM", " ", "D", ", ", "YYYY", " ", "h", ":",
						"mm", " ", "A"
					}, "LLLL"),
					Record.Create<string[], string>(new string[] { "M", "/", "D", "/", "YYYY" }, "l"),
					Record.Create<string[], string>(new string[] { "MMM", " ", "D", ", ", "YYYY" }, "ll"),
					Record.Create<string[], string>(new string[]
					{
						"MMM", " ", "D", ", ", "YYYY", " ", "h", ":", "mm", " ",
						"A"
					}, "lll"),
					Record.Create<string[], string>(new string[]
					{
						"ddd", ", ", "MMM", " ", "D", ", ", "YYYY", " ", "h", ":",
						"mm", " ", "A"
					}, "llll")
				};
			}
		}
	}
}
