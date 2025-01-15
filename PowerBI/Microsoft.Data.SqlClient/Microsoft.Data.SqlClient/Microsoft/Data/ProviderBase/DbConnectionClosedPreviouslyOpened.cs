using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.Data.Common;

namespace Microsoft.Data.ProviderBase
{
	// Token: 0x0200016F RID: 367
	internal sealed class DbConnectionClosedPreviouslyOpened : DbConnectionClosed
	{
		// Token: 0x06001AF5 RID: 6901 RVA: 0x0006E107 File Offset: 0x0006C307
		private DbConnectionClosedPreviouslyOpened()
			: base(ConnectionState.Closed, true, true)
		{
		}

		// Token: 0x06001AF6 RID: 6902 RVA: 0x0006E085 File Offset: 0x0006C285
		internal override bool TryReplaceConnection(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource<DbConnectionInternal> retry, DbConnectionOptions userOptions)
		{
			return this.TryOpenConnection(outerConnection, connectionFactory, retry, userOptions);
		}

		// Token: 0x04000AF1 RID: 2801
		internal static readonly DbConnectionInternal SingletonInstance = new DbConnectionClosedPreviouslyOpened();
	}
}
