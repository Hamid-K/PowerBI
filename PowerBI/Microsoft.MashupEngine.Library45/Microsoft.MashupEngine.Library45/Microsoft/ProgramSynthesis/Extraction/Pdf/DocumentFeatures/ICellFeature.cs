using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000CC9 RID: 3273
	[NullableContext(1)]
	internal interface ICellFeature<TCell> where TCell : class, IWordAmalgamation<TCell>
	{
		// Token: 0x17000F29 RID: 3881
		// (get) Token: 0x0600542E RID: 21550
		int Count { get; }

		// Token: 0x17000F2A RID: 3882
		// (get) Token: 0x0600542F RID: 21551
		HashSet<TCell> Cells { get; }
	}
}
