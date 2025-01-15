using System;
using System.Threading.Tasks;

namespace System
{
	// Token: 0x02000008 RID: 8
	public interface IAsyncDisposable
	{
		// Token: 0x06000008 RID: 8
		ValueTask DisposeAsync();
	}
}
