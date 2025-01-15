using System;
using System.Data.Common;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000264 RID: 612
	public class TransactionContext : DbContext
	{
		// Token: 0x06001F14 RID: 7956 RVA: 0x00056748 File Offset: 0x00054948
		public TransactionContext(DbConnection existingConnection)
			: base(existingConnection, false)
		{
			base.Configuration.ValidateOnSaveEnabled = false;
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06001F15 RID: 7957 RVA: 0x0005675E File Offset: 0x0005495E
		// (set) Token: 0x06001F16 RID: 7958 RVA: 0x00056766 File Offset: 0x00054966
		public virtual IDbSet<TransactionRow> Transactions { get; set; }

		// Token: 0x06001F17 RID: 7959 RVA: 0x0005676F File Offset: 0x0005496F
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<TransactionRow>().ToTable("__TransactionHistory");
		}

		// Token: 0x04000B45 RID: 2885
		private const string _defaultTableName = "__TransactionHistory";
	}
}
