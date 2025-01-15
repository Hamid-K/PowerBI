using System;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000D28 RID: 3368
	[NullableContext(1)]
	[Nullable(0)]
	internal static class PdfTableUtilities
	{
		// Token: 0x06005653 RID: 22099 RVA: 0x00110FF8 File Offset: 0x0010F1F8
		[return: Nullable(new byte[] { 1, 2 })]
		public static string[,] ToTextTable<TCell>([Nullable(new byte[] { 0, 2 })] this RectangularArray<TCell> table) where TCell : class, IWordAmalgamation<TCell>
		{
			return table.Select<string>(delegate([Nullable(2)] TCell cell)
			{
				TCell tcell = cell;
				if (tcell == null)
				{
					return null;
				}
				return tcell.Content;
			}).ToMultidimensionalArray();
		}

		// Token: 0x06005654 RID: 22100 RVA: 0x00111033 File Offset: 0x0010F233
		public static bool TableRangeIncludes(this IPdfTable table, int pageIndex)
		{
			return pageIndex >= table.StartingPageIndex && pageIndex <= table.EndingPageIndex;
		}
	}
}
