using System;

namespace Microsoft.AnalysisServices.Tabular.Tmdl.Schema
{
	// Token: 0x02000157 RID: 343
	internal interface ITmdlTranslationElementContainer
	{
		// Token: 0x060015A5 RID: 5541
		bool TryGetTranslationElementInfo(string keyword, out TmdlTranslationElementInfo info);
	}
}
