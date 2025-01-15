using System;
using System.Runtime.CompilerServices;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000CCB RID: 3275
	internal static class ICellFeatureExtensions
	{
		// Token: 0x06005434 RID: 21556 RVA: 0x0010948F File Offset: 0x0010768F
		[NullableContext(1)]
		public static bool IsCellSubsetOf<TCell>(this ICellFeature<TCell> possibleSubset, ICellFeature<TCell> possibleSuperset) where TCell : class, IWordAmalgamation<TCell>
		{
			return possibleSubset.Cells.IsSubsetOf(possibleSuperset.Cells);
		}
	}
}
