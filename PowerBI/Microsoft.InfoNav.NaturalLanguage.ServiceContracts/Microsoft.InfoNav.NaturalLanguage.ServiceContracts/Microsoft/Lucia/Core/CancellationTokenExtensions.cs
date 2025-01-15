using System;
using System.Threading;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000045 RID: 69
	public static class CancellationTokenExtensions
	{
		// Token: 0x06000114 RID: 276 RVA: 0x00003F29 File Offset: 0x00002129
		public static CancellationToken<T> WithTag<T>(this CancellationToken token, T tag)
		{
			return new CancellationToken<T>(token, tag);
		}
	}
}
