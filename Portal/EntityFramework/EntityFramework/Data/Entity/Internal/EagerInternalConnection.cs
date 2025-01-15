using System;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Interception;

namespace System.Data.Entity.Internal
{
	// Token: 0x020000FB RID: 251
	internal class EagerInternalConnection : InternalConnection
	{
		// Token: 0x06001258 RID: 4696 RVA: 0x000303BB File Offset: 0x0002E5BB
		public EagerInternalConnection(DbContext context, DbConnection existingConnection, bool connectionOwned)
			: base(new DbInterceptionContext().WithDbContext(context))
		{
			base.UnderlyingConnection = existingConnection;
			this._connectionOwned = connectionOwned;
			base.OnConnectionInitialized();
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06001259 RID: 4697 RVA: 0x000303E2 File Offset: 0x0002E5E2
		public override DbConnectionStringOrigin ConnectionStringOrigin
		{
			get
			{
				return DbConnectionStringOrigin.UserCode;
			}
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x000303E5 File Offset: 0x0002E5E5
		public override void Dispose()
		{
			if (this._connectionOwned)
			{
				if (base.UnderlyingConnection is EntityConnection)
				{
					base.UnderlyingConnection.Dispose();
					return;
				}
				DbInterception.Dispatch.Connection.Dispose(base.UnderlyingConnection, base.InterceptionContext);
			}
		}

		// Token: 0x04000919 RID: 2329
		private readonly bool _connectionOwned;
	}
}
