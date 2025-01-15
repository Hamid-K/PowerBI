using System;
using System.Data.Common;
using System.Data.Entity.Infrastructure;

namespace System.Data.Entity.Migrations.History
{
	// Token: 0x020000DB RID: 219
	public class HistoryContext : DbContext, IDbModelCacheKeyProvider
	{
		// Token: 0x060010B7 RID: 4279 RVA: 0x0002632E File Offset: 0x0002452E
		internal HistoryContext()
		{
			this.InternalContext.InitializerDisabled = true;
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x00026342 File Offset: 0x00024542
		public HistoryContext(DbConnection existingConnection, string defaultSchema)
			: base(existingConnection, false)
		{
			this._defaultSchema = defaultSchema;
			base.Configuration.ValidateOnSaveEnabled = false;
			this.InternalContext.InitializerDisabled = true;
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x060010B9 RID: 4281 RVA: 0x0002636B File Offset: 0x0002456B
		public virtual string CacheKey
		{
			get
			{
				return this._defaultSchema;
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x060010BA RID: 4282 RVA: 0x00026373 File Offset: 0x00024573
		protected string DefaultSchema
		{
			get
			{
				return this._defaultSchema;
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x060010BB RID: 4283 RVA: 0x0002637B File Offset: 0x0002457B
		// (set) Token: 0x060010BC RID: 4284 RVA: 0x00026383 File Offset: 0x00024583
		public virtual IDbSet<HistoryRow> History { get; set; }

		// Token: 0x060010BD RID: 4285 RVA: 0x0002638C File Offset: 0x0002458C
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.HasDefaultSchema(this._defaultSchema);
			modelBuilder.Entity<HistoryRow>().ToTable("__MigrationHistory");
			modelBuilder.Entity<HistoryRow>().HasKey((HistoryRow h) => new { h.MigrationId, h.ContextKey });
			modelBuilder.Entity<HistoryRow>().Property((HistoryRow h) => h.MigrationId).HasMaxLength(new int?(150))
				.IsRequired();
			modelBuilder.Entity<HistoryRow>().Property((HistoryRow h) => h.ContextKey).HasMaxLength(new int?(300))
				.IsRequired();
			modelBuilder.Entity<HistoryRow>().Property((HistoryRow h) => h.Model).IsRequired()
				.IsMaxLength();
			modelBuilder.Entity<HistoryRow>().Property((HistoryRow h) => h.ProductVersion).HasMaxLength(new int?(32))
				.IsRequired();
		}

		// Token: 0x040008B0 RID: 2224
		public const string DefaultTableName = "__MigrationHistory";

		// Token: 0x040008B1 RID: 2225
		internal const int ContextKeyMaxLength = 300;

		// Token: 0x040008B2 RID: 2226
		internal const int MigrationIdMaxLength = 150;

		// Token: 0x040008B3 RID: 2227
		private readonly string _defaultSchema;

		// Token: 0x040008B4 RID: 2228
		internal static readonly Func<DbConnection, string, HistoryContext> DefaultFactory = (DbConnection e, string d) => new HistoryContext(e, d);
	}
}
