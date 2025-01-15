using System;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.IngestionEngines.Office
{
	// Token: 0x02000DB6 RID: 3510
	internal struct BoundingBox
	{
		// Token: 0x0600594B RID: 22859 RVA: 0x0011BF3C File Offset: 0x0011A13C
		[return: Nullable(new byte[] { 0, 1 })]
		public Bounds<PixelUnit> AsPixelBounds(double featureScale)
		{
			return new Bounds<PixelUnit>((int)((double)this.Left * featureScale), (int)((double)this.Right * featureScale), (int)((double)this.Top * featureScale), (int)((double)this.Bottom * featureScale));
		}

		// Token: 0x040029A0 RID: 10656
		public readonly float Left;

		// Token: 0x040029A1 RID: 10657
		public readonly float Top;

		// Token: 0x040029A2 RID: 10658
		public readonly float Right;

		// Token: 0x040029A3 RID: 10659
		public readonly float Bottom;
	}
}
