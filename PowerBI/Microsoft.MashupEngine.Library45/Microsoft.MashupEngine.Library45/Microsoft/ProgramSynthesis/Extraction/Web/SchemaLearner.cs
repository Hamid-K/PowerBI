using System;
using Microsoft.ProgramSynthesis.Extraction.Web.Semantics;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.SchemaParser;

namespace Microsoft.ProgramSynthesis.Extraction.Web
{
	// Token: 0x02000FC5 RID: 4037
	public class SchemaLearner : SchemaLearner<SchemaProgram, SequenceProgram, RegionProgram, string, WebRegion>
	{
		// Token: 0x06006F63 RID: 28515 RVA: 0x0016BFCB File Offset: 0x0016A1CB
		public override WebRegion Select(WebRegion input, string selector)
		{
			return input.GetRegion(selector);
		}

		// Token: 0x06006F64 RID: 28516 RVA: 0x0016BFD4 File Offset: 0x0016A1D4
		public override WebRegion StringToInput(string input)
		{
			return new WebRegion(HtmlDoc.Create(input));
		}

		// Token: 0x06006F65 RID: 28517 RVA: 0x0016BFE1 File Offset: 0x0016A1E1
		protected override SchemaProgram Wrap(SchemaGrammar<SequenceProgram, RegionProgram, WebRegion> grammar)
		{
			return new SchemaProgram(grammar);
		}

		// Token: 0x06006F66 RID: 28518 RVA: 0x0016BFE9 File Offset: 0x0016A1E9
		protected override ExtractionLearner<SequenceProgram, RegionProgram, WebRegion> GetLearner()
		{
			return ExtractionLearner.Instance;
		}
	}
}
