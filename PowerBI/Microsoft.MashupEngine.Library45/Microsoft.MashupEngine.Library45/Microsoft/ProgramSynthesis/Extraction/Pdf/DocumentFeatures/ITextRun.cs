using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000D78 RID: 3448
	[NullableContext(1)]
	public interface ITextRun : IWordAmalgamation<ITextRun>, IApparentPixelBounded, IPixelBounded, IBounded<PixelUnit>
	{
		// Token: 0x17000FDF RID: 4063
		// (get) Token: 0x060057DF RID: 22495
		bool IsRotated { get; }

		// Token: 0x17000FE0 RID: 4064
		// (get) Token: 0x060057E0 RID: 22496
		bool IsRotatedByRightAngle { get; }

		// Token: 0x17000FE1 RID: 4065
		// (get) Token: 0x060057E1 RID: 22497
		IReadOnlyList<PartialTextRun> PartialTextRuns { get; }

		// Token: 0x17000FE2 RID: 4066
		// (get) Token: 0x060057E2 RID: 22498
		string ContentRtl { get; }

		// Token: 0x17000FE3 RID: 4067
		// (get) Token: 0x060057E3 RID: 22499
		LogicalGlyphOrderingLine LogicalGlyphOrderingLine { get; }

		// Token: 0x17000FE4 RID: 4068
		// (get) Token: 0x060057E4 RID: 22500
		LogicalGlyphOrderingLine LogicalGlyphOrderingLineRtl { get; }
	}
}
