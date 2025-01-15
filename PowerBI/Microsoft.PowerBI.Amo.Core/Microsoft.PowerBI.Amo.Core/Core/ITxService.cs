using System;

namespace Microsoft.AnalysisServices.Core
{
	// Token: 0x020000E9 RID: 233
	internal interface ITxService
	{
		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06000E69 RID: 3689
		bool IsInTransaction { get; }

		// Token: 0x06000E6A RID: 3690
		void BeginTransaction();

		// Token: 0x06000E6B RID: 3691
		void BeginTransaction(Database initiatingDB);

		// Token: 0x06000E6C RID: 3692
		void CommitTransaction();

		// Token: 0x06000E6D RID: 3693
		IModelOperationResult CommitTransactionWithResult();

		// Token: 0x06000E6E RID: 3694
		void RollbackTransaction();
	}
}
