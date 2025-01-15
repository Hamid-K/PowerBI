using System;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Extraction.Pdf.GeometryUtilities;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000CEC RID: 3308
	internal static class MergedCellExtensions
	{
		// Token: 0x060054EC RID: 21740 RVA: 0x0010ABC4 File Offset: 0x00108DC4
		[NullableContext(1)]
		internal static void MergeCell(this QuadTree<ICell, PixelUnit> cells, MergedCell<ICell> cellToMerge)
		{
			cells.RemoveRange(cellToMerge.Cells);
			cells.Add(cellToMerge.AsICell);
		}
	}
}
