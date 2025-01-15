using System;
using System.Threading;

namespace NLog.Internal
{
	// Token: 0x0200010C RID: 268
	internal struct AsyncHelpersTask
	{
		// Token: 0x06000E6A RID: 3690 RVA: 0x00023DB4 File Offset: 0x00021FB4
		public AsyncHelpersTask(WaitCallback asyncDelegate)
		{
			this.AsyncDelegate = asyncDelegate;
		}

		// Token: 0x040003DF RID: 991
		public readonly WaitCallback AsyncDelegate;
	}
}
