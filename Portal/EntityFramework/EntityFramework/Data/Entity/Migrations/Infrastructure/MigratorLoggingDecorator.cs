using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Migrations.Sql;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Xml.Linq;

namespace System.Data.Entity.Migrations.Infrastructure
{
	// Token: 0x020000D7 RID: 215
	public class MigratorLoggingDecorator : MigratorBase
	{
		// Token: 0x06001090 RID: 4240 RVA: 0x000257F3 File Offset: 0x000239F3
		public MigratorLoggingDecorator(MigratorBase innerMigrator, MigrationsLogger logger)
			: base(innerMigrator)
		{
			Check.NotNull<MigratorBase>(innerMigrator, "innerMigrator");
			Check.NotNull<MigrationsLogger>(logger, "logger");
			this._logger = logger;
			this._logger.Verbose(Strings.LoggingTargetDatabase(base.TargetDatabase));
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x00025831 File Offset: 0x00023A31
		internal override void AutoMigrate(string migrationId, VersionedModel sourceModel, VersionedModel targetModel, bool downgrading)
		{
			this._logger.Info(downgrading ? Strings.LoggingRevertAutoMigrate(migrationId) : Strings.LoggingAutoMigrate(migrationId));
			base.AutoMigrate(migrationId, sourceModel, targetModel, downgrading);
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x0002585C File Offset: 0x00023A5C
		internal override void ExecuteSql(MigrationStatement migrationStatement, DbConnection connection, DbTransaction transaction, DbInterceptionContext interceptionContext)
		{
			this._logger.Verbose(migrationStatement.Sql);
			DbProviderServices providerServices = DbProviderServices.GetProviderServices(connection);
			if (providerServices != null)
			{
				providerServices.RegisterInfoMessageHandler(connection, delegate(string message)
				{
					if (!string.Equals(message, this._lastInfoMessage, StringComparison.OrdinalIgnoreCase))
					{
						this._logger.Warning(message);
						this._lastInfoMessage = message;
					}
				});
			}
			base.ExecuteSql(migrationStatement, connection, transaction, interceptionContext);
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x000258A4 File Offset: 0x00023AA4
		internal override void Upgrade(IEnumerable<string> pendingMigrations, string targetMigrationId, string lastMigrationId)
		{
			int num = pendingMigrations.Count<string>();
			this._logger.Info((num > 0) ? Strings.LoggingPendingMigrations(num, pendingMigrations.Join(null, ", ")) : (string.IsNullOrWhiteSpace(targetMigrationId) ? Strings.LoggingNoExplicitMigrations : Strings.LoggingAlreadyAtTarget(targetMigrationId)));
			base.Upgrade(pendingMigrations, targetMigrationId, lastMigrationId);
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x00025900 File Offset: 0x00023B00
		internal override void Downgrade(IEnumerable<string> pendingMigrations)
		{
			IEnumerable<string> enumerable = pendingMigrations.Take(pendingMigrations.Count<string>() - 1);
			this._logger.Info(Strings.LoggingPendingMigrationsDown(enumerable.Count<string>(), enumerable.Join(null, ", ")));
			base.Downgrade(pendingMigrations);
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x0002594A File Offset: 0x00023B4A
		internal override void ApplyMigration(DbMigration migration, DbMigration lastMigration)
		{
			this._logger.Info(Strings.LoggingApplyMigration(((IMigrationMetadata)migration).Id));
			base.ApplyMigration(migration, lastMigration);
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x0002596F File Offset: 0x00023B6F
		internal override void RevertMigration(string migrationId, DbMigration migration, XDocument targetModel)
		{
			this._logger.Info(Strings.LoggingRevertMigration(migrationId));
			base.RevertMigration(migrationId, migration, targetModel);
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x0002598B File Offset: 0x00023B8B
		internal override void SeedDatabase()
		{
			this._logger.Info(Strings.LoggingSeedingDatabase);
			base.SeedDatabase();
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x000259A3 File Offset: 0x00023BA3
		internal override void UpgradeHistory(IEnumerable<MigrationOperation> upgradeOperations)
		{
			this._logger.Info(Strings.UpgradingHistoryTable);
			base.UpgradeHistory(upgradeOperations);
		}

		// Token: 0x040008A7 RID: 2215
		private readonly MigrationsLogger _logger;

		// Token: 0x040008A8 RID: 2216
		private string _lastInfoMessage;
	}
}
