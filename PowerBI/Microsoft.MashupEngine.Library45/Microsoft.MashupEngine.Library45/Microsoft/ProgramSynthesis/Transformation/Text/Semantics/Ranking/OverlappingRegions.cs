using System;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Ranking
{
	// Token: 0x02001D5F RID: 7519
	internal class OverlappingRegions : ProvenanceTraceFeature
	{
		// Token: 0x0600FCCF RID: 64719 RVA: 0x0035ED8E File Offset: 0x0035CF8E
		public OverlappingRegions(Grammar grammar)
			: base(grammar, "OverlappingRegions", -44.5439618477204)
		{
		}

		// Token: 0x0600FCD0 RID: 64720 RVA: 0x0035EDA8 File Offset: 0x0035CFA8
		protected override bool HasOccurrence(string input, uint start1, uint end1, uint start2, uint end2)
		{
			bool flag = end1 == end2 && start1 == start2;
			uint num = Math.Max(start1, start2);
			uint num2 = Math.Min(end1, end2);
			return num < num2 && !flag;
		}
	}
}
