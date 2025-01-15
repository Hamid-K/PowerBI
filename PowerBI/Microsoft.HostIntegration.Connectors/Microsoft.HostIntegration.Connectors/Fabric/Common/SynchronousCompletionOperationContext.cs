using System;

namespace Microsoft.Fabric.Common
{
	// Token: 0x020003F5 RID: 1013
	internal sealed class SynchronousCompletionOperationContext : OperationContext
	{
		// Token: 0x06002387 RID: 9095 RVA: 0x0006CFBD File Offset: 0x0006B1BD
		public SynchronousCompletionOperationContext(AsyncCallback callback, object state)
			: base(callback, state)
		{
			base.OperationCompleted(true, null);
		}
	}
}
