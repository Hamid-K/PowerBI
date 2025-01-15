using System;

namespace Microsoft.AnalysisServices.Tabular.Tmdl
{
	// Token: 0x02000130 RID: 304
	internal interface ITmdlReader
	{
		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x060014A1 RID: 5281
		int LineNumber { get; }

		// Token: 0x060014A2 RID: 5282
		TmdlTextLine ReadLine();
	}
}
