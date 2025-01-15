using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000CD4 RID: 3284
	[NullableContext(1)]
	[Nullable(0)]
	internal class Line
	{
		// Token: 0x17000F42 RID: 3906
		// (get) Token: 0x0600546D RID: 21613 RVA: 0x00109B1F File Offset: 0x00107D1F
		public IReadOnlyList<IWord> Words { get; }

		// Token: 0x17000F43 RID: 3907
		// (get) Token: 0x0600546E RID: 21614 RVA: 0x00109B27 File Offset: 0x00107D27
		public float Angle { get; }

		// Token: 0x17000F44 RID: 3908
		// (get) Token: 0x0600546F RID: 21615 RVA: 0x00109B2F File Offset: 0x00107D2F
		[Nullable(new byte[] { 0, 1 })]
		public Range<PixelUnit> BasicVerticalBounds
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x06005470 RID: 21616 RVA: 0x00109B37 File Offset: 0x00107D37
		public Line(IReadOnlyList<IWord> words, float angle, [Nullable(new byte[] { 0, 1 })] Range<PixelUnit> basicVerticalBounds)
		{
			this.Words = words;
			this.Angle = angle;
			this.BasicVerticalBounds = basicVerticalBounds;
		}

		// Token: 0x06005471 RID: 21617 RVA: 0x00109B54 File Offset: 0x00107D54
		public override string ToString()
		{
			return string.Format("{{ Angle={0} deg, Vertical={1}, Words: {2} }}", MathUtils.ToDegrees(this.Angle), this.BasicVerticalBounds, StringUtils.BuildLimitedListLiteral<string>(this.Words.Select((IWord word) => word.MinimalToString()), 5));
		}
	}
}
