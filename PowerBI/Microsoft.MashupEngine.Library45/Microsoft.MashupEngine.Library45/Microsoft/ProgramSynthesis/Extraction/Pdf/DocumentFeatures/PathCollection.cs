using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Extraction.Pdf.GeometryUtilities;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000D18 RID: 3352
	[NullableContext(1)]
	[Nullable(0)]
	internal class PathCollection
	{
		// Token: 0x17000F93 RID: 3987
		// (get) Token: 0x060055E7 RID: 21991 RVA: 0x0010F57D File Offset: 0x0010D77D
		public QuadTree<GraphicalPath, PixelUnit> Paths { get; }

		// Token: 0x060055E8 RID: 21992 RVA: 0x0010F585 File Offset: 0x0010D785
		public PathCollection([Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> pageBounds, IEnumerable<GraphicalPath> paths)
		{
			this.Paths = new QuadTree<GraphicalPath, PixelUnit>(pageBounds, paths);
		}
	}
}
