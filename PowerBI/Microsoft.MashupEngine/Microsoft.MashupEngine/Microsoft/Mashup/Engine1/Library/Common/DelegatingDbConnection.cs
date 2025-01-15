using System;
using System.Data;
using System.Data.Common;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x020010AF RID: 4271
	internal abstract class DelegatingDbConnection : DbConnection
	{
		// Token: 0x06006FB1 RID: 28593 RVA: 0x00181525 File Offset: 0x0017F725
		public DelegatingDbConnection(DbConnection connection)
		{
			this.baseConnection = connection;
		}

		// Token: 0x17001F72 RID: 8050
		// (get) Token: 0x06006FB2 RID: 28594 RVA: 0x00181534 File Offset: 0x0017F734
		public DbConnection InnerConnection
		{
			get
			{
				return this.baseConnection;
			}
		}

		// Token: 0x17001F73 RID: 8051
		// (get) Token: 0x06006FB3 RID: 28595 RVA: 0x0018153C File Offset: 0x0017F73C
		public override string DataSource
		{
			get
			{
				return this.baseConnection.DataSource;
			}
		}

		// Token: 0x17001F74 RID: 8052
		// (get) Token: 0x06006FB4 RID: 28596 RVA: 0x00181549 File Offset: 0x0017F749
		public override string Database
		{
			get
			{
				return this.baseConnection.Database;
			}
		}

		// Token: 0x17001F75 RID: 8053
		// (get) Token: 0x06006FB5 RID: 28597 RVA: 0x00181556 File Offset: 0x0017F756
		public override string ServerVersion
		{
			get
			{
				return this.baseConnection.ServerVersion;
			}
		}

		// Token: 0x17001F76 RID: 8054
		// (get) Token: 0x06006FB6 RID: 28598 RVA: 0x00181563 File Offset: 0x0017F763
		public override ConnectionState State
		{
			get
			{
				return this.baseConnection.State;
			}
		}

		// Token: 0x17001F77 RID: 8055
		// (get) Token: 0x06006FB7 RID: 28599 RVA: 0x00181570 File Offset: 0x0017F770
		public override int ConnectionTimeout
		{
			get
			{
				return this.baseConnection.ConnectionTimeout;
			}
		}

		// Token: 0x06006FB8 RID: 28600 RVA: 0x0018157D File Offset: 0x0017F77D
		protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
		{
			return this.baseConnection.BeginTransaction(isolationLevel);
		}

		// Token: 0x06006FB9 RID: 28601 RVA: 0x0018158B File Offset: 0x0017F78B
		public override void ChangeDatabase(string databaseName)
		{
			this.baseConnection.ChangeDatabase(databaseName);
		}

		// Token: 0x06006FBA RID: 28602 RVA: 0x00181599 File Offset: 0x0017F799
		public override void Close()
		{
			this.baseConnection.Close();
		}

		// Token: 0x17001F78 RID: 8056
		// (get) Token: 0x06006FBB RID: 28603 RVA: 0x001815A6 File Offset: 0x0017F7A6
		// (set) Token: 0x06006FBC RID: 28604 RVA: 0x001815B3 File Offset: 0x0017F7B3
		public override string ConnectionString
		{
			get
			{
				return this.baseConnection.ConnectionString;
			}
			set
			{
				this.baseConnection.ConnectionString = value;
			}
		}

		// Token: 0x06006FBD RID: 28605 RVA: 0x001815C1 File Offset: 0x0017F7C1
		protected override DbCommand CreateDbCommand()
		{
			return this.baseConnection.CreateCommand();
		}

		// Token: 0x06006FBE RID: 28606 RVA: 0x001815CE File Offset: 0x0017F7CE
		public override void Open()
		{
			this.baseConnection.Open();
		}

		// Token: 0x06006FBF RID: 28607 RVA: 0x001815DB File Offset: 0x0017F7DB
		public override DataTable GetSchema()
		{
			return this.baseConnection.GetSchema();
		}

		// Token: 0x06006FC0 RID: 28608 RVA: 0x001815E8 File Offset: 0x0017F7E8
		public override DataTable GetSchema(string collectionName)
		{
			return this.baseConnection.GetSchema(collectionName);
		}

		// Token: 0x06006FC1 RID: 28609 RVA: 0x001815F6 File Offset: 0x0017F7F6
		public override DataTable GetSchema(string collectionName, string[] restrictionValues)
		{
			return this.baseConnection.GetSchema(collectionName, restrictionValues);
		}

		// Token: 0x06006FC2 RID: 28610 RVA: 0x00181605 File Offset: 0x0017F805
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.baseConnection != null)
			{
				this.baseConnection.Dispose();
				this.baseConnection = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x04003DF2 RID: 15858
		private DbConnection baseConnection;
	}
}
