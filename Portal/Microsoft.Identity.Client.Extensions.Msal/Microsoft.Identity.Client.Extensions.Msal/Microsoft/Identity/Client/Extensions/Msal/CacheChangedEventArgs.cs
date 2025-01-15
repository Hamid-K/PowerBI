using System;
using System.Collections.Generic;

namespace Microsoft.Identity.Client.Extensions.Msal
{
	// Token: 0x02000015 RID: 21
	public class CacheChangedEventArgs : EventArgs
	{
		// Token: 0x0600004E RID: 78 RVA: 0x00002DC8 File Offset: 0x00000FC8
		public CacheChangedEventArgs(IEnumerable<string> added, IEnumerable<string> removed)
		{
			this.AccountsAdded = added;
			this.AccountsRemoved = removed;
		}

		// Token: 0x04000057 RID: 87
		public readonly IEnumerable<string> AccountsAdded;

		// Token: 0x04000058 RID: 88
		public readonly IEnumerable<string> AccountsRemoved;
	}
}
