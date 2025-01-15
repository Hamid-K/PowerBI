using System;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Core.EntityClient
{
	// Token: 0x020005E1 RID: 1505
	public class EntityTransaction : DbTransaction
	{
		// Token: 0x0600498B RID: 18827 RVA: 0x00104D39 File Offset: 0x00102F39
		internal EntityTransaction()
		{
		}

		// Token: 0x0600498C RID: 18828 RVA: 0x00104D41 File Offset: 0x00102F41
		internal EntityTransaction(EntityConnection connection, DbTransaction storeTransaction)
		{
			this._connection = connection;
			this._storeTransaction = storeTransaction;
		}

		// Token: 0x17000E87 RID: 3719
		// (get) Token: 0x0600498D RID: 18829 RVA: 0x00104D57 File Offset: 0x00102F57
		public new virtual EntityConnection Connection
		{
			get
			{
				return (EntityConnection)this.DbConnection;
			}
		}

		// Token: 0x17000E88 RID: 3720
		// (get) Token: 0x0600498E RID: 18830 RVA: 0x00104D64 File Offset: 0x00102F64
		protected override DbConnection DbConnection
		{
			get
			{
				if (((this._storeTransaction != null) ? DbInterception.Dispatch.Transaction.GetConnection(this._storeTransaction, this.InterceptionContext) : null) == null)
				{
					return null;
				}
				return this._connection;
			}
		}

		// Token: 0x17000E89 RID: 3721
		// (get) Token: 0x0600498F RID: 18831 RVA: 0x00104D96 File Offset: 0x00102F96
		public override IsolationLevel IsolationLevel
		{
			get
			{
				if (this._storeTransaction == null)
				{
					return (IsolationLevel)0;
				}
				return DbInterception.Dispatch.Transaction.GetIsolationLevel(this._storeTransaction, this.InterceptionContext);
			}
		}

		// Token: 0x17000E8A RID: 3722
		// (get) Token: 0x06004990 RID: 18832 RVA: 0x00104DBD File Offset: 0x00102FBD
		public virtual DbTransaction StoreTransaction
		{
			get
			{
				return this._storeTransaction;
			}
		}

		// Token: 0x17000E8B RID: 3723
		// (get) Token: 0x06004991 RID: 18833 RVA: 0x00104DC5 File Offset: 0x00102FC5
		private DbInterceptionContext InterceptionContext
		{
			get
			{
				return DbInterceptionContext.Combine(this._connection.AssociatedContexts.Select((ObjectContext c) => c.InterceptionContext));
			}
		}

		// Token: 0x06004992 RID: 18834 RVA: 0x00104DFC File Offset: 0x00102FFC
		public override void Commit()
		{
			try
			{
				if (this._storeTransaction != null)
				{
					DbInterception.Dispatch.Transaction.Commit(this._storeTransaction, this.InterceptionContext);
				}
			}
			catch (Exception ex)
			{
				if (ex.IsCatchableExceptionType() && !(ex is CommitFailedException))
				{
					throw new EntityException(Strings.EntityClient_ProviderSpecificError("Commit"), ex);
				}
				throw;
			}
			this.ClearCurrentTransaction();
		}

		// Token: 0x06004993 RID: 18835 RVA: 0x00104E68 File Offset: 0x00103068
		public override void Rollback()
		{
			try
			{
				if (this._storeTransaction != null)
				{
					DbInterception.Dispatch.Transaction.Rollback(this._storeTransaction, this.InterceptionContext);
				}
			}
			catch (Exception ex)
			{
				if (ex.IsCatchableExceptionType())
				{
					throw new EntityException(Strings.EntityClient_ProviderSpecificError("Rollback"), ex);
				}
				throw;
			}
			this.ClearCurrentTransaction();
		}

		// Token: 0x06004994 RID: 18836 RVA: 0x00104ECC File Offset: 0x001030CC
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.ClearCurrentTransaction();
				if (this._storeTransaction != null)
				{
					DbInterception.Dispatch.Transaction.Dispose(this._storeTransaction, this.InterceptionContext);
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06004995 RID: 18837 RVA: 0x00104F01 File Offset: 0x00103101
		private void ClearCurrentTransaction()
		{
			if (this._connection != null && this._connection.CurrentTransaction == this)
			{
				this._connection.ClearCurrentTransaction();
			}
		}

		// Token: 0x040019F8 RID: 6648
		private readonly EntityConnection _connection;

		// Token: 0x040019F9 RID: 6649
		private readonly DbTransaction _storeTransaction;
	}
}
