using System;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.IngestionEngines.Office
{
	// Token: 0x02000DB5 RID: 3509
	internal enum BidiKind : short
	{
		// Token: 0x04002994 RID: 10644
		C2_LEFTTORIGHT = 1,
		// Token: 0x04002995 RID: 10645
		C2_RIGHTTOLEFT,
		// Token: 0x04002996 RID: 10646
		C2_EUROPENUMBER,
		// Token: 0x04002997 RID: 10647
		C2_EUROPESEPARATOR,
		// Token: 0x04002998 RID: 10648
		C2_EUROPETERMINATOR,
		// Token: 0x04002999 RID: 10649
		C2_ARABICNUMBER,
		// Token: 0x0400299A RID: 10650
		C2_COMMONSEPARATOR,
		// Token: 0x0400299B RID: 10651
		C2_BLOCKSEPARATOR,
		// Token: 0x0400299C RID: 10652
		C2_SEGMENTSEPARATOR,
		// Token: 0x0400299D RID: 10653
		C2_WHITESPACE,
		// Token: 0x0400299E RID: 10654
		C2_OTHERNEUTRAL,
		// Token: 0x0400299F RID: 10655
		C2_NOTAPPLICABLE = 0
	}
}
