using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Migrations.Sql;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Migrations.Infrastructure
{
	// Token: 0x020000D8 RID: 216
	public class MigratorScriptingDecorator : MigratorBase
	{
		// Token: 0x0600109A RID: 4250 RVA: 0x000259E0 File Offset: 0x00023BE0
		public MigratorScriptingDecorator(MigratorBase innerMigrator)
			: base(innerMigrator)
		{
			Check.NotNull<MigratorBase>(innerMigrator, "innerMigrator");
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x00025A00 File Offset: 0x00023C00
		public string ScriptUpdate(string sourceMigration, string targetMigration)
		{
			this._sqlBuilder.Clear();
			if (string.IsNullOrWhiteSpace(sourceMigration))
			{
				this.Update(targetMigration);
			}
			else
			{
				if (sourceMigration.IsAutomaticMigration())
				{
					throw Error.AutoNotValidForScriptWindows(sourceMigration);
				}
				string sourceMigrationId = this.GetMigrationId(sourceMigration);
				IEnumerable<string> enumerable = from m in this.GetLocalMigrations()
					where string.CompareOrdinal(m, sourceMigrationId) > 0
					select m;
				string targetMigrationId = null;
				if (!string.IsNullOrWhiteSpace(targetMigration))
				{
					if (targetMigration.IsAutomaticMigration())
					{
						throw Error.AutoNotValidForScriptWindows(targetMigration);
					}
					targetMigrationId = this.GetMigrationId(targetMigration);
					if (string.CompareOrdinal(sourceMigrationId, targetMigrationId) > 0)
					{
						throw Error.DownScriptWindowsNotSupported();
					}
					enumerable = enumerable.Where((string m) => string.CompareOrdinal(m, targetMigrationId) <= 0);
				}
				this._updateDatabaseOperation = ((sourceMigration == "0") ? new UpdateDatabaseOperation(base.CreateDiscoveryQueryTrees().ToList<DbQueryCommandTree>()) : null);
				this.Upgrade(enumerable, targetMigrationId, sourceMigrationId);
				if (this._updateDatabaseOperation != null)
				{
					this.ExecuteStatements(base.GenerateStatements(new UpdateDatabaseOperation[] { this._updateDatabaseOperation }, null));
				}
			}
			return this._sqlBuilder.ToString();
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x00025B27 File Offset: 0x00023D27
		internal override IEnumerable<MigrationStatement> GenerateStatements(IList<MigrationOperation> operations, string migrationId)
		{
			if (this._updateDatabaseOperation == null)
			{
				return base.GenerateStatements(operations, migrationId);
			}
			this._updateDatabaseOperation.AddMigration(migrationId, operations);
			return Enumerable.Empty<MigrationStatement>();
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x00025B4C File Offset: 0x00023D4C
		internal override void EnsureDatabaseExists(Action mustSucceedToKeepDatabase)
		{
			mustSucceedToKeepDatabase();
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x00025B54 File Offset: 0x00023D54
		internal override void ExecuteStatements(IEnumerable<MigrationStatement> migrationStatements)
		{
			MigratorScriptingDecorator.BuildSqlScript(migrationStatements, this._sqlBuilder);
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x00025B64 File Offset: 0x00023D64
		internal static void BuildSqlScript(IEnumerable<MigrationStatement> migrationStatements, StringBuilder sqlBuilder)
		{
			foreach (MigrationStatement migrationStatement in migrationStatements)
			{
				if (!string.IsNullOrWhiteSpace(migrationStatement.Sql))
				{
					if (!string.IsNullOrWhiteSpace(migrationStatement.BatchTerminator) && sqlBuilder.Length > 0)
					{
						sqlBuilder.AppendLine(migrationStatement.BatchTerminator);
						sqlBuilder.AppendLine();
					}
					sqlBuilder.AppendLine(migrationStatement.Sql);
				}
			}
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x00025BEC File Offset: 0x00023DEC
		internal override void SeedDatabase()
		{
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x00025BEE File Offset: 0x00023DEE
		internal override bool HistoryExists()
		{
			return false;
		}

		// Token: 0x040008A9 RID: 2217
		private readonly StringBuilder _sqlBuilder = new StringBuilder();

		// Token: 0x040008AA RID: 2218
		private UpdateDatabaseOperation _updateDatabaseOperation;
	}
}
