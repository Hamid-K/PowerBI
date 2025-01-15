using System;
using System.Data;

namespace Microsoft.Data.ProviderBase
{
	// Token: 0x0200016E RID: 366
	internal sealed class DbConnectionClosedNeverOpened : DbConnectionClosed
	{
		// Token: 0x06001AF3 RID: 6899 RVA: 0x0006E0F0 File Offset: 0x0006C2F0
		private DbConnectionClosedNeverOpened()
			: base(ConnectionState.Closed, false, true)
		{
		}

		// Token: 0x04000AF0 RID: 2800
		internal static readonly DbConnectionInternal SingletonInstance = new DbConnectionClosedNeverOpened();
	}
}
