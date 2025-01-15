using System;

namespace Microsoft.Fabric.Common
{
	// Token: 0x020003E1 RID: 993
	internal interface IAbortableAsyncResult : IAsyncResult
	{
		// Token: 0x060022DF RID: 8927
		void Abort(Exception exception);
	}
}
