using System;

namespace Microsoft.OData.Core
{
	// Token: 0x020000A0 RID: 160
	internal interface IODataBatchOperationListener
	{
		// Token: 0x06000608 RID: 1544
		void BatchOperationContentStreamRequested();

		// Token: 0x06000609 RID: 1545
		void BatchOperationContentStreamDisposed();
	}
}
