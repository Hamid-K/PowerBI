using System;
using System.Data;

namespace Microsoft.Data.ProviderBase
{
	// Token: 0x0200016C RID: 364
	internal sealed class DbConnectionOpenBusy : DbConnectionBusy
	{
		// Token: 0x06001AEC RID: 6892 RVA: 0x0006E059 File Offset: 0x0006C259
		private DbConnectionOpenBusy()
			: base(ConnectionState.Open)
		{
		}

		// Token: 0x04000AEE RID: 2798
		internal static readonly DbConnectionInternal SingletonInstance = new DbConnectionOpenBusy();
	}
}
