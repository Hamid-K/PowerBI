using System;
using System.Data.Common;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001098 RID: 4248
	internal class DbTransactionInfo : IDisposable, IEquatable<DbTransactionInfo>
	{
		// Token: 0x06006F1D RID: 28445 RVA: 0x0017F438 File Offset: 0x0017D638
		public DbTransactionInfo(DbEnvironment environment, string identity)
		{
			this.environment = environment;
			this.identity = identity;
		}

		// Token: 0x17001F54 RID: 8020
		// (get) Token: 0x06006F1E RID: 28446 RVA: 0x0017F44E File Offset: 0x0017D64E
		public string Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x17001F55 RID: 8021
		// (get) Token: 0x06006F1F RID: 28447 RVA: 0x0017F458 File Offset: 0x0017D658
		public DbTransaction Transaction
		{
			get
			{
				this.EnsureConnectionPool();
				DbTransaction transaction;
				using (SingletonDbConnectionPool<TransactedDbConnection>.SingletonDbConnection connection = this.connectionPool.GetConnection())
				{
					transaction = connection.InnerConnection.Transaction;
				}
				return transaction;
			}
		}

		// Token: 0x06006F20 RID: 28448 RVA: 0x0017F4A0 File Offset: 0x0017D6A0
		public override int GetHashCode()
		{
			return this.identity.GetHashCode();
		}

		// Token: 0x06006F21 RID: 28449 RVA: 0x0017F4AD File Offset: 0x0017D6AD
		public override bool Equals(object other)
		{
			return this.Equals(other as DbTransactionInfo);
		}

		// Token: 0x06006F22 RID: 28450 RVA: 0x0017F4BB File Offset: 0x0017D6BB
		public bool Equals(DbTransactionInfo other)
		{
			return other != null && this.identity == other.identity;
		}

		// Token: 0x06006F23 RID: 28451 RVA: 0x0017F4D3 File Offset: 0x0017D6D3
		public void SetGetConnection(Func<DbConnection> getConnection)
		{
			this.getConnection = getConnection;
		}

		// Token: 0x06006F24 RID: 28452 RVA: 0x0017F4DC File Offset: 0x0017D6DC
		public DbConnection GetConnection()
		{
			this.EnsureConnectionPool();
			return this.connectionPool.GetConnection();
		}

		// Token: 0x06006F25 RID: 28453 RVA: 0x0017F4F0 File Offset: 0x0017D6F0
		public void Commit()
		{
			if (this.connectionPool != null)
			{
				using (SingletonDbConnectionPool<TransactedDbConnection>.SingletonDbConnection connection = this.connectionPool.GetConnection())
				{
					connection.InnerConnection.Commit();
				}
			}
		}

		// Token: 0x06006F26 RID: 28454 RVA: 0x0017F538 File Offset: 0x0017D738
		public void Rollback()
		{
			if (this.connectionPool != null)
			{
				using (SingletonDbConnectionPool<TransactedDbConnection>.SingletonDbConnection connection = this.connectionPool.GetConnection())
				{
					connection.InnerConnection.Rollback();
				}
			}
		}

		// Token: 0x06006F27 RID: 28455 RVA: 0x0017F580 File Offset: 0x0017D780
		public void Dispose()
		{
			if (this.connectionPool != null)
			{
				this.connectionPool.Dispose();
				this.connectionPool = null;
			}
			this.getConnection = null;
			this.environment.UnregisterTransaction(this);
		}

		// Token: 0x06006F28 RID: 28456 RVA: 0x0017F5AF File Offset: 0x0017D7AF
		private void EnsureConnectionPool()
		{
			if (this.getConnection == null)
			{
				throw new InvalidOperationException("GetConnection has not been set.");
			}
			if (this.connectionPool == null)
			{
				this.connectionPool = SingletonDbConnectionPool.New<TransactedDbConnection>(new TransactedDbConnection(this.environment, this.getConnection()));
			}
		}

		// Token: 0x04003DA2 RID: 15778
		private readonly DbEnvironment environment;

		// Token: 0x04003DA3 RID: 15779
		private readonly string identity;

		// Token: 0x04003DA4 RID: 15780
		private Func<DbConnection> getConnection;

		// Token: 0x04003DA5 RID: 15781
		private SingletonDbConnectionPool<TransactedDbConnection> connectionPool;
	}
}
