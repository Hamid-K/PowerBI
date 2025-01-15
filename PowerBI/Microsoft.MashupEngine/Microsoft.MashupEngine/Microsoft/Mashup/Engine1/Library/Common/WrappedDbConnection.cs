using System;
using System.Data;
using System.Data.Common;
using System.Globalization;
using Microsoft.Mashup.Common;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x0200107D RID: 4221
	internal sealed class WrappedDbConnection : DelegatingDbConnection
	{
		// Token: 0x06006E8F RID: 28303 RVA: 0x0017DC87 File Offset: 0x0017BE87
		public WrappedDbConnection(DbEnvironment environment, DbConnection baseConnection, Func<IDisposable> impersonate)
			: base(baseConnection)
		{
			this.environment = environment;
			this.impersonate = impersonate;
		}

		// Token: 0x06006E90 RID: 28304 RVA: 0x0017DCA0 File Offset: 0x0017BEA0
		protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
		{
			DbTransaction dbTransaction;
			using (this.impersonate())
			{
				dbTransaction = base.BeginDbTransaction(isolationLevel);
			}
			return dbTransaction;
		}

		// Token: 0x06006E91 RID: 28305 RVA: 0x0017DCE0 File Offset: 0x0017BEE0
		public override void ChangeDatabase(string databaseName)
		{
			using (this.impersonate())
			{
				base.ChangeDatabase(databaseName);
			}
		}

		// Token: 0x06006E92 RID: 28306 RVA: 0x0017DD1C File Offset: 0x0017BF1C
		public override void Close()
		{
			using (this.impersonate())
			{
				SafeExceptions.IgnoreSafeExceptions(this.environment.Host, string.Format(CultureInfo.InvariantCulture, "Engine/IO/WrappedDbConnection/{0}/Close", this.environment.DataSourceNameString), delegate
				{
					base.Close();
				});
			}
		}

		// Token: 0x06006E93 RID: 28307 RVA: 0x0017DD88 File Offset: 0x0017BF88
		protected override DbCommand CreateDbCommand()
		{
			DbCommand dbCommand;
			using (this.impersonate())
			{
				dbCommand = new WrappedDbCommand(this.environment, base.InnerConnection.CreateCommand(), this.impersonate);
			}
			return dbCommand;
		}

		// Token: 0x06006E94 RID: 28308 RVA: 0x0017DDDC File Offset: 0x0017BFDC
		public override void Open()
		{
			using (this.impersonate())
			{
				base.Open();
			}
		}

		// Token: 0x06006E95 RID: 28309 RVA: 0x0017DE18 File Offset: 0x0017C018
		public override DataTable GetSchema()
		{
			DataTable schema;
			using (this.impersonate())
			{
				schema = base.GetSchema();
			}
			return schema;
		}

		// Token: 0x06006E96 RID: 28310 RVA: 0x0017DE58 File Offset: 0x0017C058
		public override DataTable GetSchema(string collectionName)
		{
			DataTable schema;
			using (this.impersonate())
			{
				schema = base.GetSchema(collectionName);
			}
			return schema;
		}

		// Token: 0x06006E97 RID: 28311 RVA: 0x0017DE98 File Offset: 0x0017C098
		public override DataTable GetSchema(string collectionName, string[] restrictionValues)
		{
			DataTable schema;
			using (this.impersonate())
			{
				schema = base.GetSchema(collectionName, restrictionValues);
			}
			return schema;
		}

		// Token: 0x06006E98 RID: 28312 RVA: 0x0017DED8 File Offset: 0x0017C0D8
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				using (this.impersonate())
				{
					base.Dispose(disposing);
				}
			}
		}

		// Token: 0x04003D5C RID: 15708
		private readonly DbEnvironment environment;

		// Token: 0x04003D5D RID: 15709
		private readonly Func<IDisposable> impersonate;
	}
}
