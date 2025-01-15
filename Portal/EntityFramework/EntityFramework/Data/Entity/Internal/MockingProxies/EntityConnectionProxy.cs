using System;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Infrastructure.Interception;

namespace System.Data.Entity.Internal.MockingProxies
{
	// Token: 0x0200013A RID: 314
	internal class EntityConnectionProxy
	{
		// Token: 0x060014D8 RID: 5336 RVA: 0x00036BA6 File Offset: 0x00034DA6
		protected EntityConnectionProxy()
		{
		}

		// Token: 0x060014D9 RID: 5337 RVA: 0x00036BAE File Offset: 0x00034DAE
		public EntityConnectionProxy(EntityConnection entityConnection)
		{
			this._entityConnection = entityConnection;
		}

		// Token: 0x060014DA RID: 5338 RVA: 0x00036BBD File Offset: 0x00034DBD
		public static implicit operator EntityConnection(EntityConnectionProxy proxy)
		{
			return proxy._entityConnection;
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x060014DB RID: 5339 RVA: 0x00036BC5 File Offset: 0x00034DC5
		public virtual DbConnection StoreConnection
		{
			get
			{
				return this._entityConnection.StoreConnection;
			}
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x00036BD2 File Offset: 0x00034DD2
		public virtual void Dispose()
		{
			this._entityConnection.Dispose();
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x00036BE0 File Offset: 0x00034DE0
		public virtual EntityConnectionProxy CreateNew(DbConnection storeConnection)
		{
			EntityConnection entityConnection = new EntityConnection(this._entityConnection.GetMetadataWorkspace(), storeConnection);
			EntityTransaction currentTransaction = this._entityConnection.CurrentTransaction;
			if (currentTransaction != null && DbInterception.Dispatch.Transaction.GetConnection(currentTransaction.StoreTransaction, this._entityConnection.InterceptionContext) == storeConnection)
			{
				entityConnection.UseStoreTransaction(currentTransaction.StoreTransaction);
			}
			return new EntityConnectionProxy(entityConnection);
		}

		// Token: 0x040009C5 RID: 2501
		private readonly EntityConnection _entityConnection;
	}
}
