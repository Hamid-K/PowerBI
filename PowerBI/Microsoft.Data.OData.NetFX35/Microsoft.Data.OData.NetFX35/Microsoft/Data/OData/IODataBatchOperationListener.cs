using System;

namespace Microsoft.Data.OData
{
	// Token: 0x0200024C RID: 588
	internal interface IODataBatchOperationListener
	{
		// Token: 0x060011EE RID: 4590
		void BatchOperationContentStreamRequested();

		// Token: 0x060011EF RID: 4591
		void BatchOperationContentStreamDisposed();
	}
}
