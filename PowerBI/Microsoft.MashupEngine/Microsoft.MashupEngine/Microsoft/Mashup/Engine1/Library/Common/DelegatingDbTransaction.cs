using System;
using System.Data;
using System.Data.Common;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x020010B2 RID: 4274
	internal abstract class DelegatingDbTransaction : DbTransaction
	{
		// Token: 0x06006FF5 RID: 28661 RVA: 0x001818E7 File Offset: 0x0017FAE7
		public DelegatingDbTransaction(DbTransaction transaction)
		{
			this.transaction = transaction;
		}

		// Token: 0x17001F84 RID: 8068
		// (get) Token: 0x06006FF6 RID: 28662 RVA: 0x001818F6 File Offset: 0x0017FAF6
		public DbTransaction InnerTransaction
		{
			get
			{
				return this.transaction;
			}
		}

		// Token: 0x17001F85 RID: 8069
		// (get) Token: 0x06006FF7 RID: 28663 RVA: 0x001818FE File Offset: 0x0017FAFE
		protected override DbConnection DbConnection
		{
			get
			{
				return this.transaction.Connection;
			}
		}

		// Token: 0x17001F86 RID: 8070
		// (get) Token: 0x06006FF8 RID: 28664 RVA: 0x0018190B File Offset: 0x0017FB0B
		public override IsolationLevel IsolationLevel
		{
			get
			{
				return this.transaction.IsolationLevel;
			}
		}

		// Token: 0x06006FF9 RID: 28665 RVA: 0x00181918 File Offset: 0x0017FB18
		public override void Commit()
		{
			this.transaction.Commit();
		}

		// Token: 0x06006FFA RID: 28666 RVA: 0x00181925 File Offset: 0x0017FB25
		public override void Rollback()
		{
			this.transaction.Rollback();
		}

		// Token: 0x06006FFB RID: 28667 RVA: 0x00181932 File Offset: 0x0017FB32
		protected override void Dispose(bool disposing)
		{
			this.transaction.Dispose();
		}

		// Token: 0x04003DF5 RID: 15861
		private readonly DbTransaction transaction;
	}
}
