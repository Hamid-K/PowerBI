using System;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Ranking
{
	// Token: 0x02001D60 RID: 7520
	internal class AdjacentRegions : ProvenanceTraceFeature
	{
		// Token: 0x0600FCD1 RID: 64721 RVA: 0x0035EDDD File Offset: 0x0035CFDD
		public AdjacentRegions(Grammar grammar)
			: base(grammar, "AdjacentRegions", 2.67355894463594)
		{
		}

		// Token: 0x0600FCD2 RID: 64722 RVA: 0x0035EDF4 File Offset: 0x0035CFF4
		protected override bool HasOccurrence(string input, uint start1, uint end1, uint start2, uint end2)
		{
			return end1 == start2 || end2 == start1;
		}
	}
}
