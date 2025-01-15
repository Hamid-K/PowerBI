using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000CCA RID: 3274
	[NullableContext(1)]
	[Nullable(0)]
	internal class CompareByCellCountDescending<TCell> : IComparer<ICellFeature<TCell>> where TCell : class, IWordAmalgamation<TCell>
	{
		// Token: 0x17000F2B RID: 3883
		// (get) Token: 0x06005430 RID: 21552 RVA: 0x00109453 File Offset: 0x00107653
		public static CompareByCellCountDescending<TCell> Instance { get; } = new CompareByCellCountDescending<TCell>();

		// Token: 0x06005431 RID: 21553 RVA: 0x0010945C File Offset: 0x0010765C
		public int Compare(ICellFeature<TCell> x, ICellFeature<TCell> y)
		{
			int num;
			if (ComparableUtilities.TryHandleNullVariables(x, y, out num))
			{
				return num;
			}
			return x.Count - y.Count;
		}
	}
}
