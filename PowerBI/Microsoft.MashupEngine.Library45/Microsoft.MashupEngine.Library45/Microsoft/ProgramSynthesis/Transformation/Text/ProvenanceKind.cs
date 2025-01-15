using System;

namespace Microsoft.ProgramSynthesis.Transformation.Text
{
	// Token: 0x02001BC5 RID: 7109
	public enum ProvenanceKind
	{
		// Token: 0x040058D0 RID: 22736
		Unknown,
		// Token: 0x040058D1 RID: 22737
		Constant,
		// Token: 0x040058D2 RID: 22738
		Lookup,
		// Token: 0x040058D3 RID: 22739
		Substring,
		// Token: 0x040058D4 RID: 22740
		CaseTransformation,
		// Token: 0x040058D5 RID: 22741
		DateTimeTransformation,
		// Token: 0x040058D6 RID: 22742
		NumberTransformation
	}
}
