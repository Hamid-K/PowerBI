using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.Data.Common;

namespace Microsoft.Data.ProviderBase
{
	// Token: 0x0200016A RID: 362
	internal abstract class DbConnectionBusy : DbConnectionClosed
	{
		// Token: 0x06001AE8 RID: 6888 RVA: 0x0006E02C File Offset: 0x0006C22C
		protected DbConnectionBusy(ConnectionState state)
			: base(state, true, false)
		{
		}

		// Token: 0x06001AE9 RID: 6889 RVA: 0x0006E037 File Offset: 0x0006C237
		internal override bool TryOpenConnection(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource<DbConnectionInternal> retry, DbConnectionOptions userOptions)
		{
			throw ADP.ConnectionAlreadyOpen(base.State);
		}
	}
}
