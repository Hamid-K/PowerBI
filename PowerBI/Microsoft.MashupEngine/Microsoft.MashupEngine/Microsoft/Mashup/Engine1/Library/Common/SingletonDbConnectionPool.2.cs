using System;
using System.Data;
using System.Data.Common;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Internal;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001129 RID: 4393
	internal class SingletonDbConnectionPool<T> : IDisposable where T : DbConnection
	{
		// Token: 0x060072CC RID: 29388 RVA: 0x0018A720 File Offset: 0x00188920
		public SingletonDbConnectionPool(T connection)
		{
			this.connection = connection;
			if (this.connection.State != ConnectionState.Closed)
			{
				throw new InvalidOperationException("Connection must be closed when creating a singleton connection pool.");
			}
			this.connectionStringHash = SingletonDbConnectionPool<T>.GetSHA256Hash(connection.ConnectionString);
		}

		// Token: 0x060072CD RID: 29389 RVA: 0x0018A76D File Offset: 0x0018896D
		public SingletonDbConnectionPool<T>.SingletonDbConnection GetConnection()
		{
			if (this.connection == null)
			{
				throw new ObjectDisposedException("Singleton connection pool was disposed.");
			}
			if (this.singleton != null)
			{
				throw new InvalidOperationException("Singleton connection was accessed multiple times.");
			}
			this.singleton = new SingletonDbConnectionPool<T>.SingletonDbConnection(this);
			return this.singleton;
		}

		// Token: 0x060072CE RID: 29390 RVA: 0x0018A7AC File Offset: 0x001889AC
		public void Dispose()
		{
			if (this.connection != null)
			{
				this.connection.Close();
				this.connection.Dispose();
				this.connection = default(T);
			}
		}

		// Token: 0x060072CF RID: 29391 RVA: 0x0018A7E7 File Offset: 0x001889E7
		private void Dispose(SingletonDbConnectionPool<T>.SingletonDbConnection toDispose)
		{
			if (toDispose != this.singleton)
			{
				throw new InvalidOperationException("Disposing an invalid singleton.");
			}
			this.singleton = null;
		}

		// Token: 0x060072D0 RID: 29392 RVA: 0x0018A804 File Offset: 0x00188A04
		private static string GetSHA256Hash(string contents)
		{
			string @string;
			using (HashAlgorithm hashAlgorithm = CryptoAlgorithmFactory.CreateSHA256Algorithm())
			{
				hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(contents));
				@string = Base64Encoding.GetString(hashAlgorithm.Hash);
			}
			return @string;
		}

		// Token: 0x04003F46 RID: 16198
		private T connection;

		// Token: 0x04003F47 RID: 16199
		private string connectionStringHash;

		// Token: 0x04003F48 RID: 16200
		private SingletonDbConnectionPool<T>.SingletonDbConnection singleton;

		// Token: 0x0200112A RID: 4394
		public class SingletonDbConnection : DelegatingDbConnection
		{
			// Token: 0x060072D1 RID: 29393 RVA: 0x0018A854 File Offset: 0x00188A54
			public SingletonDbConnection(SingletonDbConnectionPool<T> pool)
				: base(pool.connection)
			{
				this.pool = pool;
			}

			// Token: 0x1700201F RID: 8223
			// (get) Token: 0x060072D2 RID: 29394 RVA: 0x0018A86E File Offset: 0x00188A6E
			public new T InnerConnection
			{
				get
				{
					return this.pool.connection;
				}
			}

			// Token: 0x17002020 RID: 8224
			// (get) Token: 0x060072D3 RID: 29395 RVA: 0x0018A87B File Offset: 0x00188A7B
			// (set) Token: 0x060072D4 RID: 29396 RVA: 0x0018A884 File Offset: 0x00188A84
			public override string ConnectionString
			{
				get
				{
					return base.ConnectionString;
				}
				set
				{
					string sha256Hash = SingletonDbConnectionPool<T>.GetSHA256Hash(value);
					if (this.InnerConnection.State != ConnectionState.Open || this.pool.connectionStringHash != sha256Hash)
					{
						base.ConnectionString = value;
						this.pool.connectionStringHash = sha256Hash;
					}
				}
			}

			// Token: 0x060072D5 RID: 29397 RVA: 0x0018A8D1 File Offset: 0x00188AD1
			public override void Open()
			{
				if (this.InnerConnection.State != ConnectionState.Open)
				{
					base.Open();
				}
			}

			// Token: 0x060072D6 RID: 29398 RVA: 0x0018A8EC File Offset: 0x00188AEC
			public override void Close()
			{
				base.Dispose();
			}

			// Token: 0x060072D7 RID: 29399 RVA: 0x0018A8F4 File Offset: 0x00188AF4
			protected override void Dispose(bool disposing)
			{
				if (disposing && this.pool != null)
				{
					this.pool.Dispose(this);
					this.pool = null;
				}
			}

			// Token: 0x04003F49 RID: 16201
			private SingletonDbConnectionPool<T> pool;
		}
	}
}
