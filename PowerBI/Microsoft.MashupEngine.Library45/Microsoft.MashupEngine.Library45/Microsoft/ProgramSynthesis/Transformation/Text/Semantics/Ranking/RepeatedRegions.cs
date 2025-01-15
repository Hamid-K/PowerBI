using System;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Ranking
{
	// Token: 0x02001D61 RID: 7521
	internal class RepeatedRegions : ProvenanceTraceFeature
	{
		// Token: 0x0600FCD3 RID: 64723 RVA: 0x0035EE02 File Offset: 0x0035D002
		public RepeatedRegions(Grammar grammar)
			: base(grammar, "RepeatedRegions", -39.6850121793903)
		{
		}

		// Token: 0x0600FCD4 RID: 64724 RVA: 0x0035EE19 File Offset: 0x0035D019
		protected override bool HasOccurrence(string input, uint start1, uint end1, uint start2, uint end2)
		{
			return end1 == end2 && start1 == start2;
		}
	}
}
