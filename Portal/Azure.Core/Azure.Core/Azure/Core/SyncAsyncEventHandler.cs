using System;
using System.Threading.Tasks;

namespace Azure.Core
{
	// Token: 0x02000064 RID: 100
	// (Invoke) Token: 0x0600036E RID: 878
	public delegate Task SyncAsyncEventHandler<T>(T e) where T : SyncAsyncEventArgs;
}
