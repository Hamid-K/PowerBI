using System;

namespace Microsoft.AnalysisServices.Tabular.Tmdl.Schema
{
	// Token: 0x02000156 RID: 342
	internal interface ITmdlPropertiesContainer
	{
		// Token: 0x060015A3 RID: 5539
		bool TryGetPropertyInfo(string keyword, out TmdlPropertyInfo info);

		// Token: 0x060015A4 RID: 5540
		TmdlPropertyInfo FindProperty(string propertyName);
	}
}
