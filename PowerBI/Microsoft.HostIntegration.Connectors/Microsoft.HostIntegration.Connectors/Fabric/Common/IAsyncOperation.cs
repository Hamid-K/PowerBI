using System;

namespace Microsoft.Fabric.Common
{
	// Token: 0x020003E7 RID: 999
	internal interface IAsyncOperation : IAbortableAsyncResult, IAsyncResult
	{
		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x0600230E RID: 8974
		Priority OperationPriority { get; }
	}
}
