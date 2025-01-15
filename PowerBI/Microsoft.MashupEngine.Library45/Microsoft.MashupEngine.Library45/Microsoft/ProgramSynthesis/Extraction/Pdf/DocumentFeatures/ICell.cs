using System;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000CC8 RID: 3272
	public interface ICell : IWordAmalgamation<ICell>, IApparentPixelBounded, IPixelBounded, IBounded<PixelUnit>
	{
		// Token: 0x0600542C RID: 21548
		[NullableContext(1)]
		ICell Join(ICell other, bool newLine, bool includeSpace = true);

		// Token: 0x0600542D RID: 21549
		[NullableContext(1)]
		ICell WithBounds([Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> bounds, bool fromBorders);
	}
}
