using System;

namespace Microsoft.AnalysisServices.Tabular.Tmdl.Schema
{
	// Token: 0x02000155 RID: 341
	internal interface ITmdlObjectContainer
	{
		// Token: 0x060015A2 RID: 5538
		bool TryGetObjectInfo(string keyword, out TmdlObjectInfo info);
	}
}
