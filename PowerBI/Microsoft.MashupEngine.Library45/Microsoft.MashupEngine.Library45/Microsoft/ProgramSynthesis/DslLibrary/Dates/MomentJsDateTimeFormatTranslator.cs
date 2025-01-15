using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.DslLibrary.Dates
{
	// Token: 0x02000881 RID: 2177
	internal class MomentJsDateTimeFormatTranslator : RewritingDateTimeFormatTranslator
	{
		// Token: 0x06002F9B RID: 12187 RVA: 0x0008BAA7 File Offset: 0x00089CA7
		public MomentJsDateTimeFormatTranslator()
			: base(DateTimeFormat.Target.MomentJS)
		{
		}

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x06002F9C RID: 12188 RVA: 0x0000A5FD File Offset: 0x000087FD
		internal override bool OrdinalDaySupported
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x06002F9D RID: 12189 RVA: 0x0008BAB0 File Offset: 0x00089CB0
		public override IReadOnlyList<Record<string[], string>> Rewrites
		{
			get
			{
				return new Record<string[], string>[]
				{
					Record.Create<string[], string>(new string[] { "h", ":", "mm", " ", "A" }, "LT"),
					Record.Create<string[], string>(new string[] { "h", ":", "mm", ":", "ss", " ", "A" }, "LTS"),
					Record.Create<string[], string>(new string[] { "MM", "/", "DD", "/", "YYYY" }, "L"),
					Record.Create<string[], string>(new string[] { "M", "/", "D", "/", "YYYY" }, "l"),
					Record.Create<string[], string>(new string[] { "MMMM", " ", "D", ", ", "YYYY" }, "LL"),
					Record.Create<string[], string>(new string[] { "MMM", " ", "D", ", ", "YYYY" }, "ll"),
					Record.Create<string[], string>(new string[]
					{
						"MMMM", " ", "D", ", ", "YYYY", " ", "h", ":", "mm", " ",
						"A"
					}, "LLL"),
					Record.Create<string[], string>(new string[]
					{
						"MMM", " ", "D", ", ", "YYYY", " ", "h", ":", "mm", " ",
						"A"
					}, "lll"),
					Record.Create<string[], string>(new string[]
					{
						"dddd", ", ", "MMMM", " ", "D", ", ", "YYYY", " ", "h", ":",
						"mm", " ", "A"
					}, "LLLL"),
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
