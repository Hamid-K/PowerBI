using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Migrations;
using System.Data.Entity.Utilities;

namespace System.Data.Entity
{
	// Token: 0x02000069 RID: 105
	public class MigrateDatabaseToLatestVersion<TContext, TMigrationsConfiguration> : IDatabaseInitializer<TContext> where TContext : DbContext where TMigrationsConfiguration : DbMigrationsConfiguration<TContext>, new()
	{
		// Token: 0x06000367 RID: 871 RVA: 0x0000C054 File Offset: 0x0000A254
		static MigrateDatabaseToLatestVersion()
		{
			DbConfigurationManager.Instance.EnsureLoadedForContext(typeof(TContext));
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000C06A File Offset: 0x0000A26A
		public MigrateDatabaseToLatestVersion()
			: this(false)
		{
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000C073 File Offset: 0x0000A273
		public MigrateDatabaseToLatestVersion(bool useSuppliedContext)
			: this(useSuppliedContext, new TMigrationsConfiguration())
		{
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000C081 File Offset: 0x0000A281
		public MigrateDatabaseToLatestVersion(bool useSuppliedContext, TMigrationsConfiguration configuration)
		{
			Check.NotNull<TMigrationsConfiguration>(configuration, "configuration");
			this._config = configuration;
			this._useSuppliedContext = useSuppliedContext;
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000C0A8 File Offset: 0x0000A2A8
		public MigrateDatabaseToLatestVersion(string connectionStringName)
		{
			Check.NotEmpty(connectionStringName, "connectionStringName");
			TMigrationsConfiguration tmigrationsConfiguration = new TMigrationsConfiguration();
			tmigrationsConfiguration.TargetDatabase = new DbConnectionInfo(connectionStringName);
			this._config = tmigrationsConfiguration;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000C0E0 File Offset: 0x0000A2E0
		public virtual void InitializeDatabase(TContext context)
		{
			Check.NotNull<TContext>(context, "context");
			new DbMigrator(this._config, this._useSuppliedContext ? context : default(TContext)).Update();
		}

		// Token: 0x040000CA RID: 202
		private readonly DbMigrationsConfiguration _config;

		// Token: 0x040000CB RID: 203
		private readonly bool _useSuppliedContext;
	}
}
