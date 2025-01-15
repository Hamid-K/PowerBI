using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000D71 RID: 3441
	[NullableContext(1)]
	[Nullable(0)]
	public class SeparatorGroup : IPixelBounded, IBounded<PixelUnit>
	{
		// Token: 0x17000FD3 RID: 4051
		// (get) Token: 0x060057BF RID: 22463 RVA: 0x00116F7B File Offset: 0x0011517B
		public IReadOnlyList<Separator> Separators { get; }

		// Token: 0x17000FD4 RID: 4052
		// (get) Token: 0x060057C0 RID: 22464 RVA: 0x00116F83 File Offset: 0x00115183
		[Nullable(new byte[] { 0, 1 })]
		Bounds<PixelUnit> IBounded<PixelUnit>.Bounds
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this.PixelBounds;
			}
		}

		// Token: 0x17000FD5 RID: 4053
		// (get) Token: 0x060057C1 RID: 22465 RVA: 0x00116F8B File Offset: 0x0011518B
		[Nullable(new byte[] { 0, 1 })]
		public Bounds<PixelUnit> PixelBounds
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000FD6 RID: 4054
		// (get) Token: 0x060057C2 RID: 22466 RVA: 0x00116F93 File Offset: 0x00115193
		public Color? StrokingColor { get; }

		// Token: 0x060057C3 RID: 22467 RVA: 0x00116F9C File Offset: 0x0011519C
		public SeparatorGroup(IEnumerable<Separator> separators)
		{
			this.Separators = separators.ToList<Separator>();
			this.StrokingColor = (from colors in this.Separators.Select((Separator sep) => sep.StrokingColor.SomeIfNotNull<Color>()).WholeSequenceOfValues<Color>()
				select colors.Mode(null)).OrElseNull<Color>();
			this.PixelBounds = Bounds<PixelUnit>.Join(this.Separators.Select((Separator sep) => sep.PixelBounds));
		}
	}
}
