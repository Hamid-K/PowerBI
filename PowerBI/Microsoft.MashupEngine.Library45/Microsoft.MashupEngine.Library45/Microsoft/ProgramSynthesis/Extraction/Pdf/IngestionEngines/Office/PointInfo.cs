using System;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.IngestionEngines.Office
{
	// Token: 0x02000DB7 RID: 3511
	[NullableContext(1)]
	[Nullable(0)]
	internal struct PointInfo
	{
		// Token: 0x0600594C RID: 22860 RVA: 0x0011BF6B File Offset: 0x0011A16B
		public override string ToString()
		{
			return string.Format("({0}, {1})", this.x, this.y);
		}

		// Token: 0x0600594D RID: 22861 RVA: 0x0011BF8D File Offset: 0x0011A18D
		public Vector<PixelUnit> AsPixelVector(double featureScale)
		{
			return new Vector<PixelUnit>((int)((double)this.x * featureScale), (int)((double)this.y * featureScale));
		}

		// Token: 0x0600594E RID: 22862 RVA: 0x0011BFA8 File Offset: 0x0011A1A8
		public DoubleVector<PixelUnit> AsPixelDoubleVector(double featureScale)
		{
			return new DoubleVector<PixelUnit>((double)this.x * featureScale, (double)this.y * featureScale);
		}

		// Token: 0x040029A4 RID: 10660
		public readonly float x;

		// Token: 0x040029A5 RID: 10661
		public readonly float y;
	}
}
