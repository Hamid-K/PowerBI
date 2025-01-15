using System;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000092 RID: 146
	internal interface IMetadataCache
	{
		// Token: 0x060008B6 RID: 2230
		IObjectCache GetObjectCache(InternalObjectType objectType);

		// Token: 0x060008B7 RID: 2231
		void Populate(InternalObjectType objectType);

		// Token: 0x060008B8 RID: 2232
		void Refresh(InternalObjectType objectType);

		// Token: 0x060008B9 RID: 2233
		void CheckCacheIsValid();

		// Token: 0x060008BA RID: 2234
		void MarkNeedCheckForValidness();

		// Token: 0x060008BB RID: 2235
		void MarkAbandoned();

		// Token: 0x060008BC RID: 2236
		DataRow FindObjectByUniqueName(SchemaObjectType objectType, string nameUnique);
	}
}
