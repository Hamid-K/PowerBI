using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.IngestionEngines
{
	// Token: 0x02000DAB RID: 3499
	[NullableContext(1)]
	[Nullable(0)]
	public class PageData
	{
		// Token: 0x17001037 RID: 4151
		// (get) Token: 0x0600591C RID: 22812 RVA: 0x0011B7A8 File Offset: 0x001199A8
		public int PageIndex { get; }

		// Token: 0x17001038 RID: 4152
		// (get) Token: 0x0600591D RID: 22813 RVA: 0x0011B7B0 File Offset: 0x001199B0
		public IReadOnlyList<IReadOnlyList<Glyph>> Glyphs { get; }

		// Token: 0x17001039 RID: 4153
		// (get) Token: 0x0600591E RID: 22814 RVA: 0x0011B7B8 File Offset: 0x001199B8
		[Nullable(new byte[] { 0, 1 })]
		public Bounds<PixelUnit> PageBounds
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x1700103A RID: 4154
		// (get) Token: 0x0600591F RID: 22815 RVA: 0x0011B7C0 File Offset: 0x001199C0
		public IReadOnlyList<GraphicalPath> Paths { get; }

		// Token: 0x1700103B RID: 4155
		// (get) Token: 0x06005920 RID: 22816 RVA: 0x0011B7C8 File Offset: 0x001199C8
		public IReadOnlyList<Image> Images { get; }

		// Token: 0x06005921 RID: 22817 RVA: 0x0011B7D0 File Offset: 0x001199D0
		public PageData(int pageIndex, IReadOnlyList<IReadOnlyList<Glyph>> glyphs, [Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> pageBounds, IReadOnlyList<GraphicalPath> paths, IReadOnlyList<Image> images)
		{
			this.PageIndex = pageIndex;
			this.Glyphs = glyphs;
			this.PageBounds = pageBounds;
			this.Paths = paths;
			this.Images = images;
		}
	}
}
