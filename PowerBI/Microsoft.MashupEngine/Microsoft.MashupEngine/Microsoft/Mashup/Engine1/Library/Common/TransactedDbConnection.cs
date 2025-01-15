using System;
using System.Data.Common;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001159 RID: 4441
	internal class TransactedDbConnection : DelegatingDbConnection
	{
		// Token: 0x06007446 RID: 29766 RVA: 0x0018F5B6 File Offset: 0x0018D7B6
		public TransactedDbConnection(IDbService service, DbConnection connection)
			: base(connection)
		{
			this.service = service;
		}

		// Token: 0x17002056 RID: 8278
		// (get) Token: 0x06007447 RID: 29767 RVA: 0x0018F5C6 File Offset: 0x0018D7C6
		public DbTransaction Transaction
		{
			get
			{
				if (this.transaction == null)
				{
					throw new InvalidOperationException("Transaction has not been started.");
				}
				if (this.concluded)
				{
					throw new InvalidOperationException("Transaction is no longer valid and cannot be reused.");
				}
				return this.transaction;
			}
		}

		// Token: 0x06007448 RID: 29768 RVA: 0x0018F5F4 File Offset: 0x0018D7F4
		public void Commit()
		{
			if (this.transaction != null)
			{
				this.concluded = true;
				this.transaction.Commit();
			}
		}

		// Token: 0x06007449 RID: 29769 RVA: 0x0018F610 File Offset: 0x0018D810
		public void Rollback()
		{
			if (this.transaction != null)
			{
				this.concluded = true;
				this.transaction.Rollback();
			}
		}

		// Token: 0x0600744A RID: 29770 RVA: 0x0018F62C File Offset: 0x0018D82C
		protected override DbCommand CreateDbCommand()
		{
			this.EnsureTransaction();
			DbCommand dbCommand = base.CreateDbCommand();
			dbCommand.Transaction = this.Transaction;
			return dbCommand;
		}

		// Token: 0x0600744B RID: 29771 RVA: 0x0017DF18 File Offset: 0x0017C118
		public override void Close()
		{
			base.Close();
		}

		// Token: 0x0600744C RID: 29772 RVA: 0x0018F646 File Offset: 0x0018D846
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.transaction != null)
			{
				this.transaction.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600744D RID: 29773 RVA: 0x0018F665 File Offset: 0x0018D865
		private void EnsureTransaction()
		{
			if (this.transaction == null)
			{
				this.transaction = base.InnerConnection.BeginTransaction();
			}
		}

		// Token: 0x04003FF7 RID: 16375
		private readonly IDbService service;

		// Token: 0x04003FF8 RID: 16376
		private DbTransaction transaction;

		// Token: 0x04003FF9 RID: 16377
		private bool concluded;
	}
}
