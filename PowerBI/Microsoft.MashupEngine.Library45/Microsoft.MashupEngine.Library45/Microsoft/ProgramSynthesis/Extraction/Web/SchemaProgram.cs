using System;
using Microsoft.ProgramSynthesis.Extraction.Web.Semantics;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.SchemaParser;

namespace Microsoft.ProgramSynthesis.Extraction.Web
{
	// Token: 0x02000FC6 RID: 4038
	public class SchemaProgram : SchemaProgram<SequenceProgram, RegionProgram, string, WebRegion>
	{
		// Token: 0x06006F68 RID: 28520 RVA: 0x0016BFF8 File Offset: 0x0016A1F8
		public SchemaProgram(SchemaGrammar<SequenceProgram, RegionProgram, WebRegion> sg)
			: base(sg)
		{
		}

		// Token: 0x06006F69 RID: 28521 RVA: 0x0016C001 File Offset: 0x0016A201
		public TreeElement<WebRegion> Run(string s)
		{
			return base.Run(new WebRegion(HtmlDoc.Create(s)));
		}

		// Token: 0x06006F6A RID: 28522 RVA: 0x0016BFCB File Offset: 0x0016A1CB
		public override WebRegion Select(WebRegion input, string selector)
		{
			return input.GetRegion(selector);
		}
	}
}
