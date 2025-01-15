using System;
using System.Data;

namespace Microsoft.Data.ProviderBase
{
	// Token: 0x0200016B RID: 363
	internal sealed class DbConnectionClosedBusy : DbConnectionBusy
	{
		// Token: 0x06001AEA RID: 6890 RVA: 0x0006E044 File Offset: 0x0006C244
		private DbConnectionClosedBusy()
			: base(ConnectionState.Closed)
		{
		}

		// Token: 0x04000AED RID: 2797
		internal static readonly DbConnectionInternal SingletonInstance = new DbConnectionClosedBusy();
	}
}
