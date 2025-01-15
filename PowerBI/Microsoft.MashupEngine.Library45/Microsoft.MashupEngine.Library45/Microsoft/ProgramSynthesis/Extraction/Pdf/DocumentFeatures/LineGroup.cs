using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000CD6 RID: 3286
	[NullableContext(1)]
	[Nullable(0)]
	internal class LineGroup
	{
		// Token: 0x17000F45 RID: 3909
		// (get) Token: 0x06005475 RID: 21621 RVA: 0x00109BCA File Offset: 0x00107DCA
		public IReadOnlyList<Line> Lines { get; }

		// Token: 0x17000F46 RID: 3910
		// (get) Token: 0x06005476 RID: 21622 RVA: 0x00109BD2 File Offset: 0x00107DD2
		public float Angle { get; }

		// Token: 0x17000F47 RID: 3911
		// (get) Token: 0x06005477 RID: 21623 RVA: 0x00109BDA File Offset: 0x00107DDA
		[Nullable(new byte[] { 0, 1 })]
		public Range<PixelUnit> BasicVerticalBounds
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x06005478 RID: 21624 RVA: 0x00109BE2 File Offset: 0x00107DE2
		public LineGroup(IReadOnlyList<Line> lines, float angle, [Nullable(new byte[] { 0, 1 })] Range<PixelUnit> basicVerticalBounds)
		{
			this.Lines = lines;
			this.Angle = angle;
			this.BasicVerticalBounds = basicVerticalBounds;
		}

		// Token: 0x06005479 RID: 21625 RVA: 0x00109C00 File Offset: 0x00107E00
		public override string ToString()
		{
			return string.Format("{{ Angle={0} deg, Vertical={1}, Lines: {2} }}", MathUtils.ToDegrees(this.Angle), this.BasicVerticalBounds, StringUtils.BuildLimitedListLiteral<string>(this.Lines.Select((Line line) => line.ToString()), 5));
		}
	}
}
