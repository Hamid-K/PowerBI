using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.Data.Common;

namespace Microsoft.Data.ProviderBase
{
	// Token: 0x0200016D RID: 365
	internal sealed class DbConnectionClosedConnecting : DbConnectionBusy
	{
		// Token: 0x06001AEE RID: 6894 RVA: 0x0006E06E File Offset: 0x0006C26E
		private DbConnectionClosedConnecting()
			: base(ConnectionState.Connecting)
		{
		}

		// Token: 0x06001AEF RID: 6895 RVA: 0x0006E077 File Offset: 0x0006C277
		internal override void CloseConnection(DbConnection owningObject, DbConnectionFactory connectionFactory)
		{
			connectionFactory.SetInnerConnectionTo(owningObject, DbConnectionClosedPreviouslyOpened.SingletonInstance);
		}

		// Token: 0x06001AF0 RID: 6896 RVA: 0x0006E085 File Offset: 0x0006C285
		internal override bool TryReplaceConnection(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource<DbConnectionInternal> retry, DbConnectionOptions userOptions)
		{
			return this.TryOpenConnection(outerConnection, connectionFactory, retry, userOptions);
		}

		// Token: 0x06001AF1 RID: 6897 RVA: 0x0006E094 File Offset: 0x0006C294
		internal override bool TryOpenConnection(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource<DbConnectionInternal> retry, DbConnectionOptions userOptions)
		{
			if (retry == null || !retry.Task.IsCompleted)
			{
				throw ADP.ConnectionAlreadyOpen(base.State);
			}
			DbConnectionInternal result = retry.Task.Result;
			if (result == null)
			{
				connectionFactory.SetInnerConnectionTo(outerConnection, this);
				throw ADP.InternalConnectionError(ADP.ConnectionError.GetConnectionReturnsNull);
			}
			connectionFactory.SetInnerConnectionEvent(outerConnection, result);
			return true;
		}

		// Token: 0x04000AEF RID: 2799
		internal static readonly DbConnectionInternal SingletonInstance = new DbConnectionClosedConnecting();
	}
}
