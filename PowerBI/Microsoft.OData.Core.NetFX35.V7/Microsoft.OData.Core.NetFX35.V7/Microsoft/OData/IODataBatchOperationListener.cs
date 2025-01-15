using System;

namespace Microsoft.OData
{
	// Token: 0x02000013 RID: 19
	internal interface IODataBatchOperationListener
	{
		// Token: 0x0600008B RID: 139
		void BatchOperationContentStreamRequested();

		// Token: 0x0600008C RID: 140
		void BatchOperationContentStreamDisposed();
	}
}
