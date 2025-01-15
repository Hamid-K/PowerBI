using System;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;

namespace System.Data.Entity
{
	// Token: 0x0200005C RID: 92
	public class DbContextTransaction : IDisposable
	{
		// Token: 0x0600029C RID: 668 RVA: 0x0000A4A4 File Offset: 0x000086A4
		internal DbContextTransaction(EntityConnection connection)
		{
			this._connection = connection;
			this.EnsureOpenConnection();
			this._entityTransaction = this._connection.BeginTransaction();
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000A4CA File Offset: 0x000086CA
		internal DbContextTransaction(EntityConnection connection, IsolationLevel isolationLevel)
		{
			this._connection = connection;
			this.EnsureOpenConnection();
			this._entityTransaction = this._connection.BeginTransaction(isolationLevel);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000A4F1 File Offset: 0x000086F1
		internal DbContextTransaction(EntityTransaction transaction)
		{
			this._connection = transaction.Connection;
			this.EnsureOpenConnection();
			this._entityTransaction = transaction;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000A512 File Offset: 0x00008712
		private void EnsureOpenConnection()
		{
			if (ConnectionState.Open != this._connection.State)
			{
				this._connection.Open();
				this._shouldCloseConnection = true;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x0000A534 File Offset: 0x00008734
		public DbTransaction UnderlyingTransaction
		{
			get
			{
				return this._entityTransaction.StoreTransaction;
			}
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000A541 File Offset: 0x00008741
		public void Commit()
		{
			this._entityTransaction.Commit();
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000A54E File Offset: 0x0000874E
		public void Rollback()
		{
			this._entityTransaction.Rollback();
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000A55B File Offset: 0x0000875B
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000A56C File Offset: 0x0000876C
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && !this._isDisposed)
			{
				this._connection.ClearCurrentTransaction();
				this._entityTransaction.Dispose();
				if (this._shouldCloseConnection && this._connection.State != ConnectionState.Closed)
				{
					this._connection.Close();
				}
				this._isDisposed = true;
			}
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000A5C1 File Offset: 0x000087C1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000A5C9 File Offset: 0x000087C9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000A5D2 File Offset: 0x000087D2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000A5DA File Offset: 0x000087DA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x040000B2 RID: 178
		private readonly EntityConnection _connection;

		// Token: 0x040000B3 RID: 179
		private readonly EntityTransaction _entityTransaction;

		// Token: 0x040000B4 RID: 180
		private bool _shouldCloseConnection;

		// Token: 0x040000B5 RID: 181
		private bool _isDisposed;
	}
}
