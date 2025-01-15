using System;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000092 RID: 146
	internal interface IMetadataCache
	{
		// Token: 0x060008A9 RID: 2217
		IObjectCache GetObjectCache(InternalObjectType objectType);

		// Token: 0x060008AA RID: 2218
		void Populate(InternalObjectType objectType);

		// Token: 0x060008AB RID: 2219
		void Refresh(InternalObjectType objectType);

		// Token: 0x060008AC RID: 2220
		void CheckCacheIsValid();

		// Token: 0x060008AD RID: 2221
		void MarkNeedCheckForValidness();

		// Token: 0x060008AE RID: 2222
		void MarkAbandoned();

		// Token: 0x060008AF RID: 2223
		DataRow FindObjectByUniqueName(SchemaObjectType objectType, string nameUnique);
	}
}
