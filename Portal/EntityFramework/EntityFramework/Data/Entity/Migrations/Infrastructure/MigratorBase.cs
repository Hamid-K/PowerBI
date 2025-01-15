using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Migrations.Sql;
using System.Diagnostics;
using System.Xml.Linq;

namespace System.Data.Entity.Migrations.Infrastructure
{
	// Token: 0x020000D6 RID: 214
	[DebuggerStepThrough]
	public abstract class MigratorBase
	{
		// Token: 0x0600107A RID: 4218 RVA: 0x0002568C File Offset: 0x0002388C
		protected MigratorBase(MigratorBase innerMigrator)
		{
			if (innerMigrator == null)
			{
				this._this = this;
				return;
			}
			this._this = innerMigrator;
			MigratorBase migratorBase = innerMigrator;
			while (migratorBase._this != innerMigrator)
			{
				migratorBase = migratorBase._this;
			}
			migratorBase._this = this;
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x000256CC File Offset: 0x000238CC
		public virtual IEnumerable<string> GetPendingMigrations()
		{
			return this._this.GetPendingMigrations();
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x0600107C RID: 4220 RVA: 0x000256D9 File Offset: 0x000238D9
		public virtual DbMigrationsConfiguration Configuration
		{
			get
			{
				return this._this.Configuration;
			}
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x000256E6 File Offset: 0x000238E6
		public void Update()
		{
			this.Update(null);
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x000256EF File Offset: 0x000238EF
		public virtual void Update(string targetMigration)
		{
			this._this.Update(targetMigration);
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x000256FD File Offset: 0x000238FD
		internal virtual string GetMigrationId(string migration)
		{
			return this._this.GetMigrationId(migration);
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x0002570B File Offset: 0x0002390B
		public virtual IEnumerable<string> GetLocalMigrations()
		{
			return this._this.GetLocalMigrations();
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x00025718 File Offset: 0x00023918
		public virtual IEnumerable<string> GetDatabaseMigrations()
		{
			return this._this.GetDatabaseMigrations();
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x00025725 File Offset: 0x00023925
		internal virtual void AutoMigrate(string migrationId, VersionedModel sourceModel, VersionedModel targetModel, bool downgrading)
		{
			this._this.AutoMigrate(migrationId, sourceModel, targetModel, downgrading);
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x00025737 File Offset: 0x00023937
		internal virtual void ApplyMigration(DbMigration migration, DbMigration lastMigration)
		{
			this._this.ApplyMigration(migration, lastMigration);
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x00025746 File Offset: 0x00023946
		internal virtual void EnsureDatabaseExists(Action mustSucceedToKeepDatabase)
		{
			this._this.EnsureDatabaseExists(mustSucceedToKeepDatabase);
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x00025754 File Offset: 0x00023954
		internal virtual void RevertMigration(string migrationId, DbMigration migration, XDocument targetModel)
		{
			this._this.RevertMigration(migrationId, migration, targetModel);
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x00025764 File Offset: 0x00023964
		internal virtual void SeedDatabase()
		{
			this._this.SeedDatabase();
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x00025771 File Offset: 0x00023971
		internal virtual void ExecuteStatements(IEnumerable<MigrationStatement> migrationStatements)
		{
			this._this.ExecuteStatements(migrationStatements);
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x0002577F File Offset: 0x0002397F
		internal virtual IEnumerable<MigrationStatement> GenerateStatements(IList<MigrationOperation> operations, string migrationId)
		{
			return this._this.GenerateStatements(operations, migrationId);
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x0002578E File Offset: 0x0002398E
		internal virtual IEnumerable<DbQueryCommandTree> CreateDiscoveryQueryTrees()
		{
			return this._this.CreateDiscoveryQueryTrees();
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x0002579B File Offset: 0x0002399B
		internal virtual void ExecuteSql(MigrationStatement migrationStatement, DbConnection connection, DbTransaction transaction, DbInterceptionContext interceptionContext)
		{
			this._this.ExecuteSql(migrationStatement, connection, transaction, interceptionContext);
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x000257AD File Offset: 0x000239AD
		internal virtual void Upgrade(IEnumerable<string> pendingMigrations, string targetMigrationId, string lastMigrationId)
		{
			this._this.Upgrade(pendingMigrations, targetMigrationId, lastMigrationId);
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x000257BD File Offset: 0x000239BD
		internal virtual void Downgrade(IEnumerable<string> pendingMigrations)
		{
			this._this.Downgrade(pendingMigrations);
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x000257CB File Offset: 0x000239CB
		internal virtual void UpgradeHistory(IEnumerable<MigrationOperation> upgradeOperations)
		{
			this._this.UpgradeHistory(upgradeOperations);
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x0600108E RID: 4238 RVA: 0x000257D9 File Offset: 0x000239D9
		internal virtual string TargetDatabase
		{
			get
			{
				return this._this.TargetDatabase;
			}
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x000257E6 File Offset: 0x000239E6
		internal virtual bool HistoryExists()
		{
			return this._this.HistoryExists();
		}

		// Token: 0x040008A6 RID: 2214
		private MigratorBase _this;
	}
}
